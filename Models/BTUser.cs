using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;


namespace TheBugTracker.Models
{
    public class BTUser : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [NotMapped]
        public string FullName { get { return $"{FirstName} {LastName}"; } }

        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile AvatarFormFile { get; set; }
        [DisplayName("Avatar")]
        public string AvatarFileName { get; set; }
        public byte[] AvatarFileData { get; set; }
        [Display(Name = "File Extension")]
        public string AvatarContentType { get; set; }


        public int CompanyId { get; set; }

        // Navigation properties
        public virtual Company Company { get; set; }
        public virtual ICollection<Project> Projects { get; set; }

    }
}
