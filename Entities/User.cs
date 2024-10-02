using Microsoft.AspNetCore.Identity;

namespace meditationApp.Entities;

public class User : IdentityUser<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<Event> Events { get; set; }
    public ICollection<Music> Musics { get; set; }
    public ICollection<Article> Articles { get; set; }
}