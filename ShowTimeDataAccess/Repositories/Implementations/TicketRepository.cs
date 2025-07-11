using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;


namespace ShowTime.DataAccess.Repositories.Implementations
{
    
    public class TicketRepository : ITicketRepository
    {
        private readonly ShowtimeDbContext _context;
        private readonly DbSet<Ticket> _tickets;

        public TicketRepository(ShowtimeDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "Context cannot be null");
            _tickets = context.Set<Ticket>();
        }
        public async Task AddTicket(Ticket ticket)
        {
            try
            {
                await _tickets.AddAsync(ticket);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to add ticket: {ex.Message}");
            }
        }

        public async Task DeleteTicket(int ticketId)
        {
            try
            {
                var ticket = await _tickets.FindAsync(ticketId);
                if (ticket == null)
                {
                    throw new KeyNotFoundException($"Ticket with ID {ticketId} not found.");
                }
                _tickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to delete ticket: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Ticket>> GetTicketByFestival(int festivalId)
        {
            try
            {
                return await _tickets
                    .Where(t => t.FestivalId == festivalId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to retrieve tickets for festival ID {festivalId}: {ex.Message}");
            }
        }

        public async Task<Ticket?> GetTicketById(int ticketId)
        {
            try
            {
                var ticket = await _tickets.FindAsync(ticketId);
                if (ticket == null)
                {
                    throw new KeyNotFoundException($"Ticket with ID {ticketId} not found.");
                }
                return ticket;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to retrieve ticket with ID {ticketId}: {ex.Message}");
            }
        }

        public async Task UpdateTicket(Ticket ticket)
        {
            try
            {
                var existingTicket = await _tickets.FindAsync(ticket.Id);
                if (existingTicket == null)
                {
                    throw new KeyNotFoundException($"Ticket with ID {ticket.Id} not found.");
                }
                _context.Entry(existingTicket).CurrentValues.SetValues(ticket);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to update ticket: {ex.Message}");
            }
        }
    }
}
