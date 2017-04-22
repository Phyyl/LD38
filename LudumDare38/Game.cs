using LudumDare38.Graphics;
using LudumDare38.Shapes;
using Newtonsoft.Json;
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

        private Pos characterPos;

        int currentLevel;
        Level[] levels;

		public Game()
		{
			InitializeWindow();
		}

		private void Load()
		{
			Shaders.InitializeShaders();

			renderContext = new RenderContext();

			triangles = IcoSphereGenerator.Generate();

            // character Start position determined in the map file
            characterPos = new Pos { X = 0, Y = 0 };

            levels = JsonConvert.DeserializeObject<Level[]>(Resources.ResourceManager.LoadEmbedded<string>("Resources/levels.json"));
        }

		private void Resize(int width, int height)
		{
			GL.Viewport(0, 0, width, height);

			renderContext.ViewMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver6, (float)width / height, 0.01f, 100f);
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
	}
}
