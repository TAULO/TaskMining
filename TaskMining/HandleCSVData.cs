using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMining
{
    internal class HandleCSVData
    {
        private readonly string Path;

        public readonly List<IndividualTask> Tasks = new List<IndividualTask>();
        public HandleCSVData(string path)
        {
            Path = path;
            AddTasks();
        }
        private List<IndividualTask> AddTasks()
        {
            var csvParser = new TextFieldParser(Path);

            csvParser.SetDelimiters(new string[] { "," });
            csvParser.HasFieldsEnclosedInQuotes = true;

            // skip header line
            csvParser.ReadLine();

            while (!csvParser.EndOfData)
            {
                string[]? fields = csvParser.ReadFields();

                if (fields != null)
                {
                    string timeStamp = fields[0];
                    string machineName = fields[1];
                    string userName = fields[3];
                    string applicationName = fields[4];

                    IndividualTaskData data = new IndividualTaskData(fields[5], IndividualTaskData.GetUserInteractiosn(fields[6]));

                    Tasks.Add(new IndividualTask(timeStamp, machineName, userName, applicationName, data));
                }
            }
            csvParser.Close();

            return Tasks;
        }   
    }   
}   
