namespace Server1.Services.Sync;

public interface ISyncService
{
    public void SyncData(CancellationToken cancellationToken);
}