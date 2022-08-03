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
        var response = await _s3.GetObjectAsync(bucket, key);
        await using var responseStream = response.ResponseStream;
        using var reader = new StreamReader(responseStream);
        var title = response.Metadata["x-amz-meta-title"]; // Assume you have "title" as medata added to the object.
        var contentType = response.Headers["Content-Type"];
        Console.WriteLine("Object metadata, Title: {0}", title);
        Console.WriteLine("Content type: {0}", contentType);

        return await reader.ReadToEndAsync(); // Now you process the response body.
    }

    public async Task DeleteFileFromUri(string uri)
    {
        var bucket = UriHelper.GetBucket(uri);
        var bucketExists = await _s3.DoesS3BucketExistAsync(bucket);
        if (!bucketExists) throw new Exception($"Bucket {bucket} does not exist.");

        var key = UriHelper.GetKey(uri);
        await _s3.DeleteObjectAsync(bucket, key);
    }
}