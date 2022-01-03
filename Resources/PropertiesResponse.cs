using ApiExample.Core.Models;
using ApiExample.Db.Models;
using System.Collections.Generic;

namespace ApiExample.Resources
{
    public class PropertiesResponse : BaseApiResponse
    {
        /// <summary>
        /// The properties
        /// </summary>
        public List<Property> Results { get; set; }
    }
}
