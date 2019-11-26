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
        
        public List<Measurement> LoadMeasurements()
        {
            _measurements = ApiProvider.ApiMeasurements();
            if (_measurements.Result == null) return new List<Measurement>();
            return _measurements.Result.Select(item => new Measurement
                {
                    Id = item.Object.Id,
                    Name = item.Object.Name,
                    Abbreviation = item.Object.Abbreviation,
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
            if (_products.Result == null) return new List<Product>();
            return _products.Result.Select(item => new Product
                {
                    Id = item.Object.Id,
                    Category = item.Object.CategoryId,
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