using Microsoft.OpenApi.Models;

namespace Supermarket.Swagger
{
    public static class SwaggerDocs
    {
        private static readonly OpenApiContact Contact = new()
        {
            Name = "Supermarket Team",
            Email = "support@supermarket.com"
        };

        private static readonly (string Name, string Title, string Description)[] ApiDocs = new[]
        {
            ("auth", "Authentication API", "API quản lý authentication: đăng nhập, đăng ký, đổi mật khẩu, quên mật khẩu"),
            ("taikhoan", "TaiKhoan Management API", "API quản lý tài khoản: CRUD operations, quản lý trạng thái"),
            ("nhanvien", "Quản lý Nhân viên", "API quản lý nhân viên: CRUD operations, quản lý nhân viên"),
            ("v1", "Supermarket API", "Supermarket Management System API")
        };

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                // Configure Swagger documents
                foreach (var (name, title, description) in ApiDocs)
                {
                    c.SwaggerDoc(name, new OpenApiInfo
                    {
                        Title = title,
                        Version = "v1",
                        Description = description,
                        Contact = Contact
                    });
                }

                // Group endpoints by tags
                c.TagActionsBy(api =>
                {
                    var controllerName = api.ActionDescriptor.RouteValues["controller"];
                    var tagName = controllerName switch
                    {
                        "Auth" => "Xác thực và truy cập",
                        "TaiKhoanManagement" => "Quản lý Tài khoản",
                        "NhanVien" => "Quản lý Nhân viên",
                        _ => controllerName
                    };
                    return new[] { tagName };
                });

                // Filter endpoints for specific documents
                c.DocInclusionPredicate((docName, api) =>
                {
                    var controllerName = api.ActionDescriptor.RouteValues["controller"];
                    return docName switch
                    {
                        "auth" => controllerName == "Auth",
                        "taikhoan" => controllerName == "TaiKhoanManagement",
                        "nhanvien" => controllerName == "NhanVien",
                        "v1" => true,
                        _ => true
                    };
                });

                // Include XML comments if available
                var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml");
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
            });
        }

        public static void ConfigureSwaggerUI(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    // Configure Swagger endpoints
                    foreach (var (name, title, _) in ApiDocs)
                    {
                        c.SwaggerEndpoint($"/swagger/{name}/swagger.json", title);
                    }

                    c.RoutePrefix = "swagger";
                    c.DocumentTitle = "Supermarket API Documentation";
                    c.EnableFilter(); // Keep search bar
                    c.InjectStylesheet("/swagger-custom.css"); // Custom CSS for better UI
                    c.DefaultModelExpandDepth(2);
                    c.DefaultModelsExpandDepth(1);
                    c.DisplayRequestDuration();
                    c.EnableDeepLinking();
                });
            }
        }
    }
}