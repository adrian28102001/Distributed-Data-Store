namespace Server2.Services.ServersDetails;

public interface IServerDetails
{
    IDictionary<int, int> GetServersCapacity();
}