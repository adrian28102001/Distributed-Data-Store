using System.Net.Sockets;
using Newtonsoft.Json;
using Server2.Helpers;
using Server2.Models;

namespace Server2.Services.TcpService;

public class TcpService : ITcpService
{
    public Result? TcpSave(Data data, int serverPort)
    {
        var requestMessage = JsonConvert.SerializeObject(data);
        var responseMessage = SendMessage(requestMessage, serverPort);

        var deserialized = JsonConvert.DeserializeObject<Result>(responseMessage);

        Console.WriteLine(responseMessage);

        return deserialized;
    }

    private string SendMessage(string message, int serverPort)
    {
        var response = "";
        try
        {
            var client = new TcpClient("127.0.0.1", serverPort);
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