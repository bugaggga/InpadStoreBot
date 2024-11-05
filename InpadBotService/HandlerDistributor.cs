using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InpadBotService
{
	public interface IHandlerDistributor
	{
		public IState? GetHandler(string buttonText);
	}

	internal class HandlerDistributor<T> : IHandlerDistributor where T : IState
	{
		private readonly Dictionary<string, T> _handlers;

		public HandlerDistributor(IEnumerable<T> handlers)
		{
			_handlers = handlers.ToDictionary(h => h.Message);
		}

		public IState? GetHandler(string buttonText)
		{
			return _handlers.TryGetValue(buttonText, out var handler) ? handler : null;
		}
	}
}
