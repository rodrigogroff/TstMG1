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

        public int virtualWidth = 1920,
                   virtualHeight = 1080;
    }

    class SmartFramerate
    {
        double currentFrametimes;
        double weight;
        int numerator;

        public double framerate
        {
            get
            {
                return (numerator / currentFrametimes);
            }
        }

        public SmartFramerate(int oldFrameWeight)
        {
            numerator = oldFrameWeight;
            weight = (double)oldFrameWeight / ((double)oldFrameWeight - 1d);
        }

        public void Update(double timeSinceLastFrame)
        {
            currentFrametimes = currentFrametimes / weight;
            currentFrametimes += timeSinceLastFrame;
        }
    }

    public class GameLooper : Game
	{
		GameOptions go = new GameOptions();

		GraphicsDeviceManager gdm;
		RenderTarget2D rTarget;
		BaseWorld gameWorld;
		Rectangle destRect;		
		SpriteBatch batch;
        SmartFramerate smartFPS = new SmartFramerate(5);
        SpriteFont font;

        public GameLooper()
		{
            #region - code - 

            gdm = new GraphicsDeviceManager(this);

			go.gdm = gdm;
			gdm.IsFullScreen = true;
            gdm.SynchronizeWithVerticalRetrace = true;

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
                font = Content.Load<SpriteFont>("MainFont");

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
            smartFPS.Update(gameTime.ElapsedGameTime.TotalSeconds);

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

                batch.DrawString(font, "FPS:" + smartFPS.framerate.ToString("00"), new Vector2(10, 10), Color.White);
                
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
