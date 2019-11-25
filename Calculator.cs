using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdeagenCalculator
{
    public abstract class Calculator
    {
        public const string SUMMATION_OPERATOR = "+";
        public const string SUBTRACTION_OPERATOR = "-";
        public const string MULTIPLICATION_OPERATOR = "*";
        public const string DIVISION_OPERATOR = "/";

        public const string LEFT_PARENTHESES = "(";
        public const string RIGHT_PARENTHESES = ")";

        protected string[] AcceptedOperators = {
            SUMMATION_OPERATOR,
            SUBTRACTION_OPERATOR,
            MULTIPLICATION_OPERATOR,
            DIVISION_OPERATOR };

        public abstract double Calculate(string sum);

        /// <summary>
        /// All of the space delimited values must either be operators or operands.
        /// Operand and operator also must appear one after another, but not in consecutive
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual bool IsExpressionValid(string expression)
        {
            // Empty expressions.
            if (string.IsNullOrWhiteSpace(expression))
            {
                return false;
            }

            var tokens = expression.Trim().Split();
            var brackets = new Stack<string>();

            for (var i = 0; i < tokens.Length; i++)
            {
                var token = tokens[i];
                // IF Any of the token is neither operator nor operand.
                if (!(IsOperand(token) || IsOperator(token) || IsParentheses(token)))
                {
                    return false;
                }

                // Pop into the stack whenever a left parentheses is encountered.
                if (IsLeftParentheses(token))
                {
                    brackets.Push(token);
                }

                // Remove one parentheses if a matching right parentheses is encountered.
                if (IsRightParentheses(token))
                {
                    // Found a right parentheses but no left parenthese found;
                    if (brackets.Count == 0)
                    {
                        return false;
                    }
                    else
                    {
                        brackets.Pop();
                    }
                }

                // Skip the first item
                if (i == 0)
                    continue;

                var prevToken = tokens[i - 1];
                if (IsRightParentheses(token) && IsOperator(prevToken))
                {
                    return false;
                }

                // IF the type of current and previous is same, then this expressions is invalid.
                if ((IsOperator(token) && IsOperator(prevToken)) || (IsOperand(token) && IsOperand(prevToken)))
                {
                    return false;
                }
            }

            // IF every bracket has opening and closing then the stack would be empty
            if (brackets.Count > 0)
            {
                return false;
            }

            return true;
        }

        public virtual double Resolve(double operand1, double operand2, string _operator)
        {
            switch (_operator)
            {
                case SUMMATION_OPERATOR:
                    return operand1 + operand2;

                case SUBTRACTION_OPERATOR:
                    return operand1 - operand2;

                case MULTIPLICATION_OPERATOR:
                    return operand1 * operand2;

                case DIVISION_OPERATOR:
                    return operand1 / operand2;

                default:
                    throw new InvalidExpressionException(ExceptionType.InvalidOperator, "Unsupported operator.");
            }
        }

        /// <summary>
        /// Return true if Operator A has higher precendence than Operator B
        /// </summary>
        /// <param name="operatorA"></param>
        /// <param name="operatorB"></param>
        /// <returns></returns>
        public virtual bool HasHigherPrecendence(string operatorA, string operatorB)
        {
            // Always true as multiply and divide is the highest precendece, regardless of B
            if (operatorA == MULTIPLICATION_OPERATOR || operatorA == DIVISION_OPERATOR)
            {
                return true;
            }

            // IF A & B are both sum/substract, then they are same precendence.
            if ((operatorA == SUMMATION_OPERATOR || operatorA == SUBTRACTION_OPERATOR) && (operatorB == SUMMATION_OPERATOR || operatorB == SUBTRACTION_OPERATOR))
            {
                return true;
            }

            // The rest of the case would be false
            // At this point the operator A is confirmed to be sum/subtract and operator B is confirmed to be multiply/divide.
            return false;
        }

        /// <summary>
        /// Is this string an valid operator?
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual bool IsOperator(string input)
        {
            return AcceptedOperators.Any(oper => oper == input);
        }

        /// <summary>
        /// Is this string an valid operands?
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual bool IsOperand(string input)
        {
            return double.TryParse(input, out double tempVal);
        }

        public virtual bool IsParentheses(string input)
        {
            return input == LEFT_PARENTHESES || input == RIGHT_PARENTHESES;
        }

        public virtual bool IsLeftParentheses(string input)
        {
            return input == LEFT_PARENTHESES;
        }

        public virtual bool IsRightParentheses(string input)
        {
            return input == RIGHT_PARENTHESES;
        }

        public virtual double ToDouble(string input)
        {
            return double.TryParse(input, out double tempVal) ? tempVal : -1;
        }
    }
}
