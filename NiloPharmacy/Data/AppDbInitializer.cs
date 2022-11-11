﻿using NiloPharmacy.Data.Enums;
using NiloPharmacy.Models;

namespace NiloPharmacy.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var servicescope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = servicescope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();
                if (!context.Suppliers.Any())
                {
                    context.Suppliers.AddRange(new List<Supplier>()
                    {
                        new Supplier()
                        {
                            
                            SupplierName="MMNZ ltd",
                            SupplierAddress = "Salem, TamilNadu",
                            
                        },
                        new Supplier()
                        {
                            
                            SupplierName="MKS Ltd",
                            SupplierAddress="Bangalore, Karnataka"
                        }


                    });
                    context.SaveChanges();

                }
                if (!context.Products.Any())

                    context.Products.AddRange(new List<Product>()
                    {
                        new Product()
                        {
                            ProductName = "Dolo650",
                            ProductPrice=4,
                            CategoryName = Category.Tablet,
                            MedicinalUse = MedicinalUse.Fever,
                            ProductImage="~/Image/1.jpeg",
                            SupplierId = 1,
                            ExpiryDate = new DateTime(2025,10,27),
                            MedicineDesc = "Dolo 650 Tablet helps relieve pain and fever by blocking the" +
                            " release of certain chemical messengers responsible for fever and pain."
                        },
                        new Product()
                        {
                            ProductName = "Safi",
                            ProductPrice=4,
                            CategoryName = Category.Syrup,
                            MedicinalUse = MedicinalUse.Cold,
                            ProductImage="~/Image/2.jpeg",
                            SupplierId = 2,
                            ExpiryDate = new DateTime(2035,11,07),
                            MedicineDesc = "Safi syrup is a non toxic blood purifier. Unique ayurvedic " +
                            "formulation for acne, pimples, blemishes, skin boils, skin rashes and other skin infections."
                        }


                    }) ;
                context.SaveChanges();

                
                
            }
        }
    }
}