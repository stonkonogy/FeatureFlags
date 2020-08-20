using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Interfaces;
using FFModels;
using Repositories;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;

using Newtonsoft.Json;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FFAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureFlagsController : ControllerBase
    {   //todo use controller injection
        private IRepository _repository; //= new AzureFFAPIRepository();

        public FeatureFlagsController(IRepository repo)
        {
            _repository = repo;
        }
        // GET: api/<FeatureFlagsController>
                

        [HttpGet("GetEnabledFlags")]
        public async Task<FeatureFlagNameCollection> GetEnabledFlags()
        {
            var flags = await _repository.GetActiveFeatureFlagNames();

            return flags;
        }

                        
        public async Task<KeyResponseModelCollection> GetAllFeatureFlags()
        {
            throw new NotImplementedException();
        }
    }
}
