using System.Text.RegularExpressions;
using FluentValidation.Validators;

namespace GitRate.Common.Extensions
{
    public static class StringExtensions
    {
        private static readonly Regex _emailRegex;
        
        static StringExtensions()
        {
            var emailValidator = new EmailValidator();
            
            _emailRegex = new Regex(emailValidator.Expression);
        }

        public static bool IsEmail(this string str)
        {
            return _emailRegex.IsMatch(str);
        }
    }
}