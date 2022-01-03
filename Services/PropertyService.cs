using ApiExample.Core.Request;
using ApiExample.Db.Context;
using ApiExample.Db.Models;
using ApiExample.Resources;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExample.Services
{
    public class PropertyService
    {
        #region Files
        private string[] permittedExtensions = { ".jpeg", ".jpg", ".png" };

        private readonly Dictionary<string, List<byte[]>> _fileSignature =
                            new Dictionary<string, List<byte[]>>
                            {
                                { ".jpeg", new List<byte[]>
                                    {
                                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                                    }
                                },
                                { ".jpg", new List<byte[]>
                                    {
                                        new byte [] {0xFF, 0xD8, 0xFF, 0xE0 },
                                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                                        new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
                                    }

                                },
                                { ".png", new List<byte[]>
                                    {
                                        new byte [] {0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A },
                                    }

                                }
                            };
        #endregion
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

        public CreatePropertyResponse CreateProperty(CreatePropertyRequest request)
        {
            var owner = _mapper.Map<Owner>(request.Owner);
            var id = _propertyContext.SaveOwner(owner);
            var property = _mapper.Map<Property>(request.Property);
            property.IdOwner = id.ToString();
            var id2 = _propertyContext.SaveProperty(property);

            return new CreatePropertyResponse { Id = id2.ToString() };
        }

        public UpdatePropertyResponse UpdateProperty(UpdatePropertyRequest request)
        {
            var p = _propertyContext.FindPropertyById(request.PropertyId);

            if(p == null)
                return new UpdatePropertyResponse().NotFound<UpdatePropertyResponse>("property");

            var owner = _mapper.Map<Owner>(request.Owner);
            var result1 = _propertyContext.UpdateOwner(p.IdOwner, owner);
            var property = _mapper.Map<Property>(request.Property);
            property.IdOwner = p.IdOwner;
            var id2 = _propertyContext.UpdateProperty(request.PropertyId, property);

            return new UpdatePropertyResponse { Id = id2.ToString() };
        }

        public PropertiesResponse ListAllProperties()
        {
            return new PropertiesResponse { Results = _propertyContext.FindAllProperties() };
        }

        public PropertiesResponse ListPropertiesByFilter(string filter)
        {
            return new PropertiesResponse { Results = _propertyContext.FindPropertiesByFilter(filter) };
        }


        private bool IsPermittedExtension(MemoryStream file)
        {
            using (var reader = new BinaryReader(file))
            {
                reader.BaseStream.Position = 0;

                for (int i = 0; i < permittedExtensions.Length; i++)
                {
                    var signatures = _fileSignature[permittedExtensions[i]];
                    var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));
                    
                    if (signatures.Any(signature =>
                        headerBytes.Take(signature.Length).SequenceEqual(signature)))
                        return true;
                }

            }
            return false;
        }
    }
}
