
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Project_I_Todo_list
{
    internal class TodoList
    {
        private static ProjectHandler projectHandler {  get; set; }

        static void Main(string[] args) 
        {
            projectHandler = new ProjectHandler();
            Start();
        }
        
        private static void Start()
        {
            // nbrOf contains three values. The first value is nbr of projects and the second value is nbr of
            // not done tasks, the third value is nbr of done tasks.
            List<int> nbrOf = projectHandler.GetNbrOfProjectsAndTasksStatus();

            Console.SetWindowSize(150, 50);

            ColoredText.WriteLine("\n Welcome to this TodoList app", ConsoleColor.Yellow);
            Console.WriteLine("\n You have " + nbrOf[0].ToString() + " projects and a total nbr of " + nbrOf[1].ToString() + " tasks"); 
            Console.WriteLine(" to be done, and " + nbrOf[2].ToString() + " tasks that are done.");

            ShowMenu();
        }
        private static void ShowMenu()
        {
            ColoredText.WriteLine("\n Pick an option:", ConsoleColor.Yellow);

            Console.Write("\n (");
            ColoredText.Write("1", ConsoleColor.Yellow);
            Console.Write(") Show Task list, sorted by project or due date.");

            Console.Write("\n (");
            ColoredText.Write("2", ConsoleColor.Yellow);
            Console.Write(") Add a new project.");

            Console.Write("\n (");
            ColoredText.Write("3", ConsoleColor.Yellow);
            Console.Write(") Edit a project.");
            Console.Write("\n (");
            ColoredText.Write("4", ConsoleColor.Yellow);
            Console.Write(") Remove a project");

            Console.Write("\n (");
            ColoredText.Write("5", ConsoleColor.Yellow);
            Console.Write(") Add a new Task.");

            Console.Write("\n (");
            ColoredText.Write("6", ConsoleColor.Yellow);
            Console.Write(") Edit a Task (update or mark as done.");

            Console.Write("\n (");
            ColoredText.Write("7", ConsoleColor.Yellow);
            Console.Write(") Remove a task.");

            Console.Write("\n (");
            ColoredText.Write("8", ConsoleColor.Yellow);
            Console.Write(") Save and Quit.");

            Navigate();
        }
        private static void Navigate()
        {
            Console.Write("\n ");
            int min = 1;
            int max = 8;
            int answer = projectHandler.GetValidatedIntFromConsole("Number", min, max);

            switch (answer)
            { 
                case 1:
                    projectHandler.ShowTasks();
                    break;
                case 2:
                    projectHandler.AddNewProject();
                    break;
                case 3:
                    projectHandler.EditAProject();
                    break;
                case 4:
                    projectHandler.RemoveAProject();
                    break;
                case 5:
                    projectHandler.AddNewTask();
                    break;
                case 6:                    
                    projectHandler.EditATask();
                    break;
                case 7:
                    projectHandler.RemoveATask();
                    break;
                case 8:
                    projectHandler.SaveFile();
                    System.Environment.Exit(0);
                    break;
            }
            
            ShowMenu();
        }
    }
}
