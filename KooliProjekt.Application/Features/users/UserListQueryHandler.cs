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

public class UserListQueryHandler : IRequestHandler<UserListQuery, OperationResult<PagedResult<UserDto>>>
{
    private readonly ApplicationDbContext _context;

    public UserListQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OperationResult<PagedResult<UserDto>>> Handle(
        UserListQuery request,
        CancellationToken cancellationToken)
    {
        var page = request.Page < 1 ? 1 : request.Page;
        var pageSize = request.PageSize < 1 ? 10 : request.PageSize;
        var query = _context.Users.AsNoTracking();
        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(u => u.id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
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
            .ToListAsync(cancellationToken);

        var result = new PagedResult<UserDto>
        {
            Results = items,
            CurrentPage = page,
            PageSize = pageSize,
            RowCount = totalCount
        };

        return OperationResult<PagedResult<UserDto>>.Success(result);
    }
}
