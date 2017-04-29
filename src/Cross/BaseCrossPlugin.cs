using System;

namespace Cross
{
	public class BaseCrossPlugin : IAppPlugin
	{
		public virtual string pluginName
		{
			get
			{
				return "";
			}
		}

		public virtual void onPreInit()
		{
		}

		public virtual void onInit()
		{
		}

		public virtual void onPostInit()
		{
		}

		public virtual void onFin()
		{
		}

		public virtual void onResize(int width, int height)
		{
		}

		public virtual void onPreRender(float tmSlice)
		{
		}

		public virtual void onRender(float tmSlice)
		{
		}

		public virtual void onPostRender(float tmSlice)
		{
		}

		public virtual void onPreProcess(float tmSlice)
		{
		}

		public virtual void onProcess(float tmSlice)
		{
		}

		public virtual void onPostProcess(float tmSlice)
		{
		}

		public virtual void onUpdate(float tmSlice)
		{
		}
	}
}
