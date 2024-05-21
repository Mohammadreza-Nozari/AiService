using System.Text;

namespace AIService.Contracts.Requests;
public class ChatRequestContract
{
    public string BotName { get; set; }
    public long? TenantId { get; set; }

    public bool HasValidBot()
    {
        return BotName.HasValue() && TenantId.HasValue;
    }
}
