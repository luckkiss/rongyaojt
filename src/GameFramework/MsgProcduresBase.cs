using Cross;
using System;
using UnityEngine;

namespace GameFramework
{
	public abstract class MsgProcduresBase : GameEventDispatcher, IMessageSend, IObjectPlugin
	{
		public NetClient g_mgr;

		private string _controlId;

		public string controlId
		{
			get
			{
				return this._controlId;
			}
			set
			{
				this._controlId = value;
			}
		}

		public MsgProcduresBase(IClientBase m)
		{
			this.g_mgr = (m as NetClient);
		}

		public abstract void init();

		public void sendRPC(uint cmd, Variant msg)
		{
			try
			{
				this.g_mgr.sendRpc(cmd, msg);
			}
			catch (Exception ex)
			{
				Debug.Log("sendRPC 消息错误 ID为" + cmd.ToString() + "  Exception " + ex.Message);
			}
		}

		public void sendTpkg(uint cmd, Variant msg)
		{
			try
			{
				this.g_mgr.sendTpkg(cmd, msg);
			}
			catch (Exception ex)
			{
				Debug.Log("sendTpkg 消息错误 ID为" + cmd.ToString() + "  Exception " + ex.Message);
			}
		}

		public void regMsgs(NetManager mgr)
		{
		}

		public void skexp_up(uint skillID, int tp = 0)
		{
		}
	}
}
