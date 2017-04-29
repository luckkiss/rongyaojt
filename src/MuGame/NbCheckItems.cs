using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MuGame
{
	public class NbCheckItems
	{
		public delegate bool boolDelegate(string[] arr);

		public static Dictionary<string, NbCheckItems.boolDelegate> dCheck;

		private List<NbCheckItem> lItems = new List<NbCheckItem>();

		public static bool checkLV(string[] arr)
		{
			int num = int.Parse(arr[1]);
			return (ulong)ModelBase<PlayerModel>.getInstance().lvl == (ulong)((long)num);
		}

		public static bool checkZhuan(string[] arr)
		{
			int num = int.Parse(arr[1]);
			return (ulong)ModelBase<PlayerModel>.getInstance().up_lvl == (ulong)((long)num);
		}

		public static bool checkCurMap(string[] arr)
		{
			string a = arr[1];
			bool flag = GRMap.instance == null || GRMap.instance.m_map == null;
			return !flag && a == GRMap.instance.m_map.id;
		}

		public static bool checkCurNotMap(string[] arr)
		{
			string a = arr[1];
			bool flag = GRMap.instance == null || GRMap.instance.m_map == null;
			return !flag && a != GRMap.instance.m_map.id;
		}

		public static bool checkPlayingPlot(string[] arr)
		{
			bool flag = int.Parse(arr[1]) == 1;
			return flag == GRMap.playingPlot;
		}

		public static bool checkFbInited(string[] arr)
		{
			return false;
		}

		public static bool noCheck(string[] arr)
		{
			return true;
		}

		public static bool checkWinOpen(string[] arr)
		{
			string winid = arr[1];
			return InterfaceMgr.getInstance().checkWinOpened(winid);
		}

		public static bool checkCurTask(string[] arr)
		{
			int num = int.Parse(arr[1]);
			int num2 = int.Parse(arr[2]);
			TaskData taskDataById = ModelBase<A3_TaskModel>.getInstance().GetTaskDataById(ModelBase<A3_TaskModel>.getInstance().main_task_id);
			bool flag = num2 == 0;
			bool result;
			if (flag)
			{
				result = (num == ModelBase<A3_TaskModel>.getInstance().main_task_id);
			}
			else
			{
				result = (num == ModelBase<A3_TaskModel>.getInstance().main_task_id);
			}
			return result;
		}

		public static bool checkHaveItem(string[] arr)
		{
			string text = arr[1];
			return false;
		}

		public static bool checkWinClose(string[] arr)
		{
			string winid = arr[1];
			return !InterfaceMgr.getInstance().checkWinOpened(winid);
		}

		public static bool checkClick(string[] arr)
		{
			bool flag = EventSystem.current.currentSelectedGameObject == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				string a = arr[1];
				result = (a == EventSystem.current.currentSelectedGameObject.name);
			}
			return result;
		}

		public static bool checkSignCount(string[] arr)
		{
			int num = int.Parse(arr[1]);
			return false;
		}

		public static bool checkLastClose(string[] arr)
		{
			string a = arr[1];
			return a == UiEventCenter.lastClosedWinID;
		}

		public static void init()
		{
			NbCheckItems.dCheck = new Dictionary<string, NbCheckItems.boolDelegate>();
			NbCheckItems.dCheck["lv"] = new NbCheckItems.boolDelegate(NbCheckItems.checkLV);
			NbCheckItems.dCheck["zhuan"] = new NbCheckItems.boolDelegate(NbCheckItems.checkZhuan);
			NbCheckItems.dCheck["map"] = new NbCheckItems.boolDelegate(NbCheckItems.checkCurMap);
			NbCheckItems.dCheck["win"] = new NbCheckItems.boolDelegate(NbCheckItems.checkWinOpen);
			NbCheckItems.dCheck["task"] = new NbCheckItems.boolDelegate(NbCheckItems.checkCurTask);
			NbCheckItems.dCheck["click"] = new NbCheckItems.boolDelegate(NbCheckItems.checkClick);
			NbCheckItems.dCheck["unwin"] = new NbCheckItems.boolDelegate(NbCheckItems.checkWinClose);
			NbCheckItems.dCheck["haveitem"] = new NbCheckItems.boolDelegate(NbCheckItems.checkHaveItem);
			NbCheckItems.dCheck["lastclose"] = new NbCheckItems.boolDelegate(NbCheckItems.checkLastClose);
			NbCheckItems.dCheck["plot"] = new NbCheckItems.boolDelegate(NbCheckItems.checkPlayingPlot);
			NbCheckItems.dCheck["fbinited"] = new NbCheckItems.boolDelegate(NbCheckItems.checkFbInited);
			NbCheckItems.dCheck["noCheck"] = new NbCheckItems.boolDelegate(NbCheckItems.noCheck);
		}

		public NbCheckItems(string str)
		{
			bool flag = NbCheckItems.dCheck == null;
			if (flag)
			{
				NbCheckItems.init();
			}
			string[] array = str.Split(new char[]
			{
				'|'
			});
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					','
				});
				bool flag2 = NbCheckItems.dCheck.ContainsKey(array2[0]);
				if (flag2)
				{
					this.lItems.Add(new NbCheckItem(NbCheckItems.dCheck[array2[0]], array2));
				}
				else
				{
					Debug.LogError("新手初始化错误:::" + array2[0]);
				}
			}
		}

		public bool doCheck()
		{
			bool result;
			for (int i = 0; i < this.lItems.Count; i++)
			{
				bool flag = !this.lItems[i].doit();
				if (flag)
				{
					result = false;
					return result;
				}
			}
			result = true;
			return result;
		}
	}
}
