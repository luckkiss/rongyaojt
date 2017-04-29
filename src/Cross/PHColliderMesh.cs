using System;
using System.Collections.Generic;

namespace Cross
{
	public class PHColliderMesh : PHEntity3D
	{
		protected List<IColliderMesh> m_collidermesnes = new List<IColliderMesh>();

		public PHColliderMesh(string id, PhysicsManager grMgr) : base(id, grMgr)
		{
		}

		public override void load(Variant conf, Action onFin)
		{
			this.m_conf = conf;
			bool flag = this.m_conf == null;
			if (!flag)
			{
				base.load(conf, onFin);
				this.m_collidermesnes.Clear();
				bool flag2 = this.m_conf.ContainsKey("file");
				if (flag2)
				{
					bool flag3 = this.m_conf["cls"]._str == "ColliderMesh";
					if (flag3)
					{
						this.m_obj = os.physics.createScene3D().createColliderMesh();
						(this.m_obj as IColliderMesh).asset = os.asset.getAsset<IAssetMesh>(this.m_conf["file"]._str);
						this.m_collidermesnes.Add(this.m_obj as IColliderMesh);
					}
				}
				onFin();
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

		public override void dispose()
		{
			foreach (IColliderMesh current in this.m_collidermesnes)
			{
				this.m_obj.dispose();
			}
			this.m_collidermesnes.Clear();
			base.dispose();
		}
	}
}
