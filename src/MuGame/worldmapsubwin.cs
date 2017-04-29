using Cross;
using GameFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class worldmapsubwin : Window
	{
		public static int id;

		public static int mapid;

		public Text txtBt1;

		public Text txtBt2;

		public Text txt;

		private bool isfree = false;

		private Variant vmap;

		private SXML xml;

		private int AREA;

		public int needMoney = 0;

		private int toid = 0;

		public override void init()
		{
			BaseButton baseButton = new BaseButton(base.getTransformByPath("bt0"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onfreeclick);
			BaseButton baseButton2 = new BaseButton(base.getTransformByPath("bt1"), 1, 1);
			baseButton2.onClick = new Action<GameObject>(this.onmoneyclick);
			this.txtBt1 = base.getComponentByPath<Text>("bt0/Text");
			this.txtBt2 = base.getComponentByPath<Text>("bt1/Text");
			this.txt = base.getComponentByPath<Text>("desc");
			this.AREA = XMLMgr.instance.GetSXML("comm.worldmap", "").getInt("area");
			EventTriggerListener.Get(base.getGameObjectByPath("btclose")).onClick = new EventTriggerListener.VoidDelegate(this.onClose);
		}

		public override void onShowed()
		{
			this.isfree = (ModelBase<PlayerModel>.getInstance().vip >= 3u);
			this.xml = XMLMgr.instance.GetSXML("mappoint.p", "id==" + worldmapsubwin.id);
			this.vmap = SvrMapConfig.instance.getSingleMapConf(this.xml.getUint("mapid"));
			string text = this.vmap.ContainsKey("map_name") ? this.vmap["map_name"]._str : "--";
			this.txt.text = text;
			this.txtBt1.text = ContMgr.getCont("worldmap_bt1", null);
			base.transform.SetAsLastSibling();
			int @int = this.xml.getInt("cost");
			this.needMoney = @int / 10 * (int)(ModelBase<PlayerModel>.getInstance().lvl / 10f) + @int;
			this.txtBt2.text = this.needMoney.ToString();
			bool flag = a3_active.onshow;
			if (flag)
			{
				a3_active_findbtu expr_11B = a3_active_findbtu.instans;
				bool flag2 = ((expr_11B != null) ? expr_11B.monobj : null) != null;
				if (flag2)
				{
					a3_active_findbtu.instans.monobj.SetActive(false);
				}
			}
		}

		public override void onClosed()
		{
			worldmapsubwin.id = 0;
			worldmapsubwin.mapid = 0;
			bool flag = a3_active.onshow;
			if (flag)
			{
				a3_active_findbtu expr_21 = a3_active_findbtu.instans;
				bool flag2 = ((expr_21 != null) ? expr_21.monobj : null) != null;
				if (flag2)
				{
					a3_active_findbtu.instans.monobj.SetActive(true);
				}
			}
		}

		private void onfreeclick(GameObject go)
		{
			Vector3 vec = new Vector3(this.xml.getFloat("ux"), 0f, this.xml.getFloat("uy"));
			SelfRole.WalkToMap(this.vmap["id"], vec, null, 0.3f);
			InterfaceMgr.getInstance().close(InterfaceMgr.WORLD_MAP);
			InterfaceMgr.getInstance().close(InterfaceMgr.WORLD_MAP_SUB);
			bool flag = a3_active.onshow;
			if (flag)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_ACTIVE);
			}
		}

		public static void onCD(cd item)
		{
			int num = (int)(cd.secCD - cd.lastCD) / 100;
			item.txt.text = ContMgr.getCont("worldmap_cd", new string[]
			{
				((float)num / 10f).ToString()
			});
		}

		private void onClose(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.WORLD_MAP_SUB);
		}

		private void onmoneyclick(GameObject go)
		{
			bool flag = !ModelBase<FindBestoModel>.getInstance().Canfly;
			if (flag)
			{
				flytxt.instance.fly(ModelBase<FindBestoModel>.getInstance().nofly_txt, 0, default(Color), null);
				InterfaceMgr.getInstance().close(InterfaceMgr.WORLD_MAP);
				InterfaceMgr.getInstance().close(InterfaceMgr.WORLD_MAP_SUB);
			}
			else
			{
				bool flag2 = (ulong)ModelBase<PlayerModel>.getInstance().money < (ulong)((long)this.needMoney);
				if (flag2)
				{
					flytxt.instance.fly(ContMgr.getCont("comm_nomoney", null), 0, default(Color), null);
				}
				else
				{
					float num = Vector3.Distance(SelfRole._inst.m_curModel.position, new Vector3(this.xml.getFloat("ux"), SelfRole._inst.m_curModel.position.y, this.xml.getFloat("uy")));
					this.toid = worldmapsubwin.id;
					bool flag3 = worldmapsubwin.mapid == GRMap.instance.m_nCurMapID && num < (float)this.AREA;
					if (flag3)
					{
						GameObject goeff = UnityEngine.Object.Instantiate<GameObject>(worldmap.EFFECT_CHUANSONG1);
						goeff.transform.SetParent(SelfRole._inst.m_curModel, false);
						Action <>9__2;
						MsgBoxMgr.getInstance().showConfirm(ContMgr.getCont("worldmap_tooclose", null), delegate
						{
							cd.updateHandle = new Action<cd>(worldmapsubwin.onCD);
							Action arg_51_0 = new Action(this.<onmoneyclick>b__17_1);
							float arg_51_1 = 2.8f;
							bool arg_51_2 = false;
							Action arg_51_3;
							if ((arg_51_3 = <>9__2) == null)
							{
								arg_51_3 = (<>9__2 = delegate
								{
									UnityEngine.Object.Destroy(goeff);
								});
							}
							cd.show(arg_51_0, arg_51_1, arg_51_2, arg_51_3, default(Vector3));
							InterfaceMgr.getInstance().close(InterfaceMgr.WORLD_MAP);
							InterfaceMgr.getInstance().close(InterfaceMgr.WORLD_MAP_SUB);
							bool flag5 = a3_active.onshow;
							if (flag5)
							{
								InterfaceMgr.getInstance().close(InterfaceMgr.A3_ACTIVE);
							}
						}, null, 0);
					}
					else
					{
						cd.updateHandle = new Action<cd>(worldmapsubwin.onCD);
						GameObject goeff = UnityEngine.Object.Instantiate<GameObject>(worldmap.EFFECT_CHUANSONG1);
						goeff.transform.SetParent(SelfRole._inst.m_curModel, false);
						cd.show(delegate
						{
							BaseProxy<MapProxy>.getInstance().sendBeginChangeMap(this.toid, true, false);
						}, 2.8f, false, delegate
						{
							UnityEngine.Object.Destroy(goeff);
						}, default(Vector3));
						InterfaceMgr.getInstance().close(InterfaceMgr.WORLD_MAP);
						InterfaceMgr.getInstance().close(InterfaceMgr.WORLD_MAP_SUB);
						bool flag4 = a3_active.onshow;
						if (flag4)
						{
							InterfaceMgr.getInstance().close(InterfaceMgr.A3_ACTIVE);
						}
					}
				}
			}
		}
	}
}
