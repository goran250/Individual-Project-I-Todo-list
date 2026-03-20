using System;
using System.Runtime.CompilerServices;

namespace Project_I_Todo_list
{

	public class Task
	{
		public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
		public string Status { get; set; }


        public Task() {}

        public Task(string title, string description, DateTime dueDate, string status)
		{
			Title = title;
			Description = description;
			DueDate = dueDate;
			Status = status;
		}

        public Task(string title, string description, DateTime dueDate)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
            Status = "Not Finished";
        }
    }
}
