using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	public class BaseRoomItem
	{
		private uint fakeItemIdx = 0u;

		private Variant svrConf;

		private bool started = false;

		private GameObject roomItemCon;

		private TickItem process;

		public uint goldnum;

		public uint expnum;

		private uint lvl;

		private uint zhuan;

		public int getach;

		public int getExp;

		private List<drop_data> list = new List<drop_data>();

		public List<DropItemdta> list2 = new List<DropItemdta>();

		public static BaseRoomItem instance;

		private int idx = 0;

		private float f = 0.05f;

		public Dictionary<uint, DropItem> dDropItem;

		public Dictionary<uint, DropItem> dDropItem_own;

		public Dictionary<uint, DropItem> dDropFakeItem;

		protected List<Vector3> lDropOffset;

		private const decimal stepDrop = 1.7m;

		public virtual void onStart(Variant conf)
		{
			bool flag = this.started;
			if (!flag)
			{
				this.started = true;
				BaseRoomItem.instance = this;
				this.dDropItem = new Dictionary<uint, DropItem>();
				this.dDropItem_own = new Dictionary<uint, DropItem>();
				this.dDropFakeItem = new Dictionary<uint, DropItem>();
				this.svrConf = conf;
				this.fakeItemIdx = 0u;
				SceneCamera.refreshMiniMapCanvas();
				this.process = new TickItem(new Action<float>(this.onTick));
				TickMgr.instance.addTick(this.process);
				this.initRoomObj();
				this.goldnum = 0u;
				this.expnum = ModelBase<PlayerModel>.getInstance().exp;
				this.lvl = ModelBase<PlayerModel>.getInstance().lvl;
				this.zhuan = ModelBase<PlayerModel>.getInstance().up_lvl;
				a3_fb_finish.room = this;
			}
		}

		public virtual void onEnd()
		{
			bool flag = !this.started;
			if (!flag)
			{
				this.dDropItem = null;
				this.dDropItem_own = null;
				DropItem.dropItemCon = null;
				this.svrConf = null;
				this.started = false;
				TickMgr.instance.removeTick(this.process);
				a3_fb_finish.room = null;
			}
		}

		public virtual void onTick(float s)
		{
			bool flag = this.list.Count > 0;
			if (flag)
			{
				this.f -= Time.deltaTime;
				for (int i = 0; i < this.list.Count; i++)
				{
					bool flag2 = this.f <= 0f;
					if (flag2)
					{
						this.idx = this.showDrop(this.list[i].vec, 0, this.idx, this.list[i].data[0], this.list[i].isfake);
						this.list[i].data.Remove(this.list[i].data[0]);
						bool flag3 = this.list[i].data.Count <= 0;
						if (flag3)
						{
							this.list.Remove(this.list[i]);
						}
						bool flag4 = i + 1 >= this.list.Count;
						if (flag4)
						{
							this.f = 0.05f;
						}
					}
				}
			}
			bool flag5 = NetClient.instance == null;
			if (!flag5)
			{
				long curServerTimeStampMS = NetClient.instance.CurServerTimeStampMS;
				List<DropItem> list = new List<DropItem>(this.dDropItem.Values);
				for (int j = 0; j < list.Count; j++)
				{
					list[j].update(curServerTimeStampMS);
					bool flag6 = list[j].itemdta.ownerId == 0u;
					if (flag6)
					{
						bool flag7 = !this.dDropItem_own.ContainsKey(list[j].itemdta.dpid);
						if (flag7)
						{
							this.dDropItem_own[list[j].itemdta.dpid] = list[j];
						}
					}
				}
			}
		}

		private void initRoomObj()
		{
			bool flag = this.svrConf.ContainsKey("l");
			if (flag)
			{
				List<Variant> arr = this.svrConf["l"]._arr;
				foreach (Variant current in arr)
				{
					this.addRoomObj(current);
				}
			}
		}

		private void addRoomObj(Variant v)
		{
			bool flag = this.roomItemCon == null;
			if (flag)
			{
				this.roomItemCon = new GameObject();
				this.roomItemCon.name = "roomObjs";
			}
			GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
			gameObject.transform.SetParent(this.roomItemCon.transform);
			gameObject.transform.position = new Vector3(v["ux"], v["uy"], v["uz"]);
			gameObject.transform.localScale = new Vector3(v["uw"], 1f, v["uh"]);
			TransMapPoint transMapPoint = gameObject.AddComponent<TransMapPoint>();
			transMapPoint.id = v["id"];
			transMapPoint.mapid = v["gto"]._uint;
			bool flag2 = v.ContainsKey("faceto");
			if (flag2)
			{
				transMapPoint.faceto = v["faceto"];
			}
			transMapPoint.init();
		}

		public void removeDropItm(uint dpid, bool isfake)
		{
			if (isfake)
			{
				bool flag = this.dDropFakeItem.ContainsKey(dpid);
				if (flag)
				{
					this.dDropFakeItem[dpid].dispose();
					this.dDropFakeItem.Remove(dpid);
				}
			}
			else
			{
				Dictionary<uint, DropItem> expr_42 = this.dDropItem;
				bool flag2 = expr_42 != null && expr_42.ContainsKey(dpid);
				if (flag2)
				{
					this.dDropItem[dpid].dispose();
					DropItemUIMgr.getInstance().hideOne(this.dDropItem[dpid]);
					this.dDropItem.Remove(dpid);
					bool flag3 = this.dDropItem_own.ContainsKey(dpid);
					if (flag3)
					{
						this.dDropItem_own.Remove(dpid);
					}
				}
			}
		}

		public void flyGetItmTxt(uint dpid, bool isfake)
		{
			if (isfake)
			{
				bool flag = this.dDropFakeItem.ContainsKey(dpid);
				if (flag)
				{
					this.flygetText(this.dDropFakeItem[dpid]);
				}
			}
			else
			{
				bool flag2 = this.dDropItem.ContainsKey(dpid);
				if (flag2)
				{
					this.flygetText(this.dDropItem[dpid]);
				}
			}
		}

		public void flygetText(DropItem item)
		{
			bool flag = item.itemdta.tpid == 0;
			if (flag)
			{
				MediaClient.instance.PlaySoundUrl("audio/common/collect_coin", false, null);
				bool flag2 = GameRoomMgr.getInstance().curRoom != null;
				if (flag2)
				{
					GameRoomMgr.getInstance().curRoom.onPickMoney(item.itemdta.count);
				}
				bool flag3 = a3_insideui_fb.instance != null;
				if (flag3)
				{
					this.goldnum = (uint)a3_insideui_fb.instance.addmoney;
				}
				else
				{
					this.goldnum += (uint)item.itemdta.count;
				}
			}
			else
			{
				string @string = item.itemdta.itemXml.getString("item_name");
				int @int = item.itemdta.itemXml.getInt("quality");
				flytxt.instance.fly(string.Concat(new object[]
				{
					ContMgr.getCont("gameroom_pick", null),
					Globle.getColorStrByQuality(@string, @int),
					"x",
					item.itemdta.count
				}), 0, default(Color), null);
				DropItemdta dropItemdta = new DropItemdta();
				dropItemdta.tpid = item.itemdta.itemXml.getInt("id");
				dropItemdta.num = item.itemdta.count;
				foreach (DropItemdta current in this.list2)
				{
					bool flag4 = dropItemdta.tpid == current.tpid;
					if (flag4)
					{
						current.num += item.itemdta.count;
						return;
					}
				}
				this.list2.Add(dropItemdta);
			}
		}

		public void clear()
		{
			foreach (DropItem current in this.dDropItem.Values)
			{
				current.dispose();
			}
			this.dDropItem.Clear();
			this.dDropItem_own.Clear();
			foreach (DropItem current2 in this.dDropFakeItem.Values)
			{
				current2.dispose();
			}
			this.dDropFakeItem.Clear();
		}

		public void showDropItem(Vector3 pos, List<DropItemdta> vecItem, bool isfake = false)
		{
			bool loading = GRMap.loading;
			if (!loading)
			{
				bool flag = vecItem.Count > 0;
				if (flag)
				{
					drop_data item = default(drop_data);
					item.data = vecItem;
					item.vec = pos;
					item.isfake = isfake;
					this.list.Add(item);
				}
				this.initDropOffset();
			}
		}

		private int showDrop(Vector3 dropPos, int wrongcount, int idx, DropItemdta item, bool isfake = false)
		{
			bool flag = this.lDropOffset.Count <= idx;
			if (flag)
			{
				idx = 0;
			}
			Vector3 vector = dropPos + this.lDropOffset[idx];
			Vector3 vector2 = vector;
			vector2.y = -99f;
			NavMeshHit navMeshHit;
			NavMesh.SamplePosition(vector, out navMeshHit, 100f, NavmeshUtils.allARE);
			Vector3 position = navMeshHit.position;
			bool flag2 = position.x == vector.x && position.z == vector.z;
			int result;
			if (flag2)
			{
				vector.y = position.y;
				DropItem dropItem = BaseRoomItem.getDropItem(vector, Vector3.zero, item, isfake);
				DropItemUIMgr.getInstance().show(dropItem, dropItem.itemdta.getDropItemName());
				bool flag3 = !isfake;
				if (flag3)
				{
					this.dDropItem[item.dpid] = dropItem;
					bool flag4 = item.ownerId == ModelBase<PlayerModel>.getInstance().cid || item.ownerId == 0u || (BaseProxy<TeamProxy>.getInstance().MyTeamData != null && item.ownerId == BaseProxy<TeamProxy>.getInstance().MyTeamData.teamId);
					if (flag4)
					{
						this.dDropItem_own[item.dpid] = dropItem;
					}
				}
				else
				{
					item.dpid = this.fakeItemIdx;
					this.dDropFakeItem[item.dpid] = dropItem;
					this.fakeItemIdx += 1u;
				}
				result = idx + 1;
			}
			else
			{
				bool flag5 = wrongcount >= 3;
				if (flag5)
				{
					debug.Log(":" + item.dpid);
					DropItem dropItem2 = BaseRoomItem.getDropItem(position, Vector3.zero, item, false);
					DropItemUIMgr.getInstance().show(dropItem2, dropItem2.itemdta.getDropItemName());
					bool flag6 = !isfake;
					if (flag6)
					{
						this.dDropItem[item.dpid] = dropItem2;
						bool flag7 = item.ownerId == ModelBase<PlayerModel>.getInstance().cid || item.ownerId == 0u || (BaseProxy<TeamProxy>.getInstance().MyTeamData != null && item.ownerId == BaseProxy<TeamProxy>.getInstance().MyTeamData.teamId);
						if (flag7)
						{
							this.dDropItem_own[item.dpid] = dropItem2;
						}
					}
					else
					{
						item.dpid = this.fakeItemIdx;
						this.dDropFakeItem[item.dpid] = dropItem2;
						this.fakeItemIdx += 1u;
					}
					result = idx + 1;
				}
				else
				{
					wrongcount++;
					result = this.showDrop(dropPos, wrongcount, idx + 1, item, isfake);
				}
			}
			return result;
		}

		public static DropItem getDropItem(Vector3 vec, Vector3 eulerAngles, DropItemdta item, bool isfake = false)
		{
			DropItem dropItem = DropItem.create(item);
			dropItem.lastGetTimer = 0;
			dropItem.transform.position = vec;
			dropItem.transform.eulerAngles = dropItem.transform.eulerAngles + eulerAngles;
			dropItem.isFake = isfake;
			return dropItem;
		}

		public void CollectAllDrops()
		{
			bool flag = this.dDropItem != null;
			if (flag)
			{
				foreach (uint current in this.dDropItem.Keys)
				{
					BaseProxy<MapProxy>.getInstance().sendPickUpItem(current);
				}
			}
		}

		public void CollectAllDrops1()
		{
			bool flag = this.dDropItem != null;
			if (flag)
			{
				foreach (uint current in this.dDropItem.Keys)
				{
					BaseProxy<MapProxy>.getInstance().sendPickUpItem(current);
				}
			}
		}

		private void initDropOffset()
		{
			bool flag = this.lDropOffset == null;
			if (flag)
			{
				this.lDropOffset = new List<Vector3>();
				this.lDropOffset.Add(Vector3.zero);
				this.formatDropOffset(1.7m);
			}
		}

		private void formatDropOffset(decimal step)
		{
			decimal num = -step;
			decimal num2 = num;
			while (num2 <= step)
			{
				bool flag = num2 == num || num2 == step;
				if (flag)
				{
					decimal num3 = num;
					while (num3 <= step)
					{
						this.lDropOffset.Add(new Vector3((float)num2, 0f, (float)num3));
						num3 += 1.7m;
					}
				}
				else
				{
					this.lDropOffset.Add(new Vector3((float)num2, 0f, (float)step));
					this.lDropOffset.Add(new Vector3((float)num2, 0f, (float)(-step)));
				}
				num2 += 1.7m;
			}
			bool flag2 = step < 5.10m;
			if (flag2)
			{
				this.formatDropOffset(step + 1.7m);
			}
		}

		public virtual void onMonsterDied(MonsterRole monster)
		{
		}

		public virtual void onPickMoney(int num)
		{
		}

		public virtual void onAddExp(int num)
		{
		}

		public virtual bool onLevelFinish(Variant msgData)
		{
			this.expnum = ModelBase<PlayerModel>.getInstance().GetNeedExp(this.zhuan, this.lvl, this.expnum, ModelBase<PlayerModel>.getInstance().up_lvl, ModelBase<PlayerModel>.getInstance().lvl, ModelBase<PlayerModel>.getInstance().exp);
			bool flag = msgData.ContainsKey("kill_exp");
			if (flag)
			{
				this.expnum = msgData["kill_exp"];
			}
			bool flag2 = msgData.ContainsKey("money");
			if (flag2)
			{
				this.goldnum += msgData["money"];
			}
			return false;
		}

		public virtual bool onPrizeFinish(Variant msgData)
		{
			return false;
		}

		public virtual bool onLevel_Status_Changes(Variant msgData)
		{
			return false;
		}

		public virtual void onGetMapMoney(int money)
		{
		}
	}
}
