using Amazon.S3;
using Core.Repositories;
using Infrastructure.Helpers;

namespace Infrastructure.Repositories;

public class S3Repository : IS3Repository
{
    private readonly IAmazonS3 _s3;

    public S3Repository(IAmazonS3 s3)
    {
        _s3 = s3;
    }

    public async Task<string> GetFileFromUri(string uri)
    {
        var bucket = UriHelper.GetBucket(uri);
        var bucketExists = await _s3.DoesS3BucketExistAsync(bucket);
        if (!bucketExists) throw new Exception($"Bucket {bucket} does not exist.");

        var key = UriHelper.GetKey(uri);
        var s3Object = await _s3.GetObjectAsync(bucket, key);
        await using var responseStream = s3Object.ResponseStream;
        using var reader = new StreamReader(responseStream);
        var title = s3Object.Metadata["x-amz-meta-title"]; // Assume you have "title" as medata added to the object.
        var contentType = s3Object.Headers["Content-Type"];
        Console.WriteLine("Object metadata, Title: {0}", title);
        Console.WriteLine("Content type: {0}", contentType);

        return await reader.ReadToEndAsync(); // Now you process the response body.
    }
}