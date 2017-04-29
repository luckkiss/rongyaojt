using System;

namespace Cross
{
	public interface IAppPlugin
	{
		string pluginName
		{
			get;
		}

		void onPreInit();

		void onInit();

		void onPostInit();

		void onResize(int width, int height);

		void onPreRender(float tmSlice);

		void onRender(float tmSlice);

		void onPostRender(float tmSlice);

		void onPreProcess(float tmSlice);

		void onProcess(float tmSlice);

		void onPostProcess(float tmSlice);
	}
}
