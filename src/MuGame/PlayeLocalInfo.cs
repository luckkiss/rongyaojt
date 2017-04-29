using System;
using UnityEngine;

namespace MuGame
{
	public class PlayeLocalInfo
	{
		public static readonly string DEBUG_UID = "debugUid";

		public static readonly string DEBUG_TKN = "debugTkn";

		public static readonly string DEBUG_SELECTED = "bebugSelect";

		public static readonly string AUTOFIGHT = "autofight";

		public static readonly string LOGIN_SERVER_SID = "sid";

		public static readonly string SYS_SOUND = "sysSound";

		public static readonly string SYS_MUSIC = "sysMusic";

		public static readonly string VIDEO_QUALITY = "video_Quality";

		public static readonly string SKILL_EFFECT = "skillEffect";

		public static readonly string DYNAM_LIGHT = "dynamicLighting";

		public static readonly string ROLE_SHADOW = "roleShadow";

		public static readonly string SCENE_DETAIL = "sceneDetail";

		public static readonly string REFUSE_TEAM_INVITE = "refuse_team_invite";

		public static readonly string IGNORE_PRIVATE_INFO = "ignore_private_info";

		public static readonly string IGNORE_KNIGHTAGE_INVITE = "ignore_knightage_invite";

		public static readonly string IGNORE_FRIEND_ADD_REMINDER = "ignore_friend_add_reminder";

		public static readonly string IGNORE_OTHER_EFFECT = "ignore_other_effect";

		public static readonly string IGNORE_OTHER_PLAYER = "ignore_other_player";

		public static readonly string IGNORE_OTHER_PET = "ignore_other_pet";

		public static void saveInt(string id, int value)
		{
			PlayerPrefs.SetInt(id, value);
			PlayerPrefs.Save();
		}

		public static void saveString(string id, string value)
		{
			PlayerPrefs.SetString(id, value);
			PlayerPrefs.Save();
		}

		public static int loadInt(string id)
		{
			bool flag = !PlayeLocalInfo.checkKey(id);
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = PlayerPrefs.GetInt(id);
			}
			return result;
		}

		public static string loadString(string id)
		{
			return PlayerPrefs.GetString(id);
		}

		public static bool checkKey(string id)
		{
			return PlayerPrefs.HasKey(id);
		}
	}
}
