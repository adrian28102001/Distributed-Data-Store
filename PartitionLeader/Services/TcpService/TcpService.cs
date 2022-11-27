using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PartitionLeader.Services.TcpService;

public static class TcpService
{
    private static readonly Encoding Encoding = Encoding.UTF8;

    public static void Send()
    {
        const string file = "Hello";
        var responseMessage = SendMessage(file);
        Console.WriteLine(responseMessage);
    }

    private static string SendMessage(string message)
    {
        var response = "";
        try
        {
            var client = new TcpClient("127.0.0.1", Settings.Settings.TcpPort); // Create a new connection  
            client.NoDelay = true; // please check TcpClient for more optimization

            var messageBytes = MessageToByteArray(message);

            using (var stream = client.GetStream())
            {
                stream.Write(messageBytes, 0, messageBytes.Length);

                // Message sent!  Wait for the response stream of bytes...
                // streamToMessage - discussed later
                response = StreamToMessage(stream);
            }

            client.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return response;
    }

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