using System;

namespace SPNR.Core.Models
{
    public class SearchInfo
    {
        public string AuthorName { get; set; }
        public string WorkName { get; set; }
        public int MaxWorks { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}