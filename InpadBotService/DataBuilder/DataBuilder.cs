
namespace InpadBotService.DataBuilder;

public class DataBuilder
{

    public void UpdateData(UserContext context, string additionalParam, string message)
    {
        var data = context.data;
        data.Append(additionalParam);
        data.Append(': ');
        data.Append(message);
        data.Append('/n');
    }

    public string Build(UserContext context)
    {
        return new string(context.data);
    }

    public void RemoveAll(UserContext context)
    {
        context.data;
        
    }
}