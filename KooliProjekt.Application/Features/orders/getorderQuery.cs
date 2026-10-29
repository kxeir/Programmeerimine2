using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Orders;

public class GetOrderQuery : IRequest<OperationResult<OrderDto>>
{
    public int Id { get; set; }
}