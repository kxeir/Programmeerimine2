using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System.Collections.Generic;

namespace KooliProjekt.Application.Features.products;

public sealed class ListFilesQuery : IRequest<OperationResult<IReadOnlyList<string>>>
{
    public int ProductId { get; set; }
}
