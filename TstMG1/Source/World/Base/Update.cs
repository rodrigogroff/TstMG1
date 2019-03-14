using System.Linq;

using GameUtil;

namespace GameObjects
{
	public partial class BaseWorld : GameObject
	{
		public override void Terminate()
		{
			if (p1 != null) p1.Terminate();
			if (p2 != null) p2.Terminate();

			lstBgObjects.ForEach(y => y.Terminate());
			lstEnemies.ForEach(y => y.Terminate());
			lstForeObjects.ForEach(y => y.Terminate());
		}

		public override void Update()
		{
			if (IsPaused) return;
			
			if (xWorldSpeed != xDesiredSpeed)
			{
				if (xWorldSpeed < xDesiredSpeed)
					xWorldSpeed += worldAccel;
				else
					xWorldSpeed -= worldAccel;
			}

			if (yWorldSpeed != yDesiredSpeed)
			{
				if (yWorldSpeed < yDesiredSpeed)
					yWorldSpeed += worldAccel;
				else
					yWorldSpeed -= worldAccel;
			}

			bool IsInMotion = (xWorldSpeed > 0 || yWorldSpeed > 0);

			if (IsInMotion)
				UpdatePosition(xWorldSpeed, yWorldSpeed);

			if (p1 != null)
			{
				if (IsInMotion)
					UpdatePlayerMotion(p1);

				p1.Update();
			}

			if (p2 != null)
			{
				if (IsInMotion)
					UpdatePlayerMotion(p2);

				p2.Update();
			}

			lstBgObjects.Where(y => y.NeedsUpdate).ToList().ForEach(y => y.Update());
			lstEnemies.ForEach(y => y.Update());

			GatherCollisions();
		}

		public void UpdatePlayerMotion(Player player)
		{
			player.UpdatePosition(xWorldSpeed, yWorldSpeed);

			foreach (var item in player.lstPlasmaFire)
				item.UpdatePosition(xWorldSpeed, yWorldSpeed);

			foreach (var item in player.lstOption)
				item.UpdatePosition(xWorldSpeed, yWorldSpeed);
		}
	}
}
