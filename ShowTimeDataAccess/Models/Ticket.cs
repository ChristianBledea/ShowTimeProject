using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.DataAccess.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public int Price { get; set; }
        public Booking Booking { get; set; }
    }
}
