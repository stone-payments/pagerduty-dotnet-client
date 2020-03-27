using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoneCo.PagerDuty.Client.IntegrationTest.Settings;

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
            services.Configure<PagerDutySettingsInTest>(_configurationRoot.GetSection("PagerDutySettingsInTest"));
        }

        public void Configure(IServiceProvider services, IApplicationBuilder app){}
    }
}
