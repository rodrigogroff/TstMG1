using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GameUtil;

namespace GameObjects
{
	public partial class Player : GameObject
	{
		PlayerSelection myPlayer;
		PlayerSkin shipSkin;

		public Texture2D plasmaShot,
						 plasmaShotBlue,
						 optionImage;

		public List<Texture2D> lstSpaceShipImg;

		public List<GameObject> lstPlasmaFire = new List<GameObject>(),
								lstOption = new List<GameObject>();

		int powerLevel = 1, // start power
			curFrame = 14,  // main image star index
			fireTimer = 1;  // counter 			

		float speed = 5;

		bool IsAutoFire = false;

		public Player ( BaseWorld world, PlayerSelection _joy, PlayerSkin _shipSkin ) : base (world)
		{
			ObjectType = GameObjectType.Player;

			myPlayer = _joy;
			shipSkin = _shipSkin;

			colisionRect = new Rectangle(40, 48, 40, 10);

			LoadContent(world.go);
		}
	}
}
