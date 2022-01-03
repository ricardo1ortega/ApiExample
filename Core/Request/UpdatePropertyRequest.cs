using System.ComponentModel.DataAnnotations;

namespace ApiExample.Core.Request
{
    public class UpdatePropertyRequest
    {
        /// <summary>
        /// Property id
        /// </summary>
        /// <example>619ec744ec6272e9b0642478</example>
        [Required]
        public string PropertyId { get; set; }
        public OwnerRequest Owner { get; set; }
        public PropertyRequest Property { get; set; }
    }
}
