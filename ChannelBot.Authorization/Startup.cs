using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChannelBot.BLL.Abstractions;
using ChannelBot.BLL.Options;
using ChannelBot.BLL.Services;
using ChannelBot.DAL.Contexts;
using ChannelBot.DAL.Models;
using ChannelBot.Utilities.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace ChannelBot.Authorization
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
            services.AddControllers();
            //string datebaseconnectionstring = "Host=185.87.48.116;Database=postgres;Username=postgres;Password=123123AAA";
            string datebaseconnectionstring = Environment.GetEnvironmentVariable("datebaseconnectionstring");
            services.AddTransient(x =>
            {
                return new MainContext(datebaseconnectionstring);
            });


            MainContext context = new MainContext(datebaseconnectionstring);

            JwtOption jwtOption = context.JwtOption.FirstOrDefault();

            services.AddSingleton(new AuthOptions(jwtOption.Key, jwtOption.Issuer, jwtOption.Audience));

            services.AddTransient<IAuthService, AuthService>();


            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureCustomExceptionMiddleware();

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseCors(x => x.AllowAnyOrigin());

            app.UseCors(x => x.AllowAnyHeader());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
