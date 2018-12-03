using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreMvcProject.Models
{
    public class ProductCreateViewModel
    {
        public Product Product { get; set; }
        public IFormFile File { get; set; }
        public List<SelectListItem> Brands{ get; set; }
        public string Brand { get; set; }
    }
}
