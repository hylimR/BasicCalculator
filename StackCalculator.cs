using System;
using System.Collections.Generic;
using System.Text;

namespace IdeagenCalculator
{
    public class StackCalculator : Calculator
    {
        private Stack<string> Operands { get; set; } = new Stack<string>();
        private Stack<string> Operators { get; set; } = new Stack<string>();

        public override double Calculate(string expression)
        {
            if (!IsExpressionValid(expression))
            {
                throw new InvalidExpressionException(ExceptionType.InvalidExpression, "Invalid expression.");
            }

            var tokens = expression.Trim().Split();
            string oprs,        // Poped operator
                   oprd1,       // Operand 1 to be resolved
                   oprd2;       // Operand 2 to be resolved
            double result;

            foreach (var token in tokens)
            {
                if (token == LEFT_PARENTHESES)
                {
                    Operators.Push(token);
                }

                else if (token == RIGHT_PARENTHESES)
                {
                    while (Operators.Peek() != LEFT_PARENTHESES)
                    {
                        oprs = Operators.Pop();
                        oprd2 = Operands.Pop();
                        oprd1 = Operands.Pop();

                        result = Resolve(ToDouble(oprd1), ToDouble(oprd2), oprs);
                        Operands.Push(result.ToString());
                    }

                    Operators.Pop();
                }

                else if (IsOperand(token))
                {
                    Operands.Push(token);
                }

                else if (IsOperator(token))
                {
                    while (Operators.Count > 0 && HasHigherPrecendence(Operators.Peek(), token))
                    {
                        oprs = Operators.Pop();
                        oprd2 = Operands.Pop();
                        oprd1 = Operands.Pop();

                        result = Resolve(ToDouble(oprd1), ToDouble(oprd2), oprs);
                        Operands.Push(result.ToString());
                    }

                    Operators.Push(token);
                }
            }

            while (Operators.Count > 0)
            {
                oprs = Operators.Pop();
                oprd2 = Operands.Pop();
                oprd1 = Operands.Pop();

                result = Resolve(ToDouble(oprd1), ToDouble(oprd2), oprs);
                Operands.Push(result.ToString());
            }

            return ToDouble(Operands.Pop());
        }
    }
}
