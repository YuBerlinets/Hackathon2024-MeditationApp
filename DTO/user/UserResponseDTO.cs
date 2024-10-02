namespace meditationApp.DTO.user;

public class UserResponseDTO
{
    public string Username { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
    public string? Email { get; set; }
    public string Token { get; set; }
}