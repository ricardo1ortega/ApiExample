using ApiExample.Core.Request;
using ApiExample.Db.Context;
using ApiExample.Db.Models;
using ApiExample.Resources;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExample.Services
{
    public class PropertyService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private PropertyContext _propertyContext;
        public PropertyService(ILoggerFactory loggerFactory,
                                IMapper mapper,
                                PropertyContext propertyContext
                                )
        {
            _logger = loggerFactory.CreateLogger<PropertyService>();
            _mapper = mapper;
            _propertyContext = propertyContext;
        }

        public async Task<CreatePropertyResponse> CreateProperty(CreatePropertyRequest request)
        {
            var owner = _mapper.Map<Owner>(request.owner);
            var id = _propertyContext.SaveOwner(owner);
            var property = _mapper.Map<Property>(request.property);
            property.IdOwner = id.ToString();
            var id2 = _propertyContext.SaveProperty(property);

            return new CreatePropertyResponse{ Id = id2.ToString()};
        }
    }
}
