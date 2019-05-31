using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Aquiris.Core.Extensions
{
    public static class AsyncExtensions
    {
        public static TaskAwaiter AsyncTask(this TimeSpan timeDelay)
        {
            return Task.Delay(timeDelay).GetAwaiter();
        }
    }
}