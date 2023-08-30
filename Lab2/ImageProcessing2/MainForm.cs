using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms.DataVisualization.Charting;

namespace ImageProcessing2
{
    public partial class MainForm : Form
    {
        private readonly Chart chart;
        private readonly Chart chart2;
        private readonly PictureBox[] _pictureBoxes;

        public MainForm()
        {
            InitializeComponent();
            chart = new();
            chart2 = new();
            chart.Dock = DockStyle.Fill;
            chart2.Dock = DockStyle.Fill;
            tableLayout.Controls.Add(chart, 1, 1);
            tableLayout.Controls.Add(chart2, 3, 1);
            chart.ChartAreas.Add(new ChartArea());
            chart2.ChartAreas.Add(new ChartArea());
            chart.ChartAreas[0].AxisX.Minimum = byte.MinValue;
            chart.ChartAreas[0].AxisX.Maximum = byte.MaxValue;
            chart2.ChartAreas[0].AxisX.Minimum = byte.MinValue;
            chart2.ChartAreas[0].AxisX.Maximum = byte.MaxValue;

            _pictureBoxes = new PictureBox[]
            {
                originalPictureBox,
                equalizedPictureBox,
                grayscalePictureBox,
                robertsPictureBox,
                prewittPictureBox,
                sobelPictureBox
            };
        }

        private static readonly double[,] xRobertsCross = new double[,]
        {
                { 0, -1 },
                { 1,  0 }
        };

        private static readonly double[,] yRobertsCross = new double[,]
        {
                { -1, 0 },
                {  0, 1 }
        };

        private static readonly double[,] xPrewitt = new double[,]
        {
                { 1, 0, -1 },
                { 1, 0, -1 },
                { 1, 0, -1 }
        };

        private static readonly double[,] yPrewitt = new double[,]
        {
                { -1, -1, -1 },
                {  0,  0,  0 },
                {  1,  1,  1 }
        };

        private static readonly double[,] xSobel = new double[,]
        {
                { 1, 0, -1 },
                { 2, 0, -2 },
                { 1, 0, -1 }
        };

        private static readonly double[,] ySobel = new double[,]
        {
                { -1, -2, -1 },
                {  0,  0,  0 },
                {  1,  2,  1 }
        };

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                RestoreDirectory = true,
                Filter = "All Images Files (*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif)|*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (var pictureBox in _pictureBoxes)
                {
                    pictureBox.Image?.Dispose();
                    pictureBox.Image = null;
                }

                FileStream fs = File.OpenRead(openFileDialog.FileName);
                MemoryStream mms = new();
                fs.CopyTo(mms);
                fs.Close();

                Bitmap original = (Bitmap)Bitmap.FromStream(mms);
                originalPictureBox.Image = original;
                CreateHistogram(chart, original);

                Bitmap equalized = EqualizeIamge(original);
                equalizedPictureBox.Image = equalized;
                CreateHistogram(chart2, equalized);

                Bitmap grayscaleImage = GetGrayScale(original);
                grayscalePictureBox.Image = grayscaleImage;

                Bitmap robertsCrossImage = EdgeDetection(grayscaleImage, xRobertsCross, yRobertsCross, 1, 1, 0, 0);
                robertsPictureBox.Image = robertsCrossImage;

                Bitmap prewittImage = EdgeDetection(grayscaleImage, xPrewitt, yPrewitt, 1, 1, 1, 1);
                prewittPictureBox.Image = prewittImage;

