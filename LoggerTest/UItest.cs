using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.White.UIItems.WindowItems;

namespace LoggerTest
{
    [TestClass]
    public class UItest
    {
        [TestMethod]
        public void StartApplication()
        {
            string curPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\"));//D:\Bitbucket\Solution\eIModelTest\bin\Debug\TestResults\y.oleynik_IT060 2016-09-15 14_19_45\Out
            string path = curPath + @"Global\bin\Debug\";
            System.Console.WriteLine(System.AppDomain.CurrentDomain.BaseDirectory);
            System.Console.WriteLine(path);
            Assert.AreEqual(path, @"Global\bin\Debug\");
            System.Diagnostics.ProcessStartInfo prc = new System.Diagnostics.ProcessStartInfo("Global.exe");
            prc.WorkingDirectory = path;
            TestStack.White.Application application = TestStack.White.Application.Launch(prc);
            Window window = application.GetWindow("Global", TestStack.White.Factory.InitializeOption.NoCache);
            //D:\Dev\Git\Logger\LoggerTest\bin\Debug // TestLoggerConsole\bin\Debug */

        }
    }
}
