using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Newtonsoft.Json;

namespace Csi.Helpers.Azure
{
    public class InstanceMetadata
    {
        public static InstanceMetadata Parse(string text) => JsonConvert.DeserializeObject<InstanceMetadata>(text);

        public ComputeEntity Compute { get; set; }
        public class ComputeEntity
        {
            public string Location { get; set; }
            // for VMSS, it will be scalesetname_instance
            public string Name { get; set; }
            public string ResourceGroupName { get; set; }
            public string SubscriptionId { get; set; }
        }

        public ResourceId GetResourceId()
        {
            var index = Compute.Name.IndexOf('_');
            return index < 0
                ? ResourceIdHelper.CreateForStandaloneVm(
                    Compute.SubscriptionId,
                    Compute.ResourceGroupName,
                    Compute.Name)
                : ResourceIdHelper.CreateForVmssVm(
                    Compute.SubscriptionId,
                    Compute.ResourceGroupName,
                    Compute.Name.Substring(0, index),
                    Compute.Name.Substring(index + 1));
        }
    }
}
