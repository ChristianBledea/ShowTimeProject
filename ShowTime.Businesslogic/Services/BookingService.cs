using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowTime.Businesslogic.Abstractions;
using ShowTime.Businesslogic.Dtos;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories.Abstractions;
using ShowTime.DataAccess.Repositories.Implementations;

namespace ShowTime.Businesslogic.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public async Task AddBookingAsync(BookingCreateDto bookingCreateDto)
        {
            if (bookingCreateDto == null)
            {
                throw new ArgumentNullException(nameof(bookingCreateDto), "BookingCreateDto cannot be null");
            }
            var booking = new Booking
            {
                UserId = bookingCreateDto.UserId,
                FestivalId = bookingCreateDto.FestivalId,
                TicketId = bookingCreateDto.TicketId,
                Type = bookingCreateDto.Type,
                Price = bookingCreateDto.Price,
                User = null
            };
            await _bookingRepository.AddBookingAsync(booking);

        }

        public async Task DeleteBookingAsync(int UserId, int FestivalId)
        {
            try
            {
                var booking = await _bookingRepository.GetBookingByUserIdAndFestivalIdAsync(UserId, FestivalId);
                if (booking == null)
                {
                    throw new Exception($"Booking not found for UserId: {UserId} and FestivalId: {FestivalId}");
                }
                await _bookingRepository.DeleteBookingAsync(UserId, FestivalId );
            } 
            catch (Exception ex)
            {
                throw new Exception($"Unable to delete booking: {ex.Message}");
            }
        }

        public async Task<IList<BookingGetDto>> GetUserBookingsAsync(int UserId)
        {
            try
            {
                var bookings = await _bookingRepository.GetByUserIdAsync(UserId);
                if (bookings == null || !bookings.Any())
                {
                    throw new KeyNotFoundException($"No bookings found for UserId: {UserId}");
                }
                return bookings.Select(b => new BookingGetDto
                {
                    UserId = b.UserId,
                    FestivalId = b.FestivalId,
                    TicketId = b.TicketId,
                    Type = b.Type,
                    Price = b.Price
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving bookings for UserId: {UserId}.", ex);
            }
        }

        public async Task UpdateBookingAsync(BookingCreateDto bookingCreateDto)
        {
            try
            {
                var booking = await _bookingRepository.GetBookingByUserIdAndFestivalIdAsync(bookingCreateDto.UserId, bookingCreateDto.FestivalId);
                if (booking == null)
                {
                    throw new KeyNotFoundException($"Booking not found for UserId: {bookingCreateDto.UserId} and FestivalId: {bookingCreateDto.FestivalId}");
                }
                booking.TicketId = bookingCreateDto.TicketId;
                booking.Type = bookingCreateDto.Type;
                booking.Price = bookingCreateDto.Price;
                
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to update booking: {ex.Message}");
            }
        }
    }
}
