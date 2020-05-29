using System.Threading;
using System.Threading.Tasks;
using Auth.Application.Commands;
using Auth.Application.Dto;
using GitRate.Common.Authentication;
using GitRate.Common.Identity.Types;
using MediatR;

namespace Auth.Application.Handlers
{
    public class RefreshJwtCommandHandler : IRequestHandler<RefreshJwtCommand, RefreshJwtResultDto>
    {
        private readonly IUserManager _userManager;
        private readonly IJwtService _jwtService;
        
        public RefreshJwtCommandHandler(IUserManager userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<RefreshJwtResultDto> Handle(RefreshJwtCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}