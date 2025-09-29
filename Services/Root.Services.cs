using Supermarket.Repositories.NhanVienRepositories;
using Supermarket.Repositories.TaiKhoanRepositories;
using Supermarket.Repositories.AuthRepositories;

namespace Supermarket.Services
{
    public static class RootServices
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<INhanVienRepository, NhanVienRepository>();
            services.AddScoped<ITaiKhoanRepositories, TaiKhoanRepositories>();
            services.AddScoped<IAuthRepositories, AuthRepositories>();
            services.AddScoped<ITaiKhoanManagementRepositories, TaiKhoanManagementRepositories>();
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<NhanVienService>();
            services.AddScoped<ITaiKhoanService, TaiKhoanService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITaiKhoanManagementService, TaiKhoanManagementService>();
        }

        public static void RegisterAllServices(this IServiceCollection services)
        {
            services.RegisterRepositories();
            services.RegisterServices();
        }
    }
}
