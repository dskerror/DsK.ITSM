using AutoMapper;
using DsK.ITSM.Security.EntityFramework.Models;

namespace DsK.ITSM.Security.Shared;
public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        //ActionDtos
        CreateMap<AuthenticationProviderCreateDto, AuthenticationProvider>().ReverseMap();
        CreateMap<AuthenticationProviderUpdateDto, AuthenticationProvider>().ReverseMap();
        CreateMap<PermissionCreateDto, Permission>().ReverseMap();
        CreateMap<PermissionUpdateDto, Permission>().ReverseMap();
        CreateMap<RequestCreateDto, RequestDto>().ReverseMap();
        CreateMap<RolePermissionChangeDto, RolePermission>().ReverseMap();        
        CreateMap<RoleCreateDto, Role>().ReverseMap();
        CreateMap<RoleUpdateDto, Role>().ReverseMap();
        CreateMap<UserAuthenticationProviderCreateDto, UserAuthenticationProvider>().ReverseMap();
        CreateMap<UserAuthenticationProviderUpdateDto, UserAuthenticationProvider>().ReverseMap();
        CreateMap<UserPermissionChangeDto, UserPermission>().ReverseMap();
        CreateMap<UserCreateDto, User>().ReverseMap();
        CreateMap<UserCreateDto, UserRegisterDto>().ReverseMap();
        CreateMap<UserRoleChangeDto, UserRole>().ReverseMap();

        //.ForMember(dest => dest.ConfirmEmail,
        //           opt => opt.MapFrom(src => src.Email));
        //CreateMap<User, UserCreateDto>();

        CreateMap<User, User>().ReverseMap().ForMember(dest => dest.Username, act => act.Ignore());

        //ModelDtos
        CreateMap<AuthenticationProviderDto, AuthenticationProvider>().ReverseMap();
        CreateMap<CategoryDto, Category>().ReverseMap();
        CreateMap<ItsystemDto, Itsystem>().ReverseMap();
        CreateMap<PermissionDto, Permission>().ReverseMap();
        CreateMap<PriorityDto, Priority>().ReverseMap();
        CreateMap<RequestDto, Request>().ReverseMap();
        CreateMap<RequestAssignedHistoryDto, RequestAssignedHistory>().ReverseMap();
        CreateMap<RequestMessageHistoryDto, RequestMessageHistory>().ReverseMap();
        CreateMap<RequestStatusHistoryDto, RequestStatusHistory>().ReverseMap();
        CreateMap<RequestTypeDto, RequestType>().ReverseMap();
        CreateMap<RolePermissionGridDto, Permission>().ReverseMap();
        CreateMap<RoleDto, Role>().ReverseMap();
        CreateMap<RolePermissionDto, RolePermission>().ReverseMap();
        CreateMap<UserAuthenticationProviderDto, UserAuthenticationProvider>().ReverseMap();
        CreateMap<UserDto, User>().ReverseMap();        
        CreateMap<UserLogDto, UserLog>().ReverseMap();
        CreateMap<UserPasswordDto, UserPassword>().ReverseMap();
        CreateMap<UserPermissionDto, UserPermission>().ReverseMap();
        CreateMap<UserRoleDto, UserRole>().ReverseMap();
        CreateMap<UserRoleGridDto, Role>().ReverseMap();
        CreateMap<UserPermissionGridDto, Permission>().ReverseMap();
        CreateMap<UserTokenDto, UserToken>().ReverseMap();
    }  
}
