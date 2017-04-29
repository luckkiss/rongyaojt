using Cross;
using System;

namespace MuGame
{
	internal class a3_PkmodelProxy : BaseProxy<a3_PkmodelProxy>
	{
		public a3_PkmodelProxy()
		{
			this.addProxyListener(2u, new Action<Variant>(this.onLoadInfo));
			this.addProxyListener(32u, new Action<Variant>(this.onLoadWashredname));
		}

		public void sendProxy(int pkstate)
		{
			Variant variant = new Variant();
			variant["pk_state"] = pkstate;
			this.sendRPC(2u, variant);
		}

		public void sendWashredname(int moneytype)
		{
			Variant variant = new Variant();
			variant["tp"] = moneytype;
			this.sendRPC(32u, variant);
		}

		public void onLoadInfo(Variant data)
		{
			debug.Log("pk模式的信息：" + data.dump());
			bool flag = data.ContainsKey("pk_state");
			if (flag)
			{
				ModelBase<PlayerModel>.getInstance().now_pkState = data["pk_state"];
				switch (ModelBase<PlayerModel>.getInstance().now_pkState)
				{
				case 0:
					ModelBase<PlayerModel>.getInstance().pk_state = PK_TYPE.PK_PEACE;
					break;
				case 1:
					ModelBase<PlayerModel>.getInstance().pk_state = PK_TYPE.PK_PKALL;
					ModelBase<PlayerModel>.getInstance().m_unPK_Param = ModelBase<PlayerModel>.getInstance().cid;
					ModelBase<PlayerModel>.getInstance().m_unPK_Param2 = ModelBase<PlayerModel>.getInstance().cid;
					break;
				case 2:
					ModelBase<PlayerModel>.getInstance().pk_state = PK_TYPE.PK_TEAM;
					ModelBase<PlayerModel>.getInstance().m_unPK_Param = ModelBase<PlayerModel>.getInstance().teamid;
					ModelBase<PlayerModel>.getInstance().m_unPK_Param2 = ModelBase<PlayerModel>.getInstance().clanid;
					break;
				}
				bool flag2 = a3_pkmodel._instance;
				if (flag2)
				{
					a3_pkmodel._instance.ShowThisImage(data["pk_state"]);
				}
				bool flag3 = SelfRole.s_LockFX.gameObject != null;
				if (flag3)
				{
					PkmodelAdmin.RefreshShow(SelfRole._inst.m_LockRole, false, false);
				}
				InterfaceMgr.doCommandByLua("PlayerModel:getInstance().modPkState", "model/PlayerModel", new object[]
				{
					ModelBase<PlayerModel>.getInstance().now_pkState,
					true
				});
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_PKMODEL);
				NewbieModel.getInstance().hide();
			}
		}

		public void onLoadWashredname(Variant data)
		{
		}
	}
}
