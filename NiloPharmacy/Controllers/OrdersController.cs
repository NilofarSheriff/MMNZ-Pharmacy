
using Rotativa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NiloPharmacy.Data.Cart;
using NiloPharmacy.Data.Services;
using NiloPharmacy.Data.Static;
using NiloPharmacy.Data.ViewModels;
using NiloPharmacy.Models;
using System.Security.Claims;
using Rotativa.NetCore;

namespace NiloPharmacy.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IProductsService _service;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _orderservice;


        public OrdersController( IProductsService Service, ShoppingCart shoppingCart,IOrdersService ordersService)
        {
            _service = Service;
            _shoppingCart = shoppingCart;
            _orderservice = ordersService;
        }

        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }
        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await _orderservice.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var orders = await _orderservice.GetOrdersByUserIdAndRoleAsync(userId, userRole);
            return View(orders);
        }
    
        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _service.GetByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _service.GetByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> Delete(int Id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var orders = await _orderservice.GetOrdersByUserIdAndRoleAsync(userId, userRole);
            //return View(orders);
            //List<Order> orders = await _orderservice.GetOrders();
            Order found = orders.FirstOrDefault(X => X.Id == Id)!;
            return View(found);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            List<Order> orderlist = await _orderservice.GetOrders();
            if (orderlist == null)
            {
                return View("NotFound");
            }
            await _orderservice.DeleteAsync(id);

            return View("OrderCancellation"); ;




        }

    }
}
