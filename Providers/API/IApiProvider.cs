using System.Threading.Tasks;
using System.Collections.Generic;
using Firebase.Database;
using Invento.Dtos;

namespace Invento.Providers.API
{
    public interface IApiProvider
    {
        Task<List<FirebaseObject<MeasurementDto>>> ApiMeasurements();
        
        Task<List<FirebaseObject<SupplierDto>>> ApiSuppliers();
        
        Task<List<FirebaseObject<CategoryDto>>> ApiCategories();
        
        Task<List<FirebaseObject<ProductDto>>> ApiProducts();
        
        Task<List<FirebaseObject<PurchaseDto>>> ApiPurchase();
        
        Task<List<FirebaseObject<UsageDto>>> ApiUsage();
        
        Task<List<FirebaseObject<LeftoverDto>>> ApiLeftover();

        Task<dynamic> ApiCreateMeasurement(string id, string name, string abbreviation, string description, bool status);
    }
}