using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IAccessSearch.Models
{
    public class TSearchString
    {
        [Key] //make ID column as Primary Key and Indexed
        public string ID { get; set; }
        public string Content { get; set; }
    }

    public class TSearchStringView
    {
        public string ID { get; set; }
        public string Content { get; set; }
        public int? MatchNo { get; set; } //make it nullable
    }
}
