using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameSystem
{
	public partial class Player : GameObject
	{
		public void ProcInput()
		{
            var kbs = Keyboard.GetState();

            GamePadState gps = GamePad.GetState(PlayerIndex.One);

            if (this.myPlayer == PlayerSelection.PlayerTwo)
				gps = GamePad.GetState(PlayerIndex.Two);

            // ------------------------------
            // animations
            // ------------------------------

            IsUp = false;
            IsDown = false;

            // ------------------------------
            // triggers
            // ------------------------------

            bool CanGoUp = false, CanGoDown = false,
                 CanGoLeft = false, CanGoRight = false,                 
                 moveLeft = false, moveRight = false,
                 moveUp = false, moveDown = false;

            // --------------------
            // map bounds
            // --------------------

            if (MyGlobalPosition.Y > MyWorld.MyGlobalPosition.Y ) CanGoUp = true;
			if (MyGlobalPosition.X > MyWorld.MyGlobalPosition.X ) CanGoLeft = true;
			if (MyGlobalPosition.Y < MyWorld.MyGlobalPosition.Y + MyWorld.go.virtualHeight - 135 ) CanGoDown = true;
			if (MyGlobalPosition.X < MyWorld.MyGlobalPosition.X + MyWorld.go.virtualWidth - 135) CanGoRight = true;

            // --------------------
            // input check
            // --------------------

            if (!gps.IsConnected)
            {
                if (myPlayer == PlayerSelection.PlayerOne)
                {
                    if (kbs.IsKeyDown(Keys.W) || kbs.IsKeyDown(Keys.Up)) { if (CanGoUp) moveUp = true; IsUp = true; }
                    if (kbs.IsKeyDown(Keys.A) || kbs.IsKeyDown(Keys.Left)) if (CanGoLeft) { moveLeft = true; }
                    if (kbs.IsKeyDown(Keys.S) || kbs.IsKeyDown(Keys.Down)) { if (CanGoDown) moveDown = true; IsDown = true; }
                    if (kbs.IsKeyDown(Keys.D) || kbs.IsKeyDown(Keys.Right)) if (CanGoRight) { moveRight = true; }

                    if (kbs.IsKeyDown(Keys.Z)) if (!IsAutoFire) { fireTimer = 0; IsAutoFire = true; }
                }
                else if (myPlayer == PlayerSelection.PlayerTwo)
                {
                    if (kbs.IsKeyDown(Keys.T) || kbs.IsKeyDown(Keys.Up)) { if (CanGoUp) moveUp = true; IsUp = true; }
                    if (kbs.IsKeyDown(Keys.F) || kbs.IsKeyDown(Keys.Left)) if (CanGoLeft) moveLeft = true; ;
                    if (kbs.IsKeyDown(Keys.G) || kbs.IsKeyDown(Keys.Down)) { if (CanGoDown) moveDown = true; IsDown = true; }
                    if (kbs.IsKeyDown(Keys.H) || kbs.IsKeyDown(Keys.Right)) if (CanGoRight) moveRight = true; 

                    if (kbs.IsKeyDown(Keys.K)) if (!IsAutoFire) { fireTimer = 0; IsAutoFire = true; }
                }
            }
            else
            {
                if (gps.DPad.Up == ButtonState.Pressed) { if (CanGoUp) moveUp = true; IsUp = true; }
                if (gps.DPad.Left == ButtonState.Pressed) if (CanGoLeft) moveLeft = true;
                if (gps.DPad.Right == ButtonState.Pressed) if (CanGoDown) moveRight = true;
                if (gps.DPad.Down == ButtonState.Pressed) { if (CanGoRight) moveDown = true; IsDown = true; }

                if (gps.Buttons.A == ButtonState.Pressed) if (!IsAutoFire) { fireTimer = 0; IsAutoFire = true; }
                if (gps.Buttons.B == ButtonState.Pressed) { }
                if (gps.Buttons.X == ButtonState.Pressed) { }
                if (gps.Buttons.Y == ButtonState.Pressed) { }
            }

            // -------------------------------------
            // extra keyboard testing...
            // -------------------------------------

            if (IsKbdReleased(ref kbs, Keys.F1, ref btnF1)) powerLevel = 1;
			if (IsKbdReleased(ref kbs, Keys.F2, ref btnF2)) powerLevel = 2;
			if (IsKbdReleased(ref kbs, Keys.F3, ref btnF3)) powerLevel = 3;
			if (IsKbdReleased(ref kbs, Keys.F4, ref btnF4)) DeployOption();

            #region - acceleration vectors - 

            if (accelX > 8) accelX = 8;

            float stepsx = 0, stepsy = 0;

            switch (accelX)
            {
                case 1: stepsx = 0.2F; break;
                case 2: stepsx = 0.4F; break;
                case 3: stepsx = 0.6F; break;
                case 4: stepsx = 1.0F; break;
                case 5: stepsx = 1.5F; break;
                case 6: stepsx = 2.0F; break;
                case 7: stepsx = 3.0F; break;
                case 8: stepsx = 4.0F; break;
            }

            if (accelY > 8) accelY = 8;

            switch (accelY)
            {
                case 1: stepsy = 0.2F; break;
                case 2: stepsy = 0.4F; break;
                case 3: stepsy = 0.6F; break;
                case 4: stepsy = 1.0F; break;
                case 5: stepsy = 1.5F; break;
                case 6: stepsy = 2.0F; break;
                case 7: stepsy = 3.0F; break;
                case 8: stepsy = 4.0F; break;
            }

            #endregion

            if (moveRight) { MyGlobalPosition.X += stepsx; accelX++; }
            if (moveLeft) { MyGlobalPosition.X -= stepsx; accelX++; }
            if (moveDown) { MyGlobalPosition.Y += stepsy; accelY++; }
            if (moveUp) { MyGlobalPosition.Y -= stepsy; accelY++; }            

            if (!moveRight && !moveLeft) accelX = 1;
            if (!moveUp && !moveDown) accelY = 1;
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
