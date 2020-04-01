using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StoneCo.PagerDuty.Client.Settings;
using System;
using System.Net.Http;

namespace StoneCo.PagerDuty.Client.Extension
{
    public static class PagerDutyDependenceExtension
    {
        public static void AddPagerDuty(this IServiceCollection service, Action<PagerDutySettings> pagerDutySettingsConfiguration, Func<IServiceProvider, HttpMessageHandler> configureHttpMessageHandler)
        {
            service.Configure(pagerDutySettingsConfiguration);

            service.AddSingleton<IPagerDutyClient, PagerDutyClient>();
            service.AddHttpClient<IPagerDutyClient, PagerDutyClient>(ConfigureClient)
                .ConfigurePrimaryHttpMessageHandler(configureHttpMessageHandler);
        }

        private static void ConfigureClient(IServiceProvider serviceProvider, HttpClient httpClient)
        {
            var pagerDutySettings = serviceProvider.GetRequiredService<IOptionsSnapshot<PagerDutySettings>>().Value;

            httpClient.BaseAddress = new Uri(pagerDutySettings.BaseAddress);
            httpClient.DefaultRequestHeaders.Add("x-routing-key", pagerDutySettings.RoutingKey);
        }
    }
}
