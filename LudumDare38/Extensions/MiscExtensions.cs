using System;

namespace LudumDare38
{
	public static class MiscExtensions
	{
		public static T[] SubArray<T>(this T[] array, int start, int length)
		{
			T[] result = new T[length];
			Array.Copy(array, start, result, 0, length);
			return result;
		}
	}
}
