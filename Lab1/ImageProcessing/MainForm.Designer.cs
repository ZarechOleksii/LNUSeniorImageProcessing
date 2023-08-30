namespace ImageProcessing
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
            splitContainer1 = new SplitContainer();
            buttonContainer = new TableLayoutPanel();
            toJPEGButton = new Button();
            originalImageButton = new Button();
            toBMPButton = new Button();
            toTIFFButton = new Button();
            tableLayout = new TableLayoutPanel();
            processedImageBox = new PictureBox();
            originalImageBox = new PictureBox();
            resultsTextBox = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            buttonContainer.SuspendLayout();
            tableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)processedImageBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)originalImageBox).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(buttonContainer);
            splitContainer1.Panel1MinSize = 300;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(tableLayout);
            splitContainer1.Panel2MinSize = 600;
            splitContainer1.Size = new Size(982, 553);
            splitContainer1.SplitterDistance = 300;
            splitContainer1.TabIndex = 0;
            // 
            // buttonContainer
            // 
            buttonContainer.ColumnCount = 1;
            buttonContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            buttonContainer.Controls.Add(toJPEGButton, 0, 3);
            buttonContainer.Controls.Add(originalImageButton, 0, 0);
            buttonContainer.Controls.Add(toBMPButton, 0, 1);
            buttonContainer.Controls.Add(toTIFFButton, 0, 2);
            buttonContainer.Dock = DockStyle.Fill;
            buttonContainer.Location = new Point(0, 0);
            buttonContainer.Margin = new Padding(5);
            buttonContainer.Name = "buttonContainer";
            buttonContainer.RowCount = 4;
            buttonContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            buttonContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            buttonContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            buttonContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            buttonContainer.Size = new Size(300, 553);
            buttonContainer.TabIndex = 0;
            // 
            // toJPEGButton
            // 
            toJPEGButton.AutoSize = true;
            toJPEGButton.Dock = DockStyle.Fill;
            toJPEGButton.Enabled = false;
            toJPEGButton.Location = new Point(5, 419);
            toJPEGButton.Margin = new Padding(5);
            toJPEGButton.Name = "toJPEGButton";
            toJPEGButton.Size = new Size(290, 129);
            toJPEGButton.TabIndex = 5;
            toJPEGButton.Text = "Convert To JPEG...";
            toJPEGButton.UseVisualStyleBackColor = true;
            toJPEGButton.Click += ToJPEGButtonClick;
            // 
            // originalImageButton
            // 
            originalImageButton.AutoSize = true;
            originalImageButton.Dock = DockStyle.Fill;
            originalImageButton.Location = new Point(5, 5);
            originalImageButton.Margin = new Padding(5);
            originalImageButton.Name = "originalImageButton";
            originalImageButton.Size = new Size(290, 128);
            originalImageButton.TabIndex = 2;
            originalImageButton.Text = "Choose original image...";
            originalImageButton.UseVisualStyleBackColor = true;
            originalImageButton.Click += OriginalImageButtonClick;
            // 
            // toBMPButton
            // 
            toBMPButton.AutoSize = true;
            toBMPButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            toBMPButton.Dock = DockStyle.Fill;
            toBMPButton.Enabled = false;
            toBMPButton.Location = new Point(5, 143);
            toBMPButton.Margin = new Padding(5);
            toBMPButton.Name = "toBMPButton";
            toBMPButton.Size = new Size(290, 128);
            toBMPButton.TabIndex = 3;
            toBMPButton.Text = "Convert to BMP...";
            toBMPButton.UseVisualStyleBackColor = true;
            toBMPButton.Click += ToBMPButtonClick;
            // 
            // toTIFFButton
            // 
            toTIFFButton.AutoSize = true;
            toTIFFButton.Dock = DockStyle.Fill;
            toTIFFButton.Enabled = false;
            toTIFFButton.Location = new Point(5, 281);
            toTIFFButton.Margin = new Padding(5);
            toTIFFButton.Name = "toTIFFButton";
            toTIFFButton.Size = new Size(290, 128);
            toTIFFButton.TabIndex = 4;
            toTIFFButton.Text = "Convert To TIFF...";
            toTIFFButton.UseVisualStyleBackColor = true;
            toTIFFButton.Click += ToTIFFButtonClick;
            // 
            // tableLayout
            // 
            tableLayout.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayout.ColumnCount = 2;
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayout.Controls.Add(processedImageBox, 1, 0);
            tableLayout.Controls.Add(originalImageBox, 0, 0);
            tableLayout.Controls.Add(resultsTextBox, 0, 1);
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.Location = new Point(0, 0);
            tableLayout.Name = "tableLayout";
            tableLayout.RowCount = 2;
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayout.Size = new Size(678, 553);
            tableLayout.TabIndex = 0;
            // 
            // processedImageBox
            // 
            processedImageBox.Dock = DockStyle.Fill;
            processedImageBox.Location = new Point(342, 4);
            processedImageBox.Name = "processedImageBox";
            processedImageBox.Size = new Size(332, 269);
            processedImageBox.SizeMode = PictureBoxSizeMode.StretchImage;
            processedImageBox.TabIndex = 1;
            processedImageBox.TabStop = false;
            // 
            // originalImageBox
            // 
            originalImageBox.Dock = DockStyle.Fill;
            originalImageBox.Location = new Point(4, 4);
            originalImageBox.Name = "originalImageBox";
            originalImageBox.Size = new Size(331, 269);
            originalImageBox.SizeMode = PictureBoxSizeMode.StretchImage;
            originalImageBox.TabIndex = 0;
            originalImageBox.TabStop = false;
            // 
            // resultsTextBox
            // 
            tableLayout.SetColumnSpan(resultsTextBox, 2);
            resultsTextBox.Dock = DockStyle.Fill;
            resultsTextBox.Location = new Point(4, 280);
            resultsTextBox.Name = "resultsTextBox";
            resultsTextBox.ReadOnly = true;
            resultsTextBox.Size = new Size(670, 269);
            resultsTextBox.TabIndex = 2;
            resultsTextBox.Text = "";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 553);
            Controls.Add(splitContainer1);
            MinimumSize = new Size(1000, 600);
            Name = "MainForm";
            Text = "Image Processing";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            buttonContainer.ResumeLayout(false);
            buttonContainer.PerformLayout();
            tableLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)processedImageBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)originalImageBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private TableLayoutPanel tableLayout;
        private PictureBox processedImageBox;
        private PictureBox originalImageBox;
        private RichTextBox resultsTextBox;
        private TableLayoutPanel buttonContainer;
        private Button toJPEGButton;
        private Button originalImageButton;
        private Button toBMPButton;
        private Button toTIFFButton;
    }
}