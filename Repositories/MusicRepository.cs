using meditationApp.Data;
using meditationApp.Entities;
using meditationApp.Repositories.Abscrations;
using Microsoft.EntityFrameworkCore;

namespace meditationApp.Repositories;

public class MusicRepository : BaseRepository, IMusicRepository
{
    public MusicRepository(StoreContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Music?>> GetAllMusicsAsync()
    {
        return await _dbContext.Musics.ToListAsync();
    }

    public async Task<Music?> GetMusicByIdAsync(int id)
    {
        return await _dbContext.Musics.FindAsync(id);
    }

    public async Task<Music?> AddMusicAsync(Music? music)
    {
        await _dbContext.Musics.AddAsync(music);
        await SaveChangesAsync(default);
        return music;
    }

    public async void UpdateMusicAsync(Music? music)
    {
        _dbContext.Musics.Update(music);
        await _dbContext.SaveChangesAsync();
    }

    public async void DeleteMusicAsync(Music? music)
    {
        _dbContext.Musics.Remove(music);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ICollection<Music>> GetTopMusicsAsync()
    {
        return await _dbContext.Musics
            .OrderByDescending(e => e.Name)
            .Take(4)
            .ToListAsync();
    }

    public async Task<ICollection<Music?>> GetMusicByCategoryAsync(string category)
    {
        return await _dbContext.Musics
            .Where(x => x.Category == category)
            .ToListAsync();
    }

    public async Task<Music?> GetMusicByTypeAsync(string keyword)
    {
        return await _dbContext.Musics
            .FirstOrDefaultAsync(x => x.Type == keyword);
    }
}