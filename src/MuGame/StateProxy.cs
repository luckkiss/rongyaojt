using MuGame.Qsmy.model;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class StateProxy : StateBase
	{
		public static StateProxy Instance = new StateProxy();

		private float timer = 0f;

		private float shop_timer = 1000f;

		private bool hasInit = false;

		private float buymptimer = 0f;

		private float buyhptimer = 0f;

		private int cdtime = 20;

		private int cdtimeM = 20;

		private List<int> hpOrder = new List<int>();

		private List<int> mpOrder = new List<int>();

		private List<int> mhpOrder = new List<int>();

		public override void Enter()
		{
		}

		public override void Execute(float delta_time)
		{
			this.buyhptimer += delta_time;
			this.buymptimer += delta_time;
			this.timer += delta_time;
			bool flag = this.timer < 1f;
			if (!flag)
			{
				this.timer -= 1f;
				this.shop_timer += 1f;
				bool isDead = SelfRole._inst.isDead;
				if (!isDead)
				{
					bool flag2 = !this.hasInit;
					if (flag2)
					{
						this.Init();
					}
					this.TryNormalHp();
					this.TryNormalMp();
					bool autofighting = SelfRole.fsm.Autofighting;
					if (autofighting)
					{
						this.TryShopHp();
					}
				}
			}
		}

		public override void Exit()
		{
		}

		private void Init()
		{
			AutoPlayModel instance = ModelBase<AutoPlayModel>.getInstance();
			SXML autoplayXml = instance.AutoplayXml;
			string @string = autoplayXml.GetNode("recover_hp", "").getString("order");
			string[] array = @string.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				this.hpOrder.Add(int.Parse(array[i]));
			}
			@string = autoplayXml.GetNode("recover_mp", "").getString("order");
			array = @string.Split(new char[]
			{
				','
			});
			for (int j = 0; j < array.Length; j++)
			{
				this.mpOrder.Add(int.Parse(array[j]));
			}
			this.hasInit = true;
		}

		private void TryNormalHp()
		{
			this.cdtime++;
			bool flag = ModelBase<PlayerModel>.getInstance().hp >= ModelBase<AutoPlayModel>.getInstance().NHpLower * ModelBase<PlayerModel>.getInstance().max_hp / 100;
			if (!flag)
			{
				int normalHpID = this.GetNormalHpID();
				bool flag2 = normalHpID == -1;
				if (flag2)
				{
					bool flag3 = ModelBase<AutoPlayModel>.getInstance().BuyDrug == 0;
					if (!flag3)
					{
						SXML autoplayXml = ModelBase<AutoPlayModel>.getInstance().AutoplayXml;
						List<SXML> nodeList = autoplayXml.GetNodeList("supply_hp", "playlimit==" + ModelBase<PlayerModel>.getInstance().up_lvl);
						uint num = 0u;
						uint num2 = 0u;
						foreach (SXML current in nodeList)
						{
							int @int = current.getInt("playerlevel");
							num = current.getUint("hp_id");
							num2 = current.getUint("max_num");
							bool flag4 = (ulong)ModelBase<PlayerModel>.getInstance().lvl < (ulong)((long)@int);
							if (flag4)
							{
								break;
							}
						}
						bool flag5 = num > 0u;
						if (flag5)
						{
							shopDatas shopDataById = ModelBase<Shop_a3Model>.getInstance().GetShopDataById((int)num);
							bool flag6 = shopDataById == null || shopDataById.value <= 0;
							if (!flag6)
							{
								bool flag7 = (ulong)ModelBase<PlayerModel>.getInstance().money < (ulong)((long)shopDataById.value);
								if (flag7)
								{
									flytxt.instance.fly(err_string.get_Err_String(-4000), 0, default(Color), null);
								}
								else
								{
									bool flag8 = !ModelBase<a3_BagModel>.getInstance().getHaveRoom();
									if (!flag8)
									{
										bool flag9 = (ulong)num2 * (ulong)((long)shopDataById.value) <= (ulong)ModelBase<PlayerModel>.getInstance().money;
										uint num3;
										if (flag9)
										{
											num3 = num2;
										}
										else
										{
											num3 = (uint)((ulong)ModelBase<PlayerModel>.getInstance().money / (ulong)((long)shopDataById.value));
										}
										bool flag10 = this.buyhptimer > 2f;
										if (flag10)
										{
											BaseProxy<Shop_a3Proxy>.getInstance().BuyStoreItems(num, num3);
											this.buyhptimer = 0f;
										}
									}
								}
							}
						}
					}
				}
				else
				{
					SXML sXML = XMLMgr.instance.GetSXML("item", "");
					SXML node = sXML.GetNode("item", "id==" + (uint)normalHpID);
					a3_ItemData a3_ItemData = default(a3_ItemData);
					a3_ItemData.tpid = (uint)normalHpID;
					a3_ItemData.cd_time = node.getFloat("cd");
					bool flag11 = this.cdtime == 0;
					if (flag11)
					{
						ModelBase<a3_BagModel>.getInstance().useItemByTpid((uint)normalHpID, 1);
					}
					bool flag12 = (float)this.cdtime > a3_ItemData.cd_time;
					if (flag12)
					{
						ModelBase<a3_BagModel>.getInstance().useItemByTpid((uint)normalHpID, 1);
						this.cdtime = 0;
					}
				}
			}
		}

		private int GetNormalHpID()
		{
			List<int>.Enumerator enumerator = this.hpOrder.GetEnumerator();
			int result;
			while (enumerator.MoveNext())
			{
				bool flag = ModelBase<a3_BagModel>.getInstance().hasItem((uint)enumerator.Current);
				a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)enumerator.Current);
				bool flag2 = (long)itemDataById.use_limit < (long)((ulong)ModelBase<PlayerModel>.getInstance().up_lvl) || ((long)itemDataById.use_limit == (long)((ulong)ModelBase<PlayerModel>.getInstance().up_lvl) && (long)itemDataById.use_lv <= (long)((ulong)ModelBase<PlayerModel>.getInstance().lvl));
				bool flag3 = flag & flag2;
				if (flag3)
				{
					result = enumerator.Current;
					return result;
				}
			}
			result = -1;
			return result;
		}

		private void TryNormalMp()
		{
			this.cdtimeM++;
			bool flag = (long)ModelBase<PlayerModel>.getInstance().mp >= (long)ModelBase<AutoPlayModel>.getInstance().NMpLower * (long)((ulong)ModelBase<PlayerModel>.getInstance().max_mp) / 100L;
			if (!flag)
			{
				int normalMpID = this.GetNormalMpID();
				bool flag2 = normalMpID == -1;
				if (flag2)
				{
					bool flag3 = ModelBase<AutoPlayModel>.getInstance().BuyDrug == 0;
					if (!flag3)
					{
						SXML autoplayXml = ModelBase<AutoPlayModel>.getInstance().AutoplayXml;
						List<SXML> nodeList = autoplayXml.GetNodeList("supply_mp", "playlimit==" + ModelBase<PlayerModel>.getInstance().up_lvl);
						uint num = 0u;
						uint num2 = 0u;
						foreach (SXML current in nodeList)
						{
							int @int = current.getInt("playerlevel");
							num = current.getUint("mp_id");
							num2 = current.getUint("max_num");
							bool flag4 = (ulong)ModelBase<PlayerModel>.getInstance().lvl < (ulong)((long)@int);
							if (flag4)
							{
								break;
							}
						}
						bool flag5 = num > 0u;
						if (flag5)
						{
							shopDatas shopDataById = ModelBase<Shop_a3Model>.getInstance().GetShopDataById((int)num);
							bool flag6 = shopDataById == null || shopDataById.value <= 0;
							if (!flag6)
							{
								bool flag7 = (ulong)ModelBase<PlayerModel>.getInstance().money < (ulong)((long)shopDataById.value);
								if (flag7)
								{
									flytxt.instance.fly(err_string.get_Err_String(-4000), 0, default(Color), null);
								}
								else
								{
									bool flag8 = !ModelBase<a3_BagModel>.getInstance().getHaveRoom();
									if (!flag8)
									{
										bool flag9 = (ulong)num2 * (ulong)((long)shopDataById.value) <= (ulong)ModelBase<PlayerModel>.getInstance().money;
										uint num3;
										if (flag9)
										{
											num3 = num2;
										}
										else
										{
											num3 = (uint)((ulong)ModelBase<PlayerModel>.getInstance().money / (ulong)((long)shopDataById.value));
										}
										bool flag10 = this.buymptimer > 2f;
										if (flag10)
										{
											BaseProxy<Shop_a3Proxy>.getInstance().BuyStoreItems(num, num3);
											this.buymptimer = 0f;
										}
									}
								}
							}
						}
					}
				}
				else
				{
					SXML sXML = XMLMgr.instance.GetSXML("item", "");
					SXML node = sXML.GetNode("item", "id==" + (uint)normalMpID);
					a3_ItemData a3_ItemData = default(a3_ItemData);
					a3_ItemData.tpid = (uint)normalMpID;
					a3_ItemData.cd_time = node.getFloat("cd");
					bool flag11 = this.cdtimeM == 0;
					if (flag11)
					{
						ModelBase<a3_BagModel>.getInstance().useItemByTpid((uint)normalMpID, 1);
					}
					bool flag12 = (float)this.cdtimeM > a3_ItemData.cd_time;
					if (flag12)
					{
						ModelBase<a3_BagModel>.getInstance().useItemByTpid((uint)normalMpID, 1);
						this.cdtimeM = 0;
					}
				}
			}
		}

		private int GetNormalMpID()
		{
			List<int>.Enumerator enumerator = this.mpOrder.GetEnumerator();
			int result;
			while (enumerator.MoveNext())
			{
				bool flag = ModelBase<a3_BagModel>.getInstance().hasItem((uint)enumerator.Current);
				a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)enumerator.Current);
				bool flag2 = (long)itemDataById.use_limit < (long)((ulong)ModelBase<PlayerModel>.getInstance().up_lvl) || ((long)itemDataById.use_limit == (long)((ulong)ModelBase<PlayerModel>.getInstance().up_lvl) && (long)itemDataById.use_lv <= (long)((ulong)ModelBase<PlayerModel>.getInstance().lvl));
				bool flag3 = flag & flag2;
				if (flag3)
				{
					result = enumerator.Current;
					return result;
				}
			}
			result = -1;
			return result;
		}

		private void TryShopHp()
		{
			bool flag = this.shop_timer < 10f;
			if (!flag)
			{
				int shopHpID = this.GetShopHpID();
				bool flag2 = shopHpID == -1;
				if (!flag2)
				{
					ModelBase<a3_BagModel>.getInstance().useItemByTpid((uint)shopHpID, 1);
					this.shop_timer = 0f;
				}
			}
		}

		private int GetShopHpID()
		{
			List<int>.Enumerator enumerator = this.mhpOrder.GetEnumerator();
			int result;
			while (enumerator.MoveNext())
			{
				bool flag = ModelBase<a3_BagModel>.getInstance().hasItem((uint)enumerator.Current);
				bool flag2 = flag;
				if (flag2)
				{
					result = enumerator.Current;
					return result;
				}
			}
			result = -1;
			return result;
		}
	}
}
