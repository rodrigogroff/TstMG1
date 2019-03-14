using GameUtil;

namespace GameObjects
{
	public partial class Player : GameObject
	{
		public void LoadContent(GameOptions go)
		{
			var im = go.im;

			switch (shipSkin)
			{
				case PlayerSkin.White: lstSpaceShipImg = im.LoadImageSequence("Players\\Blue\\cw_blue", 30, ref go.gdm); break;
				case PlayerSkin.Red: lstSpaceShipImg = im.LoadImageSequence("Players\\Red\\cw_red", 30, ref go.gdm); break;
			}

			im.LoadImage("Players\\plasmaShotBlue.png", ref plasmaShotBlue, ref go.gdm);
			im.LoadImage("Players\\plasmaShot.png", ref plasmaShot, ref go.gdm);
			im.LoadImage("Players\\option.png", ref optionImage, ref go.gdm);
		}
	}
}
