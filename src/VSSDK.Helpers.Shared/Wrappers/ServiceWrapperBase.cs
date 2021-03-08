using System.Threading.Tasks;

namespace VS
{
    public class ServiceWrapperBase<TService, TInterface> where TService : class where TInterface : class
    {
        public static async Task<TInterface> GetServiceAsync()
        {
            return await Helpers.GetServiceAsync<TService, TInterface>();
        }

        public static TInterface GetService()
        {
            return Helpers.RunSync(GetServiceAsync);
        }
    }
}