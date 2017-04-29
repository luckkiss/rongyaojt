using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class LGAvatarOther : LGAvatarGameInst
	{
		public override string processName
		{
			get
			{
				return "LGAvatarOther";
			}
			set
			{
				this._processName = value;
			}
		}

		public LGAvatarOther(gameManager m) : base(m)
		{
		}

		public override uint getIid()
		{
			return this.viewInfo["iid"]._uint;
		}

		public override uint getCid()
		{
			return this.viewInfo["cid"]._uint;
		}

		protected override void onClick(GameEvent e)
		{
			base.onClick(e);
			GameTools.PrintNotice("other click iid[" + this.getIid() + "] ");
			base.lgMainPlayer.onSelectOther(this);
		}

		public void initData(Variant info)
		{
			this.viewInfo = info;
			base.playerInfos.get_player_detailinfo(this.getCid(), new Action<Variant>(this.onGetDetialInfo));
			base.playerInfos.addActionDressChange(this.getCid(), new Action<Variant>(this.revDressChange));
		}

		private void onGetDetialInfo(Variant data)
		{
			bool destroy = base.destroy;
			if (!destroy)
			{
				foreach (string current in data.Keys)
				{
					this.viewInfo[current] = data[current];
				}
				bool flag = this.viewInfo.ContainsKey("moving");
				if (flag)
				{
					base.addMoving(this.viewInfo["moving"]);
				}
				bool flag2 = this.viewInfo.ContainsKey("atking");
				if (flag2)
				{
					base.addAttack(this.viewInfo["atking"]["tar_iid"]._uint, null);
				}
				this.g_mgr.g_sceneM.dispatchEvent(GameEvent.Createimmedi(2185u, this, null, false));
				bool flag3 = data.ContainsKey("dressments");
				if (flag3)
				{
					this.grAvatar.m_nSex = data["sex"]._int;
					List<Variant> arr = data["dressments"]._arr;
					foreach (Variant current2 in arr)
					{
						int @int = current2["dressid"]._int;
						SXML sXML = XMLMgr.instance.GetSXML("dress.dress_info", "id==" + current2["dressid"]._str);
						int int2 = sXML.getInt("dress_type");
						bool flag4 = int2 >= 0 && int2 < this.grAvatar.m_nOtherDress.Length;
						if (flag4)
						{
							this.grAvatar.m_nOtherDress[int2] = @int;
						}
					}
				}
				base.dispatchEvent(GameEvent.Create(2100u, this, this.viewInfo, false));
				base.addEventListener(2280u, new Action<GameEvent>(this.onClick));
			}
		}

		private void revDressChange(Variant data)
		{
			bool flag = this.grAvatar == null;
			if (!flag)
			{
				bool flag2 = data.ContainsKey("dressments");
				if (flag2)
				{
					List<Variant> arr = data["dressments"]._arr;
					int num = 0;
					foreach (Variant current in arr)
					{
						int num2 = num;
						int @int = current["dressid"]._int;
						bool flag3 = num2 >= 0 && num2 < this.grAvatar.m_nOtherDress.Length;
						if (flag3)
						{
							this.grAvatar.m_nOtherDress[num2] = @int;
						}
						num++;
					}
					this.grAvatar.RefreshOtherAvatar();
				}
			}
		}
	}
}
