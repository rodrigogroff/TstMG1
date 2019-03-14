using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;

namespace ImageProc
{
    class Program
    {
        static void Main(string[] args)
        {
            string BaseApp = "TstMG1";
            string currentDir = Directory.GetCurrentDirectory() + "\\" + BaseApp + "\\Content\\Images\\";

            Console.WriteLine("-----------------");
            Console.WriteLine(currentDir);
            Console.WriteLine("----------------- Players\\Blue");
            Console.WriteLine();

            // -----------------------------------------
            // minimizar numero de arquivos em um
            // -----------------------------------------

            Console.WriteLine(">> Diretorio de Lote de arquivos destino (Players\\Blue)");
            var dest = Console.ReadLine();  // "Players\\Blue"

            string  dir = currentDir + dest,
                    prefix = "cw_blue";

            Console.WriteLine(dir);

            if (!Directory.Exists (dir))
            {
                Console.WriteLine("Diretório não existe");
                return;
            }

            Console.WriteLine(">> Prefixo do arquivo");

            dest = Console.ReadLine();

            prefix = dest;

            var sample = new Bitmap(dir + "\\" + prefix + (new DirectoryInfo(dir).GetFiles().Length < 100 ? "01.png" : "0001.png"));

            string  fileTargetName = dir + "\\" + prefix + "_min.png",
                    fileTargetNameMap = dir + "\\" + prefix + "_min.txt";

            Console.WriteLine(" >> target mini => " + fileTargetName);

            if (File.Exists(fileTargetName)) File.Delete(fileTargetName);
            if (File.Exists(fileTargetNameMap)) File.Delete(fileTargetNameMap);

            int totFrames = new DirectoryInfo(dir).GetFiles().Length,
                widthPadrao = sample.Width,
                totHeight = sample.Height;

            Console.WriteLine(" widthPadrao => " + widthPadrao);
            Console.WriteLine(" totFrames => " + totFrames);
            Console.WriteLine(" totHeight => " + totHeight);

            FileInfo fi = new FileInfo(fileTargetName);
            FileStream fstr = fi.Create();
            Bitmap finalBitmap = new Bitmap(widthPadrao * totFrames, totHeight);
            fstr.Close();
            fi.Delete();

            using (var sw = new StreamWriter(fileTargetNameMap))
            {
                for (int i = 1; i <= totFrames; i++)
                {
                    var currentFrame = dir + "\\" + prefix + (totFrames < 100 ? i.ToString("00") : i.ToString("000")) + ".png";

                    Console.WriteLine(" currentFrame => " + currentFrame);

                    var sampleItem = new Bitmap(currentFrame);

                    using (Graphics grD = Graphics.FromImage(finalBitmap))
                    {
                        Rectangle destRegion = new Rectangle((i - 1) * widthPadrao, 0, widthPadrao, totHeight);
                        Rectangle srcRegion = new Rectangle(0, 0, widthPadrao, totHeight);

                        sw.Write(destRegion.X.ToString()); sw.Write(",");
                        sw.Write(destRegion.Y.ToString()); sw.Write(",");
                        sw.Write(destRegion.Width.ToString()); sw.Write(",");
                        sw.Write(destRegion.Height.ToString()); sw.Write(";");

                        grD.DrawImage(sampleItem, destRegion, srcRegion, GraphicsUnit.Pixel);
                    }
                }
            }

            finalBitmap.Save("test.png", ImageFormat.Png);
            File.Move("test.png", fileTargetName);

            Console.WriteLine(" >> Salvo com sucesso! ");
        }
    }
}
