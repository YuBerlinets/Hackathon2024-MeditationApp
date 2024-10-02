using AutoMapper;
using meditationApp.DTO;
using meditationApp.DTO.article;
using meditationApp.DTO.music;
using meditationApp.DTO.user;
using meditationApp.Entities;
using meditationApp.Helpers;
using meditationApp.Repositories.Abscrations;
using meditationApp.Services.Abstractions;
using Microsoft.IdentityModel.Tokens;

namespace meditationApp.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IEventRepository _eventRepository;
    private IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper, IEventRepository eventRepository)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _eventRepository = eventRepository;
    }

    public async Task<Result<List<UserInformation>>> GetAllUsersByEventIdAsync(int id)
    {
        var appEvent = await _eventRepository.GetEventByIdAsync(id);

        if (appEvent == null)
            return Result<List<UserInformation>>.Failure(404, "Event not found");

        var result = await _userRepository.GetAllUsersByEventIdAsync(id);

        if (result.IsNullOrEmpty())
            return Result<List<UserInformation>>.Success([]);

        var response = result.Select(user => _mapper.Map<UserInformation>(user)).ToList();

        return Result<List<UserInformation>>.Success(response);
    }

    public async Task<Result<UserInformation>> GetUserByEmailAsync(string email)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);

        if (user == null)
            return Result<UserInformation>.Failure(404, "User not found");

        return Result<UserInformation>.Success(_mapper.Map<UserInformation>(user));
    }

    public async Task JoinEventAsync(int userId, int eventId)
    {
        await _userRepository.JoinEventAsync(userId, eventId);
    }

    public async Task AddMusicAsync(int userId, int musicId)
    {
        await _userRepository.AddMusicAsync(userId, musicId);
    }

    public async Task AddArticleAsync(int userId, int articleId)
    {
        await _userRepository.AddArticleAsync(userId, articleId);
    }

    public async Task<Result<List<ArticleResponseDTO>>> GetUserArticlesAsync(int userId)
    {
        var result = await _userRepository.GetUserArticlesAsync(userId);
        if (result.IsNullOrEmpty())
            return Result<List<ArticleResponseDTO>>.Success([]);
        return Result<List<ArticleResponseDTO>>.Success(result
            .Select(article => _mapper.Map<ArticleResponseDTO>(article)).ToList());
    }

    public async Task<Result<List<MusicResponseDTO>>> GetUserMusicsAsync(int userId)
    {
        var result = await _userRepository.GetUserMusicAsync(userId);
        if (result.IsNullOrEmpty())
            return Result<List<MusicResponseDTO>>.Success([]);

        return Result<List<MusicResponseDTO>>.Success(result.Select(x => _mapper.Map<MusicResponseDTO>(x)).ToList());
    }
}