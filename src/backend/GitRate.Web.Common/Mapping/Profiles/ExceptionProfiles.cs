using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using GitRate.Common.Exceptions;
using GitRate.Web.Common.Contracts.Exception;

namespace GitRate.Web.Common.Mapping.Profiles
{
    /// <summary>
    /// Mapping profile for <see cref="Exception"/>
    /// </summary>
    public class ExceptionProfiles : Profile
    {
        /// <summary>
        /// Mapping profile for <see cref="Exception"/>
        /// </summary>
        public ExceptionProfiles()
        {
            CreateMap<Exception, ExceptionContract>()
                .ForMember(x => x.Errors,
                    options => options.MapFrom(x => MapError(x)));

            CreateMap<ValidationException, ExceptionContract>();

            CreateMap<ValidationFailure, ExceptionErrorContract>()
                .ForMember(x => x.Type, opt => opt.MapFrom(x => ExceptionTypes.VALIDATION_ERROR))
                .ForMember(x => x.Message, opt => opt.MapFrom(x => x.ErrorMessage));
        }

        private IEnumerable<ExceptionErrorContract> MapError(Exception ex)
        {
            var srcEx = ex;

            while (srcEx != null)
            {
                var type = ex is AppException appException
                    ? AppException.Type
                    : ExceptionTypes.DOMAIN_ERROR;

                yield return new ExceptionErrorContract { Type = type, Message = ex.Message };
            }
        }
    }
}