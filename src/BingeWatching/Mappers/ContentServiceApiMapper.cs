using AutoMapper;
using BingeWatching.API.ContentServiceApiClient;
using BingeWatching.API.ViewModels;
using BingeWatching.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BingeWatching.API.Mappers
{
    public class ContentServiceApiMapper : Profile
    {
        public static IMapper Instance = new Lazy<IMapper>(() =>
        {
            return new MapperConfiguration(cfg =>
               {
                   cfg.CreateMap<ContentServiceApiResponse, Content>()
                .ForMember(m => m.ReleasedOn, m => m.MapFrom(src => src.Released_On))
                .ForMember(m => m.ImdbRating, m => m.MapFrom(src => src.Imdb_Rating)).ReverseMap();
                   cfg.CreateMap<ContentServiceApiResponse, ContentViewModel>()
                  .ForMember(m => m.ReleasedOn, m => m.MapFrom(src => src.Released_On))
                  .ForMember(m => m.ImdbRating, m => m.MapFrom(src => src.Imdb_Rating)).ReverseMap();
               }).CreateMapper();
        }, true).Value;

    }
}
