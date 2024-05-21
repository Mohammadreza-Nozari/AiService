using AICore.WebApi.Controllers;
using AIService.Contracts.Common;
using AIService.Contracts.Requests.BotDatas;
using AIService.Database.Entities;
using EasyMicroservices.Database.Interfaces;
using EasyMicroservices.Mapper.Interfaces;

namespace AIService.WebApi.Controllers;

public class BotDataController : BaseController<BotDataEntity, CreateBotDataRequestContract, UpdateBotDataRequestContract, BotDataContract>
{
    public BotDataController(IMapperProvider mapper, IDatabase database)
        : base(mapper, database)
    {
    }
}
