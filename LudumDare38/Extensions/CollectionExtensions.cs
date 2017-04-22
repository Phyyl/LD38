using System.Collections.Generic;

namespace LudumDare38
{
	public static class CollectionExtensions
	{
		public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue defaultValue = default(TValue))
		{
			if (dict.ContainsKey(key))
			{
				return dict[key];
			}

			return defaultValue;
		}

		public static void AddOrSet<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
		{
			if (dict.ContainsKey(key))
			{
				dict[key] = value;
			}
			else
			{
				dict.Add(key, value);
			}
		}
	}
}
