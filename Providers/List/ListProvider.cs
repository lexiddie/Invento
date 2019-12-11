using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Invento.Dtos;
using Invento.Helpers;
using Invento.Models;
using Invento.Providers.API;
using Microsoft.EntityFrameworkCore.Storage.Internal;

namespace Invento.Providers.List
{
    public class ListProvider: IListProvider
    {
        private const string Url = "https://invento-e28df.firebaseio.com/";
        
        private static readonly ApiProvider ApiProvider = new ApiProvider();
        
        private Task<List<FirebaseObject<MeasurementDto>>> _measurements;
        
        private Task<List<FirebaseObject<SupplierDto>>> _suppliers;
        
        private Task<List<FirebaseObject<CategoryDto>>> _categories;
        
        private Task<List<FirebaseObject<ProductDto>>> _products;
        
        private Task<List<FirebaseObject<PurchaseDto>>> _purchases;
        
        private Task<List<FirebaseObject<UsageDto>>> _usages;
        
        private Task<List<FirebaseObject<LeftoverDto>>> _leftovers;
        
        public List<Measurement> LoadMeasurements()
        {
            _measurements = ApiProvider.ApiMeasurements();
            if (_measurements.Result == null) return new List<Measurement>();
            return _measurements.Result.Select(item => new Measurement
                {
                    Id = item.Object.Id,
                    Name = item.Object.Name,
                    Description = item.Object.Description,
                    IsActive = item.Object.IsActive,
                    CreatedDate = ReadDateTime.ReadDate(item.Object.CreatedAt),
                    CreatedTime = ReadDateTime.ReadTime(item.Object.CreatedAt),
                    CreatedBy = item.Object.CreatedBy
                })
                .ToList();
        }

        public List<Supplier> LoadSuppliers()
        {
            _suppliers = ApiProvider.ApiSuppliers();
            if (_suppliers.Result == null) return new List<Supplier>();
            return _suppliers.Result.Select(item => new Supplier
                {
                    Id = item.Object.Id,
                    Name = item.Object.Name,
                    Email = item.Object.Email,
                    TelephoneNumber = item.Object.TelephoneNumber,
                    Country = item.Object.Country,
                    Address = item.Object.Address,
                    Description = item.Object.Description,
                    IsActive = item.Object.IsActive,
                    CreatedDate = ReadDateTime.ReadDate(item.Object.CreatedAt),
                    CreatedTime = ReadDateTime.ReadTime(item.Object.CreatedAt),
                    CreatedBy = item.Object.CreatedBy
                })
                .ToList();
        }

        public List<Category> LoadCategories()
        {
            _categories = ApiProvider.ApiCategories();
            if (_categories.Result == null) return new List<Category>();
            return _categories.Result.Select(item => new Category
                {
                    Id = item.Object.Id,
                    Name = item.Object.Name,
                    Description = item.Object.Description,
                    IsActive = item.Object.IsActive,
                    CreatedDate = ReadDateTime.ReadDate(item.Object.CreatedAt),
                    CreatedTime = ReadDateTime.ReadTime(item.Object.CreatedAt),
                    CreatedBy = item.Object.CreatedBy
                })
                .ToList();
        }

        public List<Product> LoadProducts()
        {
            _products = ApiProvider.ApiProducts();
            _categories = ApiProvider.ApiCategories();
            _measurements = ApiProvider.ApiMeasurements();
            if (_products.Result == null) return new List<Product>();
            return _products.Result.Select(item => new Product
                {
                    Id = item.Object.Id,
                    Category = LoadCategory(item.Object.CategoryId),
                    Measurement = LoadMeasurement(item.Object.MeasurementId),
                    Name = item.Object.Name,
                    Code = item.Object.Code,
                    Description = item.Object.Description,
                    IsActive = item.Object.IsActive,
                    CreatedDate = ReadDateTime.ReadDate(item.Object.CreatedAt),
                    CreatedTime = ReadDateTime.ReadTime(item.Object.CreatedAt),
                    CreatedBy = item.Object.CreatedBy
                })
                .ToList();
        }

