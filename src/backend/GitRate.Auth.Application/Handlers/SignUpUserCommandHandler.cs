using System;
using System.Threading;
using System.Threading.Tasks;
using Auth.Application.Commands;
using GitRate.Auth.Persistence;
using GitRate.Common.Identity.Types;
using MediatR;

namespace Auth.Application.Handlers
{
    public class SignUpUserCommandHandler : IRequestHandler<SignUpUserCommand>
    {
        private AuthContext _context;
        private IUserManager _userManager;
        
        public SignUpUserCommandHandler(AuthContext context, IUserManager userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        public async Task<Unit> Handle(SignUpUserCommand request, CancellationToken cancellationToken)
        {
            var userId = await _userManager.CreateAsync(request.UserName, request.Email, request.Password);
            
            throw new NotImplementedException();
        }
    }
}