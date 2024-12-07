
namespace InpadBotService.DatasFuncs;

public static class DataBuilder
{
    //public static void UpdateData(UserContext context, string additionalParam)
    //{
    //    var data = context.data.StrBuilder;
    //    var message = context.CurrentMessage;
    //    data.Append(additionalParam);
    //    data.Append(": ");
    //    data.Append(message);
    //    data.Append("/n");
    //}

    public static void Build(UserContext context)
    {
        var dict = context.data.Data;

        //return context.data.StrBuilder.ToString();
        // отправка инпаду

        Clear(context);
    }

    public static void Clear(UserContext context)
    {
        context.data.Clear();
    }
}