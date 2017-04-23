using LudumDare38.Shapes;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38
{
	public class WorldMap
	{
		public Triangle Triangle { get; private set; }
		public Color4 Color { get; private set; }
		public int Index { get; private set; }

		public WorldMap(int index, Triangle triangle, Color4 color)
		{
			Triangle = triangle;
			Color = color;
			Index = index;
		}
	}
}
