using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskMining;

namespace TaskMiningAPI.Controllers
{
    [Route("api/tasks/[controller]")]
    [ApiController]
    public class AnalyseController : ControllerBase
    {
        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("data={data}")]
        public int GetTotalDataFreqCount(string data)
        {
            return AnalyseCompleteTask.IndividualTaskTotalFrequency(data);
        }

        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("ui={ui}")]
        public int GetTotalUserInteractionFreqCount(string ui)
        {
            return AnalyseCompleteTask.IndividualUserInteractionsTotalFrequency(ui);
        }

        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("data")]
        public Dictionary<string, int> GetTotalDataFreq()
        {
            return AnalyseCompleteTask.IndividualTaskTotalFrequency();
        }

        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("ui")]
        public Dictionary<string, int> GetTotalTaskFreq()
        {
            return AnalyseCompleteTask.IndividualUserInteractionsTotalFrequency();
        }

        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("completion-time")]
        public Dictionary<string, double> GetCompletionTime()
        {
            return AnalyseCompleteTask.CompletionTimePrCompleteTask();
        }

        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("average")]
        public double GetAverageCompletionTime()
        {
            return AnalyseCompleteTask.CompleteTaskAverageCompletionTime();
        }
        
        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("users")]
        public int GetTotalAmountOfUniqueUsers()
        {
            return AnalyseCompleteTask.CalcTotalAmountOfUniqueUsers();
        }
        
        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("total-ui")]
        public int GetTotalAmountOfUI()
        {
            return AnalyseCompleteTask.CalcTotalAmountOfUI();
        }
        
        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("total-steps")]
        public int GetTotalAmountOfSteps()
        {
            return AnalyseCompleteTask.CalcTotalAmountOfSteps();
        }
        
        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("apps")]
        public int GetTotalAmountOfApps()
        {
            return AnalyseCompleteTask.CalcTotalAmountOfApps();
        }

        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("total")]
        public int GetTotalCompleteTask()
        {
            return AnalyseCompleteTask.TotalCompleteTasks;
        }

        [EnableCors("AllowOrigin")]
        [HttpGet]
        [Route("repeatable-ui={task}")]
        public List<string> GetRepeatableUserInteractions(CompleteTask task)
        {
            return AnalyseCompleteTask.RepeatableUserInteracions(task);
        }
    }
}
