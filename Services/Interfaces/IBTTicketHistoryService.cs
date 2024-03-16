using System.Collections.Generic;
using System.Threading.Tasks;
using TheBugTracker.Models;

namespace TheBugTracker.Services.Interfaces
{
    public interface IBTTicketHistoryService
    {
        Task AddhistoryAsync(Ticket oldTicket, Ticket newTicket, string userId);

        Task AddHistoryAsync(int ticketId, string model, string userid);
        Task<List<TicketHistory>> GetProjectTicketsHistoriesAsync(int projectId, int companyId);
        Task<List<TicketHistory>> GetCompanyTickethistoriesAsync(int companyId);

    }
}
