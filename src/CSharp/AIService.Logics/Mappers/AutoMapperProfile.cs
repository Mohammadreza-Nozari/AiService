using AIService.Contracts.Common;
using AIService.Contracts.Requests.BotDatas;
using AIService.Contracts.Requests.Bots;
using AIService.Database.Entities;
using AutoMapper;

namespace AIService.Mappers;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<BotEntity, BotContract>();
        CreateMap<CreateBotRequestContract, BotEntity>();
        CreateMap<UpdateBotRequestContract, BotEntity>();

        CreateMap<BotDataEntity, BotDataContract>();
        CreateMap<CreateBotDataRequestContract, BotDataEntity>();
        CreateMap<UpdateBotDataRequestContract, BotDataEntity>();
    }
}
