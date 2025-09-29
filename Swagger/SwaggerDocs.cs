using Microsoft.OpenApi.Models;

namespace Supermarket.Swagger
{
    public static class SwaggerDocs
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("auth", new OpenApiInfo
                {
                    Title = "Authentication API",
                    Version = "v1",
                    Description = "API quản lý authentication: đăng nhập, đăng ký, đổi mật khẩu, quên mật khẩu",
                    Contact = new OpenApiContact
                    {
                        Name = "Supermarket Team",
                        Email = "support@supermarket.com"
                    }
                });

                c.SwaggerDoc("taikhoan", new OpenApiInfo
                {
                    Title = "TaiKhoan Management API",
                    Version = "v1",
                    Description = "API quản lý tài khoản: CRUD operations, quản lý trạng thái",
                    Contact = new OpenApiContact
                    {
                        Name = "Supermarket Team",
                        Email = "support@supermarket.com"
                    }
                });

                c.SwaggerDoc("nhanvien", new OpenApiInfo
                {
                    Title = "Quản lý Nhân viên",
                    Version = "v1",
                    Description = "API quản lý nhân viên: CRUD operations, quản lý nhân viên",
                    Contact = new OpenApiContact
                    {
                        Name = "Supermarket Team",
                        Email = "support@supermarket.com"
                    }
                });

                // Add default Swagger document
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Supermarket API",
                    Version = "v1",
                    Description = "Supermarket Management System API",
                    Contact = new OpenApiContact
                    {
                        Name = "Supermarket Team",
                        Email = "support@supermarket.com"
                    }
                });

                // Bearer authentication is intentionally disabled in Swagger UI to allow testing without tokens.

                // Group endpoints by tags and assign to specific documents
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

                c.DocInclusionPredicate((docName, api) =>
                {
                    var controllerName = api.ActionDescriptor.RouteValues["controller"];
                    return docName switch
                    {
                        "auth" => controllerName == "Auth",
                        "taikhoan" => controllerName == "TaiKhoanManagement",
                        "nhanvien" => controllerName == "NhanVien",
                        "v1" => true, // Default document includes all controllers
                        _ => true
                    };
                });

                // Include XML comments if available
                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = System.IO.Path.Combine(System.AppContext.BaseDirectory, xmlFile);
                if (System.IO.File.Exists(xmlPath))
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
                    // Configure multiple Swagger endpoints
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Supermarket API (All)");
                    c.SwaggerEndpoint("/swagger/auth/swagger.json", "Authentication API");
                    c.SwaggerEndpoint("/swagger/taikhoan/swagger.json", "TaiKhoan Management API");
                    c.SwaggerEndpoint("/swagger/nhanvien/swagger.json", "NhanVien Management API");

                    // Set default to v1 (all APIs)
                    c.RoutePrefix = "swagger";
                    c.DocumentTitle = "Supermarket API Documentation";
                });
            }
        }
    }
}
