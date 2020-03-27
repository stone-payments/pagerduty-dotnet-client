using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;

namespace StoneCo.PagerDuty.Client.IntegrationTest
{
    public class IntegratedTestBase : IDisposable
    {
        private readonly IocService _iocServiceLocator;

        public TestServer TestServer { get; set; }

        public IntegratedTestBase()
        {
            var builder = new WebHostBuilder()
                .UseStartup<Startup>();

            TestServer = new TestServer(builder);

            _iocServiceLocator = new IocService(TestServer.Host.Services);
        }

        public T Resolve<T>()
        {
            return _iocServiceLocator.Resolve<T>();
        }

        public void Dispose()
        {
            TestServer.Dispose();
        }
    }
}
