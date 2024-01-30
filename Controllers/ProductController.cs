using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC6CRUD.Data;
using MVC6CRUD.Models;
using MVC6CRUD.ViewModel;

namespace MVC6CRUD.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<ProductListViewModel> products = new List<ProductListViewModel>();
            var productsList = _context.Products;
            foreach (var product in productsList)
            {
                ProductListViewModel model = new ProductListViewModel();
                model.Id = product.Id;
                model.Name = product.Name;
                model.Description = product.Description;
                model.Color = product.Color;
                model.Price = product.Price;
                model.CategoryId = product.CategoryId;
                model.Image = product.Image;
                model.CategoryName = _context.Categories.Where(c => c.CategoryId == model.CategoryId).Select(c => c.CategoryName).FirstOrDefault();
                products.Add(model);
            }
            return View(products);
        }
        public IActionResult Create()
        {
            ProductViewModel model = new ProductViewModel();
            model.Category = (IEnumerable<SelectListItem>)_context.Categories.Select(c => new SelectListItem()
            {
                Text = c.CategoryName,
                Value = c.CategoryId.ToString()
            });
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductViewModel model)
        {
            model.Category = (IEnumerable<SelectListItem>)_context.Categories.Select(c => new SelectListItem()
            {
                Text = c.CategoryName,
                Value = c.CategoryId.ToString()
            });
            var product = new Product()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Color = model.Color,
                CategoryId = model.CategoryId,
                Image = model.Image
            };
            ModelState.Remove("Category");
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                TempData["SuccessMsg"] = "Product (" + product.Name + ") added successfully.";
                return RedirectToAction("Index");
            }

            return View(model);


        }
        public ActionResult Edit(int? id)
        {
            var productList = _context.Products.Find(id);
            if (productList != null)
            {
                var model = new ProductViewModel()
                {
                    Id = productList.Id,
                    Name = productList.Name,
                    Description = productList.Description,
                    Price = productList.Price,
                    CategoryId = productList.CategoryId,
                    Color = productList.Color,
                    Image = productList.Image,
                    Category = (IEnumerable<SelectListItem>)_context.Categories.Select(c => new SelectListItem()
                    {
                        Text = c.CategoryName,
                        Value = c.CategoryId.ToString()
                    })
                };
                return View(model);
            }
            else
            {
                return NotFound();
            }
        }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Edit(ProductViewModel productViewModel)
            {
                productViewModel.Category = (IEnumerable<SelectListItem>)_context.Categories.Select(c => new SelectListItem()
                {
                    Text = c.CategoryName,
                    Value = c.CategoryId.ToString()
                });
                var product = new Product()
                {
                    Id = productViewModel.Id,
                    Name = productViewModel.Name,
                    Description = productViewModel.Description,
                    Price = productViewModel.Price,
                    Color = productViewModel.Color,
                    CategoryId = productViewModel.CategoryId,
                    Image = productViewModel.Image
                };
                ModelState.Remove("Category");
                if (ModelState.IsValid)
                {
                    _context.Products.Update(product);
                    _context.SaveChanges();
                    TempData["SuccessMsg"] = "Product (" + product.Name + ") updated successfully !";
                    return RedirectToAction("Index");
                }
                return View(productViewModel);

            }
        public IActionResult Delete(int? id)
        {
            var productToEdit = _context.Products.Find(id);
            if (productToEdit != null)
            {
                var productViewModel = new ProductViewModel()
                {
                    Id = productToEdit.Id,
                    Name = productToEdit.Name,
                    Description = productToEdit.Description,
                    Price = productToEdit.Price,
                    CategoryId = productToEdit.CategoryId,
                    Color = productToEdit.Color,
                    Image = productToEdit.Image,
                    Category = (IEnumerable<SelectListItem>)_context.Categories.Select(c => new SelectListItem()
                    {
                        Text = c.CategoryName,
                        Value = c.CategoryId.ToString()
                    })
                };
                return View(productViewModel);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(int? id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            TempData["SuccessMsg"] = "Product (" + product.Name + ") deleted successfully.";
            return RedirectToAction("Index");
        }
    }
    } 
