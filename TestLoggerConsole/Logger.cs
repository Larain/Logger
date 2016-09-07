using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLoggerConsole
{
    enum ReportTemplate { Default, Memory, Speed };
    class Logger<T>
    {
        const long megabyte = 1024 * 1024;

        #region Variables
        DateTime testDate;
        List<T> valuesStorage;
        List<string> logsList;
        string appName;
        string fileName;
        string fileFolder;
        #endregion

        //
        // Summary:
        // Set this property to make report view styled for template;
        //
        ReportTemplate Template { get; set; }

        #region Constructors
        public Logger()
        {
            testDate = DateTime.Now;
            valuesStorage = new List<T>();
            logsList = new List<string>();
        }

        public Logger(ReportTemplate templateType)
        {
            testDate = DateTime.Now;
            valuesStorage = new List<T>();
            logsList = new List<string>();
            Template = templateType;
        }

        public Logger(string reportName, string reportFolder)
        {
            testDate = DateTime.Now;
            valuesStorage = new List<T>();
            logsList = new List<string>();
            fileName = reportName;
            fileFolder = reportFolder;
        }

        public Logger(string reportName, string reportFolder, string applicationName, ReportTemplate templateType)
        {
            testDate = DateTime.Now;
            valuesStorage = new List<T>();
            logsList = new List<string>();
            appName = applicationName;
            fileName = reportName;
            fileFolder = reportFolder;
            Template = templateType;
        }
        #endregion
        // Retrieves lsit of strings in format that can be written to logs file
        private void CreateCommonReport()
        {
            for (int i = 0; i < valuesStorage.Count; i++)
            {
                if (i % 20 == 0)
                    logsList.Add("\n");
                logsList.Add("stage #" + i + " value: " + valuesStorage[i] + " points;");
            }
        }
        private void CreateMemoryReport()
        {
            for (int i = 0; i < valuesStorage.Count; i++)
            {
                if (i % 20 == 0)
                    logsList.Add("\n");
                double memoryInMB = Convert.ToInt64(valuesStorage[i]) / megabyte;
                logsList.Add("Memory capacity: " + Math.Round(memoryInMB, 2) + " MB;");
            }
        }
        private void CreateSpeedReport()
        {
            for (int i = 0; i < valuesStorage.Count; i++)
            {
                if (i % 20 == 0)
                    logsList.Add("\n");
                logsList.Add("Time elapsed: " + valuesStorage[i] + " Seconds;");
            }

            double max = Convert.ToDouble(valuesStorage.Max());
            double last = Convert.ToDouble(valuesStorage.Last());

            // Convert time;
            if (last >= max)
            {
                string units = " Seconds;";
                logsList.Remove(logsList.Last());
                if (last > 60)
                {
                    last = last / 60;
                    units = " Minutes;";
                    if (last > 60)
                    {
                        last = last / 24;
                        units = " Hours;";
                    }
                }
                last = Math.Round(last, 2);
                logsList.Add("Total time elapsed: " + last + units);
            }
        }
        //
        // Summary:
        //     For template usage use seconds and bytes units.
        public void LogState(T value)
        {
            if (value != null)
                valuesStorage.Add(value);
        }
        //
        // Summary:
        //     Write current logs to file.
        public void WriteLogsToFile()
        {
            if (valuesStorage == null)
            {
                throw new NullReferenceException("There aren't any logs to write!");
            }
            else
            { 
                switch (Template)
                {
                    case ReportTemplate.Memory:
                        {
                            CreateMemoryReport();
                            if (fileName == null)
                                fileName = @"Memory.Report.txt";
                            if (fileFolder == null)
                                fileFolder = @"\Reports\Memory\";
                            break;
                        }
                    case ReportTemplate.Speed:
                        {
                            CreateSpeedReport();
                            if (fileName == null)
                                fileName = @"Speed.Report.txt";
                            if (fileFolder == null)
                                fileFolder = @"\Reports\Speed\";
                            break;
                        }
                    case ReportTemplate.Default:
                        {
                            CreateCommonReport();
                            if (fileName == null)
                                fileName = @"Default.Report.txt";
                            if (fileFolder == null)
                                fileFolder = @"\Reports\Unnamed\";
                            break;
                        }
                }
            }
            
            // Configuration Tempalte
            string header = "REPORT <" + DateTime.Now.ToString() + ">";
            if (appName != null)
                header += " Application: " + appName;
            string version = "(" + DateTime.Now.ToString().Replace(':', '-') + ")";
            string filePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, fileFolder);
            Directory.CreateDirectory(filePath);

            using (StreamWriter sw = new StreamWriter(Path.Combine(filePath, version + fileName), false, System.Text.Encoding.UTF8))
            {
                sw.WriteLine(header);
                for (int i = 0; i < logsList.Count; i++)
                    sw.WriteLine(logsList[i]);
            }
        }
    }
}
