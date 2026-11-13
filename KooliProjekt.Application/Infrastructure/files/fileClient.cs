using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Infrastructure.Files;

public interface IFileClient
{
    Task SaveAsync(IFormFile file, string relativePath, CancellationToken cancellationToken = default);
    Task DeleteAsync(string relativePath, CancellationToken cancellationToken = default);
    Task<FileData> GetAsync(string relativePath, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<string>> ListAsync(string relativeDirectory, CancellationToken cancellationToken = default);
}

public sealed class FileData
{
    public Stream Content { get; init; }
    public string ContentType { get; init; }
    public string FileName { get; init; }
}
