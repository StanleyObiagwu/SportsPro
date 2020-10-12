using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    [Authorize]
    public class TechnicianController : Controller
    {
        private SportsProContext context { get; set; }

        public TechnicianController(SportsProContext ctx)
        {
            context = ctx;
        }

        [Route("Technicians")]
        [HttpGet]
        public ViewResult List()
        {
            ViewBag.Action = "Edit";
            var Technicians = context.Technicians.OrderBy(g => g.Name).ToList();
            return View(Technicians);
        }

        [HttpGet]
        public ViewResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new Technician());
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Technicians = context.Technicians.OrderBy(g => g.Name).ToList();
            var technician = context.Technicians.Find(id);
            return View(technician);
        }

        [HttpPost]
        public IActionResult Edit(Technician technician)
        {
            if (ModelState.IsValid)
            {
                if (technician.TechnicianID == 0)
                    context.Technicians.Add(technician);
                else
                    context.Technicians.Update(technician);
                context.SaveChanges();
                TempData["message"] = $"{technician.Name} added to database";
                return RedirectToAction("List", "Technician");
            }
            else
            {
                ViewBag.Action = (technician.TechnicianID == 0) ? "Add" : "Edit";
                ViewBag.Technicians = context.Products.OrderBy(g => g.Name).ToList();
                return View(technician);
            }
        }

        [HttpGet]
        public ViewResult Delete(int id)
        {
            var technician = context.Technicians.Find(id);
            return View(technician);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Technician technician)
        {
            context.Technicians.Remove(technician);
            context.SaveChanges();
            TempData["message"] = $"{technician.Name} deleted from database";
            return RedirectToAction("List", "Technician");
        }
    }
}
