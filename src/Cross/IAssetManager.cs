using System;

namespace Cross
{
	public interface IAssetManager
	{
		string rootPath
		{
			get;
		}

		bool async
		{
			get;
			set;
		}

		int maxLoadingNum
		{
			get;
			set;
		}

		T getAsset<T>(string assetPath) where T : class, IAsset;

		T getAsset<T>(string assetPath, Action<IAsset> onFin, Action<IAsset, float> onProg, Action<IAsset, string> onFail) where T : class, IAsset;

		void checkUpdateAssets(string url, Action<int> onBegin, Action<float> onProg, Action<bool> onEnd, Action<int, string> onError);
	}
}
