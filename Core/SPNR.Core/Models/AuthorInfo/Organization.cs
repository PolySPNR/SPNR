using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SPNR.Core.Models.AuthorInfo
{
    [Index(nameof(Name), IsUnique = true)]
    public class Organization
    {
        /// <summary>
        /// Numeric ID of organization
        /// </summary>
        public int OrganizationId { get; set; }

        /// <summary>
        /// Local name of organization
        /// </summary>
        [Required] public string Name { get; set; }
        
        /// <summary>
        /// Internationalized name of organization
        /// </summary>
        [Required] public string IntName { get; set; }

        /// <summary>
        /// List of faculties organization has
        /// </summary>
        public List<Faculty> Faculties { get; set; } = new();
    }
}