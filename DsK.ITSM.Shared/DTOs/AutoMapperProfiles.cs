using AutoMapper;
using DsK.ITSM.EntityFramework.Models;

namespace DsK.ITSM.Shared.DTOs;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();

        CreateMap<Itsystem, ItsystemDto>().ReverseMap();

        CreateMap<Priority, PriorityDto>().ReverseMap();

        CreateMap<RequestAssignedHistory, RequestAssignedHistoryDto>().ReverseMap();

        CreateMap<RequestMessageHistory, RequestMessageHistoryDto>().ReverseMap();

        CreateMap<Request, RequestDto>().ReverseMap();
        CreateMap<RequestDto, RequestCreateDto>().ReverseMap();

        CreateMap<RequestStatusHistory, RequestStatusHistoryDto>().ReverseMap();
        
        CreateMap<Status, StatusDto>().ReverseMap();
        
        CreateMap<User, UserDto>().ReverseMap();
    }
}