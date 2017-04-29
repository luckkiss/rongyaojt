using System;

namespace Cross
{
	public class PHMap3D : IPHMap
	{
		protected PHWorld3D m_phworld3d;

		protected IHeightMap m_heightmap;

		protected Variant m_conf;

		protected string m_id;

		public IPHWorld world
		{
			get
			{
				return this.m_phworld3d;
			}
		}

		public PHMap3D(string id, PHWorld3D world)
		{
			this.m_conf = null;
			this.m_id = id;
			this.m_phworld3d = world;
		}

		public bool load(Variant conf, Action onFin)
		{
			this.m_conf = conf;
			bool flag = conf == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.m_conf.ContainsKey("asset");
				if (flag2)
				{
					Variant variant = this.m_conf["asset"];
					for (int i = 0; i < variant.Count; i++)
					{
						Variant variant2 = variant[i];
						bool flag3 = variant2.ContainsKey("file");
						if (flag3)
						{
							bool flag4 = variant2["cls"]._str == "htmap";
							if (flag4)
							{
								this.m_heightmap = os.physics.createScene3D().createHeightMap();
								this.m_heightmap.asset = os.asset.getAsset<IAssetHeightMap>(variant2["file"]._str);
							}
						}
					}
				}
				result = true;
			}
			return result;
		}

		public float getTerrainHeight(float x, float z)
		{
			return this.m_heightmap.pickHeight(x, z);
		}
	}
}
