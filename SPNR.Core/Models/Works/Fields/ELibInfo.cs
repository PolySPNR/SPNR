using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPNR.Core.Models.Works.Fields
{
    public class ELibInfo
    {
        public int ELibInfoId { get; set; }

        public bool RINC { get; set; }
        public bool RINCCore { get; set; }
        public bool Scopus { get; set; }
        public bool WebOfScience { get; set; }
        
        [Column(TypeName = "jsonb")]
        public Dictionary<string, string> RawMetrics { get; set; }

        public int ScientificWorkId { get; set; }
        public ScientificWork ScientificWork { get; set; }
    }
}