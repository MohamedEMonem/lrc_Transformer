using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace lrc_Transformer
{
    public class PythonRomajiService
    {
        private static string ProjectRoot
        {
            get
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;

                // 1. Release Mode: Look for the engine right next to the app
                if (File.Exists(Path.Combine(baseDir, "romaji_engine.exe")))
                {
                    return baseDir; 
                }

                // 2. Dev Mode: Look inside the dist folder where PyInstaller put it
                string devPath = Path.GetFullPath(Path.Combine(baseDir, @"..\..\..\romaji_Convertor\dist"));
                if (File.Exists(Path.Combine(devPath, "romaji_engine.exe")))
                {
                    return devPath;
                }

                return baseDir;
            }
        }

        // Point directly to the compiled engine
        private static string EnginePath => Path.Combine(ProjectRoot, "romaji_engine.exe");

        public async Task ConvertFileAsync(string inputPath, string outputPath)
        {
            if (!File.Exists(EnginePath))
                throw new FileNotFoundException($"Romaji Engine not found at: {EnginePath}");

            // OLD: "python.exe" "script.py" "arg1" "arg2"
            // NEW: "romaji_engine.exe" "arg1" "arg2"
            string arguments = $"\"{inputPath}\" \"{outputPath}\"";

            var startInfo = new ProcessStartInfo
            {
                FileName = EnginePath,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using (var process = Process.Start(startInfo))
            {
                if (process == null)
                    throw new Exception("Failed to start Romaji Engine process");

                string output = await process.StandardOutput.ReadToEndAsync();
                string error = await process.StandardError.ReadToEndAsync();
                await process.WaitForExitAsync();

                if (!output.Contains("DONE_SUCCESS"))
                {
                    throw new Exception($"Engine Failed. Error: {error} \nOutput: {output}");
                }
            }
        }
    }
}