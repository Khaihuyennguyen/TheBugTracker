using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TheBugTracker.Models;

namespace TheBugTracker.Services.Interfaces
{
    public interface IBTCompanyInfoService
    {
        public Task<Company> GetCompanyInfoByIdAsync(int? companyId);
        public Task<List<BTUser>> GetAllMembersAsync(int companyId);

        public Task<List<Project>> GetAllProjectsAsync(int companyId);
        public Task<List<Ticket>> GetAllTicketAsync(int compaynyId);


    }
}
