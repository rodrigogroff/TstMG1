
namespace GameSystem
{
	public partial class GameMap : BaseWorld
	{
		public void CreateEnemy(GameMapEnemy _event)
		{
			switch (_event.MySubType)
			{
				case "1": lstEnemies.Add(new Enemy(this, _event)); break;
			}
		}
	}
}
