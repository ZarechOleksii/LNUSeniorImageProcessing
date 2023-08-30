using System.Drawing.Imaging;

namespace ImageProcessing
{
    public partial class MainForm : Form
    {
        private const int PaletteSize = 1024;
        private const int BITMAPINFOHEADERSize = 40;
        private const int IdentifyHeaderSize = 14;
        private const int TotalHeaderSize = BITMAPINFOHEADERSize + IdentifyHeaderSize;
        private const int SizeNonPixel = PaletteSize + TotalHeaderSize;

        private Stream? _OriginalStream;
        private string? _WorkingDirectory;
        private string? _OriginalFileName;
        private Bitmap? _OriginalImage;
        private long _OriginalSize = -1;
        private double _TimeToRead = -1;
        private delegate void SafeCallDelegate(string text);

        public MainForm()
        {
            InitializeComponent();
        }

        private void Log(string message)
        {
            if (resultsTextBox.InvokeRequired)
            {
                var del = new SafeCallDelegate(Log);
                resultsTextBox.Invoke(del, new object[] { message });
            }
            else
            {
                resultsTextBox.AppendText($"{DateTime.Now} - {message}.\n");
                resultsTextBox.ScrollToCaret();
            }
        }

        private static double GetTimeSpent(Action action)
        {
            DateTime start = DateTime.Now;
            action();
            DateTime end = DateTime.Now;

            return (end - start).TotalMilliseconds;
        }

        private static ImageCodecInfo FindEncoder(ImageFormat imageFormat)
        {
            return ImageCodecInfo.GetImageEncoders().First(q => q.FormatID.Equals(imageFormat.Guid));
        }

        private void OriginalImageButtonClick(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new()
            {
                RestoreDirectory = true,
                Filter = "BMP files (*.bmp)|*.bmp"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                toBMPButton.Enabled = true;
                toTIFFButton.Enabled = true;
                toJPEGButton.Enabled = true;
                _WorkingDirectory = Path.GetDirectoryName(dialog.FileName);
                _OriginalFileName = Path.GetFileNameWithoutExtension(dialog.FileName);

                _TimeToRead = GetTimeSpent(() =>
                {
                    _OriginalStream?.Close();
                    _OriginalStream = dialog.OpenFile();
                    _OriginalImage = new Bitmap(_OriginalStream);
                    _OriginalSize = _OriginalStream.Length;
                });

                originalImageBox.Image?.Dispose();
                originalImageBox.Image = _OriginalImage;
                Log($"Loaded a new bitmap image {Path.GetFileName(dialog.FileName)}");
                Log($"Time taken to load a BMP image: {_TimeToRead} milliseconds");
                Log($"BMP image size - {_OriginalSize} bytes");
            }
        }

