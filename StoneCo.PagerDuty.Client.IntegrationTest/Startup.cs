using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoneCo.PagerDuty.Client.Settings;
using System;
using System.Net.Http;
using StoneCo.PagerDuty.Client.Extension;

namespace StoneCo.PagerDuty.Client.IntegrationTest
{
    public class Startup
    {
        private IConfigurationRoot _configurationRoot { get; }

        public Startup(IHostingEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            _configurationRoot = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPagerDuty(pds => _configurationRoot.GetSection("PagerDutySettingsTest").Bind(pds)
                , hch => new HttpClientHandler());
        }

        public void Configure(IServiceProvider services, IApplicationBuilder app){}
    }
}
