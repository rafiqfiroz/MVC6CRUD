using Microsoft.AspNetCore.Mvc;
using MVC6CRUD.Data;
using MVC6CRUD.Models;

namespace MVC6CRUD.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categories = _context.Categories;
            return View(categories);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                TempData["SuccessMsg"] = "Category (" + category.CategoryName + ") added successfully.";
                return RedirectToAction("Index");

            }
            return View(category);
        }
        public IActionResult Edit(int? id)
        {
            //var category = _context.Categories.FirstOrDefault();
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                //category = new Category();
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
                TempData["SuccessMsg"] = "Category (" + category.CategoryName + ") updated successfully.";
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public IActionResult Delete(int? id)
        {
            var category=_context.Categories.Find(id);
            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category id)
        {
           
              var category=  _context.Categories.Find(id);
                if (category ==null)
                {
                    return NotFound(category);
                }
                _context.Categories.Remove(category);
                _context.SaveChanges();
                TempData["SuccessMsg"] = "Category (" + category.CategoryName + ") deleted successfully.";
                return RedirectToAction("Index");
            }
           
        }
    }

