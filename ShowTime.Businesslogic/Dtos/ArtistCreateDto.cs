using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ShowTime.Businesslogic.Dtos
{
    public class ArtistCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        // Additional properties can be added as needed
    }
}
