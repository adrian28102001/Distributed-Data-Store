namespace PartitionLeader.Services.Data;

public interface IDataService
{
    public bool DoesKeyExist(int id);
}