using System.Net.Sockets;
using Newtonsoft.Json;
using PartitionLeader.Helpers;
using PartitionLeader.Models;

namespace PartitionLeader.Services.TcpService;

public class TcpService : ITcpService
{
    public string TcpSave(Data data)
    {
        var requestMessage = JsonConvert.SerializeObject(data);
        var responseMessage = SendMessage(requestMessage);
        Console.WriteLine(responseMessage);
        return responseMessage;
    }

    private static string SendMessage(string message)
    {
        var response = "";
        try
        {
            var client = new TcpClient("127.0.0.1", 8081);
            client.NoDelay = true;
            var messageBytes = StreamConverter.MessageToByteArray(message);

            using (var stream = client.GetStream())
            {
                stream.Write(messageBytes, 0, messageBytes.Length);

                response = StreamConverter.StreamToMessage(stream);
            }

            client.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return response;
    }
}