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
		private static Vector3[] points = new Vector3[]
		{
				new Vector3(-1, MathConstants.GoldenRatio, 0),
				new Vector3(1, MathConstants.GoldenRatio, 0),
				new Vector3(-1, -MathConstants.GoldenRatio, 0),
				new Vector3(1, -MathConstants.GoldenRatio, 0),
				new Vector3(0, -1, MathConstants.GoldenRatio),
				new Vector3(0, 1, MathConstants.GoldenRatio),
				new Vector3(0, -1, -MathConstants.GoldenRatio),
				new Vector3(0, 1, -MathConstants.GoldenRatio),
				new Vector3(MathConstants.GoldenRatio, 0, -1),
				new Vector3(MathConstants.GoldenRatio, 0, 1),
				new Vector3(-MathConstants.GoldenRatio, 0, -1),
				new Vector3(-MathConstants.GoldenRatio, 0, 1)
		};

		public static Triangle[] Generate()
		{
			List<Triangle> triangles = new List<Triangle>();

			void AddTriangle(int x, int y, int z)
			{
				triangles.Add(new Triangle(points[x], points[y], points[z]));
			}

			AddTriangle(2, 4, 11);    //1
			AddTriangle(11, 10, 2);   //2
			AddTriangle(6, 2, 10);    //3
			AddTriangle(3, 2, 6);     //4
			AddTriangle(3, 4, 2);     //5
			AddTriangle(3, 9, 4);     //6
			AddTriangle(4, 9, 5);     //7
			AddTriangle(5, 11, 4);    //8
			AddTriangle(0, 11, 5);    //9
			AddTriangle(0, 10, 11);   //10
			AddTriangle(0, 7, 10);    //11
			AddTriangle(10, 7, 6);    //12
			AddTriangle(8, 6, 7);     //13
			AddTriangle(3, 6, 8);     //14
			AddTriangle(3, 8, 9);     //15
			AddTriangle(9, 8, 1);     //16
			AddTriangle(1, 5, 9);     //17
			AddTriangle(0, 5, 1);     //18
			AddTriangle(0, 1, 7);     //19
			AddTriangle(7, 1, 8);     //20

			return triangles.ToArray();
		}
	}
}
