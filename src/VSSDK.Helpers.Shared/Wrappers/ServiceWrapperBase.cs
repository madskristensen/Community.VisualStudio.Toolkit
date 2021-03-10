using System.Threading.Tasks;

namespace VS
{
    public class ServiceWrapperBase<TService, TInterface> where TService : class where TInterface : class
    {
        public static Task<TInterface> GetServiceAsync()
        {
            return Helpers.GetServiceAsync<TService, TInterface>();
        }

        public static TInterface GetService()
        {
            return Helpers.GetService<TService, TInterface>();
        }
    }
}