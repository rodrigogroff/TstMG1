using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSystem
{
	public partial class GameObject 
	{
		public GameObjectType ObjectType;

		public BaseWorld MyWorld;
		public Texture2D MyBaseImage;
		public Vector2 MyGlobalPosition;

		public Rectangle drawRect = new Rectangle (0,0,0,0),
						 colisionRect;

		public List<GameObject> lstCollisions = new List<GameObject>();

		public Color opaqueColor = Color.White;

		public string strMyImage = "";

		public long ObjectId = 0,
					InternalTimer = 0;

		public bool Collision = false,
					NeedsUpdate = false,
					NeedsTermination = false;

        public List<string> log = new List<string>();

		public GameObject(BaseWorld _w)
		{
			MyWorld = _w;
		}

		public virtual void ProcessCollisions() { }
		public virtual void Terminate() { }

		public virtual void Update()
		{
			++InternalTimer;
		}

		public virtual void Draw(SpriteBatch sb)
		{
			sb.Draw(MyBaseImage, GetLocalPosition(), opaqueColor);
		}

        public void DumpLog()
        {
            var strFinal = "";
            foreach (var item in log)
                strFinal += item + "\r\n";
            throw (new System.Exception(strFinal));
        }
	}
}
