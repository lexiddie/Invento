using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Invento.Dtos;
using Invento.Helpers;
using Invento.Models;
using Invento.Providers.API;

namespace Invento.Providers.List
{
    public class ListProvider: IListProvider
    {
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
            if (_products.Result == null) return new List<Product>();
            return _products.Result.Select(item => new Product
                {
                    Id = item.Object.Id,
                    Category = LoadCategory(item.Object.CategoryId),
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
                    Measurement = LoadMeasurement(item.Object.ProductId),
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
                    Measurement = LoadMeasurement(item.Object.ProductId),
                    Code = item.Object.Code,
                    Quantity = item.Object.Quantity,
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
                    Measurement = LoadMeasurement(item.Object.ProductId),
                    Code = item.Object.Code,
                    Quantity = item.Object.Quantity,
                    IsVoid = item.Object.IsVoid,
                    CreatedDate = ReadDateTime.ReadDate(item.Object.CreatedAt),
                    CreatedTime = ReadDateTime.ReadTime(item.Object.CreatedAt),
                    CreatedBy = item.Object.CreatedBy
                })
                .ToList();
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

        private string LoadMeasurement(string productId)
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
        
        private string LoadSupplier(string supplierId)
        {
            var supplierName = "";
            foreach (var item in _measurements.Result.Where(item => supplierId == item.Object.Id))
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