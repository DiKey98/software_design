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

        public static bool IsFirst = true;

        public void Intercept(IInvocation invocation)
        {
            var time1 = DateTime.Now;
            //Debug.WriteLine($"{invocation.InvocationTarget}.{invocation.Method.Name} вызван {DateToString(time1)}");

            invocation.Proceed();
            Debug.WriteLine(invocation.ReturnValue);

            var diff = DateTime.Now - time1;
            if (IsFirst)
            {
                IsFirst = false;
                return;
            }
            Debug.WriteLine($"Метод {invocation.InvocationTarget}.{invocation.Method.Name} выполнялся {diff.Milliseconds} мс.");
            IsFirst = true;
        }
    }
}