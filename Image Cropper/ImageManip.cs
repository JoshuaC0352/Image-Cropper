using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


class ImageManip
{
    public static Bitmap cropImage(Bitmap loadedImage, int cropWidth, int cropHeight)
    {
        float magnitude;

        if (loadedImage.Width > loadedImage.Height)
        {
            magnitude = (float)loadedImage.Width / cropWidth;
        }
        else
        {
            magnitude = (float)loadedImage.Height / cropHeight;
        }

        int newWidth = (int)(loadedImage.Width / magnitude);
        int newHeight = (int)(loadedImage.Height / magnitude);

        loadedImage = resizeImage(loadedImage, newWidth, newHeight);

        int indexLoacation = 0;

        Bitmap returnImage = new Bitmap(cropWidth, cropHeight);

        for (int x = 0; x < cropWidth; x++)
        {
            for (int y = 0; y < cropHeight; y++)
            {

                if (x < loadedImage.Width && y < loadedImage.Height)
                {
                    returnImage.SetPixel(x, y, loadedImage.GetPixel(x, y));
                }
                else
                {
                    returnImage.SetPixel(x, y, Color.Gray);
                }
                indexLoacation++;
            }
        }

        return returnImage;

    }

    public static void saveImage(Bitmap myBitmap, string location)
    {
        ImageCodecInfo myImageCodecInfo;
        System.Drawing.Imaging.Encoder myEncoder;
        EncoderParameter myEncoderParameter;
        EncoderParameters myEncoderParameters;

        // Create a Bitmap object based on a BMP file.
        //myBitmap = new Bitmap("Shapes.bmp");

        // Get an ImageCodecInfo object that represents the JPEG codec.
        myImageCodecInfo = GetEncoderInfo("image/jpeg");

        // Create an Encoder object based on the GUID

        // for the Quality parameter category.
        myEncoder = System.Drawing.Imaging.Encoder.Quality;

        // Create an EncoderParameters object.

        // An EncoderParameters object has an array of EncoderParameter

        // objects. In this case, there is only one

        // EncoderParameter object in the array.
        myEncoderParameters = new EncoderParameters(1);

        // Save the bitmap as a JPEG file with quality level 75.
        myEncoderParameter = new EncoderParameter(myEncoder, 75L);
        myEncoderParameters.Param[0] = myEncoderParameter;
        myBitmap.Save(location, myImageCodecInfo, myEncoderParameters);
    }

    private static ImageCodecInfo GetEncoderInfo(String mimeType)
    {
        int j;
        ImageCodecInfo[] encoders;
        encoders = ImageCodecInfo.GetImageEncoders();
        for (j = 0; j < encoders.Length; ++j)
        {
            if (encoders[j].MimeType == mimeType)
                return encoders[j];
        }
        return null;
    }

    public static Bitmap resizeImage(Image image, int width, int height)
    {
        var destRect = new Rectangle(0, 0, width, height);
        var destImage = new Bitmap(width, height);

        destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

        using (var graphics = Graphics.FromImage(destImage))
        {
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            using (var wrapMode = new ImageAttributes())
            {
                wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
            }
        }

        return destImage;
    }
}

