using AutoMapper;
using Model.Dto.User;
using Model.Entitys;

namespace WebApi.Config;

public class AutoMapperConfigs : Profile
{
    public AutoMapperConfigs()
    {
        //用户
        CreateMap<Users, UserRes>();
        CreateMap<UserAdd, Users>();
        CreateMap<UserEdit, Users>();
    }
}