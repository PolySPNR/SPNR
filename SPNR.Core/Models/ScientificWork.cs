using System;

namespace SPNR.Core.Models
{
    public class ScientificWork
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string WorkName { get; set; }
        public string Source { get; set; }
        public string SourceId { get; set; }
        public DateTime PublicationDate { get; set; }
        public byte[] WorkData { get; set; }
        public string WorkDataFormat { get; set; }
    }
}