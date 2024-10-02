using meditationApp.Data;
using meditationApp.DTO.article;
using meditationApp.Entities;
using meditationApp.Helpers;
using meditationApp.Repositories.Abscrations;
using Microsoft.EntityFrameworkCore;

namespace meditationApp.Repositories;

public class ArticleRepository : BaseRepository, IArticleRepository
{
    public ArticleRepository(StoreContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Article>> GetAllArticlesAsync()
    {
        return await _dbContext.Articles
            .Include(x => x.ImageBlocks)
            .Include(x => x.ParagraphItems)
            .Include(x => x.UnorderedListBlocks).ThenInclude(x => x.Items)
            .Include(x => x.ItemsSchema).ThenInclude(p => p.SectionItems)
            .AsSplitQuery()
            .ToListAsync();
    }

    public async Task<Article?> GetArticleByIdAsync(int id)
    {
        return await _dbContext.Articles
            .Include(x => x.ImageBlocks)
            .Include(x => x.ParagraphItems)
            .Include(x => x.UnorderedListBlocks).ThenInclude(x => x.Items)
            .Include(x => x.ItemsSchema).ThenInclude(p => p.SectionItems)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> CountArticlesAsync()
    {
        return await _dbContext.Articles.CountAsync();
    }

    public async Task<Article?> AddArticleAsync(Article? article)
    {
        await _dbContext.Articles.AddAsync(article);
        await _dbContext.SaveChangesAsync();
        return article;
    }

    public async Task<ICollection<Article?>> GetArticlesByCategoryAsync(string category)
    {
        return await _dbContext.Articles
            .Where(x => x.Category == category)
            .ToListAsync();
    }

    public async Task<Article?> GetArticlesByTypeAsync(string keyword)
    {
        return await _dbContext.Articles
            .FirstOrDefaultAsync(x => x.Type == keyword);
    }
}