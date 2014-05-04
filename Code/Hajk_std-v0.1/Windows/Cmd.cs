using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace Hajk_std.Windows
{
    public class Cmd
    {
        public static void RunCommand(string command)
        {
            var p = new Process
            {
                StartInfo = new ProcessStartInfo("cmd", "/c \"dir c:\\\"")
                {
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    StandardOutputEncoding = Encoding.GetEncoding("utf-8"),
                    UseShellExecute = false,
                    CreateNoWindow = false
                }
            };

            p.Start();

            Console.ReadLine();

            // Poslání příkazu
            p.StandardInput.WriteLine(command);
            p.StandardOutput.ReadLine();

            // ukončení konzole
            p.WaitForExit();
        }

        /// <summary>
        /// Execute the command Asynchronously.
        /// </summary>
        /// <param name="command">string command.</param>
        public void ExecuteCommandAsync(string command)
        {
            try
            {
                //Asynchronously start the Thread to process the Execute command request.
                var objThread = new Thread(new ParameterizedThreadStart(ExecuteCommandSync));
                //Make the thread as background thread.
                objThread.IsBackground = true;
                //Set the Priority of the thread.
                objThread.Priority = ThreadPriority.AboveNormal;
                //Start the thread.
                objThread.Start(command);
            }
            catch (ThreadStartException ex)
            {
                throw;
            }
            catch (ThreadAbortException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Executes a shell command synchronously.
        /// </summary>
        /// <param name="command">string command</param>
        /// <returns>string, as output of the command.</returns>
        public void ExecuteCommandSync(object command)
        {
            try
            {
                var procStartInfo = new ProcessStartInfo("cmd", "/c " + command)
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = false
                };

                var proc = new Process
                {
                    StartInfo = procStartInfo
                };

                proc.Start();

                // Get output
                string result = proc.StandardOutput.ReadToEnd();

                // Display output.
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
