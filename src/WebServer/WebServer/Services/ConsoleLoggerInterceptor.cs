using System;
using System.Globalization;
using Castle.DynamicProxy;

namespace WebServer.Services
{
    public class ConsoleLoggerInterceptor : IInterceptor
    {
        private static string DateToString(DateTime date)
        {
            return date.ToString("g", CultureInfo.CurrentCulture);
        }

        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine($"{invocation.InvocationTarget}.{invocation.Method.Name} вызван {DateToString(DateTime.Now)}");
            invocation.Proceed();
        }
    }
}