using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMining
{
    internal class CompleteTask
    {
        public string IndividualTaskID { get; }
        public string IndividualTaskName { get; set; }
        public int IndividualTaskFrequency { get; }
        public int TotalIndividualTasks { get; }

        public readonly List<IndividualTask> Tasks = new List<IndividualTask>();
        public CompleteTask(string individualTaskName, int individualTaskFrequency, int totalIndividualTasks)
        {
            IndividualTaskID = Guid.NewGuid().ToString("N");
            IndividualTaskName = individualTaskName;
            IndividualTaskFrequency = individualTaskFrequency;
            TotalIndividualTasks = totalIndividualTasks;    
        }
    }
}