        public List<Purchase> LoadPurchases()
        {
            _purchases = ApiProvider.ApiPurchase();
            _products = ApiProvider.ApiProducts();
            _measurements = ApiProvider.ApiMeasurements();
            _suppliers = ApiProvider.ApiSuppliers();
            if (_purchases.Result == null) return new List<Purchase>();
            return _purchases.Result.Select(item => new Purchase
                {
                    Id = item.Object.Id,
                    Product = LoadProduct(item.Object.ProductId),
                    Measurement = LoadMeasurementByProductId(item.Object.ProductId),
                    Supplier = LoadSupplier(item.Object.SupplierId),
                    Code = item.Object.Code,
                    Quantity = item.Object.Quantity,
                    Price = item.Object.Price,
                    IsVoid = item.Object.IsVoid,
                    CreatedDate = ReadDateTime.ReadDate(item.Object.CreatedAt),
                    CreatedTime = ReadDateTime.ReadTime(item.Object.CreatedAt),
                    CreatedBy = item.Object.CreatedBy
                })
                .ToList();
        }

        public List<Usage> LoadUsages()
        {
            _usages = ApiProvider.ApiUsage();
            _products = ApiProvider.ApiProducts();
            _measurements = ApiProvider.ApiMeasurements();
            if (_usages.Result == null) return new List<Usage>();
            return _usages.Result.Select(item => new Usage
                {
                    Id = item.Object.Id,
                    Product = LoadProduct(item.Object.ProductId),
                    Measurement = LoadMeasurementByProductId(item.Object.ProductId),
                    Code = item.Object.Code,
                    Quantity = item.Object.Quantity,
                    Description = item.Object.Description,
                    IsVoid = item.Object.IsVoid,
                    CreatedDate = ReadDateTime.ReadDate(item.Object.CreatedAt),
                    CreatedTime = ReadDateTime.ReadTime(item.Object.CreatedAt),
                    CreatedBy = item.Object.CreatedBy
                })
                .ToList();
        }

        public List<Leftover> LoadLeftovers()
        {
            _leftovers = ApiProvider.ApiLeftover();
            _products = ApiProvider.ApiProducts();
            _measurements = ApiProvider.ApiMeasurements();
            if (_leftovers.Result == null) return new List<Leftover>();
            return _leftovers.Result.Select(item => new Leftover
                {
                    Id = item.Object.Id,
                    Product = LoadProduct(item.Object.ProductId),
                    Measurement = LoadMeasurementByProductId(item.Object.ProductId),
                    Code = item.Object.Code,
                    Quantity = item.Object.Quantity,
                    Description = item.Object.Description,
                    IsVoid = item.Object.IsVoid,
                    CreatedDate = ReadDateTime.ReadDate(item.Object.CreatedAt),
                    CreatedTime = ReadDateTime.ReadTime(item.Object.CreatedAt),
                    CreatedBy = item.Object.CreatedBy
                })
                .ToList();
        }

        public List<Inventory> LoadInventories()
        {
            _products = ApiProvider.ApiProducts();
            _measurements = ApiProvider.ApiMeasurements();
            _purchases = ApiProvider.ApiPurchase();
            _usages = ApiProvider.ApiUsage();
            _leftovers = ApiProvider.ApiLeftover();
            return _products.Result.Where(item => item.Object.IsActive)
                .Select(i => new Inventory
                {
                    Code = i.Object.Code,
                    Product = i.Object.Name,
                    Measurement = LoadMeasurement(i.Object.MeasurementId),
                    Quantity = LoadQuantity(i.Object.Id),
                    Usage = LoadUsage(i.Object.Id),
                    Leftover = LoadLeftover(i.Object.Id),
                    Available = LoadQuantity(i.Object.Id) - (LoadUsage(i.Object.Id) + LoadLeftover(i.Object.Id))
                })
                .ToList();
        }

        public int MeasurementQuantity()
        {
            _measurements = ApiProvider.ApiMeasurements();
            return _measurements.Result.Count(item => item.Object.IsActive);
        }
        
        public int SupplierQuantity()
        {
            _suppliers = ApiProvider.ApiSuppliers();
            return _suppliers.Result.Count(item => item.Object.IsActive);
        }

        public int CategoryQuantity()
        {
            _categories = ApiProvider.ApiCategories();
            return _categories.Result.Count(item => item.Object.IsActive);
        }
        
        public int ProductQuantity()
        {
            _products = ApiProvider.ApiProducts();
            return _products.Result.Count(item => item.Object.IsActive);
        }

        public int PurchaseQuantity()
        {
            _purchases = ApiProvider.ApiPurchase();
            return _purchases.Result.Count(item => !item.Object.IsVoid);
        }
        
        public int UsageQuantity()
        {
            _usages = ApiProvider.ApiUsage();
            return _usages.Result.Count(item => !item.Object.IsVoid);
        }
        
