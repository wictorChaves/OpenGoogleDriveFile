using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading;

namespace OpenGoogleDriveFile
{
    public class Program
    {

        public static void Main(string[] args)
        {
            if (args.Length == 0) Environment.Exit(0);
            Thread thread = new Thread(Program.Start);
            thread.Start(args[0]);
        }

        public static void Start(object fileUrl)
        {
            string browser = ConfigurationManager.AppSettings["browser"];
            using (StreamReader r = new StreamReader(fileUrl.ToString()))
            {
                string json = r.ReadToEnd();
                Model model = JsonConvert.DeserializeObject<Model>(json);
                _openBrowser(browser, model.url);
            }
        }

        private static void _openBrowser(string browser, string file)
        {
            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
            pProcess.StartInfo.FileName = browser;
            pProcess.StartInfo.Arguments = file;
            pProcess.StartInfo.UseShellExecute = false;
            pProcess.StartInfo.RedirectStandardOutput = true;
            pProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            pProcess.StartInfo.CreateNoWindow = false;
            pProcess.Start();
            string output = pProcess.StandardOutput.ReadToEnd();
            pProcess.WaitForExit();
        }
    }
}
