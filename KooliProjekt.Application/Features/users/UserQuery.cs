using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.users;

public class UserQuery : IRequest<OperationResult<UserDto>>
{
    public int Id { get; set; }
}
