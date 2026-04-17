using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        protected virtual async Task OnBeforeAsync(IInvocation invocation) { await Task.CompletedTask; }
        protected virtual async Task OnAfterAsync(IInvocation invocation) { await Task.CompletedTask; }
        protected virtual async Task OnExeptionAsync(IInvocation invocation, System.Exception e) { await Task.CompletedTask; }
        protected virtual async Task OnSuccessAsync(IInvocation invocation) { await Task.CompletedTask; }
        public override void InterceptSynchronous(IInvocation invocation)
        {

            var isSuccess = true;
            OnBeforeAsync(invocation).GetAwaiter().GetResult();
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnExeptionAsync(invocation, e).GetAwaiter().GetResult();
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccessAsync(invocation).GetAwaiter().GetResult();
                }
            }
            OnAfterAsync(invocation).GetAwaiter().GetResult();


        }
        public override void InterceptAsynchronous(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous(invocation);
        }

        private async Task InternalInterceptAsynchronous(IInvocation invocation)
        {
            var isSuccess = true;
            await OnBeforeAsync(invocation);
            try
            {
                invocation.Proceed();
                var task = (Task)invocation.ReturnValue;
                await task;
            }
            catch (Exception e)
            {
                isSuccess = false;
                await OnExeptionAsync(invocation, e);
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    await OnSuccessAsync(invocation);
                }
            }
            await OnAfterAsync(invocation);
        }
        public override void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous<TResult>(invocation);
        }
        private async Task<TResult> InternalInterceptAsynchronous<TResult>(IInvocation invocation)
        {
            var isSuccess = true;
            await OnBeforeAsync(invocation);
            try
            {
                invocation.Proceed();
                var task = (Task<TResult>)invocation.ReturnValue;
                TResult result = await task;
                return result;
            }
            catch (Exception e)
            {
                isSuccess = false;
                await OnExeptionAsync(invocation, e);
                throw;
            }
            await OnAfterAsync(invocation);
        }
    }
}
