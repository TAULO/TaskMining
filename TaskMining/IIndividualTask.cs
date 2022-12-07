using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMining
{
    internal interface IIndividualTask
    {
        public string TimeStamp { get; }
        public string MachineName { get; }
        public string UserName { get; }
        public string ApplicationName { get; }
        public IndividualTaskData Data { get; }
    }
}
