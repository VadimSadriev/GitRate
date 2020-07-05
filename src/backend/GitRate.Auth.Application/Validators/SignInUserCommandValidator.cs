using Auth.Application.Commands;
using FluentValidation;

namespace Auth.Application.Validators
{
    /// <summary>
    /// Validates <see cref="SignInUserCommand"/>
    /// </summary>
    public class SignInUserCommandValidator : AbstractValidator<SignInUserCommand>
    {
        /// <summary>
        /// Validates <see cref="SignInUserCommand"/>
        /// </summary>
        public SignInUserCommandValidator()
        {
            RuleFor(x => x.UserNameOrEmail)
                .NotEmpty()
                .WithMessage("UserName or Email cannot be empty.")
                .NotNull()
                .WithMessage("Please, provide UserName or Email.");
            
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password cannot be empty.")
                .NotNull()
                .WithMessage("Please, provide Password.");
        }
    }
}