// using Newtonsoft.Json.Linq;
using System.Text.Json;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Net.Http.Headers;

namespace Project_I_Todo_list
{
	public class TodoList
	{
        ProjectHandler projectHandler;

        public TodoList()
		{
            projectHandler = new ProjectHandler();
            GetDataFromFile();

            

            projectHandler.ProjectsList.AddRange(CreateSampleList());

        }

        private void GetDataFromFile()
		{

            // filePath = "/Programutveckling/IT Påbyggnad Programmering AI/Individual-Project-I-Todo-list/projects.json";
            // string filePath = "/Programmering/IT Påbyggnad Programmering AI/Individual-Project-I-Todo-list/projects.json";
            string filePath = Environment.CurrentDirectory + "\\projects.json";


            try
            {
                // Read the entire content of the JSON file into a string
                var json = File.ReadAllText(filePath);
                projectHandler.ProjectsList = JsonSerializer.Deserialize<List<Project>>(json);
                // TaskList.tasks = JsonSerializer.Deserialize<List<Task>>(jsonString);
                
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to open saved Projects List\n");
            }

            // var data = JObject.Parse(json);
            Console.WriteLine("Parsed JSON data:");
            // Console.WriteLine(projectHandler.ProjectsList.ToString());

        }

        public static List<Project> CreateSampleList()
        {
            //Create a populated list of tasks
            List<Project> projectsList = new List<Project>();

            Project project = new Project("My_personal_tasklist");
            project.TaskList.AddRange(
               [
                    new Task("Morning task 1/4", "Buy breakfast", new DateTime(2026,04,01), "Finished"),
                    new Task("Midday task 1/4", "Buy lunch", new DateTime(2026,04,01), "Not finished"),
                    new Task("Car washing 1/4", "Wash the boss car",new DateTime(2026,04,01), "Not finished"),
                    new Task("Afternoon task 1/4", "Buy coffey", new DateTime(2026,04,01), "Not finished"),
                    new Task("Morning task 2/4", "Buy breakfast", new DateTime(2026,04,02), "Finished"),
                    new Task("Midday task 2/4", "Buy lunch", new DateTime(2026,04,02), "Not finished"),
                    new Task("Car washing 2/4", "Wash my own car", new DateTime(2026,04,02), "Not finished"),
                    new Task("Afternoon task 2/4", "Buy coffey", new DateTime(2026,04,02), "Not finished")
               ]);

            projectsList.Add(project);


            project = new Project("Project_One");
            project.TaskList.AddRange(
               [
                   new Task("Programming", "Do this projects coding", new DateTime(2026,04,01), "Not finished"),
                   new Task("Testing", "Test this projects code", new DateTime(2026,04,01), "Not finished"),
                   new Task("Deploying", "Deploying this project code",new DateTime(2026,04,01), "Not finished")
               ]);

            projectsList.Add(project);

            project = new Project("Project_Two");
            project.TaskList.AddRange(
               [
                   new Task("Programming", "Do this projects coding startdate 2026-04-02", new DateTime(2026,04,04), "Not finished"),
                   new Task("Testing", "Test this projects code", new DateTime(2026,04,05), "Not finished"),
                   new Task("Deploying", "Deploying this project code", new DateTime(2026,04,06), "Not finished")
               ]);

            projectsList.Add(project);

            project = new Project("Project_Three");
            project.TaskList.AddRange(
               [
                   new Task("Programming", "Do this projects coding startdate 2026-04-09", new DateTime(2026,04,11), "Not finished"),
                   new Task("Testing", "Test this projects code", new DateTime(2026,04,11), "Not finished"),
                   new Task("Deploying", "Deploying this project code", new DateTime(2026,04,12), "Not finished")
               ]);
            
            projectsList.Add(project);

            return projectsList;
        }

           
        public void ShowTasklist()
        {
            Console.WriteLine("Projects");
        }

    }
}