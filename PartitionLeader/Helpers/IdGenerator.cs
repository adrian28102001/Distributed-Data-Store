namespace PartitionLeader.Helpers;

public static class IdGenerator
{
    private static readonly Mutex Mutex = new();
    private static int _id;

    public static int GenerateId()
    {
        Mutex.WaitOne();
        _id++;
        Mutex.ReleaseMutex();
        return _id;
    }
}