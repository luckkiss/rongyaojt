using System;

namespace Cross
{
	public interface IGREntity
	{
		string id
		{
			get;
		}

		IGRWorld world
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

		IGraphObject graphObject
		{
			get;
		}

		uint updateFlag
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

		int layer
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

		bool visible
		{
			get;
			set;
		}

		void load(Variant conf, IShader mtrl = null, Action onFin = null);

		void dispose();

		void onPreRender(float tmSlice);

		void onRender(float tmSlice);

		void onPostRender(float tmSlice);

		void onPreProcess(float tmSlice);

		void onProcess(float tmSlice);

		void onPostProcess(float tmSlice);

		void addScript(string objname, string name);
	}
}
