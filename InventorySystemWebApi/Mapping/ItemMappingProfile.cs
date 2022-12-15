using AutoMapper;
using Database.Entities.Item;
using InventorySystemWebApi.Models;
using System.Data;

namespace InventorySystemWebApi.Mapping
{
    public class ItemMappingProfile : Profile
    {
        public ItemMappingProfile()
        {
            // Mapping to DTO.
            CreateMap<Item, ItemDto>();
            CreateMap<Database.Entities.Item.Type, TypeDto>();
            CreateMap<Group, GroupDto>();
            CreateMap<Manufacturer, ManufacturerDto>();
            CreateMap<Seller, SellerDto>();
            CreateMap<Location, LocationDto>();

            // Mapping from DTO.
            CreateMap<CreateItemDto, Item>()
                .ForMember(
                    dest => dest.DateTimeCreated,
                    opt => opt.MapFrom(_ => $"{DateTime.Now}")
                )
                .ForMember(
                    dest => dest.InventoryNumber,
                    opt => opt.MapFrom(_ => $"{GenerateInventoryNumber()}")
                );
        }

        private string GenerateInventoryNumber()
        {
            var inventoryNumber = DateTime.Now.GetHashCode().ToString("x");

            return inventoryNumber;
        }
    }
}
