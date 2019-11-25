using System;
using System.IO;
using System.Threading.Tasks;

namespace IdeagenCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator recursionCalculator = new RecursionCalculator();
            Calculator stackCalculator = new StackCalculator();

            if (File.Exists("sample_expressions.txt"))
            {
                var lines = File.ReadAllLines("sample_expressions.txt");
                foreach (var line in lines)
                {
                    if (String.IsNullOrWhiteSpace(line)) continue;
                    if (line.StartsWith('#'))
                    {
                        Console.WriteLine(line.Substring(1));
                        Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
                        continue;
                    }

                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write($"Recursion : {line} = ");
                        Console.WriteLine(recursionCalculator.Calculate(line));

                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write($"Stack : {line} = ");
                        Console.WriteLine(stackCalculator.Calculate(line));

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            string command = "";
            do
            {
                Console.WriteLine(@"Enter ""STOP"" to stop this program.");
                Console.WriteLine(@"Enter any valid math expressions (delimited by spaces).");

                command = Console.ReadLine();

                if (command != "STOP")
                {
                    try
                    {
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write($"Recursion : {command} = ");
                            Console.WriteLine(recursionCalculator.Calculate(command));

                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write($"Stack : {command} = ");
                            Console.WriteLine(stackCalculator.Calculate(command));

                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

            } while (command != "STOP");

            Console.WriteLine("Ended. Press any key to close this windows.");
            Console.ReadKey();
        }
    }
}
