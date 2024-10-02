using API.DTOs;
using AutoMapper;
using meditationApp.Data;
using meditationApp.DTO;
using meditationApp.DTO.user;
using meditationApp.Entities;
using meditationApp.Services;

namespace meditationApp.Controller;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class AccountController : BaseApiController
{
    private readonly UserManager<User> _userManager;
    private readonly TokenService _tokenService;
    private readonly IMapper _mapper;

    public AccountController(UserManager<User> userManager, TokenService tokenService, IMapper mapper)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _mapper = mapper;
    }


    [HttpPost("login")]
    public async Task<ActionResult<UserResponseDTO>> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.Username);
        if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            return Unauthorized();

        return new UserResponseDTO
        {
            Email = user.Email,
            Token = await _tokenService.GenerateToke(user),
        };
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterDto registerDto)
    {
        var user = new User
        {
            UserName = registerDto.Username, Email = registerDto.Email, FirstName = registerDto.FirstName,
            LastName = registerDto.LastName
        };
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return ValidationProblem();
        }

        await _userManager.AddToRoleAsync(user, "Member");
        return StatusCode(201);
    }

    [Authorize]
    [HttpGet("currentUser")]
    public async Task<ActionResult<UserResponseDTO>> GetCurrentUser()
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);

        return _mapper.Map<UserResponseDTO>(user);
    }
}