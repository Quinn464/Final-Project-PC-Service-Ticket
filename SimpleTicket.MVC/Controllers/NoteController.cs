using Microsoft.AspNet.Identity;
using SimpleTicket.Models.NoteModels;
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
    public class NoteController : Controller
    {    

        public NoteService CreateNoteService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userID);
            return service;
        }

      
        public async Task<ActionResult> Index()
        {
            var service = CreateNoteService();
            var list = await service.GetNotesAsync();
            return View(list);
        }

        
        public async Task<ActionResult> Details(int id)
        {
            var service = CreateNoteService();
            var note = await service.GetNoteByIDAsync(id);
            return View(note);
        }

        
        public ActionResult Create(Guid id)
        {
            var service = CreateNoteService();
            var model = new NoteCreate
            {
                TicketID = id
            };
            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NoteCreate note)
        {
            if (!ModelState.IsValid) return View(note);
            var service = CreateNoteService();
            if (await service.CreateNoteAsync(note))
            {
                TempData["SaveResult"] = "Your note was created.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Could not create note.");
            return View(note);
        }

        
        public async Task<ActionResult> Edit(int id)
        {
            var service = CreateNoteService();
            var details = await service.GetNoteByIDAsync(id);
            var model = new NoteEdit
            {
                ID = details.ID,
                Creator = details.Creator,
                Body = details.Body,
                TicketID = details.TicketID
            };
            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, NoteEdit note)
        {
            if (!ModelState.IsValid) return View(note);
            if (note.ID != id)
            {
                ModelState.AddModelError("", "ID Mismatch, please try again");
                return View(note);
            }
            var service = CreateNoteService();
            if (await service.UpdateNoteAsync(note))
            {
                TempData["SaveResult"] = "note has been updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Could not update note.");
            return View(note);
        }

        
        public async Task<ActionResult> Delete(int id)
        {
            var service = CreateNoteService();
            var detail = await service.GetNoteByIDAsync(id);
            return View(detail);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var service = CreateNoteService();
            await service.DeleteNoteAsync(id);
            TempData["SaveResult"] = "Note has been deleted";
            return RedirectToAction("Index");
        }
    }
}
