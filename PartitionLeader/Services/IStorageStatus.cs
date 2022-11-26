namespace PartitionLeader.Services;

public interface IStorageStatus
{
    string GetBestServerUrl();
}