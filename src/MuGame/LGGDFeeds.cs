using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class LGGDFeeds : lgGDBase
	{
		private Dictionary<string, Action<string>> _callbacks = new Dictionary<string, Action<string>>();

		public LGGDFeeds(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new LGGDFeeds(m as gameManager);
		}

		public override void init()
		{
		}

		public void ShareFeedsWithCallback(string type, Action<string> callback)
		{
			this._callbacks[type] = callback;
			Variant variant = this.parseTypeStr(type);
			this.ShareFeeds(variant["type"], variant["arg"]);
		}

		private Variant parseTypeStr(string type)
		{
			Variant variant = GameTools.split(type, "_", 1u);
			Variant value = (variant.Length > 1) ? variant[1] : null;
			Variant variant2 = new Variant();
			variant2["type"] = variant[0];
			variant2["arg"] = value;
			return variant2;
		}

		public bool ShareFeeds(string type, string arg = null)
		{
			Variant variant = this.getfeeds(type);
			bool flag = variant != null;
			bool result;
			if (flag)
			{
				Variant shareFeedData = this.getShareFeedData(variant, arg);
				bool flag2 = shareFeedData != null;
				if (flag2)
				{
					bool flag3 = shareFeedData["once"];
					if (flag3)
					{
						string shareFlag = this.getShareFlag(type, arg);
						bool flag4 = this._isShared(shareFlag);
						if (flag4)
						{
							result = false;
							return result;
						}
					}
					Variant variant2 = shareFeedData["desc"];
					string type2 = (arg == null) ? type : (type + "_" + arg.ToString());
					this.shareFeeds(type2, variant2._str, variant["res"]);
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		private Variant getShareFeedData(Variant feed, string arg)
		{
			Variant result = null;
			bool flag = arg != null;
			if (flag)
			{
				bool flag2 = feed.ContainsKey("con");
				if (flag2)
				{
					result = feed["con"][arg];
				}
			}
			else
			{
				result = feed;
			}
			return result;
		}

		public bool isShared(string typeStr)
		{
			Variant variant = this.parseTypeStr(typeStr);
			string shareFlag = this.getShareFlag(variant["type"], variant["arg"]);
			return this._isShared(shareFlag);
		}

		private bool _isShared(string f)
		{
			Variant variant = null;
			Variant variant2 = variant["flag"];
			return variant2 != null && bool.Parse(variant2);
		}

		private void saveShareFlag(string f)
		{
		}

		private string getShareFlag(string type, Variant arg)
		{
			string text = "feeds" + type;
			bool flag = arg != null;
			if (flag)
			{
				text += arg.ToString();
			}
			return text + (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.cid;
		}

		private void shareFeeds(string type, string desc, string res)
		{
		}

		private void shareFeedsRes(string type, int res)
		{
			bool flag = res == 0;
			if (flag)
			{
				Action<string> action = this._callbacks[type];
				bool flag2 = action != null;
				if (flag2)
				{
					action(type);
					this._callbacks[type] = null;
				}
				string languageText = LanguagePack.getLanguageText("shareFeeds", "share_success");
				LGIUIMainUI lGIUIMainUI = (this.g_mgr.g_uiM as muUIClient).getLGUI("LGUIMainUIImpl") as LGIUIMainUI;
				lGIUIMainUI.systemmsg(languageText, 1024u);
				Variant variant = this.parseTypeStr(type);
				Variant variant2 = this.getfeeds(variant["type"]);
				bool flag3 = variant2 != null;
				if (flag3)
				{
					Variant shareFeedData = this.getShareFeedData(variant2, variant["arg"]);
					bool flag4 = shareFeedData != null;
					if (flag4)
					{
						bool flag5 = shareFeedData["once"];
						if (flag5)
						{
							this.saveShareFlag(this.getShareFlag(variant["type"], variant["arg"]));
						}
					}
				}
			}
		}

		private Variant getfeeds(string type)
		{
			Variant feedsData = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.GetFeedsData();
			return feedsData[type];
		}
	}
}
