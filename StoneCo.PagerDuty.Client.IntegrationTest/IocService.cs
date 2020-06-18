using System;

namespace StoneCo.PagerDuty.Client.IntegrationTest
{
    public class IocService
    {
        private readonly IServiceProvider _serviceProvider;

        public IocService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T Resolve<T>()
        {
            var type = typeof(T);

            var returnType = _serviceProvider.GetService(type);

            return (T)returnType;
        }
    }
}
