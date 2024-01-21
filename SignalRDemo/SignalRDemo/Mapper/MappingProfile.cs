using AutoMapper;
using SignalRDemo.Models;
using SignalRDemo.ModelView;

namespace SignalRDemo.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NotificationSettings, NotificationSettingsView>().ReverseMap();
            CreateMap<Recipients, RecipientView>().ReverseMap();
            CreateMap<Senders, SenderView>().ReverseMap();
            CreateMap<Notifications, NotificationView>().ReverseMap();

            CreateMap<UserGroups, UserGroupView>().ReverseMap();

        }

    }
}