        public int LeftoverQuantity()
        {
            _leftovers = ApiProvider.ApiLeftover();
            return _leftovers.Result.Count(item => !item.Object.IsVoid);
        }


        private string LoadProduct(string productId)
        {
            var productName = "";
            foreach (var item in _products.Result.Where(item => productId == item.Object.Id))
            {
                productName = item.Object.Name;
                break;
            }
            return productName;
        }

        private string LoadMeasurementByProductId(string productId)
        {
            var measurementName = "";
            foreach (var i in _products.Result.Where(item => productId == item.Object.Id))
            {
                foreach (var j in _measurements.Result.Where(item => i.Object.MeasurementId == item.Object.Id))
                {
                    measurementName = j.Object.Name;
                    break;
                }
            }
            return measurementName;
        }
        
        private string LoadMeasurement(string measurementId)
        {
            var measurementName = "";
            foreach (var j in _measurements.Result.Where(item => measurementId == item.Object.Id))
            {
                measurementName = j.Object.Name;
                break;
            }
            return measurementName;
        }
        
        private string LoadSupplier(string supplierId)
        {
            var supplierName = "";
            foreach (var item in _suppliers.Result.Where(item => supplierId == item.Object.Id))
            {
                supplierName = item.Object.Name;
                break;
            }
            return supplierName;
        }

        private string LoadCategory(string categoryId)
        {
            var categoryName = "";
            foreach (var item in _categories.Result.Where(item => categoryId == item.Object.Id))
            {
                categoryName = item.Object.Name;
                break;
            }
            return categoryName;
        }
        
        public string GeneratePurchase()
        {
            _purchases = ApiProvider.ApiPurchase();
            if (_purchases.Result == null) return "PC1000000";
            var current = _purchases.Result[_purchases.Result.Count - 1].Object.Code;
            return "PC" + (Convert.ToInt64(current.Substring(2)) + 1);
        }
        
        public string GenerateUsage()
        {
            _usages = ApiProvider.ApiUsage();
            if (_usages.Result == null) return "UA1000000";
            var current = _usages.Result[_usages.Result.Count - 1].Object.Code;
            return "UA" + (Convert.ToInt64(current.Substring(2)) + 1);
        }
        
        public string GenerateLeftover()
        {
            _leftovers = ApiProvider.ApiLeftover();
            if (_leftovers.Result == null) return "LO1000000";
            var current = _leftovers.Result[_leftovers.Result.Count - 1].Object.Code;
            return "LO" + (Convert.ToInt64(current.Substring(2)) + 1);
        }

        public int LoadAvailable(string productId)
        {
            _purchases = ApiProvider.ApiPurchase();
            _usages = ApiProvider.ApiUsage();
            _leftovers = ApiProvider.ApiLeftover();
            var total = _purchases.Result?.Where(item => !item.Object.IsVoid && item.Object.ProductId == productId).Sum(item => item.Object.Quantity) ?? 0;
            return total - (LoadUsage(productId) + LoadLeftover(productId));
        }

        private int LoadQuantity(string productId)
        {
            return _purchases.Result?.Where(item => !item.Object.IsVoid && item.Object.ProductId == productId).Sum(item => item.Object.Quantity) ?? 0;
        }

        private int LoadUsage(string productId)
        {
            return _usages.Result?.Where(item => !item.Object.IsVoid && item.Object.ProductId == productId).Sum(item => item.Object.Quantity) ?? 0;
        }
        
        private int LoadLeftover(string productId)
        {
            return _leftovers.Result?.Where(item => !item.Object.IsVoid && item.Object.ProductId == productId).Sum(item => item.Object.Quantity) ?? 0;
        }

//        public List<Measurement> LoadMeasurements()
//        {
//            _measurements = ApiProvider.ApiMeasurements();
//            return _measurements.Result.Select(item => new Measurement
//                {
//                    Id = item.Object.Id,
//                    Name = item.Object.Name,
//                    Abbreviation = item.Object.Abbreviation,
//                    Description = item.Object.Description,
//                    IsActive = item.Object.IsActive,
//                    CreatedDate = ReadDateTime.ReadDate(item.Object.CreatedAt),
//                    CreatedTime = ReadDateTime.ReadTime(item.Object.CreatedAt),
//                    CreatedBy = item.Object.CreatedBy
//                })
//                .ToList();
//        }
    }
}