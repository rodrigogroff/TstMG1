using System.Diagnostics;

namespace GameSystem
{
	public partial class Player : GameObject
	{
		public void LoadContent(GameOptions go)
		{
			var lib = go.im;

			switch (shipSkin)
			{
				case PlayerSkin.White:
                    //lstSpaceShipImg = lib.LoadImageSequence("Players\\Blue\\cw_blue", 30, ref go.gdm);        // 78 milis
                    lstSpaceShipImg = lib.LoadImageMappedSequence("Players\\Blue\\cw_blue", 30, ref go.gdm);    // 63 milis
                    break;

                case PlayerSkin.Red:
                    lstSpaceShipImg = lib.LoadImageMappedSequence("Players\\Red\\cw_red", 30, ref go.gdm);
                    break;
			}

			lib.LoadImage("Players\\plasmaShotBlue.png", ref plasmaShotBlue, ref go.gdm);
			lib.LoadImage("Players\\plasmaShot.png", ref plasmaShot, ref go.gdm);
			lib.LoadImage("Players\\option.png", ref optionImage, ref go.gdm);
		}
	}
}
