using ApiExample.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExample.Resources
{
    public class CreatePropertyResponse : BaseApiResponse
    {
        /// <summary>
        /// Property Id
        /// </summary>
        /// <example>3195436353543</example>
        public string Id { get; set; }
    }

    public class UpdatePropertyResponse : BaseApiResponse
    {
        /// <summary>
        /// Property Id
        /// </summary>
        /// <example>3195436353543</example>
        public string Id { get; set; }
    }
}
