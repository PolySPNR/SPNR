using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace SPNR.Core.Models.AuthorInfo
{
    [Index(nameof(Name), IsUnique = true)]
    public class Faculty
    {
        /// <summary>
        /// Numeric id of faculty
        /// </summary>
        public int FacultyId { get; set; }

        /// <summary>
        /// Local name of faculty
        /// </summary>
        [Required] public string Name { get; set; }

        /// <summary>
        /// List of departments of faculty
        /// </summary>
        public List<Department> Departments { get; } = new();

        /// <summary>
        /// Parent organization's numeric ID
        /// </summary>
        public int OrganizationId { get; set; }
        
        /// <summary>
        /// Parent organization
        /// </summary>
        [JsonIgnore]
        public Organization Organization { get; set; }
    }
}