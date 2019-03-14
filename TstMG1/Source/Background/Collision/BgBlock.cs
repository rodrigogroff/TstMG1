using Microsoft.Xna.Framework;

using GameUtil;

namespace GameObjects
{
	public class BgBlock : GameObject
	{
		public BgBlock(BaseWorld world, GameMapPanel gm) : base(world)
		{
			ObjectType = GameObjectType.BackgroundBlock;
			strMyImage = gm.MyStrImage;
			SetPos(gm.x_pos, gm.y_pos);

			var _params = gm.MyValues["collision"].ToString().Split(',');

			Collision = true;
			colisionRect = new Rectangle(I(_params[0]), I(_params[1]), I(_params[2]), I(_params[3]));
		}

		public override void ProcessCollisions()
		{
			foreach (var itemCol in lstCollisions)
			{
				switch (itemCol.ObjectType)
				{
					case GameObjectType.PlayerPlasmaFire:
						itemCol.NeedsTermination = true;
						break;
				}
			}
		}
	}
}
