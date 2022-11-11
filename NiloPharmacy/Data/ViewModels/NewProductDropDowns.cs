using NiloPharmacy.Models;

namespace NiloPharmacy.Data.ViewModels
{
    public class NewProductDropDownsVM
    {
        public NewProductDropDownsVM()
        {
            Suppliers = new List<Supplier>();
            
        }

        public List<Supplier> Suppliers { get; set; }
        
    }

}
