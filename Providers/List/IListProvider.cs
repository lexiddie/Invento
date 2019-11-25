using Invento.Models;
using System.Collections.Generic;
using Firebase.Database;

namespace Invento.Providers.List
{
    public interface IListProvider
    {
        List<Measurement> LoadMeasurements();
        
        List<Supplier> LoadSuppliers();
    }
}