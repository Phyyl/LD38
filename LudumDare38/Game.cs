using LudumDare38.Graphics;
using LudumDare38.Maths;
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

		private void Render(float delta)
		{
			renderContext.Clear();

			renderContext.EnableDepth();
			renderContext.EnableTransparency();

			renderContext.LoadIdentity();
			renderContext.RotateY(angle += delta);
			renderContext.Translate(0, 0, -10);

			foreach (var triangle in triangles)
			{
				DrawTriangle(triangle);
			}
		}

		private void DrawTriangle(Triangle triangle)
		{
			Shaders.LitPrimitives.Use();
			Shaders.LitPrimitives.Begin(renderContext.GetMatrices(), new Vector3(0, 0, -1).Normalized());

			Vector4 color = Color4.Red.ToVector();
			Vector3 normal = triangle.GetNormal();

			renderContext.BeginDrawArrays(new Vertex[]
			{
				new Vertex(triangle.A, color, normal: normal),
				new Vertex(triangle.B, color, normal: normal),
				new Vertex(triangle.C, color, normal: normal)
			});

			renderContext.DrawArrays(PrimitiveType.Triangles);
		}
	}
}
