using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBugTracker.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TheBugTracker.Models.ViewModels
{
    public class AddProjectWithPMViewModels
    {
        public Project Project { get; set; }
        public SelectList PMList { get; set; }
        public string PmId{ get; set; }
        public SelectList PriorityList { get; set; }    
 

    }
}
