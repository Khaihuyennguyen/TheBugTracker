﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheBugTracker.Data;
using TheBugTracker.Models;
using TheBugTracker.Services.Interfaces;

namespace TheBugTracker.Services
{
    public class BTProjectService : IBTProjectService
    {
        private readonly ApplicationDbContext _context;

        public BTProjectService(ApplicationDbContext context)
        {
            _context = context;

        }

        // CRUD - CREATE
        public async Task AddNewProjectAsync(Project project)
        {
            _context.Add(project);
            await _context.SaveChangesAsync();

        }
       
        public Task<bool> AddProjectManagerAsync(string userId, int projectId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> AddUserToProjectAsync(string userId, int projectId)
        {
            
            BTUser user = await _context.Users.FirstOrDefaultAsync( u => u.Id == userId );
            if (user != null)
            {
                Project project = await _context.Projects.FirstOrDefaultAsync(u => u.Id == projectId);
                if (!await IsUserOnProjectAsync(userId, projectId))
                {
                    try
                    {
                        project.Members.Add(user);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                else
                {
                    return false;
                }
            }

            else
            {
                return false;
            }
        }
        // CRUD - Archive (Delete)
        public async Task ArchiveProjectAsync(Project project)
        {
            project.Archived = true;
            _context.Update(project);
            await _context.SaveChangesAsync();
        }

        public Task<List<BTUser>> GetAllProjectMembersExceptPMAsync(int projectId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Project>> GetAllProjectsByCompany(int companyId)
        {
            List<Project> projects = new();

            projects =  await _context.Projects.Where(u => u.CompanyId == companyId && u.Archived == false )
                                            .Include(u => u.Members)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.Comments)
                                             .Include(p => p.Tickets)
                                                .ThenInclude(t => t.Attachments)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.History)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.Notifications)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.DeveloperUser)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.OwnerUser)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.TicketStatus)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.TicketPriority)
                                            .Include(p => p.Tickets)
                                                .ThenInclude(t => t.TicketType)
                                            .Include(p => p.ProjectPriority)
                                            .ToListAsync();
            return projects;

        }

        public async Task<List<Project>> GetAllProjectsByPriority(int companyId, string priorityName)
        {
            List<Project> projects = await GetAllProjectsByCompany(companyId);
            int priorityId = await LookupProjectPriorityId(priorityName);

            return projects.Where(p => p.ProjectPriorityId == priorityId).ToList();

        }

        public async Task<List<Project>> GetArchivedProjectsByCompany(int companyId)
        {
            List<Project> projects = await GetAllProjectsByCompany(companyId);

            return projects.Where(p => p.Archived == true).ToList();
        }

        public Task<List<BTUser>> GetDevelopersOnProjectAsync(int projectId)
        {
            throw new System.NotImplementedException();
        }
        // CRUD - REad
        public async Task<Project> GetProjectByIdAsync(int projectId, int companyId)
        {
            Project project = await _context.Projects
                                            .Include(p => p.Tickets)
                                            .Include(p => p.Members)
                                            .Include(p=>p.ProjectPriority)
                                            .FirstOrDefaultAsync(p => p.Id == projectId && p.CompanyId == companyId);
            return project;
        }

        public Task<BTUser> GetProjectManagerAsync(int projectId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<BTUser>> GetProjectMembersByRoleAsync(int projectId, string role)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<BTUser>> GetSubmittersOnProjectAsync(int projectId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Project>> GetUserProjectsAsync(string userId)
        {
            try
            {
                List<Project> userProjects = (await _context.Users
                            .Include(u => u.Projects)
                                .ThenInclude(p => p.Company)
                            .Include(u=>u.Projects)
                                .ThenInclude(p=>p.Members)
                            .Include(u => u.Projects)
                                .ThenInclude(p => p.Tickets)
                            .Include(u => u.Projects)
                                .ThenInclude(p => p.Tickets)
                                    .ThenInclude(t => t.DeveloperUser)
                            .Include(u => u.Projects)
                                .ThenInclude(p => p.Tickets)
                                    .ThenInclude(t => t.OwnerUser)
                            .Include(u => u.Projects)
                                .ThenInclude(p => p.Tickets)
                                    .ThenInclude(t => t.TicketPriority)
                            .Include(u => u.Projects)
                                .ThenInclude(p => p.Tickets)
                                    .ThenInclude(t => t.TicketStatus)
                            .Include(u => u.Projects)
                                .ThenInclude(p => p.Tickets)
                                    .ThenInclude(t => t.TicketType).FirstOrDefaultAsync( u => u.Id == userId)).Projects.ToList();

                return userProjects;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"*** Error *** - Error while getting userproject async. ---> {ex.Message} ***");
                throw;
            }
        }

        public Task<List<BTUser>> GetUsersNotOnProjectAsync(int projectId, int companyId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> IsUserOnProjectAsync(string userId, int projectId)
        {
            Project project = await _context.Projects.Include(p => p.Members).FirstOrDefaultAsync(p => p.Id == projectId);

            bool result = false;

            if (project != null)
            {
                result = project.Members.Any(m => m.Id == userId);
                
            }
            return result;
        }

        public async Task<int> LookupProjectPriorityId(string priorityName)
        {
            int priorityId = (await _context.ProjectPriorities.FirstOrDefaultAsync(p => p.Name == priorityName)).Id;

            return priorityId;
        }

        public Task RemoveProjectManagerAsync(int projectId)
        {
            throw new System.NotImplementedException();
        }

        public async Task RemoveUserFromProjectAsync(string userId, int projectId)
        {
            try
            {
                BTUser user = await _context.Users.FirstOrDefaultAsync(p => p.Id == userId);
                Project project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);

                try
                {
                    if (await IsUserOnProjectAsync(userId, projectId))
                    {
                        project.Members.Remove(user);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"**** ERROR **** - Error Removing User from project. ---> {ex.Message}");
            }
        }

        public Task RemoveUsersFromProjectByRoleAsync(string role, int projectId)
        {
            throw new System.NotImplementedException();
        }
        //CRUD - UPDATE
        public async Task UpdateProjectAsync(Project project)
        {
            _context.Update(project);
            await _context.SaveChangesAsync();
        }
    }
}
