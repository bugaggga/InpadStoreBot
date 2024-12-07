using System.Text;


namespace InpadBotService.DatasFuncs;

public class UserData
{
    public Dictionary<string, object> Data { get; }
    public StringBuilder StrBuilder;

    public UserData()
    {
        Data = new Dictionary<string, object>();
        StrBuilder = new StringBuilder();
    }

    public void Clear()
    {
        Data.Clear();
        StrBuilder.Clear();
    }

    public void AddPair(string key, object value)
    {
        Data[key] = value;
    }
}
