using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nopguru.Nop.Plugins.HomePage.Popup.Filter;
using Nopguru.Nop.Plugins.HomePage.Popup.Services;

namespace Nopguru.Nop.Plugins.HomePage.Popup.Infrastructure
{
    public class NopStartup : INopStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // If using Kestrel:
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // If using IIS:
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            //Register filter
            services.Configure<MvcOptions>(congig =>
            {
                congig.Filters.Add<HomePagePopupFilterAttribute>();
            });


            //Service 
            services.AddScoped<IHomePagePopupService, HomePagePopupService>();
            services.AddScoped<ICourseManagementService, CourseManagementService>();
        }

        public void Configure(IApplicationBuilder application)
        {
        }

        public int Order => 3000;
    }
}
