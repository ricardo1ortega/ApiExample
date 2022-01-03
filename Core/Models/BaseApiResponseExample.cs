using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExample.Core.Models
{
    public class BaseApi410ResponseExample
    {
        /// <summary>
        /// 410 - Not Managed.
        /// The requested process could not be processed.
        /// </summary>
        /// <value>410</value>
        /// <example>410</example>
        [DefaultValue(410)]
        public int Status { get; set; } = 410;
    }
    public class BaseApi400ResponseExample
    {
        /// <summary>
        /// 400 - Validation Error.
        /// Some of the fields do not comply with the expected format.
        /// </summary>
        /// <example>400</example>
        [DefaultValue(400)]
        public int Status { get; set; } = 400;
    }

    
    public class BaseApi401ResponseExample
    {
        /// <summary>
        /// 401 - Unauthorized.
        /// The user is Unauthorized to use the resource
        /// </summary>
        /// <example>401</example>
        [DefaultValue(401)]
        public int Status { get; set; }
    }

    public class BaseApi404ResponseExample
    {
        /// <summary>
        /// 404 - Not found.
        /// The requested resource cannot be found.
        /// </summary>
        /// <example>404</example>
        [DefaultValue(404)]
        public int Status { get; set; }
    }
    public class BaseApi500ResponseExample
    {
        /// <summary>
        /// 500 - Server error.
        /// A server error occurred.
        /// </summary>
        /// <example>500</example>
        [DefaultValue(500)]
        public int Status { get; set; }
    }
}
