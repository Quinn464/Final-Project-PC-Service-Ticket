using Microsoft.AspNet.Identity;
using SimpleTicket.Models.TicketModels;
using SimpleTicket.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SimpleTicket.MVC.Controllers
{
    // .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------. 
    //| .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. |
    //| |   ______     | || |     ______   | || |    _______   | || |  _________   | || |  _______     | || | ____ ____    | || |     _____    | || |     ______   | || |  _________   | |
    //| |  |_ __   \   | || |   .' ___  |  | || |   /  ___  |  | || | |_   ___  |  | || | |_   __ \    | || ||_  _| |_  _| | || |    |_   _|   | || |   .' ___  |  | || | |_ ___  |  | |
    //| |    | |__) |  | || |  / .'   \_|  | || |  |  (__ \_|  | || |   | |_  \_|  | || |   | |__) |   | || |  \ \   / /   | || |      | |     | || |  / .'   \_|  | || |   | |_  \_|  | |
    //| |    |  ___/   | || |  | |         | || |   '.___`-.   | || |   |  _|  _   | || |   |  __ /    | || |   \ \ / /    | || |      | |     | || |  | |         | || |   |  _|  _   | |
    //| |   _| |_      | || |  \ `.___.'\  | || |  |`\____) |  | || |  _| |___/ |  | || |  _| |  \ \_  | || |    \ ' /     | || |     _| |_    | || |  \ `.___.'\  | || |  _| |___/ |  | |
    //| |  |_____|     | || |   `._____.'  | || |  |_______.'  | || | |_________|  | || | |____| |___| | || |     \_/      | || |    |_____|   | || |   `._____.'  | || | |_________|  | |
    //| |              | || |              | || |              | || |              | || |              | || |              | || |              | || |              | || |              | |
    //| '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' |
    // '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------' 


    [Authorize]
    public class TicketController : Controller
    {
        public TicketService CreateTicketService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new TicketService(userID);
            return service;
        }
       
        public async Task<ActionResult> Index()
        {
            var service = CreateTicketService();
            var list = await service.GetTickets();
            return View(list);
        }

        
        public async Task<ActionResult> Details(Guid id)
        {
            var service = CreateTicketService();
            var model = await service.GetTicketByID(id);
            return View(model);
        }

        
        public ActionResult Create()
        {
            var service = CreateTicketService();
            var model = new TicketCreate();
            return View(model);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TicketCreate ticket)
        {
            if (!ModelState.IsValid) return View(ticket);
            var service = CreateTicketService();
            if (await service.CreateTicketAsync(ticket))
            {
                TempData["SaveResult"] = "Your ticket was created.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Could not create ticket.");
            return View(ticket);
        }

       
        public async Task<ActionResult> Edit(Guid id)
        {
            var service = CreateTicketService();
            var details = await service.GetTicketByID(id);
            var model = new TicketEdit
            {
                TicketID = details.TicketID,
                Title =details.Title,
                Description = details.Description,
                CustomerID = details.CustomerID,
                Priority = details.Priority,
                Status = details.Status

            };
            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, TicketEdit ticket)
        {
            try
            {
                if (!ModelState.IsValid) return View(ticket);
                if (ticket.TicketID != id)
                {
                    ModelState.AddModelError("", "ID Mismatch, please try again");
                    return View(ticket);
                }
                var service = CreateTicketService();
                if (await service.UpdateTicketByID(ticket))
                {
                    TempData["SaveResult"] = "ticket has been updated.";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Could not update ticket.");
                return View(ticket);
            }
            catch
            {
                ModelState.AddModelError("", "Error. Could not update ticket");
                return View(ticket);
            }
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            var service = CreateTicketService();
            var details = await service.GetTicketByID(id);
            return View(details);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteTicket(Guid id)
        {
            var service = CreateTicketService();
            await service.DeleteNoteAsync(id);
            TempData["SaveResult"] = "Ticket has been Deleted";
            return RedirectToAction("Index");
        }
    }
}
