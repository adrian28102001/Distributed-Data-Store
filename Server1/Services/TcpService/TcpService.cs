using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server1.Services.TcpService;

public static class TcpService
{
    private static readonly Encoding Encoding = Encoding.UTF8;

    public static void Test()
    {
        const int port = Settings.Settings.Port;
        Console.WriteLine("Server starting !");

        // IP Address to listen on. Loopback in this case
        var ipAddress = IPAddress.Loopback;

        // Create a network endpoint
        var ep = new IPEndPoint(ipAddress, port);

        // Create and start a TCP listener
        var listener = new TcpListener(ep);
        listener.Start();

        Console.WriteLine("Server listening on: {0}:{1}", ep.Address, ep.Port);

        while (true)
        {
            var sender = listener.AcceptTcpClient();
            var request = StreamToMessage(sender.GetStream());
            if (request != null)
            {
                var responseMessage = "Received message: " + request;
                Console.WriteLine(responseMessage);
                SendMessage(responseMessage, sender);
            }
        }
    }

    private static void SendMessage(string message, TcpClient client)
    {
        // messageToByteArray- discussed later
        var bytes = MessageToByteArray(message);
        client.GetStream().Write(bytes, 0, bytes.Length);
    }

    // using UTF8 encoding for the messages

    private static byte[] MessageToByteArray(string message)
    {
        // get the size of original message
        var messageBytes = Encoding.GetBytes(message);
        var messageSize = messageBytes.Length;
        // add content length bytes to the original size
        var completeSize = messageSize + 4;
        // create a buffer of the size of the complete message size
        var completeMessage = new byte[completeSize];

        // convert message size to bytes
        var sizeBytes = BitConverter.GetBytes(messageSize);
        // copy the size bytes and the message bytes to our overall message to be sent 
        sizeBytes.CopyTo(completeMessage, 0);
        messageBytes.CopyTo(completeMessage, 4);
        return completeMessage;
    }

    private static string StreamToMessage(Stream stream)
    {
        // size bytes have been fixed to 4
        var sizeBytes = new byte[4];
        // read the content length
        stream.ReadAsync(sizeBytes, 0, 4);
        var messageSize = BitConverter.ToInt32(sizeBytes, 0);
        // create a buffer of the content length size and read from the stream
        var messageBytes = new byte[messageSize];
        stream.ReadAsync(messageBytes, 0, messageSize);
        // convert message byte array to the message string using the encoding
        var message = Encoding.GetString(messageBytes);
        string result = null!;

        foreach (var c in message)
            if (c != '\0')
            {
                result += c;
            }

        return result;
    }
}