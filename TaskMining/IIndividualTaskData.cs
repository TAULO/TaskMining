using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMining
{
    internal interface IIndividualTaskData
    {
        public string Data { get; }
        public UserInteractions UserInteractions { get; }
    }
}
