using Microsoft.AspNetCore.Mvc.Rendering;

namespace TheBugTracker.Models.ViewModels
{
    public class AssignedPMViewModel
    {
        public Project Project { get; set; }
        public SelectList PMList { get; set; }
        public string PMID {  get; set; }

    }
}
