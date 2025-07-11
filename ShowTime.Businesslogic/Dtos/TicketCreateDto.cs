using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.Businesslogic.Dtos
{
    public class TicketCreateDto
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public string Type { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public int FestivalId { get; set; }
    }
}
