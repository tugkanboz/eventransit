using AutoMapper;
using EvenTransit.Core.Dto.Service;
using EvenTransit.Core.Entities;

namespace EvenTransit.Service.Mappers
{
    public class EventMapper : Profile
    {
        public EventMapper()
        {
            CreateMap<Event, EventDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src._id))
                .ReverseMap();
            
            CreateMap<Core.Entities.Service, ServiceDto>();
        }
    }
}