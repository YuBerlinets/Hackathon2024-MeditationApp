using AutoMapper;
using meditationApp.DTO.article;
using meditationApp.DTO.music;
using meditationApp.Entities;

namespace meditationApp.Helpers;

// public class AutoMapperConfig
// {
//     public static Mapper InitializeAutomapper()
//     {
//         var config = new MapperConfiguration(cfg =>
//         {
//             cfg.CreateMap<CreateArticleDTO, Article>();
//             cfg.CreateMap<Article, ArticleResponseDTO>();
//
//             cfg.CreateMap<CreateMusicDTO, Music>();
//             cfg.CreateMap<Music, MusicResponseDTO>();
//         });
//         var mapper = new Mapper(config);
//         return mapper;
//     }
// }