using AutoMapper;
using ShoeService_Model.Dtos;
using ShoeService_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Data.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Shoe, ShoeDto>();
            CreateMap<ShoeDto, Shoe>();
        }
    }
}
