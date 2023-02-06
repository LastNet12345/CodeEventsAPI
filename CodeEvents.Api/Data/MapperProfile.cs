using AutoMapper;
using CodeEvents.Api.Core.DTOs;
using CodeEvents.Api.Core.Entities;

namespace CodeEvents.Api.Data
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CodeEvent, CodeEventDto>().ReverseMap();
            CreateMap<Lecture, LectureDto>().ReverseMap();
            CreateMap<CodeEvent, CreateCodeEventDto>().ReverseMap();
            CreateMap<CreateCodeEventDto, CodeEventDto>().ReverseMap();
            CreateMap<CreateLectureDto, Lecture>().ReverseMap();
        }
    }
}
