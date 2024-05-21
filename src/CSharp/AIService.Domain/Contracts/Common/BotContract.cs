﻿namespace AIService.Contracts.Common;
public class BotContract
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    /// <summary>
    /// https://platform.openai.com/docs/models
    /// </summary>
    public string Model { get; set; }
    public long TenantId { get; set; }
}