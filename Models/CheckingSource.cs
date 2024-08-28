using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class CheckingSource
    {
        [Key]
        public long Id { get; set; }

        public string Veridict { get; set; }

        public long NewsId { get; set; }

        [JsonIgnore]
        public News News { get; set; }

        public string SourceSite { get; set; }

        public string link { get; set; }
        
        public List<string>? relatedHeadlines { get; set; }
    }
}
