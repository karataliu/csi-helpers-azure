using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace Csi.Helpers.Azure
{
    public static class ResourceIdHelper
    {
        public static ResourceId CreateForStandaloneVm(
            string subscriptionId,
            string resourceGroupName,
            string vmName)
            => ResourceId.FromString(
                ResourceUtils.ConstructResourceId(
                   subscriptionId.ToLower(),
                   resourceGroupName.ToLower(),
                   "Microsoft.Compute",
                   "virtualMachines",
                   vmName.ToLower(),
                   ""));

        public static ResourceId CreateForVmssVm(
            string subscriptionId,
            string resourceGroupName,
            string vmssName,
            string instanceId)
            => ResourceId.FromString(
                ResourceUtils.ConstructResourceId(
                   subscriptionId.ToLower(),
                   resourceGroupName.ToLower(),
                   "Microsoft.Compute",
                   "virtualMachines",
                   instanceId.ToLower(),
                   $"virtualMachineScaleSets/{vmssName}"));
    }
}
