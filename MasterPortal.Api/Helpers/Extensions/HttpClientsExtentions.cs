using MasterPortal.Api.Helpers.HttpClients;
using MasterPortal.Api.Helpers.Models.Configurations.Options;
using Microsoft.Extensions.Options;

namespace MasterPortal.Api.Helpers.Extensions
{
    public static class HttpClientsExtentions
    {
        public static void AddHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient<UsersHttpClient>((sp, op) =>
            {
                var config = sp.GetRequiredService<IOptionsMonitor<ServiceSettingModel>>().Get("UserService");

                op.BaseAddress = new Uri(config.BaseUrl);
                op.DefaultRequestHeaders.Add("x-api-key", config.ApiKey);
            });

            services.AddHttpClient<NotificationsHttpClient>((sp, op) =>
            {
                var config = sp.GetRequiredService<IOptionsMonitor<ServiceSettingModel>>().Get("NotificationService");

                op.BaseAddress = new Uri(config.BaseUrl);
                op.DefaultRequestHeaders.Add("x-api-key", config.ApiKey);
            });
        }
    }
}
