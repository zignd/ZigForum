using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigForum.Models.DTOs
{
    public class PostDTO
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("forum_id")]
        public int? ForumId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("is_locked")]
        public bool? IsLocked { get; set; }

        [JsonProperty("locked_reason")]
        public string LockedReason { get; set; }

        [JsonProperty("updated")]
        public DateTime? Updated { get; set; }

        [JsonProperty("created")]
        public DateTime? Created { get; set; }

        [JsonProperty("user")]
        public UserDTO User { get; set; }

        [JsonProperty("forum")]
        public ForumDTO Forum { get; set; }
    }
}
