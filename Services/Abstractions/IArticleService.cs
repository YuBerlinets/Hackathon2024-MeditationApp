using meditationApp.DTO.article;
using meditationApp.Entities;
using meditationApp.Helpers;

namespace meditationApp.Services.Abstractions;

public interface IArticleService
{
    Task<Result<ArticleResponseDTO>> GetArticleByIdAsync(int id);

    Task<Result<int>> GetCountArticlesAsync();

    Task<Result<ArticleResponseDTO>> CreateArticleAsync(CreateArticleDTO createArticleDTO);

    Task<Result<List<ArticleResponseDTO>>> GetAllArticlesAsync();
    Task<Result<List<ArticleResponseDTO>>> GetArticlesByCategoryAsync(string category);
    Task<Result<ArticleResponseDTO>> GetArticlesByTypeAsync(string keyword);
}