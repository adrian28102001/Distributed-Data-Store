namespace PartitionLeader.Services.ServersDetails;

public interface IServerDetails
{
    IDictionary<int, int> GetServersCapacity();
}