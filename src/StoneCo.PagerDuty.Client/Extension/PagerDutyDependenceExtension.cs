using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StoneCo.PagerDuty.Client.Settings;

namespace StoneCo.PagerDuty.Client.Extension
{
    public static class PagerDutyDependenceExtension
    {
        public static void AddPagerDuty(this IServiceCollection service, Func<IServiceProvider, HttpMessageHandler> configureHttpMessageHandler)
        {
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
