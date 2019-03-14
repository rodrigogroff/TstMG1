using GameObjects;

namespace GameUtil
{
	public partial class GameObject 
	{
		public virtual void LoadContent(GameOptions go, string strWorld)
		{
			if (strMyImage == "") return;

			var im = go.im;

			switch (ObjectType)
			{
				case GameObjectType.Background:
				case GameObjectType.BackgroundBlock:
				case GameObjectType.ForeGround:

					strMyImage = "Bg\\" + strMyImage;
					break;

				case GameObjectType.Enemy:

					strMyImage = "Enemy\\" + strMyImage;
					break;
			}

			MyBaseImage = go.imageLibrary.Get(strMyImage, strWorld);

			if (MyBaseImage == null)
			{
				im.LoadImage(strMyImage, ref MyBaseImage, ref go.gdm);
				go.imageLibrary.Save(strMyImage, strWorld, ref MyBaseImage);
			}

			drawRect.Width = MyBaseImage.Width;
			drawRect.Height = MyBaseImage.Height;
		}
	}
}
