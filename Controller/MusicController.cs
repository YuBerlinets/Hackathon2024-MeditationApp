using meditationApp.DTO.music;
using meditationApp.exceptions.music;
using meditationApp.Repositories.Abscrations;
using meditationApp.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace meditationApp.Controller;

public class MusicController : BaseApiController
{
    private IMusicService _musicService;

    public MusicController(IMusicService musicService)
    {
        _musicService = musicService;
    }

    [HttpGet]
    public async Task<ActionResult<List<MusicResponseDTO>>> GetMusics()
    {
        var musics = await _musicService.GetMusics();
        if (musics.StatusCode != 200)
            return StatusCode(musics.StatusCode, musics.ErrorMessage);
        return Ok(musics.Data);
    }

    [HttpGet("get-music-by-category")]
    public async Task<ActionResult<List<MusicResponseDTO>>> GetMusicsByCategory([FromQuery] string category)
    {
        var musics = await _musicService.GetMusicsByCategoryAsync(category);
        if (musics.StatusCode != 200)
            return StatusCode(musics.StatusCode, musics.ErrorMessage);
        return Ok(musics.Data);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MusicResponseDTO>> GetMusic(int id)
    {
        var music = await _musicService.GetMusic(id);

        if (music.StatusCode != 200)
            return StatusCode(music.StatusCode, music.ErrorMessage);

        return Ok(music.Data);
    }

    [HttpGet("get-top-musics")]
    public async Task<ActionResult<List<MusicResponseDTO>>> GetTopMusic()
    {
        var musics = await _musicService.GetTopMusics();
        if (musics.StatusCode != 200)
            return StatusCode(musics.StatusCode, musics.ErrorMessage);
        return Ok(musics.Data);
    }

    [HttpPost]
    public async Task<ActionResult> CreateMusic([FromBody] CreateMusicDTO createMusicDTO)
    {
        var response = await _musicService.CreateMusic(createMusicDTO);
        if (response.StatusCode != 200)
            return StatusCode(response.StatusCode, response.ErrorMessage);
        return Ok(response.Data);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateMusic(int id, [FromBody] CreateMusicDTO createMusicDTO)
    {
        var response = await _musicService.UpdateMusic(id, createMusicDTO);
        if (response.StatusCode != 200)
            return StatusCode(response.StatusCode, response.ErrorMessage);

        return Ok(response.Data);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMusic(int id)
    {
        _musicService.DeleteMusic(id);
        return NoContent();
    }
}