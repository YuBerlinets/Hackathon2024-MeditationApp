using meditationApp.Data;
using meditationApp.Entities;
using meditationApp.Repositories.Abscrations;
using Microsoft.EntityFrameworkCore;

namespace meditationApp.Repositories;

public class UserRepository : BaseRepository, IUserRepository

{
    public UserRepository(StoreContext dbContext) : base(dbContext)
    {
    }

    public Task<List<User>> GetAllUsersByEventIdAsync(int id)
    {
        return _dbContext.Users.Where(x => x.Events.Any(x => x.Id == id)).ToListAsync();
    }

    public Task<List<Event?>> GetAllEventsByUserIdAsync(int id)
    {
        return _dbContext.Events.Where(x => x.Participants.Any(x => x.Id == id)).ToListAsync();
    }

    public Task<User?> GetUserByEmailAsync(string email)
    {
        return _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task JoinEventAsync(int userId, int eventId)
    {
        var user = await _dbContext.Users
            .Include(u => u.Events)
            .FirstOrDefaultAsync(x => x.Id == userId);

        var appEvent = await _dbContext.Events
            .Include(e => e.Participants)
            .FirstOrDefaultAsync(x => x.Id == eventId);

        if (user == null || appEvent == null)
            return;

        if (!appEvent.Participants.Contains(user))
        {
            appEvent.Participants.Add(user);
            user.Events.Add(appEvent);


            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task AddMusicAsync(int userId, int musicId)
    {
        var music = await _dbContext.Musics.FindAsync(musicId);
        var user = await _dbContext.Users
            .Include(u => u.Musics)
            .FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null || music == null)
            return;

        if (!user.Musics.Contains(music))
        {
            user.Musics.Add(music);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task AddArticleAsync(int userId, int articleId)
    {
        var article = await _dbContext.Articles.FindAsync(articleId);
        var user = await _dbContext.Users
            .Include(u => u.Articles)
            .FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null || article == null)
            return;

        if (!user.Articles.Contains(article))
        {
            user.Articles.Add(article);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<List<Article>?> GetUserArticlesAsync(int userId)
    {
        var user = await _dbContext.Users
            .Include(u => u.Articles)
            .FirstOrDefaultAsync(x => x.Id == userId);

        return user?.Articles.ToList();
    }

    public async Task<List<Music>?> GetUserMusicAsync(int userId)
    {
        var user = await _dbContext.Users
            .Include(u => u.Musics)
            .FirstOrDefaultAsync(x => x.Id == userId);
        return user?.Musics.ToList();
    }
}