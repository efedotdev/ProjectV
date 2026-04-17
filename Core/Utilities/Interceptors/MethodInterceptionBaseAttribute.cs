using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptors
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IAsyncInterceptor
    {
        public int Priority { get; set; }
        public virtual void InterceptSynchronous(IInvocation invocation) { }
        public virtual void InterceptAsynchronous(IInvocation invocation) { }
        public virtual void InterceptAsynchronous<TResult>(IInvocation invocation) { }
    }
}
