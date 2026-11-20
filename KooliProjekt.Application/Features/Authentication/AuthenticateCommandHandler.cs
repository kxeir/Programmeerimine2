using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Authentication;
using KooliProjekt.Application.Authentication.Jwt;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Authentication;

public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, OperationResult<string>>
{
    private readonly ApplicationDbContext _context;
    private readonly JwtTokenBuilder _tokenBuilder;

    public AuthenticateCommandHandler(ApplicationDbContext context, JwtTokenBuilder tokenBuilder)
    {
        _context = context;
        _tokenBuilder = tokenBuilder;
    }

    public async Task<OperationResult<string>> Handle(
        AuthenticateCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .SingleOrDefaultAsync(u => u.UserName == request.UserName, cancellationToken);

        if (user == null || !PasswordHelper.Verify(request.Password, user.password))
        {
            return new OperationResult<string>().AddError("Invalid username or password.");
        }

        return OperationResult<string>.Success(_tokenBuilder.GenerateToken(user));
    }
}
