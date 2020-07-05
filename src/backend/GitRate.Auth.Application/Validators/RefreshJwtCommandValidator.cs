using Auth.Application.Commands;
using FluentValidation;

namespace Auth.Application.Validators
{
    /// <summary>
    /// Validates <see cref="RefreshJwtCommand"/>
    /// </summary>
    public class RefreshJwtCommandValidator : AbstractValidator<RefreshJwtCommand>
    {
        /// <summary>
        /// Validates <see cref="RefreshJwtCommand"/>
        /// </summary>
        public RefreshJwtCommandValidator()
        {
            RuleFor(x => x.Jwt)
                .NotNull()
                .WithMessage("Please, provide jwt.")
                .NotEmpty()
                .WithMessage("Jwt cannot be empty.");

            RuleFor(x => x.RefreshToken)
                .NotNull()
                .WithMessage("Please, provide refresh token.")
                .NotEmpty()
                .WithMessage("Refresh token cannot be empty.");
        }
    }
}