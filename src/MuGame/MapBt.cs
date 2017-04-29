using GameFramework;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	internal class MapBt : Skin
	{
		private GameObject ntxt1;

		private GameObject ntxt2;

		private Text txtLv1;

		private Text txtLv2;

		private Button bt;

		private Animator ani;

		private Transform m_transMap;

		private Transform translv;

		private string strlv;

		private Image map_image;

		public MapBt(Transform transBt, Transform transMap) : base(transBt)
		{
			this.m_transMap = transMap;
			this.initUI();
		}

		private void initUI()
		{
			this.translv = base.transform.FindChild("lv");
			this.bt = base.transform.GetChild(1).GetComponent<Button>();
			this.bt.onClick.AddListener(new UnityAction(this.onClick));
			this.txtLv1 = this.translv.FindChild("txt1").GetComponent<Text>();
			this.txtLv2 = this.translv.FindChild("txt2").GetComponent<Text>();
			this.ntxt1 = this.translv.FindChild("ntxt1").gameObject;
			this.ntxt2 = this.translv.FindChild("ntxt2").gameObject;
			this.map_image = this.m_transMap.GetComponent<Image>();
			this.ani = this.m_transMap.GetComponent<Animator>();
			this.ani.enabled = false;
		}

		private void onDrag(GameObject go)
		{
			this.ani.SetBool("ondrag", true);
		}

		private void onOut(GameObject go)
		{
			this.ani.SetBool("ondrag", false);
		}

		public void refresh()
		{
			string[] array = this.translv.GetChild(0).gameObject.name.Split(new char[]
			{
				'_'
			});
			bool flag = array.Length != 2;
			if (flag)
			{
				this.setUnEnable();
			}
			else
			{
				this.strlv = ContMgr.getCont("worldmap_lv", new string[]
				{
					array[0],
					array[1]
				});
				int up_lvl = (int)ModelBase<PlayerModel>.getInstance().up_lvl;
				int lvl = (int)ModelBase<PlayerModel>.getInstance().lvl;
				bool flag2 = up_lvl > int.Parse(array[0]) || (up_lvl == int.Parse(array[0]) && lvl >= int.Parse(array[1]));
				if (flag2)
				{
					this.txtLv1.text = this.strlv;
					this.txtLv2.text = "";
					this.ntxt1.SetActive(true);
					this.ntxt2.SetActive(false);
					this.bt.interactable = true;
					this.map_image.color = Color.clear;
				}
				else
				{
					this.ntxt2.SetActive(true);
					this.ntxt1.SetActive(false);
					this.txtLv1.text = "";
					this.txtLv2.text = this.strlv;
					this.setUnEnable();
				}
			}
		}

		private void onClick()
		{
			string a = this.bt.gameObject.name.Substring(0, 1);
			bool flag = a == "p";
			if (flag)
			{
				worldmapsubwin.id = int.Parse(this.bt.gameObject.name.Substring(1, this.bt.gameObject.name.Length - 1));
				InterfaceMgr.getInstance().open(InterfaceMgr.WORLD_MAP_SUB, null, false);
			}
			else
			{
				worldmap.mapid = int.Parse(this.bt.gameObject.name);
				worldmap.instance.tab.setSelectedIndex(1, false);
			}
		}

		public void setUnEnable()
		{
			this.bt.interactable = false;
			this.map_image.color = Color.gray;
		}
	}
}
