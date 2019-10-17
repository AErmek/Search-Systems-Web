using AutoMapper;
using Searcher.BLL.DTO;
using Searcher.BLL.Enums;
using Searcher.DAL.Entities;

namespace Searcher.BLL.Settings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SearchResult, SearchResultDto>()
                .ForMember(
                dest => dest.BrowserType,
                opt => opt.MapFrom(src => (SearchSystemType)src.BrowserType))
                .ReverseMap()
                .ForMember(
                dest => dest.BrowserType,
                opt => opt.MapFrom(src => src.BrowserType));

        }
    }
}
