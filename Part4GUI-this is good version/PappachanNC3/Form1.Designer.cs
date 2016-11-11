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
            this.cadToggle = new System.Windows.Forms.Button();
            this.toolpathToggle = new System.Windows.Forms.Button();
            this.toggleBadVolume = new System.Windows.Forms.Button();
            this.axisToggle = new System.Windows.Forms.Button();
            this.matRemoveButton = new System.Windows.Forms.Button();
            this.loadCalib = new System.Windows.Forms.Button();
            this.calibrateButton = new System.Windows.Forms.Button();
            this.drillTipToggle = new System.Windows.Forms.Button();
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
            this.lineNumberBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.currentLineLbl = new System.Windows.Forms.Label();
            this.currentLineBox = new System.Windows.Forms.TextBox();
            this.runGCode = new System.Windows.Forms.Button();
            this.GCodeTxt = new System.Windows.Forms.TextBox();
            this.referenceTab = new System.Windows.Forms.TabPage();
            this.refButton = new System.Windows.Forms.Button();
            this.ModeControl = new System.Windows.Forms.TabControl();
            this.ErrorBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.xVelBox = new System.Windows.Forms.TextBox();
            this.yVelBox = new System.Windows.Forms.TextBox();
            this.zVelBox = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.xAccelBox = new System.Windows.Forms.TextBox();
            this.yAccelBox = new System.Windows.Forms.TextBox();
            this.zAccelBox = new System.Windows.Forms.TextBox();
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
            this.menuStrip1.Size = new System.Drawing.Size(1189, 29);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // connectionToolStripMenuItem
            // 
            this.connectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.terminateToolStripMenuItem,
            this.changeIPToolStripMenuItem});
            this.connectionToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connectionToolStripMenuItem.Name = "connectionToolStripMenuItem";
            this.connectionToolStripMenuItem.Size = new System.Drawing.Size(101, 25);
            this.connectionToolStripMenuItem.Text = "Connection";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(150, 26);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // terminateToolStripMenuItem
            // 
            this.terminateToolStripMenuItem.Enabled = false;
            this.terminateToolStripMenuItem.Name = "terminateToolStripMenuItem";
            this.terminateToolStripMenuItem.Size = new System.Drawing.Size(150, 26);
            this.terminateToolStripMenuItem.Text = "Terminate";
            this.terminateToolStripMenuItem.Click += new System.EventHandler(this.terminateToolStripMenuItem_Click);
            // 
            // changeIPToolStripMenuItem
            // 
            this.changeIPToolStripMenuItem.Name = "changeIPToolStripMenuItem";
            this.changeIPToolStripMenuItem.Size = new System.Drawing.Size(150, 26);
            this.changeIPToolStripMenuItem.Text = "Change IP";
            this.changeIPToolStripMenuItem.Click += new System.EventHandler(this.changeIPToolStripMenuItem_Click);
            // 
            // glControl1
            // 
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.Location = new System.Drawing.Point(12, 29);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(810, 501);
            this.glControl1.TabIndex = 0;
            this.glControl1.VSync = false;
            this.glControl1.Load += new System.EventHandler(this.glControl1_Load);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(842, 329);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 24);
            this.label1.TabIndex = 24;
            this.label1.Text = "Feed Rate";
            this.label1.UseWaitCursor = true;
            // 
            // fBox
            // 
            this.fBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fBox.Location = new System.Drawing.Point(999, 329);
            this.fBox.Name = "fBox";
            this.fBox.Size = new System.Drawing.Size(111, 29);
            this.fBox.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(842, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 24);
            this.label2.TabIndex = 24;
            this.label2.Text = "Machine X";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(842, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 24);
            this.label3.TabIndex = 24;
            this.label3.Text = "Machine Y";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(842, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 24);
            this.label4.TabIndex = 24;
            this.label4.Text = "Machine Z";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(842, 283);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 24);
            this.label5.TabIndex = 24;
            this.label5.Text = "Spindle Speed";
            // 
            // xBox
            // 
            this.xBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xBox.Location = new System.Drawing.Point(950, 40);
            this.xBox.Name = "xBox";
            this.xBox.Size = new System.Drawing.Size(102, 29);
            this.xBox.TabIndex = 25;
            // 
            // yBox
            // 
            this.yBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yBox.Location = new System.Drawing.Point(950, 75);
            this.yBox.Name = "yBox";
            this.yBox.Size = new System.Drawing.Size(102, 29);
            this.yBox.TabIndex = 25;
            // 
            // zBox
            // 
            this.zBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zBox.Location = new System.Drawing.Point(950, 113);
            this.zBox.Name = "zBox";
            this.zBox.Size = new System.Drawing.Size(102, 29);
            this.zBox.TabIndex = 25;
            this.zBox.TextChanged += new System.EventHandler(this.zBox_TextChanged);
            // 
            // sBox
            // 
            this.sBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sBox.Location = new System.Drawing.Point(999, 280);
            this.sBox.Name = "sBox";
            this.sBox.Size = new System.Drawing.Size(111, 29);
            this.sBox.TabIndex = 25;
            // 
            // xOffseted
            // 
            this.xOffseted.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xOffseted.Location = new System.Drawing.Point(950, 161);
            this.xOffseted.Name = "xOffseted";
            this.xOffseted.Size = new System.Drawing.Size(102, 29);
            this.xOffseted.TabIndex = 26;
            // 
            // yOffseted
            // 
            this.yOffseted.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yOffseted.Location = new System.Drawing.Point(950, 196);
            this.yOffseted.Name = "yOffseted";
            this.yOffseted.Size = new System.Drawing.Size(102, 29);
            this.yOffseted.TabIndex = 26;
            // 
            // zOffseted
            // 
            this.zOffseted.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zOffseted.Location = new System.Drawing.Point(950, 231);
            this.zOffseted.Name = "zOffseted";
            this.zOffseted.Size = new System.Drawing.Size(102, 29);
            this.zOffseted.TabIndex = 26;
            // 
            // offsetButton
            // 
            this.offsetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.offsetButton.Location = new System.Drawing.Point(1069, 184);
            this.offsetButton.Name = "offsetButton";
            this.offsetButton.Size = new System.Drawing.Size(115, 57);
            this.offsetButton.TabIndex = 27;
            this.offsetButton.Text = "Offset Coordinate";
            this.offsetButton.UseVisualStyleBackColor = true;
            this.offsetButton.Click += new System.EventHandler(this.offsetButton_Click);
            // 
            // feedPlusButton
            // 
            this.feedPlusButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.feedPlusButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.feedPlusButton.Location = new System.Drawing.Point(1054, 644);
            this.feedPlusButton.Name = "feedPlusButton";
            this.feedPlusButton.Size = new System.Drawing.Size(125, 40);
            this.feedPlusButton.TabIndex = 28;
            this.feedPlusButton.Text = "Feed Rate +";
            this.feedPlusButton.UseVisualStyleBackColor = true;
            this.feedPlusButton.Click += new System.EventHandler(this.feedPlusButton_Click);
            // 
            // feedMinusButton
            // 
            this.feedMinusButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.feedMinusButton.Location = new System.Drawing.Point(832, 644);
            this.feedMinusButton.Name = "feedMinusButton";
            this.feedMinusButton.Size = new System.Drawing.Size(125, 40);
            this.feedMinusButton.TabIndex = 28;
            this.feedMinusButton.Text = "Feed Rate -";
            this.feedMinusButton.UseVisualStyleBackColor = true;
            this.feedMinusButton.Click += new System.EventHandler(this.feedMinusButton_Click);
            // 
            // feedRatePercentTxtbox
            // 
            this.feedRatePercentTxtbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.feedRatePercentTxtbox.Location = new System.Drawing.Point(963, 649);
            this.feedRatePercentTxtbox.Name = "feedRatePercentTxtbox";
            this.feedRatePercentTxtbox.Size = new System.Drawing.Size(85, 29);
            this.feedRatePercentTxtbox.TabIndex = 25;
            this.feedRatePercentTxtbox.Text = "100%";
            // 
            // alarmBtn
            // 
            this.alarmBtn.Location = new System.Drawing.Point(1192, 591);
            this.alarmBtn.Name = "alarmBtn";
            this.alarmBtn.Size = new System.Drawing.Size(75, 23);
            this.alarmBtn.TabIndex = 28;
            this.alarmBtn.Text = "clear alarm";
            this.alarmBtn.UseVisualStyleBackColor = true;
            this.alarmBtn.Visible = false;
            this.alarmBtn.Click += new System.EventHandler(this.alarmBtn_Click);
            // 
            // lineComplete
            // 
            this.lineComplete.Location = new System.Drawing.Point(1208, 629);
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
            this.statusBox.Location = new System.Drawing.Point(1103, 876);
            this.statusBox.Multiline = true;
            this.statusBox.Name = "statusBox";
            this.statusBox.Size = new System.Drawing.Size(541, 46);
            this.statusBox.TabIndex = 30;
            this.statusBox.Visible = false;
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
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(842, 162);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 24);
            this.label7.TabIndex = 24;
            this.label7.Text = "Offset X";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(842, 199);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 24);
            this.label8.TabIndex = 24;
            this.label8.Text = "Offset Y";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(842, 234);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 24);
            this.label9.TabIndex = 24;
            this.label9.Text = "Offset Z";
            // 
            // ARTab
            // 
            this.ARTab.Controls.Add(this.cadToggle);
            this.ARTab.Controls.Add(this.toolpathToggle);
            this.ARTab.Controls.Add(this.toggleBadVolume);
            this.ARTab.Controls.Add(this.axisToggle);
            this.ARTab.Controls.Add(this.matRemoveButton);
            this.ARTab.Controls.Add(this.loadCalib);
            this.ARTab.Controls.Add(this.calibrateButton);
            this.ARTab.Controls.Add(this.drillTipToggle);
            this.ARTab.Controls.Add(this.clearLine);
            this.ARTab.Location = new System.Drawing.Point(4, 33);
            this.ARTab.Name = "ARTab";
            this.ARTab.Padding = new System.Windows.Forms.Padding(3);
            this.ARTab.Size = new System.Drawing.Size(802, 243);
            this.ARTab.TabIndex = 7;
            this.ARTab.Text = "Augmented Reality";
            this.ARTab.UseVisualStyleBackColor = true;
            // 
            // cadToggle
            // 
            this.cadToggle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cadToggle.Location = new System.Drawing.Point(16, 82);
            this.cadToggle.Name = "cadToggle";
            this.cadToggle.Size = new System.Drawing.Size(189, 68);
            this.cadToggle.TabIndex = 7;
            this.cadToggle.Text = "Toggle CAD Model";
            this.cadToggle.UseVisualStyleBackColor = true;
            this.cadToggle.Click += new System.EventHandler(this.cadToggle_Click);
            // 
            // toolpathToggle
            // 
            this.toolpathToggle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolpathToggle.Location = new System.Drawing.Point(421, 8);
            this.toolpathToggle.Name = "toolpathToggle";
            this.toolpathToggle.Size = new System.Drawing.Size(189, 68);
            this.toolpathToggle.TabIndex = 8;
            this.toolpathToggle.Text = "Toggle Physical Toolpath Display";
            this.toolpathToggle.UseVisualStyleBackColor = true;
            this.toolpathToggle.Click += new System.EventHandler(this.toolpathToggle_Click);
            // 
            // toggleBadVolume
            // 
            this.toggleBadVolume.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toggleBadVolume.Location = new System.Drawing.Point(616, 8);
            this.toggleBadVolume.Name = "toggleBadVolume";
            this.toggleBadVolume.Size = new System.Drawing.Size(180, 68);
            this.toggleBadVolume.TabIndex = 6;
            this.toggleBadVolume.Text = "Toggle Unsafe Milling Volume";
            this.toggleBadVolume.UseVisualStyleBackColor = true;
            this.toggleBadVolume.Click += new System.EventHandler(this.toggleBadVolume_Click);
            // 
            // axisToggle
            // 
            this.axisToggle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.axisToggle.Location = new System.Drawing.Point(16, 156);
            this.axisToggle.Name = "axisToggle";
            this.axisToggle.Size = new System.Drawing.Size(189, 62);
            this.axisToggle.TabIndex = 6;
            this.axisToggle.Text = "Toggle Axis Display";
            this.axisToggle.UseVisualStyleBackColor = true;
            this.axisToggle.Click += new System.EventHandler(this.axisToggle_Click);
            // 
            // matRemoveButton
            // 
            this.matRemoveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.matRemoveButton.Location = new System.Drawing.Point(211, 82);
            this.matRemoveButton.Name = "matRemoveButton";
            this.matRemoveButton.Size = new System.Drawing.Size(189, 68);
            this.matRemoveButton.TabIndex = 6;
            this.matRemoveButton.Text = "Toggle Material Removal Simulation";
            this.matRemoveButton.UseVisualStyleBackColor = true;
            this.matRemoveButton.Click += new System.EventHandler(this.matRemoveButton_Click);
            // 
            // loadCalib
            // 
            this.loadCalib.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadCalib.Location = new System.Drawing.Point(211, 8);
            this.loadCalib.Name = "loadCalib";
            this.loadCalib.Size = new System.Drawing.Size(189, 68);
            this.loadCalib.TabIndex = 5;
            this.loadCalib.Text = "Load Calibration";
            this.loadCalib.Click += new System.EventHandler(this.loadCalib_Click);
            // 
            // calibrateButton
            // 
            this.calibrateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calibrateButton.Location = new System.Drawing.Point(16, 8);
            this.calibrateButton.Name = "calibrateButton";
            this.calibrateButton.Size = new System.Drawing.Size(189, 68);
            this.calibrateButton.TabIndex = 5;
            this.calibrateButton.Text = "Calibrate";
            this.calibrateButton.Click += new System.EventHandler(this.calibrateButton_Click);
            // 
            // drillTipToggle
            // 
            this.drillTipToggle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.drillTipToggle.Location = new System.Drawing.Point(211, 156);
            this.drillTipToggle.Name = "drillTipToggle";
            this.drillTipToggle.Size = new System.Drawing.Size(189, 62);
            this.drillTipToggle.TabIndex = 2;
            this.drillTipToggle.Text = "Toggle Drill Tip Display";
            this.drillTipToggle.UseVisualStyleBackColor = true;
            this.drillTipToggle.Click += new System.EventHandler(this.drillTipToggle_Click);
            // 
            // clearLine
            // 
            this.clearLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearLine.Location = new System.Drawing.Point(421, 82);
            this.clearLine.Name = "clearLine";
            this.clearLine.Size = new System.Drawing.Size(189, 68);
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
            this.mdiTab.Location = new System.Drawing.Point(4, 33);
            this.mdiTab.Name = "mdiTab";
            this.mdiTab.Padding = new System.Windows.Forms.Padding(3);
            this.mdiTab.Size = new System.Drawing.Size(802, 243);
            this.mdiTab.TabIndex = 6;
            this.mdiTab.Text = "MDI";
            this.mdiTab.UseVisualStyleBackColor = true;
            // 
            // M06Number
            // 
            this.M06Number.Location = new System.Drawing.Point(123, 162);
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
            this.M06Number.Size = new System.Drawing.Size(120, 29);
            this.M06Number.TabIndex = 25;
            this.M06Number.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // fIn
            // 
            this.fIn.Location = new System.Drawing.Point(446, 82);
            this.fIn.Name = "fIn";
            this.fIn.Size = new System.Drawing.Size(66, 29);
            this.fIn.TabIndex = 18;
            this.fIn.Text = "300";
            // 
            // zIn
            // 
            this.zIn.Location = new System.Drawing.Point(317, 82);
            this.zIn.Name = "zIn";
            this.zIn.Size = new System.Drawing.Size(66, 29);
            this.zIn.TabIndex = 18;
            this.zIn.Text = "140";
            // 
            // yIn
            // 
            this.yIn.Location = new System.Drawing.Point(225, 82);
            this.yIn.Name = "yIn";
            this.yIn.Size = new System.Drawing.Size(66, 29);
            this.yIn.TabIndex = 18;
            this.yIn.Text = "50";
            // 
            // xIn
            // 
            this.xIn.Location = new System.Drawing.Point(120, 82);
            this.xIn.Name = "xIn";
            this.xIn.Size = new System.Drawing.Size(66, 29);
            this.xIn.TabIndex = 18;
            this.xIn.Text = "180";
            // 
            // M03Param
            // 
            this.M03Param.Location = new System.Drawing.Point(123, 8);
            this.M03Param.Name = "M03Param";
            this.M03Param.Size = new System.Drawing.Size(100, 29);
            this.M03Param.TabIndex = 18;
            // 
            // M05Button
            // 
            this.M05Button.Location = new System.Drawing.Point(337, 8);
            this.M05Button.Name = "M05Button";
            this.M05Button.Size = new System.Drawing.Size(122, 31);
            this.M05Button.TabIndex = 17;
            this.M05Button.Text = "Send M05";
            this.M05Button.UseVisualStyleBackColor = true;
            this.M05Button.Click += new System.EventHandler(this.M05Button_Click);
            // 
            // M06button
            // 
            this.M06button.Location = new System.Drawing.Point(37, 148);
            this.M06button.Name = "M06button";
            this.M06button.Size = new System.Drawing.Size(75, 57);
            this.M06button.TabIndex = 17;
            this.M06button.Text = "Send M06";
            this.M06button.UseVisualStyleBackColor = true;
            this.M06button.Click += new System.EventHandler(this.SendM06_Click);
            // 
            // G01Button
            // 
            this.G01Button.Location = new System.Drawing.Point(6, 98);
            this.G01Button.Name = "G01Button";
            this.G01Button.Size = new System.Drawing.Size(75, 37);
            this.G01Button.TabIndex = 17;
            this.G01Button.Text = "G01";
            this.G01Button.UseVisualStyleBackColor = true;
            this.G01Button.Click += new System.EventHandler(this.g00Button_Click);
            // 
            // G00Button
            // 
            this.G00Button.Location = new System.Drawing.Point(6, 53);
            this.G00Button.Name = "G00Button";
            this.G00Button.Size = new System.Drawing.Size(75, 31);
            this.G00Button.TabIndex = 17;
            this.G00Button.Text = "G00";
            this.G00Button.UseVisualStyleBackColor = true;
            this.G00Button.Click += new System.EventHandler(this.g00Button_Click);
            // 
            // M03Button
            // 
            this.M03Button.Location = new System.Drawing.Point(6, 6);
            this.M03Button.Name = "M03Button";
            this.M03Button.Size = new System.Drawing.Size(106, 31);
            this.M03Button.TabIndex = 17;
            this.M03Button.Text = "Send M03";
            this.M03Button.UseVisualStyleBackColor = true;
            this.M03Button.Click += new System.EventHandler(this.M03Button_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(100, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 24);
            this.label6.TabIndex = 24;
            this.label6.Text = "X";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(198, 85);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(22, 24);
            this.label11.TabIndex = 24;
            this.label11.Text = "Y";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(413, 85);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(22, 24);
            this.label13.TabIndex = 24;
            this.label13.Text = "F";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(297, 85);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(22, 24);
            this.label12.TabIndex = 24;
            this.label12.Text = "Z";
            // 
            // jogTab
            // 
            this.jogTab.Controls.Add(this.groupBox2);
            this.jogTab.Controls.Add(this.groupBox3);
            this.jogTab.Controls.Add(this.groupBox4);
            this.jogTab.Location = new System.Drawing.Point(4, 33);
            this.jogTab.Name = "jogTab";
            this.jogTab.Padding = new System.Windows.Forms.Padding(3);
            this.jogTab.Size = new System.Drawing.Size(802, 243);
            this.jogTab.TabIndex = 4;
            this.jogTab.Text = "Jog";
            this.jogTab.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.moveXup);
            this.groupBox2.Controls.Add(this.moveXdown);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(6, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(184, 65);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "X axis";
            // 
            // moveXup
            // 
            this.moveXup.Enabled = false;
            this.moveXup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moveXup.Location = new System.Drawing.Point(88, 19);
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
            this.moveXdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moveXdown.Location = new System.Drawing.Point(7, 19);
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
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(6, 89);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(184, 65);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Y axis";
            // 
            // moveYup
            // 
            this.moveYup.Enabled = false;
            this.moveYup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moveYup.Location = new System.Drawing.Point(88, 27);
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
            this.moveYdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moveYdown.Location = new System.Drawing.Point(6, 27);
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
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(6, 160);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(184, 65);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Z axis";
            // 
            // moveZdown
            // 
            this.moveZdown.Enabled = false;
            this.moveZdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moveZdown.Location = new System.Drawing.Point(7, 24);
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
            this.moveZup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.moveZup.Location = new System.Drawing.Point(88, 24);
            this.moveZup.Name = "moveZup";
            this.moveZup.Size = new System.Drawing.Size(75, 35);
            this.moveZup.TabIndex = 0;
            this.moveZup.Text = "Up";
            this.moveZup.UseVisualStyleBackColor = true;
            this.moveZup.Click += new System.EventHandler(this.moveZup_Click);
            // 
            // memoryTab
            // 
            this.memoryTab.Controls.Add(this.lineNumberBox);
            this.memoryTab.Controls.Add(this.label10);
            this.memoryTab.Controls.Add(this.currentLineLbl);
            this.memoryTab.Controls.Add(this.currentLineBox);
            this.memoryTab.Controls.Add(this.runGCode);
            this.memoryTab.Controls.Add(this.GCodeTxt);
            this.memoryTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.memoryTab.Location = new System.Drawing.Point(4, 33);
            this.memoryTab.Name = "memoryTab";
            this.memoryTab.Padding = new System.Windows.Forms.Padding(3);
            this.memoryTab.Size = new System.Drawing.Size(802, 243);
            this.memoryTab.TabIndex = 3;
            this.memoryTab.Text = "Memory";
            this.memoryTab.UseVisualStyleBackColor = true;
            // 
            // lineNumberBox
            // 
            this.lineNumberBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lineNumberBox.Location = new System.Drawing.Point(593, 62);
            this.lineNumberBox.MaxLength = 0;
            this.lineNumberBox.Name = "lineNumberBox";
            this.lineNumberBox.ReadOnly = true;
            this.lineNumberBox.Size = new System.Drawing.Size(140, 31);
            this.lineNumberBox.TabIndex = 32;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(588, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(134, 25);
            this.label10.TabIndex = 31;
            this.label10.Text = "Line Number";
            // 
            // currentLineLbl
            // 
            this.currentLineLbl.AutoSize = true;
            this.currentLineLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentLineLbl.Location = new System.Drawing.Point(406, 132);
            this.currentLineLbl.Name = "currentLineLbl";
            this.currentLineLbl.Size = new System.Drawing.Size(130, 25);
            this.currentLineLbl.TabIndex = 31;
            this.currentLineLbl.Text = "Current Line";
            // 
            // currentLineBox
            // 
            this.currentLineBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentLineBox.Location = new System.Drawing.Point(411, 165);
            this.currentLineBox.MaxLength = 0;
            this.currentLineBox.Name = "currentLineBox";
            this.currentLineBox.ReadOnly = true;
            this.currentLineBox.Size = new System.Drawing.Size(379, 31);
            this.currentLineBox.TabIndex = 2;
            // 
            // runGCode
            // 
            this.runGCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runGCode.Location = new System.Drawing.Point(411, 27);
            this.runGCode.Name = "runGCode";
            this.runGCode.Size = new System.Drawing.Size(142, 66);
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
            this.GCodeTxt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.GCodeTxt.Size = new System.Drawing.Size(379, 206);
            this.GCodeTxt.TabIndex = 0;
            // 
            // referenceTab
            // 
            this.referenceTab.Controls.Add(this.refButton);
            this.referenceTab.Location = new System.Drawing.Point(4, 33);
            this.referenceTab.Name = "referenceTab";
            this.referenceTab.Padding = new System.Windows.Forms.Padding(3);
            this.referenceTab.Size = new System.Drawing.Size(802, 243);
            this.referenceTab.TabIndex = 0;
            this.referenceTab.Text = "Reference";
            this.referenceTab.UseVisualStyleBackColor = true;
            // 
            // refButton
            // 
            this.refButton.Enabled = false;
            this.refButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.ModeControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ModeControl.Location = new System.Drawing.Point(12, 545);
            this.ModeControl.Name = "ModeControl";
            this.ModeControl.SelectedIndex = 0;
            this.ModeControl.Size = new System.Drawing.Size(810, 280);
            this.ModeControl.TabIndex = 22;
            this.ModeControl.Click += new System.EventHandler(this.ModeControl_Click);
            // 
            // ErrorBox
            // 
            this.ErrorBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.ErrorBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorBox.Location = new System.Drawing.Point(850, 718);
            this.ErrorBox.Name = "ErrorBox";
            this.ErrorBox.Size = new System.Drawing.Size(291, 29);
            this.ErrorBox.TabIndex = 25;
            this.ErrorBox.Text = "Outside safe milling volume";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(842, 378);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(95, 24);
            this.label14.TabIndex = 24;
            this.label14.Text = "X Velocity";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(842, 413);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(93, 24);
            this.label15.TabIndex = 24;
            this.label15.Text = "Y Velocity";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(842, 453);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(93, 24);
            this.label16.TabIndex = 24;
            this.label16.Text = "Z Velocity";
            // 
            // xVelBox
            // 
            this.xVelBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xVelBox.Location = new System.Drawing.Point(999, 378);
            this.xVelBox.Name = "xVelBox";
            this.xVelBox.Size = new System.Drawing.Size(111, 29);
            this.xVelBox.TabIndex = 26;
            // 
            // yVelBox
            // 
            this.yVelBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yVelBox.Location = new System.Drawing.Point(999, 413);
            this.yVelBox.Name = "yVelBox";
            this.yVelBox.Size = new System.Drawing.Size(111, 29);
            this.yVelBox.TabIndex = 26;
            // 
            // zVelBox
            // 
            this.zVelBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zVelBox.Location = new System.Drawing.Point(999, 448);
            this.zVelBox.Name = "zVelBox";
            this.zVelBox.Size = new System.Drawing.Size(111, 29);
            this.zVelBox.TabIndex = 26;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(842, 502);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(134, 24);
            this.label17.TabIndex = 24;
            this.label17.Text = "X Acceleration";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(842, 528);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(132, 24);
            this.label18.TabIndex = 24;
            this.label18.Text = "Y Acceleration";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(842, 555);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(132, 24);
            this.label19.TabIndex = 24;
            this.label19.Text = "Z Acceleration";
            // 
            // xAccelBox
            // 
            this.xAccelBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xAccelBox.Location = new System.Drawing.Point(1001, 502);
            this.xAccelBox.Name = "xAccelBox";
            this.xAccelBox.Size = new System.Drawing.Size(109, 29);
            this.xAccelBox.TabIndex = 26;
            // 
            // yAccelBox
            // 
            this.yAccelBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yAccelBox.Location = new System.Drawing.Point(1001, 529);
            this.yAccelBox.Name = "yAccelBox";
            this.yAccelBox.Size = new System.Drawing.Size(109, 29);
            this.yAccelBox.TabIndex = 26;
            // 
            // zAccelBox
            // 
            this.zAccelBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zAccelBox.Location = new System.Drawing.Point(1001, 559);
            this.zAccelBox.Name = "zAccelBox";
            this.zAccelBox.Size = new System.Drawing.Size(109, 29);
            this.zAccelBox.TabIndex = 26;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1189, 822);
            this.Controls.Add(this.statusBox);
            this.Controls.Add(this.ModeControl);
            this.Controls.Add(this.lineComplete);
            this.Controls.Add(this.glControl1);
            this.Controls.Add(this.alarmBtn);
            this.Controls.Add(this.feedMinusButton);
            this.Controls.Add(this.feedPlusButton);
            this.Controls.Add(this.offsetButton);
            this.Controls.Add(this.zAccelBox);
            this.Controls.Add(this.zVelBox);
            this.Controls.Add(this.yAccelBox);
            this.Controls.Add(this.zOffseted);
            this.Controls.Add(this.yVelBox);
            this.Controls.Add(this.xAccelBox);
            this.Controls.Add(this.yOffseted);
            this.Controls.Add(this.xVelBox);
            this.Controls.Add(this.xOffseted);
            this.Controls.Add(this.sBox);
            this.Controls.Add(this.zBox);
            this.Controls.Add(this.feedRatePercentTxtbox);
            this.Controls.Add(this.ErrorBox);
            this.Controls.Add(this.fBox);
            this.Controls.Add(this.yBox);
            this.Controls.Add(this.xBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "iWindow";
            this.Load += new System.EventHandler(this.Form1_Load);
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
        private System.Windows.Forms.Button cadToggle;
        private System.Windows.Forms.Button toolpathToggle;
        private System.Windows.Forms.Button axisToggle;
        private System.Windows.Forms.Button drillTipToggle;
        private System.Windows.Forms.Label currentLineLbl;
        private System.Windows.Forms.TextBox currentLineBox;
        private System.Windows.Forms.TextBox lineNumberBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox ErrorBox;
        private System.Windows.Forms.Button toggleBadVolume;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox xVelBox;
        private System.Windows.Forms.TextBox yVelBox;
        private System.Windows.Forms.TextBox zVelBox;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox xAccelBox;
        private System.Windows.Forms.TextBox yAccelBox;
        private System.Windows.Forms.TextBox zAccelBox;
    }
}

