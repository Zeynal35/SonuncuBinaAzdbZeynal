using Application.Abstracts.Services;
using Application.Options;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;

namespace Infrastructure.Services;

public class S3MinioFileStorageService : IFileStorageService
{
    private readonly IMinioClient _minio;
    private readonly MinioOptions _options;

    public S3MinioFileStorageService(
        IMinioClient minio,
        IOptions<MinioOptions> options)
    {
        _minio = minio;
        _options = options.Value;
    }

    public async Task<string> SaveAsync(
        Stream content,
        string fileName,
        string contentType,
        CancellationToken ct = default)
    {
        var bucketExists = await _minio.BucketExistsAsync(
            new BucketExistsArgs()
                .WithBucket(_options.Bucket),
            ct);

        if (!bucketExists)
        {
            await _minio.MakeBucketAsync(
                new MakeBucketArgs()
                    .WithBucket(_options.Bucket),
                ct);
        }

        var objectKey = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";

        await _minio.PutObjectAsync(
            new PutObjectArgs()
                .WithBucket(_options.Bucket)
                .WithObject(objectKey)
                .WithStreamData(content)
                .WithObjectSize(content.Length)
                .WithContentType(contentType),
            ct);

        return objectKey;
    }

    public async Task DeleteFileAsync(
        string objectKey,
        CancellationToken ct = default)
    {
        await _minio.RemoveObjectAsync(
            new RemoveObjectArgs()
                .WithBucket(_options.Bucket)
                .WithObject(objectKey),
            ct);
    }
}

