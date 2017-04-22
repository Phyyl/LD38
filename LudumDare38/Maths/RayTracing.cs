using LudumDare38.Shapes;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38.Maths
{
	public class RayTracing
	{
		public static bool Intersect(Ray ray, Triangle triangle, out Vector3 position)
		{
			Vector3 pq = ray.End - ray.Start;
			Vector3 pa = triangle.A - ray.Start;
			Vector3 pb = triangle.B - ray.Start;
			Vector3 pc = triangle.C - ray.Start;

			position = new Vector3();

			position.X = VectorMath.ScalarTripleProduct(pq, pc, pb);

			if (position.X < 0) return false;

			position.Y = VectorMath.ScalarTripleProduct(pq, pa, pc);

			if (position.Y < 0) return false;

			position.Z = VectorMath.ScalarTripleProduct(pq, pb, pa);

			if (position.Z < 0) return false;

			float denom = 1f / (position.X + position.Y + position.Z);

			position *= denom;

			return true;
		}
	}
}
