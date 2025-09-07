using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBuddy.Destinations
{
    public class CreateUpdateDestinationDto
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(128)]
        public string Country { get; set; } = string.Empty;

    }
}
