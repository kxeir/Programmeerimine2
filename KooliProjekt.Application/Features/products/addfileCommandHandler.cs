using KooliProjekt.Application.Infrastructure.Files;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.products
{
    public sealed class AddFileCommandHandler : IRequestHandler<AddFileCommand, OperationResult<Unit>>
    {
        private readonly FileStore _fileStore;

        public AddFileCommandHandler(FileStore fileStore)
        {
            _fileStore = fileStore;
        }

        public async Task<OperationResult<Unit>> Handle(AddFileCommand request, CancellationToken cancellationToken)
        {
            var subFolder = Path.Combine("Products", request.ProductId.ToString());
            await _fileStore.SaveFileAsync(request.File, subFolder, cancellationToken);
            return OperationResult<Unit>.Success(Unit.Value);
        }
    }
}