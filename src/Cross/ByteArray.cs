using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;
using System.IO;
using System.Text;

namespace Cross
{
	public class ByteArray : DynamicArray<byte>
	{
		protected int m_pos = 0;

		protected Define.Endian m_endian = Define.Endian.LITTLE_ENDIAN;

		protected byte[] m_convertBuf = new byte[8];

		public int position
		{
			get
			{
				return this.m_pos;
			}
			set
			{
				this.m_pos = Math.Max(0, value);
			}
		}

		public int bytesAvailable
		{
			get
			{
				return Math.Max(0, this.m_size - this.m_pos);
			}
		}

		public override int length
		{
			get
			{
				return this.m_size;
			}
			set
			{
				bool flag = base.capcity < value;
				if (flag)
				{
					base.capcity = value;
				}
				this.m_size = value;
				bool flag2 = this.m_pos > this.m_size;
				if (flag2)
				{
					this.m_pos = this.m_size;
				}
			}
		}

		public ByteArray()
		{
		}

		public ByteArray(int cap) : base(cap)
		{
		}

		public ByteArray(byte[] d)
		{
			this.m_data = (d.Clone() as byte[]);
			this.m_size = d.Length;
			this.m_capcity = d.Length;
		}

		public ByteArray(byte[] d, int len)
		{
			this.m_data = (d.Clone() as byte[]);
			this.m_size = len;
			this.m_capcity = len;
		}

		public sbyte readByte()
		{
			bool flag = this.m_data == null || this.m_pos >= this.m_size;
			sbyte result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				byte[] arg_3C_0 = this.m_data;
				int pos = this.m_pos;
				this.m_pos = pos + 1;
				result = arg_3C_0[pos];
			}
			return result;
		}

