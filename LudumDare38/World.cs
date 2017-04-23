using LudumDare38.Maths;
using LudumDare38.Shapes;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38
{
	public class World
	{
		public WorldMap[] WorldMaps { get; private set; }

		public World()
		{
			Triangle[] triangles = IcoSphereGenerator.Generate();

			WorldMaps = new WorldMap[20];

			for (int i = 0; i < WorldMaps.Length; i++)
			{
				float f = i / 20f;

				Color4 color = new Color4(f, 1 - f, 0, 1f);

				WorldMaps[i] = new WorldMap(i, triangles[i], color);
			}
		}
	}
}
