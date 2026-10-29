using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.orderlines;

public class OrderLineQuery : IRequest<OperationResult<OrderLineDto>>
{
    public int Id { get; set; }
}
