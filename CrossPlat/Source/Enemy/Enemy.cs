
namespace GameSystem
{
	public class Enemy : GameObject
	{
		public Enemy(BaseWorld world, GameMapEnemy entity) : base (world)
		{
			ObjectType = GameObjectType.Enemy;
			strMyImage = entity.MyStrImage;
			SetPos(entity.x_pos, entity.y_pos);

			NeedsUpdate = true;
		}
	}
}
