using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSystem
{
	public class PlayerPlasma : GameObject
	{
		int maxTimer = 50;

		float	speedX = 0, 
				speedY = 0;

		public PlayerPlasma ( BaseWorld _world, 
							  Vector2 _myPos, 
							  int xAdd, 
							  int yAdd, 
							  float _speedX, 
							  float _speedY, 
							  Texture2D _myRefImage) : base(_world)
		{
			ObjectType = GameObjectType.PlayerPlasmaFire;

			speedX = _speedX;
			speedY = _speedY;
			MyBaseImage = _myRefImage;
			
			SetPos((long)_myPos.X + xAdd, (long)_myPos.Y + yAdd);

			Collision = true;
			colisionRect = new Rectangle(10, 3, 68, 2);
		}

		public override void Update()
		{
			base.Update();
				
			UpdatePosition(speedX, speedY);

			if (InternalTimer > maxTimer)
				NeedsTermination = true;
		}

		public override void Draw(SpriteBatch sb)
		{
			base.Draw(sb);
		}

		public override void ProcessCollisions()
		{
			foreach (var item in lstCollisions)
			{
				switch (item.ObjectType)
				{
					case GameObjectType.BackgroundBlock:
						// push
						item.UpdatePosition(2,0);
						break;
				}
			}
		}
	}
}
