using AutoMapper;
using meditationApp.DTO;
using meditationApp.DTO.article;
using meditationApp.DTO.events;
using meditationApp.DTO.music;
using meditationApp.DTO.user;
using meditationApp.Entities;

namespace meditationApp.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateArticleDTO, Article>()
            .ForMember(dest => dest.PublishDate,
                opt => opt.MapFrom(src => src.PublishDate));
        CreateMap<Article, ArticleResponseDTO>()
            .ForMember(dest => dest.PublishDate,
                opt => opt.MapFrom(src => src.PublishDate.ToString("yyyy-MM-dd")));

        CreateMap<CreateMusicDTO, Music>();
        CreateMap<Music, MusicResponseDTO>();

        CreateMap<User, UserResponseDTO>();
        CreateMap<User, UserInformation>();

        CreateMap<CreateEventDTO, Event>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Parse(src.Date).ToUniversalTime()));
        CreateMap<Event, EventResponseDTO>();
    }

    private DateTime? ParseDate(string dateStr)
    {
        if (DateTime.TryParse(dateStr, out var date))
        {
            return date.ToUniversalTime();
        }

        return null;
    }
}

// public class NullableDateTimeConverter : ITypeConverter<string, DateTime?>
// {
//     public DateTime? Convert(string source, DateTime? destination, ResolutionContext context)
//     {
//         if (string.IsNullOrEmpty(source))
//         {
//             return null;
//         }
//
//         return DateTime.TryParse(source, out var date) ? (DateTime?)date : null;
//     }
// }