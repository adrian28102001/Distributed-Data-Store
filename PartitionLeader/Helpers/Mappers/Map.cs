using PartitionLeader.Models;

namespace PartitionLeader.Helpers.Mappers;

public static class Map
{
    public static Data MapData(this DataModel data)
    {
        var fileData = data.File;
        return new Data
        {
            Id = IdGenerator.GenerateId(),
            ContentType = fileData.ContentType,
            FileName = fileData.Name,
            StreamData = fileData.OpenReadStream()
        };
    }
}