using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameSystem
{
	public class ImageLoader
	{
		string currentDir = Directory.GetCurrentDirectory() + "\\Content\\Images\\";

		public void LoadImage(string file, ref Texture2D texture, ref GraphicsDeviceManager gdm)
		{
			try
			{
				using (var fileStream = new FileStream(currentDir + file, FileMode.Open))
					texture = Texture2D.FromStream(gdm.GraphicsDevice, fileStream);
			}
			catch ( System.Exception ex )
			{
				var strDebug =  "file: " + file + "\r\n" +
								"path: " + currentDir + "\r\n" +
								ex.ToString();

				throw new System.Exception(strDebug);
			}
		}

		public List<Texture2D> LoadImageSequence(string prefix, int frames, ref GraphicsDeviceManager gdm)
		{
			var lst = new List<Texture2D>();
			int maskSize = frames.ToString().Length;

			string path = currentDir + prefix;
			
			for (int t = 1; t <= frames; ++t)
				using (var fileStream = new FileStream(path + t.ToString("".PadLeft(maskSize, '0')) + ".png", FileMode.Open))
					lst.Add(Texture2D.FromStream(gdm.GraphicsDevice, fileStream));

			return lst;
		}

        public List<Texture2D> LoadImageMappedSequence(string prefix, int frames, ref GraphicsDeviceManager gdm)
        {
            var lst = new List<Texture2D>();
            var log = new List<string>();


            try
            {
                int maskSize = frames.ToString().Length;

                string path = currentDir + prefix + "_min.png";
                string pathMap = currentDir + prefix + "_min.txt";

                using (var fileStream = new FileStream(path, FileMode.Open))
                {
                    using (var maxTexture = Texture2D.FromStream(gdm.GraphicsDevice, fileStream))
                    {
                        log.Add("pathMap");
                        log.Add(pathMap);

                        using (var sr = new StreamReader(pathMap))
                        {
                            //0,0,105,110;105,0,105,110;
                            var contents = sr.ReadToEnd();

                            log.Add("contents");
                            log.Add(contents);

                            var lstMap = contents.Split(';');

                            for (int t = 1; t <= frames; ++t)
                            {
                                //0,0,105,110
                                var contentItem = lstMap[t - 1];

                                log.Add("contentItem");
                                log.Add(contentItem);

                                var rectStr = contentItem.Split(',');

                                var rect = new Rectangle(Convert.ToInt32(rectStr[0]),  // X
                                                          Convert.ToInt32(rectStr[1]),  // Y
                                                          Convert.ToInt32(rectStr[2]),  // WIDTH
                                                          Convert.ToInt32(rectStr[3])); // HEIGHT

                                var newTexture = new Texture2D(gdm.GraphicsDevice, rect.Width, rect.Height);

                                int count = rect.Width * rect.Height;
                                Color[] data = new Color[count];
                                maxTexture.GetData(0, rect, data, 0, count);
                                newTexture.SetData(data);

                                lst.Add(newTexture);
                            }
                        }
                    }
                }

                //for (int t = 1; t <= frames; ++t)
                //  using (var fileStream = new FileStream(path + t.ToString("".PadLeft(maskSize, '0')) + ".png", FileMode.Open))
                //    lst.Add(Texture2D.FromStream(gdm.GraphicsDevice, fileStream));

            }
            catch (SystemException ex)
            {
                string finalLog = "";

                foreach (var item in log)
                    finalLog += item + "\r\n";
                                
                throw new SystemException(ex.ToString() + " - " + finalLog.ToString());
            }

            return lst;
        }
    }
}
