using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Database.Entities
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string? Name { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "datetimeoffset(0)")]
        public DateTimeOffset DateTimeCreated { get; set; }

        public int TypeId { get; set; }

        public int GroupId { get; set; }

        public int? ManufacturerId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string? InventoryNumber { get; set;}

        [Column(TypeName = "nvarchar(255)")]
        public string? InvoiceNumber { get; set; }

        [Column(TypeName = "datetimeoffset(0)")]
        public DateTimeOffset? InvoiceDate { get; set; }

        public double? Price { get; set; }

        public int? SellerId { get; set; }

        public int LocationId { get; set; }

        // Relationships.
        public virtual Type? Type { get; set; }
        public virtual Group? Group { get; set; }
        public virtual Manufacturer? Manufacturer { get; set; }
        public virtual Seller? Seller { get; set; }
        public virtual Location? Location { get; set; }
    }
}
