using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace lrc_Transformer
{
    public class PythonRomajiService
    {
        // Change from 'const' to properties or readonly fields so we can calculate paths dynamically
        private static string ProjectRoot
        {
            get
            {
                // Get the directory where the .exe is running (e.g., bin/Debug/net6.0)
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;

                // Move up 3 levels to get out of bin/Debug/netX.0 to the actual Project Root
                // Adjust the number of "..\" if your folder structure is different
                return Path.GetFullPath(Path.Combine(baseDir, @"..\..\..\"));
            }
        }

        private static string PythonExePath => Path.Combine(ProjectRoot, @"romaji_Convertor\.venv\Scripts\python.exe");
        private static string ScriptPath => Path.Combine(ProjectRoot, @"romaji_Convertor\main.py");

        public async Task ConvertFileAsync(string inputPath, string outputPath, string mode)
        {
            // Verify files exist before running to avoid confusing Python errors
            if (!File.Exists(PythonExePath))
                throw new FileNotFoundException($"Python executable not found at: {PythonExePath}");
            
            if (!File.Exists(ScriptPath))
                throw new FileNotFoundException($"Python script not found at: {ScriptPath}");

            // We now pass 3 arguments: Script, Input, Output, Mode
            string arguments = $"\"{ScriptPath}\" \"{inputPath}\" \"{outputPath}\" \"{mode}\"";

            var startInfo = new ProcessStartInfo
            {
                FileName = PythonExePath,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using (var process = Process.Start(startInfo))
            {
                string output = await process.StandardOutput.ReadToEndAsync();
                string error = await process.StandardError.ReadToEndAsync();
                await process.WaitForExitAsync();

                if (!output.Contains("DONE_SUCCESS"))
                {
                    throw new Exception($"Python Failed. Error: {error} \nOutput: {output}");
                }
            }
        }
    }
}