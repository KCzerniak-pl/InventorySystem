using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Seller
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string? Name { get; set; }

        // Relationships.
        public virtual ICollection<Item>? Items { get; set; }
    }
}
