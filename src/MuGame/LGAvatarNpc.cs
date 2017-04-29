using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class LGAvatarNpc : LGAvatarGameInst
	{
		public static Dictionary<int, TalkMainData> dTalk = new Dictionary<int, TalkMainData>();

		public int dialogId = -1;

		private List<int> titleImgs = new List<int>();

		private uint _npcid;

		private TalkMainData curTalkDta;

		public uint npcid
		{
			get
			{
				return this._npcid;
			}
			set
			{
				this._npcid = value;
			}
		}

		public override string processName
		{
			get
			{
				return "LGAvatarNpc";
			}
			set
			{
				this._processName = value;
			}
		}

		public LGAvatarNpc(gameManager m) : base(m)
		{
		}

		public override uint getNid()
		{
			return this._npcid;
		}

		public void doTalk()
		{
			bool flag = this.dialogId == -1;
			if (!flag)
			{
				string text = SvrNPCConfig.instance.get_dialog(this.dialogId);
				bool flag2 = text == "";
				if (!flag2)
				{
					bool flag3 = LGAvatarNpc.dTalk.ContainsKey(this.dialogId);
					if (flag3)
					{
						this.curTalkDta = LGAvatarNpc.dTalk[this.dialogId];
					}
					else
					{
						TalkMainData talkMainData = new TalkMainData();
						talkMainData.init(text, this._npcid.ToString(), this.viewInfo["name"]);
						LGAvatarNpc.dTalk[this.dialogId] = talkMainData;
						this.curTalkDta = talkMainData;
					}
					this.curTalkDta.beginTalk(this);
				}
			}
		}

		public void initData(Variant info)
		{
			this.viewInfo = info;
			this._npcid = info["nid"];
			this.viewInfo["ori"] = info["octOri"]._int;
			this.g_mgr.g_sceneM.dispatchEvent(GameEvent.Createimmedi(2182u, this, null, false));
			base.dispatchEvent(GameEvent.Create(2100u, this, this.viewInfo, false));
		}

		public override void updateProcess(float tmSlice)
		{
		}

		protected override void onClick(GameEvent e)
		{
			base.onClick(e);
			GameTools.PrintNotice("npc click [" + this.getNid() + "] ");
			base.lgMainPlayer.onSelectNpc(this);
		}
	}
}
