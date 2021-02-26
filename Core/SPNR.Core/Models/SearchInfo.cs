using System;
using System.Collections.Generic;

namespace SPNR.Core.Models
{
    public class SearchInfo
    {
        public List<string> Authors { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}