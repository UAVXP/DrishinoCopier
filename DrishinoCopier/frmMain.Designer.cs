namespace DrishinoCopier
{
	partial class frmMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.btnUpdate = new System.Windows.Forms.Button();
			this.lbMaterials = new System.Windows.Forms.ListBox();
			this.lbModels = new System.Windows.Forms.ListBox();
			this.lbSounds = new System.Windows.Forms.ListBox();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.btnOpenFile = new System.Windows.Forms.Button();
			this.btnCopy = new System.Windows.Forms.Button();
			this.tbSourceFolder = new System.Windows.Forms.TextBox();
			this.tbDestFolder = new System.Windows.Forms.TextBox();
			this.lblSourceFolder = new System.Windows.Forms.Label();
			this.lblDestFolder = new System.Windows.Forms.Label();
			this.btnSelectSourceFolder = new System.Windows.Forms.Button();
			this.btnSelectDestFolder = new System.Windows.Forms.Button();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.folderBrowserDialog2 = new System.Windows.Forms.FolderBrowserDialog();
			this.btnAbout = new System.Windows.Forms.Button();
			this.tbFilePath = new System.Windows.Forms.TextBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.cbCheckHL2Folder = new System.Windows.Forms.CheckBox();
			this.tcResourceList = new System.Windows.Forms.TabControl();
			this.tpMaterials = new System.Windows.Forms.TabPage();
			this.tpModels = new System.Windows.Forms.TabPage();
			this.tpSounds = new System.Windows.Forms.TabPage();
			this.button1 = new System.Windows.Forms.Button();
			this.rtbLog = new System.Windows.Forms.RichTextBox();
			this.btnGenerateList = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.statusStrip1.SuspendLayout();
			this.tcResourceList.SuspendLayout();
			this.tpMaterials.SuspendLayout();
			this.tpModels.SuspendLayout();
			this.tpSounds.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnUpdate
			// 
			resources.ApplyResources(this.btnUpdate, "btnUpdate");
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.UseVisualStyleBackColor = true;
			this.btnUpdate.Click += new System.EventHandler(this.button1_Click);
			// 
			// lbMaterials
			// 
			resources.ApplyResources(this.lbMaterials, "lbMaterials");
			this.lbMaterials.FormattingEnabled = true;
			this.lbMaterials.Name = "lbMaterials";
			// 
			// lbModels
			// 
			resources.ApplyResources(this.lbModels, "lbModels");
			this.lbModels.FormattingEnabled = true;
			this.lbModels.Name = "lbModels";
			// 
			// lbSounds
			// 
			resources.ApplyResources(this.lbSounds, "lbSounds");
			this.lbSounds.FormattingEnabled = true;
			this.lbSounds.Name = "lbSounds";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.DefaultExt = "*.vmf";
			this.openFileDialog1.FileName = "*.vmf";
			resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
			this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
			// 
			// btnOpenFile
			// 
			resources.ApplyResources(this.btnOpenFile, "btnOpenFile");
			this.btnOpenFile.Name = "btnOpenFile";
			this.btnOpenFile.UseVisualStyleBackColor = true;
			this.btnOpenFile.Click += new System.EventHandler(this.button2_Click);
			// 
			// btnCopy
			// 
			resources.ApplyResources(this.btnCopy, "btnCopy");
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.UseVisualStyleBackColor = true;
			this.btnCopy.Click += new System.EventHandler(this.button3_Click);
			// 
			// tbSourceFolder
			// 
			resources.ApplyResources(this.tbSourceFolder, "tbSourceFolder");
			this.tbSourceFolder.Name = "tbSourceFolder";
			this.tbSourceFolder.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// tbDestFolder
			// 
			resources.ApplyResources(this.tbDestFolder, "tbDestFolder");
			this.tbDestFolder.Name = "tbDestFolder";
			this.tbDestFolder.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
			// 
			// lblSourceFolder
			// 
			resources.ApplyResources(this.lblSourceFolder, "lblSourceFolder");
			this.lblSourceFolder.Name = "lblSourceFolder";
			// 
			// lblDestFolder
			// 
			resources.ApplyResources(this.lblDestFolder, "lblDestFolder");
			this.lblDestFolder.Name = "lblDestFolder";
			// 
			// btnSelectSourceFolder
			// 
			resources.ApplyResources(this.btnSelectSourceFolder, "btnSelectSourceFolder");
			this.btnSelectSourceFolder.Name = "btnSelectSourceFolder";
			this.btnSelectSourceFolder.UseVisualStyleBackColor = true;
			this.btnSelectSourceFolder.Click += new System.EventHandler(this.button4_Click);
			// 
			// btnSelectDestFolder
			// 
			resources.ApplyResources(this.btnSelectDestFolder, "btnSelectDestFolder");
			this.btnSelectDestFolder.Name = "btnSelectDestFolder";
			this.btnSelectDestFolder.UseVisualStyleBackColor = true;
			this.btnSelectDestFolder.Click += new System.EventHandler(this.button5_Click);
			// 
			// folderBrowserDialog1
			// 
			resources.ApplyResources(this.folderBrowserDialog1, "folderBrowserDialog1");
			this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
			this.folderBrowserDialog1.ShowNewFolderButton = false;
			// 
			// folderBrowserDialog2
			// 
			resources.ApplyResources(this.folderBrowserDialog2, "folderBrowserDialog2");
			this.folderBrowserDialog2.RootFolder = System.Environment.SpecialFolder.MyComputer;
			// 
			// btnAbout
			// 
			resources.ApplyResources(this.btnAbout, "btnAbout");
			this.btnAbout.Name = "btnAbout";
			this.btnAbout.UseVisualStyleBackColor = true;
			this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
			// 
			// tbFilePath
			// 
			resources.ApplyResources(this.tbFilePath, "tbFilePath");
			this.tbFilePath.Name = "tbFilePath";
			this.tbFilePath.TextChanged += new System.EventHandler(this.tbFilePath_TextChanged);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripProgressBar1});
			resources.ApplyResources(this.statusStrip1, "statusStrip1");
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.SizingGrip = false;
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			resources.ApplyResources(this.toolStripStatusLabel2, "toolStripStatusLabel2");
			this.toolStripStatusLabel2.Spring = true;
			// 
			// toolStripProgressBar1
			// 
			this.toolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripProgressBar1.Name = "toolStripProgressBar1";
			resources.ApplyResources(this.toolStripProgressBar1, "toolStripProgressBar1");
			// 
			// backgroundWorker1
			// 
			this.backgroundWorker1.WorkerReportsProgress = true;
			this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
			this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			// 
			// cbCheckHL2Folder
			// 
			resources.ApplyResources(this.cbCheckHL2Folder, "cbCheckHL2Folder");
			this.cbCheckHL2Folder.Checked = true;
			this.cbCheckHL2Folder.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbCheckHL2Folder.Name = "cbCheckHL2Folder";
			this.cbCheckHL2Folder.UseVisualStyleBackColor = true;
			// 
			// tcResourceList
			// 
			resources.ApplyResources(this.tcResourceList, "tcResourceList");
			this.tcResourceList.Controls.Add(this.tpMaterials);
			this.tcResourceList.Controls.Add(this.tpModels);
			this.tcResourceList.Controls.Add(this.tpSounds);
			this.tcResourceList.Name = "tcResourceList";
			this.tcResourceList.SelectedIndex = 0;
			// 
			// tpMaterials
			// 
			this.tpMaterials.Controls.Add(this.lbMaterials);
			resources.ApplyResources(this.tpMaterials, "tpMaterials");
			this.tpMaterials.Name = "tpMaterials";
			this.tpMaterials.UseVisualStyleBackColor = true;
			// 
			// tpModels
			// 
			this.tpModels.Controls.Add(this.lbModels);
			resources.ApplyResources(this.tpModels, "tpModels");
			this.tpModels.Name = "tpModels";
			this.tpModels.UseVisualStyleBackColor = true;
			// 
			// tpSounds
			// 
			this.tpSounds.Controls.Add(this.lbSounds);
			resources.ApplyResources(this.tpSounds, "tpSounds");
			this.tpSounds.Name = "tpSounds";
			this.tpSounds.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			resources.ApplyResources(this.button1, "button1");
			this.button1.Name = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// rtbLog
			// 
			resources.ApplyResources(this.rtbLog, "rtbLog");
			this.rtbLog.ForeColor = System.Drawing.SystemColors.WindowText;
			this.rtbLog.Name = "rtbLog";
			this.rtbLog.ReadOnly = true;
			// 
			// btnGenerateList
			// 
			resources.ApplyResources(this.btnGenerateList, "btnGenerateList");
			this.btnGenerateList.Name = "btnGenerateList";
			this.btnGenerateList.UseVisualStyleBackColor = true;
			this.btnGenerateList.Click += new System.EventHandler(this.btnGenerateList_Click);
			// 
			// button2
			// 
			resources.ApplyResources(this.button2, "button2");
			this.button2.Name = "button2";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click_1);
			// 
			// button3
			// 
			resources.ApplyResources(this.button3, "button3");
			this.button3.Name = "button3";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click_1);
			// 
			// frmMain
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.btnGenerateList);
			this.Controls.Add(this.rtbLog);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.tcResourceList);
			this.Controls.Add(this.cbCheckHL2Folder);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.tbFilePath);
			this.Controls.Add(this.btnAbout);
			this.Controls.Add(this.btnSelectDestFolder);
			this.Controls.Add(this.btnSelectSourceFolder);
			this.Controls.Add(this.lblDestFolder);
			this.Controls.Add(this.lblSourceFolder);
			this.Controls.Add(this.tbDestFolder);
			this.Controls.Add(this.tbSourceFolder);
			this.Controls.Add(this.btnCopy);
			this.Controls.Add(this.btnOpenFile);
			this.Controls.Add(this.btnUpdate);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "frmMain";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.tcResourceList.ResumeLayout(false);
			this.tpMaterials.ResumeLayout(false);
			this.tpModels.ResumeLayout(false);
			this.tpSounds.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnUpdate;
		private System.Windows.Forms.ListBox lbMaterials;
		private System.Windows.Forms.ListBox lbModels;
		private System.Windows.Forms.ListBox lbSounds;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Button btnOpenFile;
		private System.Windows.Forms.Button btnCopy;
		private System.Windows.Forms.TextBox tbSourceFolder;
		private System.Windows.Forms.TextBox tbDestFolder;
		private System.Windows.Forms.Label lblSourceFolder;
		private System.Windows.Forms.Label lblDestFolder;
		private System.Windows.Forms.Button btnSelectSourceFolder;
		private System.Windows.Forms.Button btnSelectDestFolder;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog2;
		private System.Windows.Forms.Button btnAbout;
		private System.Windows.Forms.TextBox tbFilePath;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
		private System.Windows.Forms.CheckBox cbCheckHL2Folder;
		private System.Windows.Forms.TabControl tcResourceList;
		private System.Windows.Forms.TabPage tpMaterials;
		private System.Windows.Forms.TabPage tpModels;
		private System.Windows.Forms.TabPage tpSounds;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.RichTextBox rtbLog;
		private System.Windows.Forms.Button btnGenerateList;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
	}
}

