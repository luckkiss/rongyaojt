using System;

namespace MuGame
{
	public class GlobleSetting
	{
		public static bool SOUND_ON = true;

		public static bool MUSIC_ON = true;

		public static bool REFUSE_TEAM_INVITE = false;

		public static bool IGNORE_PRIVATE_INFO = false;

		public static bool IGNORE_KNIGHTAGE_INVITE = false;

		public static bool IGNORE_FRIEND_ADD_REMINDER = false;

		public static bool IGNORE_OTHER_EFFECT = false;

		public static bool IGNORE_OTHER_PLAYER = false;

		public static bool IGNORE_OTHER_PET = false;

		private static int minSoundValue = 0;

		private static int minMusicValue = 0;

		public static void initSystemSetting()
		{
			bool flag = PlayeLocalInfo.checkKey(PlayeLocalInfo.SYS_SOUND);
			if (flag)
			{
				GlobleSetting.SOUND_ON = (PlayeLocalInfo.loadInt(PlayeLocalInfo.SYS_SOUND) > GlobleSetting.minSoundValue);
			}
			else
			{
				GlobleSetting.SOUND_ON = true;
			}
			MediaClient.getInstance().isPlaySound = GlobleSetting.SOUND_ON;
			bool flag2 = PlayeLocalInfo.checkKey(PlayeLocalInfo.SYS_MUSIC);
			if (flag2)
			{
				GlobleSetting.MUSIC_ON = (PlayeLocalInfo.loadInt(PlayeLocalInfo.SYS_MUSIC) > GlobleSetting.minMusicValue);
			}
			else
			{
				GlobleSetting.MUSIC_ON = true;
			}
			MediaClient.getInstance().isPlayMusic = GlobleSetting.MUSIC_ON;
			GlobleSetting.initSetting();
		}

		public static void setSound(bool on)
		{
			GlobleSetting.SOUND_ON = on;
			int num = (int)(float.Parse(MediaClient.getInstance().getSoundVolume().ToString("F2")) * 100f);
			PlayeLocalInfo.saveInt(PlayeLocalInfo.SYS_SOUND, on ? num : GlobleSetting.minSoundValue);
			MediaClient.getInstance().isPlaySound = on;
		}

		public static void setMusic(bool on)
		{
			GlobleSetting.MUSIC_ON = on;
			int num = (int)(float.Parse(MediaClient.getInstance().getMusicVolume().ToString("F2")) * 100f);
			PlayeLocalInfo.saveInt(PlayeLocalInfo.SYS_MUSIC, on ? num : GlobleSetting.minMusicValue);
			MediaClient.getInstance().isPlayMusic = on;
		}

		public static void initSetting()
		{
			GlobleSetting.initSystem();
			GlobleSetting.initGameSetting();
		}

		private static void initGameSetting()
		{
			bool flag = PlayeLocalInfo.checkKey(PlayeLocalInfo.REFUSE_TEAM_INVITE);
			if (flag)
			{
				GlobleSetting.REFUSE_TEAM_INVITE = (PlayeLocalInfo.loadInt(PlayeLocalInfo.REFUSE_TEAM_INVITE) == 1);
			}
			bool flag2 = PlayeLocalInfo.checkKey(PlayeLocalInfo.IGNORE_PRIVATE_INFO);
			if (flag2)
			{
				GlobleSetting.IGNORE_PRIVATE_INFO = (PlayeLocalInfo.loadInt(PlayeLocalInfo.IGNORE_PRIVATE_INFO) == 1);
			}
			bool flag3 = PlayeLocalInfo.checkKey(PlayeLocalInfo.IGNORE_KNIGHTAGE_INVITE);
			if (flag3)
			{
				GlobleSetting.IGNORE_KNIGHTAGE_INVITE = (PlayeLocalInfo.loadInt(PlayeLocalInfo.IGNORE_KNIGHTAGE_INVITE) == 1);
			}
			bool flag4 = PlayeLocalInfo.checkKey(PlayeLocalInfo.IGNORE_FRIEND_ADD_REMINDER);
			if (flag4)
			{
				GlobleSetting.IGNORE_FRIEND_ADD_REMINDER = (PlayeLocalInfo.loadInt(PlayeLocalInfo.IGNORE_FRIEND_ADD_REMINDER) == 1);
			}
			bool flag5 = PlayeLocalInfo.checkKey(PlayeLocalInfo.IGNORE_OTHER_EFFECT);
			if (flag5)
			{
				GlobleSetting.IGNORE_OTHER_EFFECT = (PlayeLocalInfo.loadInt(PlayeLocalInfo.IGNORE_OTHER_EFFECT) == 1);
			}
			bool flag6 = PlayeLocalInfo.checkKey(PlayeLocalInfo.IGNORE_OTHER_PLAYER);
			if (flag6)
			{
				GlobleSetting.IGNORE_OTHER_PLAYER = (PlayeLocalInfo.loadInt(PlayeLocalInfo.IGNORE_OTHER_PLAYER) == 1);
			}
			bool flag7 = PlayeLocalInfo.checkKey(PlayeLocalInfo.IGNORE_OTHER_PET);
			if (flag7)
			{
				GlobleSetting.IGNORE_OTHER_PET = (PlayeLocalInfo.loadInt(PlayeLocalInfo.IGNORE_OTHER_PET) == 1);
			}
		}

