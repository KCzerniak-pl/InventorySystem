using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities
{
    public class Location
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string? Name { get; set; }

        // Relationships.
        public virtual ICollection<Item>? Items { get; set; }
    }
}
