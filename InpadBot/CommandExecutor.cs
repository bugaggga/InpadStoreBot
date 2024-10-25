using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace InpadBot
{

	public interface ITelegramUpdateListener
	{
		public Task GetUpdate(Update update);
	}

	public class CommandExecutor : ITelegramUpdateListener
	{
		private List<ICommand> commands;

		public CommandExecutor()
		{
			commands = new List<ICommand>()
			{
				new StartCommand(),
				new HelpCommand(),
				new SupportCommand(),
			};
		}

		public async Task GetUpdate(Update update)
		{
			Console.WriteLine("Get update");
			Message msg = update.Message;
			if (msg.Text == null)//такое бывает, во избежании ошибок делаем проверку
			{
				Console.WriteLine("Wrong update message");
				return;
			}

			foreach (var command in commands)
			{
				if (command.Name == msg.Text)
				{
					await command.Execute(update);
				}
			}
		}
	}
}
