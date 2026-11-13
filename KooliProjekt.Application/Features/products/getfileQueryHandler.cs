using KooliProjekt.Application.Infrastructure.Files;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.products;

public sealed class GetFileQueryHandler : IRequestHandler<GetFileQuery, OperationResult<FileData>>
{
    private readonly FileStore _fileStore;

    public GetFileQueryHandler(FileStore fileStore) { _fileStore = fileStore; }

    public async Task<OperationResult<FileData>> Handle(GetFileQuery request, CancellationToken cancellationToken)
    {
        var file = await _fileStore.GetFileAsync(request.FileName, Path.Combine("Products", request.ProductId.ToString()), cancellationToken);
        return file is null ? OperationResult<FileData>.NotFound("File was not found.") : OperationResult<FileData>.Success(file);
    }
}
