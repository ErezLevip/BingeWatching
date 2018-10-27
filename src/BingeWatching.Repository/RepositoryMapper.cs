using AutoMapper;
using BingeWatching.Domain.Entities;
using BingeWatching.Repository.Models;
using System;

namespace BingeWatching.Repository
{
    public class RepositoryMapper
    {
        public static IMapper Instance = new Lazy<IMapper>(() =>
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ContentModel, Content>().ReverseMap();
            }).CreateMapper();
        }, true).Value;
    }
}
