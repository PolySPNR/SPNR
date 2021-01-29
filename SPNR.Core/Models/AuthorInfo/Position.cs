using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SPNR.Core.Models.AuthorInfo
{
    [Index(nameof(Name), IsUnique = true)]
    public class Position
    {
        public int PositionId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}