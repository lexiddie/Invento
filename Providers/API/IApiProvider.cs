using System.Threading.Tasks;
using System.Collections.Generic;
using Firebase.Database;
using Invento.Dtos;

namespace Invento.Providers.API
{
    public interface IApiProvider
    {
        List<FirebaseObject<MeasurementDto>> ApiMeasurements();

        Task<dynamic> ApiCreateMeasurement();
    }
}