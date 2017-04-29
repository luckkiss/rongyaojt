using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class NewbieModel
	{
		public BgItem curItem;

		public BgItem bgitem;

		public bool first_show = false;

		public static Dictionary<int, int> doneId = new Dictionary<int, int>();

		private bool inited = false;

		public static NewbieModel instanceaaa;

		public NewbieModel()
		{
			Transform transform = GameObject.Find("newbieLayer").transform;
			GameObject original = U3DAPI.U3DResLoad<GameObject>("prefab/newbiebg");
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
			this.bgitem = new BgItem(gameObject.transform, transform);
			gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
		}

		public static bool getDoneId(int id)
		{
			return NewbieModel.doneId.ContainsKey(id);
		}

		public bool getIsOpenHeroNb()
		{
			Transform transform = GameObject.Find("newbieHeroLayer").transform;
			return transform.GetChild(0).gameObject.activeSelf;
		}

		public void initNewbieData()
		{
			bool flag = this.inited;
			if (!flag)
			{
				string text = FileMgr.loadString(FileMgr.TYPE_NEWBIE, "n");
				bool flag2 = text != "";
				if (flag2)
				{
					string[] array = text.Split(new char[]
					{
						','
					});
					for (int i = 0; i < array.Length; i++)
					{
						NewbieModel.doneId[int.Parse(array[i])] = 1;
					}
				}
				this.inited = true;
				List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("newbie.n", "");
				bool flag3 = sXMLList != null;
				if (flag3)
				{
					foreach (SXML current in sXMLList)
					{
						int @int = current.getInt("id");
						bool flag4 = NewbieModel.getDoneId(@int);
						if (!flag4)
						{
							NewbieTeachMgr.getInstance().add(current.getString("p"), @int);
						}
					}
				}
			}
		}

		public void show(Vector3 pos, Vector2 size, string text = "", bool force = false, string clickItemName = "", Action clickMaskHandle = null, int cameraType = 0, bool autoClose = true)
		{
			a3_task_auto.instance.stopAuto = true;
			this.curItem = this.bgitem;
			bool flag = joystick.instance != null && SelfRole._inst != null;
			if (flag)
			{
				joystick.instance.OnDragOut(null);
			}
			this.curItem.show(pos, size, text, force, clickItemName, clickMaskHandle, cameraType, autoClose);
		}

		public void showNext(Vector3 pos, Vector2 size, string text = "", int type = 0, Action clickMaskHandle = null)
		{
			this.curItem.showNext(pos, size, text, type, clickMaskHandle);
		}

		public void showTittle(string clickItemName = "", Action clickMaskHandle = null)
		{
			this.curItem.showTittle(clickItemName, clickMaskHandle);
		}

		public void showWithoutAvatar(Vector3 pos, Vector2 size, string clickItemName = "", Action clickMaskHandle = null)
		{
			this.curItem.showWithoutAvatar(pos, size, clickItemName, clickMaskHandle);
		}

		public void hide()
		{
			a3_task_auto.instance.stopAuto = false;
			bool flag = this.curItem != null;
			if (flag)
			{
				this.curItem.hide();
				this.curItem = null;
			}
		}

		public static NewbieModel getInstance()
		{
			bool flag = NewbieModel.instanceaaa == null;
			if (flag)
			{
				NewbieModel.instanceaaa = new NewbieModel();
			}
			return NewbieModel.instanceaaa;
		}
	}
}
