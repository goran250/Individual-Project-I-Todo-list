using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json;

namespace Project_I_Todo_list
{
	public class ProjectHandler
	{
		public List<Project> ProjectsList { get; set; }
        private List<Task> taskList;
        private static int nextProjectID;
        private static int nextTaskID;

        private string FilePath { get; set; }

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

            int nrbrOfProjects = ProjectsList.Count; 
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

        public void ShowTasks()
        {
            Console.WriteLine("\n Do you want to see the tasklist sorted by project or by due date.");
            Console.Write("\n (");
            ColoredText.Write("1", ConsoleColor.Yellow);
            Console.Write(") By project.");

            Console.Write("\n (");
            ColoredText.Write("2", ConsoleColor.Yellow);
            Console.Write(") By due date.");

            Console.Write("\n ");
            int min = 1;
            int max = 2;
            int answer = GetValidatedIntegerFromConsole("Number", "higher than zero and lower than 3", min, max);

            if (answer == 1)
                ShowTaskListByProject();
            else
                ShowTaskList(true, false);
        }

        public void SaveFile()
        {
            //Tries to save the list in a JSON file
            try
            {
                string json = JsonSerializer.Serialize(ProjectsList);
                File.WriteAllText(FilePath, json);
                Console.WriteLine("Your TodoList has been saved");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to save TodoList!");
            }
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

                if (project.TaskList.Count >= 1)
                {
                    foreach (Task task in project.TaskList)
                    {
                        Console.WriteLine(" " + task.Title.PadRight(titleLength) + task.Description.PadRight(descriptionLength) + task.DueDate.ToShortDateString().PadRight(dueDateLength) + task.Status.PadRight(statusLength));
                    }
                }
                else
                {
                    Console.WriteLine(" No tasks are added to this list!");
                }
            
            }

            Console.WriteLine("\n----------------------------------------------------------------------------------------------");
        }

        private void ShowProjects()
        {
            Console.WriteLine("\n----------------------------------------------------------------------------------------------");
            ColoredText.WriteLine("\n    " + "Project name", ConsoleColor.Green);

           
            for (int i = 0; i < ProjectsList.Count; i++)
            {
                Console.WriteLine("\n " + (i+1) + ". " + ProjectsList[i].Name);
            }

            Console.WriteLine("\n----------------------------------------------------------------------------------------------");
        }

        private int ShowTaskList(bool sortedByDate, bool showLineNumbers)
        {
            int projectNameLength = GetLongestTextLengthForProjectName() + 3;
            int titleLength = GetLongestTextLengthForTitle() + 3;
            int descrLength = GetLongestTextLengthForDescription() + 3;
            int dueDateLength = 10 + 3;
            int statusLength = 12 + 3;

            taskList = new List<Task>(); 

            foreach (Project project in ProjectsList)
            {
                foreach (Task task in project.TaskList)
                {
                    task.ProjectName = project.Name;
                    taskList.Add(task);
                }
            }

            if (sortedByDate)
            {
                taskList = taskList.OrderBy(t => t.DueDate).ToList();
                ColoredText.WriteLine("\n Tasks by due date:\n", ConsoleColor.Yellow);
            }
            else
            {
                ColoredText.WriteLine("\n Tasklist:\n", ConsoleColor.Yellow);
            }

            string lineNumber = " ";
            if (showLineNumbers)
                lineNumber = lineNumber.PadRight(6);
            
            ColoredText.Write(lineNumber + "Project".PadRight(projectNameLength) + "Title".PadRight(titleLength), ConsoleColor.Green);
            ColoredText.WriteLine("Description".PadRight(descrLength) + "Due Date".PadRight(dueDateLength) + "Status".PadRight(statusLength), ConsoleColor.Green);

            lineNumber = " ";
            
            for (int i = 1;  i <= taskList.Count; i++)
            {
                
                Task task = taskList[i-1];
                if (showLineNumbers)
                {
                   lineNumber = " " + i + ". ";
                   lineNumber = lineNumber.PadRight(6);
                }

                Console.Write(lineNumber + task.ProjectName.PadRight(projectNameLength), ConsoleColor.Green);
                Console.Write(task.Title.PadRight(titleLength) + task.Description.PadRight(descrLength), ConsoleColor.Green);
                Console.WriteLine(task.DueDate.ToShortDateString().PadRight(dueDateLength) + task.Status.PadRight(statusLength), ConsoleColor.Green);
            }

            return taskList.Count;
        }

