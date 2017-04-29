using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	public class LvlUpAwardPanel : BaseAwardCenter
	{
		private Dictionary<uint, awardCenterItem4zhuan> lvlUpAwardsDic;

		private string strDesc = "角色等级到达{0}转";

		private Transform root;

		private Transform itemsParent;

		public LvlUpAwardPanel(Transform trans) : base(trans)
		{
			this.root = trans;
			this.lvlUpAwardsDic = new Dictionary<uint, awardCenterItem4zhuan>();
			this.itemsParent = this.root.FindChild("awardItems/content");
			List<WelfareModel.itemWelfareData> levelReward = ModelBase<WelfareModel>.getInstance().getLevelReward();
			foreach (WelfareModel.itemWelfareData current in levelReward)
			{
				WelfareModel.itemWelfareData iwd = current;
				iwd.desc = this.strDesc;
				a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(current.itemId);
				GameObject gameObject = IconImageMgr.getInstance().creatItemAwardCenterIcon(itemDataById);
				gameObject.name = "itemWelfare";
				gameObject.transform.SetParent(this.itemsParent);
				gameObject.transform.localScale = Vector3.one;
				awardCenterItem4zhuan value = new awardCenterItem4zhuan(gameObject.transform, iwd);
				uint id = current.itemId;
				new BaseButton(gameObject.transform.FindChild("icon"), 1, 1).onClick = delegate(GameObject oo)
				{
					bool flag = a3_awardCenter.instan;
					if (flag)
					{
						a3_awardCenter.instan.showtip(id);
					}
				};
				this.lvlUpAwardsDic.Add(current.id, value);
			}
			this.itemsParent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (float)(100 * (this.lvlUpAwardsDic.Count + 2)));
			BaseProxy<welfareProxy>.getInstance().addEventListener(welfareProxy.UPLEVELAWARD, new Action<GameEvent>(this.onUpLevelAward));
		}

		public override void onShowed()
		{
			bool flag = BaseProxy<welfareProxy>.getInstance().dengjijiangli == null;
			if (!flag)
			{
				List<uint> list = new List<uint>();
				list.Clear();
				foreach (Variant current in BaseProxy<welfareProxy>.getInstance().dengjijiangli)
				{
					uint num = current;
					list.Add(num);
					bool flag2 = this.lvlUpAwardsDic.ContainsKey(num);
					if (flag2)
					{
						this.lvlUpAwardsDic[num].transform.FindChild("state/imgChecked").gameObject.SetActive(true);
						this.lvlUpAwardsDic[num].transform.FindChild("state/btnGet").gameObject.SetActive(false);
						this.lvlUpAwardsDic[num].transform.SetAsLastSibling();
					}
				}
				foreach (KeyValuePair<uint, awardCenterItem4zhuan> current2 in this.lvlUpAwardsDic)
				{
					bool flag3 = list.Contains(current2.Key);
					if (!flag3)
					{
						bool flag4 = current2.Value.m_iwd.zhuan > ModelBase<PlayerModel>.getInstance().up_lvl;
						if (flag4)
						{
							this.lvlUpAwardsDic[current2.Key].CanNotCheck();
						}
						else
						{
							this.lvlUpAwardsDic[current2.Key].CanCheck();
						}
					}
				}
			}
		}

		private void onUpLevelAward(GameEvent e)
		{
			uint key = e.data["gift_id"];
			bool flag = this.lvlUpAwardsDic.ContainsKey(key);
			if (flag)
			{
				this.lvlUpAwardsDic[key].Checked();
			}
		}
	}
}
