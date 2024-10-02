using meditationApp.Data;

namespace meditationApp.Repositories.Abscrations;

public abstract class BaseRepository : IBaseRepository
{
    protected readonly StoreContext _dbContext;

    protected BaseRepository(StoreContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}