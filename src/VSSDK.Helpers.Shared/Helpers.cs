using System;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
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
            return await ServiceProvider.GetGlobalServiceAsync<TService, TInterface>();
        }

        public static TInterface GetService<TService, TInterface>() where TService : class where TInterface : class
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            return ServiceProvider.GlobalProvider.GetService(typeof(TService)) as TInterface;
        }

        public static T RunSync<T>(Func<Task<T>> asyncMethod)
        {
            return ThreadHelper.JoinableTaskFactory.Run(asyncMethod);
        }
    }
}
