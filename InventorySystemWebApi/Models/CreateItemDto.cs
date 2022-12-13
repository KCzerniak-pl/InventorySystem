using System.ComponentModel.DataAnnotations;

namespace InventorySystemWebApi.Models
{
    public class CreateItemDto
    {
        [Required]
        [MaxLength(150)]
        public string? Name { get; set; }

        [MaxLength(4000)]
        public string? Description { get; set; }

        [Required]
        public int TypeId { get; set; }

        [Required]
        public int GroupId { get; set; }

        public int? ManufacturerId { get; set; }

        [MaxLength(255)]
        public string? InvoiceNumber { get; set; }

        public double? Price { get; set; }

        public int? SellerID { get; set; }

        [Required]
        public int LocationId { get; set; }
    }
}