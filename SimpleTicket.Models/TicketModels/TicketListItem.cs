using SimpleTicket.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTicket.Models.TicketModels
{
    public class TicketListItem
    {
      
        [Display(Name = "Ticket ID #")]
        public Guid TicketID { get; set; }
        [Display(Name = "Create ID")]
        public Guid CreatorID { get; set; }
        public string CreateName { get; set; }
        public string Title { get; set; }
        
    
        [Display(Name = "Customer ID")]
        public int CustomerID { get; set; }
        [Display(Name = "Name")]
        public string CustomerName { get; set; }
        
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateUpdated { get; set; }

       
        [Display(Name = "Note Count")]
        public int NoteCount { get; set; }
        
        public Priority Priority { get; set; }
        public Status Status { get; set; }
    }
}
