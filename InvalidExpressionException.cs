using System;
using System.Collections.Generic;
using System.Text;

namespace IdeagenCalculator
{
    public enum ExceptionType
    {
        InvalidOperator,
        InvalidExpression
    }

    public class InvalidExpressionException : Exception
    {
        public ExceptionType ExceptionType { get; protected set; }

        public InvalidExpressionException(ExceptionType exceptionType,  string message) : base(message)
        {
            ExceptionType = exceptionType;
        }
    }
}
