using System;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using Microsoft;
using Microsoft.VisualStudio.PlatformUI.OleComponentSupport;
using Microsoft.VisualStudio.Shell;

namespace VS
{
    public static class Helpers
    {
        // DTE
        public static DTE2 GetDTE() => GetService<DTE, DTE2>();
        public static async Task<DTE2> GetDTEAsync() => await GetServiceAsync<DTE, DTE2>();

        public static async Task<TInterface> GetServiceAsync<TService, TInterface>() where TService : class where TInterface : class
        {
#if VS15
            TInterface? service = await ServiceProvider.GetGlobalServiceAsync<TService, TInterface>();
#else
            TInterface? service = await ServiceProvider.GetGlobalServiceAsync<TService, TInterface>(swallowExceptions: false);
#endif
            Assumes.Present(service);
            return service;
        }

        public static TInterface GetService<TService, TInterface>() where TService : class where TInterface : class
        {
            ThreadHelper.ThrowIfNotOnUIThread();
#if VS15
            TInterface? service = ServiceProvider.GlobalProvider.GetService(typeof(TService)) as TInterface;
#else
            TInterface? service = ServiceProvider.GlobalProvider.GetService<TService, TInterface>(throwOnFailure : true);
#endif

            Assumes.Present(service);
            return service;
        }
    }
}
