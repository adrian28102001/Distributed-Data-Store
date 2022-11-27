﻿namespace PartitionLeader.Models;

public class Result
{
    public int StorageCount { get; set; }

    public int LastProcessedId { get; set; }

    public int Port { get; set; }

    public ServerName ServerName { get; set; }

    public Result()
    {
        StorageCount = Setting.Settings.ThisPort;
    }
}