using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InpadBotService
{
	internal class ReplyButtonHandlerDistributor
	{
		private readonly Dictionary<string, IReplyMarkupHandler> _handlers;

		public ReplyButtonHandlerDistributor(IEnumerable<IReplyMarkupHandler> handlers)
		{
			_handlers = handlers.ToDictionary(h => h.Text);
		}

		public IReplyMarkupHandler? GetHandler(string buttonText)
		{
			return _handlers.TryGetValue(buttonText, out var handler) ? handler : null;
		}
	}
}
