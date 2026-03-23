using System;
using System.Runtime.CompilerServices;

namespace Project_I_Todo_list
{

	public class Task
	{
        public int ID { get; set; }
        public string ProjectName;
		public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
		public string Status { get; set; }
        


        public Task() {}

        public Task(int id,  string title, string description, DateTime dueDate, string status)
		{
			ID = id;
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
