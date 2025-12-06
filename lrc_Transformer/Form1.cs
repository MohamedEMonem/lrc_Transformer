using lrc_Transformer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lrc_Transformer
{
    public partial class Form1 : Form
    {
        private GeminiService? _geminiService;

        public Form1()
        {
            InitializeComponent();

            // Get Key
            string apiKey = Environment.GetEnvironmentVariable("GEMINI_API_KEY");

            // If you don't have the Environment Variable set yet, 
            // uncomment the line below and paste your key to test:
            //string apiKey = "Your_API_here";

            if (!string.IsNullOrEmpty(apiKey))
            {
                _geminiService = new GeminiService(apiKey);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Load saved prompt or default
            txtSystemPrompt.Text = Properties.Settings.Default.SavedPrompt;
            if (string.IsNullOrEmpty(txtSystemPrompt.Text))
            {
                txtSystemPrompt.Text = "Transliterate Japanese to Romaji. Keep timestamps.";
            }
            UpdateUI();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.SavedPrompt = txtSystemPrompt.Text;
            Properties.Settings.Default.Save();
        }

        private void chkReplaceOriginal_CheckedChanged(object sender, EventArgs e) => UpdateUI();

        private void UpdateUI()
        {
            bool isReplace = chkReplaceOriginal.Checked;
            txtOutputPath.Enabled = !isReplace;
            btnBrowseOutput.Enabled = !isReplace;
            if (isReplace && !string.IsNullOrEmpty(txtInputPath.Text)) txtOutputPath.Text = txtInputPath.Text;
        }

        private void btnBrowseInput_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog { Filter = "LRC|*.lrc|All|*.*" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtInputPath.Text = ofd.FileName;
                    if (chkReplaceOriginal.Checked) txtOutputPath.Text = ofd.FileName;
                }
            }
        }

        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog { Filter = "LRC|*.lrc" })
            {
                if (sfd.ShowDialog() == DialogResult.OK) txtOutputPath.Text = sfd.FileName;
            }
        }

        private async void btnProcess_Click(object sender, EventArgs e)
        {
            // 1. API Check
            if (_geminiService == null)
            {
                MessageBox.Show("API Key is missing! Set GEMINI_API_KEY environment variable.");
                return;
            }

            if (string.IsNullOrEmpty(txtInputPath.Text) || !File.Exists(txtInputPath.Text)) return;

            try
            {
                // 2. UI Updates
                btnProcess.Enabled = false;
                progressBar1.Visible = true;

                // 3. Read the File
                string content = await File.ReadAllTextAsync(txtInputPath.Text);


                string userInstruction = txtSystemPrompt.Text;

                // If the user left the box empty, use the default Transliteration prompt
                if (string.IsNullOrWhiteSpace(userInstruction))
                {
                    userInstruction = "Transliterate these Japanese lyrics to Romaji. Keep timestamps exact.";
                }

                // 4. Call Gemini with the confirmed instruction
                string result = await _geminiService.TransformLrcAsync(userInstruction, content);

                // 5. Cleanup & Save
                result = CleanMarkdown(result);

                string savePath = chkReplaceOriginal.Checked ? txtInputPath.Text : txtOutputPath.Text;

                if (chkReplaceOriginal.Checked) File.Copy(savePath, savePath + ".bak", true);

                await File.WriteAllTextAsync(savePath, result, Encoding.UTF8);
                MessageBox.Show("Done!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                btnProcess.Enabled = true;
                progressBar1.Visible = false;
            }
        }
        private string CleanMarkdown(string text)
        {
            var lines = text.Split('\n').ToList();
            if (lines.Count > 0 && lines[0].Contains("```")) lines.RemoveAt(0);
            if (lines.Count > 0 && lines[lines.Count - 1].Contains("```")) lines.RemoveAt(lines.Count - 1);
            return string.Join("\n", lines).Trim();
        }
    }
}