		public byte readUnsignedByte()
		{
			bool flag = this.m_data == null || this.m_pos >= this.m_size;
			byte result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				byte[] arg_3C_0 = this.m_data;
				int pos = this.m_pos;
				this.m_pos = pos + 1;
				result = arg_3C_0[pos];
			}
			return result;
		}

		public unsafe int readInt()
		{
			bool flag = this.m_data == null || this.m_pos >= this.m_size - 3;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				bool flag2 = BitConverter.IsLittleEndian == (this.m_endian == Define.Endian.LITTLE_ENDIAN);
				int num;
				if (flag2)
				{
					fixed (byte* ptr = &this.m_data[this.m_pos])
					{
						num = *(int*)ptr;
					}
				}
				else
				{
					this.m_convertBuf[3] = this.m_data[this.m_pos];
					this.m_convertBuf[2] = this.m_data[this.m_pos + 1];
					this.m_convertBuf[1] = this.m_data[this.m_pos + 2];
					this.m_convertBuf[0] = this.m_data[this.m_pos + 3];
					fixed (byte* convertBuf = this.m_convertBuf)
					{
						num = *(int*)convertBuf;
					}
				}
				this.m_pos += 4;
				result = num;
			}
			return result;
		}

		public unsafe uint readUnsignedInt()
		{
			bool flag = this.m_data == null || this.m_pos >= this.m_size - 3;
			uint result;
			if (flag)
			{
				result = 0u;
			}
			else
			{
				bool flag2 = BitConverter.IsLittleEndian == (this.m_endian == Define.Endian.LITTLE_ENDIAN);
				uint num;
				if (flag2)
				{
					fixed (byte* ptr = &this.m_data[this.m_pos])
					{
						num = *(uint*)ptr;
					}
				}
				else
				{
					this.m_convertBuf[3] = this.m_data[this.m_pos];
					this.m_convertBuf[2] = this.m_data[this.m_pos + 1];
					this.m_convertBuf[1] = this.m_data[this.m_pos + 2];
					this.m_convertBuf[0] = this.m_data[this.m_pos + 3];
					fixed (byte* convertBuf = this.m_convertBuf)
					{
						num = *(uint*)convertBuf;
					}
				}
				this.m_pos += 4;
				result = num;
			}
			return result;
		}

		public unsafe short readShort()
		{
			bool flag = this.m_data == null || this.m_pos >= this.m_size - 1;
			short result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				bool flag2 = BitConverter.IsLittleEndian == (this.m_endian == Define.Endian.LITTLE_ENDIAN);
				short num;
				if (flag2)
				{
					fixed (byte* ptr = &this.m_data[this.m_pos])
					{
						num = *(short*)ptr;
					}
				}
				else
				{
					this.m_convertBuf[1] = this.m_data[this.m_pos];
					this.m_convertBuf[0] = this.m_data[this.m_pos + 1];
					fixed (byte* convertBuf = this.m_convertBuf)
					{
						num = *(short*)convertBuf;
					}
				}
				this.m_pos += 2;
				result = num;
			}
			return result;
		}

		public unsafe ushort readUnsignedShort()
		{
			bool flag = this.m_data == null || this.m_pos >= this.m_size - 1;
			ushort result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				bool flag2 = BitConverter.IsLittleEndian == (this.m_endian == Define.Endian.LITTLE_ENDIAN);
				ushort num;
				if (flag2)
				{
					fixed (byte* ptr = &this.m_data[this.m_pos])
					{
						num = *(ushort*)ptr;
					}
				}
				else
				{
					this.m_convertBuf[1] = this.m_data[this.m_pos];
					this.m_convertBuf[0] = this.m_data[this.m_pos + 1];
					fixed (byte* convertBuf = this.m_convertBuf)
					{
						num = *(ushort*)convertBuf;
					}
				}
				this.m_pos += 2;
				result = num;
			}
			return result;
		}

		public unsafe float readFloat()
		{
			bool flag = this.m_data == null || this.m_pos >= this.m_size - 3;
			float result;
			if (flag)
			{
				result = 0f;
			}
			else
			{
				bool flag2 = BitConverter.IsLittleEndian == (this.m_endian == Define.Endian.LITTLE_ENDIAN);
				float num;
				if (flag2)
				{
					this.m_convertBuf[0] = this.m_data[this.m_pos];
					this.m_convertBuf[1] = this.m_data[this.m_pos + 1];
					this.m_convertBuf[2] = this.m_data[this.m_pos + 2];
					this.m_convertBuf[3] = this.m_data[this.m_pos + 3];
					fixed (byte* convertBuf = this.m_convertBuf)
					{
						num = *(float*)convertBuf;
					}
				}
				else
				{
					this.m_convertBuf[3] = this.m_data[this.m_pos];
					this.m_convertBuf[2] = this.m_data[this.m_pos + 1];
					this.m_convertBuf[1] = this.m_data[this.m_pos + 2];
					this.m_convertBuf[0] = this.m_data[this.m_pos + 3];
					fixed (byte* convertBuf2 = this.m_convertBuf)
					{
						num = *(float*)convertBuf2;
					}
				}
				this.m_pos += 4;
				result = num;
			}
			return result;
		}

		public unsafe double readDouble()
		{
			bool flag = this.m_data == null || this.m_pos >= this.m_size - 7;
			double result;
			if (flag)
			{
				result = 0.0;
			}
			else
			{
				bool flag2 = BitConverter.IsLittleEndian == (this.m_endian == Define.Endian.LITTLE_ENDIAN);
				double num;
				if (flag2)
				{
					fixed (byte* ptr = &this.m_data[this.m_pos])
					{
						num = *(double*)ptr;
					}
				}
				else
				{
					this.m_convertBuf[7] = this.m_data[this.m_pos];
					this.m_convertBuf[6] = this.m_data[this.m_pos + 1];
					this.m_convertBuf[5] = this.m_data[this.m_pos + 2];
					this.m_convertBuf[4] = this.m_data[this.m_pos + 3];
					this.m_convertBuf[3] = this.m_data[this.m_pos + 4];
					this.m_convertBuf[2] = this.m_data[this.m_pos + 5];
					this.m_convertBuf[1] = this.m_data[this.m_pos + 6];
					this.m_convertBuf[0] = this.m_data[this.m_pos + 7];
					fixed (byte* convertBuf = this.m_convertBuf)
					{
						num = *(double*)convertBuf;
					}
				}
				this.m_pos += 8;
				result = num;
			}
			return result;
		}

		public string readUTF8Bytes(int len)
		{
			bool flag = this.m_data == null || this.m_pos >= this.m_size - len + 1;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				bool flag2 = this.m_data.Length - this.m_pos < len;
				if (flag2)
				{
					result = "";
				}
				else
				{
					string @string = Encoding.UTF8.GetString(this.m_data, this.m_pos, len);
					this.m_pos += len;
					result = @string;
				}
			}
			return result;
		}

		public void readBytes(ByteArray o, int offset, int len)
		{
			bool flag = offset == -1 && len == -1;
			if (flag)
			{
				offset = this.m_pos;
				len = this.m_size - this.m_pos;
			}
			bool flag2 = this.m_size < offset + len;
			if (!flag2)
			{
				int num = Math.Min(len, this.m_size - this.m_pos);
				bool flag3 = num <= 0;
				if (!flag3)
				{
					int i = 0;
					while (i < num)
					{
						byte[] arg_7D_0 = this.m_data;
						int pos = this.m_pos;
						this.m_pos = pos + 1;
						o.writeUnsignedByte(arg_7D_0[pos]);
						i++;
						bool flag4 = i >= num;
						if (flag4)
						{
							o.m_pos = 0;
						}
					}
				}
			}
		}

		public void writeByte(sbyte v)
		{
			bool flag = this.m_pos > this.m_size;
			if (flag)
			{
				this.m_pos = this.m_size;
			}
			bool flag2 = this.m_pos >= this.m_size;
			if (flag2)
			{
				base.pushBack((byte)v);
				this.m_pos = this.m_size;
			}
			else
			{
				byte[] arg_66_0 = this.m_data;
				int pos = this.m_pos;
				this.m_pos = pos + 1;
				arg_66_0[pos] = (byte)v;
			}
		}

		public void writeUnsignedByte(byte v)
		{
			bool flag = this.m_pos > this.m_size;
			if (flag)
			{
				this.m_pos = this.m_size;
			}
			bool flag2 = this.m_pos >= this.m_size;
			if (flag2)
			{
				base.pushBack(v);
				this.m_pos = this.m_size;
			}
			else
			{
				byte[] arg_64_0 = this.m_data;
				int pos = this.m_pos;
				this.m_pos = pos + 1;
				arg_64_0[pos] = v;
			}
		}

		public void writeShort(short v)
		{
			bool flag = this.m_pos > this.m_size;
			if (flag)
			{
				this.m_pos = this.m_size;
			}
			byte[] bytes = BitConverter.GetBytes(v);
			while (this.m_pos + bytes.Length > this.m_capcity)
			{
				base.capcity *= 2;
			}
			bool flag2 = BitConverter.IsLittleEndian != (this.m_endian == Define.Endian.LITTLE_ENDIAN);
			if (flag2)
			{
				Array.Reverse(bytes);
			}
			bytes.CopyTo(this.m_data, this.m_pos);
			this.m_pos += bytes.Length;
			bool flag3 = this.m_pos > this.m_size;
			if (flag3)
			{
				this.m_size = this.m_pos;
			}
		}

		public void writeUnsignedShort(ushort v)
		{
			bool flag = this.m_pos > this.m_size;
			if (flag)
			{
				this.m_pos = this.m_size;
			}
			byte[] bytes = BitConverter.GetBytes(v);
			while (this.m_pos + bytes.Length > this.m_capcity)
			{
				base.capcity *= 2;
			}
			bool flag2 = BitConverter.IsLittleEndian != (this.m_endian == Define.Endian.LITTLE_ENDIAN);
			if (flag2)
			{
				Array.Reverse(bytes);
			}
			bytes.CopyTo(this.m_data, this.m_pos);
			this.m_pos += bytes.Length;
			bool flag3 = this.m_pos > this.m_size;
			if (flag3)
			{
				this.m_size = this.m_pos;
			}
		}

		public void writeUnsignedInt(uint v)
		{
			bool flag = this.m_pos > this.m_size;
			if (flag)
			{
				this.m_pos = this.m_size;
			}
			byte[] bytes = BitConverter.GetBytes(v);
			while (this.m_pos + bytes.Length > this.m_capcity)
			{
				base.capcity *= 2;
			}
			bool flag2 = BitConverter.IsLittleEndian != (this.m_endian == Define.Endian.LITTLE_ENDIAN);
			if (flag2)
			{
				Array.Reverse(bytes);
			}
			bytes.CopyTo(this.m_data, this.m_pos);
			this.m_pos += bytes.Length;
			bool flag3 = this.m_pos > this.m_size;
			if (flag3)
			{
				this.m_size = this.m_pos;
			}
		}

		public void writeInt(int v)
		{
			bool flag = this.m_pos > this.m_size;
			if (flag)
			{
				this.m_pos = this.m_size;
			}
			byte[] bytes = BitConverter.GetBytes(v);
			while (this.m_pos + bytes.Length > this.m_capcity)
			{
				base.capcity *= 2;
			}
			bool flag2 = BitConverter.IsLittleEndian != (this.m_endian == Define.Endian.LITTLE_ENDIAN);
			if (flag2)
			{
				Array.Reverse(bytes);
			}
			bytes.CopyTo(this.m_data, this.m_pos);
			this.m_pos += bytes.Length;
			bool flag3 = this.m_pos > this.m_size;
			if (flag3)
			{
				this.m_size = this.m_pos;
			}
		}

		public void writeFloat(float v)
		{
			bool flag = this.m_pos > this.m_size;
			if (flag)
			{
				this.m_pos = this.m_size;
			}
			byte[] bytes = BitConverter.GetBytes(v);
			while (this.m_pos + bytes.Length > this.m_capcity)
			{
				base.capcity *= 2;
			}
			bool flag2 = BitConverter.IsLittleEndian != (this.m_endian == Define.Endian.LITTLE_ENDIAN);
			if (flag2)
			{
				Array.Reverse(bytes);
			}
			bytes.CopyTo(this.m_data, this.m_pos);
			this.m_pos += bytes.Length;
			bool flag3 = this.m_pos > this.m_size;
			if (flag3)
			{
				this.m_size = this.m_pos;
			}
		}

		public void writeDouble(double v)
		{
			bool flag = this.m_pos > this.m_size;
			if (flag)
			{
				this.m_pos = this.m_size;
			}
			byte[] bytes = BitConverter.GetBytes(v);
			while (this.m_pos + bytes.Length > this.m_capcity)
			{
				base.capcity *= 2;
			}
			bool flag2 = BitConverter.IsLittleEndian != (this.m_endian == Define.Endian.LITTLE_ENDIAN);
			if (flag2)
			{
				Array.Reverse(bytes);
			}
			bytes.CopyTo(this.m_data, this.m_pos);
			this.m_pos += bytes.Length;
			bool flag3 = this.m_pos > this.m_size;
			if (flag3)
			{
				this.m_size = this.m_pos;
			}
		}

		public void writeBytes(ByteArray o, int offset, int len)
		{
			bool flag = this.m_pos > this.m_size;
			if (flag)
			{
				this.m_pos = this.m_size;
			}
			while (this.m_pos + len > this.m_capcity)
			{
				base.capcity *= 2;
			}
			Array.Copy(o.m_data, offset, this.m_data, this.m_pos, len);
			this.m_pos += len;
			bool flag2 = this.m_pos > this.m_size;
			if (flag2)
			{
				this.m_size = this.m_pos;
			}
		}

		public void writeBytes(byte[] data, int len)
		{
			bool flag = this.m_pos > this.m_size;
			if (flag)
			{
				this.m_pos = this.m_size;
			}
			while (this.m_pos + len > this.m_capcity)
			{
				base.capcity *= 2;
			}
			Array.Copy(data, 0, this.m_data, this.m_pos, len);
			this.m_pos += len;
			bool flag2 = this.m_pos > this.m_size;
			if (flag2)
			{
				this.m_size = this.m_pos;
			}
		}

		public void writeBytes(char[] data, int len)
		{
			bool flag = this.m_pos > this.m_size;
			if (flag)
			{
				this.m_pos = this.m_size;
			}
			while (this.m_pos + len > this.m_capcity)
			{
				base.capcity *= 2;
			}
			Array.Copy(data, 0, this.m_data, this.m_pos, len);
			this.m_pos += len;
			bool flag2 = this.m_pos > this.m_size;
			if (flag2)
			{
				this.m_size = this.m_pos;
			}
		}

		public void writeUTF8Bytes(string str)
		{
			bool flag = this.m_pos > this.m_size;
			if (flag)
			{
				this.m_pos = this.m_size;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			while (this.m_pos + bytes.Length > this.m_capcity)
			{
				base.capcity *= 2;
			}
			bytes.CopyTo(this.m_data, this.m_pos);
			this.m_pos += bytes.Length;
			bool flag2 = this.m_pos > this.m_size;
			if (flag2)
			{
				this.m_size = this.m_pos;
			}
		}

		public bool compress()
		{
			byte[] array = new byte[Math.Max(32, this.m_data.Length)];
			DeflaterOutputStream deflaterOutputStream = new DeflaterOutputStream(new MemoryStream(array));
			deflaterOutputStream.Write(this.m_data, 0, this.m_size);
			deflaterOutputStream.Finish();
			int size = Convert.ToInt32(deflaterOutputStream.Position);
			deflaterOutputStream.Close();
			base.clear();
			this.m_data = array;
			this.m_size = size;
			this.m_capcity = array.Length;
			return true;
		}

		public bool uncompress()
		{
			bool result;
			try
			{
				InflaterInputStream inflaterInputStream = new InflaterInputStream(new MemoryStream(this.m_data));
				DynamicArray<byte> dynamicArray = new DynamicArray<byte>();
				byte[] array = new byte[2048];
				while (true)
				{
					int num = inflaterInputStream.Read(array, 0, array.Length);
					bool flag = num > 0;
					if (!flag)
					{
						break;
					}
					dynamicArray.pushBack(array, num);
				}
				inflaterInputStream.Close();
				base.clear();
				this.m_data = dynamicArray.data;
				this.m_size = dynamicArray.length;
				this.m_capcity = dynamicArray.capcity;
			}
			catch (Exception)
			{
				DebugTrace.print("Failed to uncompress ByteArray");
				result = false;
				return result;
			}
			result = true;
			return result;
		}

		public string dump()
		{
			string text = "";
			text += "[";
			for (int i = 0; i < this.m_size; i++)
			{
				bool flag = i != 0;
				if (flag)
				{
					text += ",";
				}
				text += this.m_data[i];
			}
			return text + "]";
		}
	}
}
