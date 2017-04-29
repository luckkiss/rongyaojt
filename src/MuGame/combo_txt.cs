using Cross;
using DG.Tweening;
using GameFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	public class combo_txt : FightTextLayer
	{
		private Text txt;

		private RectTransform rect;

		private processStruct process;

		private int lastNum = -1;

		private Vector3 vec = new Vector3(2f, 2f, 2f);

		private static int curTick = 0;

		private static int maxTick = 120;

		private static int comboNum = 0;

		private static bool showed = false;

		public override void init()
		{
			base.alain();
			this.txt = base.getComponentByPath<Text>("Text");
			this.rect = base.getComponentByPath<RectTransform>("Text");
			this.process = new processStruct(new Action<float>(this.onUpdate), "fb_main", false, false);
		}

		public override void onShowed()
		{
			combo_txt.showed = true;
			combo_txt.comboNum = 1;
			this.lastNum = -1;
			(CrossApp.singleton.getPlugin("processManager") as processManager).addProcess(this.process, false);
			InterfaceMgr.setUntouchable(base.gameObject);
		}

		public override void onClosed()
		{
			combo_txt.showed = false;
			(CrossApp.singleton.getPlugin("processManager") as processManager).removeProcess(this.process, false);
		}

		public static void clear()
		{
			combo_txt.curTick = -1;
		}

		private void onUpdate(float s)
		{
			combo_txt.curTick--;
			bool flag = combo_txt.curTick < 0;
			if (flag)
			{
				combo_txt.curTick = 0;
				InterfaceMgr.getInstance().close(InterfaceMgr.COMBO_TEXT);
			}
			bool flag2 = this.lastNum < combo_txt.comboNum;
			if (flag2)
			{
				this.lastNum++;
				this.refresh();
			}
		}

		private void refresh()
		{
			this.txt.text = this.lastNum.ToString();
			DOTween.Sequence().Append(this.rect.DOScale(2f, 0.3f)).Append(this.rect.DOScale(1f, 0.3f).SetEase(Ease.OutBack));
		}

		public static void play()
		{
			bool flag = !combo_txt.showed;
			if (flag)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.COMBO_TEXT, null, false);
			}
			combo_txt.curTick = combo_txt.maxTick;
			combo_txt.comboNum++;
		}
	}
}
