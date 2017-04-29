using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MuGame
{
	internal class ChatProxy : BaseProxy<ChatProxy>
	{
		public const uint S2C_SYS_NOTICE = 159u;

		public static uint lis_sys_notice = 159u;

		public ChatProxy()
		{
			this.addProxyListener(161u, new Action<Variant>(this.onTalking));
			this.addProxyListener(160u, new Action<Variant>(this.getPublish));
			this.addProxyListener(159u, new Action<Variant>(this.getNotice));
		}

		private void onTalking(Variant data)
		{
			debug.Log("发送服务器成功" + data.dump());
			UIClient.instance.dispatchEvent(GameEvent.Create(14001u, this, data, false));
		}

		private void getPublish(Variant data)
		{
			debug.Log("聊天所有信息" + data.dump());
			bool flag = data.ContainsKey("cid");
			if (flag)
			{
				uint @uint = data["cid"]._uint;
				bool flag2 = BaseProxy<FriendProxy>.getInstance().BlackDataList.ContainsKey(@uint);
				if (flag2)
				{
					return;
				}
			}
			bool flag3 = data.ContainsKey("res");
			if (flag3)
			{
				int num = data["res"];
				bool flag4 = num < 0;
				if (flag4)
				{
					Globle.err_output(num);
				}
				else
				{
					bool flag5 = num == 1;
					if (flag5)
					{
						a3_chatroom._instance.meSays(false);
					}
				}
			}
			else
			{
				ChatToType @int = (ChatToType)data["tp"]._int;
				if (@int != ChatToType.Nearby)
				{
					if (@int == ChatToType.PrivateSecretlanguage)
					{
						bool iGNORE_PRIVATE_INFO = GlobleSetting.IGNORE_PRIVATE_INFO;
						if (iGNORE_PRIVATE_INFO)
						{
							return;
						}
					}
				}
				else
				{
					bool flag6 = !data.ContainsKey("url");
					if (flag6)
					{
						uint uint2 = data["cid"]._uint;
						string str = data["msg"]._str;
						foreach (KeyValuePair<uint, ProfessionRole> current in OtherPlayerMgr._inst.m_mapOtherPlayerSee)
						{
							bool flag7 = current.Value.m_unCID == uint2;
							if (flag7)
							{
								PlayerChatUIMgr.getInstance().show(current.Value, this.analysisStrName(str));
							}
						}
					}
				}
				bool flag8 = data["tp"] == 10;
				if (flag8)
				{
					bool flag9 = broadcasting.instance != null;
					if (flag9)
					{
						broadcasting.instance.addMsg(data["msg"]);
					}
					data["tp"] = 6;
					bool flag10 = a3_chatroom._instance != null;
					if (flag10)
					{
						a3_chatroom._instance.otherSays(data);
					}
				}
				else
				{
					bool flag11 = a3_chatroom._instance != null;
					if (flag11)
					{
						a3_chatroom._instance.otherSays(data);
					}
				}
			}
		}

		private void getNotice(Variant data)
		{
			debug.Log("走马灯159" + data.dump());
			base.dispatchEvent(GameEvent.Create(ChatProxy.lis_sys_notice, this, data, false));
		}

		public void sendMsg(string words, string name, uint type, bool isvoice)
		{
			Variant variant = new Variant();
			variant["tp"] = type;
			variant["name"] = name;
			if (isvoice)
			{
				variant["msg"] = "";
				variant["url"] = words;
			}
			else
			{
				variant["msg"] = words;
			}
			switch (type)
			{
			}
			this.analysisStr(words, variant);
			this.sendRPC(160u, variant);
		}

		private StringBuilder analysisStr(string str, Variant msg)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = str.Replace("]", "[");
			string[] array = text.Split(new char[]
			{
				'['
			});
			for (int i = 0; i < array.Length; i++)
			{
				bool flag = string.IsNullOrEmpty(array[i]);
				if (!flag)
				{
					stringBuilder.Append(array[i]);
					string text2 = array[i].Trim();
					Match match = Regex.Match(text2, "\\(.*?,.*?\\)");
					bool success = match.Success;
					if (success)
					{
						msg["coordinate"] = true;
					}
					else
					{
						bool flag2 = text2.Contains("#");
						if (flag2)
						{
							uint num;
							uint.TryParse(text2.Split(new char[]
							{
								'#'
							})[1], out num);
							bool flag3 = num > 0u;
							if (flag3)
							{
								bool flag4 = msg["itm_ids"] == null;
								if (flag4)
								{
									msg["itm_ids"] = new Variant();
								}
								msg["itm_ids"].pushBack(num);
							}
						}
					}
				}
			}
			return stringBuilder;
		}

		public string analysisStrName(string msg)
		{
			string text = string.Empty;
			bool flag = !msg.Contains("[");
			string result;
			if (flag)
			{
				result = msg;
			}
			else
			{
				string text2 = msg.Replace("]", "[");
				string[] array = text2.Split(new char[]
				{
					'['
				});
				for (int i = 0; i < array.Length; i++)
				{
					bool flag2 = string.IsNullOrEmpty(array[i]);
					if (!flag2)
					{
						string text3 = array[i].Trim();
						Match match = Regex.Match(text3, "\\(.*?,.*?\\)");
						bool success = match.Success;
						if (success)
						{
							text = text + "[" + text3 + "]";
						}
						else
						{
							bool flag3 = text3.Contains("#");
							if (flag3)
							{
								text = text + "[" + text3.Split(new char[]
								{
									'#'
								})[0] + "]";
							}
							else
							{
								text += text3;
							}
						}
					}
				}
				result = text;
			}
			return result;
		}
	}
}