        public void AddNewProject()
        {
            ColoredText.WriteLine("\n To enter a new project - Follow the steps\n",ConsoleColor.Yellow);

            string name = GetValidatedStringFromConsole("Project name");
            nextProjectID++;
            Project project = new Project(nextProjectID, name);
            ProjectsList.Add(project);
        }

        public void EditAProject()
        {
            ColoredText.WriteLine("\n Enter the line number for the project you want to edit", ConsoleColor.Yellow);
            ShowProjects();
            int min = 1;
            int max = ProjectsList.Count;
            int index = GetValidatedIntegerFromConsole("Number", "higher than zero and lower than " + (max + 1), min, max );
            ProjectsList[index - 1].Name = GetValidatedStringFromConsole("Project name");
        }

        public void RemoveAProject()
        {
            ColoredText.WriteLine("\n Enter the line number for the project you want to remove", ConsoleColor.Yellow);
            ShowProjects();
            int min = 1;
            int max = ProjectsList.Count;
            int index = GetValidatedIntegerFromConsole("Number", "higher than zero and lower than " + (max + 1), min, max);
            
            ColoredText.WriteLine("\n " + ProjectsList[index].Name + " has been removed", ConsoleColor.Yellow);
            ProjectsList.RemoveAt(index-1);
        }
        public void AddNewTask()
        {
            ColoredText.WriteLine("AddNewTask", ConsoleColor.Green);
        }

        public void EditATask()
        {
            ColoredText.WriteLine("\n Enter the line number for the task you want to edit", ConsoleColor.Yellow);
            int nbrOfTasks = ShowTaskList(false, true);
            int min = 1;
            int max = nbrOfTasks;
            int index = GetValidatedIntegerFromConsole("Number", "higher than zero and lower than " + (max + 1), min, max);

            ColoredText.WriteLine("\n Enter a new values. Just press enter if you do not want to change the value.", ConsoleColor.Yellow);
            
            Console.Write("\n Enter a new Title: ");
            string title = Console.ReadLine();
            if (String.IsNullOrEmpty(title) == false)
                taskList[index - 1].Title = title;

            Console.Write("\n Enter a new Description: ");
            string description = Console.ReadLine();
            if (String.IsNullOrEmpty(description) == false)
                taskList[index - 1].Description = description;

            string dateStr = GetValidatedDateFromConsole();
            if (dateStr != null){
                DateTime dueDate = DateTime.Parse(dateStr);
                taskList[index - 1].DueDate = dueDate;
            }

            string status = GetValidatedStatusFromConsole();
            if (String.IsNullOrEmpty(status) == false)
                taskList[index - 1].Status = status;

            UpdateProjectsList(taskList, index - 1);
        }

        public void RemoveATask()
        {
            ColoredText.WriteLine("\n Enter the line number for the task you want to remove", ConsoleColor.Yellow);
            int nbrOfTasks = ShowTaskList(false, true);
            int min = 1;
            int max = nbrOfTasks;
            int index = GetValidatedIntegerFromConsole("Number", "higher than zero and lower than " + (max + 1), min, max);
            
            int taskID = taskList[index - 1].ID;
            string taskTitle = taskList[index - 1].Title;

            // Remove the task from the projectsList
            foreach (Project project in ProjectsList)
            {
                int i = 0;
                bool found = false;
                while (!found)  
                { 
                    if (project.TaskList[i].ID == taskID)
                    {
                        project.TaskList.RemoveAt(i);
                        found = true;
                    }
                   
                    i++;
                }
            }
            ColoredText.WriteLine("\n The task " + taskTitle + " has been removed", ConsoleColor.Yellow);
        }


