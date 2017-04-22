using LudumDare38.Resources;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38.Graphics
{
	public static class Textures
	{
		private const int itemTileSize = 32;

		public static Texture Coin { get; private set; }
		public static Texture Double { get; private set; }
		public static Texture Drop { get; private set; }
		public static Texture Teleport { get; private set; }

		public static void Initialize()
		{
			Bitmap items = ResourceManager.LoadEmbedded<Bitmap>("Resources/items.png");
			
			//Coin = Texture.LoadFromBitmap(items);
			//Double = Texture.LoadFromBitmap(items);
			//Drop = Texture.LoadFromBitmap(items);
			//Teleport = Texture.LoadFromBitmap(items);
			
			Coin = Texture.LoadFromBitmap(items, GetItemTile(0, 0));
			Double = Texture.LoadFromBitmap(items, GetItemTile(1, 0));
			Drop = Texture.LoadFromBitmap(items, GetItemTile(2, 0));
			Teleport = Texture.LoadFromBitmap(items, GetItemTile(3, 0));
		}

		private static Rectangle GetItemTile(int x, int y)
		{
			return new Rectangle(x, y, 1, 1).Scaled(itemTileSize);
		}
	}
}
