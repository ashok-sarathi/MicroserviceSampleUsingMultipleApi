using MasterPortal.Api.Helpers.Models.Configurations.Options;

namespace MasterPortal.Api.Helpers.Extensions
{
    public static class ConfigOptionsExtentions
    {
        public static void AddConfigOptions(this IServiceCollection services)
        {
            services.AddOptions<ServiceSettingModel>("UserService")
                .BindConfiguration("ServiceSettings:UserService");

            services.AddOptions<ServiceSettingModel>("NotificationService")
                .BindConfiguration("ServiceSettings:NotificationService");

            services.AddOptions<ServiceSettingModel>("DummyJsonService")
                .BindConfiguration("ServiceSettings:DummyJsonService");
        }
    }
}
