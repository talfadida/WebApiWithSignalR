using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Guardium.Server.Model;

namespace Guardium.Server
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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IAppManager, AppManager>();
            services.AddSingleton<IUserManager, UserManager>();
            services.AddSingleton<IPageManager, PageManager>();
            services.AddSingleton<IAuthorizationManager, AuthorizationManager>();
            services.AddSingleton<INotifier, SignalRNotifier>();
            
            services.AddSignalR();

            services.AddSwaggerDocument(x => x.Title = "Guardium");

            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder.WithOrigins("http://localhost:34109", "http://localhost:62531")
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials()
                       .SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseAuthorization();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<PaintSignalRHub>("/paintSignalRHub").RequireCors("AllowAll");
            });
        }
    }
}
