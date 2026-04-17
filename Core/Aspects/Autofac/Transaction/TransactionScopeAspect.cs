using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Aspects.Autofac.Transaction
{
    public class TransactionScopeAspect : MethodInterception
    {
        private TransactionScope CreateScope()
        {
            return new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted },
                TransactionScopeAsyncFlowOption.Enabled);
        }
        public override void InterceptSynchronous(IInvocation invocation)
        {
            using (var tx = CreateScope())
            {
                invocation.Proceed();
                tx.Complete();
            }
        }
        public override void InterceptAsynchronous(IInvocation invocation)
        {
            invocation.ReturnValue = InternalAsync(invocation);
        }
        private async Task InternalAsync(IInvocation invocation)
        {
            using (var tx = CreateScope())
            {
                var task = (Task)invocation.ReturnValue;
                await task;
                tx.Complete();
            }
        }
        public override void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            invocation.ReturnValue = InternalAsync<TResult>(invocation);
        }

        private async Task<TResult> InternalAsync<TResult>(IInvocation invocation)
        {
            using (var tx = CreateScope())
            {
                invocation.Proceed();
                var task = (Task<TResult>)invocation.ReturnValue;
                TResult result = await task;
                tx.Complete();
                return result;
            }
        }
    }
}
