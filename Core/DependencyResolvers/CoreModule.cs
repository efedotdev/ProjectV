using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DependencyResolvers
{
    public class CoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            // _memorycache için
            //serviceCollection.AddMemoryCache();
            //serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
        }
    }
}
