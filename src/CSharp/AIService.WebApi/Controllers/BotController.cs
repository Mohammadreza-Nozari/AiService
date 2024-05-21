using AICore.WebApi.Controllers;
using AIService.Contracts.Common;
using AIService.Contracts.Requests;
using AIService.Contracts.Requests.Bots;
using AIService.Contracts.Responses;
using AIService.Database.Entities;
using AIService.Logics.Interfaces;
using EasyMicroservices.Database.Interfaces;
using EasyMicroservices.Mapper.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AIService.WebApi.Controllers;
public class BotController : BaseController<BotEntity, CreateBotRequestContract, UpdateBotRequestContract, BotContract>
{
    private readonly IBotService _botService;
    public BotController(IBotService botService, IMapperProvider mapper, IDatabase database)
        : base(mapper, database)
    {
        _botService = botService;
    }

    [HttpPost]
    public IAsyncEnumerable<string> SendStreamAsync(BotRequestContract botRequest)
    {
        return _botService.SendStreamAsync(botRequest);
    }

    [HttpPost]
    public Task<BotResponseContract> SendAsync(BotRequestContract botRequest)
    {
        return _botService.SendAsync(botRequest);
    }

    [HttpPost]
    public Task<ChatResponseContract> CreateChat(ChatRequestContract chatRequest)
    {
        return _botService.CreateChat(chatRequest);
    }
}
