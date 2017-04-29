using System;

namespace Cross
{
	public class DynamicArray<T>
	{
		protected T[] m_data = null;

		protected int m_size = 0;

		protected int m_capcity = 0;

		public bool isFixedSize
		{
			get
			{
				return false;
			}
		}

		public bool isReadOnly
		{
			get
			{
				return false;
			}
		}

		public int capcity
		{
			get
			{
				return this.m_capcity;
			}
			set
			{
				bool flag = this.m_capcity >= value;
				if (!flag)
				{
					T[] array = new T[value];
					bool flag2 = this.m_data != null;
					if (flag2)
					{
						this.m_data.CopyTo(array, 0);
					}
					this.m_data = array;
					this.m_capcity = value;
				}
			}
		}

		public virtual int length
		{
			get
			{
				return this.m_size;
			}
			set
			{
				bool flag = this.capcity < value;
				if (flag)
				{
					this.capcity = value;
				}
				this.m_size = value;
			}
		}

		public T[] data
		{
			get
			{
				return this.m_data;
			}
		}

		public T this[int index]
		{
			get
			{
				bool flag = index >= this.m_size;
				T result;
				if (flag)
				{
					result = default(T);
				}
				else
				{
					result = this.m_data[index];
				}
				return result;
			}
			set
			{
				bool flag = index >= this.m_size;
				if (!flag)
				{
					this.m_data[index] = value;
				}
			}
		}

		public bool isSynchronized
		{
			get
			{
				return false;
			}
		}

		public object syncRoot
		{
			get
			{
				return this;
			}
		}

		public T front
		{
			get
			{
				bool flag = this.m_size <= 0;
				T result;
				if (flag)
				{
					result = default(T);
				}
				else
				{
					result = this.m_data[0];
				}
				return result;
			}
		}

		public T back
		{
			get
			{
				bool flag = this.m_size <= 0;
				T result;
				if (flag)
				{
					result = default(T);
				}
				else
				{
					result = this.m_data[this.m_size - 1];
				}
				return result;
			}
		}

		public DynamicArray()
		{
			this.capcity = 32;
		}

		public DynamicArray(int cap)
		{
			this.capcity = cap;
		}

		public void clear()
		{
			this.m_data = null;
			this.m_size = 0;
			this.m_capcity = 0;
		}

		public void copyTo(T[] array, int index)
		{
			bool flag = array == null || index < 0;
			if (!flag)
			{
				bool flag2 = index + this.m_size > array.Length;
				if (!flag2)
				{
					this.m_data.CopyTo(array, index);
				}
			}
		}

		public void add(T v)
		{
			this.pushBack(v);
		}

		public bool contains(T v)
		{
			return this.indexOf(v) >= 0;
		}

		public int indexOf(T v)
		{
			return Array.IndexOf<T>(this.m_data, v);
		}

		public void insert(int index, T value)
		{
		}

		public bool remove(T v)
		{
			bool flag = this.m_size <= 0;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				int num = Array.IndexOf<T>(this.m_data, v);
				bool flag2 = num < 0;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = num == this.m_size - 1;
					if (flag3)
					{
						this.m_size--;
						result = true;
					}
					else
					{
						Array.Copy(this.m_data, num + 1, this.m_data, num, this.m_size - num - 1);
						this.m_size--;
						result = true;
					}
				}
			}
			return result;
		}

		public void removeAt(int index)
		{
			bool flag = this.m_size <= 0 || index >= this.m_size;
			if (!flag)
			{
				Array.Copy(this.m_data, index + 1, this.m_data, index, this.m_size - index - 1);
				this.m_size--;
			}
		}

		public void pushBack(T v)
		{
			bool flag = this.m_capcity < this.m_size + 1;
			if (flag)
			{
				this.capcity *= 2;
			}
			T[] arg_3E_0 = this.m_data;
			int size = this.m_size;
			this.m_size = size + 1;
			arg_3E_0[size] = v;
		}

		public void pushBack(T[] v, int count)
		{
			int i;
			for (i = this.m_capcity; i < this.m_size + count; i *= 2)
			{
			}
			this.capcity = i;
			for (int j = 0; j < count; j++)
			{
				T[] arg_4A_0 = this.m_data;
				int size = this.m_size;
				this.m_size = size + 1;
				arg_4A_0[size] = v[j];
			}
		}

		public T popBack()
		{
			bool flag = this.m_size <= 0;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				T[] arg_35_0 = this.m_data;
				int size = this.m_size;
				this.m_size = size - 1;
				result = arg_35_0[size];
			}
			return result;
		}

		public T popFront()
		{
			bool flag = this.m_size <= 0;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				T t = this.m_data[0];
				Array.Copy(this.m_data, 1, this.m_data, 0, this.m_size - 1);
				this.m_size--;
				result = t;
			}
			return result;
		}

		public void skip(int count)
		{
			bool flag = count > this.m_size;
			if (flag)
			{
				this.m_size = 0;
			}
			else
			{
				Array.Copy(this.m_data, count, this.m_data, 0, this.m_size - count);
				this.m_size -= count;
			}
		}
	}
}
