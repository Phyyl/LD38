using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38.Maths
{
	public static class MathConstants
	{
		public static readonly float GoldenRatio = (1 + (float)Math.Sqrt(5)) / 2;
		public static readonly float OneOverGoldenRatio = 1 / GoldenRatio;
		public static readonly float GoldenAngle = (float)Math.Atan(GoldenRatio);
		public static readonly float ReverseGoldenAngle = (float)Math.Atan(OneOverGoldenRatio);
	}
}
