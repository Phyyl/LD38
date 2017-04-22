using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace LudumDare38.Graphics
{
	public partial class Texture
	{
		internal static TextureLoaderSettings LoaderSettings { get; } = new TextureLoaderSettings();
		
		internal static Texture LoadFromStream(Stream stream)
		{
			Bitmap bitmap = new Bitmap(stream);

			Texture result = new Texture()
			{
				Width = bitmap.Width,
				Height = bitmap.Height
			};

			BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			result.Bind();

			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
			
			if (LoaderSettings.LODBias < 0)
			{
				GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureLodBias, LoaderSettings.LODBias);
			}

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)LoaderSettings.MagFilter);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)LoaderSettings.MinFilter);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)LoaderSettings.TextureWrapS);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)LoaderSettings.TextureWrapT);

			bitmap.UnlockBits(data);

			Unbind();

			return result;
		}

		internal class TextureLoaderSettings
		{
			public const TextureMagFilter DefaultMagFilter = TextureMagFilter.Linear;
			public const TextureMinFilter DefaultMinFilter = TextureMinFilter.Linear;
			public const TextureWrapMode DefaultTextureWrapS = TextureWrapMode.ClampToEdge;
			public const TextureWrapMode DefaultTextureWrapT = TextureWrapMode.ClampToEdge;

			public TextureMagFilter MagFilter { get; set; }
			public TextureMinFilter MinFilter { get; set; }
			public TextureWrapMode TextureWrapS { get; set; }
			public TextureWrapMode TextureWrapT { get; set; }
			public float LODBias { get; set; }

			public TextureLoaderSettings()
			{
				Reset();
			}

			public void Reset()
			{
				Set();
			}

			public void Set(TextureMagFilter magFilter = DefaultMagFilter, TextureMinFilter minFilter = DefaultMinFilter, TextureWrapMode wrapS = DefaultTextureWrapS, TextureWrapMode wrapT = DefaultTextureWrapT, int lodBias = 0)
			{
				MagFilter = magFilter;
				MinFilter = minFilter;
				TextureWrapS = wrapS;
				TextureWrapT = wrapT;
				LODBias = lodBias;
			}
		}
	}
}
