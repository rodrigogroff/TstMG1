using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;

namespace GameSystem
{
	public partial class BaseWorld : GameObject
	{
		public GameOptions go;		
		public Player p1, p2;
		//public Texture2D imgPause;
		public Vector2 zeroVector = new Vector2(0, 0);

		public List<GameMapEvent> lstMapEvents = new List<GameMapEvent>();

		public List<GameObject> lstEnemies = new List<GameObject>(),
								lstBgObjects = new List<GameObject>(), 
								lstForeObjects = new List<GameObject>();

		public Song levelMusic;

		public Color worldColor = Color.White;
		
		public string MyMapName = "";

		public bool IsPaused = false,
					IsFadeIn = true,
					IsFadeOut = false,
					ChangeScreen = false,
					ExitGame = false;

		public float xWorldSpeed = 0, 
				     xDesiredSpeed = 0,
					 yWorldSpeed = 0, 
					 yDesiredSpeed = 0,
					 worldAccel = 0.05f;

		public int  WorldFrameNumber = 0,
					WorldObjectId = 0,
					virtualWidth = 0,
					virtualHeight = 0,
					mDx = 0,
					mDy = 0,
					fadeIntensity = 0;

		public BaseWorld ( bool LoadPlayers, GameOptions _go ) : base (null)
		{
			ObjectType = GameObjectType.Map;
			go = _go;
			
			drawRect.Width = go.virtualWidth;
			drawRect.Height = go.virtualHeight;

			//go.im.LoadImage("Other\\pause.png", ref imgPause, ref go.gdm);
			
			if (LoadPlayers)
			{
				p1 = new Player(this, PlayerSelection.PlayerOne, PlayerSkin.White);
				p2 = new Player(this, PlayerSelection.PlayerTwo, PlayerSkin.Red);
			}
		}
		
		public void ClearWorld()
		{
			lstBgObjects.Clear();
			lstForeObjects.Clear();
			lstEnemies.Clear();
		}
	}
}
