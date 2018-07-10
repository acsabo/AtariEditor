namespace Atari2600Editor
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonNew = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.panelTools = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbSprite48 = new System.Windows.Forms.RadioButton();
            this.rbSprite8 = new System.Windows.Forms.RadioButton();
            this.rbPlayfied = new System.Windows.Forms.RadioButton();
            this.labelSelectedCell = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelTotalHeight = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelCurrentLine = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelBK = new System.Windows.Forms.Label();
            this.labelFG = new System.Windows.Forms.Label();
            this.labelBKColor = new System.Windows.Forms.Label();
            this.labelFGColor = new System.Windows.Forms.Label();
            this.buttonDecRowHeight = new System.Windows.Forms.Button();
            this.buttonIncRowHight = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.bufferedPanel1 = new PhotoShopColorSwatchLoader.BufferedPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panelTools.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelLeft.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonNew
            // 
            this.buttonNew.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonNew.Location = new System.Drawing.Point(12, 16);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(75, 23);
            this.buttonNew.TabIndex = 1;
            this.buttonNew.Text = "New";
            this.buttonNew.UseVisualStyleBackColor = true;
            this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(12, 45);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // panelTools
            // 
            this.panelTools.Controls.Add(this.groupBox1);
            this.panelTools.Controls.Add(this.labelSelectedCell);
            this.panelTools.Controls.Add(this.label3);
            this.panelTools.Controls.Add(this.labelTotalHeight);
            this.panelTools.Controls.Add(this.label2);
            this.panelTools.Controls.Add(this.labelCurrentLine);
            this.panelTools.Controls.Add(this.label1);
            this.panelTools.Controls.Add(this.labelBK);
            this.panelTools.Controls.Add(this.labelFG);
            this.panelTools.Controls.Add(this.labelBKColor);
            this.panelTools.Controls.Add(this.labelFGColor);
            this.panelTools.Controls.Add(this.buttonDecRowHeight);
            this.panelTools.Controls.Add(this.buttonIncRowHight);
            this.panelTools.Controls.Add(this.button3);
            this.panelTools.Controls.Add(this.buttonNew);
            this.panelTools.Controls.Add(this.buttonSave);
            this.panelTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTools.Location = new System.Drawing.Point(0, 0);
            this.panelTools.Name = "panelTools";
            this.panelTools.Size = new System.Drawing.Size(255, 251);
            this.panelTools.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbSprite48);
            this.groupBox1.Controls.Add(this.rbSprite8);
            this.groupBox1.Controls.Add(this.rbPlayfied);
            this.groupBox1.Location = new System.Drawing.Point(139, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(107, 88);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // rbSprite48
            // 
            this.rbSprite48.AutoSize = true;
            this.rbSprite48.Location = new System.Drawing.Point(7, 65);
            this.rbSprite48.Name = "rbSprite48";
            this.rbSprite48.Size = new System.Drawing.Size(89, 17);
            this.rbSprite48.TabIndex = 23;
            this.rbSprite48.Text = "Sprite(48 bits)";
            this.rbSprite48.UseVisualStyleBackColor = true;
            // 
            // rbSprite8
            // 
            this.rbSprite8.AutoSize = true;
            this.rbSprite8.Location = new System.Drawing.Point(7, 42);
            this.rbSprite8.Name = "rbSprite8";
            this.rbSprite8.Size = new System.Drawing.Size(86, 17);
            this.rbSprite8.TabIndex = 22;
            this.rbSprite8.Text = "Sprite (8 bits)";
            this.rbSprite8.UseVisualStyleBackColor = true;
            // 
            // rbPlayfied
            // 
            this.rbPlayfied.AutoSize = true;
            this.rbPlayfied.Checked = true;
            this.rbPlayfied.Location = new System.Drawing.Point(6, 19);
            this.rbPlayfied.Name = "rbPlayfied";
            this.rbPlayfied.Size = new System.Drawing.Size(64, 17);
            this.rbPlayfied.TabIndex = 21;
            this.rbPlayfied.TabStop = true;
            this.rbPlayfied.Text = "Playfield";
            this.rbPlayfied.UseVisualStyleBackColor = true;
            // 
            // labelSelectedCell
            // 
            this.labelSelectedCell.AutoSize = true;
            this.labelSelectedCell.Location = new System.Drawing.Point(97, 128);
            this.labelSelectedCell.Name = "labelSelectedCell";
            this.labelSelectedCell.Size = new System.Drawing.Size(13, 13);
            this.labelSelectedCell.TabIndex = 19;
            this.labelSelectedCell.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Selected:";
            // 
            // labelTotalHeight
            // 
            this.labelTotalHeight.AutoSize = true;
            this.labelTotalHeight.Location = new System.Drawing.Point(94, 145);
            this.labelTotalHeight.Name = "labelTotalHeight";
            this.labelTotalHeight.Size = new System.Drawing.Size(13, 13);
            this.labelTotalHeight.TabIndex = 17;
            this.labelTotalHeight.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Total height:";
            // 
            // labelCurrentLine
            // 
            this.labelCurrentLine.AutoSize = true;
            this.labelCurrentLine.Location = new System.Drawing.Point(53, 171);
            this.labelCurrentLine.Name = "labelCurrentLine";
            this.labelCurrentLine.Size = new System.Drawing.Size(13, 13);
            this.labelCurrentLine.TabIndex = 15;
            this.labelCurrentLine.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 171);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Line:";
            // 
            // labelBK
            // 
            this.labelBK.AutoSize = true;
            this.labelBK.Location = new System.Drawing.Point(16, 221);
            this.labelBK.Name = "labelBK";
            this.labelBK.Size = new System.Drawing.Size(65, 13);
            this.labelBK.TabIndex = 13;
            this.labelBK.Text = "Background";
            // 
            // labelFG
            // 
            this.labelFG.AutoSize = true;
            this.labelFG.Location = new System.Drawing.Point(16, 196);
            this.labelFG.Name = "labelFG";
            this.labelFG.Size = new System.Drawing.Size(61, 13);
            this.labelFG.TabIndex = 12;
            this.labelFG.Text = "Foreground";
            // 
            // labelBKColor
            // 
            this.labelBKColor.AutoSize = true;
            this.labelBKColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelBKColor.Location = new System.Drawing.Point(94, 221);
            this.labelBKColor.Name = "labelBKColor";
            this.labelBKColor.Padding = new System.Windows.Forms.Padding(0, 0, 30, 0);
            this.labelBKColor.Size = new System.Drawing.Size(32, 15);
            this.labelBKColor.TabIndex = 11;
            // 
            // labelFGColor
            // 
            this.labelFGColor.AutoSize = true;
            this.labelFGColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelFGColor.Location = new System.Drawing.Point(94, 194);
            this.labelFGColor.Name = "labelFGColor";
            this.labelFGColor.Padding = new System.Windows.Forms.Padding(0, 0, 30, 0);
            this.labelFGColor.Size = new System.Drawing.Size(32, 15);
            this.labelFGColor.TabIndex = 10;
            // 
            // buttonDecRowHeight
            // 
            this.buttonDecRowHeight.Location = new System.Drawing.Point(215, 216);
            this.buttonDecRowHeight.Name = "buttonDecRowHeight";
            this.buttonDecRowHeight.Size = new System.Drawing.Size(31, 23);
            this.buttonDecRowHeight.TabIndex = 8;
            this.buttonDecRowHeight.Text = "-";
            this.buttonDecRowHeight.UseVisualStyleBackColor = true;
            this.buttonDecRowHeight.Click += new System.EventHandler(this.buttonDecRowHeight_Click);
            // 
            // buttonIncRowHight
            // 
            this.buttonIncRowHight.Location = new System.Drawing.Point(215, 194);
            this.buttonIncRowHight.Name = "buttonIncRowHight";
            this.buttonIncRowHight.Size = new System.Drawing.Size(31, 23);
            this.buttonIncRowHight.TabIndex = 7;
            this.buttonIncRowHight.Text = "+";
            this.buttonIncRowHight.UseVisualStyleBackColor = true;
            this.buttonIncRowHight.Click += new System.EventHandler(this.buttonIncRowHeight_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(742, 15);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Mirror";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.ColumnHeadersHeight = 15;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridView1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.RowHeadersWidth = 55;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.Black;
            this.dataGridView1.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
            this.dataGridView1.RowTemplate.Height = 10;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.ShowCellErrors = false;
            this.dataGridView1.ShowCellToolTips = false;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.ShowRowErrors = false;
            this.dataGridView1.Size = new System.Drawing.Size(1214, 833);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            this.dataGridView1.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellMouseEnter);
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            this.dataGridView1.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseDoubleClick);
            this.dataGridView1.RowHeightChanged += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView1_RowHeightChanged);
            this.dataGridView1.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowLeave);
            this.dataGridView1.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView1_RowsAdded);
            this.dataGridView1.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView1_RowsRemoved);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            this.dataGridView1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGridView1_KeyPress);
            this.dataGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDown);
            this.dataGridView1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseMove);
            this.dataGridView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseUp);
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.bufferedPanel1);
            this.panelLeft.Controls.Add(this.panelTools);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(255, 865);
            this.panelLeft.TabIndex = 4;
            // 
            // bufferedPanel1
            // 
            this.bufferedPanel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.bufferedPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bufferedPanel1.Location = new System.Drawing.Point(0, 251);
            this.bufferedPanel1.Name = "bufferedPanel1";
            this.bufferedPanel1.Size = new System.Drawing.Size(255, 614);
            this.bufferedPanel1.TabIndex = 13;
            this.bufferedPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.bufferedPanelColorPalette_Paint);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(255, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1228, 865);
            this.panel1.TabIndex = 5;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "1";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.Width = 15;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1228, 865);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1220, 839);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Playfield";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1220, 839);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1483, 865);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelLeft);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Atari 2600 Editor";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.panelTools.ResumeLayout(false);
            this.panelTools.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelLeft.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonNew;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Panel panelTools;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonDecRowHeight;
        private System.Windows.Forms.Button buttonIncRowHight;
        private System.Windows.Forms.Label labelBKColor;
        private System.Windows.Forms.Label labelFGColor;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private PhotoShopColorSwatchLoader.BufferedPanel bufferedPanel1;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelBK;
        private System.Windows.Forms.Label labelFG;
        private System.Windows.Forms.Label labelCurrentLine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelTotalHeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelSelectedCell;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbSprite48;
        private System.Windows.Forms.RadioButton rbSprite8;
        private System.Windows.Forms.RadioButton rbPlayfied;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}

