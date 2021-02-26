namespace SPNR.Core.Models.Works.PublishData
{
    public class JournalPublish
    {
        public int JournalPublishId { get; set; }
        public string Name { get; set; }
        public string ISSN { get; set; }
        public string Number { get; set; }
        public int Year { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        public int ScientificWorkId { get; set; }
        public ScientificWork ScientificWork { get; set; }
    }
}