using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;

namespace Project_I_Todo_list
{
	public class TodoList
	{
		string[] projects = new string[10];
		string currentProject = "";

		public TodoList()
		{
            GetDataFromFile();
		}

        private void GetDataFromFile()
		{            
            string filePath = "/Programutveckling/IT Påbyggnad Programmering AI/Individual-Project-I-Todo-list/projects.json";

            // Read the entire content of the JSON file into a string
            var json = File.ReadAllText(filePath);

            var data = JObject.Parse(json);

            Console.WriteLine("Parsed JSON data:");
            Console.WriteLine(data.ToString());

        }

        public void ShowTasklist()
        {
            Console.WriteLine("Projects");
        }

    }
}