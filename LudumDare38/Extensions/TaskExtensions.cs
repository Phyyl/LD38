using System.Threading;
using System.Threading.Tasks;

namespace LudumDare38
{
	public static class TaskExtensions
	{
		public static async Task<TResult> ExecuteWithTimeout<TResult>(this Task<TResult> task, int timeout, CancellationTokenSource cancellationTokenSource = null, TResult defaultResult = default(TResult))
		{
			Task completedTask = await Task.WhenAny(task, Task.Delay(timeout, cancellationTokenSource?.Token ?? CancellationToken.None));

			cancellationTokenSource?.Cancel();

			if (completedTask == task)
			{
				return await task;
			}
			else
			{
				return defaultResult;
			}
		}
	}
}
