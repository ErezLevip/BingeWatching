using AutoMapper;
using BingeWatching.API.ContentServiceApiClient;
using BingeWatching.API.ViewModels;
using BingeWatching.Contracts.ViewModels;
using BingeWatching.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BingeWatching.API.Mappers
{
    public class BingeWatchingApiMapper
    {
        public static IMapper Instance = new Lazy<IMapper>(() =>
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserHistory, HistoryViewModel>()
                .ForMember(m => m.UserRanking, m => m.MapFrom(src => src.UserRanking))
                .ForMember(m => m.Title, m => m.MapFrom(src => src.Content.Title))
                .ForMember(m => m.ImdbRating, m => m.MapFrom(src => src.Content.ImdbRating))
                .ReverseMap();
            }).CreateMapper();
        }, true).Value;
    }

}
