using System.Text;


namespace InpadBotService.DataBuilder;

public class UserData
{
    public StringBuilder StrBuilder;

    public UserData()
    {
        StrBuilder = new StringBuilder();
    }

    public void Clear()
    {
        StrBuilder.Clear();
    }
}
