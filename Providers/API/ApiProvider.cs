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
                    Console.WriteLine("This is not null");
                    data = task.Result as List<FirebaseObject<SupplierDto>>;
                }
                else
                {
                    Console.WriteLine("This is null");
                    data = new List<FirebaseObject<SupplierDto>>();
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
                Abbreviation = abbreviation,
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