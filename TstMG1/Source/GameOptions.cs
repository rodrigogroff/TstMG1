using Microsoft.Xna.Framework;

using GameUtil;

namespace GameObjects
{
	public class GameOptions
	{
		public GraphicsDeviceManager gdm;
		public ImageLoader im = new ImageLoader();
		public ImageLibrary imageLibrary = new ImageLibrary();

		public int virtualWidth = 960,
				   virtualHeight = 540;
	}
}
