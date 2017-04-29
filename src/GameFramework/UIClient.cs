using Cross;
using System;
using System.Collections.Generic;

namespace GameFramework
{
	public abstract class UIClient : clientBase
	{
		public static UIClient instance;

		protected Dictionary<string, BaseLGUI> m_uiCtrlMap = new Dictionary<string, BaseLGUI>();

		protected Dictionary<string, IUI> m_uiMap = new Dictionary<string, IUI>();

		protected Dictionary<string, string> m_backgroundClickUIS = new Dictionary<string, string>();

		public timersManager timers = new timersManager();

		private List<loadUIStruct> _uiLoads = new List<loadUIStruct>();

		public UIClient(gameMain m) : base(m)
		{
		}

		public override void init()
		{
			new AttAniManager(this);
			UIClient.instance = this;
			base.addEventListener(4001u, new Action<GameEvent>(this.openUI));
			base.addEventListener(4002u, new Action<GameEvent>(this.closeUI));
			base.addEventListener(4003u, new Action<GameEvent>(this.openSwitch));
			base.addEventListener(3301u, new Action<GameEvent>(this.onPlaySound));
			base.g_processM.addProcess(new processStruct(new Action<float>(this._getUIProcess), "UIClient_getUIProcess", false, false), false);
			base.g_processM.addProcess(new processStruct(new Action<float>(AttAniManager.singleton.process), "UIClient_AttAniManagerProcess", false, false), false);
			this.onInit();
		}

		protected abstract void onInit();

		protected void regCreatorLGUI(string name, Func<IClientBase, IObjectPlugin> creator)
		{
			base.regCreator(this.tracsLguiName(name), creator);
		}

		protected void regCreatorUI(string name, Func<IClientBase, IObjectPlugin> creator)
		{
			base.regCreator(name, creator);
		}

		protected string tracsLguiName(string name)
		{
			return name + "LG";
		}

		private void openUI(GameEvent e)
		{
			Variant data = e.data;
			string uiname = data["name"];
			Variant data2 = null;
			bool flag = data.ContainsKey("data");
			if (flag)
			{
				data2 = data["data"];
			}
			this._openUI(uiname, data2);
		}

		private void openSwitch(GameEvent e)
		{
			Variant data = e.data;
			string text = data["name"];
			Variant data2 = null;
			bool flag = data.ContainsKey("data");
			if (flag)
			{
				data2 = data["data"];
			}
			bool flag2 = !this.m_uiCtrlMap.ContainsKey(text);
			if (flag2)
			{
				this._openUI(text, data2);
			}
			else
			{
				BaseLGUI baseLGUI = this.m_uiCtrlMap[text];
				bool isOpen = baseLGUI.isOpen;
				if (isOpen)
				{
					baseLGUI.close();
				}
				else
				{
					baseLGUI.open(data2);
				}
			}
		}

		protected void _openUI(string uiname, Variant data)
		{
			bool flag = !this.m_uiCtrlMap.ContainsKey(uiname);
			BaseLGUI baseLGUI;
			if (flag)
			{
				baseLGUI = (base.createInst(this.tracsLguiName(uiname), true) as BaseLGUI);
				bool flag2 = baseLGUI == null;
				if (flag2)
				{
					GameTools.PrintNotice(" createInst LGUIClass[ " + uiname + " ] err!");
				}
				baseLGUI.uiName = uiname;
				this.m_uiCtrlMap[uiname] = baseLGUI;
			}
			baseLGUI = this.m_uiCtrlMap[uiname];
			bool flag3 = !baseLGUI.isOpen;
			if (flag3)
			{
				baseLGUI.open(data);
			}
		}

		private void closeUI(GameEvent e)
		{
			Variant data = e.data;
			string uiname = data["name"];
			this._closeUI(uiname);
		}

		protected void _closeUI(string uiname)
		{
			bool flag = !this.m_uiCtrlMap.ContainsKey(uiname);
			if (!flag)
			{
				this.m_uiCtrlMap[uiname].close();
			}
		}

		public bool isUiOpened(string uiname)
		{
			bool flag = !this.m_uiCtrlMap.ContainsKey(uiname);
			return !flag && this.m_uiCtrlMap[uiname].isOpen;
		}

