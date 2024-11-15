using InpadBotService.HelpButton;
using InpadBotService.SupportButton;
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
				typeof(StartMessageHandler),
				typeof(SupportMessageHandler),
				typeof(QuestionMessageHandler)]);

			builder.Services.AddMultipleImplementations<IHelpTypeState>(
				[typeof(QuestionAboutPluginHandler),
				typeof(ReportErrorHandler),
				typeof(HelpInstallationHandler)]);

			builder.Services.AddMultipleImplementations<IHelpQuestionCategoryPlugin>(
				[typeof(HQCategoryConceptState),
				typeof(HQCategoryCommonState),
				typeof(HQCategoryConstructiveState),
				typeof(HQCategoryOBAndBKState),
				typeof(HQCategoryRengaState),
				typeof(HQCategoryArchitectureState),
				typeof(HQCategoryBoxesAndHolesState)]);

			builder.Services.AddMultipleImplementations<IHelpReportCategoryPlugin>(
				[typeof(HRCategoryConceptState),
				typeof(HRCategoryCommonState),
				typeof(HRCategoryConstructiveState),
				typeof(HRCategoryOBAndBKState),
				typeof(HRCategoryRengaState),
				typeof(HRCategoryArchitectureState),
				typeof(HRCategoryBoxesAndHolesState)]);

			builder.Services.AddMultipleImplementations<ISupportCategoryPluginState>(
                [typeof(SCategoryConceptState),
                typeof(SCategoryArchitectureState),
                typeof(SCategoryConstructiveState),
                typeof(SCategoryOBAndBKState),
                typeof(SCategoryCommonState),
                typeof(SCategoryBoxesAndHolesState)]);

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