using System;
using System.Collections.Generic;

namespace Cross
{
	public class GRStaticMesh3D : GREntity3D
	{
		protected List<IMesh> m_meshes = new List<IMesh>();

		protected float m_alpha = 1f;

		public GRStaticMesh3D(string id, GRWorld3D world) : base(id, world)
		{
		}

		public override void load(Variant conf, IShader mtrl = null, Action onFin = null)
		{
			this.m_conf = conf;
			bool flag = this.m_conf == null;
			if (!flag)
			{
				this.m_meshes.Clear();
				IMesh mesh = this.m_world.scene3d.createMesh();
				mesh.asset = os.asset.getAsset<IAssetMesh>(conf["file"]._str);
				mesh.helper["$graphObj"] = this;
				this.m_meshes.Add(mesh);
				this.m_rootObj.addChild(mesh);
				this.m_loaded = true;
			}
		}

		public override void dispose()
		{
			foreach (IMesh current in this.m_meshes)
			{
				current.dispose();
			}
			this.m_meshes.Clear();
			base.dispose();
		}
	}
}
