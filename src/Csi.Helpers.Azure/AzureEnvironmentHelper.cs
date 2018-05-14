using System;
using Microsoft.Azure.Management.ResourceManager.Fluent;

namespace Csi.Helpers.Azure
{
    public static class AzureEnvironmentHelper
    {
        public static string DefaultEnvironmentName => nameof(AzureEnvironment.AzureGlobalCloud);

        public static AzureEnvironment ParseAzureEnvironment(string environmentName)
        {
            if (string.IsNullOrEmpty(environmentName)) return AzureEnvironment.AzureGlobalCloud;

            return AzureEnvironment.FromName(environmentName) 
                ?? throw new Exception("Unknown environment: " + environmentName);
        }

        public static string GetStorageEndpointSuffix(string environmentName)
            => ParseAzureEnvironment(environmentName).StorageEndpointSuffix.TrimStart('.');
    }
}
