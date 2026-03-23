using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Project_I_Todo_list
{
	public class ProjectHandler
	{
		public List<Project> ProjectsList { get; set; }
        private string FilePath { get; set; }
        private static int nextProjectID;
        private static int nextTaskID;

        public ProjectHandler()
		{
			ProjectsList = new List<Project>();

            FilePath = Environment.CurrentDirectory + "\\projects.json";

            //If a file exists, load it. Otherwise, create a sample file with a list of projects.
            if (File.Exists(FilePath))
                LoadProjectsFile();
            else
            {
                CreateSampleList();
                SaveFile();
            }
        }

        // Returns an integer list with 3 values. The first value is nbr of projects and the second value is nbr of
        // tasks not done, the third value is nbr of tasks done .
        public List<int> GetNbrOfProjectsAndTasksStatus()
        {
            List<int> nbrOf = new List<int>(3);

            int nrbrOfProjects = ProjectsList.Count(); 
            int notDoneTask = 0;
            int doneTask = 0;


            foreach (Project project in ProjectsList)
            {
                foreach (Task task in project.TaskList)
                {
                    if (task.Status == "Not finished")
                    {
                        notDoneTask++; // nbr of notDoneTask
                    }
                    else
                    {
                        doneTask++; // nbr of doneTask
                    }
                }
            }            
            
            nbrOf.Add(nrbrOfProjects);
            nbrOf.Add(notDoneTask);
            nbrOf.Add(doneTask);

            return nbrOf;
        }

        public void ShowTaskList()
        {
            Console.WriteLine("\n Do you want to see the tasklist sorted by project or by due date.");
            Console.Write("\n (");
            ColoredText.Write("1", ConsoleColor.Magenta);
            Console.Write(") By project.");

            Console.Write("\n (");
            ColoredText.Write("2", ConsoleColor.Magenta);
            Console.Write(") By due date.");

            Console.WriteLine();
            Console.Write(" ");
            string answer = Console.ReadLine();

            if (answer == "1")
                ShowTaskListByProject();
            else
                ShowTaskListByDate();
        }

        private void ShowTaskListByProject()
        {
            int titleLength = GetLongestTextLengthForTitle() + 3;
            int descriptionLength = GetLongestTextLengthForDescription() + 3;
            int dueDateLength = 10 + 3;
            int statusLength = 12 + 3;
               
            ColoredText.WriteLine("\n Tasks by project:", ConsoleColor.Magenta);

            foreach (Project project in ProjectsList)
            {

                Console.WriteLine("\n----------------------------------------------------------------------------------------------");
                ColoredText.Write("\n Project:", ConsoleColor.Green);
                Console.Write("   " + project.Name);

                ColoredText.WriteLine("\n\n Tasklist:\n", ConsoleColor.Green);

                ColoredText.WriteLine(" Title".PadRight(titleLength) + " Description".PadRight(descriptionLength) + " Due Date".PadRight(dueDateLength) + " Status".PadRight(statusLength), ConsoleColor.Green);

                foreach (Task task in project.TaskList)
                {
                    Console.WriteLine(" " + task.Title.PadRight(titleLength) + task.Description.PadRight(descriptionLength) + task.DueDate.ToShortDateString().PadRight(dueDateLength) + task.Status.PadRight(statusLength));
                }
            }

            Console.WriteLine("\n----------------------------------------------------------------------------------------------");
        }

        private void ShowProjectsByID()
        {
            int projectNameLength = GetLongestTextLengthForTitle() + 3;
            int idLength = 3 + 3;
        
            ColoredText.WriteLine("\n Projectslist: Select the ID for the project you want to edit", ConsoleColor.Magenta);

            foreach (Project project in ProjectsList)
            {

                Console.WriteLine("\n----------------------------------------------------------------------------------------------");


                ColoredText.WriteLine("\n " + "ID".PadRight(idLength) + "Project".PadRight(projectNameLength), ConsoleColor.Green);
                Console.WriteLine("\n " + project.ID.ToString().PadRight(idLength) + project.Name.PadRight(projectNameLength) );

            }

            Console.WriteLine("\n----------------------------------------------------------------------------------------------");
        }

        private void ShowTaskListByDate()
        {
            int projectNameLength = GetLongestTextLengthForProjectName() + 3;
            int titleLength = GetLongestTextLengthForTitle() + 3;
            int descriptionLength = GetLongestTextLengthForDescription() + 3;
            int dueDateLength = 10 + 3;
            int statusLength = 12 + 3;

            List<Task> tasksByDate = new List<Task>();
            foreach (Project project in ProjectsList)
            {
                foreach (Task task in project.TaskList)
                {
                    task.ProjectName = project.Name;
                    tasksByDate.Add(task);
                }
            }

            tasksByDate = tasksByDate.OrderBy(t => t.DueDate).ToList();


            ColoredText.WriteLine("\n Tasks by due date:", ConsoleColor.Magenta);

            ColoredText.WriteLine("\n\n Tasklist:\n", ConsoleColor.Green);

            ColoredText.WriteLine(" Project".PadRight(projectNameLength) + " Title".PadRight(titleLength) + " Description".PadRight(descriptionLength) + " Due Date".PadRight(dueDateLength) + " Status".PadRight(statusLength), ConsoleColor.Green);
           
            foreach (Task task in tasksByDate)
            {
                Console.WriteLine(" " + task.ProjectName.PadRight(projectNameLength) + task.Title.PadRight(titleLength) + task.Description.PadRight(descriptionLength) + task.DueDate.ToShortDateString().PadRight(dueDateLength) + task.Status.PadRight(statusLength));
            }
        }

        public void AddNewProject()
        {
            ColoredText.WriteLine("\n To enter a new project - Follow the steps\n",ConsoleColor.Yellow);

            string name = GetValidatedStringFromConsole("Product name");

            Project project = new Project(nextProjectID, name);
            nextProjectID++;
            ProjectsList.Add(project);
        }

        public void EditAProject()
        {
            ColoredText.WriteLine("\n To edit a project - Follow the steps\n", ConsoleColor.Yellow);

            ShowProjectsByID();
            string name= GetValidatedStringFromConsole("Product name");
        }

        public void RemoveAProject()
        {
            ColoredText.WriteLine("RemoveAProject", ConsoleColor.Green);
        }
        public void AddNewTask()
        {
            ColoredText.WriteLine("AddNewTask", ConsoleColor.Green);
        }

        public void EditATask()
        {
            ColoredText.WriteLine("EditATask", ConsoleColor.Green);
        }

        public void RemoveATask()
        {
            ColoredText.WriteLine("RemoveATask", ConsoleColor.Green);
        }

        // Gets a atring from the console and validates it so its not empty.
        private string GetValidatedStringFromConsole(string variableName)
        {
            Console.Write(" Enter a " + variableName + ": ");
            string result = Console.ReadLine();

            while (String.IsNullOrEmpty(result))
            {
                ColoredText.WriteLine(" " + variableName + " can't be an empty string", ConsoleColor.Red);
                
                Console.Write(" Enter a " + variableName + ": ");
                result = Console.ReadLine();
            }

            return result;
        }

        public void SaveFile()
        {
            
            //Tries to save the list in a JSON file
            try
            {
                string json = JsonSerializer.Serialize(ProjectsList);
                File.WriteAllText(FilePath, json);
                Console.WriteLine("Your To-do list has been saved");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to save TodoList!");
            }
        }

        // GetLongestTextLengthForTitle() finds the longest word for a given variable.
        private int GetLongestTextLengthForTitle()
        {
            int textLength = "Title".Length; // 

            foreach (Project project in ProjectsList)  
            {
                foreach (Task task in project.TaskList)
                {
                    if (task.Title.Length > textLength)
                    {
                        textLength = task.Title.Length;
                    }
                }
            }

            return textLength;
        }
        
        private int GetLongestTextLengthForDescription()
        {
            int textLength = "Description".Length; // 

            foreach (Project project in ProjectsList)
            {
                foreach (Task task in project.TaskList)
                {
                    if (task.Description.Length > textLength)
                    {
                        textLength = task.Description.Length;
                    }
                }
            }

            return textLength;
        }

        private int GetLongestTextLengthForProjectName()
        {
            int textLength = "Project".Length; // 

            foreach (Project project in ProjectsList)
            {
                if (project.Name.Length > textLength)
                {
                    textLength = project.Name.Length;
                }
            }

            return textLength;
        }

        private void LoadProjectsFile()
        {
            try
            {
                // Read the entire content of the JSON file into a string
                var json = File.ReadAllText(FilePath);
                ProjectsList = JsonSerializer.Deserialize<List<Project>>(json);
                
                nextProjectID = ProjectsList.Count;

                Project project = ProjectsList[nextProjectID - 1];
                int i = project.TaskList.Count();
                nextTaskID = project.TaskList[i - 1].ID;
                 
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to open saved Projects List\n");
            }

            // var data = JObject.Parse(json);
            Console.WriteLine("Parsed JSON data:");

        }



        private void CreateSampleList()
        {
            nextProjectID = 0;
            Project project = new Project(nextProjectID, "My_personal_tasklist");
            nextTaskID = 0;
            
            project.TaskList.AddRange(
               [
                    new Task(nextTaskID++, "Morning task 1/4", "Buy breakfast", new DateTime(2026,04,01), "Finished"),
                    new Task(nextTaskID++, "Midday task 1/4", "Buy lunch", new DateTime(2026,04,01), "Not finished"),
                    new Task(nextTaskID++, "Car washing 1/4", "Wash the boss car",new DateTime(2026,04,01), "Not finished"),
                    new Task(nextTaskID++, "Afternoon task 1/4", "Buy coffey", new DateTime(2026,04,01), "Not finished"),
                    new Task(nextTaskID++, "Morning task 2/4", "Buy breakfast", new DateTime(2026,04,02), "Finished"),
                    new Task(nextTaskID++, "Midday task 2/4", "Buy lunch", new DateTime(2026,04,02), "Not finished"),
                    new Task(nextTaskID++, "Car washing 2/4", "Wash my own car", new DateTime(2026,04,02), "Not finished"),
                    new Task(nextTaskID++, "Afternoon task 2/4", "Buy coffey", new DateTime(2026,04,02), "Not finished")
               ]);

            ProjectsList.Add(project);

            nextProjectID++;
            project = new Project(nextProjectID, "Project_One");
            project.TaskList.AddRange(
               [
                   new Task(nextTaskID++, "Programming", "Do this projects coding", new DateTime(2026,04,01), "Not finished"),
                   new Task(nextTaskID++, "Testing", "Test this projects code", new DateTime(2026,04,01), "Not finished"),
                   new Task(nextTaskID++, "Deploying", "Deploying this project code",new DateTime(2026,04,01), "Not finished")
               ]);

            ProjectsList.Add(project);

            nextProjectID++;
            project = new Project(nextProjectID, "Project_Two");
            project.TaskList.AddRange(
               [
                   new Task(nextTaskID++,"Programming", "Do this projects coding startdate 2026-04-02", new DateTime(2026,04,04), "Not finished"),
                   new Task(nextTaskID++,"Testing", "Test this projects code", new DateTime(2026,04,05), "Not finished"),
                   new Task(nextTaskID++,"Deploying", "Deploying this project code", new DateTime(2026,04,06), "Not finished")
               ]);

            ProjectsList.Add(project);

            nextProjectID++;
            project = new Project(nextProjectID, "Project_Three");
            project.TaskList.AddRange(
               [
                   new Task(nextTaskID++,"Programming", "Do this projects coding startdate 2026-04-09", new DateTime(2026,04,11), "Not finished"),
                   new Task(nextTaskID++,"Testing", "Test this projects code", new DateTime(2026,04,11), "Not finished"),
                   new Task(nextTaskID++,"Deploying", "Deploying this project code", new DateTime(2026,04,12), "Not finished")
               ]);
            ColoredText.WriteLine("\nHighest project ID" + nextProjectID, ConsoleColor.Green);
            ColoredText.WriteLine("Highest task ID" + nextTaskID, ConsoleColor.Green);
            ProjectsList.Add(project);
        }
    }
}
