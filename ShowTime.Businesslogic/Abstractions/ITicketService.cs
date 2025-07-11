using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowTime.Businesslogic.Dtos;

namespace ShowTime.Businesslogic.Abstractions
{
    public interface ITicketService
    {
        Task CreateTicketAsync(TicketCreateDto ticket);
        Task DeleteTicketAsync(int ticketId);

        Task UpdateTicketAsync(TicketGetDto ticket);

        Task<TicketGetDto?> GetTicketByIdAsync(int ticketId);

        Task<IEnumerable<TicketGetDto>> GetTicketsByFestivalIdAsync(int festivalId);

       


    }
}
