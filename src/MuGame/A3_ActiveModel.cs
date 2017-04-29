using Cross;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class A3_ActiveModel : ModelBase<A3_ActiveModel>
	{
		public int mwlr_charges = 0;

		public Variant mwlr_map_info = 0;

		public int mwlr_doubletime = 0;

		public int mwlr_totaltime = 0;

		public List<int> mwlr_map_id = new List<int>();

		public Dictionary<int, Vector3> mwlr_mons_pos = new Dictionary<int, Vector3>();

		public bool mwlr_giveup;

		public List<int> listKilled;

		public bool _mwlr_on;

		public int nowlvl;

		public int pvpCount;

		public int buyCount;

		public int callenge_cnt;

		public int buy_cnt;

		public int score;

		public int grade;

		public int lastgrage;

		public int buy_zuan_count;

		public Vector3 mwlr_target_pos
		{
			get;
			set;
		}

		public bool mwlr_on
		{
			get
			{
				return this._mwlr_on;
			}
			set
			{
				this._mwlr_on = value;
				bool mwlr_on = this._mwlr_on;
				if (mwlr_on)
				{
					bool flag = this.mwlr_target_monId < 0;
					if (flag)
					{
						this._mwlr_on = false;
					}
					else
					{
						bool flag2 = !ModelBase<PlayerModel>.getInstance().task_monsterIdOnAttack.ContainsKey(-1);
						if (flag2)
						{
							ModelBase<PlayerModel>.getInstance().task_monsterIdOnAttack.Add(-1, this.mwlr_target_monId);
						}
						else
						{
							ModelBase<PlayerModel>.getInstance().task_monsterIdOnAttack[-1] = this.mwlr_target_monId;
						}
					}
				}
				else
				{
					bool flag3 = ModelBase<PlayerModel>.getInstance().task_monsterIdOnAttack.ContainsValue(this.mwlr_target_monId);
					if (flag3)
					{
						this.mwlr_target_pos = Vector3.zero;
						ModelBase<PlayerModel>.getInstance().task_monsterIdOnAttack.Remove(-1);
						this.mwlr_target_monId = -1;
					}
				}
			}
		}

		public int mwlr_target_monId
		{
			get;
			set;
		}

		public int mwlr_mon_killed
		{
			get;
			set;
		}

		public A3_ActiveModel()
		{
			this.<mwlr_target_pos>k__BackingField = Vector3.zero;
			this.nowlvl = 0;
			this.pvpCount = 0;
			this.buyCount = 0;
			this.callenge_cnt = 0;
			this.buy_cnt = 0;
			this.score = 0;
			this.grade = 0;
			this.lastgrage = 0;
			this.buy_zuan_count = 0;
			base..ctor();
			SXML sXML = XMLMgr.instance.GetSXML("jjc.info", "");
			this.buy_cnt = ModelBase<A3_VipModel>.getInstance().vip_exchange_num(7);
			this.callenge_cnt = sXML.getInt("callenge_cnt");
			this.buy_zuan_count = sXML.getInt("buy_cost");
			this.listKilled = new List<int>();
		}
	}
}
