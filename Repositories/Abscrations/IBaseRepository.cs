namespace meditationApp.Repositories.Abscrations;

public interface IBaseRepository
{
    Task SaveChangesAsync(CancellationToken cancellationToken);
}