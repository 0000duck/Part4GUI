namespace PappachanNC3
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
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.terminateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.referenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.functionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.referenceToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.statusBox = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.moveXup = new System.Windows.Forms.Button();
            this.moveXdown = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.moveYup = new System.Windows.Forms.Button();
            this.moveYdown = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.moveZdown = new System.Windows.Forms.Button();
            this.moveZup = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.counterCircle = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.circularInterpButton = new System.Windows.Forms.Button();
            this.radiusBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.feedRateBox = new System.Windows.Forms.TextBox();
            this.sendProgramButton = new System.Windows.Forms.Button();
            this.rapidMove = new System.Windows.Forms.RadioButton();
            this.SpindleOffButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.spindleOnButton = new System.Windows.Forms.Button();
            this.spindleSpeedBox = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.codeBox = new System.Windows.Forms.RichTextBox();
            this.addLocButton = new System.Windows.Forms.Button();
            this.pgmZ = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pgmY = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pgmX = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.feedDownButton = new System.Windows.Forms.Button();
            this.feedUpButton = new System.Windows.Forms.Button();
            this.jogXlabel = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.mDIModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.M03Button = new System.Windows.Forms.Button();
            this.M05Button = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.connectionToolStripMenuItem,
            this.modesToolStripMenuItem,
            this.functionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(797, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.closeToolStripMenuItem.Text = "Close";
            // 
            // connectionToolStripMenuItem
            // 
            this.connectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.terminateToolStripMenuItem});
            this.connectionToolStripMenuItem.Name = "connectionToolStripMenuItem";
            this.connectionToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.connectionToolStripMenuItem.Text = "Connection";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // terminateToolStripMenuItem
            // 
            this.terminateToolStripMenuItem.Enabled = false;
            this.terminateToolStripMenuItem.Name = "terminateToolStripMenuItem";
            this.terminateToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.terminateToolStripMenuItem.Text = "Terminate";
            this.terminateToolStripMenuItem.Click += new System.EventHandler(this.terminateToolStripMenuItem_Click);
            // 
            // modesToolStripMenuItem
            // 
            this.modesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.referenceToolStripMenuItem,
            this.memoryToolStripMenuItem,
            this.jogToolStripMenuItem,
            this.editToolStripMenuItem,
            this.mDIModeToolStripMenuItem});
            this.modesToolStripMenuItem.Name = "modesToolStripMenuItem";
            this.modesToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.modesToolStripMenuItem.Text = "Modes";
            // 
            // referenceToolStripMenuItem
            // 
            this.referenceToolStripMenuItem.Name = "referenceToolStripMenuItem";
            this.referenceToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.referenceToolStripMenuItem.Text = "Reference Mode";
            this.referenceToolStripMenuItem.Click += new System.EventHandler(this.referenceToolStripMenuItem_Click);
            // 
            // memoryToolStripMenuItem
            // 
            this.memoryToolStripMenuItem.Name = "memoryToolStripMenuItem";
            this.memoryToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.memoryToolStripMenuItem.Text = "Memory Mode";
            this.memoryToolStripMenuItem.Click += new System.EventHandler(this.memoryToolStripMenuItem_Click);
            // 
            // jogToolStripMenuItem
            // 
            this.jogToolStripMenuItem.Name = "jogToolStripMenuItem";
            this.jogToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.jogToolStripMenuItem.Text = "Jog Mode";
            this.jogToolStripMenuItem.Click += new System.EventHandler(this.jogToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.editToolStripMenuItem.Text = "Edit Mode";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // functionsToolStripMenuItem
            // 
            this.functionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.referenceToolStripMenuItem1});
            this.functionsToolStripMenuItem.Name = "functionsToolStripMenuItem";
            this.functionsToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.functionsToolStripMenuItem.Text = "Functions";
            // 
            // referenceToolStripMenuItem1
            // 
            this.referenceToolStripMenuItem1.Enabled = false;
            this.referenceToolStripMenuItem1.Name = "referenceToolStripMenuItem1";
            this.referenceToolStripMenuItem1.Size = new System.Drawing.Size(126, 22);
            this.referenceToolStripMenuItem1.Text = "Reference";
            this.referenceToolStripMenuItem1.Click += new System.EventHandler(this.referenceToolStripMenuItem1_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.statusBox);
            this.groupBox1.Location = new System.Drawing.Point(13, 249);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(772, 107);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status";
            // 
            // statusBox
            // 
            this.statusBox.Location = new System.Drawing.Point(7, 19);
            this.statusBox.Name = "statusBox";
            this.statusBox.Size = new System.Drawing.Size(759, 82);
            this.statusBox.TabIndex = 0;
            this.statusBox.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.moveXup);
            this.groupBox2.Controls.Add(this.moveXdown);
            this.groupBox2.Location = new System.Drawing.Point(615, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(170, 52);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "X axis";
            // 
            // moveXup
            // 
            this.moveXup.Enabled = false;
            this.moveXup.Location = new System.Drawing.Point(88, 19);
            this.moveXup.Name = "moveXup";
            this.moveXup.Size = new System.Drawing.Size(75, 23);
            this.moveXup.TabIndex = 1;
            this.moveXup.Text = "Right";
            this.moveXup.UseVisualStyleBackColor = true;
            this.moveXup.Click += new System.EventHandler(this.moveXup_Click);
            // 
            // moveXdown
            // 
            this.moveXdown.Enabled = false;
            this.moveXdown.Location = new System.Drawing.Point(7, 20);
            this.moveXdown.Name = "moveXdown";
            this.moveXdown.Size = new System.Drawing.Size(75, 23);
            this.moveXdown.TabIndex = 0;
            this.moveXdown.Text = "Left";
            this.moveXdown.UseVisualStyleBackColor = true;
            this.moveXdown.Click += new System.EventHandler(this.moveXdown_Click_1);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.moveYup);
            this.groupBox3.Controls.Add(this.moveYdown);
            this.groupBox3.Location = new System.Drawing.Point(615, 82);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(170, 52);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Y axis";
            // 
            // moveYup
            // 
            this.moveYup.Enabled = false;
            this.moveYup.Location = new System.Drawing.Point(88, 19);
            this.moveYup.Name = "moveYup";
            this.moveYup.Size = new System.Drawing.Size(75, 23);
            this.moveYup.TabIndex = 1;
            this.moveYup.Text = "Front";
            this.moveYup.UseVisualStyleBackColor = true;
            this.moveYup.Click += new System.EventHandler(this.moveYup_Click);
            // 
            // moveYdown
            // 
            this.moveYdown.Enabled = false;
            this.moveYdown.Location = new System.Drawing.Point(7, 20);
            this.moveYdown.Name = "moveYdown";
            this.moveYdown.Size = new System.Drawing.Size(75, 23);
            this.moveYdown.TabIndex = 0;
            this.moveYdown.Text = "Back";
            this.moveYdown.UseVisualStyleBackColor = true;
            this.moveYdown.Click += new System.EventHandler(this.moveYdown_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.moveZdown);
            this.groupBox4.Controls.Add(this.moveZup);
            this.groupBox4.Location = new System.Drawing.Point(615, 138);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(170, 52);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Z axis";
            // 
            // moveZdown
            // 
            this.moveZdown.Enabled = false;
            this.moveZdown.Location = new System.Drawing.Point(7, 19);
            this.moveZdown.Name = "moveZdown";
            this.moveZdown.Size = new System.Drawing.Size(75, 23);
            this.moveZdown.TabIndex = 1;
            this.moveZdown.Text = "Down";
            this.moveZdown.UseVisualStyleBackColor = true;
            this.moveZdown.Click += new System.EventHandler(this.moveZdown_Click);
            // 
            // moveZup
            // 
            this.moveZup.Enabled = false;
            this.moveZup.Location = new System.Drawing.Point(88, 19);
            this.moveZup.Name = "moveZup";
            this.moveZup.Size = new System.Drawing.Size(75, 23);
            this.moveZup.TabIndex = 0;
            this.moveZup.Text = "Up";
            this.moveZup.UseVisualStyleBackColor = true;
            this.moveZup.Click += new System.EventHandler(this.moveZup_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.counterCircle);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.circularInterpButton);
            this.groupBox5.Controls.Add(this.radiusBox);
            this.groupBox5.Controls.Add(this.M05Button);
            this.groupBox5.Controls.Add(this.M03Button);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.feedRateBox);
            this.groupBox5.Controls.Add(this.sendProgramButton);
            this.groupBox5.Controls.Add(this.rapidMove);
            this.groupBox5.Controls.Add(this.SpindleOffButton);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.spindleOnButton);
            this.groupBox5.Controls.Add(this.spindleSpeedBox);
            this.groupBox5.Controls.Add(this.groupBox6);
            this.groupBox5.Controls.Add(this.addLocButton);
            this.groupBox5.Controls.Add(this.pgmZ);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.pgmY);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.pgmX);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Location = new System.Drawing.Point(13, 27);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(596, 216);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Program";
            // 
            // counterCircle
            // 
            this.counterCircle.AutoSize = true;
            this.counterCircle.Location = new System.Drawing.Point(213, 99);
            this.counterCircle.Name = "counterCircle";
            this.counterCircle.Size = new System.Drawing.Size(114, 17);
            this.counterCircle.TabIndex = 21;
            this.counterCircle.Text = "Counter Clockwise";
            this.counterCircle.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Radius";
            // 
            // circularInterpButton
            // 
            this.circularInterpButton.Location = new System.Drawing.Point(131, 95);
            this.circularInterpButton.Name = "circularInterpButton";
            this.circularInterpButton.Size = new System.Drawing.Size(75, 23);
            this.circularInterpButton.TabIndex = 19;
            this.circularInterpButton.Text = "Circular Int.";
            this.circularInterpButton.UseVisualStyleBackColor = true;
            this.circularInterpButton.Click += new System.EventHandler(this.circularInterpButton_Click);
            // 
            // radiusBox
            // 
            this.radiusBox.Location = new System.Drawing.Point(62, 97);
            this.radiusBox.Name = "radiusBox";
            this.radiusBox.Size = new System.Drawing.Size(63, 20);
            this.radiusBox.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Feed rate";
            // 
            // feedRateBox
            // 
            this.feedRateBox.Location = new System.Drawing.Point(62, 154);
            this.feedRateBox.Name = "feedRateBox";
            this.feedRateBox.Size = new System.Drawing.Size(63, 20);
            this.feedRateBox.TabIndex = 15;
            this.feedRateBox.Text = "200";
            // 
            // sendProgramButton
            // 
            this.sendProgramButton.Location = new System.Drawing.Point(228, 12);
            this.sendProgramButton.Name = "sendProgramButton";
            this.sendProgramButton.Size = new System.Drawing.Size(104, 30);
            this.sendProgramButton.TabIndex = 14;
            this.sendProgramButton.Text = "Send Program";
            this.sendProgramButton.UseVisualStyleBackColor = true;
            this.sendProgramButton.Click += new System.EventHandler(this.sendProgramButton_Click);
            // 
            // rapidMove
            // 
            this.rapidMove.AutoSize = true;
            this.rapidMove.Location = new System.Drawing.Point(7, 74);
            this.rapidMove.Name = "rapidMove";
            this.rapidMove.Size = new System.Drawing.Size(106, 17);
            this.rapidMove.TabIndex = 13;
            this.rapidMove.Text = "Rapid Movement";
            this.rapidMove.UseVisualStyleBackColor = true;
            // 
            // SpindleOffButton
            // 
            this.SpindleOffButton.Location = new System.Drawing.Point(212, 125);
            this.SpindleOffButton.Name = "SpindleOffButton";
            this.SpindleOffButton.Size = new System.Drawing.Size(75, 23);
            this.SpindleOffButton.TabIndex = 12;
            this.SpindleOffButton.Text = "Spindle Off";
            this.SpindleOffButton.UseVisualStyleBackColor = true;
            this.SpindleOffButton.Click += new System.EventHandler(this.SpindleOffButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Spindle";
            // 
            // spindleOnButton
            // 
            this.spindleOnButton.Location = new System.Drawing.Point(131, 125);
            this.spindleOnButton.Name = "spindleOnButton";
            this.spindleOnButton.Size = new System.Drawing.Size(75, 23);
            this.spindleOnButton.TabIndex = 10;
            this.spindleOnButton.Text = "Spindle On";
            this.spindleOnButton.UseVisualStyleBackColor = true;
            this.spindleOnButton.Click += new System.EventHandler(this.spindleOnButton_Click);
            // 
            // spindleSpeedBox
            // 
            this.spindleSpeedBox.Location = new System.Drawing.Point(62, 127);
            this.spindleSpeedBox.Name = "spindleSpeedBox";
            this.spindleSpeedBox.Size = new System.Drawing.Size(63, 20);
            this.spindleSpeedBox.TabIndex = 9;
            this.spindleSpeedBox.Text = "1000";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.codeBox);
            this.groupBox6.Location = new System.Drawing.Point(340, 16);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(250, 194);
            this.groupBox6.TabIndex = 8;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Resulting Program";
            // 
            // codeBox
            // 
            this.codeBox.Location = new System.Drawing.Point(6, 19);
            this.codeBox.Name = "codeBox";
            this.codeBox.Size = new System.Drawing.Size(238, 169);
            this.codeBox.TabIndex = 7;
            this.codeBox.Text = "";
            // 
            // addLocButton
            // 
            this.addLocButton.Location = new System.Drawing.Point(254, 44);
            this.addLocButton.Name = "addLocButton";
            this.addLocButton.Size = new System.Drawing.Size(75, 23);
            this.addLocButton.TabIndex = 6;
            this.addLocButton.Text = "Add";
            this.addLocButton.UseVisualStyleBackColor = true;
            this.addLocButton.Click += new System.EventHandler(this.addLocButton_Click);
            // 
            // pgmZ
            // 
            this.pgmZ.Location = new System.Drawing.Point(186, 47);
            this.pgmZ.Name = "pgmZ";
            this.pgmZ.Size = new System.Drawing.Size(55, 20);
            this.pgmZ.TabIndex = 5;
            this.pgmZ.Text = "188.454";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(166, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Z";
            // 
            // pgmY
            // 
            this.pgmY.Location = new System.Drawing.Point(105, 47);
            this.pgmY.Name = "pgmY";
            this.pgmY.Size = new System.Drawing.Size(55, 20);
            this.pgmY.TabIndex = 3;
            this.pgmY.Text = "125.403";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(85, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Y";
            // 
            // pgmX
            // 
            this.pgmX.Location = new System.Drawing.Point(24, 48);
            this.pgmX.Name = "pgmX";
            this.pgmX.Size = new System.Drawing.Size(55, 20);
            this.pgmX.TabIndex = 1;
            this.pgmX.Text = "291.001";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "X";
            // 
            // groupBox7
            // 
            this.groupBox7.BackColor = System.Drawing.Color.Lavender;
            this.groupBox7.Controls.Add(this.feedDownButton);
            this.groupBox7.Controls.Add(this.feedUpButton);
            this.groupBox7.Location = new System.Drawing.Point(615, 196);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(170, 51);
            this.groupBox7.TabIndex = 4;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Feed Rate";
            // 
            // feedDownButton
            // 
            this.feedDownButton.BackColor = System.Drawing.Color.LightSteelBlue;
            this.feedDownButton.Enabled = false;
            this.feedDownButton.Location = new System.Drawing.Point(7, 19);
            this.feedDownButton.Name = "feedDownButton";
            this.feedDownButton.Size = new System.Drawing.Size(75, 23);
            this.feedDownButton.TabIndex = 1;
            this.feedDownButton.Text = "Down";
            this.feedDownButton.UseVisualStyleBackColor = false;
            // 
            // feedUpButton
            // 
            this.feedUpButton.BackColor = System.Drawing.Color.LightSteelBlue;
            this.feedUpButton.Enabled = false;
            this.feedUpButton.Location = new System.Drawing.Point(88, 19);
            this.feedUpButton.Name = "feedUpButton";
            this.feedUpButton.Size = new System.Drawing.Size(75, 23);
            this.feedUpButton.TabIndex = 0;
            this.feedUpButton.Text = "Up";
            this.feedUpButton.UseVisualStyleBackColor = false;
            // 
            // jogXlabel
            // 
            this.jogXlabel.AutoSize = true;
            this.jogXlabel.Location = new System.Drawing.Point(141, 379);
            this.jogXlabel.Name = "jogXlabel";
            this.jogXlabel.Size = new System.Drawing.Size(13, 13);
            this.jogXlabel.TabIndex = 5;
            this.jogXlabel.Text = "0";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(77, 379);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "X location";
            // 
            // mDIModeToolStripMenuItem
            // 
            this.mDIModeToolStripMenuItem.Name = "mDIModeToolStripMenuItem";
            this.mDIModeToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.mDIModeToolStripMenuItem.Text = "MDI Mode";
            this.mDIModeToolStripMenuItem.Click += new System.EventHandler(this.mDIModeToolStripMenuItem_Click);
            // 
            // M03Button
            // 
            this.M03Button.Location = new System.Drawing.Point(62, 188);
            this.M03Button.Name = "M03Button";
            this.M03Button.Size = new System.Drawing.Size(75, 23);
            this.M03Button.TabIndex = 17;
            this.M03Button.Text = "Send M03";
            this.M03Button.UseVisualStyleBackColor = true;
            this.M03Button.Click += new System.EventHandler(this.M03Button_Click);
            // 
            // M05Button
            // 
            this.M05Button.Location = new System.Drawing.Point(166, 188);
            this.M05Button.Name = "M05Button";
            this.M05Button.Size = new System.Drawing.Size(75, 23);
            this.M05Button.TabIndex = 17;
            this.M05Button.Text = "Send M05";
            this.M05Button.UseVisualStyleBackColor = true;
            this.M05Button.Click += new System.EventHandler(this.M05Button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 415);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.jogXlabel);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "PappachanNC ver 0.3";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem terminateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem referenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem memoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem functionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem referenceToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox statusBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button moveXup;
        private System.Windows.Forms.Button moveXdown;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button moveYup;
        private System.Windows.Forms.Button moveYdown;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button moveZdown;
        private System.Windows.Forms.Button moveZup;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RichTextBox codeBox;
        private System.Windows.Forms.Button addLocButton;
        private System.Windows.Forms.TextBox pgmZ;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox pgmY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox pgmX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SpindleOffButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button spindleOnButton;
        private System.Windows.Forms.TextBox spindleSpeedBox;
        private System.Windows.Forms.RadioButton rapidMove;
        private System.Windows.Forms.Button sendProgramButton;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button feedDownButton;
        private System.Windows.Forms.Button feedUpButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox feedRateBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button circularInterpButton;
        private System.Windows.Forms.TextBox radiusBox;
        private System.Windows.Forms.CheckBox counterCircle;
        private System.Windows.Forms.Label jogXlabel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolStripMenuItem mDIModeToolStripMenuItem;
        private System.Windows.Forms.Button M05Button;
        private System.Windows.Forms.Button M03Button;
    }
}

