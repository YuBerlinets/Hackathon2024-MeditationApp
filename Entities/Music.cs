using meditationApp.Entities.enums;

namespace meditationApp.Entities;

public class Music
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string Author { get; set; }

    public string Category { get; set; }

    public string Duration { get; set; }

    public string Type { get; set; }
    
    public string PreviewUrl { get; set; }
    public string Url { get; set; }

    public virtual ICollection<User> Users { get; set; }
}