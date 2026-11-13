using MediatR;
using Microsoft.AspNetCore.Http;
using KooliProjekt.Application.Infrastructure.Results;

namespace KooliProjekt.Application.Features.products;

public sealed class AddFileCommand : IRequest<OperationResult<Unit>>
{
    public int ProductId { get; set; }
    public IFormFile File { get; set; }
}