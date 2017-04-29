using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_mapChangeLine : Skin
	{
		private class itemLine : Skin
		{
			private Text txtInfo;

			private string strLine = "{0}  {1}  {2}";

			private Toggle toggleRoot;

			private int _line;

			public itemLine(a3_mapChangeLine.itemLineData ild) : base(ild.trans)
			{
				this.toggleRoot = ild.trans.GetComponent<Toggle>();
				this.toggleRoot.isOn = ild.isCurrentLine;
				this.toggleRoot.onValueChanged.AddListener(new UnityAction<bool>(this.onThisClick));
				this.txtInfo = base.getComponentByPath<Text>("Label");
				string strLineType = this.getStrLineType(ild.ilt);
				string strLineNum = this.getStrLineNum(ild.lineNum);
				string strLineStateType = this.getStrLineStateType(ild.ilst);
				this.txtInfo.text = string.Format(this.strLine, strLineType, strLineNum, strLineStateType);
				this._line = ild.lineNum;
			}

			private void onThisClick(bool b)
			{
				if (b)
				{
					BaseProxy<ChangeLineProxy>.getInstance().mSelectedLine = this._line;
				}
			}

			public void setInfor(a3_mapChangeLine.itemLineData ild)
			{
				this.toggleRoot.isOn = ild.isCurrentLine;
				string strLineType = this.getStrLineType(ild.ilt);
				string strLineNum = this.getStrLineNum(ild.lineNum);
				string strLineStateType = this.getStrLineStateType(ild.ilst);
				this._line = ild.lineNum;
				this.txtInfo.text = string.Format(this.strLine, strLineType, strLineNum, strLineStateType);
			}

			private string getStrLineType(a3_mapChangeLine.ItemLineType ilt)
			{
				string result = string.Empty;
				bool flag = ilt == a3_mapChangeLine.ItemLineType.current;
				if (flag)
				{
					result = "<color=#ff0000ff>[当前]</color>";
				}
				bool flag2 = ilt == a3_mapChangeLine.ItemLineType.recommend;
				if (flag2)
				{
					result = "<color=#0000a0ff>[推荐]</color>";
				}
				return result;
			}

			private string getStrLineNum(int num)
			{
				string[] array = new string[]
				{
					"一线",
					"二线",
					"三线",
					"四线",
					"五线"
				};
				string empty = string.Empty;
				return array[num];
			}

			private string getStrLineStateType(a3_mapChangeLine.ItemLineStateType ilst)
			{
				string result = string.Empty;
				bool flag = ilst == a3_mapChangeLine.ItemLineStateType.full;
				if (flag)
				{
					result = "<color=#ff0000ff>(爆满)</color>";
				}
				bool flag2 = ilst == a3_mapChangeLine.ItemLineStateType.busy;
				if (flag2)
				{
					result = "<color=#ffa500ff>(繁忙)</color>";
				}
				bool flag3 = ilst == a3_mapChangeLine.ItemLineStateType.fluency;
				if (flag3)
				{
					result = "<color=#008000ff>(流畅)</color>";
				}
				return result;
			}
		}

		private struct itemLineData
		{
			public bool isCurrentLine;

			public Transform trans;

			public a3_mapChangeLine.ItemLineType ilt;

			public int lineNum;

			public a3_mapChangeLine.ItemLineStateType ilst;
		}

		private enum ItemLineType
		{
			current,
			recommend
		}

		private enum ItemLineStateType
		{
			full,
			busy,
			fluency
		}

		private List<a3_mapChangeLine.itemLine> itemLineList;

		public a3_mapChangeLine(Transform trans) : base(trans)
		{
			this.itemLineList = new List<a3_mapChangeLine.itemLine>();
			base.gameObject.SetActive(false);
			BaseButton baseButton = new BaseButton(base.getTransformByPath("bottom/btnCancel"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onBtnCancelClick);
			BaseButton baseButton2 = new BaseButton(base.getTransformByPath("bottom/btnOK"), 1, 1);
			baseButton2.onClick = new Action<GameObject>(this.onBtnOKClick);
			BaseButton baseButton3 = new BaseButton(base.getTransformByPath("title/btnClose"), 1, 1);
			baseButton3.onClick = new Action<GameObject>(this.onBtnCancelClick);
			Transform transformByPath = base.getTransformByPath("body/ScrollPanel/content");
			for (int i = 0; i < transformByPath.childCount; i++)
			{
				Transform child = transformByPath.GetChild(i);
				a3_mapChangeLine.itemLine itemLine = new a3_mapChangeLine.itemLine(new a3_mapChangeLine.itemLineData
				{
					trans = child,
					lineNum = i,
					isCurrentLine = i == 0
				});
			}
		}

		public void Show(bool b)
		{
			base.gameObject.SetActive(b);
			if (b)
			{
			}
		}

		private void onBtnCancelClick(GameObject go)
		{
			this.Show(false);
		}

		private void onBtnOKClick(GameObject go)
		{
			BaseProxy<ChangeLineProxy>.getInstance().sendLineProxy((uint)BaseProxy<ChangeLineProxy>.getInstance().mSelectedLine);
			base.gameObject.SetActive(false);
		}
	}
}
