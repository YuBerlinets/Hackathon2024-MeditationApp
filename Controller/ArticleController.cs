using meditationApp.DTO.article;
using meditationApp.Entities;
using meditationApp.Services;
using meditationApp.Services.Abstractions;
using meditationApp.Services.Admin;
using Microsoft.AspNetCore.Mvc;

namespace meditationApp.Controller;

public class ArticleController : BaseApiController
{
    private readonly IArticleService _articleService;
    private readonly IWebHostEnvironment _environment;


    public ArticleController(IArticleService articleService, IWebHostEnvironment environment)
    {
        _articleService = articleService;
        _environment = environment;
    }

    [HttpGet]
    public async Task<ActionResult<List<Article>>> GetArticles()
    {
        var articles = await _articleService.GetAllArticlesAsync();
        return Ok(articles.Data);
    }

    [HttpGet("get-articles-by-category")]
    public async Task<ActionResult<List<Article>>> GetArticlesByCategory([FromQuery] string category)
    {
        var articles = await _articleService.GetArticlesByCategoryAsync(category);
        if (articles.StatusCode != 200)
            return StatusCode(articles.StatusCode, articles.ErrorMessage);
        return Ok(articles.Data);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Article>> GetArticle(int id)
    {
        var article = await _articleService.GetArticleByIdAsync(id);
        if (article.StatusCode != 200)
            return StatusCode(article.StatusCode, article.ErrorMessage);
        return Ok(article.Data);
    }

    [HttpPost]
    public async Task<ActionResult<Article>> CreateArticle([FromBody] CreateArticleDTO article)
    {
        var newArticle = await _articleService.CreateArticleAsync(article);
        if (newArticle.StatusCode != 200)
            return StatusCode(newArticle.StatusCode, newArticle.ErrorMessage);
        return Ok(newArticle.Data);
    }


    [HttpGet("count")]
    public async Task<ActionResult<int>> PrefetchCountArticles()
    {
        var count = await _articleService.GetCountArticlesAsync();
        return Ok(count.Data);
    }

    [HttpPost("add-image-to-article")]
    public async Task<string> AddImageToArticleAsync(IFormFile image)
    {
        if (image == null || image.Length == 0)
            throw new ArgumentException("Image file is invalid.");

        string uploadsFolder = Path.Combine(_environment.WebRootPath, "articles", "articleImages");

        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(image.FileName);

        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(fileStream);
        }

        string relativePath = Path.Combine("articles", "articleImages", uniqueFileName).Replace("\\", "/");

        return "/" + relativePath; 
    }
}