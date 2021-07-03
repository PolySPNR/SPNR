using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SPNR.Core.Models.AuthorInfo
{
    public class Department
    {
        /// <summary>
        /// Numeric ID of department
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Local name of department
        /// </summary>
        [Required] public string Name { get; set; }

        /// <summary>
        /// Authors in the department
        /// </summary>
        public List<Author> Authors { get; } = new();

        /// <summary>
        /// Numeric ID of parent faculty
        /// </summary>
        public int FacultyId { get; set; }
        
        /// <summary>
        /// Parent faculty
        /// </summary>
        [JsonIgnore]
        public Faculty Faculty { get; set; }
    }
}