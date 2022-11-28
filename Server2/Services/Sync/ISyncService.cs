namespace Server2.Services.Sync;

public interface ISyncService
{
    public void SyncData(CancellationToken cancellationToken);
}