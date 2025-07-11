using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowTime.DataAccess.Models;

namespace ShowTime.DataAccess.Repositories.Abstractions
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Ticket>> GetTicketByFestival(int festivalId);
        Task<Ticket?> GetTicketById(int ticketId);
        Task AddTicket(Ticket ticket);
        Task UpdateTicket(Ticket ticket);
        Task DeleteTicket(int ticketId);

    }
}
