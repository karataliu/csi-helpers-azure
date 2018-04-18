using Xunit;

namespace Csi.Helpers.Azure.Tests
{
    public class InstanceMetadataTest
    {
        [Fact]
        public void Standalone()
        {
            var json = @"
{
    ""compute"": {
        ""location"": ""eastus"",
        ""name"": ""n111"",
        ""resourceGroupName"": ""RG1"",
        ""subscriptionId"": ""FE9CE53F-277C-48BB-950D-265EBA922B9D"",
        ""zone"": """"
    },
    ""network"": {}
}
";
            var im = InstanceMetadata.Parse(json);
            Assert.Equal("n111", im.Compute.Name);
            Assert.Equal("RG1", im.Compute.ResourceGroupName);
            Assert.Equal("FE9CE53F-277C-48BB-950D-265EBA922B9D", im.Compute.SubscriptionId);

            Assert.Equal(
                "/subscriptions/fe9ce53f-277c-48bb-950d-265eba922b9d/resourcegroups/rg1"
                + "/providers/Microsoft.Compute/virtualMachines/n111",
                im.GetResourceId().Id);
        }

        [Fact]
        public void Vmss()
        {
            var json = @"
{
    ""compute"": {
        ""location"": ""eastus"",
        ""name"": ""agents_15"",
        ""resourceGroupName"": ""rg2"",
        ""subscriptionId"": ""FE9CE53F-277C-48BB-950D-265EBA922B9D"",
        ""zone"": """"
    },
    ""network"": {}
}
";
            var im = InstanceMetadata.Parse(json);
            Assert.Equal("agents_15", im.Compute.Name);
            Assert.Equal("rg2", im.Compute.ResourceGroupName);
            Assert.Equal("FE9CE53F-277C-48BB-950D-265EBA922B9D", im.Compute.SubscriptionId);
            Assert.Equal(
                "/subscriptions/fe9ce53f-277c-48bb-950d-265eba922b9d/resourcegroups/rg2"
                + "/providers/Microsoft.Compute/virtualMachineScaleSets/agents/virtualMachines/15",
                im.GetResourceId().Id);
        }
    }
}