        private void ToBMPButtonClick(object sender, EventArgs e)
        {
            if(_WorkingDirectory is not null && _OriginalFileName is not null && _OriginalImage is not null)
            {
                string newFilePath = Path.Combine(_WorkingDirectory, _OriginalFileName) + "ToBMP.bmp";
                byte[] resultingRLEWithHeader = Array.Empty<byte>();
                bool is8bit = false;

                double timeSpentEncode = GetTimeSpent(() =>
                {
                    MemoryStream mms = new();
                    _OriginalImage.Save(mms, ImageFormat.Bmp);
                    byte[] originalBytes = mms.ToArray();
                    byte[] header;
                    byte[] pixels;
                    mms.Close();

                    List<byte> result = new();
                    int rowLen;
                    byte compression;


                    if (_OriginalImage.PixelFormat == PixelFormat.Format8bppIndexed)
                    {
                        header = originalBytes.Take(SizeNonPixel).ToArray();
                        pixels = originalBytes.Skip(SizeNonPixel).ToArray();
                        is8bit = true;
                        rowLen = _OriginalImage.Width;
                        compression = 1;

                        for (int i = 0; i < _OriginalImage.Height; i++)
                        {
                            byte[] byteArr = pixels.Skip(rowLen * i).Take(rowLen).ToArray();
                            byte prevR = byteArr[0];
                            byte counter = 1;

                            for (int j = 1; j < rowLen; j++)
                            {
                                if (counter == 255 || byteArr[j] != prevR) 
                                {
                                    result.Add(counter);
                                    result.Add(prevR);
                                    prevR = byteArr[j];
                                    counter = 1;
                                }
                                else
                                {
                                    counter++;
                                }
                            }

                            result.Add(counter);
                            result.Add(prevR);

                            if (i == _OriginalImage.Height - 1)
                            {
                                result.Add(0x00);
                                result.Add(0x01);
                            }
                            else
                            {
                                result.Add(0x00);
                                result.Add(0x00);
                            }
                        }
                    }
                    else
                    {
                        header = originalBytes.Take(TotalHeaderSize).ToArray();
                        pixels = originalBytes.Skip(TotalHeaderSize).ToArray();
                        rowLen = 24 * _OriginalImage.Width * 4 / 32;
                        compression = 4;

                        for (int i = 0; i < _OriginalImage.Height; i++)
                        {
                            byte[] byteArr = pixels.Skip(rowLen * i).Take(rowLen).ToArray();
                            byte prevR = byteArr[0];
                            byte prevG = byteArr[1];
                            byte prevB = byteArr[2];
                            byte counter = 1;

                            for (int j = 3; j < rowLen; j += 3)
                            {
                                if (counter == 255 || byteArr[j] != prevR || byteArr[j + 1] != prevG || byteArr[j + 2] != prevB)
                                {
                                    result.Add(counter);
                                    result.Add(prevR);
                                    result.Add(prevG);
                                    result.Add(prevB);
                                    prevR = byteArr[j];
                                    prevG = byteArr[j + 1];
                                    prevB = byteArr[j + 2];
                                    counter = 1;
                                }
                                else
                                {
                                    counter++;
                                }
                            }

                            result.Add(counter);
                            result.Add(prevR);
                            result.Add(prevG);
                            result.Add(prevB);

                            if (i == _OriginalImage.Height - 1)
                            {
                                result.Add(0x00);
                                result.Add(0x01);
                            }
                            else
                            {
                                result.Add(0x00);
                                result.Add(0x00);
                            }
                        }
                    }

                    //pixel info
                    byte[] resultingRLE = result.ToArray();
                    //new size = total size
                    byte[] newSize = BitConverter.GetBytes(result.Count + SizeNonPixel);
                    // compression and size of header + pixel size
                    byte[] newHeaderPart = new byte[] { compression, 0, 0, 0 }.Concat(BitConverter.GetBytes(result.Count + BITMAPINFOHEADERSize)).ToArray();

                    /// Take 2 bytes of file info 
                    /// + new total file size 
                    /// + next 24 bytes of file info and original header 
                    /// + compression bytes and (header and pixel size) 
                    /// + rest of header 
                    /// + pixels 

                    resultingRLEWithHeader = 
                        header.Take(2)
                        .Concat(newSize)
                        .Concat(header.Skip(6).Take(24))
                        .Concat(newHeaderPart)
                        .Concat(header.Skip(38))
                        .Concat(resultingRLE)
                        .ToArray();

                });

                double timeSpentWrite = GetTimeSpent(() =>
                {
                    MemoryStream mmsToWrite = new(resultingRLEWithHeader);
                    var fileStream = File.OpenWrite(newFilePath);
                    mmsToWrite.WriteTo(fileStream);
                    fileStream.Close();
                    mmsToWrite.Close();
                });

                double timeSpentRead = GetTimeSpent(() =>
                {
                    var stream = File.OpenRead(newFilePath);

                    if (is8bit)
                    {
                        processedImageBox.Image?.Dispose();
                        processedImageBox.Image = new Bitmap(stream);
                    }
                    else
                    {
                        MemoryStream mms = new();
                        stream.CopyTo(mms);
                        var result = mms.ToArray();
                        mms.Close();

                        result[30] = 0;
                        byte[] readPixels = result.Skip(TotalHeaderSize).ToArray();
                        List<byte> decoded = new();

                        for(int i = 0; i < readPixels.Length - 2; i+=4)
                        {
                            if (readPixels[i] == 0x00 && readPixels[i + 1] == 0x00)
                            {
                                i -= 2;
                            }
                            else
                            {
                                decoded.AddRange(Enumerable.Repeat(new byte[] { readPixels[i+1], readPixels[i+2], readPixels[i+3] }, readPixels[i]).SelectMany(q => q));
                            }
                        }
                        byte[] decodedResult = result.Take(TotalHeaderSize).Concat(decoded).ToArray();

                        mms = new(decodedResult);
                        processedImageBox.Image?.Dispose();
                        processedImageBox.Image = new Bitmap(mms);
                    }

                    stream.Close();
                });

                Log($"Time taken to encode BMP with RLE: {timeSpentEncode} milliseconds");
                Log($"Time taken to write BMP with RLE: {timeSpentWrite} milliseconds");
                Log($"Time taken to read BMP with RLE: {timeSpentRead} milliseconds");
                Log($" BMP with RLE image size - {resultingRLEWithHeader.LongLength} bytes");
                Log($"Size change = {-(1 - (double)resultingRLEWithHeader.LongLength / _OriginalSize) * 100}%");
                Log($"Time to read change = {-(1 - (double)timeSpentRead / _TimeToRead) * 100}%");

                MemoryStream original = new();
                _OriginalImage.Save(original, ImageFormat.Bmp);
                MemoryStream compressed = new();
                processedImageBox.Image.Save(compressed, ImageFormat.Bmp);
                Task.Run(() => CompareColors(compressed, original, is8bit ? "BMP with 8-bit RLE compression" : "BMP with 24-bit RLE compression"));
            }
        }

