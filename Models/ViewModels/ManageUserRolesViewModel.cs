using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheBugTracker.Data;
using TheBugTracker.Models;
namespace TheBugTracker.Models.ViewModels
{
    public class ManageUserRolesViewModel
    {
        public BTUser BTUser { get; set; }
        public MultiSelectList Roles { get; set; }
        public List<string>  SelectedRoles { get; set; }
    }
}
