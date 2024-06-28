using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

class ParallelSobelEdgeDetection
{
    static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: ParallelSobelEdgeDetection <input_file> <output_file>");
            return;
        }

        string inputFile = args[0];
        string outputFile = args[1];

        try
        {
            using (Bitmap image = new Bitmap(inputFile))
            {
                using (Bitmap result = ApplySobelOperator(image))
                {
                    result.Save(outputFile, ImageFormat.Jpeg);
                }
            }
            Console.WriteLine("Edge detection completed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static Bitmap ApplySobelOperator(Bitmap image)
    {
        int width = image.Width;
        int height = image.Height;
        Bitmap result = new Bitmap(width, height);

        int[,] gx = new int[,] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
        int[,] gy = new int[,] { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };

        BitmapData srcData = image.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
        BitmapData resultData = result.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

        unsafe
        {
            byte* src = (byte*)srcData.Scan0;
            byte* dst = (byte*)resultData.Scan0;

            Parallel.For(1, height - 1, y =>
            {
                byte* srcRow = src + y * srcData.Stride;
                byte* dstRow = dst + y * resultData.Stride;

                for (int x = 1; x < width - 1; x++)
                {
                    int pixelX = 0, pixelY = 0;

                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            byte* pixel = srcRow + (x + i) * 4 + j * srcData.Stride;
                            int intensity = (pixel[2] + pixel[1] + pixel[0]) / 3;
                            pixelX += gx[i + 1, j + 1] * intensity;
                            pixelY += gy[i + 1, j + 1] * intensity;
                        }
                    }

                    int gradient = (int)Math.Sqrt((pixelX * pixelX) + (pixelY * pixelY));
                    gradient = Math.Min(255, Math.Max(0, gradient));

                    byte* resultPixel = dstRow + x * 4;
                    resultPixel[0] = resultPixel[1] = resultPixel[2] = (byte)gradient;
                    resultPixel[3] = 255; // Alpha channel
                }
            });
        }

        image.UnlockBits(srcData);
        result.UnlockBits(resultData);

        return result;
    }
}