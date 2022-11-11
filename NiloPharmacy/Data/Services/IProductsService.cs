using NiloPharmacy.Data.ViewModels;
using NiloPharmacy.Models;

namespace NiloPharmacy.Data.Services
{
    public interface IProductsService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int Id);

        Task AddAsync(Product supplier);
        Task<Product> UpdateAsync(int Id, Product supplier);
        Task DeleteAsync(int Id);

        Task<NewProductDropDownsVM> GetNewProductsDropdownsValues();
    }
}
