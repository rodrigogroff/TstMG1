using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using GameUtil;

namespace GameObjects
{
	public partial class Player : GameObject
	{
		public void ProcInput()
		{
			var gps = GamePad.GetState(PlayerIndex.One);
			var kbs = Keyboard.GetState();

			if (myPlayer == PlayerSelection.PlayerTwo)
				gps = GamePad.GetState(PlayerIndex.Two);

			IsUp = false; IsDown = false;

			bool CanGoUp = false,
				 CanGoLeft = false,
				 CanGoRight = false,
				 CanGoDown = false;

			if (MyGlobalPosition.Y > MyWorld.MyGlobalPosition.Y ) CanGoUp = true;
			if (MyGlobalPosition.X > MyWorld.MyGlobalPosition.X ) CanGoLeft = true;

			if (MyGlobalPosition.Y < MyWorld.MyGlobalPosition.Y + MyWorld.go.virtualHeight - 135 ) CanGoDown = true;
			if (MyGlobalPosition.X < MyWorld.MyGlobalPosition.X + MyWorld.go.virtualWidth - 135) CanGoRight = true;

			if (myPlayer == PlayerSelection.PlayerOne)
			{
				if (kbs.IsKeyDown(Keys.W) || kbs.IsKeyDown(Keys.Up)) { if (CanGoUp) MyGlobalPosition.Y -= speed; IsUp = true; }
				if (kbs.IsKeyDown(Keys.A) || kbs.IsKeyDown(Keys.Left)) if (CanGoLeft) MyGlobalPosition.X -= speed;
				if (kbs.IsKeyDown(Keys.S) || kbs.IsKeyDown(Keys.Down)) { if (CanGoDown) MyGlobalPosition.Y += speed; IsDown = true; }
				if (kbs.IsKeyDown(Keys.D) || kbs.IsKeyDown(Keys.Right)) if (CanGoRight) MyGlobalPosition.X += speed;

				if (kbs.IsKeyDown(Keys.J) || kbs.IsKeyDown(Keys.Z)) if (!IsAutoFire) { fireTimer = 0; IsAutoFire = true; }
			}
			else if (myPlayer == PlayerSelection.PlayerTwo)
			{
				if (kbs.IsKeyDown(Keys.T) || kbs.IsKeyDown(Keys.Up)) { if (CanGoUp) MyGlobalPosition.Y -= speed; IsUp = true; }
				if (kbs.IsKeyDown(Keys.F) || kbs.IsKeyDown(Keys.Left)) if (CanGoLeft) MyGlobalPosition.X -= speed;
				if (kbs.IsKeyDown(Keys.G) || kbs.IsKeyDown(Keys.Down)) { if (CanGoDown) MyGlobalPosition.Y += speed; IsDown = true; }
				if (kbs.IsKeyDown(Keys.H) || kbs.IsKeyDown(Keys.Right)) if (CanGoRight) MyGlobalPosition.X += speed;

				if (kbs.IsKeyDown(Keys.K) || kbs.IsKeyDown(Keys.Z)) if (!IsAutoFire) { fireTimer = 0; IsAutoFire = true; }
			}

			// special keys

			if (IsKbdReleased(ref kbs, Keys.F1, ref btnF1)) powerLevel = 1;
			if (IsKbdReleased(ref kbs, Keys.F2, ref btnF2)) powerLevel = 2;
			if (IsKbdReleased(ref kbs, Keys.F3, ref btnF3)) powerLevel = 3;
			if (IsKbdReleased(ref kbs, Keys.F4, ref btnF4)) DeployOption();

			if (gps.IsConnected) // joypad
			{
				if (gps.DPad.Up == ButtonState.Pressed) { if (CanGoUp) MyGlobalPosition.Y -= speed; IsUp = true; }
				if (gps.DPad.Left == ButtonState.Pressed) if (CanGoLeft) MyGlobalPosition.X -= speed;
				if (gps.DPad.Right == ButtonState.Pressed) if (CanGoDown) MyGlobalPosition.X += speed;
				if (gps.DPad.Down == ButtonState.Pressed) { if (CanGoRight) MyGlobalPosition.Y += speed; IsDown = true; }

				if (gps.Buttons.A == ButtonState.Released) PlayerFire();
				if (gps.Buttons.B == ButtonState.Released) { }
				if (gps.Buttons.X == ButtonState.Released) { }
				if (gps.Buttons.Y == ButtonState.Released) { }
			}
		}

		public void DeployOption()
		{
			if (lstOption.Count == 0)
				lstOption.Add(new PlayerFollowOption(this, optionImage));
			else
				lstOption.Add(new PlayerFollowOption(lstOption[lstOption.Count - 1], optionImage));
		}

		bool btnF1 = false, btnF2 = false, btnF3 = false, btnF4 = false;
		bool IsUp = false, IsDown = false;

		public bool IsKbdReleased(ref KeyboardState kbs, Keys key, ref bool controller)
		{
			if (kbs.IsKeyDown(key) && !controller) controller = true;
			else if (controller && kbs.IsKeyUp(key)) { controller = false; return true; }

			return false;
		}
	}
}
