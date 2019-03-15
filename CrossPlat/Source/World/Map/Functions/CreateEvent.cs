
namespace GameSystem
{
	public partial class GameMap : BaseWorld
	{
		public void ExecuteEvent(GameMapEvent _event)
		{
			switch (_event.MyType)
			{
				case "start":

					SetPos(_event.world_x_pos, _event.world_y_pos);

					if (p1 != null) p1.SetPos(_event.p1_x_pos, _event.p1_y_pos);
					if (p2 != null) p2.SetPos(_event.p2_x_pos, _event.p2_y_pos);

					break;

				case "speed":

					xDesiredSpeed = _event.world_x_speed;
					yDesiredSpeed = _event.world_y_speed;

					break;
			}
		}
	}
}
