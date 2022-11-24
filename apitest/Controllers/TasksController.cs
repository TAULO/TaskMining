using Microsoft.AspNetCore.Mvc;
using TaskMining;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Cors;

namespace TaskMiningAPI.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        [HttpGet]
        public List<CompleteTask> GetAllTasks()
        {
            return AnalyseCompleteTask.CompleteTasks;
        }

        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("order")]
        public List<CompleteTask> GetAllTaskOrderAscTaskName()
        {
            return AnalyseCompleteTask
                .CompleteTasks
                .OrderBy(data => data.CompleteTaskName)
                .ToList();
        }

        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("id")]
        public List<string> GetID()
        {
            return AnalyseCompleteTask
                .CompleteTasks
                .Select(data => data.CompleteTaskID)
                .ToList();
        }

        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("names")]
        public List<string> GetAllNames()
        {
            return AnalyseCompleteTask
                .CompleteTasks
                .Select(data => data.CompleteTaskName)
                .ToList();
        }

        [EnableCors("AllowOrigin")]
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

        [EnableCors("AllowOrigin")]
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

        [EnableCors("AllowOrigin")]
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

        [EnableCors("AllowOrigin")]
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

        [EnableCors("AllowOrigin")]
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

        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("id={id}/data={data}")]
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

        [EnableCors("AllowOrigin")]
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

        // /tasks
        [EnableCors("AllowOrigin")]
        [HttpPost]
        public async void PostCompleteTasks()
        {
            if (Response.StatusCode == 200)
            {
                var completeTasks = AnalyseCompleteTask.CompleteTasks;
                var reader = new StreamReader(Request.Body);
                var data = await reader.ReadToEndAsync();

                JArray array = JArray.Parse(data);
                foreach (JObject obj in array.Children<JObject>())
                {
                    var jID = obj["id"];
                    var jName = obj["name"];
                    var jData = obj["data"];

                    // guard check: duplicate name & duplicate id
                    for (int i = 0; i < completeTasks.Count; i++)
                    {
                        if (jID.ToString().Contains(completeTasks[i].CompleteTaskID)) 
                        {
                            // handle duplicate ID
                        }
                        if (jName.ToString().Contains(completeTasks[i].CompleteTaskName))
                        {
                            // handle duplicate names
                        }
                    }

                    var bytes = Convert.FromBase64String(jData.ToString().Split(",")[1]);
                    var contents = new StreamContent(new MemoryStream(bytes));

                    completeTasks.Add(new CompleteTask(jID.ToString(), jName.ToString(), contents.ReadAsStream()));
                }
            } else
            {
                Debug.WriteLine("HTTP Error: " + Response.StatusCode);
            }
        }

        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("reset")]
        public List<CompleteTask> DeleteCompleteTasks()
        {
            // save old tasks
            var oldTasks = new List<CompleteTask>();
            foreach(var item in AnalyseCompleteTask.CompleteTasks) oldTasks.Add(item); 

            // reset tasks
            AnalyseCompleteTask.CompleteTasks = new List<CompleteTask>();

            // return old tasks
            return oldTasks;
        }
    }
}
