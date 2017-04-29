using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Lui
{
	public class LRichText : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		public RichAlignType alignType;

		public RichPivotType pivotType;

		public int verticalOffset;

		public int maxLineWidth;

		public Font font;

		public UnityAction<string> onClickHandler;

		private List<LRichElement> _richElements;

		private List<LRenderElement> _elemRenderArr;

		private List<LRichCacheElement> _cacheLabElements;

		private List<LRichCacheElement> _cacheImgElements;

		private List<LRichCacheElement> _cacheFramAnimElements;

		private Dictionary<GameObject, string> _objectDataMap;

		public int realLineHeight
		{
			get;
			protected set;
		}

		public int realLineWidth
		{
			get;
			protected set;
		}

		public void removeAllElements()
		{
			foreach (LRichCacheElement current in this._cacheLabElements)
			{
				current.isUse = false;
				current.node.transform.SetParent(null);
			}
			foreach (LRichCacheElement current2 in this._cacheImgElements)
			{
				current2.isUse = false;
				current2.node.transform.SetParent(null);
			}
			foreach (LRichCacheElement current3 in this._cacheFramAnimElements)
			{
				current3.isUse = false;
				current3.node.transform.SetParent(null);
			}
			this._elemRenderArr.Clear();
			this._objectDataMap.Clear();
		}

		public void insertElement(string txt, Color color, int fontSize, bool isUnderLine, bool isOutLine, Color outLineColor, string data)
		{
			this._richElements.Add(new LRichElementText(color, txt, fontSize, isUnderLine, isOutLine, data));
		}

		public void insertElement(string path, float fp, string data)
		{
			this._richElements.Add(new LRichElementAnim(path, fp, data));
		}

		public void insertElement(string path, string data)
		{
			this._richElements.Add(new LRichElementImage(path, data));
		}

		public void insertElement(int newline)
		{
			this._richElements.Add(new LRichElementNewline());
		}

		public LRichText()
		{
			this.alignType = RichAlignType.LEFT_TOP;
			this.maxLineWidth = 200;
			this._richElements = new List<LRichElement>();
			this._elemRenderArr = new List<LRenderElement>();
			this._cacheLabElements = new List<LRichCacheElement>();
			this._cacheImgElements = new List<LRichCacheElement>();
			this._cacheFramAnimElements = new List<LRichCacheElement>();
			this._objectDataMap = new Dictionary<GameObject, string>();
		}

		public void reloadData()
		{
			this.removeAllElements();
			RectTransform component = base.GetComponent<RectTransform>();
			switch (this.alignType)
			{
			case RichAlignType.DESIGN_CENTER:
				component.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
				goto IL_A2;
			case RichAlignType.LEFT_TOP:
				IL_2E:
				component.GetComponent<RectTransform>().pivot = new Vector2(0f, 1f);
				goto IL_A2;
			case RichAlignType.LEFT_BOTTOM:
				component.GetComponent<RectTransform>().pivot = new Vector2(0f, 0f);
				goto IL_A2;
			case RichAlignType.RIGHT_BOTTOM:
				component.GetComponent<RectTransform>().pivot = new Vector2(1f, 0f);
				goto IL_A2;
			}
			goto IL_2E;
			IL_A2:
			foreach (LRichElement current in this._richElements)
			{
				bool flag = current.type == RichType.TEXT;
				if (flag)
				{
					LRichElementText lRichElementText = current as LRichElementText;
					char[] array = lRichElementText.txt.ToCharArray();
					TextGenerator textGenerator = new TextGenerator();
					char[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						char c = array2[i];
						LRenderElement lRenderElement = default(LRenderElement);
						lRenderElement.type = RichType.TEXT;
						lRenderElement.strChar = c.ToString();
						lRenderElement.isOutLine = lRichElementText.isOutLine;
						lRenderElement.isUnderLine = lRichElementText.isUnderLine;
						lRenderElement.font = this.font;
						lRenderElement.fontSize = lRichElementText.fontSize;
						lRenderElement.data = lRichElementText.data;
						lRenderElement.color = lRichElementText.color;
						TextGenerationSettings settings = default(TextGenerationSettings);
						settings.font = this.font;
						settings.fontSize = lRichElementText.fontSize;
						settings.lineSpacing = 1f;
						settings.verticalOverflow = VerticalWrapMode.Overflow;
						settings.horizontalOverflow = HorizontalWrapMode.Overflow;
						lRenderElement.width = (int)textGenerator.GetPreferredWidth(lRenderElement.strChar, settings);
						lRenderElement.height = (int)textGenerator.GetPreferredHeight(lRenderElement.strChar, settings);
						this._elemRenderArr.Add(lRenderElement);
					}
				}
				else
				{
					bool flag2 = current.type == RichType.IMAGE;
					if (flag2)
					{
						LRichElementImage lRichElementImage = current as LRichElementImage;
						LRenderElement lRenderElement2 = default(LRenderElement);
						lRenderElement2.type = RichType.IMAGE;
						lRenderElement2.path = lRichElementImage.path;
						lRenderElement2.data = lRichElementImage.data;
						Sprite sprite = Resources.Load(lRenderElement2.path, typeof(Sprite)) as Sprite;
						lRenderElement2.width = (int)sprite.rect.size.x;
						lRenderElement2.height = (int)sprite.rect.size.y;
						this._elemRenderArr.Add(lRenderElement2);
					}
					else
					{
						bool flag3 = current.type == RichType.ANIM;
						if (flag3)
						{
							LRichElementAnim lRichElementAnim = current as LRichElementAnim;
							LRenderElement lRenderElement3 = default(LRenderElement);
							lRenderElement3.type = RichType.ANIM;
							lRenderElement3.path = lRichElementAnim.path;
							lRenderElement3.data = lRichElementAnim.data;
							lRenderElement3.fs = lRichElementAnim.fs;
							Sprite sprite2 = Resources.Load(lRenderElement3.path + "/1", typeof(Sprite)) as Sprite;
							lRenderElement3.width = (int)sprite2.rect.size.x;
							lRenderElement3.height = (int)sprite2.rect.size.y;
							this._elemRenderArr.Add(lRenderElement3);
						}
						else
						{
							bool flag4 = current.type == RichType.NEWLINE;
							if (flag4)
							{
								LRenderElement item = default(LRenderElement);
								item.isNewLine = true;
								this._elemRenderArr.Add(item);
							}
						}
					}
				}
			}
			this._richElements.Clear();
			this.formarRenderers();
		}

		protected void formarRenderers()
		{
			int num = 0;
			int num2 = 1;
			int num3 = 27;
			int count = this._elemRenderArr.Count;
			for (int i = 0; i < count; i++)
			{
				bool flag = false;
				LRenderElement lRenderElement = this._elemRenderArr[i];
				bool isNewLine = lRenderElement.isNewLine;
				if (isNewLine)
				{
					num = 0;
					num2++;
					lRenderElement.width = 10;
					lRenderElement.height = num3;
					lRenderElement.pos = new Vector2((float)num, (float)(-(float)num2 * num3));
				}
				else
				{
					bool flag2 = num + lRenderElement.width > this.maxLineWidth;
					if (flag2)
					{
						bool flag3 = lRenderElement.type == RichType.TEXT;
						if (flag3)
						{
							bool flag4 = this.isChinese(lRenderElement.strChar) || lRenderElement.strChar == " ";
							if (flag4)
							{
								num = 0;
								num2++;
								lRenderElement.pos = new Vector2((float)num, (float)(-(float)num2 * num3));
								num = lRenderElement.width;
							}
							else
							{
								int num4 = 0;
								int j = i;
								while (j > 0)
								{
									j--;
									bool flag5 = this._elemRenderArr[j].strChar == " " && this._elemRenderArr[j].pos.y == this._elemRenderArr[i - 1].pos.y;
									if (flag5)
									{
										num4 = j;
										break;
									}
								}
								bool flag6 = num4 == 0;
								if (flag6)
								{
									num = 0;
									num2++;
									lRenderElement.pos = new Vector2((float)num, (float)(-(float)num2 * num3));
									num = lRenderElement.width;
								}
								else
								{
									num = 0;
									num2++;
									flag = true;
									for (int k = num4 + 1; k <= i; k++)
									{
										LRenderElement lRenderElement2 = this._elemRenderArr[k];
										lRenderElement2.pos = new Vector2((float)num, (float)(-(float)num2 * num3));
										num += lRenderElement2.width;
										this._elemRenderArr[k] = lRenderElement2;
									}
								}
							}
						}
						else
						{
							bool flag7 = lRenderElement.type == RichType.ANIM || lRenderElement.type == RichType.IMAGE;
							if (flag7)
							{
								num2++;
								lRenderElement.pos = new Vector2(0f, (float)(-(float)num2 * num3));
								num = lRenderElement.width;
							}
						}
					}
					else
					{
						lRenderElement.pos = new Vector2((float)num, (float)(-(float)num2 * num3));
						num += lRenderElement.width + 1 + this.countNumber(lRenderElement.strChar);
					}
				}
				bool flag8 = !flag;
				if (flag8)
				{
					this._elemRenderArr[i] = lRenderElement;
				}
			}
			Dictionary<int, List<LRenderElement>> dictionary = new Dictionary<int, List<LRenderElement>>();
			List<int> list = new List<int>();
			count = this._elemRenderArr.Count;
			for (int l = 0; l < count; l++)
			{
				LRenderElement lRenderElement3 = this._elemRenderArr[l];
				bool flag9 = !dictionary.ContainsKey((int)lRenderElement3.pos.y);
				if (flag9)
				{
					List<LRenderElement> value = new List<LRenderElement>();
					dictionary[(int)lRenderElement3.pos.y] = value;
				}
				dictionary[(int)lRenderElement3.pos.y].Add(lRenderElement3);
			}
			List<List<LRenderElement>> list2 = new List<List<LRenderElement>>();
			foreach (KeyValuePair<int, List<LRenderElement>> current in dictionary)
			{
				list.Add(-1 * current.Key);
			}
			list.Sort();
			count = list.Count;
			for (int m = 0; m < count; m++)
			{
				int key = -1 * list[m];
				string text = "";
				LRenderElement lRenderElement4 = dictionary[key][0];
				LRenderElement lRenderElement5 = dictionary[key][0];
				bool flag10 = dictionary[key].Count > 0;
				if (flag10)
				{
					List<LRenderElement> list3 = new List<LRenderElement>();
					foreach (LRenderElement current2 in dictionary[key])
					{
						bool flag11 = lRenderElement4.type == RichType.TEXT && current2.type == RichType.TEXT;
						if (flag11)
						{
							bool flag12 = lRenderElement4.color == current2.color && (lRenderElement4.data == current2.data || current2.data == "");
							if (flag12)
							{
								text += current2.strChar;
							}
							else
							{
								bool flag13 = lRenderElement5.type == RichType.TEXT;
								if (flag13)
								{
									LRenderElement item = lRenderElement5.Clone();
									item.strChar = text;
									list3.Add(item);
									lRenderElement5 = current2;
									text = current2.strChar;
								}
							}
						}
						else
						{
							bool flag14 = current2.type == RichType.IMAGE || current2.type == RichType.ANIM || current2.type == RichType.NEWLINE;
							if (flag14)
							{
								bool flag15 = lRenderElement5.type == RichType.TEXT;
								if (flag15)
								{
									LRenderElement item2 = lRenderElement5.Clone();
									item2.strChar = text;
									text = "";
									list3.Add(item2);
								}
								list3.Add(current2);
							}
							else
							{
								bool flag16 = lRenderElement4.type > RichType.TEXT;
								if (flag16)
								{
									lRenderElement5 = current2;
									bool flag17 = current2.type == RichType.TEXT;
									if (flag17)
									{
										text = current2.strChar;
									}
								}
							}
						}
						lRenderElement4 = current2;
					}
					bool flag18 = lRenderElement5.type == RichType.TEXT;
					if (flag18)
					{
						LRenderElement item3 = lRenderElement5.Clone();
						item3.strChar = text;
						list3.Add(item3);
					}
					list2.Add(list3);
				}
			}
			int num5 = 0;
			int num6 = this.font.fontSize;
			this.realLineHeight = 0;
			count = list2.Count;
			for (int n = 0; n < count; n++)
			{
				List<LRenderElement> list4 = list2[n];
				int num7 = 0;
				foreach (LRenderElement current3 in list4)
				{
					num7 = Mathf.Max(num7, current3.height);
				}
				num6 = num7;
				this.realLineHeight += num7;
				num5 += num7 - num3;
				int count2 = list4.Count;
				for (int num8 = 0; num8 < count2; num8++)
				{
					LRenderElement lRenderElement6 = list4[num8];
					lRenderElement6.pos = new Vector2(lRenderElement6.pos.x, lRenderElement6.pos.y - (float)num5);
					this.realLineHeight = Mathf.Max(this.realLineHeight, (int)Mathf.Abs(lRenderElement6.pos.y));
					list4[num8] = lRenderElement6;
				}
				list2[n] = list4;
			}
			this.realLineWidth = 0;
			GameObject gameObject = null;
			foreach (List<LRenderElement> current4 in list2)
			{
				int num9 = 0;
				foreach (LRenderElement current5 in current4)
				{
					bool flag19 = current5.type != RichType.NEWLINE;
					if (flag19)
					{
						bool flag20 = current5.type == RichType.TEXT;
						if (flag20)
						{
							gameObject = this.getCacheLabel();
							this.makeLabel(gameObject, current5);
							num9 += (int)gameObject.GetComponent<Text>().preferredWidth;
						}
						else
						{
							bool flag21 = current5.type == RichType.IMAGE;
							if (flag21)
							{
								gameObject = this.getCacheImage(true);
								this.makeImage(gameObject, current5);
								num9 += (int)gameObject.GetComponent<Image>().preferredWidth;
							}
							else
							{
								bool flag22 = current5.type == RichType.ANIM;
								if (flag22)
								{
									gameObject = this.getCacheFramAnim();
									this.makeFramAnim(gameObject, current5);
									num9 += current5.width;
								}
							}
						}
						gameObject.transform.SetParent(base.transform);
						gameObject.transform.localPosition = new Vector2(current5.pos.x, (Mathf.Abs(current5.pos.y) != (float)num6) ? (current5.pos.y + (float)this.verticalOffset) : current5.pos.y);
						this._objectDataMap[gameObject] = current5.data;
					}
				}
				this.realLineWidth = Mathf.Max(num9, this.realLineWidth);
			}
			RectTransform component = base.GetComponent<RectTransform>();
			bool flag23 = this.alignType == RichAlignType.DESIGN_CENTER;
			if (flag23)
			{
				component.sizeDelta = new Vector2((float)this.maxLineWidth, (float)this.realLineHeight);
			}
			else
			{
				bool flag24 = this.alignType == RichAlignType.LEFT_TOP;
				if (flag24)
				{
					component.sizeDelta = new Vector2((float)this.realLineWidth, (float)this.realLineHeight);
				}
			}
		}

		private void makeLabel(GameObject lab, LRenderElement elem)
		{
			Text component = lab.GetComponent<Text>();
			bool flag = component != null;
			if (flag)
			{
				component.text = elem.strChar;
				component.font = elem.font;
				component.fontSize = elem.fontSize;
				component.fontStyle = FontStyle.Normal;
				component.color = elem.color;
			}
			Outline outline = lab.GetComponent<Outline>();
			bool isOutLine = elem.isOutLine;
			if (isOutLine)
			{
				bool flag2 = outline == null;
				if (flag2)
				{
					outline = lab.AddComponent<Outline>();
				}
			}
			else
			{
				bool flag3 = outline;
				if (flag3)
				{
					UnityEngine.Object.Destroy(outline);
				}
			}
			bool isUnderLine = elem.isUnderLine;
			if (isUnderLine)
			{
				GameObject cacheImage = this.getCacheImage(false);
				Image component2 = cacheImage.GetComponent<Image>();
				component2.color = elem.color;
				component2.GetComponent<RectTransform>().sizeDelta = new Vector2(component.preferredWidth, 1f);
				cacheImage.transform.SetParent(base.transform);
				cacheImage.transform.localPosition = new Vector2(elem.pos.x, elem.pos.y);
			}
			bool isNewLine = elem.isNewLine;
			if (isNewLine)
			{
				component.text = elem.strChar;
				component.font = this.font;
				component.fontSize = elem.fontSize;
				component.fontStyle = FontStyle.Normal;
				component.color = elem.color;
			}
		}

		private void makeImage(GameObject img, LRenderElement elem)
		{
			Image component = img.GetComponent<Image>();
			bool flag = component != null;
			if (flag)
			{
				Sprite sprite = Resources.Load(elem.path, typeof(Sprite)) as Sprite;
				component.sprite = sprite;
			}
		}

		private void makeFramAnim(GameObject anim, LRenderElement elem)
		{
			LMovieClip component = anim.GetComponent<LMovieClip>();
			bool flag = component != null;
			if (flag)
			{
				component.path = elem.path;
				component.fps = elem.fs;
				component.loadTexture();
				component.play();
			}
		}

		protected GameObject getCacheLabel()
		{
			GameObject gameObject = null;
			int count = this._cacheLabElements.Count;
			for (int i = 0; i < count; i++)
			{
				LRichCacheElement lRichCacheElement = this._cacheLabElements[i];
				bool flag = !lRichCacheElement.isUse;
				if (flag)
				{
					lRichCacheElement.isUse = true;
					gameObject = lRichCacheElement.node;
					break;
				}
			}
			bool flag2 = gameObject == null;
			if (flag2)
			{
				gameObject = new GameObject();
				gameObject.AddComponent<Text>();
				ContentSizeFitter contentSizeFitter = gameObject.AddComponent<ContentSizeFitter>();
				contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
				contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
				RectTransform component = gameObject.GetComponent<RectTransform>();
				component.pivot = Vector2.zero;
				switch (this.pivotType)
				{
				case RichPivotType.LEFT_TOP:
					IL_BE:
					component.anchorMax = new Vector2(0f, 1f);
					component.anchorMin = new Vector2(0f, 1f);
					goto IL_181;
				case RichPivotType.LEFT_BOTTOM:
					component.anchorMax = new Vector2(0f, 0f);
					component.anchorMin = new Vector2(0f, 0f);
					goto IL_181;
				case RichPivotType.RIGHT_BOTTOM:
					component.anchorMax = new Vector2(1f, 0f);
					component.anchorMin = new Vector2(1f, 0f);
					goto IL_181;
				case RichPivotType.RIGHT_TOP:
					component.anchorMax = new Vector2(1f, 1f);
					component.anchorMin = new Vector2(1f, 1f);
					goto IL_181;
				}
				goto IL_BE;
				IL_181:
				LRichCacheElement lRichCacheElement2 = new LRichCacheElement(gameObject);
				lRichCacheElement2.isUse = true;
				this._cacheLabElements.Add(lRichCacheElement2);
			}
			return gameObject;
		}

		protected GameObject getCacheImage(bool isFitSize)
		{
			GameObject gameObject = null;
			int count = this._cacheLabElements.Count;
			for (int i = 0; i < count; i++)
			{
				LRichCacheElement lRichCacheElement = this._cacheLabElements[i];
				bool flag = !lRichCacheElement.isUse;
				if (flag)
				{
					lRichCacheElement.isUse = true;
					gameObject = lRichCacheElement.node;
					break;
				}
			}
			bool flag2 = gameObject == null;
			if (flag2)
			{
				gameObject = new GameObject();
				gameObject.AddComponent<Image>();
				ContentSizeFitter contentSizeFitter = gameObject.AddComponent<ContentSizeFitter>();
				contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
				contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
				RectTransform component = gameObject.GetComponent<RectTransform>();
				component.pivot = Vector2.zero;
				switch (this.pivotType)
				{
				case RichPivotType.LEFT_TOP:
					IL_C2:
					component.anchorMax = new Vector2(0f, 1f);
					component.anchorMin = new Vector2(0f, 1f);
					goto IL_185;
				case RichPivotType.LEFT_BOTTOM:
					component.anchorMax = new Vector2(0f, 0f);
					component.anchorMin = new Vector2(0f, 0f);
					goto IL_185;
				case RichPivotType.RIGHT_BOTTOM:
					component.anchorMax = new Vector2(1f, 0f);
					component.anchorMin = new Vector2(1f, 0f);
					goto IL_185;
				case RichPivotType.RIGHT_TOP:
					component.anchorMax = new Vector2(1f, 1f);
					component.anchorMin = new Vector2(1f, 1f);
					goto IL_185;
				}
				goto IL_C2;
				IL_185:
				LRichCacheElement lRichCacheElement2 = new LRichCacheElement(gameObject);
				lRichCacheElement2.isUse = true;
				this._cacheLabElements.Add(lRichCacheElement2);
			}
			ContentSizeFitter component2 = gameObject.GetComponent<ContentSizeFitter>();
			component2.enabled = isFitSize;
			return gameObject;
		}

		protected GameObject getCacheFramAnim()
		{
			GameObject gameObject = null;
			int count = this._cacheFramAnimElements.Count;
			for (int i = 0; i < count; i++)
			{
				LRichCacheElement lRichCacheElement = this._cacheFramAnimElements[i];
				bool flag = !lRichCacheElement.isUse;
				if (flag)
				{
					lRichCacheElement.isUse = true;
					gameObject = lRichCacheElement.node;
					break;
				}
			}
			bool flag2 = gameObject == null;
			if (flag2)
			{
				gameObject = new GameObject();
				gameObject.AddComponent<Image>();
				ContentSizeFitter contentSizeFitter = gameObject.AddComponent<ContentSizeFitter>();
				contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
				contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
				RectTransform component = gameObject.GetComponent<RectTransform>();
				component.pivot = Vector2.zero;
				switch (this.pivotType)
				{
				case RichPivotType.LEFT_TOP:
					IL_BE:
					component.anchorMax = new Vector2(0f, 1f);
					component.anchorMin = new Vector2(0f, 1f);
					goto IL_181;
				case RichPivotType.LEFT_BOTTOM:
					component.anchorMax = new Vector2(0f, 0f);
					component.anchorMin = new Vector2(0f, 0f);
					goto IL_181;
				case RichPivotType.RIGHT_BOTTOM:
					component.anchorMax = new Vector2(1f, 0f);
					component.anchorMin = new Vector2(1f, 0f);
					goto IL_181;
				case RichPivotType.RIGHT_TOP:
					component.anchorMax = new Vector2(1f, 1f);
					component.anchorMin = new Vector2(1f, 1f);
					goto IL_181;
				}
				goto IL_BE;
				IL_181:
				gameObject.AddComponent<LMovieClip>();
				LRichCacheElement lRichCacheElement2 = new LRichCacheElement(gameObject);
				lRichCacheElement2.isUse = true;
				this._cacheFramAnimElements.Add(lRichCacheElement2);
			}
			return gameObject;
		}

		protected bool isChinese(string text)
		{
			bool result = false;
			char[] array = text.ToCharArray();
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag = array[i] >= '一' && array[i] <= '龻';
				if (flag)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		protected int countNumber(string text)
		{
			int num = 0;
			char[] array = text.ToCharArray();
			int num2 = array.Length;
			for (int i = 0; i < num2; i++)
			{
				bool flag = array[i] >= '0' && array[i] <= '9';
				if (flag)
				{
					num++;
				}
			}
			return num;
		}

		public void OnPointerClick(PointerEventData data)
		{
			bool flag = this._objectDataMap.ContainsKey(data.pointerEnter);
			if (flag)
			{
				bool flag2 = this.onClickHandler != null && this._objectDataMap[data.pointerEnter] != "";
				if (flag2)
				{
					this.onClickHandler(this._objectDataMap[data.pointerEnter]);
				}
			}
		}
	}
}
