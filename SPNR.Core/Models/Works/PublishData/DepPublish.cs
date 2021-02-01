using System;

namespace SPNR.Core.Models.Works.PublishData
{
    public class DepPublish
    {
        public int DepPublishId { get; set; }

        public string Number { get; set; }
        public int PageNumber { get; set; }
        public string Place { get; set; }
        public DateTime DepDate { get; set; }
        public string GRNTI { get; set; }

        public int ScientificWorkId { get; set; }
        public ScientificWork ScientificWork { get; set; }
    }
}