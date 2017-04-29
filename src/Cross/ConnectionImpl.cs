using System;
using System.IO;
using System.Net.Sockets;
using UnityEngine;

namespace Cross
{
	public class ConnectionImpl : IConnection
	{
		public static bool s_bShowNetLog = false;

		protected Socket _sock = null;

		protected bool _isConnected = false;

		protected bool _isConnecting = false;

		protected bool _isLogedin = false;

		protected uint _uid = 0u;

		protected string _token = "";

		protected int _clnt = 0;

		protected long _last_hbs_time_ms = 0L;

		protected long _last_hbr_time_ms = 0L;

		protected long _latency_ms = 0L;

		protected ulong _last_server_tm_ms = 0uL;

		protected long _min_latency_ms = 0L;

		protected long _min_last_hbr_time_ms = 0L;

		protected ulong _min_last_server_tm_ms = 0uL;

		protected long _server_tm_diff_ms = 0L;

		protected long _server_tm_diff_s = 0L;

		protected uint _hb_interval_val = 2000u;

		protected long _last_hb_send_time = CCTime.getTickMillisec();

		protected uint _server_version = 0u;

		protected uint _protocal_version = 0u;

		protected Variant _config_versions = null;

		protected PackageCoderImpl _pack = new PackageCoderImpl();

		protected ByteArray _recvBuffer = new ByteArray(8192);

		protected ByteArray _sendBuffer = new ByteArray(4096);

		protected IConnectionEventHandler _cb = null;

		public bool isLogin
		{
			get
			{
				return this._isLogedin;
			}
		}

		public int Latency
		{
			get
			{
				return (int)this._latency_ms;
			}
		}

		public long CurServerTickTimeMS
		{
			get
			{
				return (long)(this._min_last_server_tm_ms + (ulong)(CCTime.getTickMillisec() - this._min_last_hbr_time_ms));
			}
		}

		public int CurServerTimeStamp
		{
			get
			{
				return (int)((long)CCTime.getCurTimestamp() + this._server_tm_diff_s);
			}
		}

		public long CurServerTimeStampMS
		{
			get
			{
				return CCTime.getCurTimestampMS() + this._server_tm_diff_ms;
			}
		}

		public uint ServerVersion
		{
			get
			{
				return this._server_version;
			}
		}

		public uint ProtocalVersion
		{
			get
			{
				return this._protocal_version;
			}
		}

		public Variant ConfigVersions
		{
			get
			{
				return this._config_versions;
			}
		}

		public uint HBInterval
		{
			get
			{
				return this._hb_interval_val;
			}
			set
			{
				this._hb_interval_val = value;
			}
		}

		public IConnectionEventHandler EventHandler
		{
			get
			{
				return this._cb;
			}
			set
			{
				this._cb = value;
			}
		}

		public bool isConnect
		{
			get
			{
				return this._isConnected;
			}
		}

		public void SetSec(ICrypt enc, ICrypt dec)
		{
		}

		public void ClearSec()
		{
		}

		public bool Connect(string addr, int port, uint uid, string token, uint client)
		{
			this._uid = uid;
			this._token = token;
			this._clnt = (int)client;
			bool isConnected = this._isConnected;
			if (isConnected)
			{
				this._sock.Close();
				this._isConnected = false;
			}
			try
			{
				bool flag = this._sock != null;
				if (flag)
				{
					this._sock.Close();
				}
				this._sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				this._sock.Blocking = false;
				this._sock.Connect(addr, port);
				this._isConnecting = true;
			}
			catch (SocketException ex)
			{
				bool flag2 = ex.ErrorCode != 10035;
				if (flag2)
				{
					this._cb.onError(ex.Message);
				}
			}
			return true;
		}

		public bool Disconnect()
		{
			this._sock.Disconnect(true);
			return true;
		}

		public void SetSHMSG(string msg, string addr, uint port)
		{
		}

