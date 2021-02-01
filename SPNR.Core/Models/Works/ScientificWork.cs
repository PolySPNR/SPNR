using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using SPNR.Core.Models.AuthorInfo;
using SPNR.Core.Models.Works.Fields;
using SPNR.Core.Models.Works.PublishData;

namespace SPNR.Core.Models.Works
{
    public class ScientificWork
    {
        public int ScientificWorkId { get; set; }
        public List<Author> Authors { get; set; }
        public string WorkName { get; set; }
        public string DigitalObjectIdentifier { get; set; }

        public ELibInfo ELibInfo { get; set; }
        
        [Column(TypeName = "jsonb")]
        public Dictionary<string, string> PublicationInfo { get; set; }
        [Column(TypeName = "jsonb")]
        public Dictionary<string, List<string>> PublicationMeta { get; set; }
        
    }
}