using System.Threading;
using System.Threading.Tasks;
using Auth.Application.Commands;
using Auth.Persistence;
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
        
        public Task<Unit> Handle(SignUpUserCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}