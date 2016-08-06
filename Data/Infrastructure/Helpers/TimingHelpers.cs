using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Data.Infrastructure.Helpers
{
    public static class TimingHelper
    {
        public static async Task ExecuteWithTimingAsync(Func<Task> executeFunction, Action<TimeSpan> logElapsedFunc,
            Action<TimeSpan, Exception> logError = null)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                await executeFunction.Invoke();
            }
            catch (Exception ex)
            {
                sw.Stop();
                logError?.Invoke(sw.Elapsed, ex);
                throw;
            }
            sw.Stop();
            logElapsedFunc.Invoke(sw.Elapsed);
        }

        public static async Task<T> ExecuteWithTimingAsync<T>(Func<Task<T>> executeFunction,
            Action<TimeSpan> logElapsedFunc, Action<TimeSpan, Exception> logError = null)
        {
            var sw = Stopwatch.StartNew();
            T result;
            try
            {
                result = await executeFunction.Invoke();
            }
            catch (Exception ex)
            {
                sw.Stop();
                logError?.Invoke(sw.Elapsed, ex);
                throw;
            }
            sw.Stop();
            logElapsedFunc.Invoke(sw.Elapsed);
            return result;
        }
    }
}