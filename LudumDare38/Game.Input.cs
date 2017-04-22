using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace LudumDare38
{
    public partial class Game
    {
        private KeyboardState previousKbState;
        private KeyboardState currentKbState;

		public bool IsUpPressed
        {
			get
            {
                return (currentKbState.IsKeyDown(Key.W) && !previousKbState.IsKeyDown(Key.W))
                    || (currentKbState.IsKeyDown(Key.Up) && !previousKbState.IsKeyDown(Key.Up));
            }
        }

        public bool IsDownPressed
        {
            get
            {
                return (currentKbState.IsKeyDown(Key.S) && !previousKbState.IsKeyDown(Key.S))
                    || (currentKbState.IsKeyDown(Key.Down) && !previousKbState.IsKeyDown(Key.Down));
            }
        }

        public bool IsLeftPressed
        {
            get
            {
                return (currentKbState.IsKeyDown(Key.A) && !previousKbState.IsKeyDown(Key.A))
                    || (currentKbState.IsKeyDown(Key.Left) && !previousKbState.IsKeyDown(Key.Left));
            }
        }

        public bool IsRightPressed
        {
            get
            {
                return (currentKbState.IsKeyDown(Key.D) && !previousKbState.IsKeyDown(Key.D))
					|| (currentKbState.IsKeyDown(Key.Right) && !previousKbState.IsKeyDown(Key.Right));
            }
        }
    }
}
