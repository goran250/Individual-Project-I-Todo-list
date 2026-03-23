
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

            ColoredText.WriteLine("\n Welcome to this TodoList app", ConsoleColor.Yellow);
            Console.WriteLine("\n You have " + nbrOf[0].ToString() + " projects and a total nbr of " + nbrOf[1].ToString() + " tasks"); 
            Console.WriteLine(" to be done, and " + nbrOf[2].ToString() + " tasks that are done.");

            ShowMenu();
        }
        private static void ShowMenu()
        {
            Console.WriteLine("\n Pick an option:");

            Console.Write("\n (");
            ColoredText.Write("1", ConsoleColor.Magenta);
            Console.Write(") Show Task list, sorted by project or due date.");

            Console.Write("\n (");
            ColoredText.Write("2", ConsoleColor.Magenta);
            Console.Write(") Add a new project.");

            Console.Write("\n (");
            ColoredText.Write("3", ConsoleColor.Magenta);
            Console.Write(") Edit a project.");
            Console.Write("\n (");
            ColoredText.Write("4", ConsoleColor.Magenta);
            Console.Write(") Remove a project");

            Console.Write("\n (");
            ColoredText.Write("5", ConsoleColor.Magenta);
            Console.Write(") Add a new Task.");

            Console.Write("\n (");
            ColoredText.Write("6", ConsoleColor.Magenta);
            Console.Write(") Edit a Task (update or mark as done.");

            Console.Write("\n (");
            ColoredText.Write("7", ConsoleColor.Magenta);
            Console.Write(") Remove a task.");

            Console.Write("\n (");
            ColoredText.Write("8", ConsoleColor.Magenta);
            Console.Write(") Save and Quit.");

            
            Navigate();
        }
        private static void Navigate()
        {
            Console.WriteLine();
            Console.Write(" ");
            string answer = Console.ReadLine();
            
            if (answer == "1")
            {
                projectHandler.ShowTaskList();
            }
            else if (answer == "2")
            {
                projectHandler.AddNewProject();
            }
            else if (answer == "3")
            {
                projectHandler.EditAProject();
            }
            else if (answer == "4")
            {
                projectHandler.RemoveAProject();
            }
            else if (answer == "5")
            {
                projectHandler.AddNewTask();
            }
            else if (answer == "6")
            {
                projectHandler.EditATask();
            }
            else if (answer == "7")
            {
                projectHandler.RemoveATask();
            }
            else if (answer == "8")
            {
                projectHandler.SaveFile();
                System.Environment.Exit(0);
            }
            
            ShowMenu();
        }

    }

}
