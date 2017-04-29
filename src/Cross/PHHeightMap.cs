using System;
using System.Collections.Generic;

namespace Cross
{
	public class PHHeightMap : PHEntity3D
	{
		protected List<IHeightMap> m_htmaps = new List<IHeightMap>();

		public PHHeightMap(string id, PhysicsManager phyMgr) : base(id, phyMgr)
		{
		}

		public override void load(Variant conf, Action onFin)
		{
			this.m_conf = conf;
			bool flag = this.m_conf == null;
			if (!flag)
			{
				base.load(this.m_conf, onFin);
				this.m_htmaps.Clear();
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
								this.m_obj = os.physics.createScene3D().createHeightMap();
								(this.m_obj as IHeightMap).asset = os.asset.getAsset<IAssetHeightMap>(variant2["file"]._str);
								this.m_htmaps.Add(this.m_obj as IHeightMap);
							}
						}
					}
				}
			}
		}

		public override Vec3 rayCast(Vec3 origin, Vec3 dir)
		{
			bool flag = this.m_obj == null;
			Vec3 result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.m_obj.rayCast(origin, dir);
			}
			return result;
		}

		public float pickHeight(float h, float v)
		{
			bool flag = !(this.m_obj is IHeightMap);
			float result;
			if (flag)
			{
				result = float.NaN;
			}
			else
			{
				result = (this.m_obj as IHeightMap).pickHeight(h, v);
			}
			return result;
		}

		public override void dispose()
		{
			foreach (IHeightMap current in this.m_htmaps)
			{
				this.m_obj.dispose();
			}
			this.m_htmaps.Clear();
			base.dispose();
		}
	}
}
