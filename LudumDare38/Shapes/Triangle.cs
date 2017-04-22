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
	}
}
