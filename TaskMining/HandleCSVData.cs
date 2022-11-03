using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMining
{
    public abstract class HandleCSVData : IHandleCSVData
    {
        public abstract void HandleCSV(string path);
    }   
}   
