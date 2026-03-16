using System;
using System.Runtime.CompilerServices;

namespace Project_I_Todo_list
{

	public class Task
	{
		public string Title { get; set; }
		public DateTime DueDate { get; set; }
		public string Project { get; set; }

        public Task()
		{

		}

        public Task(string title, string project, DateTime dueDate)
		{
			Title = title;
			Project = project;
			DueDate = dueDate;
		}
	}
}
