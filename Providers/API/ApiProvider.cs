using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Invento.Dtos;
using Invento.Models;
using Newtonsoft.Json;

namespace Invento.Providers.API
{
    public class ApiProvider : IApiProvider
    {
        private static readonly FirebaseClient Firebase = new FirebaseClient("https://invento-e28df.firebaseio.com/");


        public async Task<List<FirebaseObject<MeasurementDto>>> ApiMeasurements()
        {
            var data = new List<FirebaseObject<MeasurementDto>>();
            await Firebase.Child("measurements").OrderByKey().OnceAsync<MeasurementDto>().ContinueWith(task =>
            {
                if (task.IsCompleted && task.Result != null)
                {
                    Console.WriteLine("This is not null");
                    data = task.Result as List<FirebaseObject<MeasurementDto>>;
                }
                else
                {
                    Console.WriteLine("This is null");
                    data = new List<FirebaseObject<MeasurementDto>>();
                }
            });
//            data = (List<FirebaseObject<MeasurementDto>>) await Firebase.Child("measurements").OrderByKey().OnceAsync<MeasurementDto>();
            return data;
        }

        public async Task<List<FirebaseObject<SupplierDto>>> ApiSuppliers()
        {
            var data = new List<FirebaseObject<SupplierDto>>();
            await Firebase.Child("suppliers").OrderByKey().OnceAsync<SupplierDto>().ContinueWith(task =>
            {
                if (task.IsCompleted && task.Result != null)
                {
                    data = task.Result as List<FirebaseObject<SupplierDto>>;
                }
                else
                {
                    data = new List<FirebaseObject<SupplierDto>>();
                }
            });
            return data;
        }

        public async Task<List<FirebaseObject<CategoryDto>>> ApiCategories()
        {
            var data = new List<FirebaseObject<CategoryDto>>();
            await Firebase.Child("categories").OrderByKey().OnceAsync<CategoryDto>().ContinueWith(task =>
            {
                if (task.IsCompleted && task.Result != null)
                {
                    data = task.Result as List<FirebaseObject<CategoryDto>>;
                }
                else
                {
                    data = new List<FirebaseObject<CategoryDto>>();
                }
            });
            return data;
        }

        public async Task<List<FirebaseObject<ProductDto>>> ApiProducts()
        {
            var data = new List<FirebaseObject<ProductDto>>();
            await Firebase.Child("products").OrderByKey().OnceAsync<ProductDto>().ContinueWith(task =>
            {
                if (task.IsCompleted && task.Result != null)
                {
                    data = task.Result as List<FirebaseObject<ProductDto>>;
                }
                else
                {
                    data = new List<FirebaseObject<ProductDto>>();
                }
            });
            return data;
        }

        public async Task<List<FirebaseObject<PurchaseDto>>> ApiPurchase()
        {
            var data = new List<FirebaseObject<PurchaseDto>>();
            await Firebase.Child("purchases").OrderByKey().OnceAsync<PurchaseDto>().ContinueWith(task =>
            {
                if (task.IsCompleted && task.Result != null)
                {
                    data = task.Result as List<FirebaseObject<PurchaseDto>>;
                }
                else
                {
                    data = new List<FirebaseObject<PurchaseDto>>();
                }
            });
            return data;
        }

        public async Task<List<FirebaseObject<UsageDto>>> ApiUsage()
        {
            var data = new List<FirebaseObject<UsageDto>>();
            await Firebase.Child("usages").OrderByKey().OnceAsync<UsageDto>().ContinueWith(task =>
            {
                if (task.IsCompleted && task.Result != null)
                {
                    data = task.Result as List<FirebaseObject<UsageDto>>;
                }
                else
                {
                    data = new List<FirebaseObject<UsageDto>>();
                }
            });
            return data;
        }

        public async Task<List<FirebaseObject<LeftoverDto>>> ApiLeftover()
        {
            var data = new List<FirebaseObject<LeftoverDto>>();
            await Firebase.Child("leftovers").OrderByKey().OnceAsync<LeftoverDto>().ContinueWith(task =>
            {
                if (task.IsCompleted && task.Result != null)
                {
                    data = task.Result as List<FirebaseObject<LeftoverDto>>;
                }
                else
                {
                    data = new List<FirebaseObject<LeftoverDto>>();
                }
            });
            return data;
        }

        public async Task<dynamic> ApiCreateMeasurement(string id, string name, string abbreviation, string description, bool status)
        {
            var measurement = new MeasurementDto
            {
                Id = id,
                Name = name,
                Description = description,
                IsActive = status,
                CreatedAt = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                CreatedBy = "Admin"
            };
            await Firebase.Child("measurements").Child(id).PutAsync(measurement);
            return JsonConvert.SerializeObject(new { isSuccess = true} as dynamic);
        }
    }
}