        private void ToTIFFButtonClick(object sender, EventArgs e)
        {
            if (_WorkingDirectory is not null && _OriginalFileName is not null && _OriginalImage is not null)
            {
                string newFilePath = Path.Combine(_WorkingDirectory, _OriginalFileName) + "ToTIFF.tiff";

                MemoryStream mms = new();
                double timeSpentEncode = GetTimeSpent(() =>
                {
                    EncoderParameters encoderParameters = new();
                    encoderParameters.Param[0] = new EncoderParameter(Encoder.Compression, (long)EncoderValue.CompressionLZW);
                    ImageCodecInfo tiffEncoder = FindEncoder(ImageFormat.Tiff);
                    _OriginalImage.Save(mms, tiffEncoder, encoderParameters);
                });

                double timeSpentWrite = GetTimeSpent(() =>
                {
                    FileStream toWrite = File.OpenWrite(newFilePath);
                    mms.WriteTo(toWrite);
                    toWrite.Close();
                    mms.Close();
                });

                double timeSpentRead = GetTimeSpent(() =>
                {
                    var stream = File.OpenRead(newFilePath);
                    processedImageBox.Image?.Dispose();
                    processedImageBox.Image = Image.FromStream(stream);
                    stream.Close();
                });

                FileStream stream = File.OpenRead(newFilePath);
                long streamLength = stream.Length;

                Log($"Time taken to encode TIFF with LZW: {timeSpentEncode} milliseconds");
                Log($"Time taken to write TIFF with LZW: {timeSpentWrite} milliseconds");
                Log($"Time taken to read TIFF with LZW: {timeSpentRead} milliseconds");
                Log($"TIFF with LZW image size - {streamLength} bytes");
                Log($"Size change = { -(1 - (double)streamLength / _OriginalSize ) * 100 }%");
                Log($"Time to read change = { -(1 - (double)timeSpentRead / _TimeToRead) * 100}%");
                Log("Calculating color changes please wait...");

                MemoryStream memoryStream = new();
                _OriginalImage.Save(memoryStream, ImageFormat.Bmp);
                Task.Run(() => CompareColors(stream, memoryStream, $"TIFF with LZW compression"));
            }
        }

