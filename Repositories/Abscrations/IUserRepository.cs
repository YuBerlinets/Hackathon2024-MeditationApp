using meditationApp.DTO.music;
using meditationApp.Entities;

namespace meditationApp.Repositories.Abscrations;

public interface IUserRepository : IBaseRepository
{
    Task<List<User>> GetAllUsersByEventIdAsync(int id);
    Task<List<Event?>> GetAllEventsByUserIdAsync(int id);
    Task<User?> GetUserByEmailAsync(string email);
    Task JoinEventAsync(int userId, int eventId);
    Task AddMusicAsync(int userId, int musicId);
    Task AddArticleAsync(int userId, int articleId);
    Task<List<Article>?> GetUserArticlesAsync(int userId);
    Task<List<Music>?> GetUserMusicAsync(int userId);
}