using System;

using System;
using System.Runtime.CompilerServices;

namespace Project_I_Todo_list
{

    public class Project
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Task> TaskList { get; set; }

        public Project() {}

        public Project(int id, string name)
        {
            ID = id;
            Name = name;
            TaskList = new List<Task>();
        }
    }
}
