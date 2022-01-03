using ApiExample.Core.Mappers;
using ApiExample.Core.Models;
using ApiExample.Core.Request;
using ApiExample.Db.Context;
using ApiExample.Db.Models;
using ApiExample.Services;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiExample.UnitTest
{
    [TestFixture]
    internal class PropertyServiceTests
    {
        private IMapper _mapper { get; set; }
        private Mock<IPropertyContext> _propertyContext { get; set; }
        private Mock<ILoggerFactory> _logger;

        [SetUp]
        public void Setup()
        {
            //_mapper = new IMapper();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProperty());
            });
            //IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mappingConfig.CreateMapper();
            _propertyContext = new Mock<IPropertyContext>();
            _logger = new Mock<ILoggerFactory>();
        }

        [Test]  
        public void UpdateProperty_NotFoundWhenRequestNull()
        {
            var request = new UpdatePropertyRequest();
            var propertyService = new PropertyService(_logger.Object, _mapper, _propertyContext.Object);
            var response = propertyService.UpdateProperty(request);
            Assert.IsTrue(response.Status == BaseApiResponse.ResponseCodes.NotFound,
                "Status should be 404");
        }

        [Test]
        public void UpdateProperty_NotFoundWhenPropertyIdNull()
        {
            var request = new UpdatePropertyRequest
            {
                Owner = new OwnerRequest
                {
                    Name = "Pedro Perez",
                    Address = "Bogota, Colombia",
                    Birthday = DateTime.Now,
                    Photo = "123456789"
                },
                Property = new PropertyRequest
                {
                    Name = "Edificio Lorenal",
                    Address = "Bogota, Colombia",
                    CodeInternal = "001",
                    File = "123456789",
                    Price = 10000,
                    Year = 2
                }
            };
            var propertyService = new PropertyService(_logger.Object, _mapper, _propertyContext.Object);
            var response = propertyService.UpdateProperty(request);
            Assert.IsTrue(response.Status == BaseApiResponse.ResponseCodes.NotFound,
                "Status should be 404");
        }

        [Test]
        public void UpdateProperty_NotFoundWhenOwnerNameIsNull()
        {

            var request = new UpdatePropertyRequest
            {
                Owner = new OwnerRequest
                {
                    //Name = "Pedro Perez",
                    Address = "Bogota, Colombia",
                    Birthday = DateTime.Now,
                    Photo = "123456789"
                },
                Property = new PropertyRequest
                {
                    Name = "Edificio Lorenal",
                    Address = "Bogota, Colombia",
                    CodeInternal = "001",
                    File = "123456789",
                    Price = 10000,
                    Year = 2
                }
            };

            var propertyService = new PropertyService(_logger.Object, _mapper, _propertyContext.Object);
            var response = propertyService.UpdateProperty(request);
            Assert.IsTrue(response.Status == BaseApiResponse.ResponseCodes.NotFound,
                "Status should be 404");
        }

        [Test]
        public void UpdateProperty_NotFoundWhenPropertyNameIsNull()
        {

            var request = new UpdatePropertyRequest
            {
                Owner = new OwnerRequest
                {
                    Name = "Pedro Perez",
                    Address = "Bogota, Colombia",
                    Birthday = DateTime.Now,
                    Photo = "123456789"
                },
                Property = new PropertyRequest
                {
                    //Name = "Edificio Lorenal",
                    Address = "Bogota, Colombia",
                    CodeInternal = "001",
                    File = "123456789",
                    Price = 10000,
                    Year = 2
                }
            };

            var propertyService = new PropertyService(_logger.Object, _mapper, _propertyContext.Object);
            var response = propertyService.UpdateProperty(request);
            Assert.IsTrue(response.Status == BaseApiResponse.ResponseCodes.NotFound,
                "Status should be 404");
        }

        [Test]
        public void UpdateProperty_Ok()
        {
            var request = new UpdatePropertyRequest
            {
                PropertyId = "1",
                Owner = new OwnerRequest
                {
                    Name = "Pedro Perez",
                    Address = "Bogota, Colombia",
                    Birthday = DateTime.Now,
                    Photo = "123456789"
                },
                Property = new PropertyRequest
                {
                    Name = "Edificio Lorenal",
                    Address = "Bogota, Colombia",
                    CodeInternal = "001",
                    File = "123456789",
                    Price = 10000,
                    Year = 2
                }
            };

            
            var propertyService = new PropertyService(_logger.Object, _mapper, _propertyContext.Object);

            _propertyContext.Setup(t => t.FindPropertyById(It.IsAny<string>()))
                                .Returns((string t) => new Property { Id = MongoDB.Bson.ObjectId.Parse("61c161cb9546c40ab289e26a"), IdOwner = "61c161cb9546c40ab289e200" });

            var response = propertyService.UpdateProperty(request);
            Assert.IsTrue(response.Status == BaseApiResponse.ResponseCodes.Ok,
                "Status should be 200");
        }


    }
}
