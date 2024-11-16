
namespace InpadBotService.DataBuilder;

public class DataBuilder
{
    public void UpdateData(UserContext context, string additionalParam, string message)
    {
        var data = context.data.StrBuilder;
        data.Append(additionalParam);
        data.Append(": ");
        data.Append(message);
        data.Append("/n");
    }

    public string Build(UserContext context)
    {
        return context.data.StrBuilder.ToString();
    }

    public void RemoveAll(UserContext context)
    {
        context.data.StrBuilder.Clear();
    }
}