		public BaseLGUI getLGTUIInst(string lguiName, string uiName, Action<IUI, Variant> cb, Variant data)
		{
			bool flag = lguiName != "UI_BASE";
			BaseLGUI baseLGUI;
			BaseLGUI result;
			if (flag)
			{
				baseLGUI = (base.createInst(this.tracsLguiName(lguiName), false) as BaseLGUI);
				bool flag2 = baseLGUI == null;
				if (flag2)
				{
					GameTools.PrintNotice(" getLGTUIInst LGUIClass[" + lguiName + "]  err!!");
					result = null;
					return result;
				}
			}
			else
			{
				baseLGUI = new BaseLGUI(this);
			}
			baseLGUI.uiName = uiName;
			baseLGUI.bindui(cb, data);
			result = baseLGUI;
			return result;
		}

		public BaseLGUI getLGUI(string lguiName)
		{
			bool flag = !this.m_uiCtrlMap.ContainsKey(lguiName);
			BaseLGUI baseLGUI;
			BaseLGUI result;
			if (flag)
			{
				baseLGUI = (base.createInst(this.tracsLguiName(lguiName), true) as BaseLGUI);
				bool flag2 = baseLGUI == null;
				if (flag2)
				{
					GameTools.PrintNotice(" createInst LGUIClass[ " + lguiName + " ] err!");
					result = null;
					return result;
				}
				baseLGUI.uiName = lguiName;
				this.m_uiCtrlMap[lguiName] = baseLGUI;
			}
			baseLGUI = this.m_uiCtrlMap[lguiName];
			result = baseLGUI;
			return result;
		}

		public void getUI(string uiname, Action<IUI, Variant> cb, Variant data)
		{
			bool flag = !base.hasCreator(uiname);
			if (flag)
			{
				GameTools.PrintNotice("getUI[" + uiname + "]  err!!");
			}
			else
			{
				loadUIStruct loadUIStruct = new loadUIStruct();
				loadUIStruct.data = data;
				loadUIStruct.uiname = uiname;
				loadUIStruct.onLoadedCallback = cb;
				this._uiLoads.Add(loadUIStruct);
			}
		}

		private void _getUIProcess(float tmSlice)
		{
		}

		public void regBackgroundClick(string uiname)
		{
			this.m_backgroundClickUIS[uiname] = uiname;
		}

		public IUI getUIF(string name)
		{
			bool flag = !this.m_uiMap.ContainsKey(name);
			IUI result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.m_uiMap[name];
			}
			return result;
		}

		public virtual bool showLoading(BaseLGUI ui)
		{
			return false;
		}

		public virtual void onLoadingUI(BaseLGUI ui)
		{
		}

		public virtual void onLoadingUIEnd(BaseLGUI ui)
		{
		}

		public void addUiBase(IUI uif)
		{
		}

		public void rmUiBase(IUI uif)
		{
		}

		public IUIBaseControl getHirachyCreatedCtrlByID(string ids)
		{
			List<string> list = StringUtil.splitStr(ids, ".");
			IUI uIF = this.getUIF(list[0]);
			bool flag = uIF == null;
			IUIBaseControl result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IUIBaseControl baseCtrl = uIF.getBaseCtrl();
				bool flag2 = list.Count > 1;
				if (flag2)
				{
					bool flag3 = !(baseCtrl is IUIContainer);
					if (flag3)
					{
						result = null;
					}
					else
					{
						result = this.getHirachyUIByID(baseCtrl, list[1]);
					}
				}
				else
				{
					result = baseCtrl;
				}
			}
			return result;
		}

		public IUIBaseControl getHirachyUIByID(IUIBaseControl baseCtrl, string ids)
		{
			bool flag = baseCtrl == null;
			IUIBaseControl result;
			if (flag)
			{
				result = null;
			}
			else
			{
				List<string> list = StringUtil.splitStr(ids, ".");
				IUIBaseControl child = (baseCtrl as IUIContainer).getChild(list[0]);
				bool flag2 = list.Count > 1;
				if (flag2)
				{
					bool flag3 = !(child is IUIContainer);
					if (flag3)
					{
						result = null;
					}
					else
					{
						result = this.getHirachyUIByID(child, list[1]);
					}
				}
				else
				{
					result = child;
				}
			}
			return result;
		}

		protected virtual void onPlaySound(GameEvent e)
		{
		}

		public void removeProcess(IProcess p)
		{
			base.g_processM.removeProcess(p, false);
		}

		public void addProcess(IProcess p)
		{
			base.g_processM.addProcess(p, false);
		}
	}
}
