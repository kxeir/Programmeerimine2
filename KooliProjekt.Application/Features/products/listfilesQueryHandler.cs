using KooliProjekt.Application.Infrastructure.Files;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.products;

public sealed class ListFilesQueryHandler : IRequestHandler<ListFilesQuery, OperationResult<IReadOnlyList<string>>>
{
    private readonly FileStore _fileStore;

    public ListFilesQueryHandler(FileStore fileStore) { _fileStore = fileStore; }

    public async Task<OperationResult<IReadOnlyList<string>>> Handle(ListFilesQuery request, CancellationToken cancellationToken)
    {
        var files = await _fileStore.ListFilesAsync(Path.Combine("Products", request.ProductId.ToString()), cancellationToken);
        return OperationResult<IReadOnlyList<string>>.Success(files);
    }
}
