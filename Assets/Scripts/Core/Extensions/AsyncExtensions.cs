using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DroidDigital.Core.Extensions
{
    public static class AsyncExtensions
    {
        public static TaskAwaiter AsyncTask(this TimeSpan timeDelay)
        {
            return Task.Delay(timeDelay).GetAwaiter();
        }
    }
}