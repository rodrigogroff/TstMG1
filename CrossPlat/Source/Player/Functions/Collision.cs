
namespace GameSystem
{
	public partial class Player : GameObject
	{
		public override void ProcessCollisions()
		{
			foreach (var item in lstCollisions)
			{
				switch (item.ObjectType)
				{
					case GameObjectType.BackgroundBlock:

						int block_xPivotPoint = item.GetXPivot(),
							block_yPivotPoint = item.GetYPivot(),
							this_xPivotPoint = GetXPivot(),
							this_yPivotPoint = GetYPivot();

						float xMov = 0, yMov = 0;

						if (this_xPivotPoint < block_xPivotPoint) xMov = -30;
						if (this_xPivotPoint > block_xPivotPoint) xMov = 30;
						if (this_yPivotPoint < block_yPivotPoint) yMov = -30;
						if (this_yPivotPoint > block_yPivotPoint) yMov = 30;

						UpdatePosition(xMov, yMov);

						break;
				}
			}
		}
	}
}
