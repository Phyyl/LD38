using OpenTK;
using OpenTK.Graphics;

namespace LudumDare38
{
	public static class GraphicsExtensions
	{
		public static Vector4 ToVector(this Color4 color)
		{
			return new Vector4(color.R, color.G, color.B, color.A);
		}

		public static Color4 ToColor(this Vector3 vector)
		{
			return new Color4(vector.X, vector.Y, vector.Z, 1);
		}

		public static Color4 ToColor(this Vector4 vector)
		{
			return new Color4(vector.X, vector.Y, vector.Z, vector.W);
		}
	}
}
