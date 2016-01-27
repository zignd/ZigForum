using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using ZigForum.Controllers;
using ZigForum.Models;
using ZigForum.Models.DTOs;
using ZigForum.Tests.Common;

namespace ZigForum.Tests
{
    [TestClass]
    public class TestForumsController
    {
        [TestMethod]
        public async Task GetAllForums_ShouldReturnAllForums()
        {
            var data = new List<Forum>
            {
                new Forum { Id = 1, Name = "Forum 1", Created = DateTime.Now },
                new Forum { Id = 2, Name = "Forum 2", Created = DateTime.Now },
                new Forum { Id = 3, Name = "Forum 3", Created = DateTime.Now },
                new Forum { Id = 4, Name = "Forum 4", Created = DateTime.Now }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Forum>>();
            mockSet.As<IDbAsyncEnumerable<Forum>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Forum>(data.GetEnumerator()));

            mockSet.As<IQueryable<Forum>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Forum>(data.Provider));

            mockSet.As<IQueryable<Forum>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Forum>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Forum>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Forums).Returns(mockSet.Object);

            var controller = new ForumsController(mockContext.Object);
            var result = await controller.Get();

            var okNegotiated = (OkNegotiatedContentResult<ForumDTO[]>)result;
            Assert.AreEqual(okNegotiated.Content.AsQueryable().ElementAt(0).Id, data.ElementAt(0).Id);
        }
    }
}
