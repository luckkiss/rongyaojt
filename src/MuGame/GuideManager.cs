using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class GuideManager
	{
		public static GuideManager singleton;

		public static int GT_ROOKIE = 1;

		public static int GT_NORMAL = 2;

		protected UIClient _uiClient;

		protected bool _canGuide = true;

		private Dictionary<string, GuideData> _currGuides;

		protected Dictionary<string, Variant> _currGuideByTp;

		protected Dictionary<uint, IGuideUI> _guideUIs;

		protected Variant _finGuides;

		protected List<Variant> _waitDoAction = new List<Variant>();

		private processStruct funProcess = processStruct.create(null, "", false, false);

		private Dictionary<string, Func<IClientBase, IGuideUI>> _uiClass = new Dictionary<string, Func<IClientBase, IGuideUI>>();

		private Variant _hideFlags = new Variant();

		public GuideManager(muUIClient m)
		{
			GuideManager.singleton = this;
			this._uiClient = m;
			this._currGuides = new Dictionary<string, GuideData>();
			this._currGuideByTp = new Dictionary<string, Variant>();
			this._guideUIs = new Dictionary<uint, IGuideUI>();
			this._finGuides = new Variant();
			this.funProcess.update = new Action<float>(this.process);
		}

		public bool CanGuide()
		{
			return this._canGuide;
		}

		public void SetCanGuide(bool b)
		{
			bool flag = this._canGuide != b;
			if (flag)
			{
				this._canGuide = b;
				bool flag2 = !this._canGuide;
				if (flag2)
				{
					foreach (string current in this._currGuideByTp.Keys)
					{
						bool flag3 = current != null;
						if (flag3)
						{
							this.stopGuideImpl(current);
						}
					}
				}
			}
		}

		public void StopCurrGuide(uint tp)
		{
			string text = this._currGuideByTp[tp.ToString()];
			bool flag = text != null;
			if (flag)
			{
				this.stopGuideImpl(text);
			}
		}

		protected void stopGuideImpl(string id)
		{
			GuideData guideData = this._currGuides[id];
			bool flag = guideData != null;
			if (flag)
			{
				guideData.ui.StopGuide();
				guideData.ui.SetVisible(false);
				this._currGuides.Remove(id);
				this._currGuideByTp[guideData.conf["tp"]] = null;
			}
		}

		public bool InTpGuiding(uint tp)
		{
			return this._currGuideByTp.ContainsKey(tp.ToString()) && this._currGuideByTp[tp.ToString()] != null;
		}

		public string GetCurrGuideID(uint tp)
		{
			return this._currGuideByTp.ContainsKey(tp.ToString()) ? this._currGuideByTp[tp.ToString()] : null;
		}

		public string GetCurrGuideStep(string id)
		{
			GuideData guideData = this._currGuides[id];
			return (guideData != null && guideData.currStep != null) ? guideData.currStep["id"] : null;
		}

		public Variant GetUserdata(uint id)
		{
			GuideData guideData = this._currGuides[id.ToString()];
			return (guideData != null) ? guideData.userdata : null;
		}

		public bool Start(string id, Variant userdata = null, Action stopFun = null)
		{
			bool flag = !this._canGuide;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Variant guideConf = (this._uiClient.g_gameConfM as muCLientConfig).localGuild.GetGuideConf(id.ToString());
				bool flag2 = guideConf == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					Variant variant = guideConf["step"];
					bool flag3 = variant.Count == 0;
					if (flag3)
					{
						result = false;
					}
					else
					{
						bool flag4 = this.isGuided(guideConf);
						if (flag4)
						{
							result = false;
						}
						else
						{
							bool flag5 = guideConf.ContainsKey("level");
							if (flag5)
							{
								bool flag6 = guideConf["level"]._uint < (this._uiClient.g_netM as muNetCleint).joinWorldInfoInst.level;
								if (flag6)
								{
									result = false;
									return result;
								}
							}
							IGuideUI guideUI = null;
							bool flag7 = this._guideUIs.Count > 0;
							if (flag7)
							{
								guideUI = this._guideUIs[guideConf["tp"]];
							}
							bool flag8 = guideUI == null;
							if (flag8)
							{
								Variant guideUIConf = (this._uiClient.g_gameConfM as muCLientConfig).localGuild.GetGuideUIConf();
								bool flag9 = guideUIConf == null;
								if (flag9)
								{
									result = false;
									return result;
								}
								guideUI = this.createGuideUI(guideUIConf["template"]);
								guideUI.InitFrame();
								this._guideUIs[guideConf["tp"]] = guideUI;
								guideUI.SetPointAt(guideUIConf["ptx"], guideUIConf["pty"]);
							}
							string text = null;
							bool flag10 = this._currGuideByTp.Count > 0 && this._currGuideByTp[guideConf["tp"]] != null;
							if (flag10)
							{
								text = this._currGuideByTp[guideConf["tp"]];
							}
							bool flag11 = text != null;
							GuideData guideData;
							if (flag11)
							{
								bool flag12 = text == id;
								if (flag12)
								{
									result = false;
									return result;
								}
								guideData = this._currGuides[text];
								bool flag13 = guideConf["weight"]._int < guideData.conf["weight"]._int;
								if (flag13)
								{
									result = false;
									return result;
								}
								guideUI.ClearGuide();
								this._currGuides.Remove(text);
							}
							this._currGuideByTp[guideConf["tp"]] = id;
							guideData = new GuideData();
							this._currGuides[id.ToString()] = guideData;
							guideData.userdata = userdata;
							guideData.stopFun = stopFun;
							guideData.conf = guideConf;
							guideData.currStep = null;
							guideData.nextStep = 0;
							guideData.ui = guideUI;
							guideUI.StartGuide();
							bool flag14 = guideConf.ContainsKey("tm");
							if (flag14)
							{
								this.addWaitDoAction("stopguide", (int)(GameTools.getTimer() + (long)guideConf["tm"]._int), id);
							}
							bool flag15 = guideConf.ContainsKey("stdo");
							if (flag15)
							{
								this.addWaitDoAction("startlink", 0, guideConf["stdo"]);
							}
							result = true;
						}
					}
				}
			}
			return result;
		}

		public void TryFin(string id)
		{
			bool flag = !this._currGuides.ContainsKey(id);
			if (!flag)
			{
				GuideData guideData = this._currGuides[id];
				bool flag2 = guideData == null;
				if (!flag2)
				{
					bool flag3 = true;
					Variant conf = guideData.conf;
					Variant variant = conf["step"];
					bool flag4 = variant.Count <= guideData.nextStep;
					if (flag4)
					{
						this.saveShareFlag(conf);
						bool flag5 = guideData.stopFun != null;
						if (flag5)
						{
							guideData.stopFun();
						}
					}
					else
					{
						bool flag6 = guideData.currStep != null;
						if (flag6)
						{
							bool flag7 = conf.ContainsKey("restart") && conf["restart"] != null;
							if (flag7)
							{
								guideData.desc = null;
								guideData.target = null;
								guideData.currStep = variant[0];
								guideData.nextStep = 1;
								this.updateGuideUI(guideData.ui, guideData);
								flag3 = false;
							}
						}
					}
					bool flag8 = flag3;
					if (flag8)
					{
						bool flag9 = conf.ContainsKey("findo");
						if (flag9)
						{
							this.addWaitDoAction("finlink", 0, conf["findo"]);
						}
						this.stopGuideImpl(id);
					}
				}
			}
		}

		protected void addWaitDoAction(string fun, int tm, Variant data)
		{
			Variant variant = new Variant();
			variant["tm"] = tm;
			variant["fun"] = fun;
			variant["data"] = data;
			this._waitDoAction.Add(variant);
			bool flag = this._waitDoAction.Count == 1;
			if (flag)
			{
				this._uiClient.addProcess(this.funProcess);
			}
		}

		public void GuideStep(string id, string step, Rect target = null, string desc = null)
		{
			bool flag = !this._canGuide;
			if (!flag)
			{
				GuideData guideData = this._currGuides[id];
				bool flag2 = guideData != null;
				if (flag2)
				{
					this.guideStep(guideData, step, target, desc);
				}
			}
		}

		public void RegGuideUIClass(string name, Func<IClientBase, IGuideUI> cls)
		{
			this._uiClass[name] = cls;
		}

		protected IGuideUI createGuideUI(string name)
		{
			return this._uiClient.getLGUI(name) as IGuideUI;
		}

		private void guideStep(GuideData currGuide, string step, Rect target = null, string desc = null)
		{
			Variant variant = currGuide.conf["step"];
			bool flag = variant.Count > currGuide.nextStep;
			if (flag)
			{
				for (int i = currGuide.nextStep; i < variant.Count; i++)
				{
					Variant variant2 = variant[i];
					bool flag2 = step == variant2["id"]._str;
					if (flag2)
					{
						currGuide.currStep = variant2;
						currGuide.nextStep = i + 1;
						currGuide.target = target;
						currGuide.desc = desc;
						this.updateGuideUI(currGuide.ui, currGuide);
						break;
					}
				}
			}
		}

		private void updateGuideUI(IGuideUI uiGuide, GuideData data)
		{
			bool flag = uiGuide == null;
			if (!flag)
			{
				Variant currStep = data.currStep;
				bool flag2 = currStep != null && currStep.ContainsKey("empty") && currStep["empty"] != null;
				if (flag2)
				{
					uiGuide.SetVisible(false);
				}
				else
				{
					uiGuide.SetVisible(this._hideFlags._arr.IndexOf(data.conf["tp"]) < 0);
					string guideDesc = null;
					bool flag3 = data.desc != null;
					if (flag3)
					{
						guideDesc = LanguagePack.getLanguageText("guidedesc", data.desc);
					}
					else
					{
						bool flag4 = currStep.ContainsKey("desc");
						if (flag4)
						{
							guideDesc = LanguagePack.getLanguageText("guidedesc", currStep["desc"]._str);
						}
					}
					uiGuide.SetGuideDesc(guideDesc);
					uiGuide.AdjustChilds(currStep["ctrl"]);
					uiGuide.SetParent(currStep["panel"]);
					uiGuide.AdjustTarget(data.target, currStep["target"], currStep["ori"]);
				}
			}
		}

		protected bool isGuided(Variant guideConf)
		{
			bool result = false;
			bool flag = guideConf.ContainsKey("once") && guideConf["once"]._bool;
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = !guideConf.ContainsKey("guided");
				if (flag3)
				{
					string shareFlag = this.getShareFlag(guideConf["type"], guideConf["id"]);
				}
				result = guideConf["guided"]._bool;
			}
			return result;
		}

		private void saveShareFlag(Variant guideConf)
		{
			bool flag = guideConf.ContainsKey("once") && guideConf["once"]._bool;
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = guideConf["guided"];
				bool flag4 = !flag3;
				if (flag4)
				{
					string shareFlag = this.getShareFlag(guideConf["type"], guideConf["id"]);
					guideConf["guided"] = true;
				}
			}
		}

		private string getShareFlag(string type, string guideName)
		{
			return "guide" + type + guideName;
		}

		public void AddHideFlag(int flag)
		{
			bool flag2 = !this._canGuide;
			if (!flag2)
			{
				bool flag3 = this._hideFlags._arr.IndexOf(flag) >= 0;
				if (!flag3)
				{
					this._hideFlags.pushBack(flag);
					foreach (GuideData current in this._currGuides.Values)
					{
						bool flag4 = current.conf["tp"]._int == flag;
						if (flag4)
						{
							current.ui.SetVisible(false);
						}
					}
				}
			}
		}

		public void DelHideFlag(int flag)
		{
			bool flag2 = !this._canGuide;
			if (!flag2)
			{
				int num = this._hideFlags._arr.IndexOf(flag);
				bool flag3 = num < 0;
				if (!flag3)
				{
					this._hideFlags._arr.RemoveAt(num);
					foreach (GuideData current in this._currGuides.Values)
					{
						bool flag4 = current.conf["tp"]._int == flag;
						if (flag4)
						{
							current.ui.SetVisible(true);
						}
					}
				}
			}
		}

		public void DelAllHideFlag()
		{
			bool flag = !this._canGuide;
			if (!flag)
			{
				this._hideFlags = new Variant();
				foreach (GuideData current in this._currGuides.Values)
				{
					current.ui.SetVisible(true);
				}
			}
		}

		public void process(float tmSlice)
		{
			bool flag = this._waitDoAction.Count > 0;
			if (flag)
			{
				int num = (int)GameTools.getTimer();
				for (int i = this._waitDoAction.Count - 1; i >= 0; i--)
				{
					Variant variant = this._waitDoAction[i];
					bool flag2 = num < variant["tm"]._int;
					if (!flag2)
					{
						bool flag3 = variant["fun"]._str == "startlink";
						if (flag3)
						{
							string str = variant["data"];
							Variant variant2 = GameTools.split(str, ",", 1u);
							foreach (string current in variant2.Keys)
							{
								UILinksManager.singleton.UILinkEvent(current);
							}
						}
						else
						{
							bool flag4 = variant["fun"]._str == "finlink";
							if (flag4)
							{
								string str = variant["data"];
								Variant variant2 = GameTools.split(str, ",", 1u);
								foreach (string current2 in variant2.Keys)
								{
									UILinksManager.singleton.UILinkEvent(current2);
								}
							}
							else
							{
								bool flag5 = variant["fun"]._str == "stopguide";
								if (flag5)
								{
									this.stopGuideImpl(variant["data"]);
								}
							}
						}
						this._waitDoAction.RemoveAt(i);
					}
				}
			}
			else
			{
				this._uiClient.removeProcess(this.funProcess);
			}
		}
	}
}
