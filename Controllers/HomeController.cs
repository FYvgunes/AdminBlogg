using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AdminBlog.Models;

namespace AdminBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BlogContent _content;

        public HomeController(ILogger<HomeController> logger,BlogContent content)
        {
            _logger = logger;
            _content = content;
        }
        public async Task<IActionResult> AddCategory(Category category)
        {
            if(category.CategoryID==0)
            {
                await _content.AddAsync(category);
               
            }
            else
            {
                _content.Update(category);
            }
            await _content.SaveChangesAsync();

            return RedirectToAction(nameof(Category));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Category()
        {
            List<Category> list = _content.Categories.ToList();
            return View(list);
        }
        public IActionResult Author()
        {
            return View();
        }
        public async Task<IActionResult> CategoryDetails(int id)
        {
            var Category = await _content.Categories.FindAsync(id);
            return View(Category);
        }
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            Category category = await _content.Categories.FindAsync(id);
            _content.Remove(category);
            await _content.SaveChangesAsync();
            return RedirectToAction(nameof(Category));

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
