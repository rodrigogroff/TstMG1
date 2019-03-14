using System;
using System.IO;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSystem
{
    public class GameOptions
    {
        public GraphicsDeviceManager gdm;
        public ImageLoader im = new ImageLoader();
        public ImageLibrary imageLibrary = new ImageLibrary();

        public int virtualWidth = 960,
                   virtualHeight = 540;
    }

    public class GameLooper : Game
	{
		GameOptions go = new GameOptions();

		GraphicsDeviceManager gdm;
		RenderTarget2D rTarget;
		BaseWorld gameWorld;
		Rectangle destRect;		
		SpriteBatch batch;
		
		public GameLooper()
		{
            #region - code - 

            gdm = new GraphicsDeviceManager(this);

			go.gdm = gdm;
			gdm.IsFullScreen = false;

			if (gdm.IsFullScreen)
			{
				gdm.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
				gdm.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
			}
			else
			{
				gdm.PreferredBackBufferWidth = go.virtualWidth;
				gdm.PreferredBackBufferHeight = go.virtualHeight;
			}

			gdm.ApplyChanges();

            #endregion
        }

        protected override void Initialize()
		{
            #region - code -

            try
			{
				base.Initialize();

				var sf = GraphicsDevice.PresentationParameters.BackBufferFormat;

				batch = new SpriteBatch(GraphicsDevice);
				rTarget = new RenderTarget2D(GraphicsDevice, go.virtualWidth, go.virtualHeight, false, sf, DepthFormat.Depth24);
				destRect = new Rectangle(0, 0, gdm.PreferredBackBufferWidth, gdm.PreferredBackBufferHeight);
				
				gameWorld = new GameMap(Content,"w1.txt", true, go);
			}
			catch (Exception ex )
			{
				SaveException(ex);
			}

            #endregion
        }
				
		protected override void Update(GameTime gameTime)
		{
            #region - code - 

            if (gameWorld == null)
                return;

			try
			{
				if (gameWorld.ChangeScreen)
				{
					gameWorld.ChangeScreen = false;

					gdm.IsFullScreen = true;
					gdm.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
					gdm.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

					gdm.ApplyChanges();

					destRect = new Rectangle(0, 0, gdm.PreferredBackBufferWidth, gdm.PreferredBackBufferHeight);
				}

				if (gameWorld.ExitGame)
					Exit();

				gameWorld.UpdateCollisions();
				gameWorld.Terminate();
				gameWorld.Update();
			}
			catch (Exception ex)
			{
				SaveException(ex);
			}

            #endregion
        }
		
		protected override void Draw(GameTime gameTime)
		{
            #region - code - 

            try
			{
				// render to virtual console resolution
				GraphicsDevice.SetRenderTarget(rTarget);
				GraphicsDevice.Clear(Color.Black);
				batch.Begin(blendState: BlendState.NonPremultiplied);
				gameWorld.Draw(batch);
				batch.End();
				GraphicsDevice.SetRenderTarget(null);

				// render to full screen
				GraphicsDevice.Clear(Color.Black);
				batch.Begin();
				batch.Draw(rTarget, destRect, gameWorld.worldColor);
				batch.End();
			}
			catch (Exception ex)
			{
				SaveException(ex);
			}

            #endregion
        }

		public void SaveException (Exception ex)
		{
            #region - code - 

            var file = "log.txt";
			if (File.Exists(file)) File.Delete(file);
			using (var s = new StreamWriter(file, false, System.Text.Encoding.Unicode))
				s.Write(ex.ToString());
			Process.Start("notepad.exe", file);
			Exit();

            #endregion
        }
	}
}
