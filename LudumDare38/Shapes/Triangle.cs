using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38.Shapes
{
	[StructLayout(LayoutKind.Sequential)]
	public struct Triangle
	{
		public Vector3 A;
		public Vector3 B;
		public Vector3 C;

		public Triangle(Vector3 a, Vector3 b, Vector3 c)
		{
			A = a;
			B = b;
			C = c;
		}

		public Vector3 GetNormal()
		{
			Vector3 u = B - A;
			Vector3 v = C - A;

			return new Vector3((u.Y * v.Z) - (u.Z * v.Y), (u.Z * v.X) - (u.X * v.Z), (u.X * v.Y) - (u.Y * v.X)).Normalized();
		}
	}
}
