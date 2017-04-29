using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class lgGDSkill : lgGDBase
	{
		private Variant skill_list = new Variant();

		private Variant _manualSkills = null;

		public lgGDSkill(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new lgGDSkill(m as gameManager);
		}

		public override void init()
		{
			this.g_mgr.g_netM.addEventListener(85u, new Action<GameEvent>(this.onGetSkillInfo));
			this.g_mgr.g_netM.addEventListener(90u, new Action<GameEvent>(this.onDecCost));
			this.g_mgr.g_netM.addEventListener(87u, new Action<GameEvent>(this.onSetUpSkill));
		}

		private void onGetSkillInfo(GameEvent e)
		{
			Variant data = e.data;
			this.set_skill_list(data);
		}

		private void onLearnSkill(GameEvent e)
		{
			Variant data = e.data;
			this.learn_skill(data);
		}

		private void onDecCost(GameEvent e)
		{
			Variant data = e.data;
			this.decCost(data);
		}

		private void onSetUpSkill(GameEvent e)
		{
			Variant data = e.data;
			this.setUpSkill(data);
		}

		public Variant get_skill_list()
		{
			return this.skill_list;
		}

		public void set_skill_list(Variant msgData)
		{
		}

		public void learn_skill(Variant msgData)
		{
			this.skill_list[msgData["skid"]._str] = msgData;
			LGIUIMainUI lGIUIMainUI = (this.g_mgr.g_uiM as muUIClient).getLGUI("LGUIMainUIImpl") as LGIUIMainUI;
			lGIUIMainUI.addNewSkill(msgData);
		}

		public void decCost(Variant msgData)
		{
			bool flag = msgData.ContainsKey("gld_cost");
			if (flag)
			{
			}
			bool flag2 = msgData.ContainsKey("yb_cost");
			if (flag2)
			{
			}
			bool flag3 = msgData.ContainsKey("exp_cost");
			if (flag3)
			{
				Variant variant = new Variant();
				variant["added"] = -msgData["exp_cost"]._int;
			}
		}

		public void setUpSkill(Variant msgData)
		{
			this.skill_list[msgData["skid"]]["sklvl"] = msgData["sklvl"];
			LGIUISkill lGIUISkill = (this.g_mgr.g_uiM as muUIClient).getLGUI("skill") as LGIUISkill;
			lGIUISkill.setSkillUp(this.skill_list[msgData["skid"]]);
		}

		public Variant GetSkillData(uint skid)
		{
			Variant result;
			foreach (Variant current in this.skill_list._arr)
			{
				bool flag = skid == current["skid"];
				if (flag)
				{
					result = current;
					return result;
				}
			}
			result = null;
			return result;
		}

		public bool IsManualSkill(uint sid)
		{
			bool flag = this._manualSkills == null;
			if (flag)
			{
				this._manualSkills = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.GetManualSkill();
			}
			bool result;
			for (int i = 0; i < this._manualSkills.Length; i++)
			{
				bool flag2 = this._manualSkills[i] == sid;
				if (flag2)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public void SetSkillCD(Variant data, Variant cds)
		{
			float num = (float)(this.g_mgr.g_netM as muNetCleint).CurServerTimeStampMS;
			float val = num + (float)(data["cdtm"] * 100);
			foreach (Variant current in this.skill_list._arr)
			{
				bool flag = cds.ContainsKey(current["skid"]._str);
				if (flag)
				{
					current["cd"] = cds[current["skid"]]["start_tm"] + cds[current["skid"]]["cd_tm"] * 100;
				}
				else
				{
					current["cd"] = val;
				}
			}
		}

		public Variant GetSkillById(uint skid)
		{
			return this.skill_list[skid];
		}
	}
}
