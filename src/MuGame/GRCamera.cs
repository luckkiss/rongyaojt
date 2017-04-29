using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class GRCamera : GRBaseImpls, IObjectPlugin
	{
		private Vec3 _lookat = new Vec3();

		private float needCameraX = 0f;

		private float needCameraY = 0f;

		private float needCameraZ = 0f;

		private GRCamera3D m_cam
		{
			get
			{
				return this.m_gr as GRCamera3D;
			}
		}

		public GRCamera(muGRClient m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new GRCamera(m as muGRClient);
		}

		public override void init()
		{
		}

		protected override void onSetSceneCtrl()
		{
			this.m_ctrl.addEventListener(2100u, new Action<GameEvent>(this.setData));
			this.m_ctrl.addEventListener(2226u, new Action<GameEvent>(this.objmask));
		}

		protected override void onSetGraphImpl()
		{
		}

		private void onGlobalMouseDown(Event e)
		{
		}

		private void setData(GameEvent e)
		{
			Vec3 vec = e.orgdata as Vec3;
			this.updateXYZ(vec.x, vec.y, vec.z);
		}

		private void upDateView(GameEvent e)
		{
			Vec3 vec = e.orgdata as Vec3;
			this.updateXYZ(vec.x, vec.y, vec.z);
		}

		private void upDateZ(GameEvent e)
		{
			Variant data = e.data;
			float y = data["z"] + GameConstant.CAMERA_POSTION_OFFSET_FROM_CHAR_Z;
			this.m_cam.y = y;
		}

		private void objmask(GameEvent e)
		{
			this.m_cam.obj_mask(e.orgdata as Vec3, this.m_cam.pos);
		}

		private void updateXYZ(float unity_x, float unity_y, float unity_z)
		{
		}

		private void updataRotation(float x, float y, float z)
		{
			this.m_cam.rot = new Vec3(x, y, z);
		}

		private void onLookAt(GameEvent e)
		{
		}

		public override void dispose()
		{
			this.g_mgr.deleteEntity(this.m_cam);
		}

		public override void updateProcess(float tmSlice)
		{
		}
	}
}
