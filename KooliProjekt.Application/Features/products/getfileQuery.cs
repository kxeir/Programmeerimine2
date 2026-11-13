using KooliProjekt.Application.Infrastructure.Files;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.products;

public sealed class GetFileQuery : IRequest<OperationResult<FileData>>
{
    public int ProductId { get; set; }
    public string FileName { get; set; }
}
