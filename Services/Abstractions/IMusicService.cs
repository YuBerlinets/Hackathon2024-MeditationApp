using meditationApp.DTO.music;
using meditationApp.Entities;
using meditationApp.Helpers;

namespace meditationApp.Services.Abstractions;

public interface IMusicService
{
    Task<Result<MusicResponseDTO>> GetMusic(int id);
    Task<Result<List<MusicResponseDTO>>> GetMusics();
    Task<Result<Music>> CreateMusic(CreateMusicDTO createMusicDTO);
    Task<Result<Music>> UpdateMusic(int id, CreateMusicDTO musicRequestDTO);
    void DeleteMusic(int id);
    Task<Result<List<MusicResponseDTO>>> GetTopMusics();
    Task<Result<List<MusicResponseDTO>>> GetMusicsByCategoryAsync(string category);
    Task<Result<MusicResponseDTO>> GetMusicByTypeAsync(string keyword);
}