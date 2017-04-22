using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38.Maths
{
	public static class VectorMath
	{
		public static float ScalarTripleProduct(Vector3 u, Vector3 v, Vector3 w)
		{
			return Vector3.Dot(Vector3.Cross(u, v), w);
		}
	}
}