		public bool RequestServerVersion()
		{
			ByteArray byteArray = new ByteArray(5);
			byteArray.writeByte((sbyte)this._make_sgn_pkg_header(16u));
			byteArray.writeUnsignedInt(0u);
			int num = (int)this.OSend(byteArray.data, byteArray.length);
			return true;
		}

		public uint PSendTPKG(uint cmdID, Variant par)
		{
			bool flag = cmdID <= 0u;
			uint result;
			if (flag)
			{
				result = 0u;
			}
			else
			{
				bool flag2 = !this._sock.Connected;
				if (flag2)
				{
					result = 0u;
				}
				else
				{
					ByteArray byteArray = this._pack.PackTypePackage(cmdID, par);
					uint length = (uint)byteArray.length;
					bool flag3 = byteArray.length > 256;
					if (flag3)
					{
					}
					this._sendBuffer.position = 0;
					this._sendBuffer.writeUnsignedInt(this._make_type_pkg_header((uint)(byteArray.length + 4), 0u));
					this._sendBuffer.writeBytes(byteArray, 0, byteArray.length);
					this._flushData(this._sendBuffer);
					bool flag4 = ConnectionImpl.s_bShowNetLog;
					if (flag4)
					{
						Debug.Log(string.Concat(new object[]
						{
							"底层 发送Tpkg协议：",
							cmdID,
							" msg = ",
							par.dump()
						}));
					}
					this._sendBuffer.length = 0;
					result = length;
				}
			}
			return result;
		}

		public uint PSendRPC(uint cmdID, Variant par)
		{
			bool flag = cmdID <= 0u;
			uint result;
			if (flag)
			{
				result = 0u;
			}
			else
			{
				bool flag2 = !this._sock.Connected;
				if (flag2)
				{
					result = 0u;
				}
				else
				{
					ByteArray byteArray = this._pack.PackRPCPackage(cmdID, par);
					bool flag3 = byteArray == null;
					if (flag3)
					{
						result = 0u;
					}
					else
					{
						uint length = (uint)byteArray.length;
						bool flag4 = byteArray.length > 256;
						if (flag4)
						{
						}
						this._sendBuffer.position = 0;
						this._sendBuffer.writeShort((short)this._make_rpc_pkg_header((uint)(byteArray.length + 2), 0u));
						this._sendBuffer.writeBytes(byteArray, 0, byteArray.length);
						this._flushData(this._sendBuffer);
						bool flag5 = ConnectionImpl.s_bShowNetLog;
						if (flag5)
						{
							Debug.Log(string.Concat(new object[]
							{
								"底层 发送消息 -- ID ",
								cmdID,
								" msg = ",
								par.dump()
							}));
						}
						this._sendBuffer.length = 0;
						result = length;
					}
				}
			}
			return result;
		}

		public uint OSend(byte[] data, int len)
		{
			return this.OSend(data, len, false);
		}

		public uint OSend(byte[] data, int len, bool ignoreEnc)
		{
			bool flag = !this._sock.Connected;
			uint result;
			if (flag)
			{
				result = 0u;
			}
			else
			{
				if (ignoreEnc)
				{
					this._sock.Send(data, len, SocketFlags.None);
				}
				else
				{
					this._sendBuffer.writeBytes(data, len);
					this._flushData(this._sendBuffer);
				}
				result = (uint)len;
			}
			return result;
		}

		protected void _flushData(ByteArray d)
		{
			this._sock.Send(d.data, d.length, SocketFlags.None);
		}

