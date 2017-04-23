using OpenTK;
using OpenTK.Graphics;
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
		private GameWindow window;

		private void InitializeWindow()
		{
			window = new GameWindow(1280, 720, CreateGraphicsMode(16), "", GameWindowFlags.Default,DisplayDevice.Default, 3, 2, GraphicsContextFlags.Default);

			window.Load += Window_Load;
			window.Resize += Window_Resize;
			window.RenderFrame += Window_RenderFrame;
			window.UpdateFrame += Window_UpdateFrame;

			window.KeyDown += Window_KeyDown;
			window.KeyUp += Window_KeyUp;
			window.KeyPress += Window_KeyPress;
		}
		
		private void Window_KeyDown(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
		{
			OnKeyDown(e.Key, e.Modifiers);
		}

		private void Window_KeyUp(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
		{
			OnKeyUp(e.Key, e.Modifiers);
		}
		
		private void Window_KeyPress(object sender, KeyPressEventArgs e)
		{
			OnKeyPress(e.KeyChar);
		}

		private GraphicsMode CreateGraphicsMode(int samples)
		{
			GraphicsMode defaultMode = GraphicsMode.Default;
			return new GraphicsMode(defaultMode.ColorFormat, defaultMode.Depth, defaultMode.Stencil, samples, defaultMode.AccumulatorFormat, defaultMode.Buffers, defaultMode.Stereo);
		}

		private void Window_Load(object sender, EventArgs e)
		{
			Load();
		}

		private void Window_Resize(object sender, EventArgs e)
		{
			Resize(window.ClientSize.Width, window.ClientSize.Height);
		}

		private void Window_UpdateFrame(object sender, FrameEventArgs e)
		{
			Update((float)e.Time);

            previousKbState = currentKbState;
            currentKbState = window.Keyboard.GetState();
        }

		private void Window_RenderFrame(object sender, FrameEventArgs e)
		{
			Render((float)e.Time);
			window.SwapBuffers();
		}

		public void Run()
		{
			window.Run(120);
		}
	}
}
