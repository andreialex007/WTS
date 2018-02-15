using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using WTS.BL.Utils;

namespace WTS.BL.Exceptions
{
    public class ValidationException : AppDbCommonException
    {
        public List<DbValidationError> Errors { get; set; }

        public ValidationException()
        {
            Errors = new List<DbValidationError>();
        }

        public ValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public string AllErrors
        {
            get { return string.Join(Environment.NewLine, Errors.Select(x => x.ErrorMessage)); }
        }

        public ValidationException(string validationSummary)
            : this()
        {
            Errors.Add(new DbValidationError(validationSummary));
        }

        protected ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public ValidationException(List<DbValidationError> errors)
        {
            Errors = errors;
        }
    }
}
