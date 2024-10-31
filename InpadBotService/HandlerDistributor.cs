using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using static Telegram.Bot.TelegramBotClient;

namespace InpadBotService
{
	public class HandlerDistributor
	{
		private readonly Dictionary<string, IHandler> _handlers;
		//private readonly IReplyMarkupHandler replyMarkupHandler;

		public HandlerDistributor(ITelegramBotClient _botCLient, UserContext context, IEnumerable<IReplyMarkupHandler> implementations)
		{
			_handlers = new Dictionary<string, IHandler>
		{
			{ "Start", new ReplyButtonHandlerDistributor(implementations).GetHandler(context.CurrentMessage) },
				{"WaitingTypeCallback", new HelpTypeHandler(_botCLient) }
			};
		}

		public IHandler? GetHandler(string state)
		{
			return _handlers.ContainsKey(state) ? _handlers[state] : null;
		}
	}
}