                Bitmap sobelImage = EdgeDetection(grayscaleImage, xSobel, ySobel, 1, 1, 1, 1);
                sobelPictureBox.Image = sobelImage;
            }
        }

        public static Bitmap EdgeDetection(Bitmap sourceImage, double[,] xOp, double[,] yOp, int skipXStart, int skipYStart, int skipXEnd, int skipYEnd)
        {
            int width = sourceImage.Width;
            int height = sourceImage.Height;

            BitmapData srcData = sourceImage.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format24bppRgb);

            int bytes = srcData.Stride * srcData.Height;

            byte[] pixelBuffer = new byte[bytes];
            byte[] resultBuffer = new byte[bytes];

            IntPtr srcScan0 = srcData.Scan0;

            Marshal.Copy(srcScan0, pixelBuffer, 0, bytes);

            sourceImage.UnlockBits(srcData);

            double xg;
            double yg;
            double gt;

            int calcOffset;
            int byteOffset;

            for (int OffsetY = skipYStart; OffsetY < height - skipYEnd; OffsetY++)
            {
                for (int OffsetX = skipXStart; OffsetX < width - skipXEnd; OffsetX++)
                {
                    xg = yg = 0;

                    byteOffset = OffsetY * srcData.Stride + OffsetX * 3;

                    for (int filterY = -skipYStart; filterY <= skipYEnd; filterY++)
                    {
                        for (int filterX = -skipXStart; filterX <= skipXEnd; filterX++)
                        {
                            calcOffset = byteOffset + filterX * 3 + filterY * srcData.Stride;
                            xg += pixelBuffer[calcOffset + 1] * xOp[filterY + skipYStart, filterX + skipXStart];
                            yg += pixelBuffer[calcOffset + 1] * yOp[filterY + skipYStart, filterX + skipXStart];
                        }
                    }

                    gt = Math.Sqrt((xg * xg) + (yg * yg));

                    if (gt > 255)
                    {
                        gt = 255;
                    }
                    else if (gt < 0)
                    {
                        gt = 0;
                    }

                    resultBuffer[byteOffset] = (byte)(gt);
                    resultBuffer[byteOffset + 1] = (byte)(gt);
                    resultBuffer[byteOffset + 2] = (byte)(gt);
                }
            }

            Bitmap resultImage = new(width, height);
            BitmapData resultData = resultImage.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly,
                PixelFormat.Format24bppRgb);
            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            resultImage.UnlockBits(resultData);
            return resultImage;
        }

        private static Bitmap GetGrayScale(Bitmap imageGiven)
        {
            int width = imageGiven.Width;
            int height = imageGiven.Height;

            Bitmap res = new(width, height);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color pixelColor = imageGiven.GetPixel(i, j);
                    int grayScale = (int)((pixelColor.R * 0.3) + (pixelColor.G * 0.59) + (pixelColor.B * 0.11));
                    Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);

                    res.SetPixel(i, j, newColor);
                }
            }

            return res;
        }

        private static Bitmap EqualizeIamge(Bitmap imageGiven)
        {
            int width = imageGiven.Width;
            int height = imageGiven.Height;

            BitmapData bmpData = imageGiven.LockBits(new Rectangle(0, 0, imageGiven.Width, imageGiven.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            int bytes = bmpData.Stride * bmpData.Height;
            byte[] rgbValues = new byte[bytes];
            byte[] newBmp = new byte[bytes];

            double[] probabilityR = new double[256];
            double[] probabilityG = new double[256];
            double[] probabilityB = new double[256];

            byte[,] r = new byte[width, height];
            byte[,] g = new byte[width, height];
            byte[,] b = new byte[width, height];

            IntPtr ptr = bmpData.Scan0;

            Marshal.Copy(ptr, rgbValues, 0, imageGiven.Height * bmpData.Stride);

            imageGiven.UnlockBits(bmpData);

            int k = 0;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    b[j, i] = rgbValues[k];
                    g[j, i] = rgbValues[k + 1];
                    r[j, i] = rgbValues[k + 2];
                    k += 3;
                }
            }

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    probabilityR[r[j, i]]++;
                    probabilityG[g[j, i]]++;
                    probabilityB[b[j, i]]++;
                }
            }

            for (int i = 0; i < 256; i++)
            {
                probabilityR[i] /= (width * height);
                probabilityG[i] /= (width * height);
                probabilityB[i] /= (width * height);
            }

            int p = 0;
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    double sumR = 0;
                    double sumG = 0;
                    double sumB = 0;

                    for (int x = 0; x < r[i, j]; x++)
                    {
                        sumR += probabilityR[x];
                    }
                    for (int x = 0; x < g[i, j]; x++)
                    {
                        sumG += probabilityG[x];
                    }
                    for (int x = 0; x < b[i, j]; x++)
                    {
                        sumB += probabilityB[x];
                    }
                    newBmp[p++] = (byte)(Math.Floor(255 * sumB));
                    newBmp[p++] = (byte)(Math.Floor(255 * sumG));
                    newBmp[p++] = (byte)(Math.Floor(255 * sumR));
                }
            }
            Bitmap res = new(width, height);
            BitmapData rd = res.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            Marshal.Copy(newBmp, 0, rd.Scan0, bytes);
            res.UnlockBits(rd);

            return res;
        }

        private static void CreateHistogram(Chart chartGiven, Bitmap imageGiven)
        {
            Dictionary<byte, int> reds = new();
            Dictionary<byte, int> greens = new();
            Dictionary<byte, int> blues = new();

            for (int i = 0; i <= byte.MaxValue; i++)
            {
                reds.Add((byte)i, 0);
                greens.Add((byte)i, 0);
                blues.Add((byte)i, 0);
            }

            for (int i = 0; i < imageGiven.Width; i++)
            {
                for (int j = 0; j < imageGiven.Height; j++)
                {
                    var pixel = imageGiven.GetPixel(i, j);

                    reds[pixel.R]++;
                    greens[pixel.G]++;
                    blues[pixel.B]++;
                }
            }

            chartGiven.Series.Clear();

            Series redLine = new()
            {
                Color = Color.Red
            };
            Series greenLine = new()
            {
                Color = Color.Green
            };
            Series blueLine = new()
            {
                Color = Color.Blue
            };

            for (int i = 0; i <= byte.MaxValue; i++)
            {
                redLine.Points.AddXY(i, reds[(byte)i]);
                greenLine.Points.AddXY(i, greens[(byte)i]);
                blueLine.Points.AddXY(i, blues[(byte)i]);
            }

            chartGiven.Series.Add(redLine);
            chartGiven.Series.Add(greenLine);
            chartGiven.Series.Add(blueLine);
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var pictureBox in _pictureBoxes)
            {
                pictureBox.Image?.Dispose();
                pictureBox.Image = null;
            }

            this.Close();
        }
    }
}