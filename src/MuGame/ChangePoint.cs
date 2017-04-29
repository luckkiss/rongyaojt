using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	public class ChangePoint : MonoBehaviour
	{
		public List<int> paramInts = new List<int>();

		public int triggerTimes = 1;

		public int type = 0;

		public List<GameObject> paramGameobjects = new List<GameObject>();

		public int curTrigger = 0;

		private void Start()
		{
			BoxCollider boxCollider = base.gameObject.GetComponent<BoxCollider>();
			bool flag = boxCollider == null;
			if (flag)
			{
				boxCollider = base.gameObject.AddComponent<BoxCollider>();
			}
			boxCollider.isTrigger = true;
			base.gameObject.layer = EnumLayer.LM_PT;
		}

		public void OnTriggerEnter(Collider other)
		{
			bool flag = other.gameObject.layer == EnumLayer.LM_BT_SELF;
			if (flag)
			{
				bool flag2 = NetClient.instance != null;
				if (flag2)
				{
					bool flag3 = GRMap.changeMapTimeSt == 0 || NetClient.instance.CurServerTimeStamp - GRMap.changeMapTimeSt < 2;
					if (flag3)
					{
						return;
					}
				}
				bool flag4 = this.triggerTimes == 0;
				if (flag4)
				{
					this.onTrigger();
				}
				else
				{
					bool flag5 = this.triggerTimes > this.curTrigger;
					if (flag5)
					{
						bool flag6 = this.onTrigger();
						bool flag7 = flag6;
						if (flag7)
						{
							this.curTrigger++;
							bool flag8 = this.curTrigger >= this.triggerTimes;
							if (flag8)
							{
								UnityEngine.Object.Destroy(base.gameObject);
							}
						}
					}
				}
			}
		}

		private bool onTrigger()
		{
			bool flag = this.type == 0;
			bool result;
			if (flag)
			{
				for (int i = 0; i < base.transform.childCount; i++)
				{
					TriggerHanldePoint component = base.transform.GetChild(i).GetComponent<TriggerHanldePoint>();
					bool flag2 = component != null;
					if (flag2)
					{
						component.onTriggerHanlde();
					}
				}
				result = true;
			}
			else
			{
				bool flag3 = this.type == 1;
				if (flag3)
				{
					bool flag4 = this.paramInts.Count == 0;
					if (flag4)
					{
						result = true;
					}
					else
					{
						Variant singleMapConf = SvrMapConfig.instance.getSingleMapConf((uint)this.paramInts[0]);
						bool flag5 = singleMapConf.ContainsKey("lv_up") && singleMapConf["lv_up"] > ModelBase<PlayerModel>.getInstance().up_lvl;
						if (flag5)
						{
							flytxt.instance.fly(ContMgr.getCont("comm_nolvmap", new string[]
							{
								singleMapConf["lv_up"],
								singleMapConf["lv"]
							}), singleMapConf["map_name"], default(Color), null);
							result = false;
						}
						else
						{
							bool flag6 = singleMapConf.ContainsKey("lv") && singleMapConf["lv"] > ModelBase<PlayerModel>.getInstance().lvl;
							if (flag6)
							{
								flytxt.instance.fly(ContMgr.getCont("comm_nolvmap", new string[]
								{
									singleMapConf["lv_up"],
									singleMapConf["lv"]
								}), singleMapConf["map_name"], default(Color), null);
								result = false;
							}
							else
							{
								loading_cloud.showIt(delegate
								{
									BaseProxy<MapProxy>.getInstance().sendBeginChangeMap(this.paramInts[0], false, false);
								});
								result = true;
							}
						}
					}
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		public void changemap()
		{
			SelfRole._inst.setPos(new Vector3(0f, 0f, -11f));
			loading_cloud.instance.hide(null);
		}

		[ContextMenu("脚本使用帮助")]
		public void helper()
		{
			Debug.Log("type1:切换地图\ntype2:显示隐藏物件  go中双数为需要显示的，单数是需要隐藏的\ntype3:触发脚本  go中为需要触发的脚本物件\n");
		}
	}
}
