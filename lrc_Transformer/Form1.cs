using lrc_Transformer;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lrc_Transformer
{
    public partial class Form1 : Form
    {
        private PythonRomajiService _romajiService;
        
        public Form1()
        {
            InitializeComponent();
            _romajiService = new PythonRomajiService();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 1. Load Settings
            txtSystemPrompt.Text = Properties.Settings.Default.SavedPrompt;
            if (string.IsNullOrEmpty(txtSystemPrompt.Text)) 
                txtSystemPrompt.Text = "Transliterate Japanese to Romaji. Keep timestamps.";

            // 2. Setup Defaults
            radioOffline.Checked = true;        // Default to Offline
            
            // Try to load API key from Env Var first, then UI
            string envKey = Environment.GetEnvironmentVariable("GEMINI_API_KEY");
            if (!string.IsNullOrEmpty(envKey)) txtApiKey.Text = envKey;

            UpdateUIState();
        }

        // Run this whenever the user switches Radio Buttons
        private void radioOffline_CheckedChanged(object sender, EventArgs e) => UpdateUIState();
        private void radioGemini_CheckedChanged(object sender, EventArgs e) => UpdateUIState();

        private void UpdateUIState()
        {
            bool isGemini = radioGemini.Checked;

            // Toggle visibility based on selection
            txtApiKey.Enabled = isGemini;
            txtSystemPrompt.Enabled = isGemini;
        }

        private async void btnProcess_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtInputPath.Text) || !File.Exists(txtInputPath.Text))
            {
                MessageBox.Show("Please select a valid input file.");
                return;
            }

            // Determine Output Path
            string savePath = chkReplaceOriginal.Checked ? txtInputPath.Text : txtOutputPath.Text;
            if (!chkReplaceOriginal.Checked && string.IsNullOrEmpty(savePath))
            {
                 string dir = Path.GetDirectoryName(txtInputPath.Text);
                 string name = Path.GetFileNameWithoutExtension(txtInputPath.Text);
                 savePath = Path.Combine(dir, $"{name}_romaji.lrc");
            }

            // Backup if replacing
            if (chkReplaceOriginal.Checked) File.Copy(savePath, savePath + ".bak", true);

            btnProcess.Enabled = false;
            progressBar1.Visible = true;

            try
            {
                if (radioOffline.Checked)
                {
                    // --- OPTION A: OFFLINE ROMAJI ENGINE ---
                    await _romajiService.ConvertFileAsync(txtInputPath.Text, savePath);
                }
                else
                {
                    // --- OPTION B: GEMINI API ---
                    string key = txtApiKey.Text.Trim();
                    if (string.IsNullOrEmpty(key)) 
                    {
                        MessageBox.Show("Please enter a Gemini API Key.");
                        return;
                    }

                    // Initialize Service on the fly with the provided key
                    var gemini = new GeminiService(key);
                    string content = await File.ReadAllTextAsync(txtInputPath.Text);
                    string prompt = txtSystemPrompt.Text;
                    
                    string result = await gemini.TransformLrcAsync(prompt, content);
                    result = CleanMarkdown(result);
                    
                    await File.WriteAllTextAsync(savePath, result, Encoding.UTF8);
                }

                MessageBox.Show($"Success! File saved to:\n{savePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                btnProcess.Enabled = true;
                progressBar1.Visible = false;
            }
        }

        // Helper to clean Gemini markdown output
        private string CleanMarkdown(string text)
        {
            var lines = text.Split('\n').ToList();
            if (lines.Count > 0 && lines[0].Contains("```")) lines.RemoveAt(0);
            if (lines.Count > 0 && lines[lines.Count - 1].Contains("```")) lines.RemoveAt(lines.Count - 1);
            return string.Join("\n", lines).Trim();
        }

        // ... Keep your existing Browse/Save handlers ...
        private void btnBrowseInput_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog { Filter = "LRC|*.lrc|All|*.*" }) {
                if (ofd.ShowDialog() == DialogResult.OK) txtInputPath.Text = ofd.FileName;
            }
        }
        private void btnBrowseOutput_Click(object sender, EventArgs e) {
            using (var sfd = new SaveFileDialog { Filter = "LRC|*.lrc" }) {
                if (sfd.ShowDialog() == DialogResult.OK) txtOutputPath.Text = sfd.FileName;
            }
        }
        private void chkReplaceOriginal_CheckedChanged(object sender, EventArgs e) {
            bool isReplace = chkReplaceOriginal.Checked;
            txtOutputPath.Enabled = !isReplace;
            btnBrowseOutput.Enabled = !isReplace;
            if (isReplace) txtOutputPath.Text = txtInputPath.Text;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            Properties.Settings.Default.SavedPrompt = txtSystemPrompt.Text;
            Properties.Settings.Default.Save();
        }
    }
}