
namespace GameSystem
{
	public partial class Player : GameObject
	{
		public override void Terminate()
		{
			lstPlasmaFire.RemoveAll(y => y.NeedsTermination);
			lstOption.RemoveAll(y => y.NeedsTermination);
		}

		public override void Update()
		{
			base.Update();

			ProcessFire();
			ProcInput();

			if (IsUp)
			{
				if (curFrame > 0) --curFrame;
			}
			else if (IsDown)
			{
				if (curFrame < 29) ++curFrame;
			}
			else if (InternalTimer % 2 == 0)
			{
				if (curFrame > 14) --curFrame; else if (curFrame < 14) ++curFrame;
			}

			lstPlasmaFire.ForEach(y => y.Update());
			lstOption.ForEach(y => y.Update());
		}
	}
}
