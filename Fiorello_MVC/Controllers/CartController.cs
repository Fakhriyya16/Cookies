using Fiorello_MVC.Data;
using Fiorello_MVC.Models;
using Fiorello_MVC.ViewModels;
using Fiorello_MVC.ViewModels.Baskets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Fiorello_MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public CartController(AppDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;

        }
        public async Task<IActionResult> Index()
        {
            List<Product> test = new();

            var productsInBasket = JsonConvert.DeserializeObject<List<BasketVM>>(_contextAccessor.HttpContext.Request.Cookies["basket"]).ToList();
            foreach(var item in productsInBasket)
            {
                int id = item.Id;
                var product = _context.Products.Include(m=>m.Category).Include(m=>m.ProductImages).FirstOrDefault(m=>m.Id == id);
                test.Add(product);
            }
            List<CartVM> items = test.Select(product => new CartVM
            {
                Id = product.Id,
                Image = product.ProductImages.FirstOrDefault(m=>m.isMain).Name,
                Name = product.Name,
                Description = product.Description,
                Category = product.Category.Name,
                Count = productsInBasket.Find(m=>m.Id == product.Id).Count,
                Price = product.Price,
            }).ToList();

            TotalBasketVM model = new()
            {
                Products = items,
                TotalPrice = productsInBasket.Sum(m => m.Price * m.Count),
            };
            return View(model);
        }

        //[HttpPost]
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null) return BadRequest();

        //    var product = await _context.Products.FirstOrDefaultAsync(m=>m.Id == (int)id);

        //    if (product == null) return NotFound();

        //    List<BasketVM> updatedList = JsonConvert.DeserializeObject<List<BasketVM>>(_contextAccessor.HttpContext.Request.Cookies["basket"]).ToList();

        //    foreach (var item in updatedList)
        //    {
        //        if(item.Id == product.Id)
        //        {
        //            updatedList.Remove(item);
        //        }
        //    }

        //    int count = updatedList.Sum(m => m.Count);
        //    decimal totalPrice = updatedList.Sum(m=>m.Price * m.Count);

        //    return Ok(new{ count,totalPrice});
        //}
    }
}
