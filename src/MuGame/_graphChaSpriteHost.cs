using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class _graphChaSpriteHost
	{
		protected const int ORI_CERTER = 0;

		protected const int ORI_LEFT = 1;

		protected const int ORI_RIGHT = 2;

		protected const int ORI_TOP = 3;

		protected IUIContainer _groundSpr;

		protected IUIContainer _titleSpr;

		protected IUIContainer _dynamicSpr;

		protected IUIContainer _hpSpr;

		protected IUIContainer _dpSpr;

		protected float _groundHeight = 0f;

		protected Variant _titleSprites;

		protected Variant _titleOriData;

		protected List<_graphChaAniSprite> _aniSprites;

		protected List<_graphChatSprite> _chatSprites;

		protected List<Variant> _dynamicSprites;

		protected bool _disposed = false;

		protected static Variant _displayStyle = new Variant();

		protected List<Variant> _showDynArr = new List<Variant>();

		protected const int DAT_DEFAULT = 0;

		protected List<Action<float, Variant>> _dynamicAniFun;

		protected Action _initDpCallBack = null;

		protected Dictionary<string, Variant> _dpInfo = new Dictionary<string, Variant>();

		private int _dpNum = 0;

		protected Variant _dpAniInfo;

		public IUIContainer displyObj
		{
			get
			{
				return this._groundSpr;
			}
		}

		public float titleHeight
		{
			get
			{
				return this._titleSpr.y;
			}
			set
			{
				this._titleSpr.y = -value;
			}
		}

		public float groundHeight
		{
			get
			{
				return this._groundHeight;
			}
			set
			{
				this._groundHeight = value;
			}
		}

		public float dynamicHeight
		{
			get
			{
				return this._dynamicSpr.y;
			}
			set
			{
				this._dynamicSpr.y = -value;
			}
		}

		public float hpHeight
		{
			get
			{
				return -this._hpSpr.y;
			}
			set
			{
				this._hpSpr.y = -value;
			}
		}

		public float dpHeight
		{
			get
			{
				return -this._dpSpr.y;
			}
			set
			{
				this._dpSpr.y = -value;
			}
		}

		public float x
		{
			set
			{
				this._groundSpr.x = value;
			}
		}

		public float y
		{
			set
			{
				this._groundSpr.y = value + this._groundHeight;
			}
		}

		public float dpPosX
		{
			set
			{
				this._dpSpr.x = -value;
			}
		}

		public float dpPosY
		{
			set
			{
				this._dpSpr.y = -value;
			}
		}

		public _graphChaSpriteHost()
		{
			this._aniSprites = new List<_graphChaAniSprite>();
			this._chatSprites = new List<_graphChatSprite>();
			this._dynamicSprites = new List<Variant>();
			this._titleSprites = new Variant();
			this._titleOriData = new Variant();
			this._groundSpr.addChild(this._titleSpr);
			this._groundSpr.addChild(this._dynamicSpr);
			this._groundSpr.addChild(this._hpSpr);
			this._groundSpr.addChild(this._dpSpr);
			this._hpSpr.visible = false;
			this._dpSpr.visible = false;
		}

		public static Variant GetDisplayStyle(string id)
		{
			Variant variant = _graphChaSpriteHost._displayStyle[id];
			bool flag = variant != null;
			Variant result;
			if (flag)
			{
				result = variant;
			}
			else
			{
				result = variant;
			}
			return result;
		}

		public static _graphChaSprite createChaSprite(string tp)
		{
			_graphChaSprite result;
			if (!(tp == "txt"))
			{
				if (!(tp == "img"))
				{
					if (!(tp == "ani"))
					{
						if (!(tp == "numimg"))
						{
							if (!(tp == "chat"))
							{
								if (!(tp == "progress"))
								{
									result = null;
								}
								else
								{
									result = new _graphChaProgressSprite();
								}
							}
							else
							{
								result = new _graphChatSprite();
							}
						}
						else
						{
							result = new _graphChaImgNumSprite();
						}
					}
					else
					{
						result = new _graphChaAniSprite();
					}
				}
				else
				{
					result = new _graphChaBmpSprite();
				}
			}
			else
			{
				result = new _graphChaTxtSprite();
			}
			return result;
		}

		public void addTitleSpr(string tp, Variant titleConf, int showtp = 0, Variant showInfo = null)
		{
			Variant variant = this._titleSprites[tp];
			bool flag = variant == null;
			if (flag)
			{
				bool flag2 = titleConf == null;
				if (flag2)
				{
					return;
				}
				variant = new Variant();
				variant["conf"] = titleConf;
				variant["sprs"] = new Variant();
				variant["sprs"]._val = new Dictionary<string, Variant>();
				this._titleSprites[tp] = variant;
			}
			Variant variant2 = variant["conf"]["show"][showtp.ToString()];
			bool flag3 = variant2 != null && false;
			if (flag3)
			{
				_graphChaSprite graphChaSprite = _graphChaSpriteHost.createChaSprite(variant2["tp"]);
				bool flag4 = showInfo == null;
				if (flag4)
				{
					showInfo = new Variant();
				}
				this._adjustShowInfo(variant2, showInfo);
				bool flag5 = variant["sprs"].ContainsKey(showtp.ToString());
				if (flag5)
				{
					this.removeTitleSpr(tp, showtp, true);
				}
				graphChaSprite.userdata = GameTools.createGroup(new Variant[]
				{
					"tp",
					tp,
					"showtp",
					showtp,
					"conf",
					variant["conf"]
				});
				variant["sprs"][showtp.ToString()] = GameTools.createGroup(new object[]
				{
					"spr",
					graphChaSprite,
					"add",
					false
				});
				graphChaSprite.initShowInfo(showInfo, new Action<_graphChaSprite>(this.initTitleFinish));
			}
		}

		protected void initTitleFinish(_graphChaSprite spr)
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				spr.dispose();
			}
			else
			{
				Variant variant = this._titleSprites[spr.userdata["tp"]];
				bool flag = variant == null;
				if (flag)
				{
					spr.dispose();
				}
				else
				{
					Dictionary<string, Variant> dictionary = variant["sprs"][spr.userdata["showtp"]._str]._val as Dictionary<string, Variant>;
					bool flag2 = dictionary == null || dictionary["spr"]._val != spr;
					if (flag2)
					{
						spr.dispose();
					}
					else
					{
						bool flag3 = (dictionary["spr"]._val as _graphChaSprite).dispObj == null;
						if (flag3)
						{
							variant["sprs"][spr.userdata["showtp"]._str] = null;
						}
						else
						{
							dictionary["add"] = true;
							this._titleSpr.addChild(spr.dispObj);
							this._insertOriSprite(spr, spr.userdata["conf"]["ori"], this._titleOriData);
							bool flag4 = spr is _graphChaAniSprite;
							if (flag4)
							{
								this._aniSprites.Add(spr as _graphChaAniSprite);
							}
							bool flag5 = spr is _graphChatSprite;
							if (flag5)
							{
								this._chatSprites.Add(spr as _graphChatSprite);
							}
						}
					}
				}
			}
		}

		public void removeTitleSpr(string tp, int showtp = 0, bool updatePos = true)
		{
			Variant variant = this._titleSprites[tp];
			bool flag = variant != null;
			if (flag)
			{
				Variant variant2 = variant["sprs"][showtp.ToString()];
				bool flag2 = variant2 != null;
				if (flag2)
				{
					variant["sprs"][showtp.ToString()] = null;
					bool @bool = variant2["add"]._bool;
					if (@bool)
					{
						this._titleSpr.removeChild((variant2["spr"]._val as _graphChaSprite).dispObj, false);
						bool flag3 = variant2["spr"]._val is _graphChaAniSprite;
						int num;
						if (flag3)
						{
							num = this._aniSprites.IndexOf(variant2["spr"]._val as _graphChaAniSprite);
							bool flag4 = num >= 0;
							if (flag4)
							{
								this._aniSprites.RemoveAt(num);
							}
						}
						bool flag5 = variant2["spr"]._val is _graphChatSprite;
						if (flag5)
						{
							num = this._chatSprites.IndexOf(variant2["spr"]._val as _graphChatSprite);
							bool flag6 = num >= 0;
							if (flag6)
							{
								this._chatSprites.RemoveAt(num);
							}
						}
						int idx = variant["conf"]["ori"];
						Variant variant3 = this._titleOriData[idx];
						num = (variant3["sprs"]._val as List<_graphChaSprite>).IndexOf(variant2["spr"]._val as _graphChaSprite);
						(variant3["sprs"]._val as List<_graphChaSprite>).RemoveAt(num);
						if (updatePos)
						{
							this._updateOriSprite(variant3, this._titleOriData);
						}
					}
					(variant2["spr"]._val as _graphChaSprite).dispose();
				}
			}
		}

		protected void _adjustShowInfo(Variant showConf, Variant showInfo)
		{
			bool flag = showConf.ContainsKey("sptm");
			if (flag)
			{
				showInfo["sptm"] = showConf["sptm"];
			}
			bool flag2 = showConf.ContainsKey("res");
			if (flag2)
			{
				showInfo["res"] = showConf["res"];
			}
			bool flag3 = showConf.ContainsKey("imageNum");
			if (flag3)
			{
				showInfo["imageNum"] = showConf["imageNum"];
			}
			bool flag4 = showConf.ContainsKey("style");
			if (flag4)
			{
				showInfo["style"] = _graphChaSpriteHost.GetDisplayStyle(showConf["style"]);
			}
			bool flag5 = showInfo.ContainsKey("text");
			if (flag5)
			{
				Variant variant = new Variant();
				bool flag6 = showConf.ContainsKey("fmt");
				if (flag6)
				{
					variant = GameTools.mergeSimpleObject(showConf["fmt"][0], variant, false, true);
				}
				bool flag7 = showInfo.ContainsKey("fmt");
				if (flag7)
				{
					variant = GameTools.mergeSimpleObject(showInfo["fmt"][0], variant, false, true);
				}
				showInfo["fmt"] = variant;
			}
			bool flag8 = showConf.ContainsKey("g9");
			if (flag8)
			{
				showInfo["g9"] = showConf["g9"][0];
			}
			bool flag9 = showConf.ContainsKey("imgprop");
			if (flag9)
			{
				showInfo["imgprop"] = showConf["imgprop"][0];
			}
			bool flag10 = showConf.ContainsKey("textprop");
			if (flag10)
			{
				showInfo["textprop"] = showConf["textprop"][0];
			}
			bool flag11 = showConf.ContainsKey("tm");
			if (flag11)
			{
				showInfo["tm"] = showConf["tm"];
			}
			bool flag12 = showConf.ContainsKey("uv");
			if (flag12)
			{
				showInfo["uv"] = showConf["uv"][0];
			}
			bool flag13 = showConf.ContainsKey("playtm");
			if (flag13)
			{
				showInfo["playtm"] = showConf["playtm"];
			}
			bool flag14 = showConf.ContainsKey("width");
			if (flag14)
			{
				showInfo["width"] = showConf["width"];
			}
			bool flag15 = showConf.ContainsKey("height");
			if (flag15)
			{
				showInfo["height"] = showConf["height"];
			}
		}

		protected Variant _updateSpritesPos(List<_graphChaSprite> sprs, uint ori, int stx, int sty)
		{
			float num = 0f;
			float num2 = 0f;
			switch (ori)
			{
			case 0u:
				foreach (_graphChaSprite current in sprs)
				{
					float num3 = current.width / 2f;
					current.dispObj.x = -num3;
					bool flag = num3 > num && current.userdata["conf"]["applyoff"] != null;
					if (flag)
					{
						num = num3;
					}
					num3 = current.height;
					current.dispObj.y = -num3;
					bool flag2 = num3 > num2 && current.userdata["conf"]["applyoff"] != null;
					if (flag2)
					{
						num2 = num3;
					}
				}
				break;
			case 1u:
				num = (float)stx;
				foreach (_graphChaSprite current2 in sprs)
				{
					float num4 = 0f;
					bool flag3 = current2.userdata["conf"].ContainsKey("sp");
					if (flag3)
					{
						num4 = current2.userdata["conf"]["sp"]._float;
					}
					float num3 = num + current2.width + num4;
					current2.dispObj.x = -num3;
					current2.dispObj.y = -current2.height;
					bool flag4 = current2.userdata["conf"]["applyoff"] != null;
					if (flag4)
					{
						num = num3;
					}
				}
				break;
			case 2u:
				num = (float)stx;
				foreach (_graphChaSprite current3 in sprs)
				{
					float num4 = 0f;
					bool flag5 = current3.userdata["conf"].ContainsKey("sp");
					if (flag5)
					{
						num4 = current3.userdata["conf"]["sp"]._float;
					}
					float num3 = num + num4;
					current3.dispObj.x = -num3;
					current3.dispObj.y = -current3.height;
					bool flag6 = current3.userdata["conf"]["applyoff"] != null;
					if (flag6)
					{
						num = num3 + current3.width;
					}
				}
				break;
			case 3u:
				num2 = (float)sty;
				foreach (_graphChaSprite current4 in sprs)
				{
					float num4 = 0f;
					bool flag7 = current4.userdata["conf"].ContainsKey("sp");
					if (flag7)
					{
						num4 = current4.userdata["conf"]["sp"]._float;
					}
					float num3 = num2 + current4.height + num4;
					current4.dispObj.y = -num3;
					current4.dispObj.x = -current4.width / 2f;
					bool flag8 = current4.userdata["conf"]["applyoff"] != null;
					if (flag8)
					{
						num2 = num3;
					}
				}
				break;
			}
			return GameTools.createGroup(new Variant[]
			{
				"offx",
				num,
				"offy",
				num2
			});
		}

		protected void _updateOriSprite(Variant oriData, Variant oriDatas)
		{
			bool flag = oriData["ori"]._int == 0;
			if (flag)
			{
				Variant variant = this._updateSpritesPos(oriData["sprs"]._val as List<_graphChaSprite>, 0u, 0, 0);
				Variant variant2 = oriDatas["other"];
				bool flag2 = oriData["offx"]._float < variant["offx"]._float;
				if (flag2)
				{
					oriData["offx"] = variant["offx"];
					Variant variant3 = oriDatas[1];
					bool flag3 = variant3 != null;
					if (flag3)
					{
						Variant variant4 = this._updateSpritesPos(variant3["sprs"]._val as List<_graphChaSprite>, 1u, oriData["offx"], 0);
						variant3["offx"] = variant4["offx"];
						variant3["offy"] = variant4["offy"];
					}
					variant3 = oriDatas[2];
					bool flag4 = variant3 != null;
					if (flag4)
					{
						Variant variant4 = this._updateSpritesPos(variant3["sprs"]._val as List<_graphChaSprite>, 2u, oriData["offx"], 0);
						variant3["offx"] = variant4["offx"];
						variant3["offy"] = variant4["offy"];
					}
				}
				bool flag5 = oriData["offy"] < variant["offy"];
				if (flag5)
				{
					oriData["offy"] = variant["offy"];
					bool flag6 = variant2 != null;
					Variant variant3;
					if (flag6)
					{
						variant3 = variant2[3];
						bool flag7 = variant3 != null;
						if (flag7)
						{
							Variant variant4 = this._updateSpritesPos(variant3["sprs"]._val as List<_graphChaSprite>, 3u, 0, oriData["offy"]);
							variant3["offx"] = variant4["offx"];
							variant3["offy"] = variant4["offy"];
						}
					}
					variant3 = oriDatas[3];
					bool flag8 = variant3 != null;
					if (flag8)
					{
						Variant variant4 = this._updateSpritesPos(variant3["sprs"]._val as List<_graphChaSprite>, 3u, 0, oriData["offy"]);
						variant3["offx"] = variant4["offx"];
						variant3["offy"] = variant4["offy"];
					}
				}
			}
			else
			{
				Variant variant5 = oriDatas[0];
				bool flag9 = variant5 != null;
				Variant variant4;
				if (flag9)
				{
					variant4 = this._updateSpritesPos(oriData["sprs"]._val as List<_graphChaSprite>, oriData["ori"], variant5["offx"], variant5["offy"]);
				}
				else
				{
					variant4 = this._updateSpritesPos(oriData["sprs"]._val as List<_graphChaSprite>, oriData["ori"], 0, 19);
				}
				oriData["offx"] = variant4["offx"];
				oriData["offy"] = variant4["offy"];
			}
		}

		protected void _insertOriSprite(_graphChaSprite newspr, int ori, Variant oriDatas)
		{
			Variant variant = oriDatas[ori];
			bool flag = variant == null;
			if (flag)
			{
				variant = GameTools.createGroup(new object[]
				{
					"sprs",
					new List<_graphChaSprite>
					{
						newspr
					},
					"offx",
					0,
					"offy",
					0,
					"ori",
					ori
				});
				this._titleOriData[ori] = variant;
			}
			else
			{
				bool flag2 = ori == 0;
				if (flag2)
				{
					(variant["sprs"]._val as List<_graphChaSprite>).Add(newspr);
				}
				else
				{
					int i;
					for (i = (variant["sprs"]._val as List<_graphChaSprite>).Count; i > 0; i--)
					{
						bool flag3 = this._compareSprite((variant["sprs"]._val as List<_graphChaSprite>)[i - 1].userdata, newspr.userdata) <= 0;
						if (flag3)
						{
							break;
						}
					}
					bool flag4 = i >= (variant["sprs"]._val as List<_graphChaSprite>).Count;
					if (flag4)
					{
						(variant["sprs"]._val as List<_graphChaSprite>).Add(newspr);
					}
					else
					{
						(variant["sprs"]._val as List<_graphChaSprite>).Insert(i, newspr);
					}
				}
			}
			this._updateOriSprite(variant, oriDatas);
		}

		protected int _compareSprite(Variant first, Variant second)
		{
			Variant variant = first["conf"];
			Variant variant2 = second["conf"];
			bool flag = variant["idx"]._int < variant2["idx"]._int;
			int result;
			if (flag)
			{
				result = -1;
			}
			else
			{
				bool flag2 = variant["idx"]._int > variant2["idx"]._int;
				if (flag2)
				{
					result = 1;
				}
				else
				{
					bool flag3 = variant.ContainsKey("sort") && variant == variant2;
					if (flag3)
					{
						Variant variant3 = variant["sort"];
						int num = variant3._arr.IndexOf(first["showtp"]);
						int num2 = variant3._arr.IndexOf(second["showtp"]);
						bool flag4 = num < num2;
						if (flag4)
						{
							result = -1;
							return result;
						}
						bool flag5 = num > num2;
						if (flag5)
						{
							result = 1;
							return result;
						}
					}
					result = 0;
				}
			}
			return result;
		}

		protected void _initDynamicAniFuns()
		{
			this._dynamicAniFun = new List<Action<float, Variant>>();
			this._dynamicAniFun.Add(new Action<float, Variant>(this._dynamicDefaultAni));
			this._dynamicAniFun.Add(new Action<float, Variant>(this._dynamicAniTp1));
			this._dynamicAniFun.Add(new Action<float, Variant>(this._dynamicAniTp2));
			this._dynamicAniFun.Add(new Action<float, Variant>(this._dynamicAniTp3));
		}

		protected void initDynamicFinish(_graphChaSprite spr)
		{
			bool disposed = this._disposed;
			if (disposed)
			{
				spr.dispose();
			}
			else
			{
				spr.userdata["loaded"] = true;
			}
		}

		protected void createDynamicAni(Variant info)
		{
			Variant variant = info["showInfo"];
			IUIBaseControl dispObj = (info["spr"]._val as _graphChaSprite).dispObj;
			Variant variant2 = GameTools.createGroup(new Variant[]
			{
				"stm",
				info["stm"]._float,
				"etm",
				info["etm"]._float,
				"stx",
				0,
				"sty",
				0,
				"dir",
				variant["dir"]._int
			});
			variant2["spr"] = info["spr"];
			variant2["conf"] = info["aniConf"];
			switch (info["aniConf"]["tp"]._int)
			{
			case 0:
				variant2["stx"] = -dispObj.width / 2f;
				variant2["sty"] = 30;
				variant2["criatk"] = variant["criatk"]._bool;
				break;
			case 1:
				variant2["stx"] = -dispObj.width / 2f;
				variant2["sty"] = 30;
				break;
			case 2:
				variant2["stx"] = -dispObj.width / 2f;
				variant2["sty"] = 30;
				break;
			case 3:
				variant2["stx"] = -dispObj.width / 2f;
				variant2["sty"] = 30;
				variant2["criatk"] = variant["criatk"]._bool;
				break;
			}
			dispObj.x = variant2["stx"]._float;
			dispObj.y = variant2["sty"]._float;
			this._dynamicSpr.addChild(dispObj);
			this._showDynArr.Add(variant2);
		}

		protected void handleDynamicSprites()
		{
			float num = (float)GameTools.getTimer();
			int i = this._dynamicSprites.Count - 1;
			while (i >= 0)
			{
				Variant variant = this._dynamicSprites[i];
				bool flag = variant["stm"]._float <= num;
				if (flag)
				{
					bool flag2 = variant["etm"]._float <= num;
					if (flag2)
					{
						(variant["spr"]._val as _graphChaSprite).dispose();
						this._dynamicSprites.RemoveAt(i);
					}
					else
					{
						bool flag3 = !(variant["spr"]._val as _graphChaSprite).userdata["loaded"]._bool;
						if (!flag3)
						{
							this._dynamicSprites.RemoveAt(i);
							this.createDynamicAni(variant);
						}
					}
				}
				IL_D9:
				i--;
				continue;
				goto IL_D9;
			}
		}

		protected void dynamicAniProcess()
		{
			float num = (float)GameTools.getTimer();
			for (int i = this._showDynArr.Count - 1; i >= 0; i--)
			{
				Variant variant = this._showDynArr[i];
				bool flag = num > variant["etm"]._float;
				if (flag)
				{
					this._dynamicSpr.removeChild((variant["spr"]._val as _graphChaSprite).dispObj, false);
					(variant["spr"]._val as _graphChaSprite).dispose();
					this._showDynArr.RemoveAt(i);
				}
				else
				{
					this._dynamicAniFun[variant["conf"]["tp"]._int](num, variant);
				}
			}
		}

		protected void _dynamicDefaultAni(float currTm, Variant aniInfo)
		{
			Variant variant = aniInfo["conf"];
			float num = currTm - aniInfo["stm"];
			_graphChaSprite graphChaSprite = aniInfo["spr"]._val as _graphChaSprite;
			bool @bool = aniInfo["criatk"]._bool;
			float num2;
			if (@bool)
			{
				bool flag = num < variant["scaletm_b"]._float;
				if (flag)
				{
					num2 = num / variant["scaletm_b"]._float + 1f;
					graphChaSprite.dispObj.scale = num2;
				}
				else
				{
					bool flag2 = num < variant["scaletm_s"]._float + variant["scaletm_b"]._float;
					if (flag2)
					{
						num2 = 2f - (num - variant["scaletm_b"]._float) / variant["scaletm_s"]._float;
						graphChaSprite.dispObj.scale = num2;
					}
					else
					{
						num2 = 1f;
						graphChaSprite.dispObj.scale = num2;
						aniInfo["criatk"] = false;
					}
				}
				graphChaSprite.dispObj.x = aniInfo["stx"]._float - (num2 - 1f) * graphChaSprite.dispObj.width / 4f;
			}
			num2 = num / variant["ttm"]._float - 1f;
			graphChaSprite.dispObj.y = aniInfo["sty"]._float - 120f * (num2 * num2 * num2 + 1f);
			bool flag3 = variant["ttm"]._float - num < variant["alphatm"]._float;
			if (flag3)
			{
				graphChaSprite.dispObj.alpha = (variant["ttm"]._float - num) / variant["alphatm"]._float;
			}
		}

		protected void _dynamicAniTp1(float currTm, Variant aniInfo)
		{
			Variant variant = aniInfo["conf"];
			float num = currTm - aniInfo["stm"]._float;
			_graphChaSprite graphChaSprite = aniInfo["spr"]._val as _graphChaSprite;
			float num2 = variant["distance"]._float;
			bool flag = aniInfo["dri"]["x"]._float > 0f;
			if (flag)
			{
				num2 -= graphChaSprite.dispObj.width;
			}
			float num3 = variant["ttm"]._float - variant["stop_ttm"]._float;
			bool flag2 = num < variant["stop_s"]._float;
			float num4;
			float num5;
			float val;
			float val2;
			if (flag2)
			{
				num4 = num;
				num5 = variant["stop_s"]._float;
				val = aniInfo["stx"]._float;
				val2 = aniInfo["sty"]._float;
			}
			else
			{
				bool flag3 = num > variant["stop_s"]._float + variant["stop_ttm"]._float;
				if (flag3)
				{
					num4 = num - variant["stop_ttm"]._float - variant["stop_s"]._float;
					num5 = num3 - variant["stop_s"]._float;
					val = num2 * aniInfo["dri"]["x"]._float * (variant["stop_s"]._float / num3) + aniInfo["stx"]._float;
					val2 = num2 * aniInfo["dri"]["y"]._float * (variant["stop_s"]._float / num3) + aniInfo["sty"]._float;
				}
				else
				{
					num4 = 0f;
					num5 = 0f;
					val = num2 * aniInfo["dri"]["x"]._float * (variant["stop_s"]._float / num3) + aniInfo["stx"]._float;
					val2 = num2 * aniInfo["dri"]["y"]._float * (variant["stop_s"]._float / num3) + aniInfo["sty"]._float;
				}
			}
			float val3 = num2 * aniInfo["dri"]["x"]._float * (num5 / num3);
			float val4 = num2 * aniInfo["dri"]["y"]._float * (num5 / num3);
			num5 += (float)GameTools.randomInst.NextDouble() * 50f;
			Variant att = GameTools.createGroup(new Variant[]
			{
				"duration",
				num5,
				"change",
				val3,
				"begin",
				val
			});
			float x = (float)Algorithm.TweenExpoEaseOut(att, (double)num4);
			graphChaSprite.dispObj.x = x;
			att = GameTools.createGroup(new Variant[]
			{
				"duration",
				num5,
				"change",
				val4,
				"begin",
				val2
			});
			graphChaSprite.dispObj.y = (float)Algorithm.TweenExpoEaseOut(att, (double)num4);
			bool flag4 = variant["ttm"]._float - num < variant["alphatm"]._float;
			if (flag4)
			{
				graphChaSprite.dispObj.alpha = (variant["ttm"]._float - num) / variant["alphatm"]._float;
			}
		}

		protected void _dynamicAniTp2(float currTm, Variant aniInfo)
		{
			Variant variant = aniInfo["conf"];
			float num = currTm - aniInfo["stm"]._float;
			_graphChaSprite graphChaSprite = aniInfo["spr"]._val as _graphChaSprite;
			float @float = variant["distance"]._float;
			float num2 = variant["ttm"]._float - variant["stop_ttm"]._float;
			bool flag = num < variant["stop_s"]._float;
			float num3;
			float num4;
			float val;
			float val2;
			if (flag)
			{
				num3 = num;
				num4 = variant["stop_s"]._float;
				val = aniInfo["stx"]._float;
				val2 = aniInfo["sty"]._float;
			}
			else
			{
				bool flag2 = num > variant["stop_s"]._float + variant["stop_ttm"]._float;
				if (flag2)
				{
					num3 = num - variant["stop_ttm"]._float - variant["stop_s"]._float;
					num4 = num2 - variant["stop_s"]._float;
					val = @float * aniInfo["dri"]["x"]._float * (variant["stop_s"]._float / num2) + aniInfo["stx"]._float;
					val2 = @float * aniInfo["dri"]["y"]._float * (variant["stop_s"]._float / num2) + aniInfo["sty"]._float;
				}
				else
				{
					num3 = 0f;
					num4 = 0f;
					val = @float * aniInfo["dri"]["x"]._float * (variant["stop_s"]._float / num2) + aniInfo["stx"]._float;
					val2 = @float * aniInfo["dri"]["y"]._float * (variant["stop_s"]._float / num2) + aniInfo["sty"]._float;
				}
			}
			float val3 = @float * aniInfo["dri"]["x"]._float * (num4 / num2);
			float val4 = @float * aniInfo["dri"]["y"]._float * (num4 / num2);
			num4 += (float)GameTools.randomInst.NextDouble() * 50f;
			Variant att = GameTools.createGroup(new Variant[]
			{
				"duration",
				num4,
				"change",
				val3,
				"begin",
				val
			});
			graphChaSprite.dispObj.x = (float)Algorithm.TweenExpoEaseOut(att, (double)num3);
			GameTools.createGroup(new Variant[]
			{
				"duration",
				num4,
				"change",
				val4,
				"begin",
				val2
			});
			graphChaSprite.dispObj.y = (float)Algorithm.TweenExpoEaseOut(att, (double)num3);
			bool flag3 = variant["ttm"]._float - num < variant["alphatm"]._float;
			if (flag3)
			{
				graphChaSprite.dispObj.alpha = (variant["ttm"]._float - num) / variant["alphatm"]._float;
			}
		}

		protected void _dynamicAniTp3(float currTm, Variant aniInfo)
		{
			Variant variant = aniInfo["conf"];
			float num = currTm - aniInfo["stm"]._float;
			_graphChaSprite graphChaSprite = aniInfo["spr"]._val as _graphChaSprite;
			bool @bool = aniInfo["criatk"]._bool;
			float num2;
			if (@bool)
			{
				bool flag = num < variant["scaletm_b"]._float;
				if (flag)
				{
					num2 = num / variant["scaletm_b"]._float + 1f;
					graphChaSprite.dispObj.scale = num2;
				}
				else
				{
					bool flag2 = num < variant["scaletm_s"]._float + variant["scaletm_b"]._float;
					if (flag2)
					{
						num2 = 2f - (num - variant["scaletm_b"]._float) / variant["scaletm_s"]._float;
						graphChaSprite.dispObj.scale = num2;
					}
					else
					{
						num2 = 1f;
						graphChaSprite.dispObj.scale = num2;
						aniInfo["criatk"] = false;
					}
				}
				graphChaSprite.dispObj.x = aniInfo["stx"]._float - (num2 - 1f) * graphChaSprite.dispObj.width / 4f;
			}
			num2 = num / variant["ttm"]._float - 1f;
			graphChaSprite.dispObj.y = aniInfo["sty"]._float - 60f * (num2 * num2 * num2 + 1f);
			bool flag3 = variant["ttm"]._float - num < variant["alphatm"]._float;
			if (flag3)
			{
				graphChaSprite.dispObj.alpha = (variant["ttm"]._float - num) / variant["alphatm"]._float;
			}
		}

		public void ShowDp(bool flag)
		{
			this._dpSpr.visible = flag;
		}

		public void InitDpSpr(Variant conf, Action initCB)
		{
			this._initDpCallBack = initCB;
			this._dpNum = conf["show"].Count;
			foreach (Variant current in conf["show"].Values)
			{
				_graphChaSprite graphChaSprite = _graphChaSpriteHost.createChaSprite(current["tp"]);
				this._dpInfo[current["layer"]] = GameTools.createGroup(new object[]
				{
					"spr",
					graphChaSprite
				});
				this._dpInfo[current["layer"]]["info"] = current;
				bool flag = "txt" == current["tp"];
				if (flag)
				{
					Variant variant = current.clone();
					bool flag2 = current.ContainsKey("style");
					if (flag2)
					{
						variant["style"] = _graphChaSpriteHost.GetDisplayStyle(current["style"]);
					}
					Variant variant2 = new Variant();
					bool flag3 = current.ContainsKey("fmt");
					if (flag3)
					{
						GameTools.mergeSimpleObject(current["fmt"][0], variant2, false, true);
					}
					variant["fmt"] = variant2;
					graphChaSprite.initShowInfo(variant, new Action<_graphChaSprite>(this.initDpSprFin));
				}
				else
				{
					graphChaSprite.initShowInfo(current, new Action<_graphChaSprite>(this.initDpSprFin));
				}
			}
		}

		protected void initDpSprFin(_graphChaSprite dpSpr)
		{
			this._dpNum--;
			bool flag = this._dpNum == 0;
			if (flag)
			{
				foreach (string current in this._dpInfo.Keys)
				{
					Variant variant = this._dpInfo[current];
					Variant variant2 = variant["info"];
					_graphChaSprite graphChaSprite = variant["spr"]._val as _graphChaSprite;
					bool flag2 = variant2.ContainsKey("width");
					if (flag2)
					{
						graphChaSprite.dispObj.width = variant2["width"]._float;
					}
					bool flag3 = variant2.ContainsKey("height");
					if (flag3)
					{
						graphChaSprite.dispObj.height = variant2["height"]._float;
					}
					bool flag4 = variant2.ContainsKey("offy") && variant2["offy"]._float != 0f;
					if (flag4)
					{
						graphChaSprite.dispObj.y += variant2["offy"]._float;
					}
					bool flag5 = variant2.ContainsKey("offx") && variant2["offx"]._float != 0f;
					if (flag5)
					{
						graphChaSprite.dispObj.x += variant2["offx"]._float;
					}
					this._dpSpr.addChild(graphChaSprite.dispObj);
				}
				bool flag6 = this._initDpCallBack != null;
				if (flag6)
				{
					this._initDpCallBack();
				}
			}
		}

		protected Variant getDpAni(int idx)
		{
			string b = "ani" + idx;
			Variant result;
			foreach (Variant current in this._dpInfo.Values)
			{
				bool flag = current["info"]["id"]._str == b;
				if (flag)
				{
					result = current;
					return result;
				}
			}
			result = null;
			return result;
		}

		protected Variant getDpBar(int idx)
		{
			string b = "bar" + idx;
			Variant result;
			foreach (Variant current in this._dpInfo.Values)
			{
				bool flag = current["info"]["id"]._str == b;
				if (flag)
				{
					result = current;
					return result;
				}
			}
			result = null;
			return result;
		}

		public void UpDataDpBar(Variant info, Variant conf)
		{
			Variant dpBar = this.getDpBar(0);
			bool flag = dpBar != null;
			if (flag)
			{
				_graphChaProgressSprite graphChaProgressSprite = dpBar["spr"]._val as _graphChaProgressSprite;
				graphChaProgressSprite.maxNum = info["max"];
				graphChaProgressSprite.num = info["cur"];
			}
			bool flag2 = this._dpAniInfo == null;
			if (flag2)
			{
				this._dpAniInfo = new Variant();
			}
			this._dpAniInfo["playing"] = true;
			this._dpAniInfo["duration"] = conf["ani"]["ttm"]._float;
			this._dpAniInfo["currTm"] = 0;
			this._dpAniInfo["startVal"] = info["last"]._float / info["max"]._float;
			this._dpAniInfo["curhp"] = info["cur"];
			this._dpAniInfo["maxhp"] = info["max"];
		}

		protected void disposeDp()
		{
			bool flag = this._dpInfo.Count > 0 && this._dpSpr != null;
			if (flag)
			{
				foreach (string current in this._dpInfo.Keys)
				{
					Variant variant = this._dpInfo[current];
					this._dpSpr.removeChild((variant["spr"]._val as _graphChaSprite).dispObj, false);
					(variant["spr"]._val as _graphChaSprite).dispose();
				}
			}
			this._dpInfo = new Dictionary<string, Variant>();
			this._dpSpr.visible = false;
			this._dpSpr = null;
		}

		protected void dpAniProcess(float tm)
		{
			Variant dpAniInfo = this._dpAniInfo;
			dpAniInfo["currTm"] = dpAniInfo["currTm"] + tm * 1000f;
			float num = this._dpAniInfo["currTm"]._float / this._dpAniInfo["duration"]._float;
			bool flag = num >= 1f;
			if (flag)
			{
				this._dpAniInfo["playing"] = false;
				bool flag2 = 0f >= this._dpAniInfo["curhp"]._float || this._dpAniInfo["curhp"]._float == this._dpAniInfo["maxhp"]._float;
				if (flag2)
				{
					this._dpSpr.visible = false;
				}
			}
		}

		public void process(float timeSlice)
		{
			bool flag = this._showDynArr.Count > 0;
			if (flag)
			{
				this.dynamicAniProcess();
			}
			foreach (_graphChaSprite current in this._aniSprites)
			{
				current.update(timeSlice);
			}
			foreach (_graphChaSprite current2 in this._chatSprites)
			{
				current2.update(timeSlice);
			}
			bool flag2 = this._dpAniInfo != null && this._dpAniInfo["playing"]._bool;
			if (flag2)
			{
				this.dpAniProcess(timeSlice);
			}
		}

		public void dispose()
		{
			this._disposed = true;
			this._aniSprites = null;
			this._chatSprites = null;
			bool flag = this._titleSpr != null;
			if (flag)
			{
				this._titleSpr.dispose();
				this._titleSpr = null;
			}
			this._dynamicSpr = null;
			bool flag2 = this._titleSprites != null && this._titleSprites.Count > 0;
			if (flag2)
			{
				foreach (Variant current in this._titleSprites.Values)
				{
					foreach (Variant current2 in current["sprs"].Values)
					{
						(current2["spr"]._val as _graphChaSprite).dispose();
					}
				}
			}
			this._titleSprites = null;
			this._titleOriData = null;
			this.disposeDp();
			bool flag3 = this._groundSpr != null;
			if (flag3)
			{
				this._groundSpr.dispose();
			}
			this._groundSpr = null;
		}
	}
}
