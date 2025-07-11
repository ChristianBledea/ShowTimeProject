using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowTime.DataAccess.Models;

namespace ShowTime.DataAccess.Repositories.Abstractions
{
    public interface IBookingRepository
    {
        
        Task<IEnumerable<Booking>> GetByUserIdAsync(int userId);

        Task<Booking?> GetBookingByUserIdAndFestivalIdAsync(int userId, int festivalId);
        Task AddBookingAsync(Booking booking);
        Task UpdateAsync(Booking booking);
        Task DeleteBookingAsync(int userId, int festivalId);
    }
}
