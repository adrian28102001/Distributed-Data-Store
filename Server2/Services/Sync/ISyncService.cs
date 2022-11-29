namespace Server2.Services.Sync;

public interface ISyncService
{
    public Task SyncData(CancellationToken cancellationToken);
}