using Microsoft.EntityFrameworkCore;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories.Abstractions;


namespace ShowTime.DataAccess.Repositories.Implementations
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ShowtimeDbContext _context;
        private readonly DbSet<Booking> _bookings;
        public BookingRepository(ShowtimeDbContext context)
        {
            _context = context;
            _bookings = context.Set<Booking>();
        }
       
        public async Task<IEnumerable<Booking>> GetByUserIdAsync(int userId)
        {
            return await _bookings
                .Include(b => b.Festival)
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }
        public async Task AddBookingAsync(Booking booking)
        {
            try
            {
                await _bookings.AddAsync(booking);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to add booking: {ex.Message}");
            }
        }
        public async Task UpdateAsync(Booking booking)
        {
            try
            {
                var existingEntity = await _bookings.FindAsync(booking.UserId, booking.FestivalId);
                if (existingEntity == null)
                {
                    throw new Exception($"Booking with userId {booking.UserId} and festivalId {booking.FestivalId} not found.");
                }
                
                existingEntity.TicketId = booking.TicketId;
                existingEntity.Type = booking.Type;
                existingEntity.Price = booking.Price;
                _context.Entry(existingEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to update booking: {ex.Message}");
            }
        }

        public async Task DeleteBookingAsync(int userId, int festivalId)
        {
            try
            {
                var booking = await _bookings.FindAsync(userId, festivalId);
                if (booking == null)
                {
                    throw new KeyNotFoundException($"Booking with userId {userId} and festivalId {festivalId} not found.");
                }
                _bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to delete booking: {ex.Message}");
            }
        }
        public async Task<Booking?> GetBookingByUserIdAndFestivalIdAsync(int userId, int festivalId)
        {
            try
            {
                return await _bookings.SingleOrDefaultAsync(b => b.UserId == userId && b.FestivalId == festivalId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to retrieve booking for userId {userId} and festivalId {festivalId}: {ex.Message}");
            }
        }
    }
}
