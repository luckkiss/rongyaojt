using Cross;
using System;
using System.Collections.Generic;

namespace GameFramework
{
	public class clientBase : GameEventDispatcherCollections, IClientBase, IGameEventDispatcherCollections
	{
		private gameMain _main;

		private Dictionary<string, Func<IClientBase, IObjectPlugin>> m_creators = new Dictionary<string, Func<IClientBase, IObjectPlugin>>();

		protected Dictionary<string, IObjectPlugin> m_objectPlugins = new Dictionary<string, IObjectPlugin>();

		public NetClient g_netM
		{
			get
			{
				return this._main.g_netM as NetClient;
			}
		}

		public gameManager g_gameM
		{
			get
			{
				return this._main.g_gameM as gameManager;
			}
		}

		public GRClient g_sceneM
		{
			get
			{
				return this._main.g_sceneM as GRClient;
			}
		}

		public UIClient g_uiM
		{
			get
			{
				return this._main.g_uiM as UIClient;
			}
		}

		public ClientConfig g_gameConfM
		{
			get
			{
				return this._main.g_gameConfM as ClientConfig;
			}
		}

		public processManager g_processM
		{
			get
			{
				return CrossApp.singleton.getPlugin("processManager") as processManager;
			}
		}

		public LocalizeManager g_localizeM
		{
			get
			{
				return CrossApp.singleton.getPlugin("localize") as LocalizeManager;
			}
		}

		public clientBase(gameMain m)
		{
			this._main = m;
		}

		public virtual void init()
		{
		}

		public void regCreator(string name, Func<IClientBase, IObjectPlugin> creator)
		{
			bool flag = this.m_creators.ContainsKey(name);
			if (flag)
			{
				DebugTrace.print("gameManagerBase regCreator [" + name + "] exsit!");
			}
			else
			{
				this.m_creators[name] = creator;
			}
		}

		public IObjectPlugin createInst(string name, bool single)
		{
			bool flag = !this.m_creators.ContainsKey(name);
			IObjectPlugin result;
			if (flag)
			{
				DebugTrace.print("err gameManagerBase createInst [" + name + "] notExsit!");
				result = null;
			}
			else
			{
				bool flag2 = single && this.m_objectPlugins.ContainsKey(name);
				if (flag2)
				{
					DebugTrace.print("err gameManagerBase createInst single [" + name + "] repeated!");
					result = this.m_objectPlugins[name];
				}
				else
				{
					Func<IClientBase, IObjectPlugin> func = this.m_creators[name];
					IObjectPlugin objectPlugin = func(this);
					objectPlugin.controlId = name;
					if (single)
					{
						base.regEventDispatcher(name, objectPlugin as IGameEventDispatcher);
						this.m_objectPlugins[name] = objectPlugin;
					}
					result = objectPlugin;
				}
			}
			return result;
		}

		public bool hasCreator(string name)
		{
			return this.m_creators.ContainsKey(name);
		}

		public void createAllSingleInst()
		{
			foreach (string current in this.m_creators.Keys)
			{
				Func<IClientBase, IObjectPlugin> func = this.m_creators[current];
				IObjectPlugin objectPlugin = func(this);
				objectPlugin.controlId = current;
				this.m_objectPlugins[current] = objectPlugin;
				base.regEventDispatcher(current, objectPlugin as IGameEventDispatcher);
			}
		}

		public IObjectPlugin getObject(string objectName)
		{
			bool flag = !this.m_objectPlugins.ContainsKey(objectName);
			IObjectPlugin result;
			if (flag)
			{
				DebugTrace.print("ERR gameManagerBase getObject [" + objectName + "] not exsit!");
				result = null;
			}
			else
			{
				result = this.m_objectPlugins[objectName];
			}
			return result;
		}
	}
}
