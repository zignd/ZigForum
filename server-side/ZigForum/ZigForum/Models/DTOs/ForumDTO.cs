using FluentValidation.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZigForum.Models.Validators;

namespace ZigForum.Models.DTOs
{
    public class ForumDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("parent_id")]
        public int? ParentId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("created")]
        public DateTime? Created { get; set; }

        [JsonProperty("parent")]
        public ForumDTO Parent { get; set; }

        [JsonProperty("subforums")]
        public List<ForumDTO> SubForums { get; set; }

        [JsonProperty("posts")]
        public List<PostDTO> Posts { get; set; }
    }
}
