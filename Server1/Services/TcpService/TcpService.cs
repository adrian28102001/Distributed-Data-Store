using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using Server1.Helpers;
using Server1.Models;

namespace Server1.Services.TcpService;

public class TcpService : ITcpService
{
    public void RunTcp()
    {
        Console.WriteLine("Server starting !");

        var ipAddress = IPAddress.Loopback;
        const int port = Settings.Settings.TcpPort;
        var ipEndPoint = new IPEndPoint(ipAddress, port);
        var listener = new TcpListener(ipEndPoint);
        listener.Start();

        Console.WriteLine("Server listening on: {0}:{1}", ipEndPoint.Address, ipEndPoint.Port);

        while (true)
        {
            var sender = listener.AcceptTcpClient();
            var request = StreamConverter.StreamToMessage(sender.GetStream());
            if (request != null)
            {
                var responseMessage = MessageHandler(request);
                SendMessage(responseMessage, sender);
            }
            else
            {
                PrintConsole.Write("Requested data is empty", ConsoleColor.Red);
            }
        }
    }

    private static string MessageHandler(string message)
    {
        Console.WriteLine("Received message: " + message);
        var deserialized = JsonConvert.DeserializeObject<Data>(message);
        Console.WriteLine(deserialized?.FileName);
        return "Success";
    }

    private static void SendMessage(string message, TcpClient client)
    {
        var bytes = StreamConverter.MessageToByteArray(message);
        client.GetStream().Write(bytes, 0, bytes.Length);
    }
}