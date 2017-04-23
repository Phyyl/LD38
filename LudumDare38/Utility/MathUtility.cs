using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Math;

namespace LudumDare38.Utility
{
	public static class MathUtility
	{
		public static int Mod(int x, int mod)
		{
			return (x % mod + mod) % mod;
		}

		public static float Mod(float x, float mod)
		{
			return (x % mod + mod) % mod;
		}

		public static Vector2 GetMinVector(Vector2 a, Vector2 b)
		{
			return new Vector2(Min(a.X, b.X), Min(a.Y, b.Y));
		}

		public static Vector3 GetMinVector(Vector3 a, Vector3 b)
		{
			return new Vector3(Min(a.X, b.X), Min(a.Y, b.Y), Min(a.Z, b.Z));
		}

		public static Vector2 GetMaxVector(Vector2 a, Vector2 b)
		{
			return new Vector2(Max(a.X, b.X), Max(a.Y, b.Y));
		}

		public static Vector3 GetMaxVector(Vector3 a, Vector3 b)
		{
			return new Vector3(Max(a.X, b.X), Max(a.Y, b.Y), Max(a.Z, b.Z));
		}
	}
}
