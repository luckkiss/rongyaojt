using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MuGame
{
	internal class NbDoItems
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly NbDoItems.<>c <>9 = new NbDoItems.<>c();

			public static Action <>9__25_0;

			internal void <ContinueDo>b__25_0()
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.DIALOG, null, false);
			}
		}

		public static Animator cacheCameraAni;

		public static void showWithNorlCamera(object[] objs, Action forceDO)
		{
			NbDoItems.showTeachWin(objs, forceDO, 0);
		}

		public static void showWithOutClick(object[] objs, Action forceDo)
		{
			GameObject gameObject = GameObject.Find(objs[1] as string);
			bool flag = gameObject == null;
			if (flag)
			{
				Debug.LogError("新手脚本错误：找不到该元件：" + objs[1]);
				NewbieModel.getInstance().hide();
			}
			else
			{
				RectTransform component = gameObject.transform.GetComponent<RectTransform>();
				NewbieModel.getInstance().show(component.position, component.sizeDelta, objs[2] as string, false, objs[3] as string, null, 0, true);
				NewbieModel.getInstance().curItem.hideMarkClick();
			}
		}

		public static void showWithClick(object[] objs, Action forceDo)
		{
			GameObject x = GameObject.Find(objs[1] as string);
			bool flag = x == null;
			if (flag)
			{
				Debug.LogError("新手脚本错误：找不到该元件：" + objs[1]);
				NewbieModel.getInstance().hide();
			}
			else
			{
				NewbieModel.getInstance().showTittle(objs[1] as string, forceDo);
			}
		}

		public static void showWithOutAvatar(object[] objs, Action forceDo)
		{
			GameObject gameObject = GameObject.Find(objs[1] as string);
			bool flag = gameObject == null;
			if (flag)
			{
				Debug.LogError("新手脚本错误：找不到该元件：" + objs[1]);
				NewbieModel.getInstance().hide();
			}
			else
			{
				RectTransform component = gameObject.transform.GetComponent<RectTransform>();
				NewbieModel.getInstance().showWithoutAvatar(component.position, component.sizeDelta, objs[2] as string, forceDo);
			}
		}

		public static void showWithNext(object[] objs, Action forceDo)
		{
			GameObject gameObject = GameObject.Find(objs[1] as string);
			bool flag = gameObject == null;
			if (flag)
			{
				Debug.LogError("新手脚本错误：找不到该元件：" + objs[1]);
				NewbieModel.getInstance().hide();
			}
			else
			{
				RectTransform component = gameObject.transform.GetComponent<RectTransform>();
				NewbieModel.getInstance().showNext(component.position, component.sizeDelta, objs[2] as string, int.Parse(objs[3].ToString()), forceDo);
			}
		}

		public static void showTeachWin(object[] objs, Action forceDO, int cameraType = 0)
		{
			GameObject gameObject = GameObject.Find(objs[1] as string);
			bool flag = gameObject == null;
			if (flag)
			{
				Debug.LogError("新手脚本错误：找不到该元件：" + objs[1]);
				NewbieModel.getInstance().hide();
			}
			else
			{
				RectTransform component = gameObject.transform.GetComponent<RectTransform>();
				bool flag2 = objs.Length == 3;
				if (flag2)
				{
					NewbieModel.getInstance().show(component.position, component.sizeDelta, objs[2] as string, false, "", null, cameraType, true);
				}
				else
				{
					bool flag3 = objs.Length == 4;
					if (flag3)
					{
						NewbieModel.getInstance().show(component.position, component.sizeDelta, objs[2] as string, cameraType == 1, objs[3] as string, forceDO, cameraType, true);
					}
					else
					{
						bool flag4 = objs.Length == 5;
						if (flag4)
						{
							NewbieModel.getInstance().show(component.position, component.sizeDelta, objs[2] as string, cameraType == 1, objs[3] as string, forceDO, cameraType, int.Parse(objs[4] as string) != 1);
						}
					}
				}
			}
		}

		public static void closeAllwin(object[] objs, Action forceDo)
		{
			bool flag = objs.Length >= 2;
			if (flag)
			{
				InterfaceMgr.getInstance().closeAllWin(objs[1] as string);
			}
			else
			{
				InterfaceMgr.getInstance().closeAllWin("");
			}
			MsgBoxMgr.getInstance().hide();
		}

		public static void hideTeachWin(object[] objs, Action forceDo)
		{
			NewbieModel.getInstance().hide();
		}

		public static void stopMove(object[] objs, Action forceDo)
		{
			bool flag = joystick.instance != null;
			if (flag)
			{
				joystick.instance.OnDragOut(null);
			}
		}

		public static void stopmove1(object[] objs, Action forceDo)
		{
			bool flag = joystick.instance != null;
			if (flag)
			{
				joystick.instance.OnDragOut_wait(0.2f);
			}
		}

		public static void hidefloatUI(object[] objs, Action forceDo)
		{
			InterfaceMgr.getInstance().floatUI.transform.localScale = Vector3.zero;
		}

		public static void showfloatUI(object[] objs, Action forceDo)
		{
			InterfaceMgr.getInstance().floatUI.transform.localScale = Vector3.one;
		}

		public static void openSkill(object[] objs, Action forceDo)
		{
			bool flag = objs.Length < 2;
			if (!flag)
			{
				bool flag2 = skillbar.instance != null;
				if (flag2)
				{
					skillbar.instance.refreshAllSkills(int.Parse(objs[1].ToString()));
				}
			}
		}

		public static void closeexpbar(object[] objs, Action forceDo)
		{
			bool flag = a3_expbar.instance != null;
			if (flag)
			{
				a3_expbar.instance.On_Btn_Up();
			}
			InterfaceMgr.doCommandByLua("a3_litemap_btns.setToggle", "ui/interfaces/floatui/a3_litemap_btns", new object[]
			{
				false
			});
		}

		public static void openexpbar(object[] objs, Action forceDo)
		{
			bool flag = a3_expbar.instance != null;
			if (flag)
			{
				a3_expbar.instance.On_Btn_Down();
			}
			InterfaceMgr.doCommandByLua("a3_litemap_btns.setToggle", "ui/interfaces/floatui/a3_litemap_btns", new object[]
			{
				true
			});
		}

		public static void skillDraw(object[] objs, Action forceD)
		{
			skill_a3.show_teack_ani = true;
			bool flag = skill_a3._instance != null;
			if (flag)
			{
				skill_a3._instance.showTeachAni(true);
			}
		}

		public static void showTeachLine(object[] objs, Action forceDo)
		{
			teachline.show(objs[1].ToString(), float.Parse(objs[2].ToString()));
		}

		public static void showObj(object[] objs, Action forceDo)
		{
			bool flag = objs.Length < 3;
			if (!flag)
			{
				bool flag2 = int.Parse(objs[2].ToString()) == 1;
				if (flag2)
				{
					DoAfterMgr.instacne.addAfterRender(delegate
					{
						GameObject gameObject = TriggerHanldePoint.lGo[int.Parse(objs[1].ToString())];
						debug.Log(":::::::temp2::::::" + gameObject);
						gameObject.SetActive(true);
					});
				}
				else
				{
					GameObject go = GameObject.Find(objs[1].ToString());
					bool flag3 = go == null;
					if (flag3)
					{
						debug.Log("showObj：：未找到" + objs[1].ToString());
					}
					else
					{
						DoAfterMgr.instacne.addAfterRender(delegate
						{
							go.SetActive(int.Parse(objs[2].ToString()) == 1);
						});
					}
				}
			}
		}

		public static void clearLGo(object[] objs, Action forceDo)
		{
			NbDoItems.cacheCameraAni = null;
			bool flag = TriggerHanldePoint.lGo == null;
			if (!flag)
			{
				TriggerHanldePoint.lGo.Clear();
				TriggerHanldePoint.lGo = null;
			}
		}

		public static void endNewbie(object[] objs, Action forceDo)
		{
			ModelBase<PlayerModel>.getInstance().LeaveStandalone_CreateChar();
		}

		public static void playact(object[] objs, Action forceDo)
		{
			bool flag = objs.Length < 3;
			if (!flag)
			{
				bool flag2 = objs[1].ToString() == "#usr";
				GameObject gameObject;
				if (flag2)
				{
					gameObject = SelfRole._inst.m_curModel.gameObject;
				}
				else
				{
					gameObject = GameObject.Find(objs[1].ToString());
				}
				bool flag3 = gameObject == null;
				if (!flag3)
				{
					Animator component = gameObject.GetComponent<Animator>();
					bool flag4 = component == null;
					if (!flag4)
					{
						component.SetTrigger(objs[2].ToString());
					}
				}
			}
		}

		public static void playEff(object[] objs, Action forceDo)
		{
			bool flag = objs.Length < 3;
			if (!flag)
			{
				bool flag2 = objs[1].ToString() == "#usr";
				GameObject gameObject;
				if (flag2)
				{
					gameObject = SelfRole._inst.m_curModel.gameObject;
				}
				else
				{
					gameObject = GameObject.Find(objs[1].ToString());
				}
				bool flag3 = gameObject == null;
				if (!flag3)
				{
					GameObject gameObject2 = Resources.Load(objs[2].ToString()) as GameObject;
					bool flag4 = gameObject2 == null;
					if (!flag4)
					{
						GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(gameObject2);
						gameObject3.transform.SetParent(gameObject.transform, false);
						UnityEngine.Object.Destroy(gameObject3, 3f);
					}
				}
			}
		}

		public static void playCameani(object[] objs, Action forceDo)
		{
			NbDoItems.cacheCameraAni.speed = float.Parse(objs[1].ToString());
			NbDoItems.cacheCameraAni = null;
		}

		public static void endStory(object[] objs, Action forceDo)
		{
			SceneCamera.endStory(float.Parse(objs[1].ToString()));
		}

		public static void ContinueDo(object[] objs, Action forceDo)
		{
			DoAfterMgr arg_25_0 = DoAfterMgr.instacne;
			Action arg_25_1;
			if ((arg_25_1 = NbDoItems.<>c.<>9__25_0) == null)
			{
				arg_25_1 = (NbDoItems.<>c.<>9__25_0 = new Action(NbDoItems.<>c.<>9.<ContinueDo>b__25_0));
			}
			arg_25_0.addAfterRender(arg_25_1);
		}

		public static void showDialog(object[] objs, Action forceDo)
		{
			GameObject gameObject = GameObject.Find(objs[1].ToString());
			bool flag = gameObject == null;
			if (!flag)
			{
				NpcRole component = gameObject.GetComponent<NpcRole>();
				bool flag2 = component == null;
				if (!flag2)
				{
					dialog.showTalk(new List<string>
					{
						objs[2].ToString()
					}, null, component, true);
				}
			}
		}
	}
}
