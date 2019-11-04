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
            var response = Bat(command, filename, workingDirectory);
            return response == null
                ? response
                : response.Trim();
        }

        public string Bat(string cmd, string filename = null, string workingDirectory = null)
        {
            var escapedArgs = cmd != null ? cmd.Replace("\"", "\\\"") : string.Empty;
            var file = filename ?? "cmd.exe";
            var cmdString = filename == null
                ? $"/c \"{escapedArgs}\""
                : escapedArgs;
            string result = InvokeRunCommand(file, cmdString, workingDirectory);
            return result;
        }
        
        private string InvokeRunCommand(string filename, string arguments, string workingDirectory = null)
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
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return result;
        }
    }
}
