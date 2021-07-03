using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SPNR.Core.Models.Works;

namespace SPNR.Core.Models.AuthorInfo
{
    public class Author
    {
        /// <summary>
        /// Numeric ID of author
        /// </summary>
        public int AuthorId { get; set; }

        /// <summary>
        /// Local full name of author
        /// </summary>
        [Required] public string Name { get; set; }

        /// <summary>
        /// Internalized name of author
        /// </summary>
        public string NameEnglish { get; set; }

        /// <summary>
        /// Is author has hourly paid job in a organization
        /// </summary>
        [Required] public bool Hourly { get; set; }

        // org > fac > dep > author
        
        public int OrganizationId { get; set; }
        
        [JsonIgnore]
        public Organization Organization { get; set; }

        public int FacultyId { get; set; }
        
        [JsonIgnore]
        public Faculty Faculty { get; set; }

        public int DepartmentId { get; set; }
        
        [JsonIgnore]
        public Department Department { get; set; }

        public int PositionId { get; set; }
        
        [JsonIgnore]
        public Position Position { get; set; }
        
        /// <summary>
        /// List of an author's scientific works
        /// </summary>
        public List<ScientificWork> ScientificWorks { get; } = new();
    }
}