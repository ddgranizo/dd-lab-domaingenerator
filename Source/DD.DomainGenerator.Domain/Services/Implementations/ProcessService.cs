using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DD.DomainGenerator.Services.Implementations
{
    public class ProcessService : IProcessService
    {
        public ProcessService()
        {

        }

        public string RunCommand(string command, string filename = null, string workingDirectory = null)
        {
            return RunCommand(command, filename, workingDirectory, new string[] { });
        }

        public string RunCommand(string command, string filename = null, string workingDirectory = null, params string[] inputs)
        {
            var response = Bat(command, filename, workingDirectory, inputs);
            return response == null
                ? response
                : response.Trim();
        }

        public string Bat(string cmd, string filename, string workingDirectory, string[] inputs)
        {
            var escapedArgs = cmd != null ? cmd.Replace("\"", "\\\"") : string.Empty;
            var file = filename ?? "cmd.exe";
            var cmdString = filename == null
                ? $"/c \"{escapedArgs}\""
                : escapedArgs;
            string result = InvokeRunCommand(file, cmdString, workingDirectory, inputs);
            return result;
        }

        private string InvokeRunCommand(string filename, string arguments, string workingDirectory, string[] inputs)
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = filename,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = false,
                }
            };
            if (workingDirectory != null)
            {
                process.StartInfo.WorkingDirectory = workingDirectory;
            }
            process.Start();
            if (inputs.Length > 0)
            {
                using (var writer = process.StandardInput)
                {
                    foreach (var input in inputs)
                    {
                        System.Threading.Thread.Sleep(1000);
                        writer.WriteLine(input);
                    }
                }
            }
            
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return result;
        }


    }
}
