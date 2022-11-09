using AutoMapper;
using Entities.DataTransferObject;
using Entities.Models;

namespace AccountOwnerServer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Create Map Method here to map the Owner to the Ownerdto object.
            CreateMap<Owner, OwnerDto>();
            CreateMap<Account, AccountDto>();
            CreateMap<OwnerForCreationDto, Owner>();
            CreateMap<OwnerForUpdateDto, Owner>();


            //Create Map Method here to map the Account to the Accountdto object.
            CreateMap<Account, AccountDto>();
          
        }

        
    }
}
