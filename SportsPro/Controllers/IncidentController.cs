using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace SportsPro.Controllers
{
    [Authorize]
    public class IncidentController : Controller
    {
        private SportsProContext context { get; set; }

        public IncidentController(SportsProContext ctx)
        {
            context = ctx;
        }

        [Route("Incidents")]
        [HttpGet]
        public ViewResult List(string activeIncident = "All", string activeTechnician = "All")
        {
            string FilterString = HttpContext.Session.GetString("FilterString");
            var model = new IncidentViewModel
            {
                ActiveIncident = activeIncident,
                ActiveTechnician = activeTechnician,
                Incidents = context.Incidents.OrderBy(i => i.Title).ToList(),
                Technicians = context.Technicians.OrderBy(c => c.Name).ToList(),
                Customers = context.Customers.OrderBy(c => c.FirstName).ToList(),
                Products = context.Products.OrderBy(p => p.Name).ToList(),
                               
            };
            IQueryable<Incident> query = context.Incidents; 
            if (FilterString != "null")
            {
                if (FilterString != "unassigned")
                    query = query.Where(i => i.TechnicianID == null);
                if (FilterString != "open")
                    query = query.Where(i => i.DateClosed == null);
            }
            model.Incidents = query.ToList();
            return View(model);
        }



        [HttpGet]
        public ViewResult Add()
        {
            var model = new IncidentViewModel
            {
                Action = "Add",
                Technicians = context.Technicians.OrderBy(c => c.Name).ToList(),
                Customers = context.Customers.OrderBy(c => c.FirstName).ToList(),
                Products = context.Products.OrderBy(p => p.Name).ToList(),

            };
            
            return View("Edit", model);
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            var model = new IncidentViewModel
            {
                Action = "Edit",
                Technicians = context.Technicians.OrderBy(c => c.Name).ToList(),
                Customers = context.Customers.OrderBy(c => c.FirstName).ToList(),
                Products = context.Products.OrderBy(p => p.Name).ToList(),
                CurrentIncident = context.Incidents.Find(id)

            };           
            
            
            return View("Edit", model);
        }

        [HttpPost]
        public IActionResult Edit(IncidentViewModel model)
        {
            string Action = "Edit";
            if (model.CurrentIncident.IncidentID == 0)
                Action = "Add";

                if (ModelState.IsValid)
                {
                if (model.CurrentIncident.IncidentID == 0)
                    context.Incidents.Add(model.CurrentIncident);
                else
                    context.Incidents.Update(model.CurrentIncident);
                context.SaveChanges();
                TempData["message"] = $"{model.CurrentIncident.Title} {Action}ed to database";
                return RedirectToAction("List", "Incident");
                }
            else
            {


                model.Action = Action;
                model.Technicians = context.Technicians.OrderBy(c => c.Name).ToList();
                model.Customers = context.Customers.OrderBy(c => c.FirstName).ToList();
                model.Products = context.Products.OrderBy(p => p.Name).ToList();

            
                return View(model);
            }
        }

        [HttpGet]
        public ViewResult Delete(int id)
        {
            var incident = context.Incidents.Find(id);
            return View(incident);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Incident incident)
        {
            context.Incidents.Remove(incident);
            context.SaveChanges();
            TempData["message"] = $"{incident.Title} deleted from database";
            return RedirectToAction("List", "Incident");
        }

        public IActionResult FilterAll()
        {
            HttpContext.Session.SetString("FilterString", "null");
            return RedirectToAction("List");
        }

        public IActionResult FilterUnassigned()
        {
            HttpContext.Session.SetString("FilterString", "unassigned");
            return RedirectToAction("List");
        }

        public IActionResult FilterOpen()
        {
            HttpContext.Session.SetString("FilterString", "open");
            return RedirectToAction("List");
        }
    }
}
