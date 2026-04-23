using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Performance
{
    public class PerformanceAspect : MethodInterception
    {
        private int _interval;

        public PerformanceAspect(int interval)
        {
            _interval = interval;
        }

        // Senkron ve Asenkron metodların her birini baştan sona sarmalıyoruz
        public override void InterceptSynchronous(IInvocation invocation)
        {
            var stopwatch = Stopwatch.StartNew(); // Sadece bu isteğe özel
            try { invocation.Proceed(); }
            finally { LogPerformance(invocation, stopwatch); }
        }

        public override void InterceptAsynchronous(IInvocation invocation)
        {
            invocation.ReturnValue = InternalAsync(invocation);
        }

        private async Task InternalAsync(IInvocation invocation)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                invocation.Proceed();
                await (Task)invocation.ReturnValue;
            }
            finally { LogPerformance(invocation, stopwatch); }
        }

        public override void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            invocation.ReturnValue = InternalAsync<TResult>(invocation);
        }

        private async Task<TResult> InternalAsync<TResult>(IInvocation invocation)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                invocation.Proceed();
                TResult result = await (Task<TResult>)invocation.ReturnValue;
                return result;
            }
            finally { LogPerformance(invocation, stopwatch); }
        }

        private void LogPerformance(IInvocation invocation, Stopwatch stopwatch)
        {
            stopwatch.Stop();
            if (stopwatch.Elapsed.TotalSeconds > _interval)
            {
                Console.WriteLine($"Performance : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}-->{stopwatch.Elapsed.TotalSeconds}");
            }
        }
    }
}
