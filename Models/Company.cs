using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;

namespace TheBugTracker.Models
{
    public class Company
    {
        //Primary key
        public int Id { get; set; }
        [DisplayName("Company Name")]
        public string Name { get; set; }
        [DisplayName("Company Description")]
        public string Description { get; set; }
        //--Navigtation properties --//

        public virtual ICollection<BTUser> Members { get; set; }
        public virtual ICollection<Project> Projects { get; set; }

        //Create relationship to Invites


    }
}
