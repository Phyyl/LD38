using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38
{
	public static class IOExtensions
	{
		public static void WriteLengthPrefixed(this BinaryWriter writer, byte[] data)
		{
			writer.Write(data.Length);
			writer.Write(data);
		}

		public static byte[] ReadLengthPrefixed(this BinaryReader reader)
		{
			int length = reader.ReadInt32();
			return reader.ReadBytes(length);
		}

		public static string[] ReadAllLines(this Stream stream)
		{
			List<string> result = new List<string>();

			using (StreamReader reader = new StreamReader(stream))
			{
				string line;

				while ((line = reader.ReadLine()) != null)
				{
					result.Add(line);
				}

				return result.ToArray();
			}
		}

		public static string ReadAllText(this Stream stream)
		{
			StreamReader reader = new StreamReader(stream);
			return reader.ReadToEnd();
		}

		public static byte[] ReadAllBytes(this Stream stream)
		{
			byte[] result = new byte[stream.Length];
			stream.Read(result, 0, result.Length);
			return result;
		}

		public static Guid ReadGuid(this BinaryReader reader)
		{
			return new Guid(reader.ReadBytes(16));
		}

		public static void Write(this BinaryWriter writer, Guid guid)
		{
			writer.Write(guid.ToByteArray());
		}
	}
}
