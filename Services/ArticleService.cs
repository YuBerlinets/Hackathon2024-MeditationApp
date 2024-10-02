using AutoMapper;
using meditationApp.DTO.article;
using meditationApp.Entities;
using meditationApp.Helpers;
using meditationApp.Repositories;
using meditationApp.Repositories.Abscrations;
using meditationApp.Services.Abstractions;
using Microsoft.IdentityModel.Tokens;


namespace meditationApp.Services;

public class ArticleService : IArticleService
{
    private IArticleRepository _articleRepository;
    private IMapper _mapper;

    public ArticleService(IArticleRepository articleRepository, IMapper mapper)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<ArticleResponseDTO>>> GetAllArticlesAsync()
    {
        var articles = await _articleRepository.GetAllArticlesAsync();

        return Result<List<ArticleResponseDTO>>.Success(articles
            .Select(article => _mapper.Map<ArticleResponseDTO>(article)).ToList());
    }

    public async Task<Result<List<ArticleResponseDTO>>> GetArticlesByCategoryAsync(string category)
    {
        var articles = await _articleRepository.GetArticlesByCategoryAsync(category);

        if (articles.IsNullOrEmpty())
            return Result<List<ArticleResponseDTO>>.Failure(404, "Articles not found");

        return Result<List<ArticleResponseDTO>>.Success(articles
            .Select(article => _mapper.Map<ArticleResponseDTO>(article)).ToList());
    }

    public async Task<Result<ArticleResponseDTO>> GetArticlesByTypeAsync(string keyword)
    {
        var article = await _articleRepository.GetArticlesByTypeAsync(keyword);
        if (article == null)
        {
            return Result<ArticleResponseDTO>.Failure(404, "Article not found");
        }

        return Result<ArticleResponseDTO>.Success(_mapper.Map<ArticleResponseDTO>(article));
    }
    
    public async Task<Result<ArticleResponseDTO>> GetArticleByIdAsync(int id)
    {
        var article = await _articleRepository.GetArticleByIdAsync(id);

        return article == null
            ? Result<ArticleResponseDTO>.Failure(404, "Article not found")
            : Result<ArticleResponseDTO>.Success(_mapper.Map<ArticleResponseDTO>(article));
    }


    public async Task<Result<int>> GetCountArticlesAsync()
    {
        var count = await _articleRepository.CountArticlesAsync();

        return Result<int>.Success(count);
    }

    public async Task<Result<ArticleResponseDTO>> CreateArticleAsync(CreateArticleDTO createArticleDTO)
    {
        var article = _mapper.Map<Article>(createArticleDTO);

        var response = await _articleRepository.AddArticleAsync(article);

        return Result<ArticleResponseDTO>.Success(_mapper.Map<ArticleResponseDTO>(article));
    }
}