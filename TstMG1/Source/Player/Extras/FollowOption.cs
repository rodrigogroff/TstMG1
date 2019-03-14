using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GameUtil;

namespace GameObjects
{
	public class PlayerFollowOption : GameObject
	{
		public GameObject myFollow;

		public float speed_accel = 0;

		public PlayerFollowOption(GameObject _follow, Texture2D _myImage) : base(_follow.MyWorld)
		{
			ObjectType = GameObjectType.Player;

			myFollow = _follow;
			MyBaseImage = _myImage;

			SetPos( (long) myFollow.MyGlobalPosition.X, (long) myFollow.MyGlobalPosition.Y);
		}

		public override void Update()
		{
			base.Update();

			float perimetroMinimo = 75,
				  perimetroAceleracao = 95;

			float dist = Vector2.Distance(MyGlobalPosition, myFollow.MyGlobalPosition);

			if (dist < perimetroMinimo)
			{
				if (speed_accel > 0)
				{
					speed_accel -= 0.5f;
					if (speed_accel < 0) speed_accel = 0;
				}
			}
			else
				if (dist > perimetroAceleracao) speed_accel += 0.1f;

			if (speed_accel > 0)
				MyGlobalPosition += Vector2.Normalize(myFollow.MyGlobalPosition - MyGlobalPosition) * speed_accel;
		}

		public override void Draw(SpriteBatch sb)
		{
			base.Draw(sb);
		}
	}
}
