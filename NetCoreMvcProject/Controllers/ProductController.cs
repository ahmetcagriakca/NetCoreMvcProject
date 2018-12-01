﻿using FirstNetCoreMvcProject.Models;
using FirstNetCoreMvcProject.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FirstNetCoreMvcProject.Controllers
{

    public class ProductController : Controller
    {
        private readonly IHostingEnvironment environment;
        private readonly IProductRepository productRepository;

        public List<SelectListItem> Brands { get; } = new List<SelectListItem>
    {
        new SelectListItem { Value = "Merco", Text = "Merco" },
        new SelectListItem { Value = "BMW", Text = "BMW" },
        new SelectListItem { Value = "Porche", Text = "Porche"  },
    };
        public ProductController(IHostingEnvironment environment, IProductRepository productRepository)
        {
            this.environment = environment;
            this.productRepository = productRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            ProductCreateViewModel model = new ProductCreateViewModel();
            model.Brands = Brands;
            return View(model);
        }

        [HttpPost("CreateAsync")]
        public async Task<IActionResult> CreateAsync(ProductCreateViewModel model)
        {
            //long size = files.Sum(f => f.Length);
            //full path to file in temp location
            //var filePath = Path.GetTempFileName();
            if (model.File.Length > 0)
            {
                var filePath = Path.Combine(environment.WebRootPath, @"TempImages");
                var x = Path.Combine(Directory.GetCurrentDirectory(), @"Images");
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.File.CopyToAsync(stream);
                    model.Product.Image = model.File.FileName;
                }
            }
            productRepository.Create(model.Product);
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            //return Ok(new { count = files.Count, size, filePath });
            return View(RedirectToAction("Create"));
        }

        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size, filePath });
        }
    }
}