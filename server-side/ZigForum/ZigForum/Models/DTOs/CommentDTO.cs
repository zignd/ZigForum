using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZigForum.Models.DTOs
{
    public class CommentDTO
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("post_id")]
        public int? PostId { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("parent_id")]
        public int? ParentId { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("updated")]
        public DateTime? Updated { get; set; }

        [JsonProperty("created")]
        public DateTime? Created { get; set; }

        [JsonProperty("post")]
        public PostDTO Post { get; set; }

        [JsonProperty("user")]
        public UserDTO User { get; set; }

        [JsonProperty("parent")]
        public CommentDTO Parent { get; set; }

        [JsonProperty("children")]
        public List<CommentDTO> Children { get; set; }
    }
}