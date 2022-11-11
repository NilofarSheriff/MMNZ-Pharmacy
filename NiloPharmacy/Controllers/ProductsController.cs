using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NiloPharmacy.Data;
using NiloPharmacy.Data.Services;
using NiloPharmacy.Models;

namespace NiloPharmacy.Controllers
{
    public class ProductsController : Controller
    {
        
        
        private readonly IProductsService _service;

        public ProductsController(IProductsService Service)
        {
            _service = Service;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Welcome"] = "Welcome to our XYZ Pharmacy";
            ViewBag.Description = "XYZ Pharmacy is your go-to online pharmacy store " +
                "for all your medicine needs – be it your regular medications, or over-the-counter (OTC) medicines.";
            var data = await _service.GetAllAsync();
            return View(data);
        }

        public async Task<IActionResult> CreateAsync()
        {
            
            var movieDropdownsData = await _service.GetNewProductsDropdownsValues();

            ViewBag.Suppliers = new SelectList(movieDropdownsData.Suppliers, "SupplierId", "SupplierName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductName","ProductImage","ProductPrice","CategoryName","SupplierId","MedicinalUse","ExpiryDate","MedicineDesc")] Product prod)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View(prod);

            }
            await _service.AddAsync(prod);
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public async Task<IActionResult> Details(int Id)
        {
            var SupplierDetails = await _service.GetByIdAsync(Id);
            if (SupplierDetails == null)
            {
                return View("Not Found");
            }
            return View(SupplierDetails);
        }

        
        public async Task<IActionResult> Edit(int Id)
        {
            var movieDropdownsData = await _service.GetNewProductsDropdownsValues();

            ViewBag.Suppliers = new SelectList(movieDropdownsData.Suppliers, "SupplierId", "SupplierName");
            var supplier = await _service.GetByIdAsync(Id);
            if (supplier == null)
            {
                return View("Not Found");
            }
            return View(supplier);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id,[Bind("ProductName", "ProductImage", "ProductPrice", "CategoryName", "SupplierId", "MedicinalUse", "ExpiryDate", "MedicineDesc")] Product supplier)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View(supplier);

            }
            await _service.UpdateAsync(Id, supplier);
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Filter(string searchString)
        {
            var allProd = await _service.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allProd.Where(n => n.ProductName.Contains(searchString) || n.MedicineDesc.Contains(searchString) || n.MedicinalUse.ToString().Contains(searchString) || n.CategoryName.ToString().Contains(searchString)).ToList();
                return View("Index", filteredResult);
                
            }

            return View("Index", allProd);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            var supplier = await _service.GetByIdAsync(Id);
            if (supplier == null)
            {
                return View("Not Found");
            }
            return View(supplier);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            var supplier = await _service.GetByIdAsync(Id);
            if (supplier == null)
            {
                return View("Not Found");
            }
            await _service.DeleteAsync(Id);

            return RedirectToAction(nameof(Index));

        }
    }
}
