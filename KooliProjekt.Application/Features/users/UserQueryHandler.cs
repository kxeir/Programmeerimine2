using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.users;

public class UserQueryHandler : IRequestHandler<UserQuery, OperationResult<UserDto>>
{
    private readonly ApplicationDbContext _context;

    public UserQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OperationResult<UserDto>> Handle(
        UserQuery request,
        CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
        {
            return OperationResult<UserDto>.NotFound("User not found.");
        }

        var user = await _context.Users
            .AsNoTracking()
            .Where(u => u.id == request.Id)
            .Select(u => new UserDto
            {
                Id = u.id,
                FirstName = u.firsName,
                LastName = u.lastName,
                Email = u.email,
                PhoneNumber = u.phoneNumber,
                Address = u.address,
                UserName = u.UserName
            })
            .FirstOrDefaultAsync(cancellationToken);

        return user == null
            ? OperationResult<UserDto>.NotFound("User not found.")
            : OperationResult<UserDto>.Success(user);
    }
}
