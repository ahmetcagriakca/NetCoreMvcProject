using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NetCoreMvcProject.Models;
using NetCoreMvcProject.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NetCoreMvcProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHostingEnvironment environment;
        private readonly IProductRepository productRepository;
        private readonly ProductDbContext dbContext;

        public List<SelectListItem> Brands { get; } = new List<SelectListItem>
    {
        new SelectListItem { Value = "Merco", Text = "Merco" },
        new SelectListItem { Value = "BMW", Text = "BMW" },
        new SelectListItem { Value = "Porche", Text = "Porche"  },
    };
        public ProductController(IHostingEnvironment environment, IProductRepository productRepository,
            ProductDbContext dbContext)
        {
            this.environment = environment;
            this.productRepository = productRepository;
            this.dbContext = dbContext;
        }

        [Route("List")]
        public IActionResult Index()
        {
            var list = productRepository.GetAll();
            return View(list);
        }

        public IActionResult Create()
        {
            ProductCreateViewModel model = new ProductCreateViewModel();
            model.Brands = Brands;
            return View(model);
        }


        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = productRepository.GetById(Convert.ToInt32(id));

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            productRepository.Delete(id);

            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("CreateAsync")]
        public async Task<IActionResult> CreateAsync(ProductCreateViewModel model)
        {
            //long size = files.Sum(f => f.Length);
            //full path to file in temp location
            //var filePath = Path.GetTempFileName();
            if (model.File.Length > 0)
            {
                var filePath = Path.Combine(environment.WebRootPath, @"images\\products", model.File.FileName);
                //var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Images\\Products");
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                    model.Product.Image = model.File.FileName;
                }
            }
            productRepository.Create(model.Product);
            dbContext.SaveChanges();
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            //return Ok(new { count = files.Count, size, filePath });
            return RedirectToAction("Index");
        }
    }
}