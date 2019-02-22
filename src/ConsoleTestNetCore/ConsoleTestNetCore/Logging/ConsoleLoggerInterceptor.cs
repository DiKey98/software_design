using System;
using System.Diagnostics;
using System.Globalization;
using Castle.DynamicProxy;

namespace ConsoleTestNetCore.Logging
{
    public class ConsoleLoggerInterceptor : IInterceptor
    {
        private static string DateToString(DateTime date)
        {
            return date.ToString("g", CultureInfo.CurrentCulture);
        }

        public void Intercept(IInvocation invocation)
        {
            Debug.WriteLine($"{invocation.InvocationTarget}.{invocation.Method.Name} вызван {DateToString(DateTime.Now)}");
            invocation.Proceed();
        }
    }
}