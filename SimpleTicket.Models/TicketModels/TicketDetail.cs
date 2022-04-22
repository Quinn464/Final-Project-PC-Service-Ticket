using SimpleTicket.Data;
using SimpleTicket.Models.NoteModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTicket.Models.TicketModels
{
    public class TicketDetail
    {
        
        [Display(Name = "Ticket ID #")]
        public Guid TicketID { get; set; }
        [Display(Name = "Create ID")]
        public Guid CreatorID { get; set; }
        [Display(Name = "Create Name")]

        public string CreatorName { get; set; }
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        
        [Display(Name = "Customer ID")]
        public int CustomerID { get; set; }
        [Display(Name = "Name")]
        public string CustomerName { get; set; }
        
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateUpdated { get; set; }

        
        [Display(Name = "Note Count")]
        public int NoteCount { get; set; }
        public List<NoteListItem> Notes { get; set; } = new List<NoteListItem>();


        
        public Priority Priority { get; set; }
        public Status Status { get; set; }
    }
}
