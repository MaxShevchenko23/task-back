using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using url_shortener_server.Controllers;
using url_shortener_server.shortener_dal;
using url_shortener_server.shortener_dal.Repositories;

namespace url_shortener_server.Helpers
{
    public class ServicesInjector
    {
        public static void Inject(IServiceCollection services, IConfiguration configuration)
        {

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<ShortenerDbContext>();

            services.AddScoped<LinkRepository>();
            services.AddScoped<UserRepository>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin() 
                               .AllowAnyMethod() 
                               .AllowAnyHeader();
                    });
            });

            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer",

                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });

                options.CustomSchemaIds(type => type.FullName.Replace('.', '_'));

                var basePath = AppContext.BaseDirectory;

            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Authentication:Issuer"],
                    ValidAudience = configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Authentication:Secret"]))
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("MustBeRegistered", policy =>
                {
                    policy.RequireAuthenticatedUser();
                });
            });

        }

    }
}