        // Gets a atring from the console and validates it so its not empty.
        private string GetValidatedStringFromConsole(string variableName)
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

        private int GetValidatedIntegerFromConsole(string variableName, string higherAndLowerThanText, int min, int max)
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
                    ColoredText.WriteLine(" " + variableName + " must be non-negative and " + higherAndLowerThanText + ".", ConsoleColor.Red);
                    isValidInteger = false;
                }
            } while (isValidInteger == false);

            return index;
        }

        // Gets a atring from the console and validates it so its not empty.
        private string GetValidatedStatusFromConsole()
        {
            string result;
            bool endLoop = false;
            do
            {
                Console.Write("\n Enter a new Status: ");
                result = Console.ReadLine();

                if (String.IsNullOrEmpty(result))
                {
                    endLoop = true;
                }
                else if (result == "Not finished" || result == "Finished")
                {
                    endLoop = true;
                }
                else
                {
                    ColoredText.WriteLine(" You have not entered a valid status. Enter \"Not finished\" or \"Finished\"", ConsoleColor.Red);
                }
                
            } while (!endLoop);

            return result;
        }

        private string GetValidatedDateFromConsole()
        {
            bool isDate;
            string result;
            do
            {
                Console.Write("\n Enter a new Due date: ");
                result = Console.ReadLine();

                if (String.IsNullOrEmpty(result))
                {
                    return null;
                }
                isDate = DateTime.TryParse(result, out DateTime dueDate);

                if (isDate == false)
                {
                    ColoredText.WriteLine(" You have not entered a valid date.", ConsoleColor.Red);
                }

            } while (isDate == false);

            return result;
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

        private void UpdateProjectsList(List<Task> taskList, int index)
        {
            int taskID = taskList[index].ID;
            foreach (Project project in ProjectsList)
            {
                foreach (Task task in project.TaskList)
                {
                    if (task.ID == taskID)
                    {
                        task.Title = taskList[index].Title;
                        task.Description = taskList[index].Description;
                        task.DueDate = taskList[index].DueDate;
                        task.Status = taskList[index].Status;
                        break;
                    }
                }
            }
        }

        private void LoadProjectsFile()
        {
            try
            {
                // Read the entire content of the JSON file into a string
                var json = File.ReadAllText(FilePath);
                ProjectsList = JsonSerializer.Deserialize<List<Project>>(json);

                nextProjectID = ProjectsList.Count;
                nextTaskID = ProjectsList[nextProjectID - 1].TaskList.Count;
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to open saved Projects List\n");
            }
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
                   new Task(nextTaskID++, "Programming", "Do this projects coding startdate 2026-04-02", new DateTime(2026,04,04), "Not finished"),
                   new Task(nextTaskID++, "Testing", "Test this projects code", new DateTime(2026,04,05), "Not finished"),
                   new Task(nextTaskID++, "Deploying", "Deploying this project code", new DateTime(2026,04,06), "Not finished")
               ]);

            ProjectsList.Add(project);

            nextProjectID++;
            project = new Project(nextProjectID, "Project_Three");
            project.TaskList.AddRange(
               [
                   new Task(nextTaskID++, "Programming", "Do this projects coding startdate 2026-04-09", new DateTime(2026,04,11), "Not finished"),
                   new Task(nextTaskID++, "Testing", "Test this projects code", new DateTime(2026,04,11), "Not finished"),
                   new Task(nextTaskID++, "Deploying", "Deploying this project code", new DateTime(2026,04,12), "Not finished")
               ]);

            ProjectsList.Add(project);
        }
    }
}
