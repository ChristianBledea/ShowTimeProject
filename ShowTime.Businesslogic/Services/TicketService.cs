using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowTime.Businesslogic.Abstractions;
using ShowTime.Businesslogic.Dtos;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories.Abstractions;

namespace ShowTime.Businesslogic.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
        public async Task CreateTicketAsync(TicketCreateDto ticket)
        {
            try
            {
                var entity = new Ticket
                {
                    FestivalId = ticket.FestivalId,
                    Price = ticket.Price,
                    Type = ticket.Type,
                    Quantity = ticket.Quantity,
                    
                };
                await _ticketRepository.AddTicket(entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to create ticket: {ex.Message}");

            }
        }

        public async Task DeleteTicketAsync(int ticketId)
        {
            try
            {
                var ticket = await _ticketRepository.GetTicketById(ticketId);
                if (ticket == null)
                {
                    throw new KeyNotFoundException($"Ticket with ID {ticketId} not found.");
                }
                await _ticketRepository.DeleteTicket(ticket.Id);

            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to delete ticket: {ex.Message}");
            }
        }

       

        public async Task<TicketGetDto?> GetTicketByIdAsync(int ticketId)
        {
            try
            {
                var ticket = await _ticketRepository.GetTicketById(ticketId);
                if (ticket == null)
                {
                    throw new KeyNotFoundException($"Ticket with ID {ticketId} not found.");
                }
                return new TicketGetDto
                {
                    Id = ticket.Id,
                    FestivalId = ticket.FestivalId,
                    Price = ticket.Price,
                    Type = ticket.Type,
                    Quantity = ticket.Quantity
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to retrieve ticket: {ex.Message}");
            }
        }

        public async Task<IEnumerable<TicketGetDto>> GetTicketsByFestivalIdAsync(int festivalId)
        {
            try
            {
                var tickets = await _ticketRepository.GetTicketByFestival(festivalId);
                return tickets.Select(t => new TicketGetDto
                {
                    Id = t.Id,
                    FestivalId = t.FestivalId,
                    Price = t.Price,
                    Type = t.Type,
                    Quantity = t.Quantity
                }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to retrieve tickets for festival {festivalId}: {ex.Message}");
            }
        }
        public async Task UpdateTicketAsync(TicketGetDto ticket)
        {
            try
            {
                var existingTicket = await _ticketRepository.GetTicketById(ticket.Id);
                if (existingTicket == null)
                {
                    throw new KeyNotFoundException($"Ticket with ID {ticket.Id} not found.");
                }
                existingTicket.FestivalId = ticket.FestivalId;
                existingTicket.Price = ticket.Price;
                existingTicket.Type = ticket.Type;
                existingTicket.Quantity = ticket.Quantity;
                await _ticketRepository.UpdateTicket(existingTicket);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to update ticket: {ex.Message}");
            }
        }
    }
}
