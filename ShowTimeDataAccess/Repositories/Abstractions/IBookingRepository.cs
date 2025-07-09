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
        Task<Booking?> GetAsync(int userId, int festivalId);
        Task<IEnumerable<Booking>> GetByUserIdAsync(int userId);
        Task AddAsync(Booking booking);
        Task UpdateAsync(Booking booking);
        Task DeleteAsync(int userId, int festivalId);
    }
}
