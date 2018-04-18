using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Extensions.Logging;
using Util.Extensions.Logging.Step;

namespace Csi.Helpers.Azure
{
    public interface IInstanceMetadataService
    {
        Task<ResourceId> GetResourceId();
    }

    class InstanceMetadataService : IInstanceMetadataService
    {
        private const string apiVersion = "2017-08-01";
        private const string endpoint = "http://169.254.169.254/metadata/instance?api-version=" + apiVersion;
        private readonly ILogger<InstanceMetadataService> logger;

        public InstanceMetadataService(ILogger<InstanceMetadataService> logger) => this.logger = logger;

        public async Task<ResourceId> GetResourceId()
        {
            ResourceId result = null;
            using (var s = logger.StepDebug("Loading resource id from instance metadata"))
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Metadata", "True");
                try
                {
                    var response = await client.GetStringAsync(endpoint);
                    result = InstanceMetadata.Parse(response).GetResourceId();
                }
                catch (Exception ex)
                {
                    logger.LogWarning("Error when loading resource id from instance metadata: {0}", ex.Message);
                }
                s.Commit();
            }

            return result;
        }
    }
}
