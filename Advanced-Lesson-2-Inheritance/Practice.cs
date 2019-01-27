using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Lesson_2_Inheritance
{
    public static partial class Practice
    {
        /// <summary>
        /// A.L2.P1/1. Создать консольное приложение, которое может выводить 
        /// на печатать введенный текст  одним из трех способов: 
        /// консоль, файл, картинка. 
        /// </summary>
        public static void A_L2_P1_1()
        {
            
            Console.WriteLine("Введите способ печати (1 - консоль, 2 - файл, 3 - картинка) : ");
            string choose = Console.ReadLine();

            IPrinter printer = null;

            switch (choose)
            {
                case "1":
                    printer = new ConsolePrinter(ConsoleColor.Red);
                    break;
                case "2":
                    Console.WriteLine("Фраза записана в файл.");
                    printer = new FilePrint(filename: "test");
                    break;
                case "3":
                    printer = new ImagePrint("BitmapExample");
                    Console.WriteLine("Фраза записана в картинку.");
                    break;
            }

            Console.WriteLine("Введите фразу которую необходимо записать: ");

            for (int i = 0; i < 5; i++)
            {
                var text = Console.ReadLine();
                printer?.Print(text);
            }

            
        }
        public interface IPrinter
        {
            void Print(string str);
        }
        
        public class ConsolePrinter : IPrinter
        {
            public void Print(string str)
            {
                Console.ForegroundColor = _color;
                Console.WriteLine(str);
                Console.ResetColor();
            }

            public ConsolePrinter(ConsoleColor color)
            {
                _color = color;
            }

            private ConsoleColor _color;
        }

        public class FilePrint : IPrinter
        {
            public void Print(string str)
            {
                System.IO.File.AppendAllText($@"D:/{_filename}.txt", str);
            }

            public FilePrint(string filename)
            {
                _filename = filename;
            }

            readonly string _filename;
        }

        public class ImagePrint : IPrinter
        {
            public string Filename { get;}
            public void Print(string str)
            {
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(1000,1000);
                Font drawFont = new Font("Arial", 16);
                SolidBrush drawBrush = new SolidBrush(Color.Black);


                float x = 150.0F;
                float y = 50.0F;

                // Set format of string.
                StringFormat drawFormat = new StringFormat();
                drawFormat.FormatFlags = StringFormatFlags.DirectionVertical;  

                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.DrawString(str, drawFont, drawBrush, x, y, drawFormat);
                bitmap.Save($@"D:/{Filename }.png");
            }

            public ImagePrint(string filename)
            {
                Filename = filename;
            }
        }
    }
}
