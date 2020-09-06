namespace Mr_Fish
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.cb_processes = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pb_audio = new System.Windows.Forms.ProgressBar();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.btn_calibrate = new System.Windows.Forms.Button();
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_stop = new System.Windows.Forms.Button();
            this.tb_volume = new System.Windows.Forms.TextBox();
            this.lbl_maxVol = new System.Windows.Forms.Label();
            this.lbl_curVol = new System.Windows.Forms.Label();
            this.lbl_maxVol_txt = new System.Windows.Forms.Label();
            this.lbl_curVol_txt = new System.Windows.Forms.Label();
            this.lbl_fished = new System.Windows.Forms.Label();
            this.lbl_fished_txt = new System.Windows.Forms.Label();
            this.statStrip = new System.Windows.Forms.StatusStrip();
            this.tssl_status = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssl_startTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_sound_txt = new System.Windows.Forms.Label();
            this.tt_main = new System.Windows.Forms.ToolTip(this.components);
            this.statStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // cb_processes
            // 
            this.cb_processes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_processes.FormattingEnabled = true;
            this.cb_processes.Location = new System.Drawing.Point(10, 25);
            this.cb_processes.Name = "cb_processes";
            this.cb_processes.Size = new System.Drawing.Size(283, 21);
            this.cb_processes.Sorted = true;
            this.cb_processes.TabIndex = 0;
            this.tt_main.SetToolTip(this.cb_processes, "Bitte hier den Minecraft Prozess auswählen.");
            this.cb_processes.SelectedIndexChanged += new System.EventHandler(this.cb_processes_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Minecraft Prozess auswählen:";
            // 
            // pb_audio
            // 
            this.pb_audio.Location = new System.Drawing.Point(10, 132);
            this.pb_audio.Name = "pb_audio";
            this.pb_audio.Size = new System.Drawing.Size(283, 20);
            this.pb_audio.TabIndex = 2;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 10;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // btn_calibrate
            // 
            this.btn_calibrate.Location = new System.Drawing.Point(10, 184);
            this.btn_calibrate.Name = "btn_calibrate";
            this.btn_calibrate.Size = new System.Drawing.Size(283, 23);
            this.btn_calibrate.TabIndex = 4;
            this.btn_calibrate.Text = "Anzeige zurücksetzen";
            this.tt_main.SetToolTip(this.btn_calibrate, "Setzt die gemessene Lautstärke zurück.");
            this.btn_calibrate.UseVisualStyleBackColor = true;
            this.btn_calibrate.Click += new System.EventHandler(this.btn_calibrate_Click);
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(10, 213);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(283, 23);
            this.btn_start.TabIndex = 5;
            this.btn_start.Text = "Starten";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.Location = new System.Drawing.Point(10, 242);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(283, 23);
            this.btn_stop.TabIndex = 6;
            this.btn_stop.Text = "Stoppen";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // tb_volume
            // 
            this.tb_volume.Location = new System.Drawing.Point(170, 158);
            this.tb_volume.Name = "tb_volume";
            this.tb_volume.Size = new System.Drawing.Size(123, 20);
            this.tb_volume.TabIndex = 7;
            this.tb_volume.Text = "3";
            this.tt_main.SetToolTip(this.tb_volume, "Lautstärke, bei welcher die Angel eingeholt werden soll.");
            this.tb_volume.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_volume_KeyPress);
            // 
            // lbl_maxVol
            // 
            this.lbl_maxVol.AutoSize = true;
            this.lbl_maxVol.Location = new System.Drawing.Point(253, 59);
            this.lbl_maxVol.Name = "lbl_maxVol";
            this.lbl_maxVol.Size = new System.Drawing.Size(40, 13);
            this.lbl_maxVol.TabIndex = 8;
            this.lbl_maxVol.Text = "0,0000";
            this.tt_main.SetToolTip(this.lbl_maxVol, "Nützlich um den Bot zu kalibrieren.");
            // 
            // lbl_curVol
            // 
            this.lbl_curVol.AutoSize = true;
            this.lbl_curVol.Location = new System.Drawing.Point(253, 84);
            this.lbl_curVol.Name = "lbl_curVol";
            this.lbl_curVol.Size = new System.Drawing.Size(40, 13);
            this.lbl_curVol.TabIndex = 9;
            this.lbl_curVol.Text = "0,0000";
            // 
            // lbl_maxVol_txt
            // 
            this.lbl_maxVol_txt.AutoSize = true;
            this.lbl_maxVol_txt.Location = new System.Drawing.Point(7, 59);
            this.lbl_maxVol_txt.Name = "lbl_maxVol_txt";
            this.lbl_maxVol_txt.Size = new System.Drawing.Size(162, 13);
            this.lbl_maxVol_txt.TabIndex = 10;
            this.lbl_maxVol_txt.Text = "Höchste Gemessene Lautstärke:";
            this.tt_main.SetToolTip(this.lbl_maxVol_txt, "Nützlich um den Bot zu kalibrieren.");
            // 
            // lbl_curVol_txt
            // 
            this.lbl_curVol_txt.AutoSize = true;
            this.lbl_curVol_txt.Location = new System.Drawing.Point(7, 84);
            this.lbl_curVol_txt.Name = "lbl_curVol_txt";
            this.lbl_curVol_txt.Size = new System.Drawing.Size(101, 13);
            this.lbl_curVol_txt.TabIndex = 11;
            this.lbl_curVol_txt.Text = "Aktuelle Lautstärke:";
            // 
            // lbl_fished
            // 
            this.lbl_fished.AutoSize = true;
            this.lbl_fished.Location = new System.Drawing.Point(253, 106);
            this.lbl_fished.Name = "lbl_fished";
            this.lbl_fished.Size = new System.Drawing.Size(37, 13);
            this.lbl_fished.TabIndex = 12;
            this.lbl_fished.Text = "00000";
            this.tt_main.SetToolTip(this.lbl_fished, "Anzahl der bereits absolvierten Durchläufe (1x Angel auswerfen und wieder einhole" +
        "n)");
            // 
            // lbl_fished_txt
            // 
            this.lbl_fished_txt.AutoSize = true;
            this.lbl_fished_txt.Location = new System.Drawing.Point(7, 106);
            this.lbl_fished_txt.Name = "lbl_fished_txt";
            this.lbl_fished_txt.Size = new System.Drawing.Size(86, 13);
            this.lbl_fished_txt.TabIndex = 13;
            this.lbl_fished_txt.Text = "Bereits geangelt:";
            this.tt_main.SetToolTip(this.lbl_fished_txt, "Anzahl der bereits absolvierten Durchläufe (1x Angel auswerfen und wieder einhole" +
        "n)");
            // 
            // statStrip
            // 
            this.statStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_status,
            this.tssl_startTime});
            this.statStrip.Location = new System.Drawing.Point(0, 272);
            this.statStrip.Name = "statStrip";
            this.statStrip.Size = new System.Drawing.Size(306, 22);
            this.statStrip.TabIndex = 14;
            this.statStrip.Text = "statusStrip1";
            // 
            // tssl_status
            // 
            this.tssl_status.Name = "tssl_status";
            this.tssl_status.Size = new System.Drawing.Size(45, 17);
            this.tssl_status.Text = "warte...";
            // 
            // tssl_startTime
            // 
            this.tssl_startTime.Name = "tssl_startTime";
            this.tssl_startTime.Size = new System.Drawing.Size(0, 17);
            // 
            // lbl_sound_txt
            // 
            this.lbl_sound_txt.AutoSize = true;
            this.lbl_sound_txt.Location = new System.Drawing.Point(7, 161);
            this.lbl_sound_txt.Name = "lbl_sound_txt";
            this.lbl_sound_txt.Size = new System.Drawing.Size(103, 13);
            this.lbl_sound_txt.TabIndex = 15;
            this.lbl_sound_txt.Text = "Auslöser Lautstärke:";
            this.tt_main.SetToolTip(this.lbl_sound_txt, "Lautstärke, bei welcher die Angel eingeholt werden soll.");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 294);
            this.Controls.Add(this.lbl_sound_txt);
            this.Controls.Add(this.statStrip);
            this.Controls.Add(this.lbl_fished_txt);
            this.Controls.Add(this.lbl_fished);
            this.Controls.Add(this.lbl_curVol_txt);
            this.Controls.Add(this.lbl_maxVol_txt);
            this.Controls.Add(this.lbl_curVol);
            this.Controls.Add(this.lbl_maxVol);
            this.Controls.Add(this.tb_volume);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.btn_calibrate);
            this.Controls.Add(this.pb_audio);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_processes);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Mr. Fish";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statStrip.ResumeLayout(false);
            this.statStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_processes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar pb_audio;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button btn_calibrate;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.TextBox tb_volume;
        private System.Windows.Forms.Label lbl_maxVol;
        private System.Windows.Forms.Label lbl_curVol;
        private System.Windows.Forms.Label lbl_maxVol_txt;
        private System.Windows.Forms.Label lbl_curVol_txt;
        private System.Windows.Forms.Label lbl_fished;
        private System.Windows.Forms.Label lbl_fished_txt;
        private System.Windows.Forms.StatusStrip statStrip;
        private System.Windows.Forms.ToolStripStatusLabel tssl_status;
        private System.Windows.Forms.Label lbl_sound_txt;
        private System.Windows.Forms.ToolTip tt_main;
        private System.Windows.Forms.ToolStripStatusLabel tssl_startTime;
    }
}

