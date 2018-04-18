using Xunit;

namespace Csi.Helpers.Azure.Tests
{
    public class ResourceIdHelperTest
    {
        [Fact]
        public void Standalone()
        {
            Assert.Equal(
                "/subscriptions/fe9ce53f-277c-48bb-950d-265eba922b9d/resourcegroups/rg1"
                + "/providers/Microsoft.Compute/virtualMachines/n111",
                ResourceIdHelper.CreateForStandaloneVm("fe9ce53f-277c-48bb-950d-265eba922b9d", "rg1", "n111").Id);
        }

        [Fact]
        public void Vmss()
        {
            Assert.Equal(
                "/subscriptions/fe9ce53f-277c-48bb-950d-265eba922b9d/resourcegroups/rg2/providers"
                + "/Microsoft.Compute/virtualMachineScaleSets/agents/virtualMachines/15",
                ResourceIdHelper.CreateForVmssVm("fe9ce53f-277c-48bb-950d-265eba922b9d", "rg2", "agents", "15").Id);
        }
    }
}
