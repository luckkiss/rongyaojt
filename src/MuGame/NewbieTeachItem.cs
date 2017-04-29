using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	public class NewbieTeachItem
	{
		public delegate NewbieTeachItem itemDelegate(string[] arr);

		public static Dictionary<string, NewbieTeachItem.itemDelegate> dListener = new Dictionary<string, NewbieTeachItem.itemDelegate>();

		public static Dictionary<string, TeachDoObj> dDo = new Dictionary<string, TeachDoObj>();

		public int id;

		public int idx = -1;

		public NbCheckItems checkToDo;

		public List<TeachDoObj> needToDo = new List<TeachDoObj>();

		public NewbieTeachItem nextItem;

		protected bool addedLinstener = false;

		protected int paramsNum = 0;

		public static void initCommand()
		{
			NewbieTeachItem.regItem("onlv", new NewbieTeachItem.itemDelegate(NbLevel.create));
			NewbieTeachItem.regItem("onclick", new NewbieTeachItem.itemDelegate(NbClick.create));
			NewbieTeachItem.regItem("onwin", new NewbieTeachItem.itemDelegate(NbWinOpen.create));
			NewbieTeachItem.regItem("onwinclose", new NewbieTeachItem.itemDelegate(NbWinClose.create));
			NewbieTeachItem.regItem("stop", new NewbieTeachItem.itemDelegate(NbStop.create));
			NewbieTeachItem.regItem("go", new NewbieTeachItem.itemDelegate(NbGo.create));
			NewbieTeachItem.regItem("onmap", new NewbieTeachItem.itemDelegate(NbChangeWorld.create));
			NewbieTeachItem.regItem("delay", new NewbieTeachItem.itemDelegate(NbDelay.create));
			NewbieTeachItem.regItem("newskill", new NewbieTeachItem.itemDelegate(NbNewSkill.create));
			NewbieTeachItem.regItem("ontask", new NewbieTeachItem.itemDelegate(NbTask.create));
			NewbieTeachItem.regItem("onskillon", new NewbieTeachItem.itemDelegate(NbSkillOn.create));
			NewbieTeachItem.regItem("onJoystick", new NewbieTeachItem.itemDelegate(NbJoystick.create));
			NewbieTeachItem.regItem("onaddpoint", new NewbieTeachItem.itemDelegate(NbAddPoint.create));
			NewbieTeachItem.regItem("oninitFb", new NewbieTeachItem.itemDelegate(NbFBInit.create));
			NewbieTeachItem.regDoItem("st", new Action<object[], Action>(NbDoItems.showWithNorlCamera), 4);
			NewbieTeachItem.regDoItem("std", new Action<object[], Action>(NbDoItems.showWithOutClick), 2);
			NewbieTeachItem.regDoItem("stn", new Action<object[], Action>(NbDoItems.showWithNext), 4);
			NewbieTeachItem.regDoItem("sts", new Action<object[], Action>(NbDoItems.showWithClick), 2);
			NewbieTeachItem.regDoItem("sta", new Action<object[], Action>(NbDoItems.showWithOutAvatar), 3);
			NewbieTeachItem.regDoItem("hideteach", new Action<object[], Action>(NbDoItems.hideTeachWin), 1);
			NewbieTeachItem.regDoItem("closeallwin", new Action<object[], Action>(NbDoItems.closeAllwin), 1);
			NewbieTeachItem.regDoItem("stopmove", new Action<object[], Action>(NbDoItems.stopMove), 1);
			NewbieTeachItem.regDoItem("stopmove1", new Action<object[], Action>(NbDoItems.stopmove1), 1);
			NewbieTeachItem.regDoItem("hidefui", new Action<object[], Action>(NbDoItems.hidefloatUI), 1);
			NewbieTeachItem.regDoItem("showfui", new Action<object[], Action>(NbDoItems.showfloatUI), 1);
			NewbieTeachItem.regDoItem("playact", new Action<object[], Action>(NbDoItems.playact), 3);
			NewbieTeachItem.regDoItem("playeff", new Action<object[], Action>(NbDoItems.playEff), 3);
			NewbieTeachItem.regDoItem("continue", new Action<object[], Action>(NbDoItems.ContinueDo), 1);
			NewbieTeachItem.regDoItem("openskill", new Action<object[], Action>(NbDoItems.openSkill), 2);
			NewbieTeachItem.regDoItem("showobj", new Action<object[], Action>(NbDoItems.showObj), 3);
			NewbieTeachItem.regDoItem("stl", new Action<object[], Action>(NbDoItems.showTeachLine), 3);
			NewbieTeachItem.regDoItem("endnewbie", new Action<object[], Action>(NbDoItems.endNewbie), 1);
			NewbieTeachItem.regDoItem("dialog", new Action<object[], Action>(NbDoItems.showDialog), 3);
			NewbieTeachItem.regDoItem("cleargo", new Action<object[], Action>(NbDoItems.clearLGo), 1);
			NewbieTeachItem.regDoItem("playcameraani", new Action<object[], Action>(NbDoItems.playCameani), 2);
			NewbieTeachItem.regDoItem("endstory", new Action<object[], Action>(NbDoItems.endStory), 2);
			NewbieTeachItem.regDoItem("skilldraw", new Action<object[], Action>(NbDoItems.skillDraw), 1);
			NewbieTeachItem.regDoItem("closeexpbar", new Action<object[], Action>(NbDoItems.closeexpbar), 1);
			NewbieTeachItem.regDoItem("openexpbar", new Action<object[], Action>(NbDoItems.openexpbar), 1);
		}

		private static void regItem(string name, NewbieTeachItem.itemDelegate creatFun)
		{
			NewbieTeachItem.dListener[name] = creatFun;
		}

		private static void regDoItem(string name, Action<object[], Action> fun, int paramNum)
		{
			TeachDoObj value = default(TeachDoObj);
			value._doFun = fun;
			value._pramNum = paramNum;
			NewbieTeachItem.dDo[name] = value;
		}

		public static NewbieTeachItem initWithStr(string str)
		{
			string[] array = str.Split(new char[]
			{
				':'
			});
			bool flag = array.Length != 3 && array.Length != 2;
			NewbieTeachItem result;
			if (flag)
			{
				Debug.LogError("新手脚本初始化错误,冒号：" + str);
				result = null;
			}
			else
			{
				string[] array2 = array[0].Split(new char[]
				{
					','
				});
				string key = array2[0];
				bool flag2 = !NewbieTeachItem.dListener.ContainsKey(key);
				if (flag2)
				{
					Debug.LogError(string.Concat(new string[]
					{
						"新手脚本初始化错误文字错误：",
						array2[0],
						":",
						array[0],
						"::::",
						str
					}));
					result = null;
				}
				else
				{
					NewbieTeachItem newbieTeachItem = NewbieTeachItem.dListener[key](array2);
					bool flag3 = newbieTeachItem == null;
					if (flag3)
					{
						Debug.LogError("初始化新手错误:" + array[0]);
						result = null;
					}
					else
					{
						bool flag4 = array2[0] == "stop";
						if (flag4)
						{
							newbieTeachItem.checkToDo = null;
							string[] dos = array[1].Split(new char[]
							{
								'|'
							});
							newbieTeachItem.initDo(dos);
						}
						else
						{
							newbieTeachItem.initCheck(array[1]);
							string[] dos2 = array[2].Split(new char[]
							{
								'|'
							});
							newbieTeachItem.initDo(dos2);
						}
						result = newbieTeachItem;
					}
				}
			}
			return result;
		}

		public void initDo(object[] dos)
		{
			for (int i = 0; i < dos.Length; i++)
			{
				debug.Log(":" + (dos[i] as string));
				string[] array = (dos[i] as string).Split(new char[]
				{
					','
				});
				string text = array[0];
				TeachDoObj teachDoObj = NewbieTeachItem.dDo[text];
				bool flag = array.Length < teachDoObj._pramNum;
				if (flag)
				{
					Debug.LogError("新手指引参数不足:" + (dos[i] as string));
					break;
				}
				teachDoObj._param = array;
				bool flag2 = text == "st" || text == "sth" || text == "stn" || text == "sts" || text == "sta";
				if (flag2)
				{
					teachDoObj.forcedo = new Action(this.forceDoNext);
				}
				this.needToDo.Add(teachDoObj);
			}
		}

		public void initCheck(string str)
		{
			bool flag = str == "";
			if (flag)
			{
				this.checkToDo = null;
			}
			else
			{
				this.checkToDo = new NbCheckItems(str);
			}
		}

		public void forceDoNext()
		{
			bool flag = this.nextItem != null;
			if (flag)
			{
				this.nextItem.doit(true, false);
			}
		}

		public void save()
		{
			bool doneId = NewbieModel.getDoneId(this.id);
			if (!doneId)
			{
				string text = FileMgr.loadString(FileMgr.TYPE_NEWBIE, "n");
				bool flag = text == "";
				if (flag)
				{
					text = this.id.ToString();
				}
				else
				{
					text = text + "," + this.id;
				}
				FileMgr.saveString(FileMgr.TYPE_NEWBIE, "n", text);
			}
		}

		public bool doit(bool byforce = false, bool fromHandle = false)
		{
			bool flag = this.check(fromHandle) || (byforce && this is NbStop) || this is NbGo;
			bool result;
			if (flag)
			{
				bool flag2 = this.idx == 0;
				if (flag2)
				{
					NewbieModel.getInstance().first_show = true;
					bool flag3 = this.id == 1;
					if (flag3)
					{
						InterfaceMgr.getInstance().closeAllWin(InterfaceMgr.DIALOG);
					}
					bool flag4 = this.id == 11;
					if (flag4)
					{
						ModelBase<LotteryModel>.getInstance().isNewBie = true;
					}
					bool flag5 = this.id == 14;
					if (flag5)
					{
						InterfaceMgr.getInstance().closeAllWin("");
					}
					InterfaceMgr.getInstance().closeFui_NB();
					this.save();
				}
				else
				{
					NewbieModel.getInstance().first_show = false;
				}
				this.removeListener();
				this.addedLinstener = false;
				this.doTeach();
				bool flag6 = this.nextItem != null;
				if (flag6)
				{
					this.nextItem.doit(false, false);
				}
				result = true;
			}
			else
			{
				bool flag7 = !this.addedLinstener;
				if (flag7)
				{
					this.addedLinstener = true;
					this.addListener();
				}
				result = false;
			}
			return result;
		}

		protected void onHanlde(GameEvent e)
		{
			bool flag = this.doit(false, true);
			if (flag)
			{
				bool flag2 = this.addedLinstener;
				if (flag2)
				{
					this.removeListener();
				}
				this.addedLinstener = false;
			}
		}

		public void doTeach()
		{
			for (int i = 0; i < this.needToDo.Count; i++)
			{
				bool flag = this.needToDo[i]._doFun != null;
				if (flag)
				{
					this.needToDo[i]._doFun(this.needToDo[i]._param, this.needToDo[i].forcedo);
				}
			}
			this.needToDo.Clear();
		}

		public bool check(bool fromHandle)
		{
			bool flag = this is NbStop;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.checkToDo == null;
				if (flag2)
				{
					result = fromHandle;
				}
				else
				{
					bool flag3 = !this.addedLinstener && this.idx != 0;
					if (flag3)
					{
						result = false;
					}
					else
					{
						bool flag4 = !this.addedLinstener && this.idx == 0 && this is NbWinClose;
						result = (!flag4 && this.checkToDo.doCheck());
					}
				}
			}
			return result;
		}

		public virtual void addListener()
		{
		}

		public virtual void removeListener()
		{
		}
	}
}
