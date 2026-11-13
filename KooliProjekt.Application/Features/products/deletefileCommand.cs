using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.products;

public sealed class DeleteFileCommand : IRequest<OperationResult<Unit>>
{
    public int ProductId { get; set; }
    public string FileName { get; set; }
}
