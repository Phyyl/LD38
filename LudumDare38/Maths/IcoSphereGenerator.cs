using LudumDare38.Shapes;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38.Maths
{
	public static class IcoSphereGenerator
	{
		private const float t = 1.618034f;

		private static Vector3[] points = new Vector3[]
		{
				new Vector3(-1, t, 0),
				new Vector3(1, t, 0),
				new Vector3(-1, -t, 0),
				new Vector3(1, -t, 0),
				new Vector3(0, -1, t),
				new Vector3(0, 1, t),
				new Vector3(0, -1, -t),
				new Vector3(0, 1, -t),
				new Vector3(t, 0, -1),
				new Vector3(t, 0, 1),
				new Vector3(-t, 0, -1),
				new Vector3(-t, 0, 1)
		};

		public static Triangle[] Generate()
		{
			List<Triangle> triangles = new List<Triangle>();

			void AddTriangle(int x, int y, int z)
			{
				triangles.Add(new Triangle(points[x], points[y], points[z]));
			}

			AddTriangle(0, 11, 5);
			AddTriangle(0, 5, 1);
			AddTriangle(0, 1, 7);
			AddTriangle(0, 7, 10);
			AddTriangle(0, 10, 11);
			AddTriangle(1, 5, 9);
			AddTriangle(5, 11, 4);
			AddTriangle(11, 10, 2);
			AddTriangle(10, 7, 6);
			AddTriangle(7, 1, 8);
			AddTriangle(3, 9, 4);
			AddTriangle(3, 4, 2);
			AddTriangle(3, 2, 6);
			AddTriangle(3, 6, 8);
			AddTriangle(3, 8, 9);
			AddTriangle(4, 9, 5);
			AddTriangle(2, 4, 11);
			AddTriangle(6, 2, 10);
			AddTriangle(8, 6, 7);
			AddTriangle(9, 8, 1);
			
			return triangles.ToArray();
		}
	}
}
