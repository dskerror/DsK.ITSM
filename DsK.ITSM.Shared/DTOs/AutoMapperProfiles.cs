using AutoMapper;
using DsK.ITSM.Shared.DTOs.Requests;
using DsK.ITSM.EntityFramework.Models;

namespace DsK.ITSM.Shared.DTOs;

public class AutoMapperProfiles : Profile
{

    public AutoMapperProfiles()
    {
        CreateMap<Request, RequestDto>().ReverseMap();
        CreateMap<RequestDto, RequestCreateDto>().ReverseMap();
    }
}
