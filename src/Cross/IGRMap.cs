using System;
using System.Collections.Generic;

namespace Cross
{
	public interface IGRMap
	{
		string id
		{
			get;
		}

		IGRWorld world
		{
			get;
		}

		Dictionary<string, IGraphObject3D> obj
		{
			get;
		}

		Dictionary<string, string> objid
		{
			get;
		}

		Dictionary<string, string> file
		{
			get;
		}

		Dictionary<string, string> tag
		{
			get;
		}

		Dictionary<string, List<IGraphObject3D>> allListObj
		{
			get;
		}

		IContainer3D mapContainer
		{
			get;
		}

		bool loadConfig(Variant conf, Action<GRMap3D> onFin = null, Action<GRMap3D, float> onProgress = null, Action<GRMap3D, string> onFail = null);

		void dispose();
	}
}
