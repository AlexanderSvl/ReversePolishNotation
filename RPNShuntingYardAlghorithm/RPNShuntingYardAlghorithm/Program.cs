using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RPNShuntingYardAlghorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Reverse Polish Notation";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("----{ Reverse Polish Notation (w/Shunting Yard Alghorithm) }----");
            Console.WriteLine();

            CalculateExpression(ConvertExpression());
        }

        public static Queue<string> ConvertExpression()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Enter your expression:");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                string[] input = Console.ReadLine().Split();
                Console.ResetColor();

                Dictionary<char, int> operatorsPrecedence = new Dictionary<char, int>()
                {
                    { '+', 1 },
                    { '-', 1 },
                    { '/', 2 },
                    { '*', 2 },
                    { '^', 3 },
                    { '(', 0 },
                    { ')', 0 }
                };

                Stack<char> operators = new Stack<char>();
                Queue<string> output = new Queue<string>();

                foreach (var item in input)
                {
                    int n = 0;

                    if (int.TryParse(item, out n))
                    {
                        output.Enqueue(n.ToString());
                    }
                    else if (char.Parse(item) == '(')
                    {
                        operators.Push(char.Parse(item));
                    }
                    else if (char.Parse(item) == ')')
                    {
                        while (operators.Peek() != '(')
                        {
                            output.Enqueue(operators.Pop().ToString());
                        }

                        operators.Pop();
                    }
                    else
                    {
                        while (operators.Any() && operatorsPrecedence[operators.Peek()] >= operatorsPrecedence[char.Parse(item)])
                        {
                            output.Enqueue(operators.Pop().ToString());
                        }

                        operators.Push(char.Parse(item));
                    }
                }

                while (operators.Any())
                {
                    output.Enqueue(operators.Pop().ToString());
                }

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine();
                Console.WriteLine("Expression in Reverse Polish Notation: ");
                Console.ResetColor();

                string temp = "";

                foreach (var item in output)
                {
                    temp += item;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                string result = String.Join(' ', temp.ToCharArray());
                Console.WriteLine(result);
                Console.WriteLine();
                Console.ResetColor();

                return output;
            }
            catch (Exception exp)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(exp.Message);
                Console.ResetColor();

                return null;
            }

        }

        public static void CalculateExpression(Queue<string> exp)
        {
            Stack<int> stack = new Stack<int>();

            foreach (var item in exp)
            {
                int n;

                if (int.TryParse(item, out n))
                {
                    stack.Push(n);
                }
                else
                {
                    if (item == "+")
                    {
                        stack.Push(stack.Pop() + stack.Pop());
                    }
                    else if (item == "-") 
                    {
                        int n1 = stack.Pop();
                        int n2 = stack.Pop();
                        stack.Push(n2 - n1);
                    }
                    else if(item == "*")
                    {
                        stack.Push(stack.Pop() * stack.Pop());
                    }
                    else if(item == "/")
                    {
                        int n1 = stack.Pop();
                        int n2 = stack.Pop();
                        stack.Push(n2 / n1);
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Result: ");
            Console.WriteLine(stack.Pop());
            Console.ResetColor();
        }
    }
}
