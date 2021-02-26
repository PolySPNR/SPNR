using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SPNR.Core.Models.AuthorInfo
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Required] public string Name { get; set; }

        public List<Author> Authors { get; } = new();

        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; }
    }
}