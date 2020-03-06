using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        const int MaxJokes = 9;

        static void Main(string[] args)
        {
            Console.WriteLine("Press ? to get instructions.");
            while (true)
            {
                char key = Console.ReadKey().KeyChar;
                if (key == '?')
                {
                    Console.WriteLine();
                    Console.WriteLine("Press c to get categories");
                    Console.WriteLine("Press r to get random jokes");

                }   
                if (key == 'c')
                {
                    PrintResults(GetCategories());
                }
                if (key == 'r')
                {
                    Tuple<string, string> name;
                    Console.WriteLine();
                    Console.WriteLine("Want to use a random name? y/n");
                    key = Console.ReadKey().KeyChar;
                    if (key == 'y')
                    {
                        name = GetRandomName();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Enter a first name;");
                        String firstName = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine("Enter a surname;");
                        String lastName = Console.ReadLine();
                        name = Tuple.Create(firstName, lastName);
                    }
                    String category = null;
                    Console.WriteLine();
                    Console.WriteLine("Want to specify a category? y/n");
                    key = Console.ReadKey().KeyChar;
                    if (key == 'y')
                    {
                        Console.WriteLine();
                        Console.WriteLine("Enter a category;");
                        category = Console.ReadLine();
                    }
                    int numJokes = 0;
                    while (true)
                    {
                        Console.WriteLine();
                        Console.WriteLine("How many jokes do you want? (1-9)");
                        
                        try
                        {
                            numJokes = Int32.Parse(Console.ReadLine());
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Please enter a number between 1-9");
                            continue;
                        }

                        if (numJokes > MaxJokes)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Please enter a number between 1-9");
                            continue;
                        }

                        break;
                    }

                    PrintResults(GetRandomJokes(category, name, numJokes));
                }
            }

        }

        private static void PrintResults(String[] results)
        {
            Console.WriteLine();
            Console.WriteLine("[" + string.Join(",", results) + "]");
        }

        /// <summary>
        /// Retrieves random chuck norris jokes
        /// </summary>
        /// <returns>a string array of jokes</returns>
        private static String[] GetRandomJokes(string category, Tuple<string, string> name, int number)
        {
            return new JsonFeed("https://api.chucknorris.io").GetRandomJokes(name?.Item1, name?.Item2, category, number);
        }

        /// <summary>
        /// Retrieves available joke categories
        /// </summary>
        /// <returns>a string array of categories</returns>
        private static String[] GetCategories()
        {
            return new JsonFeed("https://api.chucknorris.io").GetCategories();
        }

        /// <summary>
        /// Retrieves random name
        /// </summary>
        /// <returns>a tuple of the random name</returns>
        private static Tuple<string, string> GetRandomName()
        {
            dynamic result = new JsonFeed("http://uinames.com/api/").Getname();
            return Tuple.Create(result.name.ToString(), result.surname.ToString());
        }
    }
}
