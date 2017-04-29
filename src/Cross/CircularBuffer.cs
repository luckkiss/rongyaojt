using System;
using System.Collections;
using System.Collections.Generic;

namespace Cross
{
	public class CircularBuffer<T> : IList<T>, IEnumerable, ICollection<T>, IEnumerable<T>
	{
		private T[] _buffer;

		private int _position;

		private long _version;

		public T this[int index]
		{
			get
			{
				bool flag = index < 0 || index >= this.Count;
				if (flag)
				{
					throw new IndexOutOfRangeException();
				}
				int num = (this._position - this.Count + index) % this.Capacity;
				return this._buffer[num];
			}
			set
			{
				this.Insert(index, value);
			}
		}

		public int Capacity
		{
			get;
			private set;
		}

		public int Count
		{
			get;
			private set;
		}

		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		public CircularBuffer(int capacity)
		{
			bool flag = capacity <= 0;
			if (flag)
			{
				throw new ArgumentException("Must be greater than zero", "capacity");
			}
			this.Capacity = capacity;
			this._buffer = new T[capacity];
		}

		public void Add(T item)
		{
			T[] arg_20_0 = this._buffer;
			int num = this._position;
			this._position = num + 1;
			arg_20_0[num % this.Capacity] = item;
			bool flag = this.Count < this.Capacity;
			if (flag)
			{
				num = this.Count;
				this.Count = num + 1;
			}
			this._version += 1L;
		}

		public void Clear()
		{
			for (int i = 0; i < this.Count; i++)
			{
				this._buffer[i] = default(T);
			}
			this._position = 0;
			this.Count = 0;
			this._version += 1L;
		}

		public bool Contains(T item)
		{
			int num = this.IndexOf(item);
			return num != -1;
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			for (int i = 0; i < this.Count; i++)
			{
				array[i + arrayIndex] = this._buffer[(this._position - this.Count + i) % this.Capacity];
			}
		}

		public IEnumerator<T> GetEnumerator()
		{
			long version = this._version;
			int num;
			for (int i = 0; i < this.Count; i = num + 1)
			{
				bool flag = version != this._version;
				if (flag)
				{
					throw new InvalidOperationException("Collection changed");
				}
				yield return this[i];
				num = i;
			}
			yield break;
		}

		public int IndexOf(T item)
		{
			int i = 0;
			int result;
			while (i < this.Count)
			{
				T t = this._buffer[(this._position - this.Count + i) % this.Capacity];
				bool flag = item == null && t == null;
				if (flag)
				{
					result = i;
				}
				else
				{
					bool flag2 = item != null && item.Equals(t);
					if (!flag2)
					{
						i++;
						continue;
					}
					result = i;
				}
				return result;
			}
			result = -1;
			return result;
		}

		public void Insert(int index, T item)
		{
			bool flag = index < 0 || index > this.Count;
			if (flag)
			{
				throw new IndexOutOfRangeException();
			}
			bool flag2 = index == this.Count;
			if (flag2)
			{
				this.Add(item);
			}
			else
			{
				int num = Math.Min(this.Count, this.Capacity - 1) - index;
				int num2 = (this._position - this.Count + index) % this.Capacity;
				for (int i = num2 + num; i > num2; i--)
				{
					int num3 = i % this.Capacity;
					int num4 = (i - 1) % this.Capacity;
					this._buffer[num3] = this._buffer[num4];
				}
				this._buffer[num2] = item;
				bool flag3 = this.Count < this.Capacity;
				if (flag3)
				{
					int count = this.Count;
					this.Count = count + 1;
					this._position++;
				}
				this._version += 1L;
			}
		}

		public bool Remove(T item)
		{
			int num = this.IndexOf(item);
			bool flag = num == -1;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				this.RemoveAt(num);
				result = true;
			}
			return result;
		}

		public void RemoveAt(int index)
		{
			bool flag = index < 0 || index >= this.Count;
			if (flag)
			{
				throw new IndexOutOfRangeException();
			}
			for (int i = index; i < this.Count - 1; i++)
			{
				int num = (this._position - this.Count + i) % this.Capacity;
				int num2 = (this._position - this.Count + i + 1) % this.Capacity;
				this._buffer[num] = this._buffer[num2];
			}
			int num3 = (this._position - 1) % this.Capacity;
			this._buffer[num3] = default(T);
			this._position--;
			int count = this.Count;
			this.Count = count - 1;
			this._version += 1L;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
