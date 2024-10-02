using meditationApp.DTO;
using meditationApp.DTO.article;
using meditationApp.DTO.music;
using meditationApp.DTO.user;
using meditationApp.Entities;
using meditationApp.Helpers;

namespace meditationApp.Services.Abstractions;

public interface IUserService
{
    Task<Result<List<UserInformation>>> GetAllUsersByEventIdAsync(int id);
    Task<Result<UserInformation>> GetUserByEmailAsync(string email);
    Task JoinEventAsync(int userId, int eventId);
    Task AddMusicAsync(int userId, int musicId);
    Task AddArticleAsync(int userId, int articleId);
    Task<Result<List<ArticleResponseDTO>>> GetUserArticlesAsync(int userId);
    Task<Result<List<MusicResponseDTO>>> GetUserMusicsAsync(int userId);
}