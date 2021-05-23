using System;
using AutoMapper;
using WebApi.DTO;
using WebApi.Models;

namespace WebApi.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Notification, NotificationDTO>();
            CreateMap<TaskRequestDTO, Task>();
            CreateMap<Task, TaskEntryDTO>()
                .ForMember(x => x.User, opt => opt.MapFrom(y => y.ApplicationUserId));
            CreateMap<ApplicationUser, UserDTO>()
                .ForMember(x => x.Role, opt => opt.MapFrom(y => y.Role.RoleName))
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.ApplicationUserId));
        }
    }
}