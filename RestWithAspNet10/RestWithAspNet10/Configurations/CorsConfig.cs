namespace RestWithAspNet10.Configurations
{
    public static class CorsConfig
    {
        public static void AddCorsConfinguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("LocalPolicy", policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
                options.AddPolicy("MultipleOriginPolicy", policy =>
                {
                    policy.WithOrigins(
                        "http://localhost:3000"
                        ,"http://localhost:8080"
                        ,"https://erudio.com.br")
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
            });
        }
        public static IApplicationBuilder UseCorsConfiguration(this WebApplication app)
        {
            app.UseCors();
            return app;

        }
    }
}
