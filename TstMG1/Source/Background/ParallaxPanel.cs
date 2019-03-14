using GameUtil;

namespace GameObjects
{
	public class ParallaxPanel : GameObject
	{
		float myLayer = 1;

		public ParallaxPanel(BaseWorld world, GameMapPanel gm) : base(world)
		{
			ObjectType = GameObjectType.Background;
			strMyImage = gm.MyStrImage;

			myLayer = F(gm.MyValues["layer"].ToString());
			SetPos(gm.x_pos, gm.y_pos);
			NeedsUpdate = true;
		}

		public override void Update()
		{
			UpdatePosition ( MyWorld.xWorldSpeed - MyWorld.xWorldSpeed / myLayer, 
							 MyWorld.yWorldSpeed - MyWorld.yWorldSpeed / myLayer);
		}
	}
}
