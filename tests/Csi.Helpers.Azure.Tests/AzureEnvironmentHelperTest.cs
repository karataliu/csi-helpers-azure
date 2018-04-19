using System;
using Xunit;

namespace Csi.Helpers.Azure.Tests
{
    public class AzureEnvironmentHelperTest
    {
        [Fact]
        public void DefaultEnvironmentName()
        {
            Assert.Equal("AzureGlobalCloud", AzureEnvironmentHelper.DefaultEnvironmentName);
        }

        [Fact]
        public void ParseAzureEnvironment()
        {
            var globalEnv = AzureEnvironmentHelper.ParseAzureEnvironment("AzureGlobalCloud");
            Assert.NotNull(globalEnv);
            var defaultEnv = AzureEnvironmentHelper.ParseAzureEnvironment("");
            Assert.Equal(globalEnv, defaultEnv);
            var germanEnv = AzureEnvironmentHelper.ParseAzureEnvironment("AzureGermanCloud");
            Assert.NotEqual(globalEnv, germanEnv);

            Assert.Throws<Exception>(() => AzureEnvironmentHelper.ParseAzureEnvironment("unknown"));
        }

        [Fact]
        public void GetStorageEndpointSuffix()
        {
            var azureGlobalCloudSuffix = AzureEnvironmentHelper.GetStorageEndpointSuffix("AzureGlobalCloud");
            Assert.False(string.IsNullOrEmpty(azureGlobalCloudSuffix));
            Assert.Equal(azureGlobalCloudSuffix, AzureEnvironmentHelper.GetStorageEndpointSuffix(""));
            Assert.Equal(azureGlobalCloudSuffix, AzureEnvironmentHelper.GetStorageEndpointSuffix("AzureGlobalCloud"));

            var azureGermanCloudSuffix = AzureEnvironmentHelper.GetStorageEndpointSuffix("AzureGermanCloud");
            Assert.False(string.IsNullOrEmpty(azureGermanCloudSuffix));
            Assert.NotEqual(azureGlobalCloudSuffix, azureGermanCloudSuffix);
            Assert.Equal(azureGermanCloudSuffix, AzureEnvironmentHelper.GetStorageEndpointSuffix("azuregermancloud"));

            Assert.Throws<Exception>(() => AzureEnvironmentHelper.GetStorageEndpointSuffix("a"));
        }
    }
}
