namespace Infrastructure.Helpers;

public class UriHelper
{
    public static bool IsValid(string uri)
    {
        if (!uri.StartsWith("s3://"))
            return false;
        if (uri.Split("/").Length < 3)
            return false;
        if (!uri.EndsWith(".txt"))
            return false;

        return true;
    }

    public static string GetBucket(string uri)
    {
        if (!IsValid(uri))
        {
            throw new Exception($"Uri {uri} invalid");
        }

        return uri.Split("/")[2];
    }

    public static string GetKey(string uri)
    {
        if (!IsValid(uri))
        {
            throw new Exception($"Uri {uri} invalid");
        }

        var bucket = GetBucket(uri);
        return uri.Replace($"s3://{bucket}/", "");
    }
}