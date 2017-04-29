using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class lgGDChat : lgGDBase
	{
		private Variant skill_list = new Variant();

		protected Variant sentChatArr = new Variant();

		protected Variant _chatPlayers = new Variant();

		private Variant _gag_keywords;

		private int _gag_limit = 3;

		private int _gag_min = 10;

		private int _gag_equal = 20;

		private int _gag_time = 60;

		private bool _is_gag = false;

		private string _savemsg = "";

		private int _cur_gag_cnt = 0;

		private bool _initConf = false;

		public lgGDChat(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new lgGDChat(m as gameManager);
		}

		public override void init()
		{
		}

		public void push(Variant msgData)
		{
			this.sentChatArr._arr.Add(msgData);
		}

		public void sendMsgSuccess()
		{
			bool flag = this.sentChatArr.Length > 0;
			if (flag)
			{
				Variant msgData = this.sentChatArr[this.sentChatArr.Length - 1];
				this.sentChatArr._arr.RemoveAt(this.sentChatArr.Length - 1);
				this.showChatMsg(msgData, true);
			}
		}

		public void on_chat_msg(Variant msgData)
		{
			bool flag = msgData.ContainsKey("cid");
			if (flag)
			{
				Dictionary<uint, Variant> buddyList = (this.g_mgr as muLGClient).g_buddyCT.getBuddyList(3u);
				bool flag2 = buddyList != null && buddyList.ContainsKey(msgData["cid"]._uint);
				if (flag2)
				{
					return;
				}
			}
			this.showChatMsg(msgData, false);
		}

		private void showChatMsg(Variant msgData, bool isself)
		{
			LGIUIMainUI lGIUIMainUI = this.g_mgr.g_uiM.getLGUI("LGUIMainUIImpl") as LGIUIMainUI;
			bool flag = msgData != null && msgData.ContainsKey("tp");
			if (flag)
			{
				bool flag2 = msgData["tp"] == 5;
				if (flag2)
				{
					lGIUIMainUI.onChatMsg(msgData);
				}
				else
				{
					bool flag3 = 1 == msgData["tp"] || 9 == msgData["tp"];
					if (flag3)
					{
						LGAvatarGameInst lGAvatarGameInst;
						if (isself)
						{
							lGAvatarGameInst = (this.g_mgr as muLGClient).g_selfPlayer;
						}
						else
						{
							lGAvatarGameInst = ((this.g_mgr as muLGClient).g_mapCT.get_player_by_cid(msgData["cid"]) as LGAvatarGameInst);
						}
						bool flag4 = lGAvatarGameInst != null;
						if (flag4)
						{
							bool flag5 = 1 == msgData["tp"];
							if (flag5)
							{
								string str = msgData["msg"]._str;
								bool flag6 = str.IndexOf("\\\\") == 0 && str.Length >= 3;
								if (flag6)
								{
									uint num = uint.Parse(str.Substring(2));
									return;
								}
								string substr = "××@@";
								Variant variant = GameTools.split(str, substr, 1u);
							}
						}
						lGIUIMainUI.onChatMsg(msgData);
					}
					else
					{
						Dictionary<uint, Variant> buddyList = (this.g_mgr as muLGClient).g_buddyCT.getBuddyList(1u);
						bool flag7 = true;
						bool flag8 = msgData.ContainsKey("cid") && msgData["cid"] != null && buddyList != null;
						if (flag8)
						{
							bool flag9 = buddyList.ContainsKey(msgData["cid"]);
							if (flag9)
							{
								flag7 = false;
								bool flag10 = buddyList[msgData["cid"]] == null;
								if (flag10)
								{
									flag7 = true;
								}
							}
						}
						bool flag11 = 7 == msgData["tp"] && !isself && ((buddyList == null || !buddyList.ContainsKey(msgData["cid"])) | flag7);
						if (flag11)
						{
							(this.g_mgr as muLGClient).g_MgrPlayerInfoCT.query_ply_info(msgData["cid"], new Action<uint, Variant>(this._playerInfoBack), -1, false);
						}
						lGIUIMainUI.onChatMsg(msgData);
					}
				}
			}
		}

		private bool gagChat(string text)
		{
			bool flag = !this._initConf;
			if (flag)
			{
				this._initConf = true;
				string str = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.GetCommonConf("gag_keywords")._str;
				this._gag_keywords = GameTools.split(str, ",", 1u);
				int @int = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.GetCommonConf("gag_limit")._int;
				bool flag2 = @int > 0;
				if (flag2)
				{
					this._gag_limit = @int;
				}
				int int2 = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.GetCommonConf("gag_min")._int;
				bool flag3 = int2 > 0;
				if (flag3)
				{
					this._gag_min = int2;
				}
				int int3 = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.GetCommonConf("gag_equal")._int;
				bool flag4 = int3 > 0;
				if (flag4)
				{
					this._gag_equal = int3;
				}
				int int4 = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.GetCommonConf("gag_time")._int;
				bool flag5 = int4 > 0;
				if (flag5)
				{
					this._gag_time = int4;
				}
				this._is_gag = bool.Parse(UIUtility.singleton.GetFlag("is_gag"));
				bool is_gag = this._is_gag;
				if (is_gag)
				{
				}
			}
			bool flag6 = text != "";
			bool result;
			if (flag6)
			{
				bool flag7 = this._cur_gag_cnt == 0;
				if (flag7)
				{
					bool flag8 = text.Length >= this._gag_equal;
					if (flag8)
					{
						this._savemsg = text.Substring(0, this._gag_equal);
					}
					else
					{
						this._savemsg = text;
					}
					this._cur_gag_cnt++;
				}
				else
				{
					bool flag9 = text.Length >= this._gag_min;
					if (flag9)
					{
						bool flag10 = text.Length >= this._gag_equal;
						string text2;
						if (flag10)
						{
							text2 = text.Substring(0, this._gag_equal);
						}
						else
						{
							text2 = text;
						}
						bool flag11 = this._savemsg == text2;
						if (flag11)
						{
							this._cur_gag_cnt++;
							bool flag12 = this._cur_gag_cnt >= this._gag_limit;
							if (flag12)
							{
								this._cur_gag_cnt = 0;
								using (List<Variant>.Enumerator enumerator = this._gag_keywords._arr.GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										string value = enumerator.Current;
										bool flag13 = text.IndexOf(value) >= 0;
										if (flag13)
										{
											this._is_gag = true;
											UIUtility.singleton.SaveFlag("is_gag", true);
											UIUtility.singleton.FlushFlag();
											string languageText = LanguagePack.getLanguageText("chat", "msgSendfailed");
											(this.g_mgr.g_uiM.getLGUI("LGUIMainUIImpl") as LGIUIMainUI).systemmsg(languageText, 1024u);
											result = false;
											return result;
										}
									}
								}
							}
						}
						else
						{
							this._savemsg = text2;
							this._cur_gag_cnt = 0;
						}
					}
				}
			}
			result = true;
			return result;
		}

		private void ungapChat()
		{
			bool is_gag = this._is_gag;
			if (is_gag)
			{
				this._is_gag = false;
				UIUtility.singleton.SaveFlag("is_gag", false);
				UIUtility.singleton.FlushFlag();
			}
		}

		public void chat_msg(uint type, string msg, uint cid = 0u, bool withtid = false, uint backshowtp = 0u)
		{
			bool flag = !this.gagChat(msg);
			if (!flag)
			{
				bool flag2 = cid != 0u && type == 5u;
				if (flag2)
				{
					Dictionary<uint, Variant> buddyList = (this.g_mgr as muLGClient).g_buddyCT.getBuddyList(3u);
					bool flag3 = buddyList != null && buddyList.ContainsKey(cid);
					if (flag3)
					{
						LGIUIMainUI lGIUIMainUI = this.g_mgr.g_uiM.getLGUI("LGUIMainUIImpl") as LGIUIMainUI;
						string languageText = LanguagePack.getLanguageText("chat", "blacklist");
						lGIUIMainUI.systemmsg(languageText, 1024u);
						return;
					}
				}
				bool flag4 = type == 1u && (this.g_mgr as muLGClient).g_levelsCT.InMultiLvl();
				if (flag4)
				{
					type = 9u;
				}
				(this.g_mgr.g_netM as muNetCleint).igChatMsgs.chat_msg(type, msg, cid, withtid, backshowtp);
			}
		}

		private void planReportMsg(string msg, uint tcid)
		{
			Variant mainPlayerInfo = (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.mainPlayerInfo;
		}

		protected void _playerInfoBack(uint cid, Variant objInfo)
		{
			bool flag = false;
			foreach (Variant current in this._chatPlayers._arr)
			{
				bool flag2 = current["cid"] == cid;
				if (flag2)
				{
					flag = true;
					GameTools.assignProp(current, objInfo);
					break;
				}
			}
			bool flag3 = !flag;
			if (flag3)
			{
				this._chatPlayers._arr.Insert(0, objInfo);
			}
			bool flag4 = this._chatPlayers.Length >= 10;
			if (flag4)
			{
				this._chatPlayers._arr.RemoveAt(this._chatPlayers.Length - 1);
			}
		}

		public Variant GetChatPlayers()
		{
			return null;
		}

		public void ResetRecomPlayers()
		{
			bool flag = this._chatPlayers.Length > 0;
			if (flag)
			{
				this._chatPlayers = new Variant();
			}
		}
	}
}
