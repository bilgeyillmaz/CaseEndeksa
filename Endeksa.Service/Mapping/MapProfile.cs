using AutoMapper;
using Endeksa.Core.DTOs;
using Endeksa.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Endeksa.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Root, RootDto>().ReverseMap();
            CreateMap<Root, RootModelDto>().ReverseMap();
        }
    }
}
