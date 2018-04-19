using System;
using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Csi.Helpers.Azure.Tests
{
    public class PlatformServiceProviderTest
    {
        [Fact]
        public void Basic()
        {
            var sc = new ServiceCollection();
            sc.AddPlatformService<ITestServiceA>(psp => psp
                .WithPlatform<TestServiceAImpl1>(OSPlatform.Linux)
                .WithPlatform<TestServiceAImpl1>(OSPlatform.Windows));
            var sp = sc.BuildServiceProvider();
            var objA = sp.GetService<ITestServiceA>();
            Assert.NotNull(objA);
            Assert.IsType<TestServiceAImpl1>(objA);
        }

        [Fact]
        public void NotImplemented()
        {
            var sc = new ServiceCollection();
            Assert.Throws<NotImplementedException>(() => sc.AddPlatformService<ITestServiceA>(psp => { }));
        }
    }

    interface ITestServiceA { }
    class TestServiceAImpl1 : ITestServiceA { }
}
