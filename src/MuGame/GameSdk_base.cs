using Cross;
using GameFramework;
using System;
using System.Runtime.CompilerServices;

namespace MuGame
{
	public class GameSdk_base
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly GameSdk_base.<>c <>9 = new GameSdk_base.<>c();

			public static Action<string, string, int> <>9__18_0;

			public static Action<string> <>9__18_1;

			internal void ctor>b__18_0(string str, string path, int sec)
			{
				debug.Log(".....endVoiceRecord sendHanlde:" + str);
			}

			internal void ctor>b__18_1(string state)
			{
				debug.Log(".....endVoiceRecord loadHanlde:" + state);
			}
		}

		public string m_voice_url = "http://10.1.8.66/upload";

		private bool recordingVoice = false;

		public Action<string, string, int> voiceRecordHanlde;

		public Action<string> voicePlayedHanlde;

		public virtual void Pay(rechargeData data)
		{
		}

		public virtual void record_createRole(Variant data)
		{
		}

		public virtual void record_login()
		{
		}

		public virtual void record_LvlUp()
		{
		}

		public virtual void record_quit()
		{
		}

		public virtual void sharemsg(string sharemsg, string sharetype, string shareappid, string shareappkey)
		{
			Variant variant = new Variant();
			variant["sharemsg"] = sharemsg;
			variant["sharetype"] = sharetype;
			variant["shareappid"] = shareappid;
			variant["shareappkey"] = shareappkey;
			string jstr = JsonManager.VariantToString(variant);
			AnyPlotformSDK.Call_Cmd("gameShare", "lanShare", jstr, false);
		}

		public virtual void record_selectionSever()
		{
			bool flag = Globle.Lan != "zh_cn";
			if (!flag)
			{
				Variant variant = new Variant();
				variant["serverId"] = Globle.curServerD.sid;
				string text = JsonManager.VariantToString(variant);
				AnyPlotformSDK.Call_Cmd("selectServer", "lanServer", text, false);
				debug.Log("[record]selectionSever:" + text);
			}
		}

		public virtual bool beginVoiceRecord()
		{
			bool flag = this.recordingVoice;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				AnyPlotformSDK.Call_Cmd("startRecord", null, null, false);
				this.recordingVoice = true;
				result = true;
			}
			return result;
		}

		public virtual void showbalance()
		{
			AnyPlotformSDK.Call_Cmd("showbalance", null, null, false);
		}

		public virtual void cancelVoiceRecord()
		{
			bool flag = !this.recordingVoice;
			if (!flag)
			{
				this.recordingVoice = false;
				connInfo connInfo = NetClient.instance.getObject("DATA_CONN") as connInfo;
				Variant variant = new Variant();
				variant["sid"] = Globle.curServerD.sid;
				variant["platid"] = Globle.YR_srvlists__platuid;
				variant["uid"] = ModelBase<PlayerModel>.getInstance().uid;
				variant["token"] = ((connInfo.token == "") ? "76b03211848f7db9b922a39fbe1d1978_2015-09-26 15:11:20-100000503" : connInfo.token);
				variant["url"] = "";
				string jstr = JsonManager.VariantToString(variant);
				AnyPlotformSDK.Call_Cmd("finishRecord", "lanVoice", jstr, false);
			}
		}

		public virtual void endVoiceRecord()
		{
			bool flag = !this.recordingVoice;
			if (!flag)
			{
				this.recordingVoice = false;
				connInfo connInfo = NetClient.instance.getObject("DATA_CONN") as connInfo;
				Variant variant = new Variant();
				variant["sid"] = Globle.curServerD.sid;
				variant["platid"] = Globle.YR_srvlists__platuid;
				variant["uid"] = ModelBase<PlayerModel>.getInstance().uid;
				variant["token"] = ((connInfo.token == "") ? "76b03211848f7db9b922a39fbe1d1978_2015-09-26 15:11:20-100000503" : connInfo.token);
				variant["url"] = this.m_voice_url;
				string jstr = JsonManager.VariantToString(variant);
				AnyPlotformSDK.Call_Cmd("finishRecord", "lanVoice", jstr, false);
			}
		}

		public virtual void playVoice(string path)
		{
			connInfo connInfo = NetClient.instance.getObject("DATA_CONN") as connInfo;
			Variant variant = new Variant();
			variant["sid"] = Globle.curServerD.sid;
			variant["platid"] = Globle.YR_srvlists__platuid;
			variant["uid"] = ModelBase<PlayerModel>.getInstance().uid;
			variant["token"] = ((connInfo.token == "") ? "76b03211848f7db9b922a39fbe1d1978_2015-09-26 15:11:20-100000503" : connInfo.token);
			variant["url"] = path;
			string jstr = JsonManager.VariantToString(variant);
			AnyPlotformSDK.Call_Cmd("playVoice", "lanVoice", jstr, false);
		}

		public virtual void stopVoice(string path)
		{
			connInfo connInfo = NetClient.instance.getObject("DATA_CONN") as connInfo;
			Variant variant = new Variant();
			variant["sid"] = Globle.curServerD.sid;
			variant["platid"] = Globle.YR_srvlists__platuid;
			variant["uid"] = ModelBase<PlayerModel>.getInstance().uid;
			variant["token"] = ((connInfo.token == "") ? "76b03211848f7db9b922a39fbe1d1978_2015-09-26 15:11:20-100000503" : connInfo.token);
			variant["url"] = path;
			string jstr = JsonManager.VariantToString(variant);
			AnyPlotformSDK.Call_Cmd("stopVoice", "lanVoice", jstr, false);
		}

		public virtual void clearVoices()
		{
			AnyPlotformSDK.Call_Cmd("deleteAllVoice", null, null, false);
		}

		public GameSdk_base()
		{
			Action<string, string, int> arg_32_1;
			if ((arg_32_1 = GameSdk_base.<>c.<>9__18_0) == null)
			{
				arg_32_1 = (GameSdk_base.<>c.<>9__18_0 = new Action<string, string, int>(GameSdk_base.<>c.<>9.<.ctor>b__18_0));
			}
			this.voiceRecordHanlde = arg_32_1;
			Action<string> arg_57_1;
			if ((arg_57_1 = GameSdk_base.<>c.<>9__18_1) == null)
			{
				arg_57_1 = (GameSdk_base.<>c.<>9__18_1 = new Action<string>(GameSdk_base.<>c.<>9.<.ctor>b__18_1));
			}
			this.voicePlayedHanlde = arg_57_1;
			base..ctor();
		}
	}
}
