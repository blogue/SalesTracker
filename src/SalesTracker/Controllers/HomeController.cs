using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalesTracker.Data;
using SalesTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace SalesTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            ViewData["ItemId"] = new SelectList(_context.Item, "ItemId", "Name");
            ViewData["Items"] = _context.Item.ToList();
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> NewSale(int ItemId, int Quantity)
        {
            Sale newSale = new Sale(ItemId, Quantity);
            var selectedItem = _context.Item.FirstOrDefault(i => i.ItemId == ItemId);
            selectedItem.Quantity -= Quantity;
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            newSale.User = currentUser;
            _context.Sale.Add(newSale);
            _context.SaveChanges();
            return Json(newSale);
        }

        [HttpPost]
        public IActionResult ReturnItem(FormCollection collection)
        {
            var sale = _context.Sale.FirstOrDefault(s => s.SaleId == int.Parse(Request.Form["id"]));
             var selectedItem = _context.Item.FirstOrDefault(i => i.ItemId == sale.ItemId);
            selectedItem.Quantity += sale.Quantity;
            _context.Sale.Remove(sale);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public IActionResult DisplayInventory()
        {
            var items = _context.Item.ToList();
            return PartialView(items);
        }

        [HttpPost]
        public IActionResult DisplayInventory(int CurrentPage, int LastPage)
        {
            ViewBag.CurrentPage = CurrentPage;
            ViewBag.LastPage = LastPage;
            return PartialView(_context.Item.OrderBy(i => i.Name).Skip((CurrentPage - 1) * 5).Take(5));
        }

        public IActionResult DisplaySales()
        {
            var sales = _context.Sale.Include(s => s.Item).Include(s => s.User).ToList();
            return PartialView(sales);
        }

        public async Task<string> UpdateSalesComment(int saleId, string newComment)
        {
            var sale = _context.Sale.FirstOrDefault(s => s.SaleId == saleId);
            sale.SalesComment = newComment;
            var result = await _context.SaveChangesAsync();
            if (result != 0)
            {
                return newComment;
            } else
            {
                return "there was a failure";
            }
        }

        public IActionResult GetRevenue()
        {
            int revenue = 0;
            var sales = _context.Sale.Include(s => s.Item);
            foreach (var sale in sales)
            {
                revenue += sale.Quantity *= sale.Item.Price;
            }
            return Json(revenue);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult HelloAjax()
        {
            return Content("Hello from the controller!", "text/plain");
        }
    }
}
