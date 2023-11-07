namespace MoneyTracker.Domain.Shared.Repositories;

public interface IBlobStorageRepository
{
    Task UploadAsync(string filePath, BlobFile blobFile, CancellationToken cancellationToken = default);

    Task<Image> GetImageFileAsync(string filePath, CancellationToken cancellationToken = default);
}