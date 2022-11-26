namespace Server1.Models;

public class Data : IData
{
    public int Id { get; init; }
    public string StreamData { get; set; }
    public string ContentType { get; set; }
    public string FileName { get; set; }
}   