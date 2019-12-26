using System;
using Invento.Models;
using System.Collections.Generic;
using Firebase.Database;

namespace Invento.Providers.List
{
    public interface IListProvider
    {
        List<Measurement> LoadMeasurements();
        
        List<Supplier> LoadSuppliers();
        
        List<Category> LoadCategories();
        
        List<Product> LoadProducts();
        
        List<Purchase> LoadPurchases();
        
        List<Usage> LoadUsages();
        
        List<Leftover> LoadLeftovers();
        
        List<Inventory> LoadInventories();

        Boolean CheckProduct(string name, string code);
    }
}