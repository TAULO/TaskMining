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
        [EnableCors("AllowOrigin")]
        [HttpGet]
        public List<CompleteTask> GetAllTasks()
        {
            return AnalyseCompleteTask.CompleteTasks;
        }

        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("id")]
        public List<string> GetID()
        {
            return AnalyseCompleteTask.GetID();
        }

        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("names")]
        public List<string> GetAllNames()
        {
            return AnalyseCompleteTask.GetAllNames();
        }

        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("total-tasks")]
        public Dictionary<string, int> GetTotalTasks()
        {
            return AnalyseCompleteTask.GetTotalTasks();
        }

        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("{name}")]
        public CompleteTask GetTask(string name)
        {
            return AnalyseCompleteTask.GetTask(name);
        }

        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("id={id}")]
        public CompleteTask GetTaskByID(string id)
        {
           return AnalyseCompleteTask.GetTaskByID(id);
        }

        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("{name}/data={data}")]
        public int GetTaskDataFreq(string name, string data)
        {
            return AnalyseCompleteTask.GetTaskDataFreq(name, data);
        }

        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("{name}/ui={ui}")]
        public int GetTaskUserInteractionFreq(string name, string ui)
        {
            return AnalyseCompleteTask.GetTaskUserInteractionFreq(name, ui);
        }

        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("id={id}/data={data}")]
        public int GetTaskDataFreqByID(string id, string data)
        {
            return AnalyseCompleteTask.GetTaskDataFreqByID(id, data);
        }

        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("id={id}/ui={ui}")]
        public int GetTaskUserInteractionFreqByID(string id, string ui)
        {
            return AnalyseCompleteTask.GetTaskUserInteractionFreqByID(id , ui);
        }

        // /tasks
        [EnableCors("AllowOrigin")]
        [HttpPost]
        public async void PostCompleteTasks()
        {
            if (Response.StatusCode == 200)
            {
                try
                {
                    var completeTasks = AnalyseCompleteTask.CompleteTasks;
                    // read the contents of the incomming request body asynchronous
                    var reader = new StreamReader(Request.Body);
                    var data = await reader.ReadToEndAsync();

                    // run through every children in the JSON array 
                    JArray array = JArray.Parse(data);
                    foreach (JObject obj in array.Children<JObject>())
                    {
                        // store the JSON properties in a variable 
                        var jID = obj["id"];
                        var jName = obj["name"];
                        var jData = obj["data"];

                        // guard check: duplicate name & duplicate id
                        for (int i = 0; i < completeTasks.Count; i++)
                        {
                            if (jID.ToString().Contains(completeTasks[i].CompleteTaskID))
                            {
                                // TODO: handle duplicate ID
                            }
                            if (jName.ToString().Contains(completeTasks[i].CompleteTaskName))
                            {
                                // TODO: handle duplicate names
                            }
                        }

                        // convert the incomming data to a stream array, which will be handle in HandleCSV method
                        var bytes = Convert.FromBase64String(jData.ToString().Split(",")[1]);
                        var contents = new StreamContent(new MemoryStream(bytes));

                        completeTasks.Add(new CompleteTask(jID.ToString(), jName.ToString(), contents.ReadAsStream()));
                    }
                } catch(Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            else
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
            foreach (var item in AnalyseCompleteTask.CompleteTasks) oldTasks.Add(item);

            // reset tasks
            AnalyseCompleteTask.CompleteTasks = new List<CompleteTask>();

            // return old tasks
            return oldTasks;
        }
    }
}
