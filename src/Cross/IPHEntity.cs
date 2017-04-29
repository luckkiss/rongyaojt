using System;

namespace Cross
{
	public interface IPHEntity
	{
		string id
		{
			get;
		}

		bool isLoaded
		{
			get;
		}

		Variant config
		{
			get;
		}

		IPhysicsObject physicsObject
		{
			get;
		}

		Vec3 pos
		{
			get;
			set;
		}

		float x
		{
			get;
			set;
		}

		float y
		{
			get;
			set;
		}

		float z
		{
			get;
			set;
		}

		Vec3 rot
		{
			get;
			set;
		}

		float rotX
		{
			get;
			set;
		}

		float rotY
		{
			get;
			set;
		}

		float rotZ
		{
			get;
			set;
		}

		Vec3 scale
		{
			get;
			set;
		}

		float scaleX
		{
			get;
			set;
		}

		float scaleY
		{
			get;
			set;
		}

		float scaleZ
		{
			get;
			set;
		}

		Vec3 axisX
		{
			get;
		}

		Vec3 axisY
		{
			get;
		}

		Vec3 axisZ
		{
			get;
		}

		void load(Variant conf, Action onFin);

		void dispose();

		void onPreRender(float tmSlice);

		void onRender(float tmSlice);

		void onPostRender(float tmSlice);

		void onPreProcess(float tmSlice);

		void onProcess(float tmSlice);

		void onPostProcess(float tmSlice);
	}
}