		protected void _tryUnpackPackage()
		{
			int num = 0;
			while (this._recvBuffer.bytesAvailable > 0)
			{
				ByteArray byteArray = new ByteArray();
				num++;
				uint num2 = (uint)this._recvBuffer.readUnsignedByte();
				uint num3 = num2 & 192u;
				bool flag = num3 == 0u;
				if (flag)
				{
					bool flag2 = num2 == 0u;
					if (flag2)
					{
						bool flag3 = this._recvBuffer.bytesAvailable < 4;
						if (flag3)
						{
							this._recvBuffer.position--;
							break;
						}
						this._last_server_tm_ms = (ulong)this._recvBuffer.readUnsignedInt();
						this._last_hbr_time_ms = CCTime.getTickMillisec();
						this._latency_ms = this._last_hbr_time_ms - this._last_hbs_time_ms;
						bool flag4 = this._min_last_server_tm_ms == 0uL;
						if (flag4)
						{
							this._min_last_server_tm_ms = this._last_server_tm_ms;
							this._min_last_hbr_time_ms = this._last_hbr_time_ms;
							this._min_latency_ms = this._latency_ms;
						}
						bool flag5 = this._latency_ms < this._min_latency_ms;
						if (flag5)
						{
							this._min_last_server_tm_ms = this._last_server_tm_ms;
							this._min_last_hbr_time_ms = this._last_hbr_time_ms;
							this._min_latency_ms = this._latency_ms;
						}
						this._cb.onHBRecv();
					}
				}
				else
				{
					bool flag6 = num3 == 64u;
					if (flag6)
					{
						bool flag7 = this._recvBuffer.bytesAvailable < 1;
						if (flag7)
						{
							this._recvBuffer.position--;
							break;
						}
						uint num4 = (num2 & 31u) << 8;
						num4 |= (uint)this._recvBuffer.readUnsignedByte();
						bool flag8 = (long)this._recvBuffer.bytesAvailable < (long)((ulong)(num4 - 2u));
						if (flag8)
						{
							this._recvBuffer.position -= 2;
							break;
						}
						this._recvBuffer.readBytes(byteArray, 0, (int)(num4 - 2u));
						bool flag9 = this._is_pkg_compressed(num2);
						if (flag9)
						{
							byteArray.uncompress();
						}
						Variant pkg_data = this._pack.UnpackRPCPackage(byteArray.data, byteArray.length);
						this._rpc_package_cb_fn(pkg_data, byteArray);
					}
					else
					{
						bool flag10 = num3 == 128u;
						if (flag10)
						{
							bool flag11 = this._recvBuffer.bytesAvailable < 3;
							if (flag11)
							{
								this._recvBuffer.position--;
								break;
							}
							uint num4 = (num2 & 31u) << 24;
							num4 |= (uint)this._recvBuffer.readUnsignedByte();
							num4 |= (uint)((uint)this._recvBuffer.readUnsignedShort() << 8);
							bool flag12 = (long)this._recvBuffer.bytesAvailable < (long)((ulong)(num4 - 4u));
							if (flag12)
							{
								this._recvBuffer.position -= 4;
								break;
							}
							this._recvBuffer.readBytes(byteArray, 0, (int)(num4 - 4u));
							bool flag13 = this._is_pkg_compressed(num2);
							if (flag13)
							{
								byteArray.uncompress();
							}
							Variant pkg_data = this._pack.UnpackTypePackage(byteArray.data, byteArray.length);
							this._type_package_cb_fn(pkg_data, byteArray);
						}
						else
						{
							bool flag14 = num3 == 192u;
							if (!flag14)
							{
								break;
							}
							bool flag15 = this._recvBuffer.bytesAvailable < 3;
							if (flag15)
							{
								this._recvBuffer.position--;
								break;
							}
							uint num4 = (num2 & 31u) << 24;
							num4 |= (uint)this._recvBuffer.readUnsignedByte();
							num4 |= (uint)((uint)this._recvBuffer.readUnsignedShort() << 8);
							bool flag16 = (long)this._recvBuffer.bytesAvailable < (long)((ulong)(num4 - 4u));
							if (flag16)
							{
								this._recvBuffer.position -= 4;
								break;
							}
							this._recvBuffer.readBytes(byteArray, 0, (int)(num4 - 4u));
							bool flag17 = this._is_pkg_compressed(num2);
							if (flag17)
							{
								byteArray.uncompress();
							}
							Variant pkg_data = this._pack.UnpackFullTypePackage(byteArray.data, byteArray.length);
							this._full_type_pacakge_cb_fn(pkg_data, byteArray);
						}
					}
				}
			}
		}

