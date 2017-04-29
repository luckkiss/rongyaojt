using GameFramework;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3tips_summon : Window
	{
		private uint curid;

		private a3_BagItemData item_data;

		private int cur_num;

		private Action backevent;

		private bool needEvent = true;

		public override void init()
		{
			BaseButton baseButton = new BaseButton(base.transform.FindChild("touch"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onclose);
			BaseButton baseButton2 = new BaseButton(base.transform.FindChild("info/btn_identify"), 1, 1);
			baseButton2.onClick = new Action<GameObject>(this.onidentify);
			BaseButton baseButton3 = new BaseButton(base.transform.FindChild("info/sell"), 1, 1);
			baseButton3.onClick = new Action<GameObject>(this.onsell);
			BaseButton baseButton4 = new BaseButton(base.transform.FindChild("details/info/action/sell/Button"), 1, 1);
			baseButton4.onClick = new Action<GameObject>(this.onsell);
			BaseButton baseButton5 = new BaseButton(base.transform.FindChild("details/info/action/use/Button"), 1, 1);
			baseButton5.onClick = new Action<GameObject>(this.onuse);
		}

		public override void onShowed()
		{
			base.transform.SetAsLastSibling();
			bool flag = this.uiData == null;
			if (!flag)
			{
				bool flag2 = this.uiData.Count != 0;
				if (flag2)
				{
					this.item_data = (a3_BagItemData)this.uiData[0];
					bool flag3 = this.uiData.Count > 2;
					if (flag3)
					{
						this.backevent = (Action)this.uiData[2];
					}
					bool flag4 = !ModelBase<A3_SummonModel>.getInstance().IsBaby(this.item_data);
					if (flag4)
					{
						base.transform.transform.FindChild("info").gameObject.SetActive(false);
						Transform transform = base.transform.transform.FindChild("details");
						bool flag5 = transform != null;
						if (flag5)
						{
							transform.gameObject.SetActive(true);
						}
						this.initItemDetail();
					}
					else
					{
						base.transform.transform.FindChild("info").gameObject.SetActive(true);
						Transform transform2 = base.transform.transform.FindChild("details");
						bool flag6 = transform2 != null;
						if (flag6)
						{
							transform2.gameObject.SetActive(false);
						}
						this.initItemInfo();
					}
					bool flag7 = this.uiData.Count > 1;
					if (flag7)
					{
						base.transform.FindChild("details/info/action/sell").gameObject.SetActive(false);
						base.transform.FindChild("details/info/action/use").gameObject.SetActive(false);
					}
					else
					{
						base.transform.FindChild("details/info/action/sell").gameObject.SetActive(true);
						base.transform.FindChild("details/info/action/use").gameObject.SetActive(true);
					}
				}
			}
		}

		public override void onClosed()
		{
			bool flag = this.backevent != null;
			if (flag)
			{
				this.backevent();
			}
		}

		private void initItemInfo()
		{
			Transform transform = base.transform.FindChild("info");
			transform.FindChild("name").GetComponent<Text>().text = this.item_data.confdata.item_name;
			transform.FindChild("name").GetComponent<Text>().color = Globle.getColorByQuality(this.item_data.confdata.quality);
			transform.FindChild("desc").GetComponent<Text>().text = this.item_data.confdata.desc;
			transform.FindChild("value").GetComponent<Text>().text = this.item_data.confdata.value.ToString();
			Transform transform2 = transform.FindChild("icon");
			bool flag = transform2.childCount > 0;
			if (flag)
			{
				UnityEngine.Object.Destroy(transform2.GetChild(0).gameObject);
			}
			GameObject gameObject = IconImageMgr.getInstance().createA3ItemIcon(this.item_data, false, -1, 1f, false);
			gameObject.transform.SetParent(transform2, false);
			this.cur_num = 1;
			string arg_12F_0 = this.item_data.summondata.isSpecial ? "变异*" : "";
			transform.FindChild("grade").GetComponent<Text>().text = ModelBase<A3_SummonModel>.getInstance().IntGradeToStr(this.item_data.summondata.grade);
			transform.FindChild("type").GetComponent<Text>().text = ModelBase<A3_SummonModel>.getInstance().IntNaturalToStr(this.item_data.summondata.naturaltype);
		}

		private void initItemDetail()
		{
			Transform transform = base.transform.FindChild("details/info");
			transform.FindChild("name").GetComponent<Text>().text = this.item_data.summondata.name;
			transform.FindChild("name").GetComponent<Text>().color = Globle.getColorByQuality(this.item_data.summondata.grade + 1);
			transform.FindChild("basic/left/lv").GetComponent<Text>().text = "等级：" + this.item_data.summondata.level;
			transform.FindChild("basic/left/blood").GetComponent<Text>().text = "血脉：" + ((this.item_data.summondata.blood > 1) ? "光" : "暗");
			string arg_F7_0 = this.item_data.summondata.isSpecial ? "变异*" : "";
			transform.FindChild("basic/left/grade").GetComponent<Text>().text = "品质：" + ModelBase<A3_SummonModel>.getInstance().IntGradeToStr(this.item_data.summondata.grade);
			transform.FindChild("basic/right/lifespan").GetComponent<Text>().text = "寿命：" + this.item_data.summondata.lifespan;
			transform.FindChild("basic/right/luck").GetComponent<Text>().text = "幸运：" + this.item_data.summondata.luck;
			transform.FindChild("basic/right/type").GetComponent<Text>().text = "类型：" + ModelBase<A3_SummonModel>.getInstance().IntNaturalToStr(this.item_data.summondata.naturaltype);
			transform.FindChild("natural/values/1").GetComponent<Text>().text = "攻：" + this.item_data.summondata.attNatural;
			transform.FindChild("natural/values/2").GetComponent<Text>().text = "防：" + this.item_data.summondata.defNatural;
			transform.FindChild("natural/values/3").GetComponent<Text>().text = "敏：" + this.item_data.summondata.agiNatural;
			transform.FindChild("natural/values/4").GetComponent<Text>().text = "体：" + this.item_data.summondata.conNatural;
			transform.FindChild("att/values/1").GetComponent<Text>().text = "生命：" + this.item_data.summondata.maxhp;
			transform.FindChild("att/values/2").GetComponent<Text>().text = string.Concat(new object[]
			{
				"攻击：",
				this.item_data.summondata.min_attack,
				" ~ ",
				this.item_data.summondata.max_attack
			});
			transform.FindChild("att/values/3").GetComponent<Text>().text = "物理防御：" + this.item_data.summondata.physics_def;
			transform.FindChild("att/values/4").GetComponent<Text>().text = "魔法防御：" + this.item_data.summondata.magic_def;
			transform.FindChild("att/values/5").GetComponent<Text>().text = "物伤减免：" + this.item_data.summondata.physics_dmg_red;
			transform.FindChild("att/values/6").GetComponent<Text>().text = "魔伤减免：" + this.item_data.summondata.magic_dmg_red;
			transform.FindChild("att/values/7").GetComponent<Text>().text = "暴击率：" + this.item_data.summondata.double_damage_rate;
			transform.FindChild("att/values/8").GetComponent<Text>().text = "回避率：" + this.item_data.summondata.reflect_crit_rate;
			Transform starRoot = transform.FindChild("stars");
			this.SetStar(starRoot, this.item_data.summondata.star);
			Transform transform2 = transform.FindChild("icon");
			bool flag = transform2.childCount > 0;
			if (flag)
			{
				UnityEngine.Object.Destroy(transform2.GetChild(0).gameObject);
			}
			this.item_data.confdata.borderfile = "icon/itemborder/b039_0" + (this.item_data.summondata.grade + 1);
			GameObject gameObject = IconImageMgr.getInstance().createA3ItemIcon(this.item_data, false, -1, 1f, false);
			gameObject.transform.SetParent(transform2, false);
			Transform transform3 = transform.FindChild("skill/values");
			for (int i = 0; i < transform3.childCount; i++)
			{
				bool flag2 = this.item_data.summondata.skills.ContainsKey(i + 1);
				if (flag2)
				{
					SXML sXML = XMLMgr.instance.GetSXML("skill.skill", "id==" + this.item_data.summondata.skills[i + 1]);
					transform3.GetChild(i).FindChild("skill").gameObject.SetActive(true);
					SXML sXML2 = XMLMgr.instance.GetSXML("skill.skill", "id==" + this.item_data.summondata.skills[i + 1]);
					transform3.GetChild(i).FindChild("skill").GetComponent<Image>().sprite = (Resources.Load("icon/smskill/" + sXML2.getInt("icon"), typeof(Sprite)) as Sprite);
					transform3.GetChild(i).GetComponent<Text>().text = sXML.getString("name");
				}
				else
				{
					transform3.GetChild(i).GetComponent<Text>().text = "";
					transform3.GetChild(i).FindChild("skill").gameObject.SetActive(false);
				}
			}
		}

		private void onclose(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3TIPS_SUMMON);
		}

		private void SetStar(Transform starRoot, int num)
		{
			int num2 = 0;
			Transform[] componentsInChildren = starRoot.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				bool flag = transform.parent != null && transform.parent.parent == starRoot.transform;
				if (flag)
				{
					bool flag2 = num2 < num;
					if (flag2)
					{
						transform.gameObject.SetActive(true);
						num2++;
					}
					else
					{
						transform.gameObject.SetActive(false);
					}
				}
			}
		}

		private void onsell(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3TIPS_SUMMON);
			BaseProxy<BagProxy>.getInstance().sendSellItems(this.item_data.id, 1);
		}

		private void onuse(GameObject go)
		{
			Debug.Log("Use Item");
			InterfaceMgr.getInstance().close(InterfaceMgr.A3TIPS_SUMMON);
			BaseProxy<A3_SummonProxy>.getInstance().sendUseSummonFromBag((int)this.item_data.id);
		}

		private void onidentify(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3TIPS_SUMMON);
			ArrayList arrayList = new ArrayList();
			arrayList.Add(1);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_SUMMON, arrayList, false);
		}
	}
}
