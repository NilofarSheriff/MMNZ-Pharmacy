using Microsoft.AspNetCore.Mvc;
using NiloPharmacy.Data.Cart;
using NiloPharmacy.Data.Services;
using NiloPharmacy.Data.ViewModels;

namespace NiloPharmacy.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IProductsService _service;
        private readonly ShoppingCart _shoppingCart;

        public OrdersController( IProductsService Service, ShoppingCart shoppingCart)
        {
            _service = Service;
            _shoppingCart = shoppingCart;
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
    }
}
