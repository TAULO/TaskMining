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
                .CompleteTasks
                .OrderBy(data => data.CompleteTaskName)
                .ToList();
        }

        [HttpGet]
        [Route("id")]
        public List<string> GetID()
        {
            return AnalyseCompleteTask
                .CompleteTasks
                .Select(data => data.CompleteTaskID)
                .ToList();
        }

        [HttpGet]
        [Route("names")]
        public List<string> GetAllNames()
        {
            return AnalyseCompleteTask
                .CompleteTasks
                .Select(data => data.CompleteTaskName)
                .ToList();
        }

        [HttpGet]
        [Route("total-tasks")]
        public Dictionary<string, int> GetTotalTasks()
        {
            var dic = new Dictionary<string, int>();

            var tasks = AnalyseCompleteTask
                .CompleteTasks
                .Select(data => new { data.CompleteTaskName, data.TotalIndividualTasks })
                .ToList();

            foreach(var task in tasks)
            {
                dic.Add(task.CompleteTaskName, task.TotalIndividualTasks);
            }
            return dic;
        }

        [HttpGet]
        [Route("{name}")]
        public CompleteTask GetTask(string name)
        {
            var task = AnalyseCompleteTask
                .CompleteTasks
                .Select(task => task)
                .Where(task => task.CompleteTaskName.ToLower().Equals(name.ToLower()))
                .FirstOrDefault();

            return task ?? throw new Exception($"{name} not found exception");
        }

        [HttpGet]
        [Route("id={id}")]
        public CompleteTask GetTaskByID(string id)
        {
            var task = AnalyseCompleteTask
                .CompleteTasks
                .Select(task => task)
                .Where(task => task.CompleteTaskID.ToLower().Equals(id.ToLower()))
                .FirstOrDefault();

            return task ?? throw new Exception($"No task corresponds with {id} exception");
        }

        [HttpGet]
        [Route("{name}/data={data}")]
        public int GetTaskDataFreq(string name, string data)
        {
            var task = AnalyseCompleteTask
                .CompleteTasks
                .Select(task => task)
                .Where(task => task.CompleteTaskName.ToLower().Equals(name.ToLower()))
                .FirstOrDefault();

            return task != null ? task.IndividualTaskFrequency(data) : 
                throw new Exception($"No task corresponds with {name} exception");
        }

        [HttpGet]
        [Route("{name}/ui={ui}")]
        public int GetTaskUserInteractionFreq(string name, string ui)
        {
            var task = AnalyseCompleteTask
                .CompleteTasks
                .Select(task => task)
                .Where(task => task.CompleteTaskName.ToLower().Equals(name.ToLower()))
                .FirstOrDefault();

            return task != null ? task.IndividualUserInteractionsFrequency(ui) : 
                throw new Exception($"No task corresponds with {name} exception");
        }

        [HttpGet]
        [Route("={id}/data={data}")]
        public int GetTaskDataFreqByID(string id, string data)
        {
            var task = AnalyseCompleteTask
                .CompleteTasks
                .Select(task => task)
                .Where(task => task.CompleteTaskID.ToLower().Equals(id.ToLower()))
                .FirstOrDefault();

            return task != null ? task.IndividualTaskFrequency(data) : 
                throw new Exception($"No task corresponds with {id} exception");
        }

        [HttpGet]
        [Route("id={id}/ui={ui}")]
        public int GetTaskUserInteractionFreqByID(string id, string ui)
        {
            var task = AnalyseCompleteTask
                .CompleteTasks
                .Select(task => task)
                .Where(task => task.CompleteTaskID.ToLower().Equals(id.ToLower()))
                .FirstOrDefault();

            return task != null ? task.IndividualUserInteractionsFrequency(ui) : 
                throw new Exception($"No task corresponds with {id} exception");
        }
    }
}
