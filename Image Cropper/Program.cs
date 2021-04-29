using System;
using System.Drawing;
using System.IO;

namespace Image_Cropper
{
    class Program
    {
        public static int BATCH_SIZE = 300;
        public static int IMAGE_SIZE = 256;
        static void Main(string[] args)
        {

            Console.Clear();
            string[] inImages = Directory.GetFiles("In/", "*.*", SearchOption.AllDirectories);

            try
            {
                IMAGE_SIZE = Int32.Parse(args[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to parse command line argument to integer");
            }
            

            //foreach (string imageLoc in inImages)
            //{
            //    Console.SetCursorPosition(0, 0);
            //    Console.WriteLine(imageLoc);
            //}


            int setSize =
                inImages.Length;
            int currentIndex = 0;

            int index = 0; 

            foreach (string imageLoc in inImages)
            {
                try
                {
                    Console.SetCursorPosition(0, 0);
                    Console.Write("Image: (" + currentIndex + "/" + setSize + ")");

                    Bitmap tempImage = (Bitmap)Image.FromFile(imageLoc);
                    Bitmap croppedImage = ImageManip.cropImage(tempImage, IMAGE_SIZE, IMAGE_SIZE);
                    ImageManip.saveImage(croppedImage, "Out/" + index + ".png");
                    index++;

                    if (currentIndex % BATCH_SIZE == 0)
                    {
                        System.GC.Collect();

                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.GetType());
                    System.GC.Collect();
                }

                currentIndex++;
            }


        }
    }
}
