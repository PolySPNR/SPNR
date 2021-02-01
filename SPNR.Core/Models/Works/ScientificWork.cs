using System;
using System.Collections.Generic;
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
        public DateTime PublicationDate { get; set; }
        public PublishType PublishType { get; set; }
        public string DigitalObjectIdentifier { get; set; }

        // WebOfScience, Scopus

        public ELibInfo ELibInfo { get; set; }
        public JournalPublish JournalPublish { get; set; }
    }
}