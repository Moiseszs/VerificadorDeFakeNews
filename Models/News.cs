using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class News
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Title { get; set;}

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Url { get; set; }

        [JsonInclude]
        public List<CheckingSource> checkingSources;

        public News()
        {
            checkingSources = new List<CheckingSource>();
        }
    }
}
