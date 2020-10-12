using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Data;
using SportsPro.Models;


namespace SportsPro.Controllers
{
    [Authorize]
    public class RegistrationController : Controller
    {
        private IUnitOfWork UnitOfWork { get; }

        public RegistrationController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

       
        public ViewResult GetCustomer()
        {
            HttpContext.Session.Remove("SessionID");
            Customer activeCustomer = new Customer();
            var model = new RegisterViewModel
            {
                CurrentCustomer = activeCustomer,
                Customers = UnitOfWork.CustomerRepository.Get().ToList(),
                Products = UnitOfWork.ProductRepository.Get(orderBy:product => product.OrderBy(p => p.Name)).ToList()
            };
            model.Customers = UnitOfWork.CustomerRepository.Get().ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Register(int id = 0)
        {
            return Registrations(id);
        }

        [HttpPost]
        public IActionResult Registrations(int customerID=0) 
        {
                    HttpContext.Session.SetInt32("SessionID", customerID);
            // int? sessionID = HttpContext.Session.GetInt32("sessionID");
            // if (customerID == 0 || sessionID == null)
            if (customerID == 0)
            {
                TempData["message"] = "Please select a customer";
                return RedirectToAction("GetCustomer");
            }
            else
            {
               
                // if (customerID == 0) customerID = (int)sessionID;
                var model = new RegisterViewModel();
                model.CurrentCustomer = UnitOfWork.CustomerRepository.Get(customerID);
                model.Customers = UnitOfWork.CustomerRepository.Get().ToList();
                model.Products = UnitOfWork.ProductRepository.Get().ToList();

                IEnumerable<Registration> query = UnitOfWork.RegistrationRepository.Get(reg => reg.CustomerID == model.CurrentCustomer.CustomerID);
                model.Registrations = query.ToList();

                if (model.Registrations.Count == 0)
                    TempData["message"] = $"No registered products for this customer";

                return View("Registrations",model);
            }
        }

        

        [HttpPost]
        public IActionResult Add(Registration registration, int productId, int customerId)
        {
            int? sessionID = HttpContext.Session.GetInt32("sessionID");
            if (registration.ProductID == 0 && sessionID == null)
            {
                TempData["message"] = "Please select a product to register";
                return RedirectToAction("Registrations", "Registration");
            }
            else
            {
                UnitOfWork.RegistrationRepository.Insert(new Registration() { CustomerID = customerId, ProductID = productId });
                UnitOfWork.Save();
                return RedirectToAction("Register", new { id = customerId });
            }
        }

     
        [HttpPost]
        public IActionResult Delete(int productId, int customerId)
        {
            
          
            UnitOfWork.RegistrationRepository.Delete(
                new Registration() { CustomerID = customerId, ProductID = productId });
            UnitOfWork.Save();
            
            return RedirectToAction("Register",new { id=customerId});
        }

      
    }
}
