using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SPNR.Core.Models.AuthorInfo
{
    [Index(nameof(Name), IsUnique = true)]
    public class Faculty
    {
        public int FacultyId { get; set; }
        
        [Required]
        public string Name { get; set; }

        public List<Department> Departments { get; } = new();
        
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
    }
}