using System;
using Microsoft.Xna.Framework;

namespace GameSystem
{
	public partial class GameObject 
	{
		public void SetPos(long _x, long _y)
		{
			MyGlobalPosition.X = _x;
			MyGlobalPosition.Y = _y;

			drawRect.X = (int)_x;
			drawRect.Y = (int)_y;
		}

		public void UpdatePosition(float _xIncrease, float _yIncrease)
		{
			MyGlobalPosition.X += _xIncrease;
			MyGlobalPosition.Y += _yIncrease;

			drawRect.X = (int)MyGlobalPosition.X;
			drawRect.Y = (int)MyGlobalPosition.Y;
		}

		public Vector2 GetLocalPosition()
		{
			return new Vector2 ( MyGlobalPosition.X - MyWorld.MyGlobalPosition.X,
								 MyGlobalPosition.Y - MyWorld.MyGlobalPosition.Y  );
		}
		
		public int GetXPivot ()
		{
			return (int) (MyGlobalPosition.X + colisionRect.X + colisionRect.Width / 2);
		}

		public int GetYPivot()
		{
			return (int)(MyGlobalPosition.Y + colisionRect.Y + colisionRect.Height / 2);
		}

		public int I(string par) { return Convert.ToInt32(par); }
		public int I(float par) { return Convert.ToInt32(par); }
		public float F(string par) { return (float)Convert.ToDouble(par); }
	}
}
