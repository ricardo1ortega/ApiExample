using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExample.Core.Request
{
    public class CreatePropertyRequest
    {
        public OwnerRequest Owner { get; set; }
        public PropertyRequest Property { get; set; }
    }

    public class OwnerRequest
    {
        /// <summary>
        /// Name of the Owner
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Address of the Owner
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Base64 representation of the photo of the Owner
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// Birthday of the Owner
        /// </summary>
        public DateTime Birthday { get; set; }
    }

    public class PropertyRequest
    {
        /// <summary>
        /// Name of the Property
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Address of the Property
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Price of the Property without decimals
        /// </summary>
        public int Price { get; set; }

        /// <summary>
        /// Code for internal reference of the Property
        /// </summary>
        public string CodeInternal { get; set; }

        /// <summary>
        /// Year of the Property
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Base64 representation of the image of the Property
        /// </summary>
        public string File { get; set; }
    }
}
