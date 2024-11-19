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
		public async Task<int> HandleAsync(
			TelegramRequest request,
			CancellationToken cancellationToken,
			UserContext context)
		{
			var handler = new HandlerDistributor<T>(_handlers).GetHandler(context.CurrentMessage);
			if (handler == null) 
			{
				var againMesId = context.PreviousMessageId;
				context.SaveBotMessageId(0);
				return againMesId;
			}
			return await handler!.HandleAsync(request, cancellationToken, context);
		}
	}
}
