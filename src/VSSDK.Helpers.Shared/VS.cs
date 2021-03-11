using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;

namespace Microsoft.VisualStudio.Helpers
{
    /// <summary>
    /// The entry point for a wide variety of extensibility helper classes and methods.
    /// </summary>
    public static class VS
    {
        public static Commanding Commanding => new();
        public static Debugger Debugger => new();
        public static Editor Editor => new();
        public static Notifications Notifications => new();
        public static Shell Shell => new();
        public static Solution Solution => new();
        public static Windows Windows => new();


        public static Task<DTE2> GetDTEAsync() => GetServiceAsync<DTE, DTE2>();

        /// <summary>
        /// Gets a global service asynchronously.
        /// </summary>
        /// <typeparam name="TService">The type identity of the service.</typeparam>
        /// <typeparam name="TInterface">The interface to cast the service to.</typeparam>
        /// <returns>A task whose result is the service, if found; otherwise null.</returns>
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
    }
}
