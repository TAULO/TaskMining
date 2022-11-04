using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMining
{
    internal class AnalyseCompleteTask
    {
        // repeatable complete task
        //
        public static List<CompleteTask>? CompleteTasks = new List<CompleteTask>();

        public static List<CompleteTask> RepeatableCompleteTasks()
        {
            var comTasks = new List<CompleteTask>();
            var invTasks = new List<IndividualTask>();

            //foreach (var task in CompleteTasks)
            //{
            //    var hey = task.IndividualTasks
            //        .GroupBy(task => task)
            //        .Where(x => x.Count() > 1)
            //        .Select(x => new { x.Key })
            //        .ToList();
            //}

           
            return null;
        }
    }
}
