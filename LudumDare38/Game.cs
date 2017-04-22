using LudumDare38.Shapes;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38
{
	public partial class Game
	{
		private RenderContext renderContext;

		Triangle[] triangles;

		public Game()
		{
			InitializeWindow();
		}

		private void Load()
		{
			Shaders.InitializeShaders();

			renderContext = new RenderContext();

			triangles = IcoSphereGenerator.Generate();
		}

		private void Resize(int width, int height)
		{
			GL.Viewport(0, 0, width, height);

			renderContext.ViewMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver6, (float)width / height, 0.01f, 100f);
		}

		private void Update(float delta)
		{

		}

		float angle;
		Random random = new Random();

		private void Render()
		{
			renderContext.Clear();

			renderContext.EnableDepth();
			renderContext.EnableTransparency();

			renderContext.LoadIdentity();
			renderContext.RotateY(angle += 0.001f);
			renderContext.Translate(0, 0, -10);

			foreach (var triangle in triangles)
			{
				Color4 color = Color4.Red;
				
				renderContext.DrawTriangle(triangle.A, triangle.B, triangle.C, color);
			}
		}
	}
}