		private static void initSystem()
		{
			bool flag = PlayeLocalInfo.checkKey(PlayeLocalInfo.VIDEO_QUALITY);
			if (flag)
			{
				SceneCamera.m_fScreenGQ_Level = float.Parse(PlayeLocalInfo.loadString(PlayeLocalInfo.VIDEO_QUALITY));
			}
			bool flag2 = PlayeLocalInfo.checkKey(PlayeLocalInfo.DYNAM_LIGHT);
			if (flag2)
			{
				bool flag3 = PlayeLocalInfo.loadInt(PlayeLocalInfo.DYNAM_LIGHT) != 0;
				if (flag3)
				{
					SceneCamera.m_nLightGQ_Level = PlayeLocalInfo.loadInt(PlayeLocalInfo.DYNAM_LIGHT);
				}
			}
			bool flag4 = PlayeLocalInfo.checkKey(PlayeLocalInfo.ROLE_SHADOW);
			if (flag4)
			{
				bool flag5 = PlayeLocalInfo.loadInt(PlayeLocalInfo.ROLE_SHADOW) != 0;
				if (flag5)
				{
					SceneCamera.m_nShadowGQ_Level = PlayeLocalInfo.loadInt(PlayeLocalInfo.ROLE_SHADOW);
				}
			}
			bool flag6 = PlayeLocalInfo.checkKey(PlayeLocalInfo.SCENE_DETAIL);
			if (flag6)
			{
				bool flag7 = PlayeLocalInfo.loadInt(PlayeLocalInfo.SCENE_DETAIL) != 0;
				if (flag7)
				{
					SceneCamera.m_nSceneGQ_Level = PlayeLocalInfo.loadInt(PlayeLocalInfo.SCENE_DETAIL);
				}
			}
			bool flag8 = PlayeLocalInfo.checkKey(PlayeLocalInfo.SYS_SOUND);
			if (flag8)
			{
				int num = PlayeLocalInfo.loadInt(PlayeLocalInfo.SYS_SOUND);
				float soundVolume = (float)num / 100f;
				MediaClient.getInstance().setSoundVolume(soundVolume);
			}
			else
			{
				MediaClient.getInstance().setSoundVolume(0.8f);
			}
			bool flag9 = PlayeLocalInfo.checkKey(PlayeLocalInfo.SYS_MUSIC);
			if (flag9)
			{
				int num2 = PlayeLocalInfo.loadInt(PlayeLocalInfo.SYS_MUSIC);
				float musicVolume = (float)num2 / 100f;
				MediaClient.getInstance().setMusicVolume(musicVolume);
			}
			else
			{
				MediaClient.getInstance().setMusicVolume(0.8f);
			}
			bool flag10 = PlayeLocalInfo.checkKey(PlayeLocalInfo.SKILL_EFFECT);
			if (flag10)
			{
				int nSkillEff_Level = PlayeLocalInfo.loadInt(PlayeLocalInfo.SKILL_EFFECT);
				SceneCamera.m_nSkillEff_Level = nSkillEff_Level;
			}
		}
	}
}
