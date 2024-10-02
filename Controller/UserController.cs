using meditationApp.Entities;
using meditationApp.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace meditationApp.Controller;

public class UserController : BaseApiController
{
    private readonly UserManager<User> _userManager;

    private readonly IUserService _userService;

    public UserController(IUserService userService, UserManager<User> userManager)
    {
        _userService = userService;
        _userManager = userManager;
    }

    [HttpGet("users-by-event-id/{id}")]
    public async Task<ActionResult> GetUsersByEventId(int id)
    {
        var users = await _userService.GetAllUsersByEventIdAsync(id);
        if (users.StatusCode != 200)
            return StatusCode(users.StatusCode, users.ErrorMessage);
        return Ok(users.Data);
    }

    [HttpGet("user-by-email/{email}")]
    public async Task<ActionResult> GetUserByEmail(string email)
    {
        var user = await _userService.GetUserByEmailAsync(email);
        if (user.StatusCode != 200)
            return StatusCode(user.StatusCode, user.ErrorMessage);
        return Ok(user.Data);
    }

    [Authorize]
    [HttpPost("join-event/{eventId}")]
    public async Task<ActionResult> JoinEvent(int eventId)
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);

        if (user == null)
            return StatusCode(404, "User not found");

        await _userService.JoinEventAsync(user.Id, eventId);

        return Ok();
    }

    [HttpPost("add-music/{musicId}")]
    public async Task<ActionResult> AddMusic(int musicId)
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);

        if (user == null)
            return StatusCode(404, "User not found");

        await _userService.AddMusicAsync(user.Id, musicId);

        return Ok();
    }

    [HttpPost("add-article/{articleId}")]
    public async Task<ActionResult> AddArticle(int articleId)
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);

        if (user == null)
            return StatusCode(404, "User not found");

        await _userService.AddArticleAsync(user.Id, articleId);

        return Ok();
    }

    [HttpGet("get-user-articles")]
    public async Task<ActionResult> GetUserArticles()
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);

        if (user == null)
            return StatusCode(404, "User not found");

        var articles = await _userService.GetUserArticlesAsync(user.Id);

        if (articles.StatusCode != 200)
            return StatusCode(articles.StatusCode, articles.ErrorMessage);

        return Ok(articles.Data);
    }

    [HttpGet("get-user-musics")]
    public async Task<ActionResult> GetUserMusics()
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);

        if (user == null)
            return StatusCode(404, "User not found");

        var musics = await _userService.GetUserMusicsAsync(user.Id);

        if (musics.StatusCode != 200)
            return StatusCode(musics.StatusCode, musics.ErrorMessage);

        return Ok(musics.Data);
    }
}