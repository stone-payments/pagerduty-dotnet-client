using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StoneCo.PagerDuty.Client.Settings;
using System;
using System.Net.Http;

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
            var pagerDutySettings = new PagerDutySettings();
            _configurationRoot.GetSection("PagerDutySettingsTest").Bind(pagerDutySettings);

            services.AddSingleton(pagerDutySettings);

            AddPagerDuty(services, pds => _configurationRoot.GetSection("PagerDutySettingsTest").Bind(pds)
                , hch => new HttpClientHandler());
        }

        public void Configure(IServiceProvider services, IApplicationBuilder app){}

        public static void AddPagerDuty(IServiceCollection service, Action<PagerDutySettings> pagerDutySettingsConfiguration, Func<IServiceProvider, HttpMessageHandler> configureHttpMessageHandler)
        {
            service.Configure(pagerDutySettingsConfiguration);

            service.AddSingleton<IPagerDutyClient, PagerDutyClient>();
            service.AddHttpClient<IPagerDutyClient, PagerDutyClient>(ConfigureClient)
                .ConfigurePrimaryHttpMessageHandler(configureHttpMessageHandler);

            static void ConfigureClient(IServiceProvider serviceProvider, HttpClient httpClient)
            {
                var pagerDutySettings = serviceProvider.GetRequiredService<IOptionsSnapshot<PagerDutySettings>>().Value;

                ConfigureHttpClient(httpClient, pagerDutySettings);
            }

            static void ConfigureHttpClient(HttpClient httpClient, PagerDutySettings pagerDutySettings)
            {
                httpClient.BaseAddress = new Uri(pagerDutySettings.BaseAddress);
                httpClient.DefaultRequestHeaders.Add("x-routing-key", pagerDutySettings.RoutingKey);
            }
        }
    }
}
