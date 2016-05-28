using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using FoodOrderingBuddy.Models;

namespace FoodOrderingBuddy.Helpers
{
    public static class LogFilterHelper
    {
        private readonly static string fileDir = ConfigurationManager.AppSettings["weblogs"];
        private readonly static string logFile = fileDir + DateTime.Now.ToString("yyyyMMdd") + ".txt";

        public static void Log(string serializedActionLog)
        {
            if (!System.IO.File.Exists(logFile))
                System.IO.File.Create(logFile).Close();

            using (StreamWriter textWriter = new StreamWriter(logFile, true))
            {
                string dateTime = DateTime.Now.ToString();
                textWriter.WriteLine(dateTime + " - " + serializedActionLog);
            }
        }

        public static void Log(ExceptionViewModel model)
        {
            if (!System.IO.File.Exists(logFile))
                System.IO.File.Create(logFile).Close();

            using (StreamWriter textWriter = new StreamWriter(logFile, true))
            {
                string dateTime = DateTime.Now.ToString();
                textWriter.WriteLine(dateTime + " - " + model.Exception.Message + " - " + model.StackTrace + " - " + model.Exception.Source);
            }
        }

        public static void Log(Exception exception)
        {
            if (!System.IO.File.Exists(logFile))
                System.IO.File.Create(logFile).Close();

            using (StreamWriter textWriter = new StreamWriter(logFile, true))
            {
                string dateTime = DateTime.Now.ToString();
                textWriter.WriteLine(dateTime + " - " + exception.Message + " - " + exception.StackTrace + " - " + exception.Source);
            }
        }
    }
}