using Cross;
using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_legion : BaseShejiao
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_legion.<>c <>9 = new a3_legion.<>c();

			public static Action<GameObject> <>9__26_7;

			public static Action<GameObject> <>9__26_12;

			internal void <init>b__26_7(GameObject go)
			{
				bool flag = FunctionOpenMgr.instance.checkLegion(FunctionOpenMgr.LEGION, false);
				if (flag)
				{
					ArrayList arrayList = new ArrayList();
					ModelBase<Shop_a3Model>.getInstance().selectnum = 4;
					arrayList.Add(4);
					InterfaceMgr.getInstance().open(InterfaceMgr.SHOP_A3, arrayList, false);
					shop_a3 expr_4B = shop_a3._instance;
					if (expr_4B != null)
					{
						expr_4B.transform.SetAsLastSibling();
					}
					shop_a3 expr_61 = shop_a3._instance;
					if (expr_61 != null)
					{
						expr_61.tab.setSelectedIndex(4, false);
					}
				}
				else
				{
					flytxt.instance.fly(ContMgr.getCont("func_limit_38", null), 0, default(Color), null);
				}
			}

			internal void <init>b__26_12(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_SLAY_DRAGON, null, false);
			}
		}

		public static a3_legion mInstance;

		private a3BaseLegion _currentAuction = null;

		private Dictionary<string, a3BaseLegion> _legions = new Dictionary<string, a3BaseLegion>();

		private TabControl _tabCtl;

		private BaseButton create_legion;

		private BaseButton join_legion;

		private Transform[] s;

		private Transform select;

		private Transform toggle;

		private Transform toggle0;

		private A3_LegionData selectData;

		private Transform content;

		private Text Avalable;

		private Text _txtNameKeyWord;

		private Text _txtBoardKeyWord;

		private string enterName = null;

		public Text gxdtext;

		private Toggle dic;

		private bool can_use;

		private bool can_jx;

		private SXML itemsXMl;

		public Toggle dic0;

		public bool movetoNPCdart = false;

		public a3_legion(Transform trans) : base(trans)
		{
			this.init();
		}

		public void Linit()
		{
			this._legions["info"] = new a3_legion_info(this, "contents/info");
			this._legions["member"] = new a3_legion_member(this, "contents/member");
			this._legions["active"] = new a3_legion_active(this, "contents/active");
			this._legions["application"] = new a3_legion_application(this, "contents/application");
			this._legions["diary"] = new a3_legion_diary(this, "contents/diary");
			this._tabCtl = this.InitLayout();
		}

		private TabControl InitLayout()
		{
			TabControl tabControl = new TabControl();
			List<Transform> contents = new List<Transform>();
			Transform transform = base.getGameObjectByPath("s4/contents").transform;
			Transform[] componentsInChildren = transform.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform2 = componentsInChildren[i];
				bool flag = transform2.parent == transform;
				if (flag)
				{
					transform2.gameObject.SetActive(false);
					contents.Add(transform2);
				}
			}
			TabControl expr_85 = tabControl;
			expr_85.onClickHanle = (Action<TabControl>)Delegate.Combine(expr_85.onClickHanle, new Action<TabControl>(delegate(TabControl tb)
			{
				bool flag2 = !this._legions.ContainsKey(contents[tb.getSeletedIndex()].name);
				if (flag2)
				{
					Debug.Log("没有界面配置");
					bool flag3 = this._legions.Count > 0;
					if (flag3)
					{
						tb.setSelectedIndex(tb.getIndexByName(new List<a3BaseLegion>(this._legions.Values)[0].pathStrName), false);
					}
				}
				else
				{
					for (int j = 0; j < contents.Count; j++)
					{
						bool flag4 = j != tb.getSeletedIndex();
						if (flag4)
						{
							contents[j].gameObject.SetActive(false);
						}
						else
						{
							contents[j].gameObject.SetActive(true);
						}
					}
					bool flag5 = this._currentAuction != null;
					if (flag5)
					{
						this._currentAuction.onClose();
					}
					bool flag6 = this._currentAuction != null && this._legions.ContainsKey(contents[tb.getSeletedIndex()].name);
					if (flag6)
					{
						this._currentAuction = this._legions[contents[tb.getSeletedIndex()].name];
						this._currentAuction.onShowed();
					}
					else
					{
						this._currentAuction = this._legions[contents[tb.getSeletedIndex()].name];
					}
				}
			}));
			tabControl.create(base.transform.FindChild("s4/tabs").gameObject, base.transform.FindChild("s4").gameObject, 0, 0, false);
			return tabControl;
		}

		private void init()
		{
			a3_legion.mInstance = this;
			this.Linit();
			this.gxdtext = base.getTransformByPath("s5/gxd/text").GetComponent<Text>();
			this.Avalable = base.getTransformByPath("s3/Avalable").GetComponent<Text>();
			this._txtNameKeyWord = base.getComponentByPath<Text>("s3/txtKeyWord");
			this._txtNameKeyWord.gameObject.SetActive(false);
			this._txtBoardKeyWord = base.getComponentByPath<Text>("s4/contents/info/rt/txtKeyWord");
			this._txtBoardKeyWord.gameObject.SetActive(false);
			this.content = base.getTransformByPath("s2/root/cells/scroll/content");
			this.select = base.getTransformByPath("s2/root/cells/scroll/select");
			this.toggle = base.getTransformByPath("s2/root/Toggle");
			this.dic = this.toggle.GetComponent<Toggle>();
			this.dic.onValueChanged.AddListener(delegate(bool b)
			{
				BaseProxy<A3_LegionProxy>.getInstance().SendGetList();
				this.RefreshAllList(null);
			});
			this.toggle0 = base.getTransformByPath("s1/Toggle");
			this.dic0 = this.toggle0.GetComponent<Toggle>();
			this.dic0.onValueChanged.AddListener(delegate(bool b)
			{
				this.dic0.isOn = b;
				BaseProxy<A3_LegionProxy>.getInstance().SendChangeToggleMode(b);
			});
			BaseProxy<A3_LegionProxy>.getInstance().addEventListener(26u, new Action<GameEvent>(this.Repair));
			this.s = new Transform[]
			{
				base.getTransformByPath("s1"),
				base.getTransformByPath("s2"),
				base.getTransformByPath("s3"),
				base.getTransformByPath("s4"),
				base.getTransformByPath("s5"),
				base.getTransformByPath("s6")
			};
			this.join_legion = new BaseButton(base.getTransformByPath("s1/bt1"), 1, 1);
			this.join_legion.onClick = delegate(GameObject g)
			{
				bool flag = (ulong)(ModelBase<PlayerModel>.getInstance().up_lvl * 100u + ModelBase<PlayerModel>.getInstance().lvl) >= (ulong)((long)(ModelBase<A3_LegionModel>.getInstance().create_needzhuan * 100 + ModelBase<A3_LegionModel>.getInstance().create_needlv));
				bool flag2 = flag;
				if (flag2)
				{
					this.ShowS(1);
					BaseProxy<A3_LegionProxy>.getInstance().SendGetList();
				}
				else
				{
					flytxt.instance.fly("1转1级后可加入军团！", 0, default(Color), null);
				}
			};
			this.create_legion = new BaseButton(base.getTransformByPath("s1/bt2"), 1, 1);
			this.create_legion.onClick = delegate(GameObject g)
			{
				bool flag = (ulong)(ModelBase<PlayerModel>.getInstance().up_lvl * 100u + ModelBase<PlayerModel>.getInstance().lvl) >= (ulong)((long)(ModelBase<A3_LegionModel>.getInstance().create_needzhuan * 100 + ModelBase<A3_LegionModel>.getInstance().create_needlv));
				bool flag2 = (ulong)ModelBase<PlayerModel>.getInstance().money >= (ulong)((long)ModelBase<A3_LegionModel>.getInstance().create_needmoney);
				bool flag3 = flag & flag2;
				if (flag3)
				{
					this.OpenCreatePanel();
				}
				else
				{
					flytxt.instance.fly("不符合创建条件！", 0, default(Color), null);
				}
			};
			new BaseButton(base.getTransformByPath("s2/root/bt1"), 1, 1).onClick = delegate(GameObject g)
			{
				base.transform.FindChild("s2").gameObject.SetActive(false);
				base.transform.FindChild("s1").gameObject.SetActive(true);
			};
			new BaseButton(base.getTransformByPath("s2/root/bt2"), 1, 1).onClick = delegate(GameObject g)
			{
				bool flag = this.selectData.id > 0;
				if (flag)
				{
					BaseProxy<A3_LegionProxy>.getInstance().SendApplyFor((uint)this.selectData.id);
				}
			};
			new BaseButton(base.getTransformByPath("s2/root/bt3"), 1, 1).onClick = delegate(GameObject g)
			{
				BaseProxy<A3_LegionProxy>.getInstance().SendGetList();
				this.RefreshAllList(null);
				int index = UnityEngine.Random.Range(0, ModelBase<A3_LegionModel>.getInstance().list.Count);
				bool flag = ModelBase<A3_LegionModel>.getInstance().list[index].id > 0;
				if (flag)
				{
					BaseProxy<A3_LegionProxy>.getInstance().SendApplyFor((uint)ModelBase<A3_LegionModel>.getInstance().list[index].id);
				}
			};
			BaseButton arg_296_0 = new BaseButton(base.getTransformByPath("s4/contents/info/btn_shop"), 1, 1);
			Action<GameObject> arg_296_1;
			if ((arg_296_1 = a3_legion.<>c.<>9__26_7) == null)
			{
				arg_296_1 = (a3_legion.<>c.<>9__26_7 = new Action<GameObject>(a3_legion.<>c.<>9.<init>b__26_7));
			}
			arg_296_0.onClick = arg_296_1;
			new BaseButton(base.getTransformByPath("s3/btn_close"), 1, 1).onClick = delegate(GameObject g)
			{
				this.CloseCreatePanel();
			};
			new BaseButton(base.getTransformByPath("s7/bg"), 1, 1).onClick = delegate(GameObject g)
			{
				base.transform.FindChild("s7").gameObject.SetActive(false);
			};
			base.getTransformByPath("s3/InputName").GetComponent<InputField>().onEndEdit.AddListener(delegate(string ss)
			{
				debug.Log("Enter====Enter:   " + ss);
				bool flag = this.checkKeyWord(ss, 3);
				if (!flag)
				{
					BaseProxy<A3_LegionProxy>.getInstance().SendCheckName(ss);
					this.enterName = ss;
					this.can_use = true;
				}
			});
			new BaseButton(base.getTransformByPath("s3/btn_create"), 1, 1).onClick = delegate(GameObject g)
			{
				bool flag = (ulong)(ModelBase<PlayerModel>.getInstance().up_lvl * 100u + ModelBase<PlayerModel>.getInstance().lvl) >= (ulong)((long)(ModelBase<A3_LegionModel>.getInstance().create_needzhuan * 100 + ModelBase<A3_LegionModel>.getInstance().create_needlv));
				bool flag2 = (ulong)ModelBase<PlayerModel>.getInstance().money >= (ulong)((long)ModelBase<A3_LegionModel>.getInstance().create_needmoney);
				bool flag3 = this.enterName != string.Empty && this.can_use;
				if (flag3)
				{
					bool flag4 = flag & flag2;
					if (flag4)
					{
						BaseProxy<A3_LegionProxy>.getInstance().SendCreateLegion(this.enterName);
					}
					else
					{
						flytxt.instance.fly("未满足创建条件！", 0, default(Color), null);
					}
				}
				else
				{
					flytxt.instance.fly("军团名不符合要求！", 0, default(Color), null);
				}
			};
			BaseProxy<A3_LegionProxy>.getInstance().SendGetMember();
			BaseButton arg_36B_0 = new BaseButton(base.getTransformByPath("s4/contents/active/rect_mask/rect_scroll/slayDragon"), 1, 1);
			Action<GameObject> arg_36B_1;
			if ((arg_36B_1 = a3_legion.<>c.<>9__26_12) == null)
			{
				arg_36B_1 = (a3_legion.<>c.<>9__26_12 = new Action<GameObject>(a3_legion.<>c.<>9.<init>b__26_12));
			}
			arg_36B_0.onClick = arg_36B_1;
		}

		public bool checkKeyWord(string s, int sNum)
		{
			bool flag = KeyWord.isContain(s);
			if (sNum != 3)
			{
				if (sNum == 4)
				{
					this._txtBoardKeyWord.gameObject.SetActive(flag);
				}
			}
			else
			{
				this._txtNameKeyWord.gameObject.SetActive(flag);
			}
			return flag;
		}

		public override void onShowed()
		{
			base.onShowed();
			this.enterName = string.Empty;
			base.getTransformByPath("s3/InputName").GetComponent<InputField>().text = string.Empty;
			this.select.gameObject.SetActive(false);
			BaseProxy<A3_LegionProxy>.getInstance().addEventListener(17u, new Action<GameEvent>(this.RefreshAllList));
			BaseProxy<A3_LegionProxy>.getInstance().addEventListener(2u, new Action<GameEvent>(this.OnCreate));
			BaseProxy<A3_LegionProxy>.getInstance().addEventListener(34u, new Action<GameEvent>(this.OnApplySuccessful));
			bool flag = ModelBase<A3_LegionModel>.getInstance().showtype >= 0;
			if (flag)
			{
				this._tabCtl.setSelectedIndex(ModelBase<A3_LegionModel>.getInstance().showtype, false);
				ModelBase<A3_LegionModel>.getInstance().showtype = -1;
			}
			else
			{
				bool flag2 = ModelBase<A3_LegionModel>.getInstance().myLegion.id <= 0;
				if (flag2)
				{
					this.ShowS(0);
				}
				else
				{
					this.ShowS(3);
					bool flag3 = this._tabCtl.getSeletedIndex() > 0;
					if (flag3)
					{
						this._tabCtl.setSelectedIndex(0, false);
					}
					else
					{
						bool flag4 = this._currentAuction != null;
						if (flag4)
						{
							this._currentAuction.onShowed();
						}
					}
				}
				bool flag5 = BaseProxy<A3_LegionProxy>.getInstance().join_legion;
				if (flag5)
				{
					this.ShowS(3);
					bool flag6 = this._tabCtl.getSeletedIndex() > 0;
					if (flag6)
					{
						this._tabCtl.setSelectedIndex(0, false);
					}
					else
					{
						bool flag7 = this._currentAuction != null;
						if (flag7)
						{
							this._currentAuction.onShowed();
						}
					}
					BaseProxy<A3_LegionProxy>.getInstance().join_legion = false;
				}
			}
		}

		private void Repair(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data.ContainsKey("clname") && !data.ContainsKey("clan_lvl");
			if (flag)
			{
				flytxt.instance.fly(ContMgr.getCont("clan_log_15", null), 0, default(Color), null);
			}
			bool flag2 = data.ContainsKey("clname") && data.ContainsKey("clan_lvl");
			if (flag2)
			{
				uint num = data["clan_lvl"];
				flytxt.instance.fly(ContMgr.getCont("clan_log_14", new List<string>
				{
					num.ToString()
				}), 0, default(Color), null);
			}
		}

		public override void onClose()
		{
			base.onClose();
			this.selectData = default(A3_LegionData);
			BaseProxy<A3_LegionProxy>.getInstance().removeEventListener(17u, new Action<GameEvent>(this.RefreshAllList));
			BaseProxy<A3_LegionProxy>.getInstance().removeEventListener(2u, new Action<GameEvent>(this.OnCreate));
			BaseProxy<A3_LegionProxy>.getInstance().removeEventListener(34u, new Action<GameEvent>(this.OnApplySuccessful));
			bool flag = this._currentAuction != null;
			if (flag)
			{
				this._currentAuction.onClose();
			}
		}

		private void RefreshAllList(GameEvent e)
		{
			this.select.gameObject.SetActive(false);
			this.selectData = default(A3_LegionData);
			this.select.SetParent(this.content.parent);
			Transform transformByPath = base.getTransformByPath("s2/root/cells/scroll/0");
			Transform[] componentsInChildren = this.content.GetComponentsInChildren<Transform>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				bool flag = transform.parent == this.content;
				if (flag)
				{
					UnityEngine.Object.Destroy(transform.gameObject);
				}
			}
			int num = 0;
			bool isOn = this.dic.isOn;
			if (isOn)
			{
				for (int j = 0; j < ModelBase<A3_LegionModel>.getInstance().list.Count; j++)
				{
					bool flag2 = ModelBase<A3_LegionModel>.getInstance().list[j].direct_join == 0;
					if (flag2)
					{
						ModelBase<A3_LegionModel>.getInstance().list.Remove(ModelBase<A3_LegionModel>.getInstance().list[j]);
					}
				}
			}
			bool flag3 = !this.dic.isOn;
			if (flag3)
			{
				ModelBase<A3_LegionModel>.getInstance().list.Clear();
				foreach (A3_LegionData current in ModelBase<A3_LegionModel>.getInstance().list2)
				{
					ModelBase<A3_LegionModel>.getInstance().list.Add(current);
				}
			}
			ModelBase<A3_LegionModel>.getInstance().list.Sort(new myComparer());
			foreach (A3_LegionData current2 in ModelBase<A3_LegionModel>.getInstance().list)
			{
				GameObject go = UnityEngine.Object.Instantiate<GameObject>(transformByPath.gameObject);
				go.transform.SetParent(this.content);
				go.transform.FindChild("name").GetComponent<Text>().text = current2.clname;
				go.transform.FindChild("sl").GetComponent<Text>().text = current2.name;
				go.transform.FindChild("rs").GetComponent<Text>().text = current2.plycnt + "/" + 200;
				Text arg_2A5_0 = go.transform.FindChild("zdl").GetComponent<Text>();
				int num2 = current2.combpt;
				arg_2A5_0.text = num2.ToString();
				Text arg_2D6_0 = go.transform.FindChild("dj").GetComponent<Text>();
				num2 = current2.lvl;
				arg_2D6_0.text = num2.ToString();
				Text arg_307_0 = go.transform.FindChild("hy").GetComponent<Text>();
				num2 = current2.huoyue;
				arg_307_0.text = num2.ToString();
				bool flag4 = num % 2 == 0;
				if (flag4)
				{
					go.transform.FindChild("bg1").gameObject.SetActive(true);
					go.transform.FindChild("bg2").gameObject.SetActive(false);
				}
				else
				{
					go.transform.FindChild("bg1").gameObject.SetActive(false);
					go.transform.FindChild("bg2").gameObject.SetActive(true);
				}
				go.transform.localScale = Vector3.one;
				go.SetActive(true);
				List<A3_LegionData> temp = new List<A3_LegionData>();
				temp.Add(current2);
				new BaseButton(go.transform, 1, 1).onClick = delegate(GameObject g)
				{
					this.select.SetParent(go.transform);
					this.select.localPosition = Vector3.zero;
					this.select.gameObject.SetActive(true);
					this.selectData = temp[0];
				};
				num++;
			}
			this.content.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, this.content.GetComponent<GridLayoutGroup>().cellSize.y * (float)ModelBase<A3_LegionModel>.getInstance().list.Count);
		}

		private void OnCreate(GameEvent e)
		{
			this.ShowS(3);
			bool flag = this._tabCtl.getSeletedIndex() > 0;
			if (flag)
			{
				this._tabCtl.setSelectedIndex(0, false);
			}
			else
			{
				bool flag2 = this._currentAuction != null;
				if (flag2)
				{
					this._currentAuction.onShowed();
				}
			}
			BaseProxy<A3_LegionProxy>.getInstance().SendGetMember();
		}

		private void OpenCreatePanel()
		{
			this.s[2].gameObject.SetActive(true);
			this.can_use = false;
			BaseProxy<A3_LegionProxy>.getInstance().addEventListener(24u, new Action<GameEvent>(this.SetCheckName));
			this.Avalable.text = "（待检测）";
		}

		private void CloseCreatePanel()
		{
			this.s[2].gameObject.SetActive(false);
			BaseProxy<A3_LegionProxy>.getInstance().removeEventListener(24u, new Action<GameEvent>(this.SetCheckName));
		}

		public void SetCheckName(GameEvent e)
		{
			int num = e.data["err"];
			bool flag = num == -107 || num == -103;
			if (flag)
			{
				this.can_use = false;
				this.Avalable.text = "(<color=#cd5c5c>不可用</color>)";
			}
			else
			{
				bool flag2 = num == -1420;
				if (flag2)
				{
					this.can_use = false;
					this.Avalable.text = "(<color=#ff9900>已存在</color>)";
				}
				else
				{
					bool flag3 = num > 0;
					if (flag3)
					{
						this.can_use = true;
						this.Avalable.text = "(<color=#7fff00>可用</color>)";
					}
				}
			}
		}

		private void Refresh()
		{
		}

		private void OnApplySuccessful(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data["approved"];
			bool flag2 = flag;
			if (flag2)
			{
				this.ShowS(3);
				bool flag3 = this._tabCtl.getSeletedIndex() > 0;
				if (flag3)
				{
					this._tabCtl.setSelectedIndex(0, false);
				}
				else
				{
					bool flag4 = this._currentAuction != null;
					if (flag4)
					{
						this._currentAuction.onShowed();
					}
				}
			}
		}

		public void ShowS(int index)
		{
			for (int i = 0; i < this.s.Length; i++)
			{
				this.s[i].gameObject.SetActive(false);
			}
			bool flag = index >= 0 && index < this.s.Length;
			if (flag)
			{
				this.s[index].gameObject.SetActive(true);
			}
			bool flag2 = BaseProxy<A3_LegionProxy>.getInstance().hasEventListener(24u);
			if (flag2)
			{
				BaseProxy<A3_LegionProxy>.getInstance().removeEventListener(24u, new Action<GameEvent>(this.SetCheckName));
			}
		}

		public void OpenSub(Transform t)
		{
			t.SetParent(base.transform.parent);
			t.SetAsLastSibling();
			t.gameObject.SetActive(true);
		}

		public void CloseSub(Transform t, Transform p)
		{
			t.SetParent(p);
			t.SetAsLastSibling();
			t.gameObject.SetActive(false);
		}
	}
}
