using System;

namespace Cross
{
	public interface IAsset
	{
		string path
		{
			get;
		}

		bool isReady
		{
			get;
		}

		float autoDisposeTime
		{
			get;
			set;
		}

		void dispose();

		void load();

		void addCallbacks(Action<IAsset> onFin, Action<IAsset, float> onProg, Action<IAsset, string> onFail);
	}
}
