using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Metasequoia
{
	public class ReadWriteIndexer<TKey, TValue>
	{
		readonly Func<TKey, TValue> getter;
		readonly Action<TKey, TValue> setter;

		public ReadWriteIndexer(Func<TKey, TValue> getter, Action<TKey, TValue> setter)
		{
			this.getter = getter;
			this.setter = setter;
		}

		public TValue this[TKey key]
		{
			get
			{
				return getter(key);
			}
			set
			{
				setter(key, value);
			}
		}
	}

	public class ReadWriteIndexer<T> : ReadWriteIndexer<int, T>, IEnumerable<T>
	{
		readonly Func<int> count;

		public ReadWriteIndexer(Func<int, T> getter, Action<int, T> setter, Func<int> count)
			: base(getter, setter)
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
							 .GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
