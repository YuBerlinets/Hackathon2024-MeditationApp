namespace meditationApp.exceptions.article;

public class ArticleNotFoundException : Exception
{
    public ArticleNotFoundException(string message) : base(message)
    {
    }
}