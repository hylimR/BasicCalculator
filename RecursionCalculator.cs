using System;
using System.Collections.Generic;
using System.Linq;

namespace IdeagenCalculator
{
    public class RecursionCalculator : Calculator
    {
        public override double Calculate(string expression)
        {
            if (!IsExpressionValid(expression))
            {
                throw new InvalidExpressionException(ExceptionType.InvalidExpression, "Invalid expression.");
            }

            var tokens = expression.Split().ToList();
            // return the value as it is.
            if (tokens.Count == 1)
            {
                return double.TryParse(tokens[0], out var result) ? result : 0;
            }

            // Simple arithmetric will just end here.
            if (tokens.Count == 3)
            {
                return Resolve(ToDouble(tokens[0]), ToDouble(tokens[2]), tokens[1]);
            }

            // Perform all the bracket operation first.
            tokens = PerformBracketArithmetric(tokens);

            // Then follow by multiplication and division.
            tokens = PerformArithmetric(tokens, MULTIPLICATION_OPERATOR, DIVISION_OPERATOR);

            // Then follow by addition and substraction.
            tokens = PerformArithmetric(tokens, SUMMATION_OPERATOR, SUBTRACTION_OPERATOR);

            // At this point the tokens list should only have 1 item left, and the last item is the result.
            return ToDouble(tokens.First());
        }

        /// <summary>
        /// Resolve bracketed operation.
        /// </summary>
        /// <param name="tokens">Tokenized expression</param>
        /// <returns>Tokenized expression after the operation</returns>
        private List<string> PerformBracketArithmetric(List<string> tokens) 
        {
            var subExpressionBody = new List<string>();

            // Keep track on how many opening bracket along the way, so we can find correct closing bracket.
            int leftParenthesesCount = 0;

            bool foundOpeningBracket = false;

            for (var i = 0; i < tokens.Count; i++)
            {
                // Skip when empty which is possible after the operation in the nested loop.
                if (String.IsNullOrEmpty(tokens[i])) continue;

                // When a bracket opening is found, start a nested loop to search for the closing bracket.
                if (tokens[i] == LEFT_PARENTHESES && !foundOpeningBracket)
                {
                    // To know we actually want to start extract value now
                    foundOpeningBracket = true;
                    // Remove the left parentheses.
                    tokens[i] = String.Empty;

                    // Move the next iteration
                    continue;
                }

                // Increment when found a opening bracket.
                if (tokens[i] == LEFT_PARENTHESES && foundOpeningBracket)
                {
                    leftParenthesesCount++;
                }

                // When a closing bracket is found
                if (tokens[i] == RIGHT_PARENTHESES && foundOpeningBracket)
                {
                    // See if we have any inner bracket.
                    // If yes, decrement the count and continue the search.
                    if (leftParenthesesCount > 0)
                    {
                        leftParenthesesCount--;
                    }
                    // Otherwise, mean this is the correct closing bracket
                    // Create a subexpression by removing the opening and closing bracket, and recall this method through recursion.
                    // Also break out of the nested loop to continue searching for next outer opening bracket.
                    else
                    {
                        var subExpression = String.Join(" ", subExpressionBody);
                        var subOperationResult = Calculate(subExpression).ToString();

                        tokens[i] = subOperationResult;

                        // Reset these
                        foundOpeningBracket = false;
                        leftParenthesesCount = 0;
                        subExpressionBody.Clear();
                        continue;
                    }
                }

                if (foundOpeningBracket)
                {
                    // Push all the items into the list along the way.
                    subExpressionBody.Add(tokens[i]);

                    // Also set ALL the item along the way to empty so we could clear them later.
                    tokens[i] = String.Empty;
                }
            }

            return RemoveEmptyString(tokens);
        }

        /// <summary>
        /// Resolve sum, substract, multiply, and divide.
        /// </summary>
        /// <param name="tokens">Tokenized expression</param>
        /// <param name="operators">The operator to resolve</param>
        /// <returns>Tokenized expression after the operation</returns>
        private List<string> PerformArithmetric(List<string> tokens, params string[] operators)
        {
            var indexes = FindIndex(tokens, operators);
            for (var i = 0; i < indexes.Count(); i++)
            {
                var index = indexes[i];
                var subExpression = String.Join(" ", new List<string>() { tokens[index - 1], tokens[index], tokens[index + 1] });
                var subOperationResult = Calculate(subExpression).ToString();

                tokens[index + 1] = subOperationResult;
                tokens[index - 1] = String.Empty;
                tokens[index] = String.Empty;
            }

            return RemoveEmptyString(tokens);
        }

        /// <summary>
        /// Finding all the index(s) in the expressions
        /// </summary>
        /// <param name="tokens">Tokenized expression</param>
        /// <param name="operators">Operator to look for</param>
        /// <returns></returns>
        private List<int> FindIndex(IEnumerable<string> tokens, params string[] operators)
        {
            return tokens.Select((token, index) => new { token, index })
                         .Where(x => operators.Any(oper => oper == x.token))
                         .Select(x => x.index)
                         .ToList();
        }

        /// <summary>
        /// Clean up all empty token in the expression.
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        private List<string> RemoveEmptyString(IEnumerable<string> tokens)
        {
            return tokens.Where(token => !String.IsNullOrEmpty(token)).ToList();
        }
    }
}
