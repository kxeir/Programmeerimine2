using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.products;

public class GetProductQuery : IRequest<OperationResult<ProductDto>>
{
    public int Id { get; set; }
}