		public void onProcess()
		{
			bool flag = !this._sock.Connected;
			if (flag)
			{
				bool isConnected = this._isConnected;
				if (isConnected)
				{
					this._isConnected = false;
					this._cb.onConnectionClose();
				}
			}
			else
			{
				bool isConnecting = this._isConnecting;
				if (isConnecting)
				{
					this._isConnecting = false;
					this._isConnected = true;
					this._cb.onConnect();
					Variant variant = new Variant();
					variant["uid"] = this._uid;
					variant["tkn"] = this._token;
					variant["clnt"] = this._clnt;
					this.PSendTPKG(2u, variant);
				}
				long tickMillisec = CCTime.getTickMillisec();
				bool flag2 = tickMillisec - this._last_hb_send_time > (long)((ulong)this._hb_interval_val);
				if (flag2)
				{
					bool connected = this._sock.Connected;
					if (connected)
					{
						this._last_hbs_time_ms = tickMillisec;
						ByteArray byteArray = new ByteArray();
						byteArray.writeByte((sbyte)this._make_sgn_pkg_header(0u));
						byteArray.writeUnsignedInt((uint)(this._last_server_tm_ms + (ulong)(this._last_hbs_time_ms - this._last_hbr_time_ms)));
						this._flushData(byteArray);
						byteArray.length = 0;
						this._last_hb_send_time = tickMillisec;
					}
				}
				try
				{
					bool flag3 = this._sock.Available > 0;
					if (flag3)
					{
						bool flag4 = this._recvBuffer.position > 0;
						if (flag4)
						{
							this._recvBuffer.skip(this._recvBuffer.position);
							this._recvBuffer.position = 0;
						}
						this._recvBuffer.capcity = this._recvBuffer.length + this._sock.Available + 4096;
						int num = this._sock.Receive(this._recvBuffer.data, this._recvBuffer.length, this._sock.Available, SocketFlags.None);
						this._recvBuffer.length = this._recvBuffer.length + num;
						this._tryUnpackPackage();
					}
				}
				catch (SocketException ex)
				{
					bool flag5 = ex.ErrorCode != 10035;
					if (flag5)
					{
						this._cb.onError(ex.Message);
					}
				}
			}
		}

		private void _rpc_package_cb_fn(Variant pkg_data, ByteArray tmp)
		{
			bool flag = ConnectionImpl.s_bShowNetLog;
			if (flag)
			{
				Debug.Log("底层 rpc接收到消息包 -- ID " + pkg_data["cmd_id"] + "   msg = " + pkg_data.dump());
			}
			this._cb.onRPC(pkg_data["cmd_id"]._uint, pkg_data["cmd"]._str, pkg_data["data"]);
		}

		private void _type_package_cb_fn(Variant pkg_data, ByteArray tmp)
		{
			bool flag = ConnectionImpl.s_bShowNetLog;
			if (flag)
			{
				Debug.Log("底层 type接收到消息包 -- ID " + pkg_data.dump());
			}
			bool flag2 = pkg_data["cmd"]._uint == 2u;
			if (flag2)
			{
				this._isLogedin = (pkg_data["data"]["res"]._int == 1);
				bool isLogedin = this._isLogedin;
				if (isLogedin)
				{
					this._server_tm_diff_ms = (long)((ulong)(pkg_data["data"]["time"]._uint * 1000u) - (ulong)CCTime.getCurTimestampMS());
					this._server_tm_diff_s = (long)((ulong)pkg_data["data"]["time"]._uint - (ulong)((long)CCTime.getCurTimestamp()));
					this._cb.onLogin(pkg_data["data"]);
				}
				else
				{
					DebugTrace.add(Define.DebugTrace.DTT_ERR, "Login failed: " + pkg_data["data"]["res"]._int);
				}
			}
			else
			{
				this._cb.onTPKG(pkg_data["cmd"]._uint, pkg_data["data"]);
			}
		}

