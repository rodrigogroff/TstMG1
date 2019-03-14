using GameUtil;

namespace GameObjects
{
	public partial class BaseWorld : GameObject
	{
		public void GatherCollisions()
		{
			if (p1 != null)
			{
				lstCollisions.Add(p1);
				lstCollisions.AddRange(p1.lstPlasmaFire);
			}

			if (p2 != null)
			{
				lstCollisions.Add(p2);
				lstCollisions.AddRange(p2.lstPlasmaFire);
			}

			foreach (var item in lstBgObjects)
				if (item.Collision)
					lstCollisions.Add(item);

			foreach (var item in lstEnemies)
				if (item.Collision)
					lstCollisions.Add(item);
		}

		public void UpdateCollisions()
		{
			GatherCollisions();
			
			foreach (var obj in lstCollisions)
			{
				obj.lstCollisions.Clear();

				var src = obj.colisionRect;

				src.X += I(obj.MyGlobalPosition.X);
				src.Y += I(obj.MyGlobalPosition.Y);

				int srcXW = src.X + src.Width,
					srcYH = src.Y + src.Height,
					colls = 0;

				foreach (var objCollider in lstCollisions)
				{
					if (objCollider == obj)
						continue;

					var target = objCollider.colisionRect;

					target.X += I(objCollider.MyGlobalPosition.X);
					target.Y += I(objCollider.MyGlobalPosition.Y);

					if (target.X < srcXW && target.X + target.Width > src.X &&
						target.Y < srcYH && target.Y + target.Height > src.Y)
					{
						obj.lstCollisions.Add(objCollider);
						colls++;
					}
				}

				if (colls > 0)
					obj.ProcessCollisions();
			}
		}
	}
}
