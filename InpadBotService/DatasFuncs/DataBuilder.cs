
namespace InpadBotService.DatasFuncs;

public static class DataBuilder
{
    public static void UpdateData(UserContext context, string additionalParam)
    {
        var data = context.data.StrBuilder;
        var message = context.CurrentMessage;
        data.Append(additionalParam);
        data.Append(": ");
        data.Append(message);
        data.Append("/n");
    }

    public static string Build(UserContext context)
    {
        return context.data.StrBuilder.ToString();
    }

    public static void Clear(UserContext context)
    {
        context.data.StrBuilder.Clear();
    }
}