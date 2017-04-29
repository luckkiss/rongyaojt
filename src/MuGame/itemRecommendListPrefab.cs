using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	internal class itemRecommendListPrefab : toggleGropFriendBase
	{
		public new Transform root;

		public Toggle toggle;

		public Text txtNickName;

		public string name;

		public Text txtLvl;

		public Text txtProfessional;

		public BaseButton btnAction;

		public uint cid;

		private Transform actionPanelPos;

		public new void init()
		{
			Transform transform = toggleGropFriendBase.mTransform.FindChild("itemPrefabs/itemAddFriend");
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(transform.gameObject);
			this.root = gameObject.transform;
			this.toggle = this.root.FindChild("Toggle").GetComponent<Toggle>();
			this.toggle.onValueChanged.AddListener(new UnityAction<bool>(this.onToggleClick));
			this.txtNickName = this.root.transform.FindChild("texts/txtNickName").GetComponent<Text>();
			this.txtLvl = this.root.transform.FindChild("texts/txtLevel").GetComponent<Text>();
			this.txtProfessional = this.root.transform.FindChild("texts/txtProfessional").GetComponent<Text>();
			this.actionPanelPos = this.root.FindChild("btnAction/actionPanelPos");
			this.btnAction = new BaseButton(this.root.transform.FindChild("btnAction"), 1, 1);
			gameObject.SetActive(true);
			this.root.SetParent(recommendList._instance.containt, false);
			this.toggle.isOn = true;
		}

		public void show(itemFriendData data, out Transform rootT)
		{
			this.name = data.name;
			this.txtNickName.text = data.name;
			this.txtLvl.text = string.Concat(new object[]
			{
				data.zhuan,
				"转",
				data.lvl,
				"级"
			});
			this.txtProfessional.text = this.getProfessional(data.carr);
			this.cid = data.cid;
			data.root = this.root;
			rootT = this.root;
			this.toggle.isOn = true;
			this.btnAction.onClick = new Action<GameObject>(this.onBtnActionClick);
		}

		private void onBtnActionClick(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_SHEJIAO);
			uint num = this.cid;
			ArrayList arrayList = new ArrayList();
			arrayList.Add(num);
			ModelBase<PlayerModel>.getInstance().showFriend = true;
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_TARGETINFO, arrayList, false);
			a3_targetinfo expr_4D = a3_targetinfo.instan;
			if (expr_4D != null)
			{
				expr_4D.transform.SetAsLastSibling();
			}
		}

		private string getProfessional(uint carr)
		{
			string result = string.Empty;
			switch (carr)
			{
			case 1u:
				result = "全职业";
				break;
			case 2u:
				result = "战士";
				break;
			case 3u:
				result = "法师";
				break;
			case 5u:
				result = "暗影";
				break;
			}
			return result;
		}

		public void set(itemFriendData data)
		{
			this.root.gameObject.SetActive(true);
			this.txtNickName.text = data.name;
			this.txtLvl.text = string.Concat(new object[]
			{
				data.zhuan,
				"转",
				data.lvl,
				"级"
			});
			this.txtProfessional.text = this.getProfessional(data.carr);
			this.cid = data.cid;
			data.root = this.root;
			this.toggle.isOn = true;
		}

		private void onToggleClick(bool b)
		{
			friendList._instance.btnAdd.transform.GetComponent<Button>().interactable = true;
		}
	}
}
