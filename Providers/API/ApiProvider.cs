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

//        //Completion API
//        public async Task<List<SemesterCompletion>> CompletionSemesters()
//        {
//            List<SemesterCompletion> data;
//            using (var httpClient = new HttpClient())
//            {
//                using (var response = await httpClient.GetAsync(Url + "Semester/SelectList"))
//                {
//                    var res = await response.Content.ReadAsStringAsync();
//                    data = JsonConvert.DeserializeObject<List<SemesterCompletion>>(res);
//                }
//            }
//            return data;
//        }
        
//        public async Task<List<Measurement>> ApiMeasurements()
//        {
//            
//            
//            //            var data = await firebase
//            //                .Child("User")
//            //                .OrderByKey()
//            //                .StartAt("pterodactyl")
//            //                .LimitToFirst(2)
//            //                .OnceAsync<Dinosaur>();
//
//            var data = await firebase.Child("measurements").OrderByKey().OnceAsync<Measurement>();
//            Console.WriteLine(data.ToString());
//            Console.WriteLine(data.Count);
//
//            foreach (var item in data)
//            {
//                Console.WriteLine($"{item.Key} {item.Object.ToString()}");
//            }
//
//            return data;
//        }
        public List<FirebaseObject<MeasurementDto>> ApiMeasurements()
        {
            var data = new List<FirebaseObject<MeasurementDto>>();
            Firebase.Child("measurements").OrderByKey().OnceAsync<MeasurementDto>().ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    data = task.Result as List<FirebaseObject<MeasurementDto>>;
                }
            });
//            data = (List<FirebaseObject<MeasurementDto>>) await Firebase.Child("measurements").OrderByKey().OnceAsync<MeasurementDto>();
            return data;
        }

        public async Task<dynamic> ApiCreateMeasurement()
        {
            var measurement = new MeasurementDto
            {
                Abbreviation = "Kg",
                Name = "Kilogram",
                Description = "This is Kilogram",
                IsActive = true,
                CreatedAt = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                CreatedBy = "Admin"
            };
            await Firebase.Child("measurements").PostAsync(measurement);
            return JsonConvert.SerializeObject(new { isSuccess = true} as dynamic);
        }
    }
}