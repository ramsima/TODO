
using System;
using System.Collections.Generic;
using System.Text;

namespace TODO.APPLICATION.Common.Exceptions
{
    public class DomainException : Exception
    {
        public string ErrorCode { get; set; }
        public DomainException(string errorCode,string message) : base(message) 
        {
            ErrorCode = errorCode;
        }
    }

    public class NotFoundException : DomainException
    {
        public NotFoundException(string message) : base("NOT_FOUND", message) { }
    }

    public class ForbiddenException : DomainException
    {
        public ForbiddenException(string message) : base("FORBIDDEN", message) { }
    }

    public class ConflictException : DomainException
    {
        public ConflictException(string message) : base("CONFLICT", message) { }

    }

    public class BadRequestException : DomainException
    {
        public BadRequestException(string message) : base("BAD_REQUEST", message) { }
    }
    public class DuplicateFieldException : DomainException
    {
        public DuplicateFieldException(string message) : base("DUPLICATE_FIELD", message) { }

    }
}