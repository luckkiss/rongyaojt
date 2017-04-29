using System;

namespace Cross
{
	public class GRBillboard : GREntity3D
	{
		protected IBillboard m_billBoard;

		public GRBillboard(string id, GRWorld3D world) : base(id, world)
		{
		}

		public override void load(Variant conf, IShader mtrl = null, Action onFin = null)
		{
			this.m_billBoard = this.m_world.scene3d.createBillboard();
			this.m_billBoard.asset = os.asset.getAsset<IAssetBitmap>(conf["file"]._str);
			this.m_billBoard.setHeight = (float)(conf.ContainsKey("height") ? conf["height"]._int : this.m_billBoard.asset.height);
			this.m_billBoard.setWidth = (float)(conf.ContainsKey("width") ? conf["width"]._int : this.m_billBoard.asset.width);
			base.rootObj.addChild(this.m_billBoard);
			this.m_billBoard.helper["$graphObj"] = this;
			BoundBox boxCollider = new BoundBox(new Vec3(float.Parse((this.m_billBoard.setWidth / 2f).ToString()), float.Parse((-this.m_billBoard.setHeight / 2f).ToString()), float.Parse("0.5")), new Vec3(float.Parse(this.m_billBoard.setWidth.ToString()), float.Parse(this.m_billBoard.setHeight.ToString()), float.Parse("1")));
			this.m_billBoard.boxCollider = boxCollider;
		}

		public override void dispose()
		{
			this.m_billBoard.dispose();
			base.dispose();
		}
	}
}
