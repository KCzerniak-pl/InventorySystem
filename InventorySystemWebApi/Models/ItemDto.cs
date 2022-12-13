using Database.Entities;

namespace InventorySystemWebApi.Models
{
    public class ItemDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public DateTimeOffset DateTimeCreated { get; set; }

        public TypeDto? Type { get; set; }

        public GroupDto? Group { get; set; }

        public ManufacturerDto? Manufacturer { get; set; }

        public string? InventoryNumber { get; set; }

        public string? InvoiceNumber { get; set; }

        public DateTimeOffset? InvoiceDate { get; set; }

        public double? Price { get; set; }

        public SellerDto? Seller { get; set; }

        public LocationDto? Location { get; set;}
    }
}
