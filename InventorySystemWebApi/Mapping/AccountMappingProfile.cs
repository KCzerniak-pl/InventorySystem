using AutoMapper;
using Database.Entities.User;
using InventorySystemWebApi.Models.Account;

namespace InventorySystemWebApi.Mapping
{
    public class AccountMappingProfile : Profile
    {
        public AccountMappingProfile()
        {
            // Mapping from DTO.
            CreateMap<CreateAccountDto, User>()
                .ForMember(
                    dest => dest.DateTimeCreated,
                    opt => opt.MapFrom(_ => $"{DateTime.Now}")
                );
        }
    }
}
