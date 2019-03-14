﻿using Microsoft.Xna.Framework.Graphics;

namespace GameSystem
{
	public partial class Player : GameObject
	{
		public override void Draw(SpriteBatch sb)
		{
			sb.Draw(lstSpaceShipImg[curFrame], GetLocalPosition(), opaqueColor);

			lstPlasmaFire.ForEach(y => y.Draw(sb));
			lstOption.ForEach(y => y.Draw(sb));
		}
	}
}
