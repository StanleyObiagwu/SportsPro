using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsPro.Data;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    [Authorize]
    public class TechIncidentController : Controller
    {
        private IUnitOfWork UnitOfWork { get; }
        private IGenericRepository<Incident> IncidentRepository { get; }
        private IGenericRepository<Technician> TechnicianRepository { get; }
        private IGenericRepository<Customer> CustomerRepository { get; }
        private IGenericRepository<Product> ProductRepository { get; }

        public TechIncidentController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            IncidentRepository = UnitOfWork.IncidentRepository;
            TechnicianRepository = UnitOfWork.TechnicianRepository;
            CustomerRepository = UnitOfWork.CustomerRepository;
            ProductRepository = UnitOfWork.ProductRepository;
        }
        
        [HttpGet]
        public ViewResult Delete(int id)
        {
            var incident = IncidentRepository.Get(id);
            return View(incident);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Incident incident)
        {
            IncidentRepository.Delete(incident);
            UnitOfWork.Save();
            TempData["message"] = $"{incident.Title} deleted from database";
            return RedirectToAction("List", "Incident");
        }

        [HttpGet]
        public ViewResult ListByTech(string activeIncident = "All", string activeTechnician = "All")
        {

            var model = new IncidentViewModel
            {
                //ActiveIncident = activeIncident,
                ActiveTechnician = activeTechnician,
                //Incidents = context.Incidents.OrderBy(i => i.Title).ToList(),
                Technicians = TechnicianRepository.Get(orderBy:technicians => technicians.OrderBy(c => c.Name)).ToList()

            };
            IEnumerable<Incident> query = IncidentRepository.Get();
            if (activeIncident != "All")
                query = IncidentRepository.Get(incident => incident.IncidentID.ToString() == activeIncident);
            if (activeTechnician != "All")
                query = IncidentRepository.Get(i => i.Technician.TechnicianID.ToString() == activeTechnician);
            model.Incidents = query.ToList();
            return View(model);
        }

        [HttpGet]
        public ViewResult TechIncident(string activeIncident = "All", string activeTechnician = "All")
        {

            var model = new IncidentViewModel
            {
                ActiveIncident = activeIncident,
                ActiveTechnician = activeTechnician,
                Incidents = IncidentRepository.Get(orderBy:i => i.OrderBy(ii => ii.Title)).ToList(),
                Technicians = TechnicianRepository.Get(orderBy:t => t.OrderBy(tt => tt.Name)).ToList(),
                Customers = CustomerRepository.Get(orderBy:c => c.OrderBy(cc => cc.FirstName)).ToList(),
                Products = ProductRepository.Get(orderBy:p => p.OrderBy(pp => pp.Name)).ToList()
            };
            IEnumerable<Incident> query = IncidentRepository.Get();
            if (activeIncident != "All")
                query = IncidentRepository.Get(i => i.IncidentID.ToString() == activeIncident);
            if (activeTechnician != "All")
                query = IncidentRepository.Get(i => i.Technician.TechnicianID.ToString() == activeIncident);
            model.Incidents = query.ToList();
            return View(model);
        }

        
        [HttpPost]

        public ViewResult TechIncident(IncidentViewModel inc)

        {

            var techId = inc.CurrentIncident.TechnicianID;
            var technician = TechnicianRepository.Get(t => t.TechnicianID == techId).Single();
            var model = new IncidentViewModel
            {

                ActiveIncident = inc.ActiveIncident,  
                Incidents = IncidentRepository.Get(orderBy:i => i.OrderBy(ii => ii.Title)).ToList(),
                Technicians = TechnicianRepository.Get(orderBy:t => t.OrderBy(tt => tt.Name)).ToList(),
                Customers = CustomerRepository.Get(orderBy:c => c.OrderBy(cc => cc.FirstName)).ToList(),
                Products = ProductRepository.Get(orderBy:p => p.OrderBy(pp => pp.Name)).ToList(),
                ActiveTechnician = technician.Name
            };

            return View(model);


        }

        [HttpGet]
        public ViewResult EditIncident(int id)
        {
            Incident activeIncident = IncidentRepository.Get(id);
            var model = new IncidentViewModel
            {
                Action = "EditIncident",
                Technicians = TechnicianRepository.Get(orderBy:t => t.OrderBy(tt => tt.Name)).ToList(),
                Customers = CustomerRepository.Get(orderBy:c => c.OrderBy(cc => cc.FirstName)).ToList(),
                Products = ProductRepository.Get(orderBy:p => p.OrderBy(pp => pp.Name)).ToList(),
                CurrentIncident = IncidentRepository.Get(id)
            };
            
            IEnumerable<Incident> query = IncidentRepository.Get();
            if (activeIncident.IncidentID != 0)
                query = IncidentRepository.Get(i => i.IncidentID == activeIncident.IncidentID);
            model.Incidents = query.ToList();
            model.CurrentIncident = IncidentRepository.Get(id);

            return View("EditIncident", model);
        }

        [HttpPost]
        public IActionResult EditIncident(IncidentViewModel model)
        {
            string Action = "EditIncident";

            if (ModelState.IsValid)
            {
                
                IncidentRepository.Update(model.CurrentIncident);
                UnitOfWork.Save();
                TempData["message"] = $"{model.CurrentIncident.Title} {Action}ed to database";
                return RedirectToAction("List", "Incident");
            }
            else
            {

                //model.Action = Action;
                model.CurrentIncident = IncidentRepository.Get(model.CurrentIncident.IncidentID);                
                return View("EditIncident", model);
            }
        }
    }
}
