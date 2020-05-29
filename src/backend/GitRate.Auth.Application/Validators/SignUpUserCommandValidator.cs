using Auth.Application.Commands;
using FluentValidation;
using GitRate.Auth.Persistence;
using GitRate.Common.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Auth.Application.Validators
{
    public class SignUpUserCommandValidator : AbstractValidator<SignUpUserCommand>
    {
        public SignUpUserCommandValidator(AuthContext context, IOptions<IdentityOptions> identityOptions)
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("UserName cannot be empty.")
                .NotNull()
                .WithMessage("Please provide UserName.");

            RuleFor(x => x.UserName)
                .MustAsync(async (val, token) => !await context.Users.AnyAsync(x => x.NormalizedUserName == val.ToUpper()))
                .WithMessage(x => $"UserName: {x.UserName} is already taken.")
                .When(model => !string.IsNullOrEmpty(model.UserName));

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email cannot be empty.")
                .NotNull()
                .WithMessage("Please provide Email.")
                .Must(emailVal => emailVal.IsEmail())
                .WithMessage(x => $"Email: {x.Email} is not in correct format.");

            RuleFor(x => x.Email)
                .MustAsync(async (val, token) => !await context.Users.AnyAsync(x => x.NormalizedEmail == val.ToUpper()))
                .WithMessage(x => $"Email: {x.Email} is already taken.")
                .When(model => !string.IsNullOrEmpty(model.Email));

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password cannot be empty.")
                .NotNull()
                .WithMessage("Please provide Password.")
                .Must(x => !(x.Length < identityOptions.Value.Password.RequiredLength))
                .WithMessage($"Password must be at least {identityOptions.Value.Password.RequiredLength} characters.");
        }
    }
}