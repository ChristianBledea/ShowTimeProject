using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowTime.Businesslogic.Dtos;
using ShowTime.DataAccess.Models;


namespace ShowTime.Businesslogic.Abstractions
{
    public interface IBookingService
    {
        Task <IList<BookingGetDto>> GetUserBookingsAsync(int UserId);
        Task AddBookingAsync(BookingCreateDto bookingCreateDto);
        Task UpdateBookingAsync(BookingCreateDto bookingCreateDto);
        Task DeleteBookingAsync(int UserId, int FestivalId);


    }
}
