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
            SuspendLayout();
            // 
            // txtSystemPrompt
            // 
            txtSystemPrompt.Location = new Point(12, 50);
            txtSystemPrompt.Multiline = true;
            txtSystemPrompt.Name = "txtSystemPrompt";
            txtSystemPrompt.ScrollBars = ScrollBars.Vertical;
            txtSystemPrompt.Size = new Size(349, 100);
            txtSystemPrompt.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 27);
            label1.Name = "label1";
            label1.Size = new Size(109, 20);
            label1.TabIndex = 1;
            label1.Text = "System Prompt";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(19, 178);
            label2.Name = "label2";
            label2.Size = new Size(99, 20);
            label2.TabIndex = 2;
            label2.Text = "Input LRC File";
            // 
            // txtInputPath
            // 
            txtInputPath.Location = new Point(12, 201);
            txtInputPath.Name = "txtInputPath";
            txtInputPath.Size = new Size(349, 27);
            txtInputPath.TabIndex = 3;
            // 
            // btnBrowseInput
            // 
            btnBrowseInput.Location = new Point(367, 199);
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
            chkReplaceOriginal.Location = new Point(14, 257);
            chkReplaceOriginal.Name = "chkReplaceOriginal";
            chkReplaceOriginal.Size = new Size(168, 24);
            chkReplaceOriginal.TabIndex = 5;
            chkReplaceOriginal.Text = "Replace Original File";
            chkReplaceOriginal.UseVisualStyleBackColor = true;
            chkReplaceOriginal.Click += chkReplaceOriginal_CheckedChanged;
            // 
            // txtOutputPath
            // 
            txtOutputPath.Location = new Point(14, 294);
            txtOutputPath.Name = "txtOutputPath";
            txtOutputPath.Size = new Size(347, 27);
            txtOutputPath.TabIndex = 6;
            // 
            // btnBrowseOutput
            // 
            btnBrowseOutput.Location = new Point(367, 293);
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
            btnProcess.Location = new Point(604, 386);
            btnProcess.Name = "btnProcess";
            btnProcess.Size = new Size(184, 52);
            btnProcess.TabIndex = 8;
            btnProcess.Text = "TRANSFORM";
            btnProcess.UseVisualStyleBackColor = true;
            btnProcess.Click += btnProcess_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(145, 398);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(453, 29);
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.TabIndex = 9;
            progressBar1.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
            Text = "Form1";
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
    }
}
