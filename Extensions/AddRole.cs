namespace Web_API_Assessment.Extensions
{
    public static class AddRole
    {
        public static WebApplicationBuilder addAdminAuthorization(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("Role", "Admin");
                });

            });
            return builder;
        }
    }
}
