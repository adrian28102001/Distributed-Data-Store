namespace PartitionLeader.Models;

public class Data : IData
{
    public int Id { get; set; }
    public Stream StreamData { get; set; }
    public string ContentType { get; set; }
    public string FileName { get; set; }
}   