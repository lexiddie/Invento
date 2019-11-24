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