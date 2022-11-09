using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskMining;

namespace TaskMiningAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        [HttpGet]
        public List<CompleteTask> GetAllTasks()
        {
            return AnalyseCompleteTask.GetData();
        }

        [HttpGet]
        [Route("order")]
        public List<CompleteTask> GetAllTaskOrderAscTaskName()
        {
            return AnalyseCompleteTask
                .GetData()
                .OrderBy(data => data.CompleteTaskName)
                .ToList();
        }

        [HttpGet]
        [Route("id")]
        public List<string> GetID()
        {
            return AnalyseCompleteTask
                .GetData()
                .Select(data => data.CompleteTaskID)
                .ToList();
        }

        [HttpGet]
        [Route("names")]
        public List<string> GetAllNames()
        {
            return AnalyseCompleteTask
                .GetData()
                .Select(data => data.CompleteTaskName)
                .ToList();
        }

        [HttpGet]
        [Route("total-tasks")]
        public Dictionary<string, int> GetTotalTasks()
        {
            var dic = new Dictionary<string, int>();

            var tasks = AnalyseCompleteTask
                .GetData()
                .Select(data => new { data.CompleteTaskName, data.TotalIndividualTasks })
                .ToList();

            foreach(var task in tasks)
            {
                dic.Add(task.CompleteTaskName, task.TotalIndividualTasks);
            }

            return dic;
        }

        [HttpGet]
        [Route("test")]
        public List<string> GetTest()
        {
            return AnalyseCompleteTask.CompleteTasks
                .Select(task => task.CompleteTaskName)
                .ToList();
        }
    }
}
