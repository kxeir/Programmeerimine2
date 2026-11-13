using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Infrastructure.Files;

public sealed class LocalFileClient : IFileClient
{
    private readonly string _rootPath;

    public LocalFileClient(string rootPath)
    {
        _rootPath = Path.GetFullPath(rootPath);
        Directory.CreateDirectory(_rootPath);
    }

    public async Task SaveAsync(IFormFile file, string relativePath, CancellationToken cancellationToken = default)
    {
        var path = GetSafePath(relativePath);
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);

        await using var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
        await file.CopyToAsync(stream, cancellationToken);
    }

    public Task DeleteAsync(string relativePath, CancellationToken cancellationToken = default)
    {
        var path = GetSafePath(relativePath);
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        return Task.CompletedTask;
    }

    public Task<FileData> GetAsync(string relativePath, CancellationToken cancellationToken = default)
    {
        var path = GetSafePath(relativePath);
        if (!File.Exists(path))
        {
            return Task.FromResult<FileData>(null);
        }

        var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        return Task.FromResult(new FileData
        {
            Content = stream,
            ContentType = GetContentType(path),
            FileName = Path.GetFileName(path)
        });
    }

    public Task<IReadOnlyList<string>> ListAsync(string relativeDirectory, CancellationToken cancellationToken = default)
    {
        var directory = GetSafePath(relativeDirectory);
        IReadOnlyList<string> files = Directory.Exists(directory)
            ? Directory.EnumerateFiles(directory).Select(Path.GetFileName).ToList()
            : Array.Empty<string>();

        return Task.FromResult(files);
    }

    private string GetSafePath(string relativePath)
    {
        var path = Path.GetFullPath(Path.Combine(_rootPath, relativePath));
        if (!path.StartsWith(_rootPath + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase) &&
            !string.Equals(path, _rootPath, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("The file path is outside the configured file root.");
        }

        return path;
    }

    private static string GetContentType(string path) => Path.GetExtension(path).ToLowerInvariant() switch
    {
        ".jpg" or ".jpeg" => "image/jpeg",
        ".png" => "image/png",
        ".gif" => "image/gif",
        ".pdf" => "application/pdf",
        ".txt" => "text/plain",
        ".json" => "application/json",
        _ => "application/octet-stream"
    };
}
