using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Cahing;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Cahing
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        private string BuildKey(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            return $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
        }
        public override void InterceptSynchronous(IInvocation invocation)
        {
            var key = BuildKey(invocation);
            if (_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }
            invocation.Proceed();
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }

        public override void InterceptAsynchronous(IInvocation invocation)
        {
            throw new InvalidOperationException("CacheAspect error");
        }

        public override void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            invocation.ReturnValue = InternalCacheAsync<TResult>(invocation);
        }

        private async Task<TResult> InternalCacheAsync<TResult>(IInvocation invocation)
        {
            var key = BuildKey(invocation);
            if (_cacheManager.IsAdd(key))
            {
                return _cacheManager.Get<TResult>(key);
            }
            invocation.Proceed();
            var task = (Task<TResult>)invocation.ReturnValue;
            
            
            TResult result = await task; 
            _cacheManager.Add(key, result, _duration);
            return result;
        }

    }
}
