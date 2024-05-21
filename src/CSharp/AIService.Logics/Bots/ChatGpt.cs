using AIService.Contracts.Requests;
using AIService.Contracts.Responses;
using AIService.Database.Entities;
using AIService.Logics.Interfaces;
using EasyMicroservices.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using System;
using System.Collections.Concurrent;

namespace AIService.Bots;
public class ChatGpt : IBotService
{
    static ConcurrentDictionary<Guid, Conversation> Chats = new ConcurrentDictionary<Guid, Conversation>();
    //https://github.com/OkGoDoIt/OpenAI-API-dotnet
    private readonly OpenAIAPI _client;
    private readonly IEasyReadableQueryableAsync<BotEntity> _botRepository;
    public ChatGpt(IConfiguration configuration, IDatabase database)
    {
        _botRepository = database.GetReadableOf<BotEntity>();
        _client = new OpenAIAPI(configuration.GetSection("OpenAICredentials").GetSection("ApiKey").Value);
    }

    public async Task<ChatResponseContract> CreateChat(ChatRequestContract chatRequest)
    {
        var key = Guid.NewGuid();
        Conversation chat = default;
        if (chatRequest.HasValidBot())
        {
            var bot = await _botRepository
                .Include(x => x.BotDatas)
                .FirstOrDefaultAsync(x => x.Name == chatRequest.BotName && x.TenantId == chatRequest.TenantId);
            chat = _client.Chat.CreateConversation();
            chat.Model = new Model(bot.Model);
            chat.RequestParameters.Temperature = 0;
            foreach (var item in bot.BotDatas)
            {
                chat.AppendSystemMessage(item.Content);
            }
        }
        else
        {
            chat.Model = Model.GPT4;
            chat.RequestParameters.Temperature = 0;
        }
        Chats.TryAdd(key, chat);

        return new ChatResponseContract()
        {
            Session = key
        };
    }

    public async Task<BotResponseContract> SendAsync(BotRequestContract botRequest)
    {
        if (!Chats.TryGetValue(botRequest.ChatKey, out Conversation conversation))
            throw new Exception($"Session {botRequest.ChatKey} not found!");
        conversation.AppendUserInput(botRequest.Message);
        var customResult = await conversation.GetResponseFromChatbotAsync();
        return new BotResponseContract()
        {
            Message = customResult
        };
    }

    public IAsyncEnumerable<string> SendStreamAsync(BotRequestContract botRequest)
    {
        throw new NotImplementedException();
        //await foreach (string chunk in _client.StreamChatCompletions(new UserMessage(botRequest.Message), maxTokens: 80))
        //{
        //    yield return chunk;
        //}
    }
}