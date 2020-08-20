using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FFModels;


namespace Interfaces
{
    public interface IRepository
    {
        Task<FeatureFlagNameCollection> GetActiveFeatureFlagNames();
        Task<KeyResponseModelCollection> GetAllFeatureFlags();
    }
}
