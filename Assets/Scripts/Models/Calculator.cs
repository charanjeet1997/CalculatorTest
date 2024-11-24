using System;
using System.Collections.Generic;
using UnityEngine;

namespace CalculatorSystem
{
    public class Calculator : ICalculator
    {
        public string EvaluateExpression(string expression)
        {
            try
            {
                return Evaluate(expression).ToString();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error evaluating expression: {ex.Message}");
                return "Error";
            }
        }

        public string Clear()
        {
            return "";
        }

        public string Reset()
        {
            return "";
        }

        private float Evaluate(string expression)
        {
            var tokens = Tokenize(expression);
            var postfix = ConvertToPostfix(tokens);
            return EvaluatePostfix(postfix);
        }

        private List<string> Tokenize(string expression)
        {
            List<string> tokens = new List<string>();
            string number = "";
            foreach (char ch in expression)
            {
                if (char.IsDigit(ch) || ch == '.')
                {
                    number += ch;
                }
                else
                {
                    if (!string.IsNullOrEmpty(number))
                    {
                        tokens.Add(number);
                        number = "";
                    }

                    tokens.Add(ch.ToString());
                }
            }

            if (!string.IsNullOrEmpty(number)) tokens.Add(number);
            return tokens;
        }

        private Queue<string> ConvertToPostfix(List<string> tokens)
        {
            Dictionary<string, int> precedence = new Dictionary<string, int>
            {
                { "+", 1 }, { "-", 1 },
                { "X", 2 }, { "/", 2 }, { "%", 2 }
            };

            Queue<string> output = new Queue<string>();
            Stack<string> operators = new Stack<string>();

            foreach (var token in tokens)
            {
                if (float.TryParse(token, out _))
                {
                    output.Enqueue(token);
                }
                else
                {
                    while (operators.Count > 0 && precedence[operators.Peek()] >= precedence[token])
                    {
                        output.Enqueue(operators.Pop());
                    }

                    operators.Push(token);
                }
            }

            while (operators.Count > 0) output.Enqueue(operators.Pop());
            return output;
        }

        private float EvaluatePostfix(Queue<string> postfix)
        {
            Stack<float> stack = new Stack<float>();
            while (postfix.Count > 0)
            {
                var token = postfix.Dequeue();
                if (float.TryParse(token, out var number))
                {
                    stack.Push(number);
                }
                else
                {
                    var b = stack.Pop();
                    var a = stack.Pop();
                    switch (token)
                    {
                        case "+":
                            stack.Push(a + b);
                            break;
                        case "-":
                            stack.Push(a - b);
                            break;
                        case "X":
                            stack.Push(a * b);
                            break;
                        case "/":
                            stack.Push(a / b);
                            break;
                        case "%":
                            stack.Push(a % b);
                            break;
                    }
                }
            }

            return stack.Pop();
        }
    }
}
