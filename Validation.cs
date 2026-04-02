using System;
using System.Runtime.CompilerServices;

namespace Project_I_Todo_list
{

    public static class Validation
    {

        // Gets a atring from the console and validates it so its not empty.
        public static string GetValidatedStringFromConsole(string variableName)
        {
            Console.Write("\n Enter a " + variableName + ": ");
            string result = Console.ReadLine();

            while (String.IsNullOrEmpty(result))
            {
                ColoredText.WriteLine(" " + variableName + " can't be an empty string", ConsoleColor.Red);
                Console.Write(" Enter a " + variableName + ": ");
                result = Console.ReadLine();
            }

            return result;
        }

        // Gets an integer from the console and validates it so its not empty and only contains digits, and also checks if the number is between min and max.
        public static int GetValidatedIntFromConsole(string variableName, int min, int max)
        {
            bool isValidInteger;
            int index;
            do
            {
                Console.Write("\n Enter a " + variableName + ": ");
                isValidInteger = int.TryParse(Console.ReadLine(), out index);

                if (isValidInteger == false)
                {
                    ColoredText.WriteLine(" " + variableName + " can only contain digits and can't be empty.", ConsoleColor.Red);
                }
                else if (index < min || index > max)
                {
                    ColoredText.WriteLine(" " + variableName + " must be non-negative and higher than zero and lower than " + (max + 1) + ".", ConsoleColor.Red);
                    isValidInteger = false;
                }
            } while (isValidInteger == false);

            return index;
        }

        // Thera are two cases, in the first case NullOrEmpty is allowed, in the second case its treated as an error.
        public static string GetValidatedDateFromConsole(bool checkNullOrEmpty)
        {
            bool isDate;
            string result;
            do
            {
                Console.Write("\n Enter a new Due date: ");
                result = Console.ReadLine();

                if (!checkNullOrEmpty && String.IsNullOrEmpty(result))
                {
                    return null;
                }

                else if (String.IsNullOrEmpty(result))
                {
                    ColoredText.WriteLine(" You have entered an empty string for date.", ConsoleColor.Red);
                    isDate = false;
                }
                else
                {
                    isDate = DateTime.TryParse(result, out DateTime dueDate);

                    if (isDate == false)
                    {
                        ColoredText.WriteLine(" You have not entered a valid date.", ConsoleColor.Red);
                    }
                }

            } while (isDate == false);

            return result;
        }

        // Thera are two cases, in the first case NullOrEmpty is allowed, in the second case its treated as an error.
        public static string GetValidatedStatusFromConsole(bool checkNullOrEmpty)
        {
            string result;
            bool endLoop = false;
            do
            {
                Console.Write("\n Enter a new Status: ");
                result = Console.ReadLine();

                if (!checkNullOrEmpty && String.IsNullOrEmpty(result))
                {
                    endLoop = true;
                }
                else if (result == "Not finished" || result == "Finished")
                {
                    endLoop = true;
                }
                else if (String.IsNullOrEmpty(result))
                {
                    ColoredText.WriteLine(" You have entered an empty string. Please enter a status", ConsoleColor.Red);
                }
                else
                {
                    ColoredText.WriteLine(" You have not entered a valid status. Please enter \"Not finished\" or \"Finished\"", ConsoleColor.Red);
                }

            } while (!endLoop);

            return result;
        }
    }
}
