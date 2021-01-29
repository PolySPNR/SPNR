using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using SPNR.Core.Models.Works;

namespace SPNR.Core.Models.AuthorInfo
{
    public class Author
    {
        public int AuthorId { get; set; }
        
        [Required]
        public string Name { get; set; }
        public string NameEnglish { get; set; }
        
        [Required]
        public bool Hourly { get; set; }
        
        // org > fac > dep > author
        
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        
        public int PositionId { get; set; }
        public Position Position { get; set; }
        
        public List<ScientificWork> ScientificWorks { get; } = new();
    }
}