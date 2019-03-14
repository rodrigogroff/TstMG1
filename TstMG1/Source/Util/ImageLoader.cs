using System.IO;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameUtil
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
	}
}
