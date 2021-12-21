using ApiExample.Core.Request;
using ApiExample.Db.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExample.Core.Mappers
{
    public class MappingProperty : Profile
    {
        public MappingProperty()
        {
            CreateMap<OwnerRequest, Owner>();
            CreateMap<Owner, OwnerRequest>();

            CreateMap<PropertyRequest, Property>();
            CreateMap<Property, PropertyRequest>();
        }
    }
}
