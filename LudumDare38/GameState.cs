using LudumDare38.Maths;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38
{
	public struct GameState
	{
		private const float renderDistanceChangeSpeed = 10;

		public int MenuSelectedX;
		public int MenuSelectedY;
		public bool InLevel;

		public float LevelRenderDistance;

		public Vector3 MenuCurrentAngle;
		public Vector3 MenuTargetAngle;
		public Vector3 MenuPosition;
		
		public void Initialize()
		{
			MenuPosition.Z = -10;

			UpdateAngles();
			MenuCurrentAngle = MenuTargetAngle;
		}

		public void Update(float delta)
		{
			float renderDistanceDelta = (float)((Math.Cos(2 * Math.PI * (LevelRenderDistance + 0.5f)) + 1.2f) / 2.4f) * delta * renderDistanceChangeSpeed;

			if (InLevel)
			{
				if (LevelRenderDistance < 1)
				{
					LevelRenderDistance += renderDistanceDelta;
				}
			}
			else
			{
				if (LevelRenderDistance > 0)
				{
					LevelRenderDistance -= renderDistanceDelta;
				}
			}

			LevelRenderDistance = MathHelper.Clamp(LevelRenderDistance, 0, 1);
		}

		public void UpdateAngles()
		{
			MenuTargetAngle.Z = MathConstants.ReverseGoldenAngle;
			MenuTargetAngle.Y = (-MenuSelectedX / 10f) * MathHelper.TwoPi + MathHelper.Pi / 10;
			MenuTargetAngle.X = MathConstants.ReverseGoldenAngle * (MenuSelectedY - 1) * 2;
		}

		public int GetSelectedIndex()
		{
			if (MenuSelectedY == 0)
			{
				return ((4 - (MenuSelectedX / 2)) + 1) % 5;
			}
			else if (MenuSelectedY == 1)
			{
				return (((9 - MenuSelectedX) + 3) % 10) + 5;
			}

			return (((4 - (MenuSelectedX / 2 + 1)) + 3) % 5) + 15;
		}
	}
}
