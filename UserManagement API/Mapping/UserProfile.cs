using AutoMapper;
using UserManagement_API.DTOs.Request;
using UserManagement_API.DTOs.Response;
using UserManagement_API.Models;

namespace UserManagement_API.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserRequestDto, User>().ReverseMap();
            CreateMap<UpdateUserRequestDto, User>().ReverseMap();

            CreateMap<User, UserResponseDto>().ReverseMap();
        }
    }
}
