using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InpadBotService
{
	public interface IDistributorState<T> : IState where T : IState;

	public class DistributorState<T> : IDistributorState<T> where T : IState
	{
		private readonly IEnumerable<T> _handlers;
		public string Message { get; } = "";
		public DistributorState(IEnumerable<T> handlers)
		{
			_handlers = handlers;
		}
		public async Task HandleAsync(
			TelegramRequest request,
			CancellationToken cancellationToken,
			UserContext context)
		{
			await new HandlerDistributor<T>(_handlers).GetHandler(context.CurrentMessage)!
				.HandleAsync(request, cancellationToken, context);
		}
	}
}
