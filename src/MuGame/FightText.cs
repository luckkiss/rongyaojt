using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	public class FightText
	{
		public static readonly string WARRIOR_TEXT = "ani/fightingText/warriortext";

		public static readonly string CRIT_TEXT = "ani/fightingText/crittext";

		public static readonly string MAGE_TEXT = "ani/fightingText/magetext";

		public static readonly string ASSI_TEXT = "ani/fightingText/assitext";

		public static readonly string ENEMY_TEXT = "ani/fightingText/enemy";

		public static readonly string HEAL_TEXT = "ani/fightingText/healtext";

		public static readonly string MONEY_TEXT = "ani/fightingText/money1";

		public static readonly string MISS_TEXT = "ani/fightingText/miss";

		public static readonly string SHEILD_TEXT = "ani/fightingText/warriortext";

		public static readonly string IMG_TEXT = "ani/fightingText/Imgtext";

		public static readonly string BUFF_TEXT = "ani/fightingText/Bufftext";

		public static readonly string ADD_IMG_TEXT = "ani/fightingText/Add_Imgtext";

		public static string userText = "ani/fightingText/enemy";

		public static readonly string MOUSE_POINT = "ani/mousetouch/mouse_pointa";

		private static Dictionary<string, List<FightTextTempSC>> normalFightTextPool = new Dictionary<string, List<FightTextTempSC>>();

		private static List<FightTextTempSC> playingPool = new List<FightTextTempSC>();

		private static List<Vector3> offsetPos;

		private static int posIdx = 0;

		private static TickItem process;

		private static Transform instacne;

		private static Transform mousePointCon;

		private static float tick;

		public static BaseRole CurrentCauseRole
		{
			get;
			set;
		}

		public static void play(string id, Vector3 pos, int num, bool criatk = false, int type = -1)
		{
			bool flag = FightText.instacne == null;
			if (flag)
			{
				FightText.instacne = GameObject.Find("fightText").transform;
				FightText.process = new TickItem(new Action<float>(FightText.onUpdate));
				TickMgr.instance.addTick(FightText.process);
				FightText.mousePointCon = GameObject.Find("mouseTouchAni").transform;
				InterfaceMgr.setUntouchable(FightText.mousePointCon.gameObject);
			}
			bool flag2 = !FightText.normalFightTextPool.ContainsKey(id + type);
			if (flag2)
			{
				FightText.normalFightTextPool[id + type] = new List<FightTextTempSC>();
			}
			List<FightTextTempSC> list = FightText.normalFightTextPool[id + type];
			bool flag3 = list.Count == 0;
			FightTextTempSC fightTextTempSC;
			if (flag3)
			{
				GameObject original = Resources.Load(id) as GameObject;
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
				bool flag4 = type != -1;
				if (flag4)
				{
					Sprite sprite = Resources.Load("icon/rune_fight/" + type, typeof(Sprite)) as Sprite;
					bool flag5 = sprite != null;
					if (!flag5)
					{
						UnityEngine.Object.Destroy(gameObject);
						return;
					}
					gameObject.transform.FindChild("Text/Image").GetComponent<Image>().sprite = sprite;
				}
				fightTextTempSC = gameObject.GetComponent<FightTextTempSC>();
				bool flag6 = fightTextTempSC == null;
				if (flag6)
				{
					fightTextTempSC = gameObject.AddComponent<FightTextTempSC>();
				}
				bool flag7 = id == FightText.MOUSE_POINT || id == FightText.MISS_TEXT;
				if (flag7)
				{
					gameObject.transform.SetParent(FightText.instacne, false);
					fightTextTempSC.init(FightTextTempSC.TYPE_ANI);
				}
				else
				{
					gameObject.transform.SetParent(FightText.instacne, false);
					fightTextTempSC.init(FightTextTempSC.TYPE_TEXT);
				}
				fightTextTempSC.pool = list;
				fightTextTempSC.playingPool = FightText.playingPool;
			}
			else
			{
				fightTextTempSC = list[0];
				fightTextTempSC.setActive(true);
				list.RemoveAt(0);
			}
			fightTextTempSC.timer = Time.time;
			FightText.playingPool.Add(fightTextTempSC);
			Vector2 one = Vector2.one;
			bool flag8 = FightText.CurrentCauseRole != null;
			if (flag8)
			{
				Vector3 vector;
				try
				{
					vector = GRMap.GAME_CAM_CAMERA.WorldToScreenPoint(FightText.CurrentCauseRole.m_curModel.position);
				}
				catch (Exception)
				{
					return;
				}
				one = new Vector2((float)((vector.x - pos.x > 0f) ? 1 : -1), (float)((vector.y - pos.y > 0f) ? 1 : -1));
			}
			fightTextTempSC.ani.SetFloat("HorizontalValue", one.x);
			fightTextTempSC.ani.SetFloat("VerticalValue", one.y);
			fightTextTempSC.play(pos, num, criatk);
			FightText.posIdx++;
			bool flag9 = FightText.posIdx >= 20;
			if (flag9)
			{
				FightText.posIdx = 0;
			}
		}

		public static void play1(string id, Vector3 pos, int num, bool criatk = false, int type = -1)
		{
			bool flag = FightText.offsetPos == null;
			if (flag)
			{
				FightText.offsetPos = new List<Vector3>();
				for (int i = 0; i < 20; i++)
				{
					Vector3 item = new Vector3((float)ConfigUtil.getRandom(-30, 30), (float)ConfigUtil.getRandom(-30, 10), 0f);
					FightText.offsetPos.Add(item);
				}
			}
			bool flag2 = FightText.instacne == null;
			if (flag2)
			{
				FightText.instacne = GameObject.Find("fightText").transform;
				FightText.process = new TickItem(new Action<float>(FightText.onUpdate));
				TickMgr.instance.addTick(FightText.process);
				FightText.mousePointCon = GameObject.Find("mouseTouchAni").transform;
				InterfaceMgr.setUntouchable(FightText.mousePointCon.gameObject);
			}
			GameObject original = Resources.Load(id) as GameObject;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
			bool flag3 = type != -1;
			if (flag3)
			{
				Sprite sprite = Resources.Load("icon/rune_fight/" + type, typeof(Sprite)) as Sprite;
				bool flag4 = sprite != null;
				if (flag4)
				{
					gameObject.transform.FindChild("Text/Image").GetComponent<Image>().sprite = sprite;
				}
			}
			FightTextTempSC fightTextTempSC = gameObject.GetComponent<FightTextTempSC>();
			bool flag5 = fightTextTempSC == null;
			if (flag5)
			{
				fightTextTempSC = gameObject.AddComponent<FightTextTempSC>();
			}
			bool flag6 = id == FightText.MOUSE_POINT || id == FightText.MISS_TEXT;
			if (flag6)
			{
				gameObject.transform.SetParent(FightText.instacne, false);
				fightTextTempSC.init(FightTextTempSC.TYPE_ANI);
			}
			else
			{
				gameObject.transform.SetParent(FightText.instacne, false);
				fightTextTempSC.init(FightTextTempSC.TYPE_TEXT);
			}
			fightTextTempSC.play(pos + ((id == FightText.MOUSE_POINT) ? Vector3.zero : FightText.offsetPos[FightText.posIdx]), num, criatk);
			FightText.posIdx++;
			bool flag7 = FightText.posIdx >= 20;
			if (flag7)
			{
				FightText.posIdx = 0;
			}
		}

		public static void clear()
		{
			bool flag = FightText.instacne == null;
			if (!flag)
			{
				for (int i = 0; i < FightText.playingPool.Count; i++)
				{
					FightTextTempSC fightTextTempSC = FightText.playingPool[i];
					fightTextTempSC.onAniOver();
					i--;
				}
			}
		}

		private static void onUpdate(float s)
		{
			float time = Time.time;
			for (int i = 0; i < FightText.playingPool.Count; i++)
			{
				FightTextTempSC fightTextTempSC = FightText.playingPool[i];
				bool flag = time - fightTextTempSC.timer > 2f;
				if (flag)
				{
					fightTextTempSC.onAniOver();
					i--;
				}
			}
		}
	}
}
