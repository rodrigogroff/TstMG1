using GameUtil;

namespace GameObjects
{
	public class BgPanel : GameObject
	{
		public BgPanel(BaseWorld world, GameMapPanel gm) : base(world)
		{
			ObjectType = GameObjectType.Background;
			strMyImage = gm.MyStrImage;
			SetPos(gm.x_pos, gm.y_pos);
		}
	}
}
