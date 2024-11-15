using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace InpadBotService
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = Host.CreateApplicationBuilder(args);
			builder.Services.AddHostedService<TgBotBackgroundService>();
			builder.Services.Configure<BotOptions>(builder.Configuration.GetSection("BotOptions"));
			builder.Services.AddSingleton<UserContextManager>();
			builder.Services.AddSingleton<ITelegramBotClient, TelegramBotClient>(serviceProvider =>
			{
				var token = serviceProvider.GetRequiredService<IOptions<BotOptions>>().Value.Token;
				return new TelegramBotClient(token);
			});

			builder.Services.AddMultipleImplementations<IReplyMarkupHandler>(
				[typeof(HelpMessageHandler),
				typeof(SupportMessageHandler),
				typeof(QuestionMessageHandler)]);

			builder.Services.AddMultipleImplementations<IHelpTypeAnswerHandler>(
				[typeof(HelpTypeHandler),
				typeof(HelpDownloadHandler)]);

			builder.Services.AddMultipleImplementations<IPlugin>(
				[typeof(PluginConcept),
				typeof(PluginCommon),
				typeof(PluginConstructive),
				typeof(PluginOBAndBK),
				typeof(PluginRenga),
				typeof(PluginArchitecture),
				typeof(PluginBoxesAndHoles)]);

			var host = builder.Build();
			host.Run();
		}
	}

	public static class ServiceCollectionExtensions
	{
		public static void AddMultipleImplementations<TInterface>(this IServiceCollection services, params Type[] implementations)
		{
			foreach (var implementation in implementations)
			{
				services.AddTransient(typeof(TInterface), implementation);
			}
		}
	}
}