using Auth.Application.Commands;
using FluentValidation;
using GitRate.Auth.Persistence;
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
                .MustAsync(async (val, token) => !await context.Users.AnyAsync(x => x.NormalizedUserName == val.ToUpper()))
                .WithMessage(x => $"UserName: {x.UserName} already taken.");

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage(x => $"Email: {x.Email} is not in correct format.")
                .MustAsync(async (val, token) => !await context.Users.AnyAsync(x => x.NormalizedEmail == val.ToUpper()))
                .WithMessage(x => $"Email: {x.Email} is already taken.");

            RuleFor(x => x.Password)
                .Must(x => x.Length < identityOptions.Value.Password.RequiredLength)
                .WithMessage($"Password must be at least {identityOptions.Value.Password.RequiredLength} characters.");
        }
    }
}