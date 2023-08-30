using System.Windows.Forms.DataVisualization.Charting;

namespace ImageProcessing2
{
    partial class MainForm
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
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            tableLayout = new TableLayoutPanel();
            sobelPictureBox = new PictureBox();
            grayscalePictureBox = new PictureBox();
            label8 = new Label();
            equalizedPictureBox = new PictureBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            originalPictureBox = new PictureBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            prewittPictureBox = new PictureBox();
            robertsPictureBox = new PictureBox();
            menuStrip1.SuspendLayout();
            tableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)sobelPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)grayscalePictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)equalizedPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)originalPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)prewittPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)robertsPictureBox).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1095, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            openToolStripMenuItem.Size = new Size(181, 26);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(181, 26);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            // 
            // tableLayout
            // 
            tableLayout.AutoScroll = true;
            tableLayout.ColumnCount = 8;
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 500F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 500F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 500F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 500F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 500F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 500F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 500F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 500F));
            tableLayout.Controls.Add(robertsPictureBox, 5, 1);
            tableLayout.Controls.Add(prewittPictureBox, 6, 1);
            tableLayout.Controls.Add(sobelPictureBox, 7, 1);
            tableLayout.Controls.Add(grayscalePictureBox, 4, 1);
            tableLayout.Controls.Add(label8, 4, 0);
            tableLayout.Controls.Add(equalizedPictureBox, 2, 1);
            tableLayout.Controls.Add(label4, 3, 0);
            tableLayout.Controls.Add(label3, 2, 0);
            tableLayout.Controls.Add(label2, 1, 0);
            tableLayout.Controls.Add(label1, 0, 0);
            tableLayout.Controls.Add(originalPictureBox, 0, 1);
            tableLayout.Controls.Add(label7, 7, 0);
            tableLayout.Controls.Add(label6, 6, 0);
            tableLayout.Controls.Add(label5, 5, 0);
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.Location = new Point(0, 28);
            tableLayout.Name = "tableLayout";
            tableLayout.RowCount = 3;
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 500F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayout.Size = new Size(1095, 590);
            tableLayout.TabIndex = 1;
            // 
            // sobelPictureBox
            // 
            sobelPictureBox.Dock = DockStyle.Fill;
            sobelPictureBox.Location = new Point(3503, 33);
            sobelPictureBox.Name = "sobelPictureBox";
            sobelPictureBox.Size = new Size(494, 494);
            sobelPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            sobelPictureBox.TabIndex = 11;
            sobelPictureBox.TabStop = false;
            // 
            // grayscalePictureBox
            // 
            grayscalePictureBox.Dock = DockStyle.Fill;
            grayscalePictureBox.Location = new Point(2003, 33);
            grayscalePictureBox.Name = "grayscalePictureBox";
            grayscalePictureBox.Size = new Size(494, 494);
            grayscalePictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            grayscalePictureBox.TabIndex = 10;
            grayscalePictureBox.TabStop = false;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Location = new Point(2003, 0);
            label8.Name = "label8";
            label8.Size = new Size(494, 30);
            label8.TabIndex = 9;
            label8.Text = "Grayscale";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // equalizedPictureBox
            // 
            equalizedPictureBox.Dock = DockStyle.Fill;
            equalizedPictureBox.Location = new Point(1003, 33);
            equalizedPictureBox.Name = "equalizedPictureBox";
            equalizedPictureBox.Size = new Size(494, 494);
            equalizedPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            equalizedPictureBox.TabIndex = 8;
            equalizedPictureBox.TabStop = false;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new Point(1503, 0);
            label4.Name = "label4";
            label4.Size = new Size(494, 30);
            label4.TabIndex = 3;
            label4.Text = "Equalized Histogram";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(1003, 0);
            label3.Name = "label3";
            label3.Size = new Size(494, 30);
            label3.TabIndex = 2;
            label3.Text = "Equalized Image";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(503, 0);
            label2.Name = "label2";
            label2.Size = new Size(494, 30);
            label2.TabIndex = 1;
            label2.Text = "Original Histogram";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(494, 30);
            label1.TabIndex = 0;
            label1.Text = "Original Image";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // originalPictureBox
            // 
            originalPictureBox.Dock = DockStyle.Fill;
            originalPictureBox.Location = new Point(3, 33);
            originalPictureBox.Name = "originalPictureBox";
            originalPictureBox.Size = new Size(494, 494);
            originalPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            originalPictureBox.TabIndex = 7;
            originalPictureBox.TabStop = false;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Location = new Point(3503, 0);
            label7.Name = "label7";
            label7.Size = new Size(494, 30);
            label7.TabIndex = 6;
            label7.Text = "Sobel Operator Image";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Location = new Point(3003, 0);
            label6.Name = "label6";
            label6.Size = new Size(494, 30);
            label6.TabIndex = 5;
            label6.Text = "Prewitt Operator Image";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Location = new Point(2503, 0);
            label5.Name = "label5";
            label5.Size = new Size(494, 30);
            label5.TabIndex = 4;
            label5.Text = "Roberts Operator Image";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // prewittPictureBox
            // 
            prewittPictureBox.Dock = DockStyle.Fill;
            prewittPictureBox.Location = new Point(3003, 33);
            prewittPictureBox.Name = "prewittPictureBox";
            prewittPictureBox.Size = new Size(494, 494);
            prewittPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            prewittPictureBox.TabIndex = 12;
            prewittPictureBox.TabStop = false;
            // 
            // robertsPictureBox
            // 
            robertsPictureBox.Dock = DockStyle.Fill;
            robertsPictureBox.Location = new Point(2503, 33);
            robertsPictureBox.Name = "robertsPictureBox";
            robertsPictureBox.Size = new Size(494, 494);
            robertsPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            robertsPictureBox.TabIndex = 13;
            robertsPictureBox.TabStop = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1095, 618);
            Controls.Add(tableLayout);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "ImageProcessing2";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tableLayout.ResumeLayout(false);
            tableLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)sobelPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)grayscalePictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)equalizedPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)originalPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)prewittPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)robertsPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private TableLayoutPanel tableLayout;
        private Label label1;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label7;
        private Label label6;
        private Label label5;
        private PictureBox originalPictureBox;
        private PictureBox equalizedPictureBox;
        private Label label8;
        private PictureBox grayscalePictureBox;
        private PictureBox sobelPictureBox;
        private PictureBox prewittPictureBox;
        private PictureBox robertsPictureBox;
    }
}