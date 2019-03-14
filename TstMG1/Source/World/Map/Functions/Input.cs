using Microsoft.Xna.Framework.Input;

namespace GameSystem
{
	public partial class GameMap : BaseWorld
	{
		bool btnF5 = false,
			 btnPause = false;
		
		public bool IsKbdReleased(ref KeyboardState kbs, Keys key, ref bool controller)
		{
			if (kbs.IsKeyDown(key) && !controller) controller = true;
			else if (controller && kbs.IsKeyUp(key)) { controller = false; return true; }
			return false;
		}

		public void ProcessInput()
		{
			var kbs = Keyboard.GetState();

			if (IsKbdReleased(ref kbs, Keys.Pause, ref btnPause))
			{
				IsPaused = !IsPaused;
				return;
			}

			if (!go.gdm.IsFullScreen)
				if (kbs.IsKeyDown(Keys.LeftAlt) && kbs.IsKeyDown(Keys.Enter))
					ChangeScreen = true;

			if (kbs.IsKeyDown(Keys.Escape))
				ExitGame = true;

			#if GAMEDEV

				if (IsKbdReleased(ref kbs, Keys.F5, ref btnF5))
			{
				Reload();
				return;
			}

			#endif

			if (IsPaused)
			{
				if (kbs.IsKeyDown(Keys.NumPad6)) UpdatePosition(10, 0);
				if (kbs.IsKeyDown(Keys.NumPad4)) UpdatePosition(-10, 0);
				if (kbs.IsKeyDown(Keys.NumPad8)) UpdatePosition(0, -10);
				if (kbs.IsKeyDown(Keys.NumPad2)) UpdatePosition(0, 10);
				if (kbs.IsKeyDown(Keys.NumPad9)) UpdatePosition(10, -10);
				if (kbs.IsKeyDown(Keys.NumPad7)) UpdatePosition(-10, -10);
				if (kbs.IsKeyDown(Keys.NumPad1)) UpdatePosition(-10, 10);
				if (kbs.IsKeyDown(Keys.NumPad3)) UpdatePosition(10, 10);
			}
		}
	}
}
