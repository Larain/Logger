using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLoggerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger<double> logger = new Logger<double>(ReportTemplate.Speed);
            Random upper = new Random();
            Random lower = new Random();
            for (int i = 0; i < 100; i++)
            {
                double value = upper.Next(200, 999) / lower.Next(1, 300);
                logger.LogState(value);
            }
            logger.LogState(5000);
            logger.WriteLogsToFile();
        }
    }
}
