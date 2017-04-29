using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	public class MsgBoxMgr
	{
		private GameObject root;

		private GameObject root_dart_awd;

		private GameObject root_task_fb;

		private GameObject root_swtz_fb;

		private Button btn_yes;

		private Button btn_no;

		private Button btn_dart;

		private Button btn_onekey;

		private Text text_str;

		private Text text_dart;

		private UnityAction _call;

		private UnityAction _handleNo;

		private static MsgBoxMgr _instance;

		public static bool isShow_guide = false;

		public static MsgBoxMgr getInstance()
		{
			bool flag = MsgBoxMgr._instance == null;
			if (flag)
			{
				MsgBoxMgr._instance = new MsgBoxMgr();
				MsgBoxMgr._instance.init();
			}
			return MsgBoxMgr._instance;
		}

		private void init()
		{
			Transform transform = GameObject.Find("winLayer").transform;
			GameObject original = Resources.Load("prefab/msgbox") as GameObject;
			this.root = UnityEngine.Object.Instantiate<GameObject>(original);
			this.root.transform.SetParent(transform, false);
			this.text_str = this.root.transform.FindChild("Text").GetComponent<Text>();
			this.btn_yes = this.root.transform.FindChild("yes").GetComponent<Button>();
			BaseButton baseButton = new BaseButton(this.btn_yes.transform, 1, 1);
			baseButton.onClick = null;
			this.btn_no = this.root.transform.FindChild("no").GetComponent<Button>();
			BaseButton baseButton2 = new BaseButton(this.btn_no.transform, 1, 1);
			baseButton2.onClick = null;
			this.btn_onekey = this.root.transform.FindChild("confirm").GetComponent<Button>();
			BaseButton baseButton3 = new BaseButton(this.btn_onekey.transform, 1, 1);
			baseButton3.onClick = null;
			this.btn_yes.onClick.AddListener(new UnityAction(this.onClick));
			this.btn_no.onClick.AddListener(new UnityAction(this.hide));
			this.btn_onekey.onClick.AddListener(new UnityAction(this.onClick));
			this.root.SetActive(false);
			GameObject original2 = Resources.Load("prefab/msgbox_dart") as GameObject;
			this.root_dart_awd = UnityEngine.Object.Instantiate<GameObject>(original2);
			this.root_dart_awd.transform.SetParent(transform, false);
			this.root_dart_awd.SetActive(false);
			this.text_dart = this.root_dart_awd.transform.FindChild("str").GetComponent<Text>();
			this.btn_dart = this.root_dart_awd.transform.FindChild("yes").GetComponent<Button>();
			this.btn_dart.onClick.AddListener(new UnityAction(this.onClick));
			GameObject original3 = Resources.Load("prefab/msgbox_task_fb") as GameObject;
			this.root_task_fb = UnityEngine.Object.Instantiate<GameObject>(original3);
			this.root_task_fb.transform.SetParent(transform, false);
			this.root_task_fb.SetActive(false);
			GameObject original4 = Resources.Load("prefab/msgbox_swtz_fb") as GameObject;
			this.root_swtz_fb = UnityEngine.Object.Instantiate<GameObject>(original4);
			this.root_swtz_fb.transform.SetParent(transform, false);
			this.root_swtz_fb.SetActive(false);
		}

		public void showConfirmWithContId(string id, UnityAction handleYes, UnityAction handleNo = null, List<string> l = null, int type = 0)
		{
			this.showConfirm(ContMgr.getCont(id, l), handleYes, handleNo, type);
		}

		public void showConfirm(string str, UnityAction handleYes, UnityAction handleNo = null, int type = 0)
		{
			this.clear();
			this.root.transform.SetAsLastSibling();
			this.text_str.text = str;
			this.root.SetActive(true);
			bool flag = type == 0;
			if (flag)
			{
				this.btn_yes.gameObject.SetActive(true);
				this.btn_no.gameObject.SetActive(true);
				this.btn_onekey.gameObject.SetActive(false);
				this._handleNo = handleNo;
				this._call = handleYes;
			}
			bool flag2 = type == 1;
			if (flag2)
			{
				this.btn_yes.gameObject.SetActive(false);
				this.btn_no.gameObject.SetActive(false);
				this.btn_onekey.gameObject.SetActive(true);
				this._handleNo = handleNo;
			}
		}

		public void showTask_fb_confirm(string title, string str, bool guide, UnityAction handleYes, UnityAction handleNo = null)
		{
			this.root_task_fb.transform.SetAsLastSibling();
			this.root_task_fb.transform.FindChild("title").GetComponent<Text>().text = title;
			this.root_task_fb.transform.FindChild("Text").GetComponent<Text>().text = str;
			if (guide)
			{
				MsgBoxMgr.isShow_guide = true;
				a3_liteMinimap.instance.setGuide();
				this.root_task_fb.transform.FindChild("confirm/guide_task_info").gameObject.SetActive(true);
			}
			else
			{
				this.root_task_fb.transform.FindChild("confirm/guide_task_info").gameObject.SetActive(false);
				MsgBoxMgr.isShow_guide = false;
				a3_liteMinimap.instance.setGuide();
			}
			new BaseButton(this.root_task_fb.transform.FindChild("confirm"), 1, 1).onClick = delegate(GameObject g)
			{
				handleYes();
				this.root_task_fb.SetActive(false);
				bool flag = a3_liteMinimap.instance;
				if (flag)
				{
					MsgBoxMgr.isShow_guide = false;
					a3_liteMinimap.instance.setGuide();
				}
			};
			new BaseButton(this.root_task_fb.transform.FindChild("cancel"), 1, 1).onClick = delegate(GameObject g)
			{
				this.root_task_fb.SetActive(false);
				bool flag = a3_liteMinimap.instance;
				if (flag)
				{
					MsgBoxMgr.isShow_guide = false;
					a3_liteMinimap.instance.setGuide();
				}
			};
			new BaseButton(this.root_task_fb.transform.FindChild("bg"), 1, 1).onClick = delegate(GameObject g)
			{
				this.root_task_fb.SetActive(false);
				bool flag = a3_liteMinimap.instance;
				if (flag)
				{
					MsgBoxMgr.isShow_guide = false;
					a3_liteMinimap.instance.setGuide();
				}
			};
			new BaseButton(this.root_task_fb.transform.FindChild("yes"), 1, 1).onClick = delegate(GameObject g)
			{
				handleYes();
				this.root_task_fb.SetActive(false);
				bool flag = a3_liteMinimap.instance;
				if (flag)
				{
					MsgBoxMgr.isShow_guide = false;
					a3_liteMinimap.instance.setGuide();
				}
			};
			new BaseButton(this.root_task_fb.transform.FindChild("no"), 1, 1).onClick = delegate(GameObject g)
			{
				handleNo();
				this.root_task_fb.SetActive(false);
				bool flag = a3_liteMinimap.instance;
				if (flag)
				{
					MsgBoxMgr.isShow_guide = false;
					a3_liteMinimap.instance.setGuide();
				}
			};
			this.root_task_fb.SetActive(true);
		}

		public void showTask_fb_confirm(string title, string str, bool guide, int zhanli, UnityAction handleYes, UnityAction handleNo = null)
		{
			this.root_swtz_fb.transform.SetAsLastSibling();
			this.root_swtz_fb.transform.FindChild("title").GetComponent<Text>().text = title;
			this.root_swtz_fb.transform.FindChild("Text").GetComponent<Text>().text = str;
			this.root_swtz_fb.transform.FindChild("zhandouli").GetComponent<Text>().text = zhanli.ToString();
			if (guide)
			{
				this.root_swtz_fb.transform.FindChild("confirm/guide_task_info").gameObject.SetActive(true);
				MsgBoxMgr.isShow_guide = true;
				a3_liteMinimap.instance.setGuide();
			}
			else
			{
				this.root_swtz_fb.transform.FindChild("confirm/guide_task_info").gameObject.SetActive(false);
				MsgBoxMgr.isShow_guide = false;
				a3_liteMinimap.instance.setGuide();
			}
			new BaseButton(this.root_swtz_fb.transform.FindChild("confirm"), 1, 1).onClick = delegate(GameObject g)
			{
				handleYes();
				this.root_swtz_fb.SetActive(false);
				bool flag = a3_liteMinimap.instance;
				if (flag)
				{
					MsgBoxMgr.isShow_guide = false;
					a3_liteMinimap.instance.setGuide();
				}
			};
			new BaseButton(this.root_swtz_fb.transform.FindChild("cancel"), 1, 1).onClick = delegate(GameObject g)
			{
				this.root_swtz_fb.SetActive(false);
				bool flag = a3_liteMinimap.instance;
				if (flag)
				{
					MsgBoxMgr.isShow_guide = false;
					a3_liteMinimap.instance.setGuide();
				}
			};
			new BaseButton(this.root_swtz_fb.transform.FindChild("bg"), 1, 1).onClick = delegate(GameObject g)
			{
				this.root_swtz_fb.SetActive(false);
				bool flag = a3_liteMinimap.instance;
				if (flag)
				{
					MsgBoxMgr.isShow_guide = false;
					a3_liteMinimap.instance.setGuide();
				}
			};
			new BaseButton(this.root_swtz_fb.transform.FindChild("yes"), 1, 1).onClick = delegate(GameObject g)
			{
				handleYes();
				this.root_swtz_fb.SetActive(false);
				bool flag = a3_liteMinimap.instance;
				if (flag)
				{
					MsgBoxMgr.isShow_guide = false;
					a3_liteMinimap.instance.setGuide();
				}
			};
			new BaseButton(this.root_swtz_fb.transform.FindChild("no"), 1, 1).onClick = delegate(GameObject g)
			{
				handleNo();
				this.root_swtz_fb.SetActive(false);
				bool flag = a3_liteMinimap.instance;
				if (flag)
				{
					MsgBoxMgr.isShow_guide = false;
					a3_liteMinimap.instance.setGuide();
				}
			};
			this.root_swtz_fb.SetActive(true);
		}

		public void showDartGetAwd(string str, uint item_id, int num, int per)
		{
			this._call = null;
			this.root_dart_awd.transform.FindChild("2/icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("icon/item/" + item_id);
			this.root_dart_awd.transform.FindChild("2/Text").GetComponent<Text>().text = num.ToString();
			this.root_dart_awd.transform.FindChild("1/Text").GetComponent<Text>().text = per.ToString();
			this.root_dart_awd.transform.SetAsLastSibling();
			this.root_dart_awd.SetActive(true);
			this.btn_dart.gameObject.SetActive(true);
			new BaseButton(this.root_dart_awd.transform.FindChild("2"), 1, 1).onClick = delegate(GameObject go)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(item_id);
				arrayList.Add(1);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_MINITIP, arrayList, false);
			};
		}

		public void show(string str, UnityAction call)
		{
			this.clear();
			this.root.transform.SetAsLastSibling();
			this._call = call;
			this.text_str.text = str;
			this.root.SetActive(true);
		}

		public void showGoldNeed(string id, int goldenNeedNum, UnityAction handleYes, UnityAction handleNo = null, List<string> contPram = null)
		{
			this.showConfirm(ContMgr.getCont(id, contPram), delegate
			{
				bool flag = (long)goldenNeedNum > (long)((ulong)ModelBase<PlayerModel>.getInstance().gold);
				if (flag)
				{
					flytxt.flyUseContId("rechange_noGolden", new List<string>
					{
						goldenNeedNum.ToString()
					}, 0);
					InterfaceMgr.getInstance().open(InterfaceMgr.RECHARGE, null, false);
					this.hide();
				}
				else
				{
					bool flag2 = handleYes != null;
					if (flag2)
					{
						handleYes();
					}
				}
			}, handleNo, 0);
		}

		public void showMoneyNeed()
		{
		}

		public void hide()
		{
			bool flag = this._handleNo != null;
			if (flag)
			{
				this._handleNo();
			}
			this.root.SetActive(false);
			this.root_task_fb.SetActive(false);
			this.root_swtz_fb.SetActive(false);
		}

		protected void onClick()
		{
			bool flag = this._call != null;
			if (flag)
			{
				this._call();
			}
			this.root.SetActive(false);
			this.root_task_fb.SetActive(false);
			this.root_swtz_fb.SetActive(false);
			this.root_dart_awd.SetActive(false);
		}

		private void clear()
		{
			this._call = null;
			this._handleNo = null;
			this.text_str.text = "";
		}
	}
}
