using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using News_API.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using News_API.Data;
using News_API.Models;

namespace News_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            { // Authorization For All project
                var policy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }); // Routing instead of AddMvc()

            //DB Connect
            services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("MyConnection")));
            // CORS Policy
            services.AddCors();
            // CloudinarySettings
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
            // ASP Identity For JWT Token
            IdentityBuilder builder = services.AddIdentityCore<User>(
                Option =>
                {
                    Option.Password.RequireDigit = false;
                    Option.Password.RequiredLength = 4;
                    Option.Password.RequireLowercase = false;
                    Option.Password.RequireUppercase = false;
                    Option.Password.RequiredUniqueChars = 0;
                    Option.Password.RequireNonAlphanumeric = false;
                });
            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<DataContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();
            // Authorization For JWT Middelware
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(Options =>
           {
               Options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.
                   GetSection("AppSettings:Token").Value)),
                   ValidateIssuer = false,
                   ValidateAudience = false

               };
           });
            // AddAutoMapper
            services.AddAutoMapper();
           // Mapper.Reset();
           // Repository
            services.AddScoped<INewsRepo,NewsRepo>();
            services.AddScoped<IAuthRepo,AuthRepo>();
            //Trial Data
            services.AddTransient<TrialNews>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,TrialNews trialNews)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else //1- setting Production Mood
            {
                app.UseExceptionHandler(BuilderExtensions =>
                {
                    BuilderExtensions.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
                // app.UseHsts();
            }

            //app.UseHttpsRedirection();
            trialNews.NewTrialNews();
            app.UseRouting();
            // CORS Policy
            app.UseCors(z=>z.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            // ASP Identity
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
