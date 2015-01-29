﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.IO;

namespace CheckMapp.Utils
{
    public static class Utility
    {
        #region Numbers Functions

        public static int StringToNumber(string str)
        {
            try
            {
                return Convert.ToInt32(str);
            }
            catch (FormatException e)
            {
                Console.WriteLine("Input string is not a sequence of digits. Err: " + e.Message);
            }
            catch (OverflowException e)
            {
                Console.WriteLine("The number cannot fit in an Int32. Err: " + e.Message);
            }

            return -1;
        }

        #endregion

        #region Images Functions

        public static MemoryStream ImageToByteArray(string imagePath)
        {
            BitmapImage image = new BitmapImage();
            image.CreateOptions = BitmapCreateOptions.None;
            image.UriSource = new Uri(String.Format(imagePath), UriKind.Relative);
            WriteableBitmap wbmp = new WriteableBitmap(image);
            MemoryStream ms = new MemoryStream();
            wbmp.SaveJpeg(ms, wbmp.PixelWidth, wbmp.PixelHeight, 0, 100);

            return ms;
        }

        public static BitmapImage ByteArrayToImage(byte[] imageByteArray)
        {
            BitmapImage img = new BitmapImage();
            using (MemoryStream memStream = new MemoryStream(imageByteArray))
            {
                img.SetSource(memStream);
            }

            return img;
        }

        #endregion

        #region Stream Functions

        /// <summary>
        /// Transforme le stream d'une photo en tableau de byte
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] ReadFully(Stream input)
        {
            if (input == null)
                return null;

            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        #endregion
    }
}
