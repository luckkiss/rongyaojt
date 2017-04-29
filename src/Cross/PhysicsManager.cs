using System;
using System.Collections.Generic;

namespace Cross
{
	public class PhysicsManager : IAppPlugin
	{
		protected Variant m_conf;

		protected Dictionary<string, IPHWorld> m_worlds = new Dictionary<string, IPHWorld>();

		protected PHWorld3D m_phworld3d;

		public string pluginName
		{
			get
			{
				return "physics";
			}
		}

		public PhysicsManager()
		{
			this.m_conf = null;
			this.m_phworld3d = null;
		}

		public void onPreInit()
		{
		}

		public void onInit()
		{
			ConfigManager configManager = CrossApp.singleton.getPlugin("conf") as ConfigManager;
			configManager.regFormatFunc("physics", new Action<Variant>(this._formatConfig));
		}

		public void onPostInit()
		{
		}

		protected void _formatConfig(Variant conf)
		{
		}

		public PHWorld3D createWorld3D(string id)
		{
			this.m_phworld3d = new PHWorld3D(id, this);
			bool flag = !this.m_worlds.ContainsKey(id);
			PHWorld3D result;
			if (flag)
			{
				this.m_worlds.Add(id, this.m_phworld3d);
				result = this.m_phworld3d;
			}
			else
			{
				result = (this.m_worlds[id] as PHWorld3D);
			}
			return result;
		}

		public IPHWorld getWorld(string id)
		{
			IPHWorld result = null;
			this.m_worlds.TryGetValue(id, out result);
			return result;
		}

		public void deleteWorld(string id)
		{
			this.m_worlds.Remove(id);
		}

		public void deleteWorld(IPHWorld world)
		{
			bool flag = world == null;
			if (!flag)
			{
				this.m_worlds.Remove(world.id);
			}
		}

		public void onFin()
		{
		}

		public void onResize(int width, int height)
		{
		}

		public void onPreRender(float tmSlice)
		{
		}

		public void onRender(float tmSlice)
		{
		}

		public void onPostRender(float tmSlice)
		{
		}

		public void onPreProcess(float tmSlice)
		{
		}

		public void onProcess(float tmSlice)
		{
		}

		public void onPostProcess(float tmSlice)
		{
		}
	}
}
