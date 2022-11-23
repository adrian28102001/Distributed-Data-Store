using Newtonsoft.Json;
using PartitionLeader.Helpers;
using PartitionLeader.Models;

namespace PartitionLeader.Services.HttpService;

public static class GenericHttp
{
    public static async Task<IDictionary<int, Data>?> Get(string url)
    {
        try
        {
            var deserialized = await GetObject(url);

            PrintConsole.Write($"Got data from url {url}", ConsoleColor.Green);
            return deserialized;
        }
        catch (Exception e)
        {
            PrintConsole.Write($"Failed get from {url}", ConsoleColor.DarkRed);
        }

        return null;
    }

    public static async Task<IDictionary<int, Data>?> Get(int id, string url)
    {
        try
        {
            var deserialized = await GetObject(url);

            PrintConsole.Write($"Got data from url {url} for id: {id}", ConsoleColor.Green);
            return deserialized;
        }
        catch (Exception e)
        {
            PrintConsole.Write($"Failed get from {url}for id: {id}", ConsoleColor.DarkRed);
        }

        return null;
    }

    private static async Task<IDictionary<int, Data>> GetObject(string url)
    {
        using var client = new HttpClient();

        var response = await client.GetAsync($"{url}");

        var dataAsJson = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<IDictionary<int, Data>>(dataAsJson);
    }
}