using Cross;
using GameFramework;
using System;
using UnityEngine;

namespace MuGame
{
	internal class A3_PetProxy : BaseProxy<A3_PetProxy>
	{
		public static uint EVENT_GET_LAST_TIME = 1201u;

		public static uint EVENT_GET_PET = 1202u;

		public static uint EVENT_HAVE_PET = 1203u;

		public static uint EVENT_SHOW_PET = 1204u;

		public static uint EVENT_FEED_PET = 1205u;

		public static uint PET_RENEW = 1206u;

		public static uint EVENT_USE_PETFEED = 0u;

		public A3_PetProxy()
		{
			this.addProxyListener(88u, new Action<Variant>(this.OnPet));
		}

		public void GetPets()
		{
			Variant variant = new Variant();
			variant["op"] = 1;
			this.sendRPC(88u, variant);
		}

		public void SendPetId(int id)
		{
			Variant variant = new Variant();
			variant["op"] = 14;
			variant["pet_id"] = id;
			this.sendRPC(88u, variant);
		}

		public void SendTime(int food_id)
		{
			Variant variant = new Variant();
			variant["op"] = 15;
			variant["food_id"] = food_id;
			this.sendRPC(88u, variant);
		}

		public void Feed()
		{
			Variant variant = new Variant();
			variant["op"] = 2;
			this.sendRPC(88u, variant);
		}

		public void Bless(bool useyb = false)
		{
			Variant variant = new Variant();
			variant["op"] = 3;
			variant["use_yb"] = (useyb ? 1 : 0);
			this.sendRPC(88u, variant);
		}

		public void Stage(int crystal_num)
		{
			Variant variant = new Variant();
			variant["op"] = 4;
			variant["crystal_num"] = crystal_num;
			this.sendRPC(88u, variant);
		}

		public void SetAutoFeed(uint isOn)
		{
			Variant variant = new Variant();
			variant["op"] = 8;
			variant["auto_feed"] = isOn;
			this.sendRPC(88u, variant);
		}

		public void SetAutoBuy(uint isOn)
		{
			Variant variant = new Variant();
			variant["op"] = 13;
			variant["auto_buy_feeds"] = isOn;
			this.sendRPC(88u, variant);
		}

		public void OneKeyBless(bool useyb = false)
		{
			Variant variant = new Variant();
			variant["op"] = 12;
			variant["use_yb"] = (useyb ? 1 : 0);
			this.sendRPC(88u, variant);
		}

		public void OnPet(Variant data)
		{
			debug.Log("宠物信息:" + data.dump());
			int err_code = data["res"];
			switch (err_code)
			{
			case 13:
				A3_PetModel.showrenew = true;
				break;
			case 14:
				this.OnPetId(data);
				break;
			case 15:
				this.GetTime(data);
				break;
			case 16:
				this.Getpet(data);
				break;
			case 17:
				this.petHas(data);
				break;
			default:
				flytxt.instance.fly(err_string.get_Err_String(err_code), 0, default(Color), null);
				break;
			}
		}

		private void petHas(Variant data)
		{
			debug.Log(data.dump());
			base.dispatchEvent(GameEvent.Create(A3_PetProxy.EVENT_HAVE_PET, this, data, false));
		}

		private void OnPetId(Variant data)
		{
			debug.Log(data.dump());
			OtherPlayerMgr._inst.otherPlayPetAvatarChange(data["iid"], data["pet_id"], 0);
		}

		private void GetTime(Variant data)
		{
			debug.Log(data.dump());
			base.dispatchEvent(GameEvent.Create(A3_PetProxy.EVENT_GET_LAST_TIME, this, data, false));
			base.dispatchEvent(GameEvent.Create(A3_PetProxy.EVENT_SHOW_PET, this, data, false));
			base.dispatchEvent(GameEvent.Create(A3_PetProxy.EVENT_FEED_PET, this, data, false));
		}

		private void Getpet(Variant data)
		{
			debug.Log(data.dump());
			ModelBase<A3_PetModel>.getInstance().petId = data["pet"]["id_arr"]._arr;
			A3_PetModel.curPetid = (uint)data["pet"]["id"]._int;
			base.dispatchEvent(GameEvent.Create(A3_PetProxy.EVENT_GET_PET, this, data, false));
		}

		private void OnBless(Variant data)
		{
			A3_PetModel instance = ModelBase<A3_PetModel>.getInstance();
			instance.Tpid = data["id"];
			instance.Level = data["level"];
			instance.Exp = data["exp"];
		}

		private void OnGetPets(Variant data)
		{
			bool flag = data.ContainsKey("pet");
			if (flag)
			{
				Variant variant = data["pet"];
				A3_PetModel instance = ModelBase<A3_PetModel>.getInstance();
				instance.Tpid = variant["id"];
			}
		}

		private void OnStage(Variant data)
		{
			A3_PetModel instance = ModelBase<A3_PetModel>.getInstance();
			instance.Tpid = data["id"];
			instance.Stage = data["stage"];
			instance.Level = 1;
			instance.Exp = 0;
			flytxt.instance.fly("恭喜,升阶成功!", 0, default(Color), null);
		}

		private void OnAutoPlayUpgrade(Variant data)
		{
			A3_PetModel instance = ModelBase<A3_PetModel>.getInstance();
			instance.Tpid = data["id"];
			instance.Exp = data["exp"];
			bool flag = data.ContainsKey("level");
			if (flag)
			{
				instance.Level = data["level"];
			}
		}

		private void OnSyncStarvation(Variant data)
		{
			A3_PetModel instance = ModelBase<A3_PetModel>.getInstance();
			instance.Starvation = data["starvation"];
			bool flag = a3_pet_skin.instan != null;
			if (flag)
			{
				a3_pet_skin.instan.OnStarvationChange();
			}
		}

		private void OnSyncAutoFeed(Variant data)
		{
			A3_PetModel instance = ModelBase<A3_PetModel>.getInstance();
			bool flag = data["auto_feed"] == 0;
			if (flag)
			{
				instance.Auto_feed = false;
			}
			else
			{
				instance.Auto_feed = true;
			}
		}

		private void OnSyncAutoBuy(Variant data)
		{
			A3_PetModel instance = ModelBase<A3_PetModel>.getInstance();
			bool flag = data["auto_buy_feeds"] == 0;
			if (flag)
			{
				instance.Auto_buy = false;
			}
			else
			{
				instance.Auto_buy = true;
			}
			int itemNumByTpid = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(instance.GetFeedItemTpid());
			bool flag2 = instance.Auto_buy && itemNumByTpid <= 0;
			if (flag2)
			{
				ModelBase<A3_PetModel>.getInstance().AutoBuy();
			}
		}

		private void OnOtherPlayerPetChange(Variant data)
		{
			OtherPlayerMgr._inst.PlayPetAvatarChange(data["iid"], data["id"], 0);
		}

		private void OnOneKeyBless(Variant data)
		{
			A3_PetModel instance = ModelBase<A3_PetModel>.getInstance();
			bool flag = data.ContainsKey("multi_res");
			if (flag)
			{
				int err_code = data["multi_res"];
				flytxt.instance.fly(err_string.get_Err_String(err_code), 0, default(Color), null);
			}
			instance.Tpid = data["id"];
			instance.Level = data["level"];
			instance.Exp = data["exp"];
		}
	}
}
