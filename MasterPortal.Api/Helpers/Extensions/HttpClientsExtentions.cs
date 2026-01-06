using MasterPortal.Api.Helpers.HttpClients;

namespace MasterPortal.Api.Helpers.Extensions
{
    public static class HttpClientsExtentions
    {
        public static void AddHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient<UsersHttpClient>(op =>
            {
                op.BaseAddress = new Uri("http://localhost:5203");
            });

            services.AddHttpClient<NotificationsHttpClient>(op =>
            {
                op.BaseAddress = new Uri("http://localhost:5202");
            });
        }
    }
}
