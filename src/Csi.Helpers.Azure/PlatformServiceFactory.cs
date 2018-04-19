using System;
using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;

namespace Csi.Helpers.Azure
{
    public interface IPlatformServiceProvider<TService>
    {
        IPlatformServiceProvider<TService> WithPlatform<TImplementation>(OSPlatform osplatform)
            where TImplementation : TService;
        Type GetService();
    }

    sealed class PlatformServiceProvider<TService> : IPlatformServiceProvider<TService>
    {
        private Type type = null;
        public IPlatformServiceProvider<TService> WithPlatform<TImplementation>(OSPlatform osplatform)
            where TImplementation : TService
        {
            if (RuntimeInformation.IsOSPlatform(osplatform)) type = typeof(TImplementation);
            return this;
        }

        public Type GetService() => type ?? throw new NotImplementedException();
    }

    public static class PlatformServiceProviderExtensions
    {
        public static IServiceCollection AddPlatformService<TService>(
            this IServiceCollection sc,
            Action<IPlatformServiceProvider<TService>> action)
        {
            var psp = new PlatformServiceProvider<TService>();
            action(psp);
            return sc.AddSingleton(typeof(TService), psp.GetService());
        }
    }
}
