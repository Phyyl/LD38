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
	public struct Ray
	{
		public Vector3 Start;
		public Vector3 End;

		public Ray(Vector3 start, Vector3 end)
		{
			Start = start;
			End = end;
		}
	}
}
