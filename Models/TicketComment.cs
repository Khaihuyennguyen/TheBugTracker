using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
namespace TheBugTracker.Models
{
    public class TicketComment
    {
        //ID
        public int Id { get; set; }
        //Comment
        
        [DisplayName("Member Comment")]
        public string Comment { get; set; }
        //Craeted

        [DisplayName("Date")]
        public DateTimeOffset Created { get; set; }
        //TicketID
        [DisplayName("Ticket")]
        public int TicketId { get; set; }
        //userID
        
        [DisplayName("Team Member")]
        public string UserId { get; set; }
        //Navigation Properties
        public virtual Ticket Ticket { get; set; }
        public virtual BTUser User { get; set; }
    }
}
