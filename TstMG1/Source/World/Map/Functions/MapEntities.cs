using System.Collections;
using System.Collections.Generic;

namespace GameObjects
{
	public class MapEvent
	{
		public List<string> MyParams = new List<string>();
		public Hashtable MyValues = new Hashtable();
	}

	public class PathPosition
	{
		public int  offsetX = 0,
					offsetY = 0,
					waitFrames = 0;
	}

	public class GameMapPathList
	{
		public List<PathPosition> Path = new List<PathPosition>();
	}

	public class GameMapEvent : MapEvent
	{
		public string MyType = "";

		public long MyFrame = 0,
					world_x_pos = 0,
					world_y_pos = 0,
					p1_x_pos = 0,
					p1_y_pos = 0,
					p2_x_pos = 0,
					p2_y_pos = 0;

		public float world_x_speed = 0,
					 world_y_speed = 0;
	}
	
	public class GameMapEnemy : MapEvent
	{
		public string	MyLabel = "",
						MySubType = "",
						MyStrImage = "";
		
		public long x_pos = 0,
					y_pos = 0;
	}

	public class GameMapPanel : MapEvent
	{
		public string	MySubType = "",
						MyStrImage = "";
		
		public long x_pos = 0,
					y_pos = 0;
	}
}
