namespace PartitionLeader.Services.StorageService;

public interface IStorageStatus
{
    string GetBestServerUrl();
}