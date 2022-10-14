using AutoMapper;
using ContactListWebService.Entities;
using ContactListWebService.Models;

namespace ContactListWebService.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, UsersDto>();
            CreateMap<User, ExtUserDto>();
            CreateMap<UserForCreationDto, User>();
            CreateMap<User, UserForCreationDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryForCreationDto, Category>();
            CreateMap<Category, CategoryForCreationDto>();
            

        }
    }
}
