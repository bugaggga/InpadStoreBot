using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using static Telegram.Bot.TelegramBotClient;

namespace InpadBotService
{
	public class StateDistributor
	{
		private readonly Dictionary<string, IHandlerDistributor> _handlers;

		public StateDistributor(ITelegramBotClient _botCLient,
			IEnumerable<IReplyMarkupHandler> implementationsReply,
			IEnumerable<IHelpTypeAnswerHandler> implementationsHelpType)
		{
			_handlers = new Dictionary<string, IHandlerDistributor>
			{
				{ "Start", new HandlerDistributor<IReplyMarkupHandler>(implementationsReply) },
                {"WaitingHelpTypeCallback", new HandlerDistributor<IHelpTypeAnswerHandler>(implementationsHelpType) }
            };
		}

		public IState? GetHandler(UserContext context)
		{
			return _handlers.TryGetValue(context.CurrentState, out IHandlerDistributor? value) 
				? value.GetHandler(context.CurrentMessage): null;
		}
	}
}
