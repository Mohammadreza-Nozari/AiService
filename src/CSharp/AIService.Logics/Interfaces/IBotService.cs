using AIService.Contracts.Requests;
using AIService.Contracts.Responses;

namespace AIService.Logics.Interfaces;
public interface IBotService
{
    Task<BotResponseContract> SendAsync(BotRequestContract botRequest);
    IAsyncEnumerable<string> SendStreamAsync(BotRequestContract botRequest);
    Task<ChatResponseContract> CreateChat(ChatRequestContract chatRequest);
}
