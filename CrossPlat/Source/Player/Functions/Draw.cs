using Microsoft.Xna.Framework.Graphics;

namespace GameSystem
{
	public partial class Player : GameObject
	{
		public override void Draw(SpriteBatch sb)
		{
            {
                var retImg = lstSpaceShipImg[curFrame];
                var rectDest = GetLocalPosition();

                rectDest.X += framesDx[curFrame];
                rectDest.Y += framesDy[curFrame];

                sb.Draw(retImg, rectDest, opaqueColor);
            }

			lstPlasmaFire.ForEach(y => y.Draw(sb));
			lstOption.ForEach(y => y.Draw(sb));
		}
	}
}
