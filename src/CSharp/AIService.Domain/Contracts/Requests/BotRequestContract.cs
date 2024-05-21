
using System.Text;

namespace AIService.Contracts.Requests;

public class BotRequestContract
{
    public Guid ChatKey { get; set; }
    public string Message { get; set; }
}
