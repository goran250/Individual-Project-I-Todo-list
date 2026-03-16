using System.Text.Json;

namespace TaskMaster
{
    static internal class FileHandler
    {
        static string fileName = string.Format(@"{0}\ToDoList.json", Environment.CurrentDirectory);

        public static void Save()
        {
            //Tries to save the list in a JSON file
            try
            {
                string jsonString = JsonSerializer.Serialize(TaskList.tasks);
                File.WriteAllText(fileName, jsonString);
                ColoredText.WriteLine("Your To-do list has been saved", ConsoleColor.Green);
            }
            catch (Exception)
            {
                ColoredText.WriteLine("Filed to save To-Do List!", ConsoleColor.Red);
            }

        }

        public static void Open()
        {
            //If a file exists, load it. Otherwise, create a sample list of tasks.
            if (File.Exists(fileName)) Load();
            else CreateFirstTimeList();
        }
        public static void Load()
        {
            //Try to load a list
            try
            {
                string jsonString = File.ReadAllText(fileName);
                TaskList.tasks = JsonSerializer.Deserialize<List<Task>>(jsonString);
                ColoredText.WriteLine("Opened your saved list\n", ConsoleColor.Green);
            }
            catch (Exception)
            {
                ColoredText.WriteLine("Failed to open saved To-Do List\n", ConsoleColor.Red);
            }
        }

        public static void CreateFirstTimeList()
        {
            //Create a populated list of tasks
            TaskList.tasks =
            [
                new Task("Do dishes", "Chores", "20/2", false),
                new Task("Take out trash", "Chores", "22/2", true),
                new Task("Read a book", "", "25/3", false),
                new Task("Mini Project", "C# .NET", "16/2", true),
                new Task("Individual Project", "C# .NET", "22/2", true),
                new Task("HTML & CSS", "C# .NET", "25/2", false)
            ];

            ColoredText.WriteLine("Did not find a saved list to load. Created a sample list with some tasks\n", ConsoleColor.Yellow);
        }
    }
}
