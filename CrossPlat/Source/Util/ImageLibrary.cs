using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace GameSystem
{
	public class ImageLibrary
	{
		public Hashtable hshImage = new Hashtable(),
						 hshTags = new Hashtable();

		public Texture2D Get(string path, string tag)
		{
			var lst = hshTags[tag] as List<string>;
			if (lst == null)
			{
				lst = new List<string>();
				hshTags[tag] = lst;
			}
			lst.Add(path);
			return hshImage[path] as Texture2D;
		}

		public void Clear(string tag)
		{
			var lst = hshTags[tag] as List<string>;
			if (lst != null)
				lst.ForEach(y => hshImage[y] = null);
		}

		public void Save(string path, string tag, ref Texture2D image)
		{
			hshImage[path] = image;
		}
	}
}
