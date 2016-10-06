namespace iWindow
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.connectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.terminateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeIPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.glControl1 = new OpenTK.GLControl();
            this.label1 = new System.Windows.Forms.Label();
            this.fBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.xBox = new System.Windows.Forms.TextBox();
            this.yBox = new System.Windows.Forms.TextBox();
            this.zBox = new System.Windows.Forms.TextBox();
            this.sBox = new System.Windows.Forms.TextBox();
            this.xOffseted = new System.Windows.Forms.TextBox();
            this.yOffseted = new System.Windows.Forms.TextBox();
            this.zOffseted = new System.Windows.Forms.TextBox();
            this.offsetButton = new System.Windows.Forms.Button();
            this.feedPlusButton = new System.Windows.Forms.Button();
            this.feedMinusButton = new System.Windows.Forms.Button();
            this.feedRatePercentTxtbox = new System.Windows.Forms.TextBox();
            this.alarmBtn = new System.Windows.Forms.Button();
            this.lineComplete = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusBox = new System.Windows.Forms.TextBox();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.ARTab = new System.Windows.Forms.TabPage();
            this.matRemoveButton = new System.Windows.Forms.Button();
            this.loadCalib = new System.Windows.Forms.Button();
            this.calibrateButton = new System.Windows.Forms.Button();
            this.clearLine = new System.Windows.Forms.Button();
            this.mdiTab = new System.Windows.Forms.TabPage();
            this.M06Number = new System.Windows.Forms.NumericUpDown();
            this.fIn = new System.Windows.Forms.TextBox();
            this.zIn = new System.Windows.Forms.TextBox();
            this.yIn = new System.Windows.Forms.TextBox();
            this.xIn = new System.Windows.Forms.TextBox();
            this.M03Param = new System.Windows.Forms.TextBox();
            this.M05Button = new System.Windows.Forms.Button();
            this.M06button = new System.Windows.Forms.Button();
            this.G01Button = new System.Windows.Forms.Button();
            this.G00Button = new System.Windows.Forms.Button();
            this.M03Button = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.jogTab = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.moveXup = new System.Windows.Forms.Button();
            this.moveXdown = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.moveYup = new System.Windows.Forms.Button();
            this.moveYdown = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.moveZdown = new System.Windows.Forms.Button();
            this.moveZup = new System.Windows.Forms.Button();
            this.memoryTab = new System.Windows.Forms.TabPage();
            this.runGCode = new System.Windows.Forms.Button();
            this.GCodeTxt = new System.Windows.Forms.TextBox();
            this.referenceTab = new System.Windows.Forms.TabPage();
            this.refButton = new System.Windows.Forms.Button();
            this.ModeControl = new System.Windows.Forms.TabControl();
            this.menuStrip1.SuspendLayout();
            this.ARTab.SuspendLayout();
            this.mdiTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.M06Number)).BeginInit();
            this.jogTab.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.memoryTab.SuspendLayout();
            this.referenceTab.SuspendLayout();
            this.ModeControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1350, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // connectionToolStripMenuItem
            // 
            this.connectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.terminateToolStripMenuItem,
            this.changeIPToolStripMenuItem});
            this.connectionToolStripMenuItem.Name = "connectionToolStripMenuItem";
            this.connectionToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.connectionToolStripMenuItem.Text = "Connection";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // terminateToolStripMenuItem
            // 
            this.terminateToolStripMenuItem.Enabled = false;
            this.terminateToolStripMenuItem.Name = "terminateToolStripMenuItem";
            this.terminateToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.terminateToolStripMenuItem.Text = "Terminate";
            this.terminateToolStripMenuItem.Click += new System.EventHandler(this.terminateToolStripMenuItem_Click);
            // 
            // changeIPToolStripMenuItem
            // 
            this.changeIPToolStripMenuItem.Name = "changeIPToolStripMenuItem";
            this.changeIPToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.changeIPToolStripMenuItem.Text = "Change IP";
            this.changeIPToolStripMenuItem.Click += new System.EventHandler(this.changeIPToolStripMenuItem_Click);
            // 
            // glControl1
            // 
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.Location = new System.Drawing.Point(12, 29);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(810, 488);
            this.glControl1.TabIndex = 0;
            this.glControl1.VSync = false;
            this.glControl1.Load += new System.EventHandler(this.glControl1_Load);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1048, 231);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 25);
            this.label1.TabIndex = 24;
            this.label1.Text = "feed rate";
            // 
            // fBox
            // 
            this.fBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fBox.Location = new System.Drawing.Point(1151, 231);
            this.fBox.Name = "fBox";
            this.fBox.Size = new System.Drawing.Size(100, 31);
            this.fBox.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(831, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 25);
            this.label2.TabIndex = 24;
            this.label2.Text = "Machine X";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(832, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 25);
            this.label3.TabIndex = 24;
            this.label3.Text = "Machine Y";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(832, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 25);
            this.label4.TabIndex = 24;
            this.label4.Text = "Machine Z";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(848, 231);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 25);
            this.label5.TabIndex = 24;
            this.label5.Text = "Spindle";
            // 
            // xBox
            // 
            this.xBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xBox.Location = new System.Drawing.Point(949, 29);
            this.xBox.Name = "xBox";
            this.xBox.Size = new System.Drawing.Size(100, 31);
            this.xBox.TabIndex = 25;
            // 
            // yBox
            // 
            this.yBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yBox.Location = new System.Drawing.Point(950, 69);
            this.yBox.Name = "yBox";
            this.yBox.Size = new System.Drawing.Size(100, 31);
            this.yBox.TabIndex = 25;
            // 
            // zBox
            // 
            this.zBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zBox.Location = new System.Drawing.Point(950, 106);
            this.zBox.Name = "zBox";
            this.zBox.Size = new System.Drawing.Size(100, 31);
            this.zBox.TabIndex = 25;
            // 
            // sBox
            // 
            this.sBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sBox.Location = new System.Drawing.Point(938, 231);
            this.sBox.Name = "sBox";
            this.sBox.Size = new System.Drawing.Size(100, 31);
            this.sBox.TabIndex = 25;
            // 
            // xOffseted
            // 
            this.xOffseted.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xOffseted.Location = new System.Drawing.Point(1151, 32);
            this.xOffseted.Name = "xOffseted";
            this.xOffseted.Size = new System.Drawing.Size(100, 31);
            this.xOffseted.TabIndex = 26;
            // 
            // yOffseted
            // 
            this.yOffseted.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yOffseted.Location = new System.Drawing.Point(1151, 69);
            this.yOffseted.Name = "yOffseted";
            this.yOffseted.Size = new System.Drawing.Size(100, 31);
            this.yOffseted.TabIndex = 26;
            // 
            // zOffseted
            // 
            this.zOffseted.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zOffseted.Location = new System.Drawing.Point(1151, 106);
            this.zOffseted.Name = "zOffseted";
            this.zOffseted.Size = new System.Drawing.Size(100, 31);
            this.zOffseted.TabIndex = 26;
            // 
            // offsetButton
            // 
            this.offsetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.offsetButton.Location = new System.Drawing.Point(1151, 143);
            this.offsetButton.Name = "offsetButton";
            this.offsetButton.Size = new System.Drawing.Size(136, 64);
            this.offsetButton.TabIndex = 27;
            this.offsetButton.Text = "Offset Coordinate";
            this.offsetButton.UseVisualStyleBackColor = true;
            this.offsetButton.Click += new System.EventHandler(this.offsetButton_Click);
            // 
            // feedPlusButton
            // 
            this.feedPlusButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.feedPlusButton.Location = new System.Drawing.Point(949, 268);
            this.feedPlusButton.Name = "feedPlusButton";
            this.feedPlusButton.Size = new System.Drawing.Size(141, 31);
            this.feedPlusButton.TabIndex = 28;
            this.feedPlusButton.Text = "feed rate +";
            this.feedPlusButton.UseVisualStyleBackColor = true;
            this.feedPlusButton.Click += new System.EventHandler(this.feedPlusButton_Click);
            // 
            // feedMinusButton
            // 
            this.feedMinusButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.feedMinusButton.Location = new System.Drawing.Point(1200, 268);
            this.feedMinusButton.Name = "feedMinusButton";
            this.feedMinusButton.Size = new System.Drawing.Size(138, 31);
            this.feedMinusButton.TabIndex = 28;
            this.feedMinusButton.Text = "feed rate -";
            this.feedMinusButton.UseVisualStyleBackColor = true;
            this.feedMinusButton.Click += new System.EventHandler(this.feedMinusButton_Click);
            // 
            // feedRatePercentTxtbox
            // 
            this.feedRatePercentTxtbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.feedRatePercentTxtbox.Location = new System.Drawing.Point(1096, 270);
            this.feedRatePercentTxtbox.Name = "feedRatePercentTxtbox";
            this.feedRatePercentTxtbox.Size = new System.Drawing.Size(98, 29);
            this.feedRatePercentTxtbox.TabIndex = 25;
            this.feedRatePercentTxtbox.Text = "100%";
            // 
            // alarmBtn
            // 
            this.alarmBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.alarmBtn.Location = new System.Drawing.Point(848, 268);
            this.alarmBtn.Name = "alarmBtn";
            this.alarmBtn.Size = new System.Drawing.Size(75, 59);
            this.alarmBtn.TabIndex = 28;
            this.alarmBtn.Text = "clear alarm";
            this.alarmBtn.UseVisualStyleBackColor = true;
            this.alarmBtn.Visible = false;
            this.alarmBtn.Click += new System.EventHandler(this.alarmBtn_Click);
            // 
            // lineComplete
            // 
            this.lineComplete.Location = new System.Drawing.Point(845, 312);
            this.lineComplete.Name = "lineComplete";
            this.lineComplete.Size = new System.Drawing.Size(100, 20);
            this.lineComplete.TabIndex = 29;
            this.lineComplete.Text = "0";
            this.lineComplete.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // statusBox
            // 
            this.statusBox.Location = new System.Drawing.Point(12, 523);
            this.statusBox.Multiline = true;
            this.statusBox.Name = "statusBox";
            this.statusBox.Size = new System.Drawing.Size(810, 195);
            this.statusBox.TabIndex = 30;
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.BackColor = System.Drawing.SystemColors.ControlDark;
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(61, 4);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(1057, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 25);
            this.label7.TabIndex = 24;
            this.label7.Text = "Offset X";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1055, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 25);
            this.label8.TabIndex = 24;
            this.label8.Text = "Offset Y";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(1057, 106);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 25);
            this.label9.TabIndex = 24;
            this.label9.Text = "Offset Z";
            // 
            // ARTab
            // 
            this.ARTab.Controls.Add(this.matRemoveButton);
            this.ARTab.Controls.Add(this.loadCalib);
            this.ARTab.Controls.Add(this.calibrateButton);
            this.ARTab.Controls.Add(this.clearLine);
            this.ARTab.Location = new System.Drawing.Point(4, 29);
            this.ARTab.Name = "ARTab";
            this.ARTab.Padding = new System.Windows.Forms.Padding(3);
            this.ARTab.Size = new System.Drawing.Size(487, 347);
            this.ARTab.TabIndex = 7;
            this.ARTab.Text = "Augmented Reality";
            this.ARTab.UseVisualStyleBackColor = true;
            // 
            // matRemoveButton
            // 
            this.matRemoveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.matRemoveButton.Location = new System.Drawing.Point(35, 150);
            this.matRemoveButton.Name = "matRemoveButton";
            this.matRemoveButton.Size = new System.Drawing.Size(180, 100);
            this.matRemoveButton.TabIndex = 6;
            this.matRemoveButton.Text = "Toggle Material Removal Simulation";
            this.matRemoveButton.UseVisualStyleBackColor = true;
            this.matRemoveButton.Click += new System.EventHandler(this.matRemoveButton_Click);
            // 
            // loadCalib
            // 
            this.loadCalib.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadCalib.Location = new System.Drawing.Point(260, 31);
            this.loadCalib.Name = "loadCalib";
            this.loadCalib.Size = new System.Drawing.Size(180, 100);
            this.loadCalib.TabIndex = 5;
            this.loadCalib.Text = "Load Calibration";
            this.loadCalib.Click += new System.EventHandler(this.loadCalib_Click);
            // 
            // calibrateButton
            // 
            this.calibrateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calibrateButton.Location = new System.Drawing.Point(32, 31);
            this.calibrateButton.Name = "calibrateButton";
            this.calibrateButton.Size = new System.Drawing.Size(180, 100);
            this.calibrateButton.TabIndex = 5;
            this.calibrateButton.Text = "Calibrate";
            this.calibrateButton.Click += new System.EventHandler(this.calibrateButton_Click);
            // 
            // clearLine
            // 
            this.clearLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearLine.Location = new System.Drawing.Point(260, 150);
            this.clearLine.Name = "clearLine";
            this.clearLine.Size = new System.Drawing.Size(180, 100);
            this.clearLine.TabIndex = 2;
            this.clearLine.Text = "Clear Tool Path History";
            this.clearLine.UseVisualStyleBackColor = true;
            this.clearLine.Click += new System.EventHandler(this.clearLine_Click);
            // 
            // mdiTab
            // 
            this.mdiTab.Controls.Add(this.M06Number);
            this.mdiTab.Controls.Add(this.fIn);
            this.mdiTab.Controls.Add(this.zIn);
            this.mdiTab.Controls.Add(this.yIn);
            this.mdiTab.Controls.Add(this.xIn);
            this.mdiTab.Controls.Add(this.M03Param);
            this.mdiTab.Controls.Add(this.M05Button);
            this.mdiTab.Controls.Add(this.M06button);
            this.mdiTab.Controls.Add(this.G01Button);
            this.mdiTab.Controls.Add(this.G00Button);
            this.mdiTab.Controls.Add(this.M03Button);
            this.mdiTab.Controls.Add(this.label6);
            this.mdiTab.Controls.Add(this.label11);
            this.mdiTab.Controls.Add(this.label13);
            this.mdiTab.Controls.Add(this.label12);
            this.mdiTab.Location = new System.Drawing.Point(4, 29);
            this.mdiTab.Name = "mdiTab";
            this.mdiTab.Padding = new System.Windows.Forms.Padding(3);
            this.mdiTab.Size = new System.Drawing.Size(487, 347);
            this.mdiTab.TabIndex = 6;
            this.mdiTab.Text = "MDI";
            this.mdiTab.UseVisualStyleBackColor = true;
            // 
            // M06Number
            // 
            this.M06Number.Location = new System.Drawing.Point(123, 205);
            this.M06Number.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.M06Number.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.M06Number.Name = "M06Number";
            this.M06Number.Size = new System.Drawing.Size(120, 26);
            this.M06Number.TabIndex = 25;
            this.M06Number.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // fIn
            // 
            this.fIn.Location = new System.Drawing.Point(405, 79);
            this.fIn.Name = "fIn";
            this.fIn.Size = new System.Drawing.Size(66, 26);
            this.fIn.TabIndex = 18;
            this.fIn.Text = "300";
            // 
            // zIn
            // 
            this.zIn.Location = new System.Drawing.Point(295, 82);
            this.zIn.Name = "zIn";
            this.zIn.Size = new System.Drawing.Size(66, 26);
            this.zIn.TabIndex = 18;
            this.zIn.Text = "140";
            // 
            // yIn
            // 
            this.yIn.Location = new System.Drawing.Point(198, 84);
            this.yIn.Name = "yIn";
            this.yIn.Size = new System.Drawing.Size(66, 26);
            this.yIn.TabIndex = 18;
            this.yIn.Text = "50";
            // 
            // xIn
            // 
            this.xIn.Location = new System.Drawing.Point(106, 84);
            this.xIn.Name = "xIn";
            this.xIn.Size = new System.Drawing.Size(66, 26);
            this.xIn.TabIndex = 18;
            this.xIn.Text = "180";
            // 
            // M03Param
            // 
            this.M03Param.Location = new System.Drawing.Point(102, 22);
            this.M03Param.Name = "M03Param";
            this.M03Param.Size = new System.Drawing.Size(100, 26);
            this.M03Param.TabIndex = 18;
            this.M03Param.TextChanged += new System.EventHandler(this.M03Param_TextChanged);
            // 
            // M05Button
            // 
            this.M05Button.Location = new System.Drawing.Point(291, 6);
            this.M05Button.Name = "M05Button";
            this.M05Button.Size = new System.Drawing.Size(70, 59);
            this.M05Button.TabIndex = 17;
            this.M05Button.Text = "Send M05";
            this.M05Button.UseVisualStyleBackColor = true;
            this.M05Button.Click += new System.EventHandler(this.M05Button_Click);
            // 
            // M06button
            // 
            this.M06button.Location = new System.Drawing.Point(10, 187);
            this.M06button.Name = "M06button";
            this.M06button.Size = new System.Drawing.Size(90, 61);
            this.M06button.TabIndex = 17;
            this.M06button.Text = "Send M06";
            this.M06button.UseVisualStyleBackColor = true;
            this.M06button.Click += new System.EventHandler(this.SendM06_Click);
            // 
            // G01Button
            // 
            this.G01Button.Location = new System.Drawing.Point(6, 129);
            this.G01Button.Name = "G01Button";
            this.G01Button.Size = new System.Drawing.Size(75, 34);
            this.G01Button.TabIndex = 17;
            this.G01Button.Text = "G01";
            this.G01Button.UseVisualStyleBackColor = true;
            this.G01Button.Click += new System.EventHandler(this.g00Button_Click);
            // 
            // G00Button
            // 
            this.G00Button.Location = new System.Drawing.Point(6, 84);
            this.G00Button.Name = "G00Button";
            this.G00Button.Size = new System.Drawing.Size(79, 28);
            this.G00Button.TabIndex = 17;
            this.G00Button.Text = "G00";
            this.G00Button.UseVisualStyleBackColor = true;
            this.G00Button.Click += new System.EventHandler(this.g00Button_Click);
            // 
            // M03Button
            // 
            this.M03Button.Location = new System.Drawing.Point(6, 6);
            this.M03Button.Name = "M03Button";
            this.M03Button.Size = new System.Drawing.Size(79, 59);
            this.M03Button.TabIndex = 17;
            this.M03Button.Text = "Send M03";
            this.M03Button.UseVisualStyleBackColor = true;
            this.M03Button.Click += new System.EventHandler(this.M03Button_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(86, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 20);
            this.label6.TabIndex = 24;
            this.label6.Text = "X";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(178, 85);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(20, 20);
            this.label11.TabIndex = 24;
            this.label11.Text = "Y";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(372, 82);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(19, 20);
            this.label13.TabIndex = 24;
            this.label13.Text = "F";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(270, 87);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(19, 20);
            this.label12.TabIndex = 24;
            this.label12.Text = "Z";
            // 
            // jogTab
            // 
            this.jogTab.Controls.Add(this.groupBox2);
            this.jogTab.Controls.Add(this.groupBox3);
            this.jogTab.Controls.Add(this.groupBox4);
            this.jogTab.Location = new System.Drawing.Point(4, 29);
            this.jogTab.Name = "jogTab";
            this.jogTab.Padding = new System.Windows.Forms.Padding(3);
            this.jogTab.Size = new System.Drawing.Size(487, 347);
            this.jogTab.TabIndex = 4;
            this.jogTab.Text = "Jog";
            this.jogTab.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.moveXup);
            this.groupBox2.Controls.Add(this.moveXdown);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(6, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(174, 84);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "X axis";
            // 
            // moveXup
            // 
            this.moveXup.Enabled = false;
            this.moveXup.Location = new System.Drawing.Point(88, 30);
            this.moveXup.Name = "moveXup";
            this.moveXup.Size = new System.Drawing.Size(75, 35);
            this.moveXup.TabIndex = 1;
            this.moveXup.Text = "Right";
            this.moveXup.UseVisualStyleBackColor = true;
            this.moveXup.Click += new System.EventHandler(this.moveXup_Click);
            // 
            // moveXdown
            // 
            this.moveXdown.Enabled = false;
            this.moveXdown.Location = new System.Drawing.Point(7, 30);
            this.moveXdown.Name = "moveXdown";
            this.moveXdown.Size = new System.Drawing.Size(75, 35);
            this.moveXdown.TabIndex = 0;
            this.moveXdown.Text = "Left";
            this.moveXdown.UseVisualStyleBackColor = true;
            this.moveXdown.Click += new System.EventHandler(this.moveXdown_Click_1);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.moveYup);
            this.groupBox3.Controls.Add(this.moveYdown);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(6, 108);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(174, 77);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Y axis";
            // 
            // moveYup
            // 
            this.moveYup.Enabled = false;
            this.moveYup.Location = new System.Drawing.Point(85, 30);
            this.moveYup.Name = "moveYup";
            this.moveYup.Size = new System.Drawing.Size(75, 35);
            this.moveYup.TabIndex = 1;
            this.moveYup.Text = "Front";
            this.moveYup.UseVisualStyleBackColor = true;
            this.moveYup.Click += new System.EventHandler(this.moveYup_Click);
            // 
            // moveYdown
            // 
            this.moveYdown.Enabled = false;
            this.moveYdown.Location = new System.Drawing.Point(7, 30);
            this.moveYdown.Name = "moveYdown";
            this.moveYdown.Size = new System.Drawing.Size(75, 35);
            this.moveYdown.TabIndex = 0;
            this.moveYdown.Text = "Back";
            this.moveYdown.UseVisualStyleBackColor = true;
            this.moveYdown.Click += new System.EventHandler(this.moveYdown_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.moveZdown);
            this.groupBox4.Controls.Add(this.moveZup);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(6, 191);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(174, 78);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Z axis";
            // 
            // moveZdown
            // 
            this.moveZdown.Enabled = false;
            this.moveZdown.Location = new System.Drawing.Point(7, 30);
            this.moveZdown.Name = "moveZdown";
            this.moveZdown.Size = new System.Drawing.Size(75, 35);
            this.moveZdown.TabIndex = 1;
            this.moveZdown.Text = "Down";
            this.moveZdown.UseVisualStyleBackColor = true;
            this.moveZdown.Click += new System.EventHandler(this.moveZdown_Click);
            // 
            // moveZup
            // 
            this.moveZup.Enabled = false;
            this.moveZup.Location = new System.Drawing.Point(88, 30);
            this.moveZup.Name = "moveZup";
            this.moveZup.Size = new System.Drawing.Size(75, 35);
            this.moveZup.TabIndex = 0;
            this.moveZup.Text = "Up";
            this.moveZup.UseVisualStyleBackColor = true;
            this.moveZup.Click += new System.EventHandler(this.moveZup_Click);
            // 
            // memoryTab
            // 
            this.memoryTab.Controls.Add(this.runGCode);
            this.memoryTab.Controls.Add(this.GCodeTxt);
            this.memoryTab.Location = new System.Drawing.Point(4, 29);
            this.memoryTab.Name = "memoryTab";
            this.memoryTab.Padding = new System.Windows.Forms.Padding(3);
            this.memoryTab.Size = new System.Drawing.Size(487, 347);
            this.memoryTab.TabIndex = 3;
            this.memoryTab.Text = "Memory";
            this.memoryTab.UseVisualStyleBackColor = true;
            // 
            // runGCode
            // 
            this.runGCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runGCode.Location = new System.Drawing.Point(322, 49);
            this.runGCode.Name = "runGCode";
            this.runGCode.Size = new System.Drawing.Size(106, 66);
            this.runGCode.TabIndex = 1;
            this.runGCode.Text = "Run G Code";
            this.runGCode.UseVisualStyleBackColor = true;
            this.runGCode.Click += new System.EventHandler(this.runGCode_Click);
            // 
            // GCodeTxt
            // 
            this.GCodeTxt.Location = new System.Drawing.Point(15, 26);
            this.GCodeTxt.MaxLength = 0;
            this.GCodeTxt.Multiline = true;
            this.GCodeTxt.Name = "GCodeTxt";
            this.GCodeTxt.Size = new System.Drawing.Size(296, 177);
            this.GCodeTxt.TabIndex = 0;
            // 
            // referenceTab
            // 
            this.referenceTab.Controls.Add(this.refButton);
            this.referenceTab.Location = new System.Drawing.Point(4, 29);
            this.referenceTab.Name = "referenceTab";
            this.referenceTab.Padding = new System.Windows.Forms.Padding(3);
            this.referenceTab.Size = new System.Drawing.Size(487, 347);
            this.referenceTab.TabIndex = 0;
            this.referenceTab.Text = "Reference";
            this.referenceTab.UseVisualStyleBackColor = true;
            // 
            // refButton
            // 
            this.refButton.Enabled = false;
            this.refButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refButton.Location = new System.Drawing.Point(22, 23);
            this.refButton.Name = "refButton";
            this.refButton.Size = new System.Drawing.Size(138, 83);
            this.refButton.TabIndex = 0;
            this.refButton.Text = "Reference Machine";
            this.refButton.UseVisualStyleBackColor = true;
            this.refButton.Click += new System.EventHandler(this.refButton_Click);
            // 
            // ModeControl
            // 
            this.ModeControl.Controls.Add(this.referenceTab);
            this.ModeControl.Controls.Add(this.memoryTab);
            this.ModeControl.Controls.Add(this.jogTab);
            this.ModeControl.Controls.Add(this.mdiTab);
            this.ModeControl.Controls.Add(this.ARTab);
            this.ModeControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ModeControl.Location = new System.Drawing.Point(843, 338);
            this.ModeControl.Name = "ModeControl";
            this.ModeControl.SelectedIndex = 0;
            this.ModeControl.Size = new System.Drawing.Size(495, 380);
            this.ModeControl.TabIndex = 22;
            this.ModeControl.Click += new System.EventHandler(this.ModeControl_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 730);
            this.Controls.Add(this.statusBox);
            this.Controls.Add(this.ModeControl);
            this.Controls.Add(this.lineComplete);
            this.Controls.Add(this.glControl1);
            this.Controls.Add(this.alarmBtn);
            this.Controls.Add(this.feedMinusButton);
            this.Controls.Add(this.feedPlusButton);
            this.Controls.Add(this.offsetButton);
            this.Controls.Add(this.zOffseted);
            this.Controls.Add(this.yOffseted);
            this.Controls.Add(this.xOffseted);
            this.Controls.Add(this.sBox);
            this.Controls.Add(this.zBox);
            this.Controls.Add(this.feedRatePercentTxtbox);
            this.Controls.Add(this.fBox);
            this.Controls.Add(this.yBox);
            this.Controls.Add(this.xBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "iWindow";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ARTab.ResumeLayout(false);
            this.mdiTab.ResumeLayout(false);
            this.mdiTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.M06Number)).EndInit();
            this.jogTab.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.memoryTab.ResumeLayout(false);
            this.memoryTab.PerformLayout();
            this.referenceTab.ResumeLayout(false);
            this.ModeControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem connectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem terminateToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox fBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox xBox;
        private System.Windows.Forms.TextBox yBox;
        private System.Windows.Forms.TextBox zBox;
        private System.Windows.Forms.TextBox sBox;
        private System.Windows.Forms.TextBox xOffseted;
        private System.Windows.Forms.TextBox yOffseted;
        private System.Windows.Forms.TextBox zOffseted;
        private System.Windows.Forms.Button offsetButton;
        private OpenTK.GLControl glControl1;
        private System.Windows.Forms.Button feedPlusButton;
        private System.Windows.Forms.Button feedMinusButton;
        private System.Windows.Forms.TextBox feedRatePercentTxtbox;
        private System.Windows.Forms.Button alarmBtn;
        private System.Windows.Forms.TextBox lineComplete;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox statusBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TabPage ARTab;
        private System.Windows.Forms.Button matRemoveButton;
        private System.Windows.Forms.Button loadCalib;
        private System.Windows.Forms.Button calibrateButton;
        private System.Windows.Forms.Button clearLine;
        private System.Windows.Forms.TabPage mdiTab;
        private System.Windows.Forms.NumericUpDown M06Number;
        private System.Windows.Forms.TextBox fIn;
        private System.Windows.Forms.TextBox zIn;
        private System.Windows.Forms.TextBox yIn;
        private System.Windows.Forms.TextBox xIn;
        private System.Windows.Forms.TextBox M03Param;
        private System.Windows.Forms.Button M05Button;
        private System.Windows.Forms.Button M06button;
        private System.Windows.Forms.Button G01Button;
        private System.Windows.Forms.Button G00Button;
        private System.Windows.Forms.Button M03Button;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TabPage jogTab;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button moveXup;
        private System.Windows.Forms.Button moveXdown;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button moveYup;
        private System.Windows.Forms.Button moveYdown;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button moveZdown;
        private System.Windows.Forms.Button moveZup;
        private System.Windows.Forms.TabPage memoryTab;
        private System.Windows.Forms.Button runGCode;
        private System.Windows.Forms.TextBox GCodeTxt;
        private System.Windows.Forms.TabPage referenceTab;
        private System.Windows.Forms.Button refButton;
        private System.Windows.Forms.TabControl ModeControl;
        private System.Windows.Forms.ToolStripMenuItem changeIPToolStripMenuItem;
    }
}

