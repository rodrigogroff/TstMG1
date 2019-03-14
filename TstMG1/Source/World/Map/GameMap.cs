using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameSystem
{
	public partial class GameMap : BaseWorld
	{
		public GameMap ( ContentManager cm, 
					     string strMapName, 
						 bool LoadPlayer, 
						 GameOptions _go) : base(LoadPlayer, _go)
		{
			ObjectType = GameObjectType.Map;
			MyMapName = strMapName;

			LoadMap();
			LoadContent();

		//	switch (MyMapName)
		//	{
	//			default: levelMusic = cm.Load<Song>("Content/Music/Axelay_Kick_My_Axe_OC_ReMix"); break;
		//	}

			MediaPlayer.Play(levelMusic);
		}

		public void LoadContent()
		{
			lstBgObjects.ForEach(y => y.LoadContent(go, MyMapName));
			lstEnemies.ForEach(y => y.LoadContent(go, MyMapName));
			lstForeObjects.ForEach(y => y.LoadContent(go, MyMapName));
		}
				
		public override void Update()
		{
			++WorldFrameNumber;
			ProcessInput();
			foreach (var item in lstMapEvents.Where(y => y.MyFrame == WorldFrameNumber))
				ExecuteEvent(item);
			base.Update();
		}

		public override void Draw(SpriteBatch sb)
		{
			if (IsFadeIn)
			{
				worldColor = Color.FromNonPremultiplied ( fadeIntensity, 
														  fadeIntensity, 
														  fadeIntensity, 
														  fadeIntensity );
				fadeIntensity++;

				if (fadeIntensity > 255)
					IsFadeIn = false;
			}			
			else if (IsFadeOut)
			{
				worldColor = Color.FromNonPremultiplied(fadeIntensity,
														  fadeIntensity,
														  fadeIntensity,
														  fadeIntensity);
				fadeIntensity--;

				if (fadeIntensity < 0)
					IsFadeOut = false;
			}

			base.Draw(sb);
		}
		
		#if GAMEDEV

		public void Reload()
		{
			if (IsPaused)
			{
				Vector2 pos = MyGlobalPosition,
						p1Pos = p1.MyGlobalPosition,
						p2Pos = p2.MyGlobalPosition;

				int frame = WorldFrameNumber;

				go.imageLibrary.Clear(MyMapName);

				ClearWorld();
				LoadMap();
				LoadContent();

				MyGlobalPosition = pos;
				p1.MyGlobalPosition = p1Pos;
				p2.MyGlobalPosition = p2Pos;
				WorldFrameNumber = frame;
			}
		}

		#endif
	}
}
