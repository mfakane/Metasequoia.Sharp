using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Metasequoia
{
	public class ReadOnlyIndexer<TKey, TValue>
	{
		readonly Func<TKey, TValue> getter;

		public ReadOnlyIndexer(Func<TKey, TValue> getter)
		{
			this.getter = getter;
		}

		public TValue this[TKey key]
		{
			get
			{
				return getter(key);
			}
		}
	}

	public class ReadOnlyIndexer<T> : ReadOnlyIndexer<int, T>, IEnumerable<T>
		where T: class
	{
		readonly Func<int> count;

		public ReadOnlyIndexer(Func<int, T> getter, Func<int> count)
			: base(getter)
		{
			this.count = count;
		}

		public int Count
		{
			get
			{
				return count();
			}
		}

		public IEnumerator<T> GetEnumerator()
		{
			return Enumerable.Range(0, this.Count)
							 .Select(_ => this[_])
							 .Where(_ => _ != null)
							 .GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
