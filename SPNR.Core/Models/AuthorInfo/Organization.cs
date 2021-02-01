using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SPNR.Core.Models.AuthorInfo
{
    [Index(nameof(Name), IsUnique = true)]
    public class Organization
    {
        public int OrganizationId { get; set; }

        [Required] public string Name { get; set; }

        public List<Faculty> Faculties { get; set; } = new();
    }
}