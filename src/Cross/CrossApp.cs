using System;
using System.Collections.Generic;

namespace Cross
{
	public class CrossApp
	{
		protected static CrossApp m_singleton;

		protected Dictionary<string, IAppPlugin> m_plugins;

		protected List<IAppPlugin> m_pluginList;

		protected List<Action<float>> m_procFuncList;

		protected List<Action<float>> m_renderFuncList;

		protected List<Action<int, int>> m_resizeFuncList;

		protected TickProcessArrayHost m_tickProcObjs;

		public static CrossApp singleton
		{
			get
			{
				return CrossApp.m_singleton;
			}
		}

		public int width
		{
			get
			{
				return os.sys.windowWidth;
			}
		}

		public int height
		{
			get
			{
				return os.sys.windowHeight;
			}
		}

		public CrossApp(bool compatibleMode)
		{
			CrossApp.m_singleton = this;
			this.m_plugins = new Dictionary<string, IAppPlugin>();
			this.m_pluginList = new List<IAppPlugin>();
			this.m_procFuncList = new List<Action<float>>();
			this.m_renderFuncList = new List<Action<float>>();
			this.m_resizeFuncList = new List<Action<int, int>>();
			this.m_tickProcObjs = new TickProcessArrayHost();
			this.regPlugin(new ConfigManager());
			this.regPlugin(new NetManager());
			this.regPlugin(new GraphManager(compatibleMode));
			this.regPlugin(new LocalizeManager());
			this.regPlugin(new PhysicsManager());
		}

		public IAppPlugin getPlugin(string name)
		{
			bool flag = this.m_plugins.ContainsKey(name);
			IAppPlugin result;
			if (flag)
			{
				result = this.m_plugins[name];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public void init()
		{
			foreach (IAppPlugin current in this.m_pluginList)
			{
				current.onPreInit();
			}
			foreach (IAppPlugin current2 in this.m_pluginList)
			{
				current2.onInit();
			}
			foreach (IAppPlugin current3 in this.m_pluginList)
			{
				current3.onPostInit();
			}
		}

		public void render(float tmSlice)
		{
			foreach (IAppPlugin current in this.m_pluginList)
			{
				current.onPreRender(tmSlice);
			}
			foreach (IAppPlugin current2 in this.m_pluginList)
			{
				current2.onRender(tmSlice);
			}
			foreach (IAppPlugin current3 in this.m_pluginList)
			{
				current3.onPostRender(tmSlice);
			}
			foreach (Action<float> current4 in this.m_renderFuncList)
			{
				current4(tmSlice);
			}
		}

		public void process(float tmSlice)
		{
			foreach (IAppPlugin current in this.m_pluginList)
			{
				current.onPreProcess(tmSlice);
			}
			foreach (IAppPlugin current2 in this.m_pluginList)
			{
				current2.onProcess(tmSlice);
			}
			foreach (IAppPlugin current3 in this.m_pluginList)
			{
				current3.onPostProcess(tmSlice);
			}
			foreach (Action<float> current4 in this.m_procFuncList)
			{
				current4(tmSlice);
			}
		}

		public void resize(int width, int height)
		{
			foreach (Action<int, int> current in this.m_resizeFuncList)
			{
				current(width, height);
			}
		}

		public void regPlugin(IAppPlugin plg)
		{
			bool flag = this.m_plugins.ContainsKey(plg.pluginName);
			if (!flag)
			{
				this.m_plugins[plg.pluginName] = plg;
				this.m_pluginList.Add(plg);
			}
		}

		public void regProcessFunc(Action<float> func)
		{
			bool flag = !this.m_procFuncList.Contains(func);
			if (flag)
			{
				this.m_procFuncList.Add(func);
			}
		}

		public void regRenderFunc(Action<float> func)
		{
			bool flag = !this.m_renderFuncList.Contains(func);
			if (flag)
			{
				this.m_renderFuncList.Add(func);
			}
		}

		public void regResizeFunc(Action<int, int> func)
		{
			bool flag = !this.m_resizeFuncList.Contains(func);
			if (flag)
			{
				this.m_resizeFuncList.Add(func);
			}
		}

		public void pushTickProcObject(ITickProcessObject obj, Variant prop)
		{
			this.m_tickProcObjs.pushObject(obj, prop);
		}
	}
}
