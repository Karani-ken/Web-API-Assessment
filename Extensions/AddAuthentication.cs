using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace Web_API_Assessment.Extensions
{
    public static class AddAuthentication
    {
        public static WebApplicationBuilder AddAppAuthentication(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,

                    //what is valid
                    ValidAudience = builder.Configuration["TokenSecurity:Audience"],
                    ValidIssuer = builder.Configuration["TokenSecurity:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenSecurity:SecretKey"]))
                };
            });

            return builder;
        }
    }
}
