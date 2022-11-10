using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskMining;

namespace TaskMiningAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyseController : ControllerBase
    {
        [HttpGet]
        [Route("data={data}")]
        public int GetTotalDataFreqCount(string data)
        {
            return AnalyseCompleteTask
                .IndividualTaskTotalFrequency(data);
        }

        [HttpGet]
        [Route("ui={ui}")]
        public int GetTotalUserInteractionFreqCount(string ui)
        {
            return AnalyseCompleteTask
                .IndividualUserInteractionsTotalFrequency(ui);
        }

        [HttpGet]
        [Route("data")]
        public Dictionary<string, int> GetTotalDataFreq()
        {
            return AnalyseCompleteTask.IndividualTaskTotalFrequency();
        }

        [HttpGet]
        [Route("ui")]
        public Dictionary<string, int> GetTotalTaskFreq()
        {
            return AnalyseCompleteTask.IndividualUserInteractionsTotalFrequency();
        }
        
        [HttpGet]
        [Route("completionTime")]
        public Dictionary<string, double> GetCompletionTime()
        {
            return AnalyseCompleteTask.CompletionTimePrCompleteTask();
        }
        
        [HttpGet]
        [Route("average")]
        public double GetAverageCompletionTime()
        {
            return AnalyseCompleteTask.CompleteTaskAverageCompletionTime();
        }

        [HttpGet]
        [Route("total")]
        public int GetTotalCompleteTask()
        {
            return AnalyseCompleteTask.TotalCompleteTasks;
        }
    }
}
