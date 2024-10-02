using meditationApp.DTO.article;
using meditationApp.Entities;
using meditationApp.Helpers;

namespace meditationApp.Repositories.Abscrations;

public interface IArticleRepository : IBaseRepository
{
    Task<List<Article>> GetAllArticlesAsync();

    Task<Article?> GetArticleByIdAsync(int id);

    Task<int> CountArticlesAsync();
    Task<Article?> AddArticleAsync(Article? article);
    Task<ICollection<Article?>> GetArticlesByCategoryAsync(string category);
    Task<Article?> GetArticlesByTypeAsync(string keyword);
}