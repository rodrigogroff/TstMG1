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
			catch ( Exception ex )
			{
				var strDebug =  "file: " + file + "\r\n" +
								"path: " + currentDir + "\r\n" +
								ex.ToString();

				throw new Exception(strDebug);
			}
		}

		public List<Texture2D> LoadImageSequence(string prefix, int frames, ref GraphicsDeviceManager gdm, ref List<int> framesDx, ref List<int> framesDy)
		{
			var lst = new List<Texture2D>();
			int maskSize = frames.ToString().Length;

			string path = currentDir + prefix;
			
			for (int t = 1; t <= frames; ++t)
                using (var fileStream = new FileStream(path + t.ToString("".PadLeft(maskSize, '0')) + ".png", FileMode.Open))
                {
                    // distance from left x
                    framesDx.Add(0);
                    framesDy.Add(0);
                    lst.Add(Texture2D.FromStream(gdm.GraphicsDevice, fileStream));
                }                    

			return lst;
		}

        public List<Texture2D> LoadImageMappedSequence(string prefix, int frames, ref GraphicsDeviceManager gdm, ref List<int> framesDx, ref List<int> framesDy)
        {
            var lst = new List<Texture2D>();
            var log = new List<string>();
            
            try
            {
                int maskSize = frames.ToString().Length;

                string pathPNG = currentDir + prefix + "_min.png";
                string pathMap = currentDir + prefix + "_min.txt";

                using (var fileStream = new FileStream(pathPNG, FileMode.Open))
                {
                    using (var maxTexture = Texture2D.FromStream(gdm.GraphicsDevice, fileStream))
                    {
                        using (var sr = new StreamReader(pathMap))
                        {
                            var contents = sr.ReadToEnd(); 
                            var lstMap = contents.Split(';');

                            for (int indexImg = 1; indexImg <= frames; ++indexImg)
                            {                                
                                var contentItem = lstMap[indexImg - 1];
                                var rectStr = contentItem.Split(',');

                                var rect = new Rectangle( Convert.ToInt32(rectStr[0]),  // X
                                                          Convert.ToInt32(rectStr[1]),  // Y
                                                          Convert.ToInt32(rectStr[2]),  // WIDTH
                                                          Convert.ToInt32(rectStr[3])); // HEIGHT

                                // distance from left x
                                framesDx.Add(Convert.ToInt32(rectStr[4]));
                                // distance from top x
                                framesDy.Add(Convert.ToInt32(rectStr[5]));

                                var newTexture = new Texture2D(gdm.GraphicsDevice, rect.Width, rect.Height );

                                int count = rect.Width * rect.Height;
                                Color[] data = new Color[count];
                                maxTexture.GetData(0, rect, data, 0, count);
                                newTexture.SetData(data);

                                lst.Add(newTexture);
                            }
                        }
                    }                    
                }
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
