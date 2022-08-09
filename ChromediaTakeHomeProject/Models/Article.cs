using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChromediaTakeHomeProject.Models
{
    public class Article
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("num_comments")]
        public int? CommentCount { get; set; }

        [JsonProperty("story_id")]
        public int? StoryId { get; set; }

        [JsonProperty("story_title")]
        public string StoryTitle { get; set; }

        [JsonProperty("story_url")]
        public string StoryUrl { get; set; }

        [JsonProperty("parent_id")]
        public int? ParentId { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        [JsonIgnore]
        public string FinalTitle
        {
            get => Title ?? StoryTitle;
        }
    }
}
