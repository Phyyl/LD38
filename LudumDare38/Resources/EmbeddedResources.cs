using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace LudumDare38.Resources
{
	public static class EmbeddedResources
	{
		private static string GetResourcePath(string path, Assembly assembly)
		{
			return assembly.GetName().Name + "." + path.Replace('/', '.');
		}

		public static Stream GetStream(string path, Assembly assembly = null)
		{
			try
			{
				assembly = (assembly ?? Assembly.GetExecutingAssembly());
				return assembly.GetManifestResourceStream(GetResourcePath(path, assembly));
			}
			catch
			{
				//TODO: Log for embedded resource not found
				return null;
			}
		}

		public static string ReadAllText(string path, Assembly assembly = null)
		{
			using (Stream stream = GetStream(path, assembly))
			{
				if (stream == null)
				{
					return null;
				}

				return stream.ReadAllText();
			}
		}

		public static string[] ReadAllLines(string path, Assembly assembly = null)
		{
			using (Stream stream = GetStream(path, assembly))
			{
				if (stream == null)
				{
					return null;
				}

				return stream.ReadAllLines();
			}
		}

		public static byte[] ReadAllBytes(string path, Assembly assembly = null)
		{
			using (Stream stream = GetStream(path, assembly))
			{
				if (stream == null)
				{
					return null;
				}

				return stream.ReadAllBytes();
			}
		}
	}
}
