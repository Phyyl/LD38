using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38.Graphics
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Vertex
	{
		public static readonly int SizeInBytes = Marshal.SizeOf<Vertex>();
		public static readonly int PositionOffset = (int)Marshal.OffsetOf<Vertex>(nameof(Position));
		public static readonly int ColorOffset = (int)Marshal.OffsetOf<Vertex>(nameof(Color));
		public static readonly int TexCoordOffset = (int)Marshal.OffsetOf<Vertex>(nameof(TexCoord));
		public static readonly int NormalOffset = (int)Marshal.OffsetOf<Vertex>(nameof(Normal));

		public Vector3 Position;
		public Vector4 Color;
		public Vector2 TexCoord;
		public Vector3 Normal;

		public Vertex(Vector3 position, Vector4 color = default(Vector4), Vector2 texCoord = default(Vector2), Vector3 normal = default(Vector3))
		{
			Position = position;
			Color = color;
			TexCoord = texCoord;
			Normal = normal;
		}

		public Vertex(Vector2 position, Vector4 color = default(Vector4), Vector2 texCoord = default(Vector2), Vector3 normal = default(Vector3))
			: this(new Vector3(position), color, texCoord, normal)
		{

		}
		
		public override string ToString()
		{
			return $"Position:{Position} Color:{Color} TexCoord:{TexCoord} Normal:{Normal}";
		}
	}
}
