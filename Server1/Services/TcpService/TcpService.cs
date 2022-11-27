using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using Server1.Helpers;
using Server1.Models;
using Server1.Services.DataService;

namespace Server1.Services.TcpService;

public class TcpService : ITcpService
{
    private readonly IDataService _dataService;

    public TcpService(IDataService dataService)
    {
        _dataService = dataService;
    }

    public async Task RunTcp()
    {
        Console.WriteLine("Server starting !");

        var ipAddress = IPAddress.Loopback;
        int port = Settings.Settings.TcpPort;
        var ipEndPoint = new IPEndPoint(ipAddress, port);
        var listener = new TcpListener(ipEndPoint);
        listener.Start();

        Console.WriteLine("Server listening on: {0}:{1}", ipEndPoint.Address, ipEndPoint.Port);

        while (true)
        {
            var sender = await listener.AcceptTcpClientAsync();
            var request = StreamConverter.StreamToMessage(sender.GetStream());
            if (request != null)
            {
                var responseMessage = await MessageHandler(request);
                SendMessage(responseMessage, sender);
            }
            else
            {
                PrintConsole.Write("Requested data is empty", ConsoleColor.Red);
            }
        }
    }

    private async Task<string> MessageHandler(string message)
    {
        Console.WriteLine("Received message: " + message);
        var deserialized = JsonConvert.DeserializeObject<Data>(message);
        var resultSummary = await _dataService.Save(deserialized);

        var serializeObject = JsonConvert.SerializeObject(resultSummary);
        Console.WriteLine(deserialized.FileName);

        return serializeObject;
    }

    private static void SendMessage(string message, TcpClient client)
    {
        var bytes = StreamConverter.MessageToByteArray(message);
        client.GetStream().Write(bytes, 0, bytes.Length);
    }
}