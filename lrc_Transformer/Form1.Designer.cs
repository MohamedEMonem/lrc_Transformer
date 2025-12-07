namespace lrc_Transformer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtSystemPrompt = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txtInputPath = new TextBox();
            btnBrowseInput = new Button();
            chkReplaceOriginal = new CheckBox();
            txtOutputPath = new TextBox();
            btnBrowseOutput = new Button();
            btnProcess = new Button();
            progressBar1 = new ProgressBar();
            
            // --- NEW CONTROLS ---
            grpMethod = new GroupBox();
            radioGemini = new RadioButton();
            radioOffline = new RadioButton();
            txtApiKey = new TextBox();
            // --------------------

            grpMethod.SuspendLayout();
            SuspendLayout();

            // 
            // label1 (System Prompt Label)
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(109, 20);
            label1.TabIndex = 1;
            label1.Text = "System Prompt";

            // 
            // txtSystemPrompt
            // 
            txtSystemPrompt.Location = new Point(12, 38);
            txtSystemPrompt.Multiline = true;
            txtSystemPrompt.Name = "txtSystemPrompt";
            txtSystemPrompt.ScrollBars = ScrollBars.Vertical;
            txtSystemPrompt.Size = new Size(349, 60);
            txtSystemPrompt.TabIndex = 0;

            // 
            // grpMethod (New GroupBox for Method Selection)
            // 
            grpMethod.Controls.Add(txtApiKey);
            grpMethod.Controls.Add(radioGemini);
            grpMethod.Controls.Add(radioOffline);
            grpMethod.Location = new Point(12, 110);
            grpMethod.Name = "grpMethod";
            grpMethod.Size = new Size(429, 100);
            grpMethod.TabIndex = 10;
            grpMethod.TabStop = false;
            grpMethod.Text = "Processing Method";

            // 
            // radioOffline
            // 
            radioOffline.AutoSize = true;
            radioOffline.Location = new Point(15, 26);
            radioOffline.Name = "radioOffline";
            radioOffline.Size = new Size(135, 24);
            radioOffline.TabIndex = 0;
            radioOffline.TabStop = true;
            radioOffline.Text = "Offline accuracy may vary (90+%)";
            radioOffline.UseVisualStyleBackColor = true;
            radioOffline.CheckedChanged += radioOffline_CheckedChanged;

            // 
            // radioGemini
            // 
            radioGemini.AutoSize = true;
            radioGemini.Location = new Point(15, 60);
            radioGemini.Name = "radioGemini";
            radioGemini.Size = new Size(97, 24);
            radioGemini.TabIndex = 1;
            radioGemini.TabStop = true;
            radioGemini.Text = "Gemini AI";
            radioGemini.UseVisualStyleBackColor = true;
            radioGemini.CheckedChanged += radioGemini_CheckedChanged;

            // 
            // txtApiKey
            // 
            txtApiKey.Location = new Point(118, 59);
            txtApiKey.Name = "txtApiKey";
            txtApiKey.PlaceholderText = "Paste Gemini API Key Here";
            txtApiKey.Size = new Size(290, 27);
            txtApiKey.TabIndex = 3;

            // 
            // label2 (Input File Label)
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 230);
            label2.Name = "label2";
            label2.Size = new Size(99, 20);
            label2.TabIndex = 2;
            label2.Text = "Input LRC File";

            // 
            // txtInputPath
            // 
            txtInputPath.Location = new Point(12, 253);
            txtInputPath.Name = "txtInputPath";
            txtInputPath.Size = new Size(349, 27);
            txtInputPath.TabIndex = 3;

            // 
            // btnBrowseInput
            // 
            btnBrowseInput.Location = new Point(367, 251);
            btnBrowseInput.Name = "btnBrowseInput";
            btnBrowseInput.Size = new Size(74, 29);
            btnBrowseInput.TabIndex = 4;
            btnBrowseInput.Text = "Browse...";
            btnBrowseInput.UseVisualStyleBackColor = true;
            btnBrowseInput.Click += btnBrowseInput_Click;

            // 
            // chkReplaceOriginal
            // 
            chkReplaceOriginal.AutoSize = true;
            chkReplaceOriginal.Location = new Point(14, 295);
            chkReplaceOriginal.Name = "chkReplaceOriginal";
            chkReplaceOriginal.Size = new Size(168, 24);
            chkReplaceOriginal.TabIndex = 5;
            chkReplaceOriginal.Text = "Replace Original File";
            chkReplaceOriginal.UseVisualStyleBackColor = true;
            chkReplaceOriginal.Click += chkReplaceOriginal_CheckedChanged;

            // 
            // txtOutputPath
            // 
            txtOutputPath.Location = new Point(14, 330);
            txtOutputPath.Name = "txtOutputPath";
            txtOutputPath.Size = new Size(347, 27);
            txtOutputPath.TabIndex = 6;

            // 
            // btnBrowseOutput
            // 
            btnBrowseOutput.Location = new Point(367, 329);
            btnBrowseOutput.Name = "btnBrowseOutput";
            btnBrowseOutput.Size = new Size(80, 29);
            btnBrowseOutput.TabIndex = 7;
            btnBrowseOutput.Text = "Save As...";
            btnBrowseOutput.UseVisualStyleBackColor = true;
            btnBrowseOutput.Click += btnBrowseOutput_Click;

            // 
            // btnProcess
            // 
            btnProcess.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnProcess.Location = new Point(14, 380);
            btnProcess.Name = "btnProcess";
            btnProcess.Size = new Size(427, 45);
            btnProcess.TabIndex = 8;
            btnProcess.Text = "TRANSFORM";
            btnProcess.UseVisualStyleBackColor = true;
            btnProcess.Click += btnProcess_Click;

            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(14, 435);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(427, 23);
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.TabIndex = 9;
            progressBar1.Visible = false;

            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(460, 480);
            Controls.Add(grpMethod); // Add the new group box
            Controls.Add(progressBar1);
            Controls.Add(btnProcess);
            Controls.Add(btnBrowseOutput);
            Controls.Add(txtOutputPath);
            Controls.Add(chkReplaceOriginal);
            Controls.Add(btnBrowseInput);
            Controls.Add(txtInputPath);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtSystemPrompt);
            Name = "Form1";
            Text = "LRC Transformer";
            Load += Form1_Load;
            grpMethod.ResumeLayout(false);
            grpMethod.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtSystemPrompt;
        private Label label1;
        private Label label2;
        private TextBox txtInputPath;
        private Button btnBrowseInput;
        private CheckBox chkReplaceOriginal;
        private TextBox txtOutputPath;
        private Button btnBrowseOutput;
        private Button btnProcess;
        private ProgressBar progressBar1;

        // --- NEW DECLARATIONS ---
        private GroupBox grpMethod;
        private RadioButton radioGemini;
        private RadioButton radioOffline;
        private TextBox txtApiKey;
    }
}