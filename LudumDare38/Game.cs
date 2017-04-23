using LudumDare38.Graphics;
using LudumDare38.Maths;
using LudumDare38.Resources;
using LudumDare38.Shapes;
using LudumDare38.Utility;
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
		private const float menuMoveSpeed = 5;

		private RenderContext renderContext3D;
		private RenderContext renderContext2D;

		private World world;
		private GameState state;

		private Level[] levels;

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

			world = new World();
			levels = JsonConvert.DeserializeObject<Level[]>(ResourceManager.LoadEmbedded<string>("Resources/levels.json"));

			state.Initialize();
		}

		private void Resize(int width, int height)
		{
			GL.Viewport(0, 0, width, height);

			renderContext3D.ViewMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver6, (float)width / height, 0.01f, 100f);
			renderContext2D.ViewMatrix = Matrix4.CreateOrthographicOffCenter(0, width, height, 0, 1, -1);
		}

		private void OnKeyDown(Key key, KeyModifiers modifiers)
		{
			if (state.InLevel)
			{
				if (key == Key.Escape) state.InLevel = false;
			}
			else
			{
				if (key == Key.Enter) state.InLevel = true;
				if (key == Key.A) state.MenuSelectedX -= 2 - (state.MenuSelectedY % 2);
				if (key == Key.D) state.MenuSelectedX += 2 - (state.MenuSelectedY % 2);
				if (key == Key.W && (state.MenuSelectedY == 0 || state.MenuSelectedX % 2 != 0)) state.MenuSelectedY++;
				if (key == Key.S && (state.MenuSelectedY == 2 || state.MenuSelectedX % 2 == 0)) state.MenuSelectedY--;

				state.MenuSelectedY = MathHelper.Clamp(state.MenuSelectedY, 0, 2);
				state.MenuSelectedX = MathUtility.Mod(state.MenuSelectedX, 10);

				state.UpdateAngles();
			}
		}

		private void OnKeyUp(Key key, KeyModifiers modifiers)
		{

		}

		private void OnKeyPress(char keyChar)
		{

		}

		private void Update(float delta)
		{
			//if (levels[currentLevel].Map[characterPos.X, characterPos.Y] != 100)
			//{
			//	MoveCharacter();
			//}

			state.Update(delta);
		}

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

			int selectedIndex = state.GetSelectedIndex();

			for (int i = 0; i < 20; i++)
			{
				renderContext3D.Push();

				renderContext3D.RotateZ(state.MenuTargetAngle.Z);
				renderContext3D.RotateY(state.MenuTargetAngle.Y);
				renderContext3D.RotateX(state.MenuTargetAngle.X);

				renderContext3D.Translate(state.MenuPosition);

				if (i == selectedIndex)
				{
					renderContext3D.Translate(new Vector3(0, 0, 4 * state.LevelRenderDistance));
				}

				WorldMap worldMap = world.WorldMaps[i];
				DrawTriangle(world.WorldMaps[i].Triangle, worldMap.Color);

				renderContext3D.Pop();
			}
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

		private void MoveCharacter()
		{
			//Pos pos = characterPos;
			//int[,] map = levels[currentLevel].Map;

			//// some movement have priority over other if pressed at the same time cuz im that bad
			//if (IsDownPressed)
			//{
			//	if (pos.Y < 8
			//		&& ((pos.X % 2 == 0 && pos.Y % 2 != 0)
			//		|| (pos.X % 2 != 0 && pos.Y % 2 == 0))
			//		&& map[pos.X, pos.Y + 1] != 0)
			//	{
			//		characterPos.Y++;
			//	}
			//}
			//else if (IsLeftPressed)
			//{
			//	if (pos.X > 0 && map[pos.X - 1, pos.Y] != 0)
			//	{
			//		characterPos.X--;
			//	}
			//}
			//else if (IsRightPressed)
			//{
			//	if (pos.X < 15 - 1 && map[pos.X + 1, pos.Y] != 0)
			//	{
			//		characterPos.X++;
			//	}
			//}
			//else if (IsUpPressed)
			//{
			//	if (pos.X > 0
			//		&& ((pos.X % 2 == 0 && pos.Y % 2 == 0)
			//		|| (pos.X % 2 != 0 && pos.Y % 2 != 0))
			//		&& map[pos.X, pos.Y - 1] != 0)
			//	{
			//		characterPos.Y--;
			//	}
			//}
		}
	}
}
