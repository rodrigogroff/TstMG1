using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProc
{
    class Program
    {
        static void Main(string[] args)
        {            
            Console.WriteLine("----------------- Image Processing -------------");
            Console.WriteLine(">> Cmd ('build' for all)");

            // ---------------------------------------------------------------------------
            // minimizar numero de arquivos em um ( +- 50% tamanho, +- 80% tempo load)
            // ---------------------------------------------------------------------------

            var dest = Console.ReadLine(); 

            if (dest == "build")
            {
                ProcX("Players\\Blue", "cw_blue");
                ProcX("Players\\Red", "cw_red");

                Console.WriteLine();
                Console.WriteLine(">> ------ Build Complete! ------------");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(">> Prefixo do arquivo");

                var prefix = Console.ReadLine();

                Proc(dest, prefix);
            }
        }

        static void ProcX(string destino, string prefix)
        {
            string BaseApp = "CrossPlat";
            string currentDir = Directory.GetCurrentDirectory() + "\\" + BaseApp + "\\Content\\Images\\";
            currentDir = currentDir.Replace("ImageProc\\bin\\Release\\", "");
            string dir = currentDir + destino;

            Console.WriteLine(dir);

            if (!Directory.Exists(dir))
            {
                Console.WriteLine("Diretório não existe");
                return;
            }

            var sample = new Bitmap(dir + "\\" + prefix + (new DirectoryInfo(dir).GetFiles().Length < 100 ? "01.png" : "0001.png"));

            string fileTargetName = dir + "\\" + prefix + "_min.png",
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
            
            int totXSizeMax = 0;
            int minY = 99999, maxY = 0;

            using (var sw = new StreamWriter(fileTargetNameMap))
            {
                for (int i = 1; i <= totFrames; i++)
                {
                    var currentFrame = dir + "\\" + prefix + (totFrames < 100 ? i.ToString("00") : i.ToString("000")) + ".png";
                    var sampleItem = new Bitmap(currentFrame);
                   
                    int minX = 99999, maxX = 0;                   

                    for (int y = 0; y < totHeight; y++)
                        for (int x = 0; x < widthPadrao; ++x)
                        {
                            Color utd = sampleItem.GetPixel(x, y);

                            if (utd.A > 0)
                            {
                                if (x < minX) minX = x;
                                if (x > maxX) maxX = x;

                                if (y < minY) minY = y;
                                if (y > maxY) maxY = y;
                            }
                        }

                    totXSizeMax += maxX - minX;
                }
            }

            FileInfo fi = new FileInfo(fileTargetName);
            FileStream fstr = fi.Create();
            Bitmap finalBitmap = new Bitmap(totXSizeMax, maxY - minY);
            fstr.Close();
            fi.Delete();

            using (var sw = new StreamWriter(fileTargetNameMap))
            {
                int xCustomAcc = 0;

                for (int i = 1; i <= totFrames; i++)
                {
                    var currentFrame = dir + "\\" + prefix + (totFrames < 100 ? i.ToString("00") : i.ToString("000")) + ".png";

                    Console.WriteLine(" currentFrame => " + currentFrame);

                    var sampleItem = new Bitmap(currentFrame);

                    using (Graphics grD = Graphics.FromImage(finalBitmap))
                    {
                        int minX = 99999, maxX = 0;

                        for (int y = 0; y < totHeight; y++)
                            for (int x = 0; x < widthPadrao; ++x)
                            {
                                Color utd = sampleItem.GetPixel(x, y);

                                if (utd.A > 0)
                                {
                                    if (x < minX) minX = x;
                                    if (x > maxX) maxX = x;
                                }
                            }

                        int curSafeWidth = maxX - minX;

                        Rectangle destRegion = new Rectangle(xCustomAcc, 0, curSafeWidth, maxY - minY);
                        Rectangle srcRegion = new Rectangle(minX, minY, curSafeWidth, maxY - minY);

                        sw.Write(destRegion.X.ToString()); sw.Write(",");
                        sw.Write(destRegion.Y.ToString()); sw.Write(",");
                        sw.Write(destRegion.Width.ToString()); sw.Write(",");
                        sw.Write(destRegion.Height.ToString()); sw.Write(",");
                        sw.Write(minX); sw.Write(",");
                        sw.Write(minY); sw.Write(";");

                        grD.DrawImage(sampleItem, destRegion, srcRegion, GraphicsUnit.Pixel);

                        xCustomAcc += curSafeWidth;
                    }
                }
            }

            finalBitmap.Save("test.png", ImageFormat.Png);
            File.Move("test.png", fileTargetName);

            Console.WriteLine(" >> Salvo com sucesso! ");
            Console.WriteLine();
        }

        static void Proc (string destino, string prefix )
        {
            string BaseApp = "CrossPlat";
            string currentDir = Directory.GetCurrentDirectory() + "\\" + BaseApp + "\\Content\\Images\\";
            currentDir = currentDir.Replace("ImageProc\\bin\\Release\\", "");
            string dir = currentDir + destino;                    

            Console.WriteLine(dir);

            if (!Directory.Exists (dir))
            {
                Console.WriteLine("Diretório não existe");
                return;
            }

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
                        int minX = 99999, maxX = 0;

                        for (int y = 0; y < totHeight; y++)
                            for (int x = 0; x < widthPadrao; ++x)
                            {
                                Color utd = sampleItem.GetPixel(x, y);

                                if (utd.A > 0)
                                {
                                    if (x < minX) minX = x;
                                    if (x > maxX) maxX = x;
                                }
                            }

                        Rectangle destRegion = new Rectangle((i - 1) * widthPadrao, 0, widthPadrao, totHeight);
                        Rectangle srcRegion = new Rectangle(0, 0, widthPadrao, totHeight);

                        sw.Write(destRegion.X.ToString()); sw.Write(",");
                        sw.Write(destRegion.Y.ToString()); sw.Write(",");
                        sw.Write(destRegion.Width.ToString()); sw.Write(",");
                        sw.Write(destRegion.Height.ToString()); sw.Write(";");
                        sw.Write("0"); sw.Write(",");
                        sw.Write("0"); sw.Write(";");

                        grD.DrawImage(sampleItem, destRegion, srcRegion, GraphicsUnit.Pixel);
                    }
                }
            }

            finalBitmap.Save("test.png", ImageFormat.Png);
            File.Move("test.png", fileTargetName);

            Console.WriteLine(" >> Salvo com sucesso! ");
            Console.WriteLine();
        }
    }
}
