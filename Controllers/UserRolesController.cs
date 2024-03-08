using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBugTracker.Extensions;
using TheBugTracker.Models;
using TheBugTracker.Models.ViewModels;
using TheBugTracker.Services.Interfaces;

namespace TheBugTracker.Controllers
{
    public class UserRolesController : Controller
    {
        private readonly IBTRolesService _rolesService;
        private readonly IBTCompanyInfoService _companyInfoService;
        public UserRolesController(IBTRolesService rolesService, IBTCompanyInfoService companyInfoService)
        {
            _rolesService = rolesService;
            _companyInfoService = companyInfoService;
        }
        [HttpGet]
        public async Task<IActionResult> ManageUserRoles()
        {
            //Add an instance of a view model as a list

            List<ManageUserRolesViewModel> model = new();
            //Get CompanyId
            int companyId = User.Identity.GetCompanyId().Value;
            //Get all company users
            List<BTUser> users = await _companyInfoService.GetAllMembersAsync(companyId);

            //Loop over the users to populate the ViewModel
            //Instantiate viewmodel
            //use _roleService
            //Create multi selects
            foreach(BTUser user in users)
            {
                ManageUserRolesViewModel viewModel = new ();
                viewModel.BTUser = user;
                IEnumerable<string> selected = await _rolesService.GetUserRolesAsync(user);
                viewModel.Roles = new MultiSelectList(await _rolesService.GetRolesAsync(), "Name", "Name", selected);
                
                model.Add(viewModel);
            }
            // return the model to the view
            return View(model);
        }
    }
}
