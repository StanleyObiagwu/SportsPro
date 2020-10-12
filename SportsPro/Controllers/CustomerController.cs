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
    public class CustomerController : Controller
    {
        private SportsProContext context { get; set; }

        public CustomerController(SportsProContext ctx)
        {
            context = ctx;
        }

        [Route("Customers")]
        [HttpGet]
        public ViewResult List()
        {
            ViewBag.Action = "Edit";
            var Customers = context.Customers.OrderBy(g => g.FirstName).ToList();
            return View(Customers);
        }

        [HttpGet]
        public ViewResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Countries = context.Countries.OrderBy(g => g.Name).ToList();
            return View("Edit", new Customer());
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Countries = context.Countries.OrderBy(g => g.Name).ToList();
            var customer = context.Customers.Find(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (customer.CustomerID == 0)
                    context.Customers.Add(customer);
                else
                    context.Customers.Update(customer);
                context.SaveChanges();
                TempData["message"] = $"{customer.FullName} added to database";
                return RedirectToAction("List", "Customer");
            }
            else
            {
                ViewBag.Action = (customer.CustomerID == 0) ? "Add" : "Edit";
                ViewBag.Countries = context.Countries.OrderBy(g => g.Name).ToList();
                return View(customer);
            }
        }

        [HttpGet]
        public ViewResult Delete(int id)
        {
            var customer = context.Customers.Find(id);
            return View(customer);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Customer customer)
        {
            context.Customers.Remove(customer);
            context.SaveChanges();
            TempData["message"] = $"{customer.FullName} deleted from database";
            return RedirectToAction("List", "Customer");
        }
    }
}
