using AutoMapper;
using meditationApp.DTO.music;
using meditationApp.Entities;
using meditationApp.exceptions.music;
using meditationApp.Helpers;
using meditationApp.Repositories.Abscrations;
using meditationApp.Services.Abstractions;

namespace meditationApp.Services;

public class MusicService : IMusicService
{
    private IMusicRepository _musicRepository;
    private IMapper _mapper;

    public MusicService(IMusicRepository musicRepository, IMapper mapper)
    {
        _musicRepository = musicRepository;
        _mapper = mapper;
    }

    public async Task<Result<MusicResponseDTO>> GetMusic(int id)
    {
        var music = await _musicRepository.GetMusicByIdAsync(id);
        return music == null
            ? Result<MusicResponseDTO>.Failure(404, "Music not found")
            : Result<MusicResponseDTO>.Success(_mapper.Map<MusicResponseDTO>(music));
    }

    public async Task<Result<List<MusicResponseDTO>>> GetMusics()
    {
        var allMusic = await _musicRepository.GetAllMusicsAsync();
        return Result<List<MusicResponseDTO>>.Success(allMusic
            .Select(music => _mapper.Map<MusicResponseDTO>(music)).ToList());
    }

    public async Task<Result<Music>> CreateMusic(CreateMusicDTO createMusicDTO)
    {
        var music = _mapper.Map<Music>(createMusicDTO);

        await _musicRepository.AddMusicAsync(music);

        return Result<Music>.Success(music);
    }

    public async Task<Result<Music>> UpdateMusic(int id, CreateMusicDTO musicRequestDTO)
    {
        var music = await _musicRepository.GetMusicByIdAsync(id);

        if (music == null)
            return Result<Music>.Failure(404, "Music not found");

        music.Name = musicRequestDTO.Name;
        music.Author = musicRequestDTO.Author;
        music.Type = musicRequestDTO.Type;
        music.Duration = musicRequestDTO.Duration;
        music.Url = musicRequestDTO.Url;

        _musicRepository.UpdateMusicAsync(music);
        return Result<Music>.Success(music);
    }

    public async void DeleteMusic(int id)
    {
        var music = await _musicRepository.GetMusicByIdAsync(id);

        if (music == null)
            throw new MusicNotFoundException($"Music with id {id} not found");

        _musicRepository.DeleteMusicAsync(music);
    }

    public async Task<Result<List<MusicResponseDTO>>> GetTopMusics()
    {
        var musics = await _musicRepository.GetTopMusicsAsync();
        return Result<List<MusicResponseDTO>>.Success(musics.Select(music => _mapper.Map<MusicResponseDTO>(music))
            .ToList());
    }

    public async Task<Result<List<MusicResponseDTO>>> GetMusicsByCategoryAsync(string category)
    {
        var musics = await _musicRepository.GetMusicByCategoryAsync(category);
        Console.WriteLine(musics);
        return Result<List<MusicResponseDTO>>.Success(musics.Select(music => _mapper.Map<MusicResponseDTO>(music))
            .ToList());
    }

    public async Task<Result<MusicResponseDTO>> GetMusicByTypeAsync(string keyword)
    {
        var music = await _musicRepository.GetMusicByTypeAsync(keyword);
        return music == null
            ? Result<MusicResponseDTO>.Failure(404, "Music not found")
            : Result<MusicResponseDTO>.Success(_mapper.Map<MusicResponseDTO>(music));
    }
}