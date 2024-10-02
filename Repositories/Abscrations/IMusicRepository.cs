using meditationApp.Entities;

namespace meditationApp.Repositories.Abscrations;

public interface IMusicRepository : IBaseRepository
{
    Task<IEnumerable<Music?>> GetAllMusicsAsync();

    Task<Music?> GetMusicByIdAsync(int id);

    Task<Music?> AddMusicAsync(Music? music);

    void UpdateMusicAsync(Music? music);

    void DeleteMusicAsync(Music? music);
    Task<ICollection<Music>> GetTopMusicsAsync();
    Task<ICollection<Music?>> GetMusicByCategoryAsync(string category);
    Task<Music?> GetMusicByTypeAsync(string keyword);
}