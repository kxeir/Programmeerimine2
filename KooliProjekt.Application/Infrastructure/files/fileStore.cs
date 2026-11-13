using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Infrastructure.Files;

public sealed class FileStore
{
    private readonly IFileClient _fileClient;

    public FileStore(IFileClient fileClient)
    {
        _fileClient = fileClient;
    }

    public Task SaveFileAsync(IFormFile file, string relativeDirectory, CancellationToken cancellationToken = default)
    {
        var fileName = GetFileName(file?.FileName);
        return _fileClient.SaveAsync(file, Path.Combine(relativeDirectory, fileName), cancellationToken);
    }

    public Task DeleteFileAsync(string fileName, string relativeDirectory, CancellationToken cancellationToken = default)
    {
        return _fileClient.DeleteAsync(Path.Combine(relativeDirectory, GetFileName(fileName)), cancellationToken);
    }

    public Task<FileData> GetFileAsync(string fileName, string relativeDirectory, CancellationToken cancellationToken = default)
    {
        return _fileClient.GetAsync(Path.Combine(relativeDirectory, GetFileName(fileName)), cancellationToken);
    }

    public Task<IReadOnlyList<string>> ListFilesAsync(string relativeDirectory, CancellationToken cancellationToken = default)
    {
        return _fileClient.ListAsync(relativeDirectory, cancellationToken);
    }

    private static string GetFileName(string fileName)
    {
        var safeFileName = Path.GetFileName(fileName);
        if (string.IsNullOrWhiteSpace(safeFileName))
        {
            throw new ArgumentException("A file name is required.", nameof(fileName));
        }

        return safeFileName;
    }
}
