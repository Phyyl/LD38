using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38.Resources
{
	public class ResourceManager
	{
		public static T LoadEmbedded<T>(string path, Assembly assembly = null) where T : class
		{
			return LoadStream<T>(EmbeddedResources.GetStream(path, assembly ?? Assembly.GetCallingAssembly()));
		}

		public static T LoadFile<T>(string path) where T : class
		{
			return LoadStream<T>(File.OpenRead(path));
		}

		public static T LoadFileOrEmbedded<T>(string path, Assembly assembly) where T : class
		{
			return LoadStream<T>(File.OpenRead(path) ?? EmbeddedResources.GetStream(path, assembly ?? Assembly.GetCallingAssembly()));
		}

		public static T LoadEmbeddedOrFile<T>(string path, Assembly assembly) where T : class
		{
			return LoadStream<T>(EmbeddedResources.GetStream(path, assembly ?? Assembly.GetCallingAssembly()) ?? File.OpenRead(path));
		}

		private static T LoadStream<T>(Stream stream) where T : class
		{
			Type type = typeof(T);

			if (stream == null)
			{
				return null;
			}
			
			if (type == typeof(string))
			{
				return stream.ReadAllText() as T;
			}
			else if (type == typeof(string[]))
			{
				return stream.ReadAllLines() as T;
			}
			else if (type == typeof(byte[]))
			{
				return stream.ReadAllBytes() as T;
			}

			throw new InvalidOperationException($"Cannot load resource of type {type}");
		}
	}
}
