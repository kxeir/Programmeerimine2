using KooliProjekt.Application.Infrastructure.Files;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.products;

public sealed class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, OperationResult<Unit>>
{
    private readonly FileStore _fileStore;

    public DeleteFileCommandHandler(FileStore fileStore) { _fileStore = fileStore; }

    public async Task<OperationResult<Unit>> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
    {
        await _fileStore.DeleteFileAsync(request.FileName, Path.Combine("Products", request.ProductId.ToString()), cancellationToken);
        return OperationResult<Unit>.Success(Unit.Value);
    }
}
