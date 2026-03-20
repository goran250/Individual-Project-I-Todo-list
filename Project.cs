using System;

using System;
using System.Runtime.CompilerServices;

namespace Project_I_Todo_list
{

    public class Project
    {
        public string Name { get; set; }
        public List<Task> TaskList { get; set; }

        public Project() {}

        public Project(string name)
        {
            Name = name;
            TaskList = new List<Task>();

        }
    }
}
