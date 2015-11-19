namespace XlcmAuto
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.configFilePath = new System.Windows.Forms.TextBox();
            this.configOpen = new System.Windows.Forms.Button();
            this.configFile = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.usageHint = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // configFilePath
            // 
            this.configFilePath.Location = new System.Drawing.Point(77, 12);
            this.configFilePath.Name = "configFilePath";
            this.configFilePath.ReadOnly = true;
            this.configFilePath.Size = new System.Drawing.Size(408, 20);
            this.configFilePath.TabIndex = 0;
            // 
            // configOpen
            // 
            this.configOpen.Location = new System.Drawing.Point(491, 9);
            this.configOpen.Name = "configOpen";
            this.configOpen.Size = new System.Drawing.Size(75, 23);
            this.configOpen.TabIndex = 1;
            this.configOpen.Text = "打开";
            this.configOpen.UseVisualStyleBackColor = true;
            this.configOpen.Click += new System.EventHandler(this.configOpen_Click);
            // 
            // configFile
            // 
            this.configFile.AutoSize = true;
            this.configFile.Location = new System.Drawing.Point(13, 18);
            this.configFile.Name = "configFile";
            this.configFile.Size = new System.Drawing.Size(58, 13);
            this.configFile.TabIndex = 2;
            this.configFile.Text = " 配置文件";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(16, 72);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 3;
            this.startButton.Text = "开始";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(97, 72);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 4;
            this.stopButton.Text = "停止";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // usageHint
            // 
            this.usageHint.AutoSize = true;
            this.usageHint.Location = new System.Drawing.Point(13, 44);
            this.usageHint.Name = "usageHint";
            this.usageHint.Size = new System.Drawing.Size(556, 13);
            this.usageHint.TabIndex = 5;
            this.usageHint.Text = "先载入配置文件，然后买好票，到青龙、黄龙门口，选图界面，选中要打的[青龙]or[黄龙]之后，点击开始";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 107);
            this.Controls.Add(this.usageHint);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.configFile);
            this.Controls.Add(this.configOpen);
            this.Controls.Add(this.configFilePath);
            this.Name = "Form1";
            this.Text = "自动青龙黄龙";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox configFilePath;
        private System.Windows.Forms.Button configOpen;
        private System.Windows.Forms.Label configFile;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Label usageHint;
    }
}

