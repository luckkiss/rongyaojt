using System;
using System.Text;

namespace Cross
{
	public class BitsArray
	{
		protected ByteArray m_byteAry = new ByteArray();

		protected int m_bytesSize = 0;

		protected int m_bitPos = 0;

		protected int m_bytePos = 0;

		public ByteArray data
		{
			get
			{
				return this.m_byteAry;
			}
			set
			{
				this.m_byteAry.writeBytes(value.data, value.length);
				this.m_bytesSize = this.m_byteAry.length;
				this.m_bytePos = 0;
				this.m_bitPos = 0;
			}
		}

		public int length
		{
			get
			{
				return this.m_bytesSize * 8;
			}
		}

		public int position
		{
			get
			{
				return this.m_bytePos * 8 + this.m_bitPos;
			}
			set
			{
				this.m_bytePos = value / 8;
				this.m_bitPos = (value & 7);
				bool flag = this.m_bytePos >= this.m_bytesSize;
				if (flag)
				{
					this.m_bitPos = this.m_bytesSize;
				}
			}
		}

		public int bitsAvailable
		{
			get
			{
				bool flag = this.m_bytePos >= this.m_bytesSize;
				int result;
				if (flag)
				{
					result = 0;
				}
				else
				{
					result = (this.m_bytesSize - this.m_bytePos) * 8 - this.m_bitPos;
				}
				return result;
			}
		}

		public void writeBool(bool b)
		{
			while (this.m_bytePos + 1 >= this.m_byteAry.length)
			{
				this.m_byteAry.length++;
			}
			this._writeFlag(b);
		}

		public void writeInt(int i, int bits)
		{
			bool flag = bits == 0;
			if (!flag)
			{
				while (this.m_bytePos + 4 >= this.m_byteAry.length)
				{
					this.m_byteAry.length += 4;
				}
				bool flag2 = i < 0;
				if (flag2)
				{
					this._writeFlag(true);
					this._writeUint32((uint)(-(uint)i), bits - 1);
				}
				else
				{
					this._writeFlag(false);
					this._writeUint32((uint)i, bits - 1);
				}
			}
		}

		public void writeUint(uint i, int bits)
		{
			while (this.m_bytePos + 4 >= this.m_byteAry.length)
			{
				this.m_byteAry.length += 4;
			}
			this._writeUint32(i, bits);
		}

		public void writeDecimal(float f, int bits)
		{
			this.writeUint((uint)((f + 1f) * 0.5f * (float)((1 << bits) - 1)), bits);
		}

		public void writeUdecimal(float f, int bits)
		{
			this.writeUint((uint)(f * (float)((1 << bits) - 1)), bits);
		}

		public void writeStr(string str)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			bool flag = this.m_bitPos > 0;
			if (flag)
			{
				this.m_bytePos++;
				this.m_bitPos = 0;
			}
			bool flag2 = this.m_bytePos + bytes.Length + 1 >= this.m_byteAry.length;
			if (flag2)
			{
				this.m_byteAry.length += bytes.Length + 1;
			}
			int bytePos;
			for (int i = 0; i < bytes.Length; i++)
			{
				DynamicArray<byte> arg_8A_0 = this.m_byteAry;
				bytePos = this.m_bytePos;
				this.m_bytePos = bytePos + 1;
				arg_8A_0[bytePos] = bytes[i];
			}
			DynamicArray<byte> arg_BB_0 = this.m_byteAry;
			bytePos = this.m_bytePos;
			this.m_bytePos = bytePos + 1;
			arg_BB_0[bytePos] = 0;
			bool flag3 = this.m_bytePos > this.m_bytesSize;
			if (flag3)
			{
				this.m_bytesSize = this.m_bytePos;
			}
		}

		public bool readBool()
		{
			return this._readFlag();
		}

