using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_chapter_hint : Window
	{
		private float animatime = 6f;

		private static int chapid = -1;

		private GameObject gg;

		private Animator ant;

		private Image title;

		private Image nameI;

		private Transform tfReward;

		private GameObject tfPrefabIconBg;

		public static a3_chapter_hint instance;

		private float ii = 0f;

		public static void ShowChapterHint(int id)
		{
			a3_chapter_hint.chapid = id;
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_CHAPTERHINT, null, false);
		}

		public override void init()
		{
			this.title = base.getComponentByPath<Image>("show/title/chap");
			this.nameI = base.getComponentByPath<Image>("show/desc/name");
			this.tfReward = base.transform.FindChild("show/reward");
			this.tfPrefabIconBg = base.transform.FindChild("show/template/rewardIconBg").gameObject;
			this.gg = base.getGameObjectByPath("show");
			this.ant = this.gg.GetComponent<Animator>();
			this.gg.SetActive(false);
		}

		public override void onShowed()
		{
			a3_chapter_hint.instance = this;
			Transform transformByPath = base.getTransformByPath("ig_bg_bg");
			bool flag = transformByPath != null;
			if (flag)
			{
				transformByPath.gameObject.SetActive(false);
			}
			base.CancelInvoke("CloseM");
			this.SetShow();
			base.InvokeRepeating("CloseM", 1f, 0.1f);
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_SHOW_ONLYWIN);
		}

		public override void onClosed()
		{
			a3_chapter_hint.instance = null;
			this.gg.SetActive(false);
			bool flag = InterfaceMgr.getInstance().checkWinOpened(InterfaceMgr.NPC_TASK_TALK);
			if (flag)
			{
				InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_STORY);
				bool flag2 = npctalk.instance != null;
				if (flag2)
				{
					npctalk.instance.MinOrMax(true);
				}
			}
			else
			{
				InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_NORMAL);
			}
			base.CancelInvoke("CloseM");
		}

		private void OnDisable()
		{
			a3_chapter_hint.instance = null;
		}

		private void SetShow()
		{
			bool flag = a3_chapter_hint.chapid < 0;
			if (!flag)
			{
				this.gg.SetActive(true);
				ChapterInfos chapterInfosById = ModelBase<A3_TaskModel>.getInstance().GetChapterInfosById(a3_chapter_hint.chapid);
				this.title.sprite = Resources.Load<Sprite>("icon/chapter/no" + a3_chapter_hint.chapid);
				this.title.SetNativeSize();
				this.nameI.sprite = Resources.Load<Sprite>("icon/chapter/" + a3_chapter_hint.chapid);
				this.nameI.SetNativeSize();
				List<SXML> list = new List<SXML>();
				list.Add(XMLMgr.instance.GetSXML("task.Cha_gift", "id==" + a3_chapter_hint.chapid).GetNode("RewardEqp", "carr==" + ModelBase<PlayerModel>.getInstance().profession));
				list.AddRange(XMLMgr.instance.GetSXML("task.Cha_gift", "").GetNodeList("RewardItem", ""));
				for (int i = this.tfReward.childCount - 1; i > -1; i--)
				{
					UnityEngine.Object.Destroy(this.tfReward.GetChild(i).gameObject);
				}
				for (int j = 0; j < list.Count; j++)
				{
					uint @uint = list[j].getUint("item_id");
					bool flag2 = @uint == 0u;
					if (flag2)
					{
						@uint = list[j].getUint("id");
					}
					int @int = list[j].getInt("value");
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.tfPrefabIconBg);
					Transform transform = gameObject.transform.FindChild("bg");
					GameObject gameObject2 = IconImageMgr.getInstance().createA3ItemIcon(@uint, false, @int, 1f, false, -1, 0, false, false, true, false);
					gameObject2.transform.SetParent(transform, false);
					gameObject.transform.SetParent(this.tfReward, false);
					transform.GetComponent<RectTransform>().localPosition = Vector2.zero;
				}
				this.ii = 0f;
			}
		}

		private void CloseM()
		{
			this.ii += 0.1f;
			bool flag = this.ant.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f || this.ii > this.animatime;
			if (flag)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_CHAPTERHINT);
			}
		}
	}
}
