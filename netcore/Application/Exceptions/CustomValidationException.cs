using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Application.Interfaces.IExceptions;
using FluentValidation.Results;

namespace Application.Exceptions
{
    /// <summary>
    /// Fluent validation exception
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// COnstructor
        /// </summary>
        public ValidationException()
            : base("One or more")
        {
            Failures = new Dictionary<string, string[]>();
        }

        /// <summary>
        /// Constructor with parameter
        /// </summary>
        /// <param name="failures"></param>
        public ValidationException(IList<ValidationFailure> failures)
            : this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                Failures.Add(propertyName, propertyFailures);
            }
        }

        /// <summary>
        /// List of errors
        /// </summary>
        /// <value></value>
        public IDictionary<string, string[]> Failures { get; }
    }
}