		private void _full_type_pacakge_cb_fn(Variant pkg_data, ByteArray tmp)
		{
			bool flag = ConnectionImpl.s_bShowNetLog;
			if (flag)
			{
				Debug.Log("底层 full_type接收到消息包 -- ID " + pkg_data.dump());
			}
			bool flag2 = pkg_data["cmd"]._uint == 1u;
			if (flag2)
			{
				bool flag3 = tmp != null;
				if (flag3)
				{
					string text = AssetManagerImpl.UPDATE_DOWN_PATH + "pl_" + this._protocal_version.ToString() + ".qsmsg";
					AssetManagerImpl.preparePath(text);
					FileStream fileStream = new FileStream(text, FileMode.Create);
					fileStream.Write(tmp.data, 0, tmp.length);
					fileStream.Flush();
					fileStream.Close();
					Debug.Log("写入服务器消息文件 " + text);
				}
				bool flag4 = this._pack.InitRPCPackageDescribe(pkg_data);
				if (flag4)
				{
					this._cb.onServerVersionRecv();
				}
				else
				{
					uint num = (uint)this._recvBuffer.readUnsignedShort();
					this._cb.onError("RPC_PROTOCAL_ERROR");
				}
			}
			else
			{
				bool flag5 = pkg_data["cmd"]._uint == 2u;
				if (flag5)
				{
					try
					{
						this._server_version = pkg_data["data"]["sver"]._uint;
						this._protocal_version = pkg_data["data"]["rpcver"]._uint;
					}
					catch (Exception ex)
					{
						this._cb.onError("SERVER_VERSION_ERROR" + ex.Message);
						return;
					}
					this._config_versions = pkg_data["data"];
					Debug.Log("当前_server_version版本号：" + this._server_version.ToString());
					Debug.Log("当前_protocal_version版本号：" + this._protocal_version.ToString());
					string path = AssetManagerImpl.UPDATE_DOWN_PATH + "pl_" + this._protocal_version.ToString() + ".qsmsg";
					bool flag6 = File.Exists(path) && false;
					if (flag6)
					{
						Debug.Log("加载消息描述 ---------------- 本地加载.............");
						new URLReqImpl
						{
							dataFormat = "binary",
							url = "pl_" + this._protocal_version.ToString() + ".qsmsg"
						}.load(delegate(IURLReq local_r, object local_ret)
						{
							byte[] array = local_ret as byte[];
							Variant pkg_data2 = this._pack.UnpackFullTypePackage(array, array.Length);
							this._full_type_pacakge_cb_fn(pkg_data2, null);
						}, null, delegate(IURLReq local_r, string err)
						{
							this._get_protocal_desc(this._protocal_version);
						});
					}
					else
					{
						this._get_protocal_desc(this._protocal_version);
					}
				}
				else
				{
					this._cb.onFullTPKG(pkg_data["cmd"]._uint, pkg_data["data"]);
				}
			}
		}

		private bool _get_protocal_desc(uint protocal_version)
		{
			DebugTrace.add(Define.DebugTrace.DTT_SYS, "request protocal info from server");
			this.PSendTPKG(1u, new Variant(0));
			return true;
		}

		private uint _make_sgn_pkg_header(uint cmd)
		{
			return cmd & 63u;
		}

		private uint _make_rpc_pkg_header(uint leng, uint comp)
		{
			return 64u | (comp & 1u) << 5 | (leng & 7936u) >> 8 | (leng & 255u) << 8;
		}

		private uint _make_type_pkg_header(uint leng, uint comp)
		{
			return 128u | (comp & 1u) << 5 | (leng & 520093696u) >> 24 | (leng & 16777215u) << 8;
		}

		private uint _make_full_type_pkg_header(uint leng, uint comp)
		{
			return 192u | (comp & 1u) << 5 | (leng & 520093696u) >> 24 | (leng & 16777215u) << 8;
		}

		private bool _is_pkg_compressed(uint header)
		{
			return (header & 32u) > 0u;
		}
	}
}
