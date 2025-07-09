using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.Businesslogic.Abstractions
{
    public interface IBookingService
    {
        /// <summary>
        /// Creates a booking for a user at a festival.
        /// </summary>
        /// <param name="userId">The ID of the user making the booking.</param>
        /// <param name="festivalId">The ID of the festival for which the booking is made.</param>
        /// <param name="ticketId">The ID of the ticket being booked.</param>
        /// <param name="type">The type of booking (e.g., VIP, General Admission).</param>
        /// <param name="price">The price of the booking.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task CreateBookingAsync(int userId, int festivalId, int ticketId, string type, int price);
    }
}
