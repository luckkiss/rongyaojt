using System;
using System.Collections.Generic;

namespace Cross
{
	public class NetManager : IAppPlugin
	{
		public delegate RPCMsgProcesser RPCProcCreator();

		public delegate TPKGMsgProcesser TPKGProcCreator();

		protected Dictionary<uint, Session> _sessionMap = new Dictionary<uint, Session>();

		protected Dictionary<uint, NetManager.RPCProcCreator> _rpcMsgProcCls = new Dictionary<uint, NetManager.RPCProcCreator>();

		protected Dictionary<uint, LinkedList<RPCMsgProcesser>> _rpcMsgProcPool = new Dictionary<uint, LinkedList<RPCMsgProcesser>>();

		protected Dictionary<uint, NetManager.TPKGProcCreator> _tpkgMsgProcCls = new Dictionary<uint, NetManager.TPKGProcCreator>();

		protected Dictionary<uint, LinkedList<TPKGMsgProcesser>> _tpkgMsgProcPool = new Dictionary<uint, LinkedList<TPKGMsgProcesser>>();

		public string pluginName
		{
			get
			{
				return "net";
			}
		}

		public void onPreInit()
		{
		}

		public void onInit()
		{
		}

		public void onPostInit()
		{
		}

		public void onFin()
		{
		}

		public void onResize(int width, int height)
		{
		}

		public void onPreRender(float tmSlice)
		{
		}

		public void onRender(float tmSlice)
		{
		}

		public void onPostRender(float tmSlice)
		{
		}

		public void onPreProcess(float tmSlice)
		{
		}

		public void onProcess(float tmSlice)
		{
			foreach (Session current in this._sessionMap.Values)
			{
				current.process(tmSlice);
			}
		}

		public void onPostProcess(float tmSlice)
		{
		}

		public void AddSession(uint uid, Session s)
		{
			this._sessionMap[uid] = s;
		}

		public void RemoveSession(uint uid)
		{
			bool flag = !this._sessionMap.ContainsKey(uid);
			if (!flag)
			{
				this._sessionMap.Remove(uid);
			}
		}

		public Session GetSession(uint uid)
		{
			bool flag = !this._sessionMap.ContainsKey(uid);
			Session result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this._sessionMap[uid];
			}
			return result;
		}

		public void regRPCProcesser(uint msgID, NetManager.RPCProcCreator procCrtFunc)
		{
			this._rpcMsgProcCls[msgID] = procCrtFunc;
		}

		public MsgProcesser createRPCProcesser(uint msgID, Session ses, Variant data)
		{
			bool flag = !this._rpcMsgProcCls.ContainsKey(msgID);
			MsgProcesser result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = !this._rpcMsgProcPool.ContainsKey(msgID);
				if (flag2)
				{
					this._rpcMsgProcPool[msgID] = new LinkedList<RPCMsgProcesser>();
				}
				LinkedList<RPCMsgProcesser> linkedList = this._rpcMsgProcPool[msgID];
				bool flag3 = linkedList.Count > 0;
				MsgProcesser msgProcesser;
				if (flag3)
				{
					msgProcesser = linkedList.Last.Value;
				}
				else
				{
					msgProcesser = this._rpcMsgProcCls[msgID]();
				}
				msgProcesser.msgId = msgID;
				msgProcesser.msgData = data;
				msgProcesser.session = ses;
				result = msgProcesser;
			}
			return result;
		}

		public void regTpkgProcesser(uint msgID, NetManager.TPKGProcCreator procCrtFunc)
		{
			this._tpkgMsgProcCls[msgID] = procCrtFunc;
		}

		public MsgProcesser createTpkgProcesser(uint msgID, Session ses, Variant data)
		{
			bool flag = !this._tpkgMsgProcCls.ContainsKey(msgID);
			MsgProcesser result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = !this._tpkgMsgProcPool.ContainsKey(msgID);
				if (flag2)
				{
					this._tpkgMsgProcPool[msgID] = new LinkedList<TPKGMsgProcesser>();
				}
				LinkedList<TPKGMsgProcesser> linkedList = this._tpkgMsgProcPool[msgID];
				bool flag3 = linkedList.Count > 0;
				MsgProcesser msgProcesser;
				if (flag3)
				{
					msgProcesser = linkedList.Last.Value;
				}
				else
				{
					msgProcesser = this._tpkgMsgProcCls[msgID]();
				}
				msgProcesser.msgId = msgID;
				msgProcesser.msgData = data;
				msgProcesser.session = ses;
				result = msgProcesser;
			}
			return result;
		}

		public void deleteMSGProcesser(MsgProcesser mp)
		{
			uint msgId = mp.msgId;
			bool flag = mp is RPCMsgProcesser;
			if (flag)
			{
				bool flag2 = !this._rpcMsgProcCls.ContainsKey(msgId);
				if (!flag2)
				{
					bool flag3 = !this._rpcMsgProcPool.ContainsKey(msgId);
					if (flag3)
					{
						this._rpcMsgProcPool[msgId] = new LinkedList<RPCMsgProcesser>();
					}
					LinkedList<RPCMsgProcesser> linkedList = this._rpcMsgProcPool[msgId];
					mp.msgData = null;
					mp.session = null;
					linkedList.AddLast(mp as RPCMsgProcesser);
				}
			}
			else
			{
				bool flag4 = mp is TPKGMsgProcesser;
				if (flag4)
				{
					bool flag5 = !this._tpkgMsgProcCls.ContainsKey(msgId);
					if (!flag5)
					{
						bool flag6 = !this._tpkgMsgProcPool.ContainsKey(msgId);
						if (flag6)
						{
							this._tpkgMsgProcPool[msgId] = new LinkedList<TPKGMsgProcesser>();
						}
						LinkedList<TPKGMsgProcesser> linkedList2 = this._tpkgMsgProcPool[msgId];
						mp.msgData = null;
						mp.session = null;
						linkedList2.AddLast(mp as TPKGMsgProcesser);
					}
				}
			}
		}
	}
}
