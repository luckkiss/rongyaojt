using System;
using System.IO;

namespace Cross
{
	public class AssetHeightMapImpl : AssetImpl, IAssetHeightMap, IAsset
	{
		protected float m_width;

		protected float m_height;

		protected int m_pixelWidth;

		protected int m_pixelHeight;

		protected float m_heightMin;

		protected float m_heightMax;

		protected float m_proportion;

		protected int m_produc;

		protected byte[] m_byt;

		public float width
		{
			get
			{
				return this.m_width;
			}
		}

		public float height
		{
			get
			{
				return this.m_height;
			}
		}

		public int pixelWidth
		{
			get
			{
				return this.m_pixelWidth;
			}
		}

		public int pixelHeight
		{
			get
			{
				return this.m_pixelHeight;
			}
		}

		public float proportion
		{
			get
			{
				return this.m_proportion;
			}
		}

		public float heightMin
		{
			get
			{
				return this.m_heightMin;
			}
		}

		public float heightMax
		{
			get
			{
				return this.m_heightMax;
			}
		}

		public byte[] byt
		{
			get
			{
				bool flag = this.m_byt == null;
				byte[] result;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.m_byt;
				}
				return result;
			}
		}

		public override void loadImpl(bool bSync)
		{
			base.loadImpl(bSync);
			bool flag = this.m_path == null || this.m_path == "";
			if (!flag)
			{
				bool flag2 = this.m_ready || this.m_loaded;
				if (!flag2)
				{
					FileStream input = new FileStream(base.path, FileMode.Open, FileAccess.Read);
					BinaryReader binaryReader = new BinaryReader(input);
					try
					{
						char c = binaryReader.ReadChar();
						char c2 = binaryReader.ReadChar();
						char c3 = binaryReader.ReadChar();
						char c4 = binaryReader.ReadChar();
						int num = (int)binaryReader.ReadInt16();
						this.m_pixelWidth = (int)binaryReader.ReadInt16();
						this.m_pixelHeight = (int)binaryReader.ReadInt16();
						this.m_proportion = binaryReader.ReadSingle();
						this.m_heightMin = binaryReader.ReadSingle();
						this.m_heightMax = binaryReader.ReadSingle();
						this.m_produc = this.m_pixelWidth * this.m_pixelHeight;
						this.m_byt = new byte[this.m_produc];
						binaryReader.Read(this.byt, 0, this.m_produc);
					}
					catch (EndOfStreamException ex)
					{
						DebugTrace.print(ex.Message);
					}
					bool flag3 = string.Concat(this.m_pixelWidth) != null && string.Concat(this.m_pixelHeight) != null && string.Concat(this.m_heightMin) != null && string.Concat(this.m_heightMax) != null && string.Concat(this.m_proportion) != null && this.m_byt != null;
					if (flag3)
					{
						this.m_ready = true;
						(os.asset as AssetManagerImpl).readyAsset(this);
					}
					this.m_loaded = true;
				}
			}
		}
	}
}
