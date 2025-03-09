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

        public string Veridict { get; set; }

        [JsonIgnore]
        public News News { get; set; }

        public string headline { get; set; }

        public string SourceSite { get; set; }

        public string link { get; set; }
        
        public List<string>? relatedHeadlines { get; set; }
    }
}
