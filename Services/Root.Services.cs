using Supermarket.Repositories.NhanVienRepositories;
using Supermarket.Repositories.KhachHangRepositories;
using Supermarket.Repositories.TaiKhoanRepositories;
using Supermarket.Repositories.AuthRepositories;
using Supermarket.Repositories.SanPhamRepositories;
using Supermarket.Repositories.HoaDonRepositories;

namespace Supermarket.Services
{
    public static class RootServices
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<INhanVienRepository, NhanVienRepository>();
            services.AddScoped<IKhachHangRepository, KhachHangRepository>();
            services.AddScoped<IAuthRepositories, AuthRepositories>();
            services.AddScoped<ITaiKhoanRepositories, TaiKhoanRepositories>();
            services.AddScoped<ISanPhamRepository, SanPhamRepository>();
            services.AddScoped<IHoaDonRepository, HoaDonRepository>();
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<NhanVienService>();
            services.AddScoped<KhachHangService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITaiKhoanService, TaiKhoanService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<ISanPhamService, SanPhamService>();
            services.AddScoped<IHoaDonService, HoaDonService>();
        }

        public static void RegisterAllServices(this IServiceCollection services)
        {
            services.RegisterRepositories();
            services.RegisterServices();
        }
    }
}
