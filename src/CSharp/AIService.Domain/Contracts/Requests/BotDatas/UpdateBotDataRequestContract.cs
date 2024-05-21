﻿namespace AIService.Contracts.Requests.BotDatas;
public class UpdateBotDataRequestContract
{
    public long Id { get; set; }
    public string Name { get; set; }
    /// <summary>
    /// https://platform.openai.com/docs/guides/text-generation/chat-completions-api
    /// </summary>
    public string Role { get; set; }
    public string Content { get; set; }

    public long BotId { get; set; }
}