		public int readInt(int bits)
		{
			bool flag = bits == 0;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				bool flag2 = this._readFlag();
				if (flag2)
				{
					result = (int)(-(int)this._readUint32(bits - 1));
				}
				else
				{
					result = (int)this._readUint32(bits - 1);
				}
			}
			return result;
		}

		public uint readUint(int bits)
		{
			return this._readUint32(bits);
		}

		public float readDecimal(int bits)
		{
			return this._readUint32(bits) * 2f / (float)((1 << bits) - 1) - 1f;
		}

		public float readUdecimal(int bits)
		{
			return this._readUint32(bits) / (float)((1 << bits) - 1);
		}

		public string readStr()
		{
			bool flag = this.m_bytePos >= this.m_bytesSize;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = this.m_bitPos > 0;
				if (flag2)
				{
					this.m_bytePos++;
					this.m_bitPos = 0;
				}
				int num = 0;
				int bytePos = this.m_bytePos;
				while (this.m_byteAry[bytePos++] > 0)
				{
					num++;
				}
				this.m_byteAry.position = this.m_bytePos;
				string text = this.m_byteAry.readUTF8Bytes(num);
				this.m_bytePos += num + 1;
				result = text;
			}
			return result;
		}

		private bool _writeFlag(bool b)
		{
			if (b)
			{
				ByteArray byteAry = this.m_byteAry;
				int bytePos = this.m_bytePos;
				byteAry[bytePos] |= (byte)(1 << this.m_bitPos);
			}
			else
			{
				ByteArray byteAry = this.m_byteAry;
				int bytePos = this.m_bytePos;
				byteAry[bytePos] &= (byte)(~(byte)(1 << this.m_bitPos));
			}
			this.m_bitPos++;
			bool flag = this.m_bitPos >= 8;
			if (flag)
			{
				this.m_bytePos++;
				this.m_bitPos &= 7;
			}
			bool flag2 = this.m_bytePos >= this.m_bytesSize;
			if (flag2)
			{
				this.m_bytesSize = this.m_bytePos + ((this.m_bitPos > 0) ? 1 : 0);
			}
			return b;
		}

		private bool _readFlag()
		{
			bool flag = this.m_bytePos >= this.m_bytesSize;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = ((int)this.m_byteAry[this.m_bytePos] & 1 << this.m_bitPos) != 0;
				this.m_bitPos++;
				bool flag3 = this.m_bitPos >= 8;
				if (flag3)
				{
					this.m_bytePos++;
					this.m_bitPos &= 7;
				}
				result = flag2;
			}
			return result;
		}

		private void _writeUint32(uint i, int bits)
		{
			bool flag = bits > 32;
			if (flag)
			{
				bits = 32;
			}
			int num2;
			for (int j = bits; j > 0; j -= num2)
			{
				int num = 8 - this.m_bitPos;
				num2 = ((num > j) ? j : num);
				this.m_byteAry[this.m_bytePos] = (byte)(((uint)this.m_byteAry[this.m_bytePos] & (1u << this.m_bitPos) - 1u) | (uint)((uint)((ulong)(i >> bits - j) & (ulong)((long)((1 << num2) - 1))) << this.m_bitPos));
				this.m_bitPos += num2;
				bool flag2 = this.m_bitPos >= 8;
				if (flag2)
				{
					this.m_bytePos++;
					this.m_bitPos &= 7;
				}
			}
			bool flag3 = this.m_bytePos >= this.m_bytesSize;
			if (flag3)
			{
				this.m_bytesSize = this.m_bytePos + ((this.m_bitPos > 0) ? 1 : 0);
			}
		}

		private uint _readUint32(int bits)
		{
			bool flag = this.m_bytePos >= this.m_bytesSize;
			uint result;
			if (flag)
			{
				result = 0u;
			}
			else
			{
				bool flag2 = bits > 32;
				if (flag2)
				{
					bits = 32;
				}
				uint num = 0u;
				int num3;
				for (int i = bits; i > 0; i -= num3)
				{
					int num2 = 8 - this.m_bitPos;
					num3 = ((num2 > i) ? i : num2);
					num |= (uint)((uint)(this.m_byteAry[this.m_bytePos] >> this.m_bitPos & (1 << num3) - 1) << bits - i);
					this.m_bitPos += num3;
					bool flag3 = this.m_bitPos >= 8;
					if (flag3)
					{
						this.m_bytePos++;
						this.m_bitPos &= 7;
					}
				}
				result = num;
			}
			return result;
		}
	}
}
