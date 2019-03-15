using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameSystem
{
	public partial class BaseWorld : GameObject
	{
		public override void Draw(SpriteBatch sb)
		{
			lstCollisions.Clear();

			mDx = drawRect.X + drawRect.Width;
			mDy = drawRect.Y + drawRect.Height;

			DrawOnScreenObjects(lstBgObjects, sb);

			if (p1 != null)
			{
				lstCollisions.AddRange(p1.lstPlasmaFire);
				p1.Draw(sb);
			}

			if (p2 != null)
			{
				lstCollisions.AddRange(p2.lstPlasmaFire);
				p2.Draw(sb);
			}

			DrawOnScreenObjects(lstEnemies, sb);
			DrawOnScreenObjects(lstForeObjects, sb);

		//	if (IsPaused)
			//	sb.Draw(imgPause, zeroVector, Color.White);
		}

		public void DrawOnScreenObjects(List<GameObject> lst, SpriteBatch sb)
		{
			foreach (var obj in lst)
			{
				obj.Draw(sb);

				
			}
		}
	}
}
