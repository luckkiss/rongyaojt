using System;
using System.Collections.Generic;

namespace Cross
{
	public class Variant
	{
		protected static LinkedList<Variant> m_pool = new LinkedList<Variant>();

		protected object m_val = null;

		public Dictionary<string, Variant>.KeyCollection Keys
		{
			get
			{
				bool flag = this.m_val == null || !(this.m_val is Dictionary<string, Variant>);
				Dictionary<string, Variant>.KeyCollection result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = (this.m_val as Dictionary<string, Variant>).Keys;
				}
				return result;
			}
		}

		public Dictionary<string, Variant>.ValueCollection Values
		{
			get
			{
				bool flag = this.m_val == null || !(this.m_val is Dictionary<string, Variant>);
				Dictionary<string, Variant>.ValueCollection result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = (this.m_val as Dictionary<string, Variant>).Values;
				}
				return result;
			}
		}

		public Dictionary<int, Variant>.KeyCollection IntKeys
		{
			get
			{
				bool flag = this.m_val == null || !(this.m_val is Dictionary<int, Variant>);
				Dictionary<int, Variant>.KeyCollection result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = (this.m_val as Dictionary<int, Variant>).Keys;
				}
				return result;
			}
		}

		public Dictionary<int, Variant>.ValueCollection IntKeyValues
		{
			get
			{
				bool flag = this.m_val == null || !(this.m_val is Dictionary<int, Variant>);
				Dictionary<int, Variant>.ValueCollection result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = (this.m_val as Dictionary<int, Variant>).Values;
				}
				return result;
			}
		}

		public int Count
		{
			get
			{
				bool flag = this.m_val == null;
				int result;
				if (flag)
				{
					result = 0;
				}
				else
				{
					bool flag2 = this.m_val is List<Variant>;
					if (flag2)
					{
						result = (this.m_val as List<Variant>).Count;
					}
					else
					{
						bool flag3 = this.m_val is string;
						if (flag3)
						{
							result = (this.m_val as string).Length;
						}
						else
						{
							bool flag4 = this.m_val is Dictionary<string, Variant>;
							if (flag4)
							{
								result = (this.m_val as Dictionary<string, Variant>).Keys.Count;
							}
							else
							{
								bool flag5 = this.m_val is Dictionary<int, Variant>;
								if (flag5)
								{
									result = (this.m_val as Dictionary<int, Variant>).Keys.Count;
								}
								else
								{
									bool flag6 = this.m_val is ByteArray;
									if (flag6)
									{
										result = (this.m_val as ByteArray).length;
									}
									else
									{
										result = 0;
									}
								}
							}
						}
					}
				}
				return result;
			}
		}

		public int Length
		{
			get
			{
				bool flag = this.m_val is List<Variant>;
				int result;
				if (flag)
				{
					result = (this.m_val as List<Variant>).Count;
				}
				else
				{
					bool flag2 = this.m_val is string;
					if (flag2)
					{
						result = (this.m_val as string).Length;
					}
					else
					{
						bool flag3 = this.m_val is ByteArray;
						if (flag3)
						{
							result = (this.m_val as ByteArray).length;
						}
						else
						{
							result = 0;
						}
					}
				}
				return result;
			}
		}

		public Variant this[Variant key]
		{
			get
			{
				bool flag = !key.isStr;
				Variant result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this[key._str];
				}
				return result;
			}
			set
			{
				bool flag = !key.isStr;
				if (!flag)
				{
					this[key._str] = value;
				}
			}
		}

		public Variant this[string key]
		{
			get
			{
				bool flag = this.m_val != null && this.m_val is Dictionary<string, Variant>;
				Variant result;
				if (flag)
				{
					Variant variant = null;
					(this.m_val as Dictionary<string, Variant>).TryGetValue(key, out variant);
					result = variant;
				}
				else
				{
					result = null;
				}
				return result;
			}
			set
			{
				bool flag = this.m_val == null;
				if (flag)
				{
					this.m_val = new Dictionary<string, Variant>();
				}
				bool flag2 = this.m_val is Dictionary<string, Variant>;
				if (flag2)
				{
					(this.m_val as Dictionary<string, Variant>)[key] = value;
				}
			}
		}

		public Variant this[int idx]
		{
			get
			{
				bool flag = this.m_val != null;
				Variant result;
				if (flag)
				{
					bool flag2 = this.m_val is List<Variant>;
					if (flag2)
					{
						bool flag3 = idx >= 0 && idx < (this.m_val as List<Variant>).Count;
						if (flag3)
						{
							result = (this.m_val as List<Variant>)[idx];
							return result;
						}
						result = null;
						return result;
					}
					else
					{
						bool flag4 = this.m_val is Dictionary<int, Variant>;
						if (flag4)
						{
							result = (this.m_val as Dictionary<int, Variant>)[idx];
							return result;
						}
					}
				}
				result = null;
				return result;
			}
			set
			{
				bool flag = this.m_val == null;
				if (flag)
				{
					this.m_val = new List<Variant>();
				}
				bool flag2 = this.m_val is List<Variant>;
				if (flag2)
				{
					List<Variant> list = this.m_val as List<Variant>;
					while (idx >= list.Count)
					{
						list.Add(null);
					}
					list[idx] = value;
				}
				else
				{
					bool flag3 = this.m_val is Dictionary<int, Variant>;
					if (flag3)
					{
						(this.m_val as Dictionary<int, Variant>)[idx] = value;
					}
				}
			}
		}

		public bool isStr
		{
			get
			{
				return this.m_val is string;
			}
		}

		public bool isDct
		{
			get
			{
				return this.m_val is Dictionary<string, Variant>;
			}
		}

		public bool isIntkeyDct
		{
			get
			{
				return this.m_val is Dictionary<int, Variant>;
			}
		}

		public bool isArr
		{
			get
			{
				return this.m_val is List<Variant>;
			}
		}

		public bool isNumber
		{
			get
			{
				return !(this.m_val is Dictionary<string, Variant>) && !(this.m_val is List<Variant>) && !(this.m_val is string) && !(this.m_val is bool) && !(this.m_val is ByteArray);
			}
		}

		public bool isInteger
		{
			get
			{
				return this.m_val is byte || this.m_val is ushort || this.m_val is uint || this.m_val is ulong || this.m_val is sbyte || this.m_val is short || this.m_val is int || this.m_val is long;
			}
		}

		public bool isSignedInteger
		{
			get
			{
				return this.m_val is sbyte || this.m_val is short || this.m_val is int || this.m_val is long;
			}
		}

		public bool isByte
		{
			get
			{
				return this.m_val is byte;
			}
		}

		public bool isUint16
		{
			get
			{
				return this.m_val is ushort;
			}
		}

		public bool isUint32
		{
			get
			{
				return this.m_val is uint;
			}
		}

		public bool isUint64
		{
			get
			{
				return this.m_val is ulong;
			}
		}

		public bool isSByte
		{
			get
			{
				return this.m_val is sbyte;
			}
		}

		public bool isInt16
		{
			get
			{
				return this.m_val is short;
			}
		}

		public bool isInt32
		{
			get
			{
				return this.m_val is int;
			}
		}

		public bool isInt64
		{
			get
			{
				return this.m_val is long;
			}
		}

		public bool isFloat
		{
			get
			{
				return this.m_val is float;
			}
		}

		public bool isDouble
		{
			get
			{
				return this.m_val is double;
			}
		}

		public bool isBool
		{
			get
			{
				return this.m_val is bool;
			}
		}

		public bool isByteArray
		{
			get
			{
				return this.m_val is ByteArray;
			}
		}

		public Dictionary<string, Variant> _dct
		{
			get
			{
				bool flag = this.m_val == null;
				if (flag)
				{
					this.m_val = new Dictionary<string, Variant>();
				}
				bool flag2 = this.m_val is Dictionary<string, Variant>;
				Dictionary<string, Variant> result;
				if (flag2)
				{
					result = (this.m_val as Dictionary<string, Variant>);
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		public Dictionary<int, Variant> _intDct
		{
			get
			{
				bool flag = this.m_val == null;
				if (flag)
				{
					this.m_val = new Dictionary<int, Variant>();
				}
				bool flag2 = this.m_val is Dictionary<int, Variant>;
				Dictionary<int, Variant> result;
				if (flag2)
				{
					result = (this.m_val as Dictionary<int, Variant>);
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		public List<Variant> _arr
		{
			get
			{
				bool flag = this.m_val == null;
				if (flag)
				{
					this.m_val = new List<Variant>();
				}
				bool flag2 = this.m_val is List<Variant>;
				List<Variant> result;
				if (flag2)
				{
					result = (this.m_val as List<Variant>);
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		public string _str
		{
			get
			{
				bool flag = this.m_val == null;
				string result;
				if (flag)
				{
					result = "";
				}
				else
				{
					bool flag2 = this.m_val is string;
					if (flag2)
					{
						result = (this.m_val as string);
					}
					else
					{
						bool flag3 = this.m_val is int || this.m_val is uint;
						if (flag3)
						{
							result = this.m_val.ToString();
						}
						else
						{
							result = "";
						}
					}
				}
				return result;
			}
			set
			{
				this.m_val = value;
			}
		}

		public ByteArray _byteAry
		{
			get
			{
				bool flag = this.m_val is ByteArray;
				ByteArray result;
				if (flag)
				{
					result = (this.m_val as ByteArray);
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		public sbyte _sbyte
		{
			get
			{
				bool flag = this.m_val is sbyte;
				sbyte result;
				if (flag)
				{
					result = (sbyte)this.m_val;
				}
				else
				{
					bool flag2 = this.m_val is string;
					if (flag2)
					{
						result = sbyte.Parse(this.m_val as string);
					}
					else
					{
						result = Convert.ToSByte(this.m_val);
					}
				}
				return result;
			}
			set
			{
				this.m_val = value;
			}
		}

		public byte _byte
		{
			get
			{
				bool flag = this.m_val is byte;
				byte result;
				if (flag)
				{
					result = (byte)this.m_val;
				}
				else
				{
					bool flag2 = this.m_val is string;
					if (flag2)
					{
						result = byte.Parse(this.m_val as string);
					}
					else
					{
						result = Convert.ToByte(this.m_val);
					}
				}
				return result;
			}
			set
			{
				this.m_val = value;
			}
		}

		public short _int16
		{
			get
			{
				bool flag = this.m_val is short;
				short result;
				if (flag)
				{
					result = (short)this.m_val;
				}
				else
				{
					bool flag2 = this.m_val is string;
					if (flag2)
					{
						result = short.Parse(this.m_val as string);
					}
					else
					{
						result = Convert.ToInt16(this.m_val);
					}
				}
				return result;
			}
			set
			{
				this.m_val = value;
			}
		}

		public ushort _uint16
		{
			get
			{
				bool flag = this.m_val is ushort;
				ushort result;
				if (flag)
				{
					result = (ushort)this.m_val;
				}
				else
				{
					bool flag2 = this.m_val is string;
					if (flag2)
					{
						result = ushort.Parse(this.m_val as string);
					}
					else
					{
						result = Convert.ToUInt16(this.m_val);
					}
				}
				return result;
			}
			set
			{
				this.m_val = value;
			}
		}

		public int _int32
		{
			get
			{
				bool flag = this.m_val is int;
				int result;
				if (flag)
				{
					result = (int)this.m_val;
				}
				else
				{
					bool flag2 = this.m_val is string;
					if (flag2)
					{
						result = int.Parse(this.m_val as string);
					}
					else
					{
						result = Convert.ToInt32(this.m_val);
					}
				}
				return result;
			}
			set
			{
				this.m_val = value;
			}
		}

		public uint _uint32
		{
			get
			{
				bool flag = this.m_val is uint;
				uint result;
				if (flag)
				{
					result = (uint)this.m_val;
				}
				else
				{
					bool flag2 = this.m_val is string;
					if (flag2)
					{
						result = uint.Parse(this.m_val as string);
					}
					else
					{
						result = Convert.ToUInt32(this.m_val);
					}
				}
				return result;
			}
			set
			{
				this.m_val = value;
			}
		}

		public int _int
		{
			get
			{
				return this._int32;
			}
			set
			{
				this.m_val = value;
			}
		}

		public uint _uint
		{
			get
			{
				return this._uint32;
			}
			set
			{
				this.m_val = value;
			}
		}

		public long _int64
		{
			get
			{
				bool flag = this.m_val is long;
				long result;
				if (flag)
				{
					result = (long)this.m_val;
				}
				else
				{
					bool flag2 = this.m_val is string;
					if (flag2)
					{
						result = long.Parse(this.m_val as string);
					}
					else
					{
						result = Convert.ToInt64(this.m_val);
					}
				}
				return result;
			}
			set
			{
				this.m_val = value;
			}
		}

		public ulong _uint64
		{
			get
			{
				bool flag = this.m_val is ulong;
				ulong result;
				if (flag)
				{
					result = (ulong)this.m_val;
				}
				else
				{
					bool flag2 = this.m_val is string;
					if (flag2)
					{
						result = ulong.Parse(this.m_val as string);
					}
					else
					{
						result = Convert.ToUInt64(this.m_val);
					}
				}
				return result;
			}
			set
			{
				this.m_val = value;
			}
		}

		public float _float
		{
			get
			{
				bool flag = this.m_val is float;
				float result;
				if (flag)
				{
					result = (float)this.m_val;
				}
				else
				{
					bool flag2 = this.m_val is string;
					if (flag2)
					{
						bool flag3 = this.m_val as string == "NaN" || this.m_val as string == "NAN";
						if (flag3)
						{
							result = float.NaN;
						}
						else
						{
							result = (float)double.Parse(this.m_val as string);
						}
					}
					else
					{
						result = Convert.ToSingle(this.m_val);
					}
				}
				return result;
			}
			set
			{
				this.m_val = value;
			}
		}

		public double _double
		{
			get
			{
				bool flag = this.m_val is double;
				double result;
				if (flag)
				{
					result = (double)this.m_val;
				}
				else
				{
					bool flag2 = this.m_val is string;
					if (flag2)
					{
						result = (double)((float)double.Parse(this.m_val as string));
					}
					else
					{
						result = Convert.ToDouble(this.m_val);
					}
				}
				return result;
			}
			set
			{
				this.m_val = value;
			}
		}

		public bool _bool
		{
			get
			{
				bool flag = this.m_val is bool;
				bool result;
				if (flag)
				{
					result = (bool)this.m_val;
				}
				else
				{
					bool flag2 = this.m_val is string;
					if (flag2)
					{
						result = ((this.m_val as string).ToLower() == "true");
					}
					else
					{
						result = Convert.ToBoolean(this.m_val);
					}
				}
				return result;
			}
			set
			{
				this.m_val = value;
			}
		}

		public object _val
		{
			get
			{
				return this.m_val;
			}
			set
			{
				this.m_val = value;
			}
		}

		public static Variant alloc()
		{
			bool flag = Variant.m_pool.Count > 0;
			Variant result;
			if (flag)
			{
				Variant value = Variant.m_pool.Last.Value;
				Variant.m_pool.RemoveLast();
				result = value;
			}
			else
			{
				result = new Variant();
			}
			return result;
		}

		public static Variant alloc(string val)
		{
			bool flag = Variant.m_pool.Count > 0;
			Variant result;
			if (flag)
			{
				Variant value = Variant.m_pool.Last.Value;
				Variant.m_pool.RemoveLast();
				value.m_val = val;
				result = value;
			}
			else
			{
				result = new Variant(val);
			}
			return result;
		}

		public static Variant alloc(byte val)
		{
			bool flag = Variant.m_pool.Count > 0;
			Variant result;
			if (flag)
			{
				Variant value = Variant.m_pool.Last.Value;
				Variant.m_pool.RemoveLast();
				value.m_val = val;
				result = value;
			}
			else
			{
				result = new Variant(val);
			}
			return result;
		}

		public static Variant alloc(ushort val)
		{
			bool flag = Variant.m_pool.Count > 0;
			Variant result;
			if (flag)
			{
				Variant value = Variant.m_pool.Last.Value;
				Variant.m_pool.RemoveLast();
				value.m_val = val;
				result = value;
			}
			else
			{
				result = new Variant(val);
			}
			return result;
		}

		public static Variant alloc(uint val)
		{
			bool flag = Variant.m_pool.Count > 0;
			Variant result;
			if (flag)
			{
				Variant value = Variant.m_pool.Last.Value;
				Variant.m_pool.RemoveLast();
				value.m_val = val;
				result = value;
			}
			else
			{
				result = new Variant(val);
			}
			return result;
		}

		public static Variant alloc(ulong val)
		{
			bool flag = Variant.m_pool.Count > 0;
			Variant result;
			if (flag)
			{
				Variant value = Variant.m_pool.Last.Value;
				Variant.m_pool.RemoveLast();
				value.m_val = val;
				result = value;
			}
			else
			{
				result = new Variant(val);
			}
			return result;
		}

		public static Variant alloc(sbyte val)
		{
			bool flag = Variant.m_pool.Count > 0;
			Variant result;
			if (flag)
			{
				Variant value = Variant.m_pool.Last.Value;
				Variant.m_pool.RemoveLast();
				value.m_val = val;
				result = value;
			}
			else
			{
				result = new Variant(val);
			}
			return result;
		}

		public static Variant alloc(short val)
		{
			bool flag = Variant.m_pool.Count > 0;
			Variant result;
			if (flag)
			{
				Variant value = Variant.m_pool.Last.Value;
				Variant.m_pool.RemoveLast();
				value.m_val = val;
				result = value;
			}
			else
			{
				result = new Variant(val);
			}
			return result;
		}

		public static Variant alloc(int val)
		{
			bool flag = Variant.m_pool.Count > 0;
			Variant result;
			if (flag)
			{
				Variant value = Variant.m_pool.Last.Value;
				Variant.m_pool.RemoveLast();
				value.m_val = val;
				result = value;
			}
			else
			{
				result = new Variant(val);
			}
			return result;
		}

		public static Variant alloc(long val)
		{
			bool flag = Variant.m_pool.Count > 0;
			Variant result;
			if (flag)
			{
				Variant value = Variant.m_pool.Last.Value;
				Variant.m_pool.RemoveLast();
				value.m_val = val;
				result = value;
			}
			else
			{
				result = new Variant(val);
			}
			return result;
		}

		public static Variant alloc(float val)
		{
			bool flag = Variant.m_pool.Count > 0;
			Variant result;
			if (flag)
			{
				Variant value = Variant.m_pool.Last.Value;
				Variant.m_pool.RemoveLast();
				value.m_val = val;
				result = value;
			}
			else
			{
				result = new Variant(val);
			}
			return result;
		}

		public static Variant alloc(double val)
		{
			bool flag = Variant.m_pool.Count > 0;
			Variant result;
			if (flag)
			{
				Variant value = Variant.m_pool.Last.Value;
				Variant.m_pool.RemoveLast();
				value.m_val = val;
				result = value;
			}
			else
			{
				result = new Variant(val);
			}
			return result;
		}

		public static Variant alloc(bool val)
		{
			bool flag = Variant.m_pool.Count > 0;
			Variant result;
			if (flag)
			{
				Variant value = Variant.m_pool.Last.Value;
				Variant.m_pool.RemoveLast();
				value.m_val = val;
				result = value;
			}
			else
			{
				result = new Variant(val);
			}
			return result;
		}

		public static Variant alloc(ByteArray val)
		{
			bool flag = Variant.m_pool.Count > 0;
			Variant result;
			if (flag)
			{
				Variant value = Variant.m_pool.Last.Value;
				Variant.m_pool.RemoveLast();
				value.m_val = val;
				result = value;
			}
			else
			{
				result = new Variant(val);
			}
			return result;
		}

		public static Variant alloc(byte[] val, int len)
		{
			bool flag = Variant.m_pool.Count > 0;
			Variant result;
			if (flag)
			{
				Variant value = Variant.m_pool.Last.Value;
				Variant.m_pool.RemoveLast();
				value.m_val = new ByteArray(val, len);
				result = value;
			}
			else
			{
				result = new Variant(val, len);
			}
			return result;
		}

		public static void free(Variant v)
		{
			bool flag = v.m_val is Dictionary<string, Variant>;
			if (flag)
			{
				foreach (Variant current in (v.m_val as Dictionary<string, Variant>).Values)
				{
					Variant.free(current);
				}
			}
			else
			{
				bool flag2 = v.m_val is Dictionary<int, Variant>;
				if (flag2)
				{
					foreach (Variant current2 in (v.m_val as Dictionary<int, Variant>).Values)
					{
						Variant.free(current2);
					}
				}
				else
				{
					bool flag3 = v.m_val is List<Variant>;
					if (flag3)
					{
						foreach (Variant current3 in (v.m_val as List<Variant>))
						{
							Variant.free(current3);
						}
					}
				}
			}
			v.m_val = null;
			Variant.m_pool.AddLast(v);
		}

		public Variant()
		{
		}

		public Variant(string val)
		{
			this.m_val = val;
		}

		public Variant(byte val)
		{
			this.m_val = val;
		}

		public Variant(ushort val)
		{
			this.m_val = val;
		}

		public Variant(uint val)
		{
			this.m_val = val;
		}

		public Variant(ulong val)
		{
			this.m_val = val;
		}

		public Variant(sbyte val)
		{
			this.m_val = val;
		}

		public Variant(short val)
		{
			this.m_val = val;
		}

		public Variant(int val)
		{
			this.m_val = val;
		}

		public Variant(long val)
		{
			this.m_val = val;
		}

		public Variant(float val)
		{
			this.m_val = val;
		}

		public Variant(double val)
		{
			this.m_val = val;
		}

		public Variant(bool val)
		{
			this.m_val = val;
		}

		public Variant(ByteArray val)
		{
			this.m_val = val;
		}

		public Variant(byte[] val, int len)
		{
			this.m_val = new ByteArray(val, len);
		}

		public static implicit operator Variant(string val)
		{
			return new Variant(val);
		}

		public static implicit operator Variant(byte val)
		{
			return new Variant(val);
		}

		public static implicit operator Variant(ushort val)
		{
			return new Variant(val);
		}

		public static implicit operator Variant(uint val)
		{
			return new Variant(val);
		}

		public static implicit operator Variant(ulong val)
		{
			return new Variant(val);
		}

		public static implicit operator Variant(sbyte val)
		{
			return new Variant(val);
		}

		public static implicit operator Variant(short val)
		{
			return new Variant(val);
		}

		public static implicit operator Variant(int val)
		{
			return new Variant(val);
		}

		public static implicit operator Variant(long val)
		{
			return new Variant(val);
		}

		public static implicit operator Variant(float val)
		{
			return new Variant(val);
		}

		public static implicit operator Variant(double val)
		{
			return new Variant(val);
		}

		public static implicit operator Variant(bool val)
		{
			return new Variant(val);
		}

		public static implicit operator Variant(ByteArray val)
		{
			return new Variant(val);
		}

		public static implicit operator string(Variant val)
		{
			return val._str;
		}

		public static implicit operator byte(Variant val)
		{
			return val._byte;
		}

		public static implicit operator ushort(Variant val)
		{
			return val._uint16;
		}

		public static implicit operator uint(Variant val)
		{
			return val._uint32;
		}

		public static implicit operator ulong(Variant val)
		{
			return val._uint64;
		}

		public static implicit operator sbyte(Variant val)
		{
			return val._sbyte;
		}

		public static implicit operator short(Variant val)
		{
			return val._int16;
		}

		public static implicit operator int(Variant val)
		{
			return val._int32;
		}

		public static implicit operator long(Variant val)
		{
			return val._int64;
		}

		public static implicit operator float(Variant val)
		{
			return val._float;
		}

		public static implicit operator double(Variant val)
		{
			return val._double;
		}

		public static implicit operator bool(Variant val)
		{
			return val._bool;
		}

		public static implicit operator ByteArray(Variant val)
		{
			return val._byteAry;
		}

		public void setToArray()
		{
			this.m_val = new List<Variant>();
		}

		public void setToDct()
		{
			this.m_val = new Dictionary<string, Variant>();
		}

		public void setToIntkeyDct()
		{
			this.m_val = new Dictionary<int, Variant>();
		}

		public void setToByteArray()
		{
			this.setToByteArray(0);
		}

		public void setToByteArray(int cap)
		{
			this.m_val = new ByteArray(cap);
		}

		public Variant clone()
		{
			Variant variant = null;
			bool isDct = this.isDct;
			if (isDct)
			{
				variant = new Variant();
				variant.setToDct();
				Dictionary<string, Variant> dictionary = this.m_val as Dictionary<string, Variant>;
				foreach (string current in dictionary.Keys)
				{
					bool flag = dictionary[current] != null;
					if (flag)
					{
						variant[current] = dictionary[current].clone();
					}
					else
					{
						variant[current] = null;
					}
				}
			}
			else
			{
				bool isIntkeyDct = this.isIntkeyDct;
				if (isIntkeyDct)
				{
					variant = new Variant();
					variant.setToIntkeyDct();
					Dictionary<int, Variant> dictionary2 = this.m_val as Dictionary<int, Variant>;
					foreach (int current2 in dictionary2.Keys)
					{
						bool flag2 = dictionary2[current2] != null;
						if (flag2)
						{
							variant[current2] = dictionary2[current2].clone();
						}
						else
						{
							variant[current2] = null;
						}
					}
				}
				else
				{
					bool isArr = this.isArr;
					if (isArr)
					{
						variant = new Variant();
						variant.setToArray();
						List<Variant> list = this.m_val as List<Variant>;
						for (int i = 0; i < list.Count; i++)
						{
							bool flag3 = list[i] != null;
							if (flag3)
							{
								variant.pushBack(list[i].clone());
							}
							else
							{
								variant.pushBack(null);
							}
						}
					}
					else
					{
						bool isByteArray = this.isByteArray;
						if (isByteArray)
						{
							variant = new Variant(this.m_val as ByteArray);
						}
						else
						{
							variant = new Variant();
							variant.m_val = this.m_val;
						}
					}
				}
			}
			return variant;
		}

		public Variant mergeFrom(Variant src)
		{
			bool flag = src == null;
			Variant result;
			if (flag)
			{
				result = this;
			}
			else
			{
				bool isDct = src.isDct;
				if (isDct)
				{
					bool flag2 = !this.isDct;
					if (flag2)
					{
						this.setToDct();
					}
					Dictionary<string, Variant> dictionary = src.m_val as Dictionary<string, Variant>;
					foreach (string current in dictionary.Keys)
					{
						bool flag3 = this.ContainsKey(current) && this[current] != null;
						if (flag3)
						{
							bool flag4 = dictionary[current].m_val != null;
							if (flag4)
							{
								this[current].mergeFrom(dictionary[current]);
							}
						}
						else
						{
							bool flag5 = src[current] != null;
							if (flag5)
							{
								this[current] = dictionary[current].clone();
							}
						}
					}
				}
				else
				{
					bool isIntkeyDct = src.isIntkeyDct;
					if (isIntkeyDct)
					{
						bool flag6 = !this.isIntkeyDct;
						if (flag6)
						{
							this.setToIntkeyDct();
						}
						Dictionary<int, Variant> dictionary2 = src.m_val as Dictionary<int, Variant>;
						foreach (int current2 in dictionary2.Keys)
						{
							bool flag7 = this.ContainsKey(current2) && this[current2] != null;
							if (flag7)
							{
								this[current2].mergeFrom(dictionary2[current2]);
							}
							else
							{
								this[current2] = dictionary2[current2].clone();
							}
						}
					}
					else
					{
						bool isArr = src.isArr;
						if (isArr)
						{
							bool flag8 = src.Count != 0;
							if (flag8)
							{
								bool flag9 = !this.isArr;
								if (flag9)
								{
									this.setToArray();
								}
								this.pushBack(src[0].clone());
							}
						}
						else
						{
							this.m_val = src.m_val;
						}
					}
				}
				result = this;
			}
			return result;
		}

		public Variant convertToDct(string key)
		{
			bool flag = this.isDct || this.isIntkeyDct;
			Variant result;
			if (flag)
			{
				result = this.clone();
			}
			else
			{
				bool isArr = this.isArr;
				if (isArr)
				{
					Variant variant = new Variant();
					variant.setToDct();
					List<Variant> list = this.m_val as List<Variant>;
					for (int i = 0; i < list.Count; i++)
					{
						Variant variant2 = list[i];
						bool flag2 = variant2 != null;
						if (flag2)
						{
							variant[variant2[key]._str] = variant2.clone();
						}
					}
					result = variant;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		public bool ContainsKey(string key)
		{
			bool flag = this.m_val == null || !(this.m_val is Dictionary<string, Variant>);
			return !flag && (this.m_val as Dictionary<string, Variant>).ContainsKey(key);
		}

		public void RemoveKey(string key)
		{
			bool flag = this.m_val == null || !(this.m_val is Dictionary<string, Variant>);
			if (!flag)
			{
				(this.m_val as Dictionary<string, Variant>).Remove(key);
			}
		}

		public bool ContainsKey(int key)
		{
			bool flag = this.m_val == null || !(this.m_val is Dictionary<int, Variant>);
			return !flag && (this.m_val as Dictionary<int, Variant>).ContainsKey(key);
		}

		public void pushBack(Variant v)
		{
			bool flag = this.m_val == null;
			if (flag)
			{
				this.m_val = new List<Variant>();
			}
			bool flag2 = this.m_val is List<Variant>;
			if (flag2)
			{
				List<Variant> list = this.m_val as List<Variant>;
				list.Add(v);
			}
		}

		public void pushValue(object obj)
		{
			bool flag = this.m_val == null;
			if (flag)
			{
				this.m_val = new List<Variant>();
			}
			bool flag2 = this.m_val is List<Variant>;
			if (flag2)
			{
				List<Variant> list = this.m_val as List<Variant>;
				bool flag3 = obj is Variant;
				if (flag3)
				{
					list.Add((Variant)obj);
				}
				else
				{
					list.Add(new Variant
					{
						m_val = obj
					});
				}
			}
		}

		public Variant getValue(string key)
		{
			bool flag = this.m_val != null && this.m_val is Dictionary<string, Variant>;
			Variant result;
			if (flag)
			{
				Variant variant = null;
				(this.m_val as Dictionary<string, Variant>).TryGetValue(key, out variant);
				result = variant;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public void setValue(string key, object value)
		{
			bool flag = this.m_val == null;
			if (flag)
			{
				this.m_val = new Dictionary<string, Variant>();
			}
			bool flag2 = this.m_val is Dictionary<string, Variant>;
			if (flag2)
			{
				bool flag3 = value is Variant;
				if (flag3)
				{
					(this.m_val as Dictionary<string, Variant>)[key] = (Variant)value;
				}
				else
				{
					Variant variant = new Variant();
					variant.m_val = value;
					(this.m_val as Dictionary<string, Variant>)[key] = variant;
				}
			}
		}

		public bool deepEqual(Variant v1)
		{
			bool flag = v1 == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this == v1;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = this.isDct && v1.isDct;
					if (flag3)
					{
						Dictionary<string, Variant> dictionary = this.m_val as Dictionary<string, Variant>;
						Dictionary<string, Variant> dictionary2 = v1.m_val as Dictionary<string, Variant>;
						bool flag4 = dictionary.Count != dictionary2.Count;
						if (flag4)
						{
							result = false;
							return result;
						}
						foreach (string current in dictionary.Keys)
						{
							bool flag5 = !dictionary[current].deepEqual(dictionary2[current]);
							if (flag5)
							{
								result = false;
								return result;
							}
						}
					}
					else
					{
						bool flag6 = this.isIntkeyDct && v1.isIntkeyDct;
						if (flag6)
						{
							Dictionary<int, Variant> dictionary3 = this.m_val as Dictionary<int, Variant>;
							Dictionary<int, Variant> dictionary4 = v1.m_val as Dictionary<int, Variant>;
							bool flag7 = dictionary3.Count != dictionary4.Count;
							if (flag7)
							{
								result = false;
								return result;
							}
							foreach (int current2 in dictionary3.Keys)
							{
								bool flag8 = dictionary3[current2].deepEqual(dictionary4[current2]);
								if (flag8)
								{
									result = false;
									return result;
								}
							}
						}
						else
						{
							bool flag9 = this.isArr && v1.isArr;
							if (flag9)
							{
								List<Variant> list = this.m_val as List<Variant>;
								List<Variant> list2 = v1.m_val as List<Variant>;
								bool flag10 = list.Count != list2.Count;
								if (flag10)
								{
									result = false;
									return result;
								}
								for (int i = 0; i < list.Count; i++)
								{
									bool flag11 = list[i].deepEqual(list2[i]);
									if (flag11)
									{
										result = false;
										return result;
									}
								}
							}
							else
							{
								bool flag12 = this.m_val == null && v1.m_val == null;
								if (flag12)
								{
									result = true;
									return result;
								}
								bool flag13 = this.m_val != null && v1.m_val != null;
								result = (flag13 && this.m_val.Equals(v1.m_val));
								return result;
							}
						}
					}
					result = true;
				}
			}
			return result;
		}

		public string dump()
		{
			return this._dump(0, "");
		}

		public string _dump(int offset, string key)
		{
			string text = "";
			string text2 = "";
			for (int i = 0; i < offset; i++)
			{
				text2 += " ";
			}
			bool isStr = this.isStr;
			if (isStr)
			{
				text += text2;
				bool flag = key != "";
				if (flag)
				{
					text = text + "[" + key + "]:";
				}
				text = text + "\"" + this._str.Replace("\0", "") + "\"";
				text += "\n";
			}
			else
			{
				bool flag2 = this.isNumber && !this.isIntkeyDct;
				if (flag2)
				{
					text += text2;
					bool flag3 = key != "";
					if (flag3)
					{
						text = text + "[" + key + "]:";
					}
					bool flag4 = this.isDouble || this.isFloat;
					if (flag4)
					{
						text += this._double;
					}
					else
					{
						text += this._int64;
					}
					text += "\n";
				}
				else
				{
					bool isBool = this.isBool;
					if (isBool)
					{
						text += text2;
						bool flag5 = key != "";
						if (flag5)
						{
							text = text + "[" + key + "]:";
						}
						text += (this._bool ? "true" : "false");
						text += "\n";
					}
					else
					{
						bool isByteArray = this.isByteArray;
						if (isByteArray)
						{
							text += text2;
							bool flag6 = key != "";
							if (flag6)
							{
								text = text + "[" + key + "]:";
							}
							text += "<ByteArray:";
							for (int j = 0; j < (this.m_val as ByteArray).length; j++)
							{
								bool flag7 = j != 0;
								if (flag7)
								{
									text += ",";
								}
								text += (this.m_val as ByteArray)[j];
							}
							text += ">\n";
						}
						else
						{
							bool isDct = this.isDct;
							if (isDct)
							{
								text += text2;
								bool flag8 = key != "";
								if (flag8)
								{
									text = text + "[" + key + "]:";
								}
								text = string.Concat(new object[]
								{
									text,
									"<Dct:",
									this.Count,
									">\n"
								});
								foreach (string current in this.Keys)
								{
									bool flag9 = this[current] != null;
									if (flag9)
									{
										text += this[current]._dump(offset + 4, current);
									}
								}
							}
							else
							{
								bool isArr = this.isArr;
								if (isArr)
								{
									text += text2;
									bool flag10 = key != "";
									if (flag10)
									{
										text = text + "[" + key + "]:";
									}
									text = string.Concat(new object[]
									{
										text,
										"<Array:",
										this.Count,
										">\n"
									});
									for (int k = 0; k < this.Length; k++)
									{
										text += this[k]._dump(offset + 4, string.Concat(k));
									}
								}
								else
								{
									bool isIntkeyDct = this.isIntkeyDct;
									if (isIntkeyDct)
									{
										text += text2;
										bool flag11 = key != "";
										if (flag11)
										{
											text = text + "[" + key + "]:";
										}
										text = string.Concat(new object[]
										{
											text,
											"<IntDct:",
											this.Count,
											">\n"
										});
										foreach (int current2 in this._intDct.Keys)
										{
											text += this[current2]._dump(offset + 4, current2.ToString());
										}
									}
								}
							}
						}
					}
				}
			}
			return text;
		}
	}
}
