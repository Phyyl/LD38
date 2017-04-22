using LudumDare38.Graphics;
using LudumDare38.Maths;
using LudumDare38.Shapes;
using Newtonsoft.Json;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38
{
	public partial class Game
	{
		private RenderContext renderContext3D;
		private RenderContext renderContext2D;

		Triangle[] triangles;

        private Pos characterPos;

        int currentLevel;
        Level[] levels;

		public Game()
		{
			InitializeWindow();
		}

		private void Load()
		{
			Shaders.Initialize();
			Textures.Initialize();

			renderContext3D = new RenderContext();
			renderContext2D = new RenderContext();

			triangles = IcoSphereGenerator.Generate();

            // character Start position determined in the map file
            characterPos = new Pos { X = 0, Y = 0 };

            levels = JsonConvert.DeserializeObject<Level[]>(Resources.ResourceManager.LoadEmbedded<string>("Resources/levels.json"));
        }

		private void Resize(int width, int height)
		{
			GL.Viewport(0, 0, width, height);

			renderContext3D.ViewMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver6, (float)width / height, 0.01f, 100f);
			renderContext2D.ViewMatrix = Matrix4.CreateOrthographicOffCenter(0, width, height, 0, 1, -1);
		}

		private void Update(float delta)
		{
            // if level is not done
            if (levels[currentLevel].Map[characterPos.X, characterPos.Y] != 100 /*&& in a level*/)
            {
                MoveCharacter(); ;
            }
		}

		float angle;
		Random random = new Random();

		private void Render(float delta)
		{
			renderContext3D.Clear();

			renderContext3D.EnableTransparency();
			renderContext3D.EnableDepth();
			Render3D(delta);

			renderContext2D.DisableDepth();
			RenderUI(delta);
		}

		private void Render3D(float delta)
		{
			renderContext3D.LoadIdentity();
			renderContext3D.RotateY(angle += delta);
			renderContext3D.Translate(0, 0, -10);

			foreach (var triangle in triangles)
			{
				DrawTriangle(triangle, Color4.Red);
			}
		}
        
        private void MoveCharacter()
        {
            Pos pos = characterPos;
            int[,] map = levels[currentLevel].Map;

            // some movement have priority over other if pressed at the same time cuz im that bad
            if (IsDownPressed)
            {
                if (pos.Y < 8
                    && ((pos.X % 2 == 0 && pos.Y % 2 != 0)
                    || (pos.X % 2 != 0 && pos.Y % 2 == 0))
                    && map[pos.X, pos.Y + 1] != 0)
                {
                    characterPos.Y++;
                }
            }
            else if (IsLeftPressed)
            {
                if (pos.X > 0 && map[pos.X - 1, pos.Y] != 0)
                {
                    characterPos.X--;
                }
            }
            else if (IsRightPressed)
            {
                if (pos.X < 15 - 1 && map[pos.X + 1, pos.Y] != 0)
                {
                    characterPos.X++;
                }
            }
            else if (IsUpPressed)
            {
                if (pos.X > 0 
                    && ((pos.X % 2 == 0 && pos.Y % 2 == 0) 
                    || (pos.X % 2 != 0 && pos.Y % 2 != 0)) 
                    && map[pos.X, pos.Y - 1] != 0)
                {
                    characterPos.Y--;
                }
            }
        }

        struct Pos
        {
            public int X;
            public int Y;
        }

		private void RenderUI(float delta)
		{

		}

		private void DrawTriangle(Triangle triangle, Color4 color)
		{
			Shaders.LitPrimitives.Use();
			Shaders.LitPrimitives.Begin(renderContext3D.GetMatrices(), new Vector3(0, 0, -1).Normalized());

			Vector4 colorVector = color.ToVector();
			Vector3 normal = triangle.GetNormal();

			renderContext3D.BeginDrawArrays(new Vertex[]
			{
				new Vertex(triangle.A, colorVector, normal: normal),
				new Vertex(triangle.B, colorVector, normal: normal),
				new Vertex(triangle.C, colorVector, normal: normal)
			});

			renderContext3D.DrawArrays(PrimitiveType.Triangles);
		}
	}
}
