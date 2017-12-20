using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TravelPlanner.Presentation.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TravelPlanner.Presentation.Model.Entities;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using TravelPlanner.Presentation.Services;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TravelPlanner.Presentation.Model.Repositories.IRepositories;
using TravelPlanner.Presentation.Model.Repositories;

namespace TravelPlanner.Presentation
{
    public class Startup
    {
        private readonly IConfiguration _conf;

        public Startup(IConfiguration conf)
        {
            _conf = conf;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddIdentity<TravelUser, IdentityRole>(ConfigureIdentity)
                .AddEntityFrameworkStores<TravelPlannerContext>().AddDefaultTokenProviders();
            services.AddAuthentication()
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = _conf["Tokens:Issuer"],
                        ValidAudience = _conf["Tokens:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf["Tokens:Key"]))
                    };
                }).AddGoogle(googleOptions =>
                {
                    googleOptions.Scope.Add("profile");
                    googleOptions.Scope.Add("email");
                    googleOptions.ClientId = _conf["SocialMediaAuthentication:Google:ClientId"];
                    googleOptions.ClientSecret = _conf["SocialMediaAuthentication:Google:ClientSecret"];
                }).AddFacebook(facebookOptions =>
                {
                    facebookOptions.Scope.Add("public_profile");
                    facebookOptions.Scope.Add("email");
                    facebookOptions.AppId = _conf["SocialMediaAuthentication:Facebook:AppId"];
                    facebookOptions.AppSecret = _conf["SocialMediaAuthentication:Facebook:AppSecret"];
                });

            services.AddDbContext<TravelPlannerContext>(conf =>
            {
                conf.UseSqlServer(_conf.GetConnectionString("TravelPlannerConnectionString"));
            });

            services.AddTransient<DBSeeder>();

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ITripRepository, TripRepository>();
            services.AddTransient<ITravelUserRepository, TravelUserRepository>();

            services.AddAutoMapper();

            services.AddMvc()
                .AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.Configure<AuthMessageSenderOptions>(options =>
            {
                options.SendGridUser = _conf["SendGrid:SendGridUser"];
                options.SendGridKey = _conf["SendGrid:SendGridKey"];
            });

        }


        private void ConfigureIdentity(IdentityOptions conf)
        {
            conf.User.RequireUniqueEmail = true;
            conf.Password.RequiredLength = 6;
            conf.SignIn.RequireConfirmedEmail = true;
            conf.Password.RequireDigit = false;
            conf.Password.RequiredUniqueChars = 0;
            conf.Password.RequireLowercase = false;
            conf.Password.RequireNonAlphanumeric = false;
            conf.Password.RequireUppercase = false;
            conf.Lockout.AllowedForNewUsers = true;
            conf.Lockout.DefaultLockoutTimeSpan = new TimeSpan(400,0,0,0,0);
            conf.Lockout.MaxFailedAccessAttempts = 3;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseCors(
                builder => builder.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .AllowAnyOrigin());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    scope.ServiceProvider.GetService<DBSeeder>().Seed().Wait();
                }
            }
            app.UseAuthentication();

            app.UseMvc();

        }

    }
}
