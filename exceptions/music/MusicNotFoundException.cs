namespace meditationApp.exceptions.music;

public class MusicNotFoundException : Exception
{
    public MusicNotFoundException(string message) : base(message)
    {
    }
}