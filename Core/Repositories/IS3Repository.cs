﻿namespace Core.Repositories;

public interface IS3Repository
{
    Task<string> GetFileFromUri(string uri);
}