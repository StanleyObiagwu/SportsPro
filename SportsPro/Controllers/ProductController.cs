using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using System.Linq;
using SportsPro.Data;
using Microsoft.AspNetCore.Authorization;

namespace SportsPro.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private IGenericRepository<Product> ProductRepo { get; }
        private IUnitOfWork UnitOfWork { get; }

        public ProductController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            ProductRepo = unitOfWork.ProductRepository;
        }

        [Route("Products")]
        [HttpGet]
        public ViewResult List()
        {
            ViewBag.Action = "Edit";
            var Products = ProductRepo.Get(orderBy: products => products.OrderBy(g => g.Name)).ToList();
            return View(Products);
        }

        [HttpGet]
        public ViewResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit",new Product());
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Products = ProductRepo.Get(orderBy: products => products.OrderBy(g => g.Name)).ToList();
            var product = ProductRepo.Get(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductID == 0)
                    ProductRepo.Insert(product);
                else
                    ProductRepo.Update(product);
                UnitOfWork.Save();
                TempData["message"] = $"{product.Name} added to database";
                return RedirectToAction("List", "Product");
            }
            else
            {
                ViewBag.Action = (product.ProductID == 0) ? "Add" : "Edit";
                ViewBag.Products = ProductRepo.Get(orderBy: products => products.OrderBy(g => g.Name)).ToList();
                return View(product);
            }
        }

        [HttpGet]
        public ViewResult Delete(int id)
        {
            var product = ProductRepo.Get(id);
            return View (product);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Product product)
        {
            ProductRepo.Delete(product);
            TempData["message"] = $"{product.Name} deleted from database";
            return RedirectToAction ("List", "Product");
        }
    }

}