        private void ToJPEGButtonClick(object sender, EventArgs e)
        {
            if (_WorkingDirectory is not null && _OriginalFileName is not null && _OriginalImage is not null)
            {
                string newFilePath = Path.Combine(_WorkingDirectory, _OriginalFileName) + "ToJPEG.jpeg";
                long quality = 100;

                MemoryStream mms = new();
                double timeSpentEncode = GetTimeSpent(() =>
                {
                    Encoder encoder = Encoder.Quality;
                    EncoderParameters encoderParameters = new(1);
                    encoderParameters.Param[0] = new EncoderParameter(encoder, quality);
                    _OriginalImage.Save(mms, FindEncoder(ImageFormat.Jpeg), encoderParameters);
                });

                double timeSpentWrite = GetTimeSpent(() =>
                {
                    FileStream toWrite = File.OpenWrite(newFilePath);
                    mms.WriteTo(toWrite);
                    toWrite.Close();
                    mms.Close();
                }); 
                
                double timeSpentRead = GetTimeSpent(() =>
                {
                    FileStream stream = File.OpenRead(newFilePath);
                    processedImageBox.Image?.Dispose();
                    processedImageBox.Image = Image.FromStream(stream);
                    stream.Close();
                });

                FileStream stream = File.OpenRead(newFilePath);
                long streamLength = stream.Length;

                Log($"Time taken to encode JPEG with {quality} quality: {timeSpentEncode} milliseconds");
                Log($"Time taken to write JPEG with {quality} quality: {timeSpentWrite} milliseconds");
                Log($"Time taken to read JPEG with {quality} quality: {timeSpentRead} milliseconds");
                Log($"JPEG with {quality} quality image size - {streamLength} bytes");
                Log($"Size change = {-(1 - (double)streamLength / _OriginalSize) * 100}%");
                Log($"Time to read change = {-(1 - (double)timeSpentRead / _TimeToRead) * 100}%");
                Log("Calculating color changes please wait...");

                MemoryStream memoryStream = new();
                _OriginalImage.Save(memoryStream, ImageFormat.Bmp);
                Task.Run(() => CompareColors(stream, memoryStream, $"JPEG with {quality} quality"));
            }
        }

        private void CompareColors(Stream stream, Stream originalStream, string imageName)
        {
            Bitmap original= new(originalStream);
            Bitmap toCompare = new(stream);

            List<byte> differences = new();

            int width = original.Width;
            int height = original.Height;   

            var pixels = Enumerable
                .Range(0, height)
                .SelectMany(y => Enumerable.Range(0, width).Select(x => new { x, y }))
                .Select(coordinate => new { Left = toCompare.GetPixel(coordinate.x, coordinate.y), Right = original.GetPixel(coordinate.x, coordinate.y) })
                .ToArray();

            stream.Close();
            originalStream.Close();

            Task<double[]> red = Task.Run(() =>
            {
                var midResult = pixels.Select(pixel => Math.Abs(pixel.Left.R - pixel.Right.R));
                long redDiffSum = midResult.Sum();
                double redDiffAvg = midResult.Average();
                Log($"Total difference in color Red between original and compressed {imageName}: {redDiffSum}");
                Log($"Average difference in color Red between original and compressed {imageName} per pixel: {Math.Round(redDiffAvg, 2)}");
                return new[] { redDiffSum, redDiffAvg };
            });

            Task<double[]> green = Task.Run(() =>
            {
                var midResult = pixels.Select(pixel => Math.Abs(pixel.Left.G - pixel.Right.G));
                long greenDiffSum = midResult.Sum();
                double greenDiffAvg = midResult.Average();
                Log($"Total difference in color Green between original and compressed {imageName}: {greenDiffSum}");
                Log($"Average difference in color Green between original and compressed {imageName} per pixel: {Math.Round(greenDiffAvg, 2)}");
                return new[] { greenDiffSum, greenDiffAvg };
            });

            Task<double[]> blue = Task.Run(() =>
            {
                var midResult = pixels.Select(pixel => Math.Abs(pixel.Left.B - pixel.Right.B));
                long blueDiffSum = midResult.Sum();
                double blueDiffAvg = midResult.Average();
                Log($"Total difference in color Blue between original and compressed {imageName}: {blueDiffSum}");
                Log($"Average difference in color Blue between original and compressed {imageName} per pixel: {Math.Round(blueDiffAvg, 2)}");
                return new[] { blueDiffSum, blueDiffAvg };
            });

            Task.WhenAll(new[] { red, green, blue }).Wait();

            long totalDiffSum = (long)Math.Round(red.Result[0] + green.Result[0] + blue.Result[0]);
            double totalDiffAvg = red.Result[1] + green.Result[1] + blue.Result[1];

            Log($"Total difference in all colors between original and compressed {imageName}: {totalDiffSum}");
            Log($"Average difference in all colors between original and compressed {imageName} per pixel: {Math.Round(totalDiffAvg, 2)}");
        }
    }
}