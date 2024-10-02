using System.Text;
using meditationApp.Controller;
using meditationApp.DTO.article;
using meditationApp.DTO.chat;
using meditationApp.DTO.music;
using meditationApp.Entities;
using meditationApp.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public class AIChatController : BaseApiController
{
    private readonly HttpClient _httpClient;
    private readonly IArticleService _articleService;
    private readonly IMusicService _musicService;

    public AIChatController(HttpClient httpClient, IArticleService articleService, IMusicService musicService)
    {
        _httpClient = httpClient;
        _articleService = articleService;
        _musicService = musicService;
    }

    [HttpPost("analyze")]
    public async Task<IActionResult> AnalyzeMentalHealth([FromBody] ProblemDescription? problem)
    {
        if (problem == null || string.IsNullOrWhiteSpace(problem.description))
        {
            return BadRequest("Invalid problem description.");
        }

        var knownKeywords = new List<string>
        {
            "Chronic fatigue", "fatigue", "overexertion", "exhaustion", "lack of energy",
            "Anxiety", "worry", "nervousness", "panic", "stress tension",
            "Distractibility", "difficulty concentrating", "distraction", "inability to focus",
            "Stress", "pressure", "emotional overload", "fatigue from life", "tension",
            "Burnout", "work fatigue", "professional exhaustion", "emotional burnout",
            "Apathy", "loss of motivation", "indifference", "lack of energy", "disinterest",
            "Perfectionism", "high standards", "self-criticism", "striving for ideal", "fear of failure",
            "Insomnia", "sleep difficulties", "sleep disorders", "lack of sleep", "problems falling asleep",
            "Loneliness", "isolation", "feeling abandoned", "social disconnection", "lack of communication",
            "Lack of self-confidence", "low self-esteem", "self-doubt", "fear of failure", "inner criticism"
        };

        var jsonContent = JsonConvert.SerializeObject(problem);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("path-to-your-endpoint", content);

        if (!response.IsSuccessStatusCode)
        {
            return StatusCode((int)response.StatusCode, "Error communicating with the FastAPI server.");
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);

        var keywordsResponse = JsonConvert.DeserializeObject<KeywordsResponse>(responseContent);

        if (!string.IsNullOrEmpty(keywordsResponse?.Keywords))
        {
            var returnedKeywords = keywordsResponse.Keywords
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(keyword => keyword.Trim())
                .ToList();

            var matchedKeywords = knownKeywords.Intersect(returnedKeywords).ToList();

            var keywordDataList = new List<KeywordData>();

            if (matchedKeywords.Count > 4)
            {
                matchedKeywords = matchedKeywords.Take(4).ToList();
            }

            foreach (var keyword in matchedKeywords)
            {
                var article = await FetchArticleForKeyword(keyword);
                var music = await FetchMusicForKeyword(keyword);
                if (article != null || music != null)
                {
                    keywordDataList.Add(new KeywordData
                    {
                        Keyword = keyword,
                        Article = article,
                        Music = music
                    });
                }
            }

            return Ok(keywordDataList);
        }

        return BadRequest("Could not extract keywords.");
    }

    public class KeywordData
    {
        public string Keyword { get; set; }
        public ArticleResponseDTO Article { get; set; }
        public MusicResponseDTO Music { get; set; }
    }

    private async Task<ArticleResponseDTO> FetchArticleForKeyword(string keyword)
    {
        var response = await _articleService.GetArticlesByTypeAsync(keyword);
        if (response.StatusCode != 200)
        {
            return null;
        }

        return response.Data;
    }

    private async Task<MusicResponseDTO> FetchMusicForKeyword(string keyword)
    {
        var response = await _musicService.GetMusicByTypeAsync(keyword);

        if (response.StatusCode != 200)
        {
            return null;
        }

        return response.Data;
    }
}