namespace PartitionLeader.Services.Sync;

public interface ISyncService
{
    public Task SyncData(CancellationToken cancellationToken);
}