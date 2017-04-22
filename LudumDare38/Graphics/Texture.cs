using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;

namespace LudumDare38.Graphics
{
	public partial class Texture
	{
		public int TextureID { get; private set; }
		public int Width { get; private set; }
		public int Height { get; private set; }

		public Rectangle Bounds => new Rectangle(0, 0, Width, Height);
		public Vector2 Size => new Vector2(Width, Height);
		       
		public Texture()
		{
			TextureID = GL.GenTexture();
		}

		public Texture(int width, int height)
			: this()
		{
			SetData(null, width, height);
		}

		public void SetData(IntPtr data, int width, int height)
		{
			BeginSetData(width, height);
			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, data);
			EndSetData();
		}

		public void SetData(byte[] data, int width, int height)
		{
			BeginSetData(width, height);
			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, data);
			EndSetData();
		}

		private void BeginSetData(int width, int height)
		{
			Bind();

			Width = width;
			Height = height;
		}

		private void EndSetData()
		{
			Unbind();
		}

		public void Bind()
		{
			GL.BindTexture(TextureTarget.Texture2D, TextureID);
		}

		public static void Unbind()
		{
			GL.BindTexture(TextureTarget.Texture2D, 0);
		}
	}
}
