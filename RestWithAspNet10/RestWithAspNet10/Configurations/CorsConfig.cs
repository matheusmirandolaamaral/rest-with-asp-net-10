namespace RestWithAspNet10.Configurations
{
    public static class CorsConfig
    {
        public static void AddCorsConfinguration(this IServiceCollection services, IConfiguration configuration)
        {
            var origins = configuration.GetSection("Cors:Origins").Get<string[]>() ?? Array.Empty<string>();

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
                options.AddPolicy("DefaultPolicy", policy =>
                {
                    policy.WithOrigins(origins)
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
            });
        }
        public static IApplicationBuilder UseCorsConfiguration(this WebApplication app)
        {
            //app.UseCors();
            app.UseCors("DefaultPolicy");
            return app;

        }
    }
}
