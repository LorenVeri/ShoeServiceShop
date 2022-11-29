using AutoMapper;
using ShoeService_Model.Dtos;
using ShoeService_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Data.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Shoes, ShoesDto>();
            CreateMap<ShoesDto, Shoes>();

            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
            CreateMap<RegisterDto, User>()
                .ForMember(r => r.PasswordHash, act => act.MapFrom(src => src.Password));

            CreateMap<MemberShip, MemberShipDto>();
            CreateMap<MemberShipDto, MemberShip>();

            CreateMap<Service, ServiceDto>();
            CreateMap<ServiceDto, ServiceDto>();

            CreateMap<Storage, StorageDto>();
            CreateMap<StorageDto, Storage>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
        }
    }
}
