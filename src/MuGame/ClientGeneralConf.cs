using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace MuGame
{
	public class ClientGeneralConf : configParser
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly ClientGeneralConf.<>c <>9 = new ClientGeneralConf.<>c();

			public static Comparison<Variant> <>9__14_0;

			internal int <GetTransferAllMis>b__14_0(Variant left, Variant right)
			{
				int result = 0;
				int num = int.Parse(left._str);
				int num2 = int.Parse(right._str);
				bool flag = num > num2;
				if (flag)
				{
					result = 1;
				}
				bool flag2 = num == num2;
				if (flag2)
				{
					result = 0;
				}
				bool flag3 = num < num2;
				if (flag3)
				{
					result = -1;
				}
				return result;
			}
		}

		public static ClientGeneralConf instance;

		private Variant _options;

		private Variant _achiveSort;

		private Variant _nomoveData;

		private Variant _combptExatt;

		private Variant _avaCha;

		private Variant _monAtk;

		private Variant _plyAtk;

		private Variant _npcHelpData;

		private Variant _npcMarketData;

		private Variant _npcRanShopData;

		private Variant _npcGiftData;

		private Variant _npcTransferData;

		private Variant _npcMarryData;

		private Variant _npcPkKingData;

		private Variant _npcLevelData;

		private Variant _mapLanguageConf;

		private Variant _comConfs;

		private Variant _attChangeShowArr;

		private Variant _thousandArr;

		private Variant _rotateskill = new Variant();

		private Variant _rotateFlySkill = new Variant();

		private Variant _carrSkillAction = new Variant();

		private Variant _carrDefSkill = null;

		private Variant _lotterys;

		private Variant _nobColor;

		private Variant _mapsizeinfo;

		private Variant _achiveData;

		private Variant _lvlDiffitem;

		private Variant _ltpidArr;

		protected Variant _worldBossData;

		private Variant build_data;

		private Variant _itemsFeatures;

		protected Variant _worldmaps;

		protected Variant _mapDropItems;

		private Variant _buffArr;

		private Variant _buybuffData;

		private Variant _scriptinfos;

		private Variant _itemFilter = null;

		protected Variant _autoGameConf;

		private Variant rdArr;

		protected Variant _randposConf;

		private Variant _buffDataArr;

		private Variant _bossDieEff;

		private Variant _broad_mis_awd;

		private Variant _broad_boss;

		private Variant _broad_mall_items;

		private Variant _broad_items;

		private Variant _pkKingShowInfo = new Variant();

		private Variant _pkKingNPCInfo = null;

		private Variant _hotKeyData = new Variant();

		private Variant _lines_arr;

		private Variant _mlineTipInfo = new Variant();

		private Variant _lvlnodedata = null;

		protected Variant _chaFilters = new Variant();

		private Variant _meriPosArr;

		private Variant _typeRmis;

		private Variant _RmisType;

		private Variant _clanRmis;

		private Variant _newcomer_gift = null;

		private Variant _linkInfos;

		private Variant _npcPropObj;

		protected Variant _cgoaldata;

		protected Variant _bestoneArr;

		private Variant _permanentItemsArr;

		private Variant _objAchives;

		private Variant combptConf;

		private Variant _pvipChargeData;

		private Variant _feeds;

		private Variant _maplimit;

		private Variant _levelAvatar;

		private Variant _actfestival;

		private Variant _carrAtt;

		private Variant _autoMis;

		private Variant _rideSkillPosArr;

		private Variant _bsBuffuse;

		private Variant _lvlDirIds;

		private Variant _mapAchieveInfo = new Variant();

		public Variant getmap_need_lvl
		{
			get
			{
				Variant variant = new Variant();
				for (int i = 0; i < base.conf["map_need_lvl"].Count; i++)
				{
					variant[base.conf["map_need_lvl"][i]["mapid"]._int] = base.conf["map_need_lvl"][i]["lvl"]._int;
				}
				return variant;
			}
		}

		public ClientGeneralConf(ClientConfig m) : base(m)
		{
			ClientGeneralConf.instance = this;
		}

		public static ClientGeneralConf create(IClientBase m)
		{
			return new ClientGeneralConf(m as ClientConfig);
		}

		protected override void onData()
		{
		}

		protected override Variant _formatConfig(Variant conf)
		{
			bool flag = conf.ContainsKey("carr");
			if (flag)
			{
				Variant variant = new Variant();
				foreach (Variant current in conf["carr"]._arr)
				{
					current["sex"] = GameTools.array2Map(current["sex"], "id", 1u);
					variant[current["id"]] = current;
				}
				conf["carr"] = variant;
			}
			bool flag2 = conf.ContainsKey("ltrans");
			if (flag2)
			{
				Variant variant2 = new Variant();
				foreach (Variant current2 in conf["ltrans"]._arr)
				{
					current2["t"] = GameTools.array2Map(current2["t"], "txt", 1u);
					variant2[current2["group"]] = current2;
				}
				conf["ltrans"] = variant2;
			}
			bool flag3 = conf.ContainsKey("follow");
			if (flag3)
			{
				conf["follow"] = GameTools.array2Map(conf["follow"], "id", 1u);
			}
			bool flag4 = conf.ContainsKey("hots");
			if (flag4)
			{
				conf["hots"] = GameTools.array2Map(conf["hots"], "tp", 1u);
			}
			bool flag5 = conf.ContainsKey("worldChat");
			if (flag5)
			{
				conf["worldChat"] = GameTools.array2Map(conf["worldChat"], "tp", 1u);
			}
			bool flag6 = conf.ContainsKey("chatFace");
			if (flag6)
			{
				conf["chatFace"] = GameTools.array2Map(conf["chatFace"], "tp", 1u);
			}
			bool flag7 = conf.ContainsKey("mapMusic");
			if (flag7)
			{
				conf["mapMusic"] = GameTools.array2Map(conf["mapMusic"], "mapid", 1u);
			}
			bool flag8 = conf.ContainsKey("mapsound");
			if (flag8)
			{
				conf["mapsound"] = GameTools.array2Map(conf["mapsound"], "mapid", 1u);
			}
			bool flag9 = conf.ContainsKey("normalAttsound");
			if (flag9)
			{
				conf["normalAttsound"] = GameTools.array2Map(conf["normalAttsound"], "id", 1u);
			}
			bool flag10 = conf.ContainsKey("outAttsound");
			if (flag10)
			{
				conf["outAttsound"] = GameTools.array2Map(conf["outAttsound"], "id", 1u);
			}
			bool flag11 = conf.ContainsKey("skillsound");
			if (flag11)
			{
				conf["skillsound"] = GameTools.array2Map(conf["skillsound"], "id", 1u);
			}
			bool flag12 = conf.ContainsKey("dropsound");
			if (flag12)
			{
				conf["dropsound"] = GameTools.array2Map(conf["dropsound"], "id", 1u);
			}
			bool flag13 = conf.ContainsKey("picksound");
			if (flag13)
			{
				conf["picksound"] = GameTools.array2Map(conf["picksound"], "id", 1u);
			}
			bool flag14 = conf.ContainsKey("chafilter");
			if (flag14)
			{
				conf["chafilter"] = GameTools.array2Map(conf["chafilter"], "tp", 1u);
			}
			bool flag15 = conf.ContainsKey("npcsound");
			if (flag15)
			{
				conf["npcsound"] = GameTools.array2Map(conf["npcsound"], "id", 1u);
			}
			bool flag16 = conf.ContainsKey("monsound");
			if (flag16)
			{
				conf["monsound"] = GameTools.array2Map(conf["monsound"], "id", 1u);
			}
			bool flag17 = conf.ContainsKey("monAttsound");
			if (flag17)
			{
				conf["monAttsound"] = GameTools.array2Map(conf["monAttsound"], "id", 1u);
			}
			bool flag18 = conf.ContainsKey("othersound");
			if (flag18)
			{
				conf["othersound"] = GameTools.array2Map(conf["othersound"], "id", 1u);
			}
			bool flag19 = conf.ContainsKey("createPro");
			if (flag19)
			{
				conf["createPro"] = GameTools.array2Map(conf["createPro"], "tp", 1u);
			}
			bool flag20 = conf.ContainsKey("skill");
			if (flag20)
			{
				this._rotateskill = new Variant();
				foreach (Variant current3 in conf["skill"][0]["rotateskill"]._arr)
				{
					this._rotateskill[current3["sid"]._str] = current3;
				}
				this._carrSkillAction = GameTools.array2Map(conf["skill"][0]["carr"], "carr", 1u);
			}
			bool flag21 = conf.ContainsKey("lottery");
			if (flag21)
			{
				conf["lottery"] = GameTools.array2Map(conf["lottery"], "tp", 1u);
			}
			bool flag22 = conf.ContainsKey("mapsize");
			if (flag22)
			{
				conf["mapsize"] = GameTools.array2Map(conf["mapsize"], "tp", 1u);
			}
			bool flag23 = conf.ContainsKey("mapsizeinfo");
			if (flag23)
			{
				conf["mapsizeinfo"] = GameTools.array2Map(conf["mapsizeinfo"], "tp", 1u);
			}
			bool flag24 = conf.ContainsKey("worldboss");
			if (flag24)
			{
				conf["worldboss"] = GameTools.array2Map(conf["worldboss"], "tp", 1u);
			}
			bool flag25 = conf.ContainsKey("worldmap");
			if (flag25)
			{
				conf["worldmap"] = GameTools.array2Map(conf["worldmap"], "tp", 1u);
			}
			bool flag26 = conf.ContainsKey("mapdropitems");
			if (flag26)
			{
				conf["mapdropitems"] = GameTools.array2Map(conf["mapdropitems"], "tp", 1u);
			}
			bool flag27 = conf.ContainsKey("common");
			if (flag27)
			{
				conf["common"] = GameTools.array2Map(conf["common"], "tp", 1u);
			}
			bool flag28 = conf.ContainsKey("lackprompt");
			if (flag28)
			{
				Variant value = new Variant();
				foreach (Variant current4 in conf["lackprompt"]._arr)
				{
					current4["type"] = GameTools.array2Map(current4["type"], "id", 1u);
					value = current4["type"];
				}
				conf["lackprompt"] = value;
			}
			bool flag29 = conf.ContainsKey("level");
			if (flag29)
			{
				conf["level"] = GameTools.array2Map(conf["level"], "tp", 1u);
			}
			bool flag30 = conf.ContainsKey("lvlbuff");
			if (flag30)
			{
				conf["lvlbuff"] = GameTools.array2Map(conf["lvlbuff"], "tp", 1u);
			}
			bool flag31 = conf.ContainsKey("mapLanguage");
			if (flag31)
			{
				this._mapLanguageConf = new Variant();
				foreach (Variant current5 in conf["mapLanguage"].Values)
				{
					this._mapLanguageConf[current5["mapid"]] = current5;
				}
			}
			bool flag32 = conf.ContainsKey("buybuff");
			if (flag32)
			{
				conf["buybuff"] = GameTools.array2Map(conf["buybuff"], "tp", 1u);
			}
			bool flag33 = conf.ContainsKey("mapautogame");
			if (flag33)
			{
				conf["mapautogame"] = GameTools.array2Map(conf["mapautogame"], "tp", 1u);
			}
			bool flag34 = conf.ContainsKey("randpos");
			if (flag34)
			{
				conf["randpos"] = GameTools.array2Map(conf["randpos"], "tp", 1u);
			}
			bool flag35 = conf.ContainsKey("actinfo");
			if (flag35)
			{
				conf["actinfo"] = GameTools.array2Map(conf["actinfo"][0]["type"], "id", 1u);
			}
			bool flag36 = conf.ContainsKey("itemsFeatures");
			if (flag36)
			{
				conf["itemsFeatures"] = GameTools.array2Map(conf["itemsFeatures"], "id", 1u);
			}
			bool flag37 = conf.ContainsKey("autoPoint");
			if (flag37)
			{
				Variant value2 = new Variant();
				foreach (Variant current6 in conf["autoPoint"]._arr)
				{
					current6["carr"] = GameTools.array2Map(current6["carr"], "carr", 1u);
					value2 = current6["carr"];
				}
				conf["autoPoint"] = value2;
			}
			bool flag38 = conf.ContainsKey("vipAddPoint");
			if (flag38)
			{
				Variant value3 = new Variant();
				foreach (Variant current7 in conf["vipAddPoint"]._arr)
				{
					current7["vip"] = GameTools.array2Map(current7["vip"], "tp", 1u);
					value3 = current7["vip"];
				}
				conf["vipAddPoint"] = value3;
			}
			bool flag39 = conf.ContainsKey("vipState");
			if (flag39)
			{
				Variant value4 = new Variant();
				foreach (Variant current8 in conf["vipState"]._arr)
				{
					current8["vip"] = GameTools.array2Map(current8["vip"], "lvl", 1u);
					value4 = current8["vip"];
				}
				conf["vipState"] = value4;
			}
			bool flag40 = conf.ContainsKey("tranmission");
			if (flag40)
			{
				conf["tranmission"] = GameTools.array2Map(conf["tranmission"], "id", 1u);
			}
			bool flag41 = conf.ContainsKey("NPC");
			if (flag41)
			{
				this.readNpcHelp(conf["NPC"][0]["npcHelp"]);
				this.readNpcMarket(conf["NPC"][0]["npcMarket"]);
				this.readRanShop(conf["NPC"][0]["npcRanShop"]);
				this.readGift(conf["NPC"][0]["npcGift"]);
				this.readTransfer(conf["NPC"][0]["npcTransfer"]);
				this.readMarry(conf["NPC"][0]["npcMarry"]);
				this.readPkKing(conf["NPC"][0]["npcPkKing"]);
				this.readLevel(conf["NPC"][0]["npcLevel"]);
			}
			bool flag42 = conf.ContainsKey("npcfun");
			if (flag42)
			{
				conf["npcfun"] = GameTools.array2Map(conf["npcfun"], "id", 1u);
			}
			bool flag43 = conf.ContainsKey("npcshow");
			if (flag43)
			{
				conf["npcshow"] = GameTools.array2Map(conf["npcshow"], "id", 1u);
			}
			bool flag44 = conf.ContainsKey("broad_citywar_buybuff");
			if (flag44)
			{
				conf["broad_citywar_buybuff"] = GameTools.array2Map(conf["broad_citywar_buybuff"], "tp", 1u);
			}
			bool flag45 = conf.ContainsKey("boss_die_eff");
			if (flag45)
			{
				conf["boss_die_eff"] = GameTools.array2Map(conf["boss_die_eff"], "tp", 1u);
			}
			bool flag46 = conf.ContainsKey("hotKey");
			if (flag46)
			{
				conf["hotKey"] = GameTools.array2Map(conf["hotKey"][0]["k"], "code", 1u);
			}
			bool flag47 = conf.ContainsKey("broad_rmis_desc");
			if (flag47)
			{
				conf["broad_rmis_desc"] = GameTools.array2Map(conf["broad_rmis_desc"], "tp", 1u);
			}
			bool flag48 = conf.ContainsKey("broad_boss");
			if (flag48)
			{
				conf["broad_boss"] = GameTools.array2Map(conf["broad_boss"], "tp", 1u);
			}
			bool flag49 = conf.ContainsKey("broad_mall_items");
			if (flag49)
			{
				conf["broad_mall_items"] = GameTools.array2Map(conf["broad_mall_items"], "tp", 1u);
			}
			bool flag50 = conf.ContainsKey("broad_items");
			if (flag50)
			{
				conf["broad_items"] = GameTools.array2Map(conf["broad_items"], "tp", 1u);
			}
			bool flag51 = conf.ContainsKey("dmis");
			if (flag51)
			{
				conf["dmis"] = GameTools.array2Map(conf["dmis"], "tp", 1u);
			}
			bool flag52 = conf.ContainsKey("warm_hint");
			if (flag52)
			{
				conf["warm_hint"] = GameTools.array2Map(conf["warm_hint"][0]["lvl"], "lvlid", 1u);
			}
			bool flag53 = conf.ContainsKey("ol_award");
			if (flag53)
			{
				conf["ol_award"] = GameTools.array2Map(conf["ol_award"], "gid", 1u);
			}
			bool flag54 = conf.ContainsKey("auto_buff");
			if (flag54)
			{
				conf["auto_buff"] = GameTools.array2Map(conf["auto_buff"][0]["skillbuff"], "sid", 1u);
			}
			bool flag55 = conf.ContainsKey("animation");
			if (flag55)
			{
				conf["animation"] = GameTools.array2Map(conf["animation"], "tp", 1u);
			}
			bool flag56 = conf.ContainsKey("missionIcon");
			if (flag56)
			{
				conf["missionIcon"] = GameTools.array2Map(conf["missionIcon"], "chapter", 1u);
			}
			bool flag57 = conf.ContainsKey("mlineTip");
			if (flag57)
			{
				conf["mlineTip"] = GameTools.array2Map(conf["mlineTip"], "chapter", 1u);
			}
			bool flag58 = conf.ContainsKey("mlineshow3D");
			if (flag58)
			{
				conf["mlineshow3D"] = GameTools.array2Map(conf["mlineshow3D"], "tpid", 1u);
			}
			bool flag59 = conf.ContainsKey("monatk");
			if (flag59)
			{
				this._monAtk = new Variant();
				foreach (Variant current9 in conf["monatk"]._arr)
				{
					bool flag60 = current9.ContainsKey("remote");
					if (flag60)
					{
						current9["remote"] = current9["remote"][0];
						current9["remote"]["speed"] = (double)current9["remote"]["speed"]._float / 1000.0;
					}
					this._monAtk[current9["monid"]] = current9;
				}
			}
			bool flag61 = conf.ContainsKey("plyatk");
			if (flag61)
			{
				this._plyAtk = new Variant();
				foreach (Variant current10 in conf["plyatk"]._arr)
				{
					bool flag62 = current10.ContainsKey("remote");
					if (flag62)
					{
						current10["remote"] = current10["remote"][0];
						current10["remote"]["speed"] = (double)current10["remote"]["speed"]._float / 1000.0;
					}
					this._plyAtk[current10["carr"]] = current10;
				}
			}
			bool flag63 = conf.ContainsKey("misTrackShowLevel");
			if (flag63)
			{
				conf["misTrackShowLevel"] = GameTools.array2Map(conf["misTrackShowLevel"][0]["mission"], "id", 1u);
			}
			bool flag64 = conf.ContainsKey("monsters");
			if (flag64)
			{
				conf["monsters"] = GameTools.array2Map(conf["monsters"], "tp", 1u);
			}
			bool flag65 = conf.ContainsKey("avacha");
			if (flag65)
			{
				this._avaCha = new Variant();
				foreach (Variant current11 in conf["avacha"]._arr)
				{
					this._avaCha[current11["id"]] = current11;
				}
			}
			bool flag66 = conf.ContainsKey("stateDisTip");
			if (flag66)
			{
				conf["stateDisTip"] = GameTools.array2Map(conf["stateDisTip"], "id", 1u);
			}
			bool flag67 = conf.ContainsKey("stateAddTip");
			if (flag67)
			{
				conf["stateAddTip"] = GameTools.array2Map(conf["stateAddTip"], "id", 1u);
			}
			bool flag68 = conf.ContainsKey("mapNameImg");
			if (flag68)
			{
				conf["mapNameImg"] = GameTools.array2Map(conf["mapNameImg"], "mapid", 1u);
			}
			bool flag69 = conf.ContainsKey("skillList");
			if (flag69)
			{
				conf["skillList"] = GameTools.array2Map(conf["skillList"], "carr", 1u);
			}
			bool flag70 = conf.ContainsKey("links");
			if (flag70)
			{
				conf["links"] = GameTools.array2Map(conf["links"], "tp", 1u);
			}
			bool flag71 = conf.ContainsKey("playerguide");
			if (flag71)
			{
				conf["playerguide"] = GameTools.array2Map(conf["playerguide"], "tp", 1u);
			}
			bool flag72 = conf.ContainsKey("updateboard");
			if (flag72)
			{
				conf["updateboard"] = GameTools.array2Map(conf["updateboard"], "tp", 1u);
			}
			bool flag73 = conf.ContainsKey("win_awd");
			if (flag73)
			{
				conf["win_awd"] = GameTools.array2Map(conf["win_awd"], "ltpid", 1u);
			}
			bool flag74 = conf.ContainsKey("lose_awd");
			if (flag74)
			{
				conf["lose_awd"] = GameTools.array2Map(conf["lose_awd"], "ltpid", 1u);
			}
			bool flag75 = conf.ContainsKey("acura");
			if (flag75)
			{
				conf["acura"] = GameTools.array2Map(conf["acura"], "tpid", 1u);
			}
			bool flag76 = conf.ContainsKey("mapachieve");
			if (flag76)
			{
				conf["mapachieve"] = GameTools.array2Map(conf["mapachieve"], "idx", 1u);
			}
			bool flag77 = conf.ContainsKey("achieve");
			if (flag77)
			{
				conf["achieve"] = GameTools.array2Map(conf["achieve"], "id", 1u);
			}
			bool flag78 = conf.ContainsKey("openeff");
			if (flag78)
			{
				conf["openeff"] = GameTools.array2Map(conf["openeff"], "id", 1u);
			}
			bool flag79 = conf.ContainsKey("hfactivity");
			if (flag79)
			{
				conf["hfactivity"] = GameTools.array2Map(conf["hfactivity"], "tp", 1u);
			}
			bool flag80 = conf.ContainsKey("bfactivity");
			if (flag80)
			{
				conf["bfactivity"] = GameTools.array2Map(conf["bfactivity"], "tp", 1u);
			}
			bool flag81 = conf.ContainsKey("mislinks");
			if (flag81)
			{
				conf["mislinks"] = GameTools.array2Map(conf["mislinks"][0]["mis"], "id", 1u);
			}
			bool flag82 = conf.ContainsKey("vipawd");
			if (flag82)
			{
				conf["vipawd"] = GameTools.array2Map(conf["vipawd"], "id", 1u);
			}
			bool flag83 = conf.ContainsKey("logintile");
			if (flag83)
			{
				conf["logintile"] = GameTools.array2Map(conf["logintile"], "id", 1u);
			}
			bool flag84 = conf.ContainsKey("attchangeshow");
			if (flag84)
			{
				conf["attchangeshow"] = GameTools.array2Map(conf["attchangeshow"], "tp", 1u);
			}
			bool flag85 = conf.ContainsKey("clientItem");
			if (flag85)
			{
				conf["clientItem"]["itm"] = GameTools.array2Map(conf["clientItem"][0]["itm"], "tp", 1u);
				conf["clientItem"]["item"] = GameTools.array2Map(conf["clientItem"][0]["item"], "tpid", 1u);
			}
			bool flag86 = conf.ContainsKey("misaction");
			if (flag86)
			{
				conf["misaction"] = GameTools.array2Map(conf["misaction"][0]["mis"], "misid", 1u);
			}
			bool flag87 = conf.ContainsKey("hidelvltips");
			if (flag87)
			{
				conf["hidelvltips"] = GameTools.array2Map(conf["hidelvltips"][0]["lvl"], "tpid", 1u);
			}
			bool flag88 = conf.ContainsKey("posavatar");
			if (flag88)
			{
				conf["posavatar"] = GameTools.array2Map(conf["posavatar"][0]["avt"], "stid", 1u);
			}
			bool flag89 = conf.ContainsKey("exattchain");
			if (flag89)
			{
				conf["exattchain"]["fp"] = GameTools.array2Map(conf["exattchain"][0]["fp"], "id", 1u);
				conf["exattchain"]["exatt"] = GameTools.array2Map(conf["exattchain"][0]["exatt"], "id", 1u);
				conf["exattchain"]["flvl"] = GameTools.array2Map(conf["exattchain"][0]["flvl"], "id", 1u);
				conf["exattchain"]["exattInfo"] = GameTools.array2Map(conf["exattchain"][0]["exattInfo"], "id", 1u);
				conf["exattchain"]["flvlInfo"] = GameTools.array2Map(conf["exattchain"][0]["flvlInfo"], "id", 1u);
				conf["exattchain"]["color"] = GameTools.array2Map(conf["exattchain"][0]["color"], "id", 1u);
			}
			bool flag90 = conf.ContainsKey("combptExatt");
			if (flag90)
			{
				this._combptExatt = new Variant();
				foreach (Variant current12 in conf["combptExatt"]._arr)
				{
					Variant variant3 = new Variant();
					foreach (Variant current13 in current12["grade"]._arr)
					{
						variant3[current13["lvl"]._int] = current13["val"];
					}
					this._combptExatt[current12["name"]._str] = variant3;
				}
			}
			bool flag91 = conf.ContainsKey("randshopshow");
			if (flag91)
			{
				conf["randshopshow"] = GameTools.array2Map(conf["randshopshow"][0]["show"], "tpid", 1u);
			}
			bool flag92 = conf.ContainsKey("npctopeff");
			if (flag92)
			{
				conf["npctopeff"] = GameTools.array2Map(conf["npctopeff"], "id", 1u);
			}
			bool flag93 = conf.ContainsKey("veapon");
			if (flag93)
			{
				conf["veapon"] = GameTools.array2Map(conf["veapon"], "tp", 1u);
			}
			bool flag94 = conf.ContainsKey("respawn");
			if (flag94)
			{
				Variant variant4 = GameTools.array2Map(conf["respawn"][0]["strength"], "id", 0u);
				Variant variant5 = GameTools.array2Map(conf["respawn"][0]["banlvl"], "tpid", 0u);
				conf["respawn"] = GameTools.createGroup(new Variant[]
				{
					"strength",
					variant4,
					"banlvl",
					variant5
				});
			}
			bool flag95 = conf.ContainsKey("mount");
			if (flag95)
			{
				conf["mount"] = GameTools.array2Map(conf["mount"], "qual", 1u);
			}
			bool flag96 = conf.ContainsKey("chatlayer");
			if (flag96)
			{
				conf["chatlayer"] = conf["chatlayer"][0];
			}
			bool flag97 = conf.ContainsKey("tower");
			if (flag97)
			{
				conf["tower"] = GameTools.array2Map(conf["tower"][0]["lvlmis"], "tpid", 1u);
			}
			bool flag98 = conf.ContainsKey("attackSkill");
			if (flag98)
			{
				Variant variant6 = new Variant();
				foreach (Variant current14 in conf["attackSkill"]._arr)
				{
					variant6[current14["carr"]] = GameTools.split(current14["skid"]._str, ",", 1u);
				}
				conf["attackSkill"] = variant6;
			}
			bool flag99 = conf.ContainsKey("clkuseitem");
			if (flag99)
			{
				conf["clkuseitem"] = GameTools.array2Map(conf["clkuseitem"], "tpid", 1u);
			}
			bool flag100 = conf.ContainsKey("dailyMis");
			if (flag100)
			{
				conf["dailyMis"] = GameTools.array2Map(conf["dailyMis"], "mis", 1u);
			}
			bool flag101 = conf.ContainsKey("diemove");
			if (flag101)
			{
				conf["diemove"] = GameTools.array2Map(conf["diemove"], "tp", 1u);
			}
			bool flag102 = conf.ContainsKey("mondiemove");
			if (flag102)
			{
				conf["mondiemove"] = conf["mondiemove"][0];
			}
			bool flag103 = conf.ContainsKey("screenShake");
			if (flag103)
			{
				Variant variant7 = conf["screenShake"][0]["e"];
				bool flag104 = variant7 != null;
				if (flag104)
				{
					Variant variant8 = new Variant();
					foreach (Variant current15 in variant7._arr)
					{
						variant8[current15["name"]] = current15;
					}
					conf["screenShake"] = variant8;
				}
			}
			bool flag105 = conf.ContainsKey("actpuzzle");
			if (flag105)
			{
				conf["actpuzzle"] = conf["actpuzzle"][0];
			}
			bool flag106 = conf.ContainsKey("hideAll");
			if (flag106)
			{
				conf["hideAll"] = GameTools.array2Map(conf["hideAll"][0]["lvl"], "tpid", 1u);
			}
			bool flag107 = conf.ContainsKey("openpkmode");
			if (flag107)
			{
				conf["openpkmode"] = GameTools.array2Map(conf["openpkmode"][0]["lvl"], "tpid", 1u);
			}
			bool flag108 = conf.ContainsKey("replaceAni");
			if (flag108)
			{
				conf["replaceAni"] = GameTools.array2Map(conf["replaceAni"], "cid", 1u);
				foreach (Variant current16 in conf["replaceAni"].Values)
				{
					current16["rep"] = GameTools.array2Map(current16["rep"], "ani", 1u);
				}
			}
			bool flag109 = conf.ContainsKey("clanhandle");
			if (flag109)
			{
				conf["clanhandle"] = GameTools.array2Map(conf["clanhandle"], "id", 1u);
			}
			bool flag110 = conf.ContainsKey("chatAchieve");
			if (flag110)
			{
				conf["chatAchieve"] = GameTools.array2Map(conf["chatAchieve"], "id", 1u);
			}
			bool flag111 = conf.ContainsKey("quiz");
			if (flag111)
			{
				conf["quiz"] = GameTools.array2Map(conf["quiz"], "id", 1u);
			}
			bool flag112 = conf.ContainsKey("mislinktips");
			if (flag112)
			{
				conf["mislinktips"] = GameTools.array2Map(conf["mislinktips"][0]["mis"], "id", 1u);
			}
			bool flag113 = conf.ContainsKey("reminditm");
			if (flag113)
			{
				conf["reminditm"] = GameTools.array2Map(conf["reminditm"], "tpid", 1u);
			}
			bool flag114 = conf.ContainsKey("weekgoal");
			if (flag114)
			{
				conf["weekgoal"] = GameTools.array2Map(conf["weekgoal"], "id", 1u);
			}
			bool flag115 = conf.ContainsKey("resetlevel");
			if (flag115)
			{
				conf["resetlevel"] = GameTools.array2Map(conf["resetlevel"], "lvl", 1u);
			}
			bool flag116 = conf.ContainsKey("weekgoalcarr");
			if (flag116)
			{
				conf["weekgoalcarr"] = GameTools.array2Map(conf["weekgoalcarr"], "carr", 1u);
			}
			bool flag117 = conf.ContainsKey("itmpkgs");
			if (flag117)
			{
				conf["itmpkgs"] = GameTools.array2Map(conf["itmpkgs"], "id", 1u);
			}
			bool flag118 = conf.ContainsKey("dropitmpkg");
			if (flag118)
			{
				conf["dropitmpkg"] = GameTools.array2Map(conf["dropitmpkg"][0]["dtpid"], "id", 1u);
			}
			bool flag119 = conf.ContainsKey("multiPosKil");
			if (flag119)
			{
				conf["multiPosKil"] = GameTools.array2Map(conf["multiPosKil"], "id", 1u);
			}
			bool flag120 = conf.ContainsKey("expdouble");
			if (flag120)
			{
				conf["expdouble"] = conf["expdouble"][0];
			}
			bool flag121 = conf.ContainsKey("monori");
			if (flag121)
			{
				conf["monori"] = GameTools.array2Map(conf["monori"][0]["mon"], "mid", 1u);
			}
			bool flag122 = conf.ContainsKey("options");
			if (flag122)
			{
				conf["options"] = GameTools.array2Map(conf["options"][0]["option"], "id", 1u);
			}
			bool flag123 = conf.ContainsKey("ownermon");
			if (flag123)
			{
				conf["ownermon"] = GameTools.array2Map(conf["ownermon"], "mid", 1u);
			}
			bool flag124 = conf.ContainsKey("transferitm");
			if (flag124)
			{
				conf["transferitm"] = GameTools.array2Map(conf["transferitm"], "carr", 1u);
			}
			bool flag125 = conf.ContainsKey("itemRecipe");
			if (flag125)
			{
				conf["itemRecipe"] = GameTools.array2Map(conf["itemRecipe"][0]["info"], "needid", 1u);
			}
			bool flag126 = conf.ContainsKey("comboard");
			if (flag126)
			{
				conf["comboard"] = GameTools.array2Map(conf["comboard"], "tpid", 1u);
			}
			bool flag127 = conf.ContainsKey("doubleAttDesc");
			if (flag127)
			{
				conf["doubleAttDesc"] = GameTools.array2Map(conf["doubleAttDesc"][0]["item"], "tpid", 1u);
			}
			bool flag128 = conf.ContainsKey("showcolor");
			if (flag128)
			{
				conf["showcolor"] = GameTools.array2Map(conf["showcolor"], "type", 1u);
			}
			bool flag129 = conf.ContainsKey("shopFilter");
			if (flag129)
			{
				conf["shopFilter"] = GameTools.array2Map(conf["shopFilter"], "lvl", 1u);
				foreach (Variant current17 in conf["shopFilter"].Values)
				{
					string str = current17["items"];
					Variant value5 = GameTools.split(str, ",", 1u);
					current17["items"] = value5;
				}
			}
			bool flag130 = conf.ContainsKey("rateItem");
			if (flag130)
			{
				conf["rateItm"] = GameTools.array2Map(conf["rateItm"], "itmid", 1u);
			}
			bool flag131 = conf.ContainsKey("mulitCompose");
			if (flag131)
			{
				conf["mulitCompose"] = GameTools.array2Map(conf["mulitCompose"], "id", 1u);
				foreach (Variant current18 in conf["mulitCompose"].Values)
				{
					current18["carr"] = GameTools.array2Map(current18["carr"], "id", 1u);
					foreach (Variant current19 in current18["carr"].Values)
					{
						current19["recipe"] = GameTools.split(current19["recipe"]._str, ",", 1u);
					}
				}
			}
			bool flag132 = conf.ContainsKey("rideskillimg");
			if (flag132)
			{
				conf["rideskillimg"] = GameTools.array2Map(conf["rideskillimg"], "qual", 1u);
			}
			bool flag133 = conf.ContainsKey("checkGroup");
			if (flag133)
			{
				conf["checkGroup"] = GameTools.array2Map(conf["checkGroup"], "id", 1u);
			}
			bool flag134 = conf.ContainsKey("crosswar");
			if (flag134)
			{
				conf["crosswar"] = GameTools.array2Map(conf["crosswar"], "id", 1u);
			}
			bool flag135 = conf.ContainsKey("fashion");
			if (flag135)
			{
				conf["fashion"] = GameTools.array2Map(conf["fashion"], "id", 1u);
			}
			bool flag136 = conf.ContainsKey("decompgrade");
			if (flag136)
			{
				conf["decompgrade"] = GameTools.array2Map(conf["decompgrade"], "id", 1u);
			}
			bool flag137 = conf.ContainsKey("hasRateItm");
			if (flag137)
			{
				conf["hasRateItm"] = GameTools.array2Map(conf["hasRateItm"], "itmid", 1u);
			}
			bool flag138 = conf.ContainsKey("crosswarAchieve");
			if (flag138)
			{
				conf["crosswarAchieve"] = conf["crosswarAchieve"][0];
			}
			bool flag139 = conf.ContainsKey("boxId");
			if (flag139)
			{
				conf["boxId"] = GameTools.array2Map(conf["boxId"], "id", 1u);
			}
			bool flag140 = conf.ContainsKey("mapeffect");
			if (flag140)
			{
				conf["mapeffect"] = GameTools.array2Map(conf["mapeffect"], "lpid", 1u);
				foreach (Variant current20 in conf["mapeffect"].Values)
				{
					current20["blockZone"] = GameTools.array2Map(current20["blockZone"], "id", 1u);
				}
			}
			bool flag141 = conf.ContainsKey("lvlNeedHide");
			if (flag141)
			{
				conf["lvlNeedHide"] = GameTools.array2Map(conf["lvlNeedHide"], "id", 1u);
			}
			bool flag142 = conf.ContainsKey("monwarn");
			if (flag142)
			{
				conf["monwarn"] = GameTools.array2Map(conf["monwarn"], "sid", 1u);
			}
			bool flag143 = conf.ContainsKey("redPaper");
			if (flag143)
			{
				conf["redPaper"] = GameTools.array2Map(conf["redPaper"], "id", 1u);
			}
			bool flag144 = conf.ContainsKey("mapstateff");
			if (flag144)
			{
				conf["mapstateff"] = GameTools.array2Map(conf["mapstateff"], "sid", 1u);
			}
			bool flag145 = conf.ContainsKey("systemset");
			if (flag145)
			{
				conf["systemset"] = GameTools.array2Map(conf["systemset"], "tp", 1u);
			}
			bool flag146 = conf.ContainsKey("secondInterface");
			if (flag146)
			{
				conf["secondInterface"] = GameTools.array2Map(conf["secondInterface"][0]["face"], "oid", 1u);
			}
			bool flag147 = conf.ContainsKey("levelhall");
			if (flag147)
			{
				conf["levelhall"] = GameTools.array2Map(conf["levelhall"], "id", 1u);
			}
			bool flag148 = conf.ContainsKey("transcriptinfo");
			if (flag148)
			{
				conf["transcriptinfo"] = GameTools.array2Map(conf["transcriptinfo"], "tpid", 1u);
			}
			return conf;
		}

		public Variant GetMapstatEff(uint sid)
		{
			bool flag = this.m_conf.ContainsKey("mapstateff");
			Variant result;
			if (flag)
			{
				result = this.m_conf["mapstateff"][sid];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetRedPaper(uint sid)
		{
			bool flag = this.m_conf.ContainsKey("redPaper");
			Variant result;
			if (flag)
			{
				result = this.m_conf["redPaper"][sid];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetMonWarningEff(uint sid)
		{
			bool flag = this.m_conf.ContainsKey("monwarn");
			Variant result;
			if (flag)
			{
				result = this.m_conf["monwarn"][sid];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public uint GetBoxId(uint id)
		{
			uint result = 0u;
			foreach (Variant current in this.m_conf["boxId"]._arr)
			{
				Variant variant = GameTools.split(current["id"], ",", 1u);
				for (int i = 0; i < variant.Count; i++)
				{
					bool flag = id == variant[i];
					if (flag)
					{
						result = current["type"];
						break;
					}
				}
			}
			return result;
		}

		public Variant GetDecompGradeConf()
		{
			bool flag = this.m_conf.ContainsKey("decompgrade");
			Variant result;
			if (flag)
			{
				result = this.m_conf["decompgrade"][0];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetDoubleAttDescConf(uint tpid)
		{
			bool flag = this.m_conf.ContainsKey("doubleAttDesc");
			Variant result;
			if (flag)
			{
				result = this.m_conf["doubleAttDesc"][tpid];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetComboardConf(uint tpid)
		{
			bool flag = this.m_conf.ContainsKey("comboard");
			Variant result;
			if (flag)
			{
				result = this.m_conf["comboard"][tpid];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetItemRecipe(uint needid)
		{
			bool flag = this.m_conf.ContainsKey("itemRecipe");
			Variant result;
			if (flag)
			{
				result = this.m_conf["itemRecipe"][needid];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetTransferItem(int carr)
		{
			bool flag = this.m_conf.ContainsKey("transferitm");
			Variant result;
			if (flag)
			{
				result = this.m_conf["transferitm"][carr.ToString()];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetTransferAllMis(int carr)
		{
			Variant variant = new Variant();
			Variant transferItem = this.GetTransferItem(carr);
			bool flag = transferItem != null && transferItem["item"] != null;
			if (flag)
			{
				foreach (Variant current in transferItem["item"]._arr)
				{
					bool flag2 = current["misid"] != null;
					if (flag2)
					{
						Variant variant2 = GameTools.split(current["misid"]._str, ",", 1u);
						variant._arr.AddRange(variant2._arr);
					}
				}
			}
			List<Variant> arg_D7_0 = variant._arr;
			Comparison<Variant> arg_D7_1;
			if ((arg_D7_1 = ClientGeneralConf.<>c.<>9__14_0) == null)
			{
				arg_D7_1 = (ClientGeneralConf.<>c.<>9__14_0 = new Comparison<Variant>(ClientGeneralConf.<>c.<>9.<GetTransferAllMis>b__14_0));
			}
			arg_D7_0.Sort(arg_D7_1);
			return variant;
		}

		public Variant GetOwnerMonConf(int mid)
		{
			bool flag = this.m_conf.ContainsKey("ownermon");
			Variant result;
			if (flag)
			{
				result = this.m_conf["ownermon"][mid.ToString()];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetPlatOption()
		{
			bool flag = this._options == null;
			if (flag)
			{
				this._options = new Variant();
			}
			bool flag2 = this.m_conf.ContainsKey("options");
			if (flag2)
			{
				foreach (Variant current in this.m_conf["options"]._arr)
				{
					Variant variant = GameTools.split(current["plat"]._str, ",", 1u);
					for (int i = 0; i < variant.Count; i++)
					{
						bool flag3 = variant[i] == "";
						if (flag3)
						{
						}
					}
					GameTools.createGroup(new Variant[]
					{
						"id",
						current["id"],
						"plat",
						variant,
						"open",
						current["open"]
					});
				}
			}
			return this._options;
		}

		public Variant GetMonOriConf(int mid)
		{
			bool flag = this.m_conf.ContainsKey("monori");
			Variant result;
			if (flag)
			{
				result = this.m_conf["monori"][mid.ToString()];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public string GetExpDoubleTime()
		{
			bool flag = this.m_conf.ContainsKey("expdouble");
			string result;
			if (flag)
			{
				result = this.m_conf["expdouble"]["time"];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetDropItmpkg(uint dpid)
		{
			bool flag = this.m_conf.ContainsKey("dropitmpkg");
			Variant result;
			if (flag)
			{
				result = this.m_conf["dropitmpkg"][dpid];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetItmpkgsByType(string type)
		{
			bool flag = this.m_conf.ContainsKey("itmpkgs");
			Variant result;
			if (flag)
			{
				foreach (Variant current in this.m_conf["itmpkgs"]._arr)
				{
					bool flag2 = current["type"] == type;
					if (flag2)
					{
						result = current["item"];
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public Variant GetWeekGoalByCarr(uint carr)
		{
			bool flag = this.m_conf.ContainsKey("weekgoalcarr");
			Variant result;
			if (flag)
			{
				Variant variant = this.m_conf["weekgoalcarr"][carr];
				Variant variant2 = GameTools.createGroup(new Variant[]
				{
					variant["id"]._str,
					","
				});
				result = variant2;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetWeekGoalById(uint id)
		{
			bool flag = this.m_conf.ContainsKey("weekgoal");
			Variant result;
			if (flag)
			{
				result = this.m_conf["weekgoal"][id];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetWeekGoalByName(uint id, string name)
		{
			bool flag = this.m_conf.ContainsKey("weekgoal");
			Variant result;
			if (flag)
			{
				Variant variant = this.m_conf["weekgoal"][id]["goal"];
				foreach (Variant current in variant._arr)
				{
					bool flag2 = current["name"] == name;
					if (flag2)
					{
						result = current;
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public Variant GetRemindItem(uint tpid)
		{
			bool flag = this.m_conf.ContainsKey("reminditm");
			Variant result;
			if (flag)
			{
				result = this.m_conf["reminditm"][tpid];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetMisLinkTips(int id)
		{
			bool flag = this.m_conf.ContainsKey("mislinktips");
			Variant result;
			if (flag)
			{
				result = this.m_conf["mislinktips"][id];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetChatAchieveShow(int id)
		{
			bool flag = this.m_conf.ContainsKey("chatAchieve");
			Variant result;
			if (flag)
			{
				Variant variant = this.m_conf["chatAchieve"][0]["show"];
				foreach (Variant current in variant.Values)
				{
					bool flag2 = current["id"] == id;
					if (flag2)
					{
						result = current;
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public Variant GetChatAchieveSort()
		{
			bool flag = this._achiveSort == null && this.m_conf.ContainsKey("chatAchieve");
			if (flag)
			{
				bool flag2 = this.m_conf["chatAchieve"]["0"].ContainsKey("sort");
				if (flag2)
				{
					string str = this.m_conf["chatAchieve"]["0"]["sort"];
					Variant variant = GameTools.split(str, ",", 1u);
					for (int i = 0; i < variant.Count; i++)
					{
						variant[i] = variant[i];
					}
					this._achiveSort = variant;
				}
			}
			return this._achiveSort;
		}

		public Variant GetClanHandleConf()
		{
			return base.conf["clanhandle"];
		}

		public Variant GetOpenPKModeConf(int ltpid)
		{
			bool flag = this.m_conf.ContainsKey("openpkmode");
			Variant result;
			if (flag)
			{
				result = this.m_conf["openpkmode"][ltpid];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetHideAllConf(int ltpid)
		{
			bool flag = this.m_conf.ContainsKey("hideAll");
			Variant result;
			if (flag)
			{
				result = this.m_conf["hideAll"][ltpid];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetActPuzzleRecipe()
		{
			bool flag = this.m_conf["actpuzzle"].ContainsKey("comp");
			Variant result;
			if (flag)
			{
				result = this.m_conf["actpuzzle"]["comp"];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetActPuzzleItmConf()
		{
			bool flag = this.m_conf["actpuzzle"].ContainsKey("itm");
			Variant result;
			if (flag)
			{
				result = this.m_conf["actpuzzle"]["itm"];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public string GetMapLinkEff(int mapid, int go_to)
		{
			bool flag = this.m_conf.ContainsKey("linkEffect");
			string result;
			if (flag)
			{
				foreach (Variant current in this.m_conf["linkEffect"]._arr)
				{
					bool flag2 = current["mapid"]._int == mapid;
					if (flag2)
					{
						foreach (Variant current2 in current["map"]._arr)
						{
							bool flag3 = current2["goto"]._int == go_to;
							if (flag3)
							{
								result = current2["effID"];
								return result;
							}
						}
					}
				}
			}
			result = "";
			return result;
		}

		public Variant GetShakeConf(string id)
		{
			return this.m_conf["screenShake"][id];
		}

		public Variant GetRandomDieMove(int dir)
		{
			bool flag = this.m_conf["diemove"];
			Variant result;
			if (flag)
			{
				int count = this.m_conf["diemove"].Count;
				int random = ConfigUtil.getRandom(0, count);
				result = this.GetDieMove(random, dir);
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetDieMove(int tp, int dir)
		{
			Variant variant = this.m_conf["diemove"][tp];
			bool flag = variant;
			Variant result;
			if (flag)
			{
				foreach (Variant current in variant["move"]._arr)
				{
					bool flag2 = current["dir"]._int == dir;
					if (flag2)
					{
						result = current;
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public Variant GetMonDieMove(int mid, int dir)
		{
			Variant variant = this.m_conf["diemove"];
			Variant result;
			foreach (Variant current in variant._arr)
			{
				bool flag = current["mid"] == mid;
				if (flag)
				{
					result = this.GetDieMove(current["movetp"], dir);
					return result;
				}
			}
			result = null;
			return result;
		}

		public bool IsNoMove(int mid)
		{
			bool flag = !this._nomoveData;
			if (flag)
			{
				this._nomoveData = new Variant();
				bool flag2 = this.m_conf["mondiemove"] != null && this.m_conf["mondiemove"]["nomove"] != null;
				if (flag2)
				{
					Variant variant = this.m_conf["mondiemove"]["nomove"][0];
					string str = variant["mids"]._str;
					Variant variant2 = GameTools.split(str, ",", 1u);
					using (List<Variant>.Enumerator enumerator = variant2._arr.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text = enumerator.Current;
							this._nomoveData[text] = int.Parse(text);
						}
					}
				}
			}
			return this._nomoveData[mid];
		}

		public Variant GetClkUseItemConf(uint tpid)
		{
			bool flag = this.m_conf.ContainsKey("clkuseitem");
			Variant result;
			if (flag)
			{
				result = this.m_conf["clkuseitem"][tpid];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetTowerLvlMis(uint ltpid)
		{
			return this.m_conf["tower"][ltpid];
		}

		public Variant GetChatLayer(int layer)
		{
			return this.m_conf["chatlayer"]["layer"][layer];
		}

		public int ChatMaxLayer()
		{
			return this.m_conf["chatlayer"]["max"];
		}

		public int GetMiniChatLayer()
		{
			Variant variant = this.m_conf["chatlayer"]["layer"];
			int result = 0;
			foreach (Variant current in variant._arr)
			{
				bool flag = current["mini"] != null;
				if (flag)
				{
					result = current["tp"];
					break;
				}
			}
			return result;
		}

		public int GetRespawnStrengthPageLen()
		{
			int num = 0;
			foreach (Variant current in this.m_conf["respawn"]["strength"]._arr)
			{
				bool flag = current["itm"] != null;
				if (flag)
				{
					num++;
				}
			}
			return num;
		}

		public Variant GetRespawnStrength(int id)
		{
			Variant variant = this.m_conf["respawn"]["strength"][id];
			bool flag = variant != null;
			Variant result;
			if (flag)
			{
				result = variant["itm"];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public bool GetRespawnBanLvl(int tpid)
		{
			Variant variant = this.m_conf["respawn"]["banlvl"][tpid];
			return variant != null;
		}

		public Variant GetNpcTopEff(int id)
		{
			return this.m_conf["npctopeff"][id];
		}

		public Variant GetRandShopShow(int tpid)
		{
			Variant result;
			foreach (string current in this.m_conf["randshopshow"].Keys)
			{
				bool flag = this.m_conf["randshopshow"][current]["tpid"]._int == tpid;
				if (flag)
				{
					result = this.m_conf["randshopshow"][current];
					return result;
				}
			}
			result = null;
			return result;
		}

		public Variant GetRandShopShowIdArr()
		{
			Variant variant = new Variant();
			using (Dictionary<string, Variant>.KeyCollection.Enumerator enumerator = this.m_conf["randshopshow"].Keys.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Variant val = enumerator.Current;
					variant.pushBack(this.m_conf["randshopshow"][val ?? ""]["tpid"]._int);
				}
			}
			return (variant.Length > 0) ? variant : null;
		}

		public Variant GetPosAvatar(int stid)
		{
			return this.m_conf["posavatar"][stid];
		}

		public Variant GetExattchainAtt(int id)
		{
			return this.m_conf["exattchain"]["exatt"][id];
		}

		public Variant GetExattchainFlvl(int id)
		{
			return this.m_conf["exattchain"]["flvl"][id];
		}

		public Variant GetExattchainAttInfo(int id)
		{
			return this.m_conf["exattchain"]["exattInfo"][id];
		}

		public Variant GetExattchainFlvlInfo(int id)
		{
			return this.m_conf["exattchain"]["flvlInfo"][id];
		}

		public Variant GetExattchainColor(int id)
		{
			return this.m_conf["exattchain"]["color"][id];
		}

		public Variant GetHideLvlTips(int tpid)
		{
			return this.m_conf["hidelvltips"][tpid];
		}

		public bool is_need_show_hidetips(int tpid)
		{
			Variant hideLvlTips = this.GetHideLvlTips(tpid);
			return hideLvlTips != null;
		}

		public Variant GetMisAction(int misid)
		{
			return this.m_conf["misaction"][misid];
		}

		public Variant GetClientItem(int id)
		{
			return this.m_conf["clientItem"]["itm"][id];
		}

		public Variant GetLoginTile()
		{
			return this.m_conf["logintile"];
		}

		public Variant GetClientVipAwd(int id)
		{
			return this.m_conf["vipawd"][id.ToString()]["itm"];
		}

		public int GetBuyVipId(int id)
		{
			return this.m_conf["vipawd"][id.ToString()]["tpid"]._int;
		}

		private void readNpcHelp(Variant node)
		{
			this._npcHelpData = new Variant();
			int num = (node[0]["npcid"]._str == "") ? 0 : node[0]["npcid"]._int;
			bool flag = this._npcHelpData[num] != null;
			if (flag)
			{
				this._npcHelpData[num]["desc"]._arr.Add(node[0]["desc"]._str);
			}
			else
			{
				Variant variant = new Variant();
				variant._arr.Add(node[0]["desc"]._str);
				this._npcHelpData[num] = new Variant();
				this._npcHelpData[num]["npcid"] = num;
				this._npcHelpData[num]["name"] = node[0]["name"]._str;
				this._npcHelpData[num]["desc"] = variant;
			}
		}

		public Variant get_npcHelpData(int npcid)
		{
			bool flag = !this._npcHelpData.ContainsKey(npcid);
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this._npcHelpData[npcid];
			}
			return result;
		}

		private void readNpcMarket(Variant node)
		{
			this._npcMarketData = new Variant();
			string str = node[0]["npcid"]._str;
			Variant variant = GameTools.split(str, ",", 1u);
			using (List<Variant>.Enumerator enumerator = variant._arr.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string text = enumerator.Current;
					this._npcMarketData[text] = text;
				}
			}
		}

		public Variant get_npcMarketData(int npcid)
		{
			return this._npcMarketData[npcid];
		}

		private void readRanShop(Variant node)
		{
			this._npcRanShopData = new Variant();
			string str = node[0]["npcid"]._str;
			Variant variant = GameTools.split(str, ",", 1u);
			using (List<Variant>.Enumerator enumerator = variant._arr.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string text = enumerator.Current;
					this._npcRanShopData[text] = text;
				}
			}
		}

		public Variant get_npcRanShopData(int npcid)
		{
			return this._npcRanShopData[npcid];
		}

		private void readGift(Variant node)
		{
			this._npcGiftData = new Variant();
			string str = node[0]["npcid"]._str;
			Variant variant = GameTools.split(str, ",", 1u);
			using (List<Variant>.Enumerator enumerator = variant._arr.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string text = enumerator.Current;
					this._npcGiftData[text] = text;
				}
			}
		}

		public Variant get_npcGiftData(int npcid)
		{
			return this._npcGiftData[npcid];
		}

		private void readTransfer(Variant node)
		{
			this._npcTransferData = new Variant();
			string str = node[0]["npcid"];
			Variant variant = GameTools.split(str, ",", 1u);
			using (List<Variant>.Enumerator enumerator = variant._arr.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string text = enumerator.Current;
					this._npcTransferData[text] = text;
				}
			}
		}

		public Variant get_npcTransferData(int npcid)
		{
			return this._npcTransferData[npcid];
		}

		private void readMarry(Variant node)
		{
			this._npcMarryData = new Variant();
			string str = node[0]["npcid"];
			Variant variant = GameTools.split(str, ",", 1u);
			using (List<Variant>.Enumerator enumerator = variant._arr.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string text = enumerator.Current;
					this._npcMarryData[text] = text;
				}
			}
		}

		public Variant get_npcMarryData(int npcid)
		{
			return this._npcMarryData[npcid];
		}

		private void readPkKing(Variant node)
		{
			this._npcPkKingData = new Variant();
			string str = node[0]["npcid"];
			Variant variant = GameTools.split(str, ",", 1u);
			using (List<Variant>.Enumerator enumerator = variant._arr.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string text = enumerator.Current;
					this._npcPkKingData[text] = text;
				}
			}
		}

		public Variant get_npcPkKingData(int npcid)
		{
			return this._npcPkKingData[npcid];
		}

		private void readLevel(Variant node)
		{
			this._npcLevelData = new Variant();
			foreach (Variant current in node._arr)
			{
				this._npcLevelData[current["tpid"]] = current;
				bool flag = current.ContainsKey("linknpc");
				if (flag)
				{
					Variant value = GameTools.split(current["linknpc"], ",", 0u);
					this._npcLevelData[current["tpid"]]["linknpc"] = value;
				}
			}
		}

		public Variant GetNpcLevelData(uint ltpid, int npcid)
		{
			Variant variant = this._npcLevelData[ltpid.ToString()];
			bool flag = variant != null && variant["level"] != null;
			Variant result;
			if (flag)
			{
				foreach (Variant current in variant["level"].Values)
				{
					bool flag2 = current["npcid"]._int == npcid;
					if (flag2)
					{
						result = current;
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public Variant GetNpcLevelConf(uint ltpid)
		{
			return this._npcLevelData[ltpid];
		}

		public Variant GetCarrConfig(uint carr, uint sex)
		{
			bool flag = this.m_conf["carr"][carr.ToString()] == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Variant variant = this.m_conf["carr"][carr.ToString()];
				result = variant["sex"][sex.ToString()];
			}
			return result;
		}

		public Variant GetCommonConf(string id)
		{
			bool flag = this._comConfs != null;
			Variant result;
			if (flag)
			{
				result = this._comConfs[id];
			}
			else
			{
				this._comConfs = new Variant();
				Variant variant = base.conf["common"]["0"]["cfg"];
				foreach (Variant current in variant._arr)
				{
					this._comConfs[current["id"]] = current["val"];
				}
				result = this._comConfs[id];
			}
			return result;
		}

		public int IsAttChangeShow(string att)
		{
			bool flag = this._attChangeShowArr == null;
			if (flag)
			{
				this._attChangeShowArr = new Variant();
				foreach (string current in base.conf["attchangeshow"].Keys)
				{
					Variant value = GameTools.split(base.conf["attchangeshow"][current]._str, ",", 1u);
					this._attChangeShowArr[current] = value;
				}
			}
			int result;
			foreach (string current2 in this._attChangeShowArr.Keys)
			{
				bool flag2 = this._attChangeShowArr[current2]._str.IndexOf(att) > -1;
				if (flag2)
				{
					result = int.Parse(current2);
					return result;
				}
			}
			result = -1;
			return result;
		}

		private void initThousand()
		{
			bool flag = this._thousandArr == null;
			if (flag)
			{
				this._thousandArr = new Variant();
				Variant commonConf = this.GetCommonConf("thousand_Str");
				string str = commonConf._str;
				this._thousandArr = GameTools.split(str, ",", 1u);
			}
		}

		public float changethousandtohundred(string name, float val)
		{
			this.initThousand();
			float result;
			using (List<Variant>.Enumerator enumerator = this._thousandArr._arr.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string b = enumerator.Current;
					bool flag = name == b;
					if (flag)
					{
						float num = val / 10f;
						result = num;
						return result;
					}
				}
			}
			result = val;
			return result;
		}

		public bool IsThousand(string name)
		{
			this.initThousand();
			return this._thousandArr._str.IndexOf(name) > -1;
		}

		private Variant getLangtransRepText(string group, string txt)
		{
			Variant variant = base.conf["ltrans"];
			bool flag = variant != null;
			Variant result;
			if (flag)
			{
				Variant variant2 = variant[group];
				bool flag2 = variant2 != null;
				if (flag2)
				{
					Variant variant3 = variant2["t"][txt];
					bool flag3 = variant3 != null;
					if (flag3)
					{
						result = variant3["rept"];
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public string FormatSuperTextXML(string str, string fmtStr)
		{
			string text = "text=\"" + str + "\" ";
			bool flag = fmtStr != null;
			if (flag)
			{
				Variant variant = GameTools.split(fmtStr, ",", 1u);
				using (List<Variant>.Enumerator enumerator = variant._arr.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string str2 = enumerator.Current;
						Variant variant2 = GameTools.split(str2, "=", 1u);
						bool flag2 = variant2.Length == 2;
						if (flag2)
						{
							string str3 = variant2[0]._str;
							uint num = <PrivateImplementationDetails>.ComputeStringHash(str3);
							if (num <= 2052402371u)
							{
								if (num <= 1196563446u)
								{
									if (num != 109274769u)
									{
										if (num != 194001507u)
										{
											if (num == 1196563446u)
											{
												if (str3 == "sp")
												{
													text = text + "letterSpacing=\"" + variant2[1];
													goto IL_4A5;
												}
											}
										}
										else if (str3 == "clk")
										{
											text = text + "onclick=\"" + variant2[1]._str;
											goto IL_4A5;
										}
									}
									else if (str3 == "ckp")
									{
										text = text + "clickpar=\"" + variant2[1]._str;
										goto IL_4A5;
									}
								}
								else if (num <= 1297229160u)
								{
									if (num != 1263673922u)
									{
										if (num == 1297229160u)
										{
											if (str3 == "sz")
											{
												text = text + "size=\"" + variant2[1]._str;
												goto IL_4A5;
											}
										}
									}
									else if (str3 == "st")
									{
										text = text + "style=\"" + variant2[1]._str;
										goto IL_4A5;
									}
								}
								else if (num != 1721890582u)
								{
									if (num == 2052402371u)
									{
										if (str3 == "vrp")
										{
											text = text + "overpar=\"" + variant2[1]._str;
											goto IL_4A5;
										}
									}
								}
								else if (str3 == "evp")
								{
									text = text + "eventpar=\"" + variant2[1]._str;
									goto IL_4A5;
								}
							}
							else if (num <= 3203343032u)
							{
								if (num != 2870621791u)
								{
									if (num != 3071511934u)
									{
										if (num == 3203343032u)
										{
											if (str3 == "fmt")
											{
												text = text + "format=\"" + variant2[1]._str;
												goto IL_4A5;
											}
										}
									}
									else if (str3 == "ovr")
									{
										text = text + "onover=\"" + variant2[1]._str;
										goto IL_4A5;
									}
								}
								else if (str3 == "out")
								{
									text = text + "onout=\"" + variant2[1]._str;
									goto IL_4A5;
								}
							}
							else if (num <= 3859557458u)
							{
								if (num != 3239582314u)
								{
									if (num == 3859557458u)
									{
										if (str3 == "c")
										{
											Variant langtransRepText = this.getLangtransRepText("c", variant2[1]._str);
											bool flag3 = langtransRepText != null;
											if (flag3)
											{
												text = text + "color=\"0x" + langtransRepText._str;
											}
											else
											{
												text = text + "color=\"" + variant2[1]._str;
											}
											goto IL_4A5;
										}
									}
								}
								else if (str3 == "otp")
								{
									text = text + "outpar=\"" + variant2[1]._str;
									goto IL_4A5;
								}
							}
							else if (num != 3876335077u)
							{
								if (num == 4027333648u)
								{
									if (str3 == "u")
									{
										text = text + "underline=\"" + variant2[1]._str;
										goto IL_4A5;
									}
								}
							}
							else if (str3 == "b")
							{
								text = text + "bold=\"" + variant2[1]._str;
								goto IL_4A5;
							}
							continue;
							IL_4A5:
							text += "\" ";
						}
						else
						{
							bool flag4 = variant2.Length > 2;
							if (flag4)
							{
								string str4 = variant2[0]._str;
								bool flag5 = "ckp" == str4;
								if (flag5)
								{
									text += "clickpar=\"";
									int length = variant2.Length;
									for (int i = 1; i < length; i++)
									{
										text += variant2[i]._str;
										bool flag6 = i < length - 1;
										if (flag6)
										{
											text += "=";
										}
									}
									text += "\" ";
								}
							}
						}
					}
				}
			}
			return "<txt " + text + "/>";
		}

		public string TransToSuperText(string str)
		{
			bool flag = str == null;
			string result;
			if (flag)
			{
				result = str;
			}
			else
			{
				Regex regex = new Regex("{[^<>\\{\\}\\[\\]\\(\\)\\(\\)]+}");
				Match match = regex.Match(str);
				bool flag2 = match == null || match.ToString() == "";
				if (flag2)
				{
					Regex regex2 = new Regex("<(.*?)\\/>");
					Match match2 = regex2.Match(str);
					bool flag3 = match2 != null && match2.ToString() != "";
					if (flag3)
					{
						result = str;
					}
					else
					{
						result = this.FormatSuperTextXML(str, null);
					}
				}
				else
				{
					string text = "";
					string fmtStr = null;
					while (match != null && match.ToString() != "")
					{
						bool flag4 = match.Index > 0;
						if (flag4)
						{
							text += this.FormatSuperTextXML(str.Substring(0, match.Index), fmtStr);
						}
						string text2 = match.ToString();
						str = str.Substring(match.Index + text2.Length);
						text2 = text2.Substring(1, text2.Length - 2);
						string a = text2;
						if (!(a == "end"))
						{
							if (!(a == "br"))
							{
								fmtStr = text2;
							}
							else
							{
								text += "<br/>";
							}
						}
						else
						{
							fmtStr = null;
						}
						regex = new Regex("{[^<>\\{\\}\\[\\]\\(\\)\\(\\)]+}");
						match = regex.Match(str, 0);
					}
					bool flag5 = str.Length > 0;
					if (flag5)
					{
						text += this.FormatSuperTextXML(str, fmtStr);
					}
					result = text;
				}
			}
			return result;
		}

		public Variant GetFollowConf(uint id)
		{
			Variant variant = base.conf["follow"];
			bool flag = variant != null;
			Variant result;
			if (flag)
			{
				result = variant[id.ToString()];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant getPlayerCarrStruct(uint carr, uint carrlvl)
		{
			bool flag = !base.conf["carr"].ContainsKey(carr.ToString());
			Variant result;
			if (flag)
			{
				result = "";
			}
			else
			{
				Variant variant = base.conf["carr"][carr.ToString()];
				bool flag2 = variant["carrName"][(int)carrlvl] == null;
				if (flag2)
				{
					result = base.conf["carr"]["name"];
				}
				else
				{
					result = variant["carrName"][(int)carrlvl];
				}
			}
			return result;
		}

		public string getPlayerHeadImgPath(uint uCarr, uint uCarrlvl = 0u)
		{
			bool flag = base.conf.ContainsKey("headImg");
			string result;
			if (flag)
			{
				Variant variant = base.conf["headImg"];
				for (int i = 0; i < variant[0]["img"].Count; i++)
				{
					bool flag2 = variant[0]["img"][i]["carr"]._uint == uCarr;
					if (flag2)
					{
						result = variant[0]["img"][i]["content"];
						return result;
					}
				}
			}
			result = "";
			return result;
		}

		public string getNotificationImgPath(string strNotifyType)
		{
			bool flag = base.conf.ContainsKey("notification");
			string result;
			if (flag)
			{
				Variant variant = base.conf["notification"];
				foreach (Variant current in variant[0]["item"]._arr)
				{
					bool flag2 = current["type"]._str == strNotifyType;
					if (flag2)
					{
						result = current["path"]._str;
						return result;
					}
				}
			}
			result = "";
			return result;
		}

		public Variant get_compose_conf()
		{
			bool flag = base.conf == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = base.conf["compose"];
			}
			return result;
		}

		public Variant GetItemComposeBytype(int recipe, int type)
		{
			Variant result;
			foreach (Variant current in base.conf["compose"]._arr)
			{
				Variant variant = current["items"];
				foreach (Variant current2 in variant._arr)
				{
					Variant variant2 = current2["tp"]._arr[0]["info"];
					foreach (Variant current3 in variant2._arr)
					{
						bool flag = current3["recipe"]._int == recipe && current3["type"]._int == type;
						if (flag)
						{
							result = variant2;
							return result;
						}
					}
				}
			}
			result = null;
			return result;
		}

		public Variant GetItemComposeRecipe(uint tpid, int comp = 0)
		{
			Variant result;
			foreach (Variant current in base.conf["itemRecipe"].Values)
			{
				bool flag = comp != 0;
				if (flag)
				{
					bool flag2 = current["needid"]._uint == tpid && current.ContainsKey("comp") && current["comp"]._int != 0;
					if (flag2)
					{
						result = current;
						return result;
					}
				}
				else
				{
					bool flag3 = comp == 0 && current["needid"]._uint == tpid && (!current.ContainsKey("comp") || current["comp"]._int == 0);
					if (flag3)
					{
						result = current;
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public Variant get_actinfo_conf()
		{
			bool flag = base.conf == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = base.conf["actinfo"];
			}
			return result;
		}

		public Variant GetConfByTypeId(string type, uint id)
		{
			bool flag = base.conf == null || !base.conf["actinfo"].ContainsKey(type);
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Variant variant = base.conf["actinfo"][type]["act"];
				foreach (Variant current in variant._arr)
				{
					bool flag2 = current["id"]._uint == id;
					if (flag2)
					{
						result = current;
						return result;
					}
				}
				result = null;
			}
			return result;
		}

		public Variant get_hotsell_conf()
		{
			bool flag = base.conf == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = base.conf["hots"];
			}
			return result;
		}

		public Variant get_worldChat_conf()
		{
			bool flag = base.conf == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = base.conf["worldChat"];
			}
			return result;
		}

		public Variant get_chatFace_conf()
		{
			bool flag = base.conf == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = base.conf["chatFace"];
			}
			return result;
		}

		public Variant getMapMusic(uint mpid)
		{
			Variant variant = base.conf["mapMusic"];
			bool flag = variant != null;
			Variant result;
			if (flag)
			{
				result = variant[mpid.ToString()];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant getMapSound(uint mpid)
		{
			Variant variant = base.conf["mapsound"];
			bool flag = variant != null;
			Variant result;
			if (flag)
			{
				result = variant[mpid.ToString()];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant getMonSound(uint id)
		{
			Variant variant = base.conf["monsound"];
			bool flag = variant != null;
			Variant result;
			if (flag)
			{
				result = variant[id.ToString()];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant getMonAttSound(string id)
		{
			bool flag = base.conf.ContainsKey("monAttsound") && base.conf["monAttsound"] != null;
			Variant result;
			if (flag)
			{
				result = base.conf["monAttsound"][id.ToString()];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public string getNormalAttSound(uint id)
		{
			bool flag = base.conf.ContainsKey("normalAttsound") && base.conf["normalAttsound"] != null;
			string result;
			if (flag)
			{
				bool flag2 = !base.conf["normalAttsound"].ContainsKey(id.ToString());
				if (flag2)
				{
					result = "";
				}
				else
				{
					result = base.conf["normalAttsound"][id.ToString()]["sid"]._str;
				}
			}
			else
			{
				result = "";
			}
			return result;
		}

		public string getOutAttSound(uint id)
		{
			bool flag = base.conf.ContainsKey("outAttsound") && base.conf["outAttsound"] != null;
			string result;
			if (flag)
			{
				bool flag2 = !base.conf["outAttsound"].ContainsKey(id.ToString());
				if (flag2)
				{
					result = "";
				}
				else
				{
					result = base.conf["outAttsound"][id.ToString()]["sid"]._str;
				}
			}
			else
			{
				result = "";
			}
			return result;
		}

		public string getPickSound(string id)
		{
			bool flag = base.conf.ContainsKey("picksound") && base.conf["picksound"] != null;
			string result;
			if (flag)
			{
				bool flag2 = !base.conf["picksound"].ContainsKey(id.ToString());
				if (flag2)
				{
					result = "";
				}
				else
				{
					result = base.conf["picksound"][id.ToString()]["sid"]._str;
				}
			}
			else
			{
				result = "";
			}
			return result;
		}

		public string getDropSound(string id)
		{
			bool flag = base.conf.ContainsKey("dropsound") && base.conf["dropsound"] != null;
			string result;
			if (flag)
			{
				bool flag2 = !base.conf["dropsound"].ContainsKey(id.ToString());
				if (flag2)
				{
					result = "";
				}
				else
				{
					result = base.conf["dropsound"][id.ToString()]["sid"]._str;
				}
			}
			else
			{
				result = "";
			}
			return result;
		}

		public string getSkillSound(uint id)
		{
			bool flag = base.conf.ContainsKey("skillsound") && base.conf["skillsound"] != null;
			string result;
			if (flag)
			{
				bool flag2 = !base.conf["skillsound"].ContainsKey(id.ToString());
				if (flag2)
				{
					result = "";
				}
				else
				{
					result = base.conf["skillsound"][id.ToString()]["sid"]._str;
				}
			}
			else
			{
				result = "";
			}
			return result;
		}

		public string getNpcSound(uint id)
		{
			bool flag = base.conf.ContainsKey("npcsound") && base.conf["npcsound"] != null;
			string result;
			if (flag)
			{
				bool flag2 = !base.conf["npcsound"].ContainsKey(id.ToString());
				if (flag2)
				{
					result = "";
				}
				else
				{
					result = base.conf["npcsound"][id.ToString()]["sid"]._str;
				}
			}
			else
			{
				result = "";
			}
			return result;
		}

		public float getNpcRadius(uint id)
		{
			bool flag = base.conf.ContainsKey("npcsound") && base.conf["npcsound"] != null;
			float result;
			if (flag)
			{
				bool flag2 = !base.conf["npcsound"].ContainsKey(id.ToString());
				if (flag2)
				{
					result = 0f;
				}
				else
				{
					result = base.conf["npcsound"][id.ToString()]["radius"]._float;
				}
			}
			else
			{
				result = 0f;
			}
			return result;
		}

		public Variant getTranMission(uint id)
		{
			bool flag = base.conf["tranmission"] != null;
			Variant result;
			if (flag)
			{
				bool flag2 = !base.conf["tranmission"].ContainsKey(id.ToString());
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = base.conf["tranmission"][id.ToString()];
				}
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetMisLinks()
		{
			return this.m_conf["mislinks"];
		}

		public Variant GetMisLinksById(uint misid)
		{
			return this.m_conf["mislinks"][misid];
		}

		public Variant GetOpenEff(string id)
		{
			return this.m_conf["openeff"][id];
		}

		public Variant getRotateSkill(uint sid)
		{
			return this._rotateskill[sid.ToString()];
		}

		public bool IsRotateFlySkill(uint sid)
		{
			bool result;
			for (int i = 0; i < this._rotateFlySkill.Length; i++)
			{
				bool flag = this._rotateFlySkill[i] == sid;
				if (flag)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public Variant get_carrSkillAction(int carr)
		{
			return this._carrSkillAction[carr.ToString()];
		}

		public Variant GetManualSkill()
		{
			Variant variant = null;
			bool flag = base.conf.ContainsKey("skill");
			Variant result;
			if (flag)
			{
				variant = new Variant();
				Variant variant2 = base.conf["skill"][0]["manualskill"];
				string str = variant2[0]["ids"]._str;
				Variant variant3 = GameTools.split(str, ",", 1u);
				for (int i = 0; i < variant3.Length; i++)
				{
					variant._arr.Add(variant3[i]._uint);
				}
				result = variant;
			}
			else
			{
				result = variant;
			}
			return result;
		}

		public Variant GetCarrDefSkill(int carr)
		{
			bool flag = this._carrDefSkill == null;
			if (flag)
			{
				this._carrDefSkill = new Variant();
				bool flag2 = base.conf.ContainsKey("skill") && base.conf["skill"][0].ContainsKey("defskill");
				if (flag2)
				{
					foreach (Variant current in base.conf["skill"][0]["defskill"]._arr)
					{
						this._carrDefSkill[current["carr"]._str] = GameTools.split(current["ids"]._str, ",", 0u);
					}
				}
			}
			return this._carrDefSkill.ContainsKey(carr.ToString()) ? this._carrDefSkill[carr.ToString()] : null;
		}

		public bool IsDefSkill(int carr, uint sid)
		{
			Variant carrDefSkill = this.GetCarrDefSkill(carr);
			bool flag = carrDefSkill != null;
			bool result;
			if (flag)
			{
				for (int i = 0; i < carrDefSkill.Count; i++)
				{
					bool flag2 = carrDefSkill[i]._uint == sid;
					if (flag2)
					{
						result = true;
						return result;
					}
				}
			}
			result = false;
			return result;
		}

		public Variant GetlotterysByIdx(int id)
		{
			bool flag = this._lotterys != null;
			Variant result;
			if (flag)
			{
				result = this._lotterys[id];
			}
			else
			{
				bool flag2 = base.conf.ContainsKey("lottery");
				if (flag2)
				{
					this._lotterys = new Variant();
					Variant variant = base.conf["lottery"]["0"]["ltr"];
					foreach (Variant current in variant._arr)
					{
						this._lotterys._arr.Add(current);
					}
					result = this._lotterys[id];
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		public Variant getLotteryUseItems()
		{
			bool flag = base.conf.ContainsKey("lottery");
			Variant result;
			if (flag)
			{
				bool flag2 = base.conf["lottery"]["0"].ContainsKey("showTip");
				if (flag2)
				{
					Variant variant = new Variant();
					foreach (Variant current in base.conf["lottery"]["0"]["showTip"]._arr)
					{
						variant[current["id"]] = current["tpid"];
					}
					result = variant;
					return result;
				}
			}
			result = null;
			return result;
		}

		public Variant GetNobilityColor(int id)
		{
			bool flag = this._nobColor == null;
			if (flag)
			{
				this._nobColor = new Variant();
				bool flag2 = base.conf != null && base.conf["nobilityColor"] != null;
				if (flag2)
				{
					Variant variant = base.conf["nobilityColor"];
					foreach (Variant current in variant._arr)
					{
						this.getNobilityColor(current);
					}
				}
			}
			return this._nobColor[id.ToString()];
		}

		private void getNobilityColor(Variant data)
		{
			bool flag = data != null;
			if (flag)
			{
				string str = data["lvl"]._str;
				bool flag2 = str == "";
				if (!flag2)
				{
					Variant variant = GameTools.split(str, ",", 1u);
					int length = variant.Length;
					for (int i = 0; i < length; i++)
					{
						int num = variant[i];
						bool flag3 = !this._nobColor.ContainsKey(num.ToString()) || this._nobColor[num.ToString() == null];
						if (flag3)
						{
							this._nobColor[num.ToString()] = new Variant();
						}
						this._nobColor[num.ToString()]["color"] = data["color"];
						bool flag4 = data.ContainsKey("star");
						if (flag4)
						{
							this._nobColor[num.ToString()]["star"] = data["star"]._bool;
						}
						else
						{
							this._nobColor[num.ToString()]["star"] = false;
						}
					}
				}
			}
		}

		public Variant getMapsize()
		{
			Variant variant = base.conf["mapsize"]["0"];
			int @int = variant["size"][0]["width"]._int;
			int int2 = variant["size"][0]["height"]._int;
			Variant variant2 = new Variant();
			variant2["width"] = @int;
			variant2["height"] = int2;
			return variant2;
		}

		public Variant getMapsizeinfo(uint mapId)
		{
			bool flag = this._mapsizeinfo != null;
			Variant result;
			if (flag)
			{
				result = this._mapsizeinfo[mapId.ToString()];
			}
			else
			{
				this._mapsizeinfo = new Variant();
				Variant variant = base.conf["mapsizeinfo"]["0"]["map"];
				foreach (Variant current in variant._arr)
				{
					int @int = current["id"]._int;
					this._mapsizeinfo[@int.ToString()] = current;
				}
				result = this._mapsizeinfo[mapId.ToString()];
			}
			return result;
		}

		public Variant getAchiveData(int id)
		{
			bool flag = this._achiveData == null;
			if (flag)
			{
				this._achiveData = new Variant();
				bool flag2 = base.conf != null && base.conf.ContainsKey("achiveInfo") && base.conf["achiveInfo"] != null;
				if (flag2)
				{
					Variant variant = base.conf["achiveInfo"];
					foreach (Variant current in variant._arr)
					{
						this._achiveData[current["id"]] = current;
					}
				}
			}
			return this._achiveData[id.ToString()];
		}

		public Variant getLvlDiff(int tpid)
		{
			bool flag = this._lvlDiffitem == null;
			if (flag)
			{
				this.setlvlDiff();
			}
			return this._lvlDiffitem[tpid.ToString()];
		}

		private void setlvlDiff()
		{
			this._lvlDiffitem = new Variant();
			bool flag = base.conf["level"]["0"].ContainsKey("lvldiff");
			if (flag)
			{
				this._lvlDiffitem = GameTools.array2Map(base.conf["level"]["0"]["lvldiff"], "id", 1u);
			}
		}

		public bool NeedShowRank(uint ltpid)
		{
			bool flag = this._ltpidArr == null;
			if (flag)
			{
				this._ltpidArr = new Variant();
				Variant commonConf = this.GetCommonConf("lvl_unshow_rank");
				string str = commonConf._str;
				this._ltpidArr = GameTools.split(str, ",", 1u);
			}
			bool result;
			using (Dictionary<string, Variant>.ValueCollection.Enumerator enumerator = this._ltpidArr.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string s = enumerator.Current;
					bool flag2 = (ulong)ltpid == (ulong)((long)int.Parse(s));
					if (flag2)
					{
						result = false;
						return result;
					}
				}
			}
			result = true;
			return result;
		}

		public bool is_lvl_need_hideUI(uint current_lvl)
		{
			Variant commonConf = this.GetCommonConf("lvl_need_hideUI");
			string str = commonConf._str;
			Variant variant = GameTools.split(str, ",", 1u);
			bool result;
			foreach (Variant current in variant._arr)
			{
				bool flag = uint.Parse(current._str) == current_lvl;
				if (flag)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public bool IsShowLvlDiffHeapGold(int tpid, int diff)
		{
			Variant lvlDiff = this.getLvlDiff(tpid);
			bool flag = lvlDiff == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Variant variant = lvlDiff["diff"][diff.ToString()];
				bool flag2 = variant == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = variant["isSGold"]._int == 1;
					result = flag3;
				}
			}
			return result;
		}

		public Variant getLvlDiffItems(int tpid, int diff)
		{
			Variant lvlDiff = this.getLvlDiff(tpid);
			bool flag = lvlDiff != null;
			Variant result;
			if (flag)
			{
				Variant variant = lvlDiff["diff"][diff];
				bool flag2 = variant != null;
				if (flag2)
				{
					result = variant["items"];
					return result;
				}
			}
			result = null;
			return result;
		}

		public bool IsShowLvlDiffItems(int tpid, int diff)
		{
			Variant lvlDiff = this.getLvlDiff(tpid);
			bool flag = lvlDiff == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Variant variant = lvlDiff["diff"][diff];
				bool flag2 = variant == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = variant.ContainsKey("isShowItem") && variant["isShowItem"]._int == 1;
					result = flag3;
				}
			}
			return result;
		}

		public Variant GetLvlAwdsConf(int tpid, int diff)
		{
			Variant lvlDiff = this.getLvlDiff(tpid);
			bool flag = lvlDiff == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				foreach (Variant current in lvlDiff["diff"]._arr)
				{
					bool flag2 = current["id"]._int == diff;
					if (flag2)
					{
						result = current;
						return result;
					}
				}
				result = null;
			}
			return result;
		}

		public bool IsShowLvlDiffHeapExp(int tpid, int diff)
		{
			Variant lvlDiff = this.getLvlDiff(tpid);
			bool flag = lvlDiff == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Variant variant = lvlDiff["diff"][diff];
				bool flag2 = variant == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = variant["isSExp"]._int == 1;
					result = flag3;
				}
			}
			return result;
		}

		protected void initWorldBosData(int carr)
		{
			this._worldBossData = new Variant();
			Variant variant = base.conf["worldboss"]["0"]["boss"];
			foreach (Variant current in variant._arr)
			{
				int @int = current["mid"]._int;
				string str = current["desc2"]._str;
				this._worldBossData[@int.ToString()] = current;
				this._worldBossData[@int.ToString()]["items"] = this.getBossDesc2(@int, carr);
			}
		}

		private Variant getBossDesc2(int bossid, int carr)
		{
			Variant variant = new Variant();
			bool flag = base.conf["worldbossDrop"] != null;
			if (flag)
			{
				Variant variant2 = base.conf["worldbossDrop"][0]["boss"];
				foreach (Variant current in variant2._arr)
				{
					bool flag2 = current["mid"] == bossid;
					if (flag2)
					{
						foreach (Variant current2 in current["item"]._arr)
						{
							bool flag3 = !current2.ContainsKey("carr") || current2["carr"]._int == carr;
							if (flag3)
							{
								variant._arr.Add(current2);
							}
						}
						break;
					}
				}
			}
			return variant;
		}

		private Variant translateStr(string str)
		{
			Variant variant = GameTools.split(str, ",", 1u);
			bool flag = variant.Length == 0;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Variant variant2 = new Variant();
				result = variant2;
			}
			return result;
		}

		public Variant getWorldBossConfById(uint bossid, int carr)
		{
			bool flag = this._worldBossData == null;
			if (flag)
			{
				this.initWorldBosData(carr);
			}
			return this._worldBossData[bossid.ToString()];
		}

		public Variant getWorldBossConfByMapId(uint mpid, int carr)
		{
			Variant variant = new Variant();
			bool flag = this._worldBossData == null;
			if (flag)
			{
				this.initWorldBosData(carr);
			}
			foreach (Variant current in this._worldBossData.Values)
			{
				bool flag2 = current.ContainsKey("mapid") && current["mapid"]._uint == mpid;
				if (flag2)
				{
					variant._arr.Add(current);
				}
			}
			return variant;
		}

		public Variant getWorldBossConfByLtpid(uint mapid, uint ltpid, uint diff, int carr)
		{
			Variant variant = new Variant();
			bool flag = this._worldBossData == null;
			if (flag)
			{
				this.initWorldBosData(carr);
			}
			foreach (Variant current in this._worldBossData._arr)
			{
				bool flag2 = current.ContainsKey("mapid") && current.ContainsKey("ltpid") && current.ContainsKey("diff");
				if (flag2)
				{
					bool flag3 = current["mapid"]._uint == mapid && current["ltpid"]._uint == ltpid && current["diff"]._uint == diff;
					if (flag3)
					{
						variant._arr.Add(current);
					}
				}
			}
			return variant;
		}

		public bool NoteBossInfo(uint ltpid, uint mid, int carr)
		{
			Variant variant = new Variant();
			bool flag = this._worldBossData == null;
			if (flag)
			{
				this.initWorldBosData(carr);
			}
			bool result;
			foreach (Variant current in this._worldBossData._arr)
			{
				bool flag2 = current.ContainsKey("ltpid") && current.ContainsKey("mid");
				if (flag2)
				{
					bool flag3 = current["ltpid"]._uint == ltpid && current["mid"]._uint == mid;
					if (flag3)
					{
						result = true;
						return result;
					}
				}
			}
			result = false;
			return result;
		}

		public uint GetBuildIndex(uint mapid, uint mid)
		{
			bool flag = this.build_data == null;
			if (flag)
			{
				this.setBuildData();
			}
			Variant variant = this.build_data[mapid.ToString()];
			bool flag2 = variant != null;
			uint result;
			if (flag2)
			{
				foreach (Variant current in variant._arr)
				{
					bool flag3 = current["mid"]._uint == mid;
					if (flag3)
					{
						result = current["idx"]._uint;
						return result;
					}
				}
			}
			result = 0u;
			return result;
		}

		private void setBuildData()
		{
			this.build_data = new Variant();
			bool flag = base.conf.ContainsKey("buildIndex");
			if (flag)
			{
				bool flag2 = base.conf["buildIndex"][0].ContainsKey("map");
				if (flag2)
				{
					foreach (Variant current in base.conf["buildIndex"][0]["map"]._arr)
					{
						this.build_data[current["mapid"]] = current["mon"];
					}
				}
			}
		}

		private void readitemsFeatures()
		{
			this._itemsFeatures = new Variant();
			bool flag = base.conf.ContainsKey("features");
			if (flag)
			{
				foreach (Variant current in base.conf["features"]._arr)
				{
					string str = current["type"]._str;
					string str2 = current["tpid"]._str;
					string str3 = current["mktp"]._str;
					Variant variant = new Variant();
					Variant value = GameTools.split(str2, ",", 1u);
					Variant value2 = GameTools.split(str3, ",", 1u);
					variant["type"] = str;
					variant["items"] = value;
					variant["mktps"] = value2;
					this._itemsFeatures[str] = variant;
				}
			}
		}

		public uint get_itemId_byFeatures(string type)
		{
			bool flag = this._itemsFeatures == null;
			if (flag)
			{
				this.readitemsFeatures();
			}
			Variant variant = this._itemsFeatures[type];
			bool flag2 = variant == null;
			uint result;
			if (flag2)
			{
				result = 0u;
			}
			else
			{
				Variant variant2 = variant["items"];
				bool flag3 = variant2 != null && variant2.Length > 0;
				if (flag3)
				{
					result = variant2[0];
				}
				else
				{
					result = 0u;
				}
			}
			return result;
		}

		public Variant GetItemsByFeatures(string type)
		{
			bool flag = this._itemsFeatures == null;
			if (flag)
			{
				this.readitemsFeatures();
			}
			Variant variant = this._itemsFeatures[type];
			bool flag2 = variant != null;
			Variant result;
			if (flag2)
			{
				Variant variant2 = variant["items"];
				bool flag3 = variant2 != null && variant2.Length > 0;
				if (flag3)
				{
					result = variant2;
					return result;
				}
			}
			result = null;
			return result;
		}

		public Variant GetScriptItem(int tpid, int diff_lvl)
		{
			Variant variant = base.conf["level"]["0"]["lvldiff"];
			Variant result;
			foreach (Variant current in variant._arr)
			{
				bool flag = current["id"]._int == tpid;
				if (flag)
				{
					Variant variant2 = current["diff"];
					foreach (Variant current2 in variant2._arr)
					{
						bool flag2 = current2["id"]._int == diff_lvl;
						if (flag2)
						{
							result = current2;
							return result;
						}
					}
				}
			}
			result = null;
			return result;
		}

		public Variant getAllWorldmap()
		{
			bool flag = this._worldmaps != null;
			Variant worldmaps;
			if (flag)
			{
				worldmaps = this._worldmaps;
			}
			else
			{
				bool flag2 = base.conf.ContainsKey("worldmap");
				if (flag2)
				{
					this._worldmaps = base.conf["worldmap"]["0"]["wm"];
				}
				worldmaps = this._worldmaps;
			}
			return worldmaps;
		}

		public Variant getWorldMapConfByMapid(int mpid)
		{
			bool flag = this._worldmaps == null;
			if (flag)
			{
				this.getAllWorldmap();
			}
			Variant result;
			foreach (Variant current in this._worldmaps._arr)
			{
				bool flag2 = current["mapid"]._int == mpid;
				if (flag2)
				{
					result = current;
					return result;
				}
			}
			result = null;
			return result;
		}

		public Variant getWorldMapDropItems()
		{
			bool flag = this._mapDropItems != null;
			Variant mapDropItems;
			if (flag)
			{
				mapDropItems = this._mapDropItems;
			}
			else
			{
				bool flag2 = base.conf.ContainsKey("worldmap");
				if (flag2)
				{
					this._mapDropItems = base.conf["mapdropitems"]["0"]["map"];
				}
				mapDropItems = this._mapDropItems;
			}
			return mapDropItems;
		}

		public Variant getWorldmapDropItem(int mapid)
		{
			Variant variant = new Variant();
			bool flag = this._mapDropItems == null;
			if (flag)
			{
				this.getWorldMapDropItems();
			}
			Variant result;
			foreach (Variant current in this._mapDropItems._arr)
			{
				bool flag2 = current["id"]._int == mapid;
				if (flag2)
				{
					result = current;
					return result;
				}
			}
			result = variant;
			return result;
		}

		public Variant getPromptConf()
		{
			bool flag = base.conf == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = base.conf["lackprompt"];
			}
			return result;
		}

		public Variant lvl_need_buy_buff(int ltpid)
		{
			Variant variant = this._getBuffArr();
			Variant result;
			foreach (Variant current in variant._arr)
			{
				bool flag = current["ids"]._str.IndexOf(ltpid.ToString()) != -1;
				if (flag)
				{
					result = current;
					return result;
				}
			}
			result = null;
			return result;
		}

		private Variant _getBuffArr()
		{
			bool flag = this._buffArr == null;
			if (flag)
			{
				this._buffArr = new Variant();
				Variant variant = new Variant();
				Variant variant2 = base.conf["lvlbuff"][0];
				bool flag2 = variant2 != null && variant2["lvl"];
				if (flag2)
				{
					variant = variant2["lvl"];
				}
				foreach (Variant current in variant._arr)
				{
					Variant value = GameTools.split(current["id"]._str, ",", 1u);
					current["ids"] = value;
					this._buffArr._arr.Add(current);
				}
			}
			return this._buffArr;
		}

		public Variant GetBuyBuff(string type)
		{
			bool flag = this._buybuffData == null;
			if (flag)
			{
				this._buybuffData = new Variant();
				Variant variant = base.conf["buybuff"][0]["buy"];
				bool flag2 = variant && variant.Length > 0;
				if (flag2)
				{
					foreach (string current in variant[0].Keys)
					{
						Variant value = GameTools.split(variant[0][current]._str, ",", 1u);
						this._buybuffData[current] = value;
					}
				}
			}
			bool flag3 = this._buybuffData;
			Variant result;
			if (flag3)
			{
				result = this._buybuffData[type];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetScriptInfo()
		{
			bool flag = this._scriptinfos == null;
			if (flag)
			{
				Variant variant = base.conf["scriptdropitem"][0];
				bool flag2 = variant;
				if (flag2)
				{
					this._scriptinfos = variant["lvl"];
				}
			}
			return this._scriptinfos;
		}

		private void readFastSellAtt(Variant node)
		{
			this._itemFilter = new Variant();
			foreach (Variant current in node._arr)
			{
				Variant variant = new Variant();
				variant["attchk"] = this.readAttSubNode(current["attchk"]);
				variant["cfgchk"] = this.readAttSubNode(current["cfgchk"]);
				this._itemFilter[current["tp"]] = variant;
			}
		}

		private Variant readAttSubNode(Variant nodeList)
		{
			Variant variant = new Variant();
			bool flag = nodeList != null;
			if (flag)
			{
				foreach (Variant current in nodeList._arr)
				{
					Variant variant2 = new Variant();
					string str = current["fun"]._str;
					variant2["fun"] = str;
					variant2["name"] = current["name"]._str;
					bool flag2 = "match" == str || "notmatch" == str;
					if (flag2)
					{
						string str2 = current["val"]._str;
						Variant variant3 = GameTools.split(str2, ",", 1u);
						Variant variant4 = new Variant();
						using (List<Variant>.Enumerator enumerator2 = variant3._arr.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								string s = enumerator2.Current;
								variant4._arr.Add(int.Parse(s));
							}
						}
						variant2["val"] = variant4;
					}
					else
					{
						variant2["val"] = current["val"]._int;
					}
					variant._arr.Add(variant2);
				}
			}
			return variant;
		}

		public Variant GetFastSellAtt(uint tp = 0u)
		{
			bool flag = this._itemFilter == null;
			if (flag)
			{
				this.readFastSellAtt(base.conf["eqpattchk"][0]["itemFilter"]);
			}
			return this._itemFilter[tp.ToString()];
		}

		public Variant GetItemCheck()
		{
			bool flag = this._itemFilter == null;
			if (flag)
			{
				this.readFastSellAtt(base.conf["eqpattchk"][0]["itemFilter"]);
			}
			return this._itemFilter;
		}

		protected void _initAutoGame()
		{
			bool flag = this._autoGameConf != null;
			if (!flag)
			{
				this._autoGameConf = new Variant();
				Variant variant = base.conf["mapautogame"]["0"]["map"];
				foreach (Variant current in variant._arr)
				{
					this._autoGameConf[current["id"]] = current;
				}
			}
		}

		public Variant getAutoGameConfByMapid(int mpid)
		{
			bool flag = this._autoGameConf == null;
			if (flag)
			{
				this._initAutoGame();
			}
			return this._autoGameConf[mpid.ToString()];
		}

		public Variant getAutoGameConf()
		{
			bool flag = this._autoGameConf == null;
			if (flag)
			{
				this._initAutoGame();
			}
			return this._autoGameConf;
		}

		public string GetRandPos(string id)
		{
			bool flag = this._randposConf == null;
			if (flag)
			{
				this._initRandpos();
			}
			Variant variant = this._randposConf[id];
			bool flag2 = variant;
			string result;
			if (flag2)
			{
				Variant variant2 = variant["pos"];
				bool flag3 = variant2.Length > 0;
				if (flag3)
				{
					foreach (Variant current in variant2._arr)
					{
						bool flag4 = current.ContainsKey("checks");
						if (flag4)
						{
						}
						bool flag5 = this.rdArr == null;
						if (flag5)
						{
							this.rdArr = new Variant();
						}
						this.rdArr._arr.Add(current);
					}
					bool flag6 = this.rdArr && this.rdArr.Length > 0;
					if (flag6)
					{
						Variant variant3 = new Variant();
						int length = this.rdArr.Length;
						bool flag7 = 1 == length;
						if (flag7)
						{
							variant3 = this.rdArr[0];
						}
						else
						{
							Random random = new Random();
							int idx = random.Next(0, length);
							variant3 = this.rdArr[idx];
						}
						result = string.Concat(new object[]
						{
							variant3["mapid"]._int,
							"_",
							variant3["x"]._int,
							"_",
							variant3["y"]._int
						});
						return result;
					}
				}
			}
			result = "";
			return result;
		}

		protected void _initRandpos()
		{
			bool flag = this._randposConf != null;
			if (!flag)
			{
				this._randposConf = new Variant();
				Variant variant = base.conf["randpos"]["0"]["auto"];
				foreach (Variant current in variant._arr)
				{
					this._randposConf[current["id"]] = current;
				}
			}
		}

		public Variant getRandPosConfById(int id)
		{
			bool flag = this._randposConf == null;
			if (flag)
			{
				this._initRandpos();
			}
			return this._randposConf[id.ToString()];
		}

		public Variant getAutoPointDataById(int id)
		{
			bool flag = base.conf != null;
			Variant result;
			if (flag)
			{
				result = base.conf["autoPoint"][id.ToString()];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant getVipAddPointData()
		{
			bool flag = base.conf != null;
			Variant result;
			if (flag)
			{
				result = base.conf["vipAddPoint"];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant getVipStateData(int lvl)
		{
			bool flag = base.conf != null;
			Variant result;
			if (flag)
			{
				result = base.conf["vipState"][lvl.ToString()];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetNpcFunData(int npcid)
		{
			return base.conf["npcfun"][npcid.ToString()];
		}

		public Variant GetNpc3Dshow(int npcid)
		{
			bool flag = base.conf.ContainsKey("npcshow") && base.conf["npcshow"].ContainsKey(npcid.ToString());
			Variant result;
			if (flag)
			{
				result = base.conf["npcshow"][npcid.ToString()];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant Getnobfile()
		{
			bool flag = base.conf.ContainsKey("nobfile");
			Variant result;
			if (flag)
			{
				result = base.conf["nobfile"];
			}
			else
			{
				result = null;
			}
			return result;
		}

		private void readBroadCitywarBuybuff(Variant data)
		{
			this._buffDataArr = new Variant();
			Variant variant = data[0]["buff"];
			foreach (Variant current in variant._arr)
			{
				string languageText = LanguagePack.getLanguageText("chat", current["desc"]._str);
				current["desc"] = languageText;
				this._buffDataArr._arr.Add(current);
			}
		}

		public string get_buff_desc_bytpid(string id, string lvl)
		{
			bool flag = this._buffDataArr == null;
			if (flag)
			{
				this.readBroadCitywarBuybuff(base.conf["broad_citywar_buybuff"]);
			}
			string result;
			foreach (Variant current in this._buffDataArr._arr)
			{
				bool flag2 = current["id"]._str == id && current["lvl"]._str == lvl;
				if (flag2)
				{
					result = current["desc"];
					return result;
				}
			}
			result = "";
			return result;
		}

		private void readBossDieEffFun(Variant data)
		{
			this._bossDieEff = new Variant();
			Variant variant = data[0]["bcase"];
			foreach (Variant current in variant._arr)
			{
				string languageText = LanguagePack.getLanguageText("chat", current["msg"]._str);
				current["msg"] = languageText;
				this._bossDieEff[current["id"].ToString()] = current;
			}
		}

		public Variant GetBossDieEffById(int id)
		{
			bool flag = this._bossDieEff == null;
			if (flag)
			{
				this.readBossDieEffFun(base.conf["boss_die_eff"]);
			}
			return this._bossDieEff[id.ToString()];
		}

		private void readBroadRMisAwd(Variant data)
		{
			Variant variant = data[0]["rmis"];
			foreach (Variant current in variant._arr)
			{
				string str = current["misid"]._str;
				string str2 = current["desc"]._str;
				this._broad_mis_awd[str]["misid"] = str;
				this._broad_mis_awd[str]["desc"] = str2;
			}
		}

		public string get_acpmis_desc_bymisid(uint misid)
		{
			bool flag = this._broad_mis_awd == null;
			if (flag)
			{
				this._broad_mis_awd = new Variant();
				this.readBroadRMisAwd(base.conf["broad_rmis_desc"]);
			}
			string result;
			foreach (Variant current in this._broad_mis_awd._arr)
			{
				bool flag2 = current["misid"]._uint == misid;
				if (flag2)
				{
					result = current["desc"];
					return result;
				}
			}
			result = "";
			return result;
		}

		private void readBroadBoss(Variant data)
		{
			this._broad_boss = new Variant();
			Variant variant = data[0]["boss"];
			foreach (Variant current in variant._arr)
			{
				string key = current["type"];
				Variant variant2 = new Variant();
				variant2["desc"] = current["desc"];
				variant2["type"] = current["type"];
				bool flag = current.ContainsKey("mid");
				if (flag)
				{
					Variant value = GameTools.split(current["mid"]._str, ",", 1u);
					variant2["mids"] = value;
				}
				bool flag2 = current.ContainsKey("mapid");
				if (flag2)
				{
					Variant value = GameTools.split(current["mapid"]._str, ",", 1u);
					variant2["mapids"] = value;
				}
				bool flag3 = this._broad_boss[key] == null;
				if (flag3)
				{
					this._broad_boss[key] = new Variant();
				}
				this._broad_boss[key]._arr.Add(variant2);
			}
		}

		private string get_boss_desc_bytype(string type, uint mapid, uint mid)
		{
			bool flag = mapid == 0u && mid == 0u;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				Variant variant = this._broad_boss[type];
				bool flag2 = variant;
				if (flag2)
				{
					foreach (Variant current in variant._arr)
					{
						bool flag3 = mapid != 0u && mid > 0u;
						if (flag3)
						{
							bool flag4 = current.ContainsKey("mids") && current.ContainsKey("mapids");
							if (flag4)
							{
								bool flag5 = current["mids"]._str.IndexOf(mid.ToString()) != -1 && current["mapids"]._str.IndexOf(mapid.ToString()) != -1;
								if (flag5)
								{
									result = current["desc"];
									return result;
								}
							}
						}
						else
						{
							bool flag6 = mapid > 0u;
							if (flag6)
							{
								bool flag7 = current.ContainsKey("mapids") && !current.ContainsKey("mids");
								if (flag7)
								{
									bool flag8 = current["mapids"]._str.IndexOf(mapid.ToString()) != -1;
									if (flag8)
									{
										result = current["desc"];
										return result;
									}
								}
							}
							else
							{
								bool flag9 = current.ContainsKey("mids") && !current.ContainsKey("mapids");
								if (flag9)
								{
									bool flag10 = current["mids"]._str.IndexOf(mid.ToString()) != -1;
									if (flag10)
									{
										result = current["desc"];
										return result;
									}
								}
							}
						}
					}
				}
				result = "";
			}
			return result;
		}

		public string get_bossrevive_desc_bytpid(uint mid = 0u, uint mapid = 0u)
		{
			bool flag = this._broad_boss == null;
			if (flag)
			{
				this.readBroadBoss(base.conf["broad_boss"]);
			}
			return this.get_boss_desc_bytype("born", mapid, mid);
		}

		public string get_bossdie_desc_bytpid(uint mid = 0u, uint mapid = 0u)
		{
			bool flag = this._broad_boss == null;
			if (flag)
			{
				this.readBroadBoss(base.conf["broad_boss"]);
			}
			return this.get_boss_desc_bytype("die", mapid, mid);
		}

		private void readBroadMalldItems(Variant data)
		{
			this._broad_mall_items = new Variant();
			Variant variant = data[0]["itm"];
			foreach (Variant current in variant._arr)
			{
				string languageText = LanguagePack.getLanguageText("chat", current["desc"]._str);
				current["desc"] = languageText;
				this._broad_mall_items[current["tpid"].ToString()] = current;
			}
		}

		public string get_desc_bytpid(string tpid)
		{
			bool flag = this._broad_mall_items == null;
			if (flag)
			{
				this.readBroadMalldItems(base.conf["broad_mall_items"]);
			}
			string result;
			foreach (Variant current in this._broad_mall_items._arr)
			{
				bool flag2 = current["tpid"]._str == tpid;
				if (flag2)
				{
					result = current["desc"];
					return result;
				}
			}
			result = "";
			return result;
		}

		private void readBroaddItems(Variant data)
		{
			this._broad_items = new Variant();
			Variant variant = data[0]["itm"];
			foreach (Variant current in variant._arr)
			{
				this._broad_items[current["tpid"].ToString()] = current;
			}
		}

		public bool is_need_broad(string tpid)
		{
			bool flag = this._broad_items == null;
			if (flag)
			{
				this.readBroaddItems(base.conf["broad_items"]);
			}
			return this._broad_items[tpid] != null;
		}

		public string get_broadstr_bytpid(string tpid)
		{
			string result;
			foreach (Variant current in this._broad_items._arr)
			{
				bool flag = current["tpid"]._str == tpid;
				if (flag)
				{
					string languageText = LanguagePack.getLanguageText("chat", current["desc"]._str);
					result = languageText;
					return result;
				}
			}
			result = "";
			return result;
		}

		private void readPkKingInfo(Variant data)
		{
		}

		public Variant GetPkKingInfo(int carr)
		{
			return this._pkKingShowInfo[carr.ToString()];
		}

		private void readPkKingNPC(Variant data)
		{
		}

		public Variant GetPkKingNPC()
		{
			return this._pkKingNPCInfo;
		}

		public Variant getInputKey(uint code)
		{
			return this._hotKeyData[code.ToString()];
		}

		public uint get_percent_byscale(uint scale)
		{
			Variant variant = base.conf["lines_show"];
			bool flag = this._lines_arr == null;
			if (flag)
			{
				bool flag2 = variant != null && variant[0] != null;
				if (flag2)
				{
					this._lines_arr = variant[0]["line"];
				}
			}
			uint result;
			foreach (Variant current in this._lines_arr._arr)
			{
				bool flag3 = scale <= current["max"]._uint && scale >= current["min"]._uint;
				if (flag3)
				{
					result = current["show"]._uint;
					return result;
				}
			}
			result = 100u;
			return result;
		}

		public Variant get_warmhintData(int lvlid)
		{
			bool flag = base.conf.ContainsKey("warm_hint");
			Variant result;
			if (flag)
			{
				result = base.conf["warm_hint"][lvlid.ToString()];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant objGetOlAwardConfByAid(int iAid)
		{
			Variant variant = base.conf["ol_award"];
			uint num = 0u;
			Variant result;
			while ((ulong)num != (ulong)((long)variant.Length))
			{
				Variant variant2 = variant[num];
				bool flag = variant2 != null;
				if (flag)
				{
					uint @uint = variant2["aid"]._uint;
					bool flag2 = (ulong)@uint == (ulong)((long)iAid);
					if (flag2)
					{
						result = variant2;
						return result;
					}
				}
				num += 1u;
			}
			result = null;
			return result;
		}

		public Variant GetBackGift()
		{
			return base.conf["backGift"][0]["gift"];
		}

		public Variant getBackItemsByTpid(uint tpid)
		{
			Variant variant = base.conf["backGift"][0]["gift"];
			Variant result;
			foreach (Variant current in variant._arr)
			{
				bool flag = current["itmid"]._uint == tpid;
				if (flag)
				{
					result = current;
					return result;
				}
			}
			result = null;
			return result;
		}

		public Variant GetMissionIcon()
		{
			return base.conf["missionIcon"];
		}

		public Variant GetMlineAwardInfo(int chapter)
		{
			bool flag = chapter < 0;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = !this._mlineTipInfo[chapter.ToString()];
				if (flag2)
				{
					this._mlineTipInfo[chapter.ToString()] = base.conf["mlineTip"][chapter.ToString()];
					string str = base.conf["mlineTip"][chapter.ToString()]["misid"];
					Variant value = GameTools.split(str, ",", 1u);
					this._mlineTipInfo[chapter.ToString()]["misid"] = value;
				}
				result = this._mlineTipInfo[chapter.ToString()];
			}
			return result;
		}

		public Variant GetMlineAwardInfoByMisid(int misid, int lvl, uint lastid)
		{
			Variant variant = base.conf["mlineTip"];
			int chapter = -1;
			for (int i = 0; i < variant.Length; i++)
			{
				bool flag = variant[i];
				if (flag)
				{
					int num = misid;
					int num2 = lvl;
					bool flag2 = variant[i]["isclient"];
					if (flag2)
					{
						num2++;
						num++;
					}
					bool flag3 = num <= variant[i]["awdmis"]._int && num2 <= variant[i]["level"]._int && lastid < variant[i]["awdmis"]._uint;
					if (flag3)
					{
						chapter = variant[i]["chapter"]._int;
						break;
					}
				}
			}
			return this.GetMlineAwardInfo(chapter);
		}

		public Variant GetMlineShow3D(int tpid)
		{
			return base.conf["mlineshow3D"][tpid.ToString()];
		}

		public bool IsLastMis(int misid)
		{
			int length = base.conf["mlineTip"].Length;
			return misid >= base.conf["mlineTip"][length - 1]["awdmis"]._int;
		}

		public Variant GetFestivalData(uint ractid)
		{
			Variant variant = base.conf["actinfo"]["festival"]["act"];
			Variant result;
			foreach (Variant current in variant._arr)
			{
				bool flag = current["id"]._uint == ractid;
				if (flag)
				{
					result = current;
					return result;
				}
			}
			result = null;
			return result;
		}

		public Variant GetMonAtk(uint monid)
		{
			return (this._monAtk != null) ? this._monAtk[monid.ToString()] : null;
		}

		public Variant GetPlyAtk(uint carrid)
		{
			return (this._plyAtk != null) ? this._plyAtk[carrid.ToString()] : null;
		}

		private void _initLvlNodeData()
		{
			Variant variant = base.conf["level"][0]["level"];
			foreach (Variant current in variant._arr)
			{
				int @int = current["tpid"]._int;
				bool flag = this._lvlnodedata[@int.ToString()] == null;
				if (flag)
				{
					this._lvlnodedata[@int.ToString()] = new Variant();
				}
				Variant variant2 = current["diff"];
				foreach (Variant current2 in variant2._arr)
				{
					int int2 = current2["diffid"]._int;
					Variant variant3 = current2["node"];
					bool flag2 = this._lvlnodedata[@int.ToString()][int2.ToString()] == null;
					if (flag2)
					{
						this._lvlnodedata[@int.ToString()][int2.ToString()] = new Variant();
					}
					for (int i = 0; i < variant3.Length; i++)
					{
						int int3 = variant3[i]["x"]._int;
						int int4 = variant3[i]["y"]._int;
						Variant variant4 = new Variant();
						variant4["x"] = int3;
						variant4["y"] = int4;
						this._lvlnodedata[@int.ToString()][int2.ToString()]._arr.Add(variant4);
					}
				}
			}
		}

		public Variant get_lvl_node(int tpid, int diff)
		{
			bool flag = this._lvlnodedata == null;
			if (flag)
			{
				this._lvlnodedata = new Variant();
				this._initLvlNodeData();
			}
			Variant variant = this._lvlnodedata[tpid.ToString()];
			bool flag2 = variant == null;
			Variant result;
			if (flag2)
			{
				result = null;
			}
			else
			{
				Variant variant2 = this._lvlnodedata[tpid.ToString()][diff.ToString()];
				bool flag3 = variant2 == null;
				if (flag3)
				{
					int num = 0;
					using (Dictionary<string, Variant>.ValueCollection.Enumerator enumerator = variant.Values.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string s = enumerator.Current;
							int num2 = int.Parse(s);
							bool flag4 = num2 < diff;
							if (flag4)
							{
								bool flag5 = num < num2;
								if (flag5)
								{
									num = num2;
								}
							}
						}
					}
					variant2 = this._lvlnodedata[tpid.ToString()][num.ToString()];
				}
				result = variant2;
			}
			return result;
		}

		public Variant GetChaFilterConf(string tp)
		{
			return this._chaFilters[tp];
		}

		private Variant meriPosArr()
		{
			bool flag = this._meriPosArr == null;
			if (flag)
			{
				this._meriPosArr = new Variant();
				Variant variant = base.conf["meri"];
				foreach (Variant current in variant._arr)
				{
					this._meriPosArr[current["id"].ToString()] = current;
				}
			}
			return this._meriPosArr;
		}

		public Variant GetMeriPos(int idx)
		{
			Variant variant = this.meriPosArr();
			bool flag = variant && variant.ContainsKey(idx.ToString());
			Variant result;
			if (flag)
			{
				result = variant[idx.ToString()]["acup"];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetMisTrackShowLevel()
		{
			return base.conf["misTrackShowLevel"];
		}

		public int GetHideAutoGameBtnMisid()
		{
			return base.conf["hideAGBtnConf"][0]["finMline"][0]["id"]._int;
		}

		public Variant GetDalyrepTypeRmis(uint type)
		{
			bool flag = this._typeRmis == null;
			if (flag)
			{
				this._typeRmis = new Variant();
				Variant variant = base.conf["rmisDalyrep"];
				bool flag2 = variant != null && variant[0] != null;
				if (flag2)
				{
					foreach (Variant current in variant[0]["type"]._arr)
					{
						bool flag3 = current != null;
						if (flag3)
						{
							this._typeRmis[current["id"].ToString()] = GameTools.split(current["rmis"]._str, ",", 1u);
						}
					}
				}
			}
			return this._typeRmis[type.ToString()];
		}

		public uint GetDalyrepRmisType(uint rmis)
		{
			bool flag = this._RmisType == null;
			if (flag)
			{
				this._RmisType = new Variant();
				Variant variant = base.conf["rmisDalyrep"];
				bool flag2 = variant != null && variant[0] != null;
				if (flag2)
				{
					foreach (Variant current in variant[0]["type"]._arr)
					{
						bool flag3 = current;
						if (flag3)
						{
							Variant variant2 = GameTools.split(current["rmis"], ",", 1u);
							using (Dictionary<string, Variant>.ValueCollection.Enumerator enumerator2 = variant2.Values.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									string s = enumerator2.Current;
									this._RmisType[uint.Parse(s)] = current["id"];
								}
							}
						}
					}
				}
			}
			return this._RmisType[rmis.ToString()];
		}

		public Variant GetClanLvlRmis(int id = 1)
		{
			bool flag = this._clanRmis == null;
			if (flag)
			{
				this._clanRmis = new Variant();
				Variant variant = base.conf["rmisClan"];
				bool flag2 = variant != null;
				if (flag2)
				{
					foreach (Variant current in variant._arr)
					{
						bool flag3 = current != null;
						if (flag3)
						{
							this._clanRmis[current["id"].ToString()] = GameTools.split(current["rmis"]._str, ",", 1u);
						}
					}
				}
			}
			return this._clanRmis[id.ToString()];
		}

		public Variant GetAvatarApp(int avaid)
		{
			return this._avaCha[avaid.ToString()];
		}

		public bool IsNeedStateDisTip(int id)
		{
			bool flag = base.conf["stateDisTip"] == null;
			return !flag && base.conf["stateDisTip"][id.ToString()] != null;
		}

		public bool IsNeedStateAddTip(int id)
		{
			bool flag = base.conf["stateAddTip"] == null;
			return !flag && base.conf["stateAddTip"][id.ToString()] != null;
		}

		public string get_mapname_icon(uint mapid)
		{
			bool flag = base.conf["mapNameImg"].ContainsKey(mapid.ToString());
			string result;
			if (flag)
			{
				result = base.conf["mapNameImg"][mapid.ToString()]["icon"];
			}
			else
			{
				result = "";
			}
			return result;
		}

		public Variant get_carr_gift_arr(int carr, int carrlvl)
		{
			bool flag = this._newcomer_gift == null;
			if (flag)
			{
				this._newcomer_gift = new Variant();
				Variant variant = base.conf["newcomer_gift"][0]["gift"];
				for (int i = 0; i < variant.Length; i++)
				{
					Variant variant2 = variant[i];
					string str = variant2["id"]._str;
					string str2 = variant2["level"]._str;
					string str3 = variant2["dropid"]._str;
					string str4 = variant2["carr"]._str;
					string str5 = variant2["carrlvl"]._str;
					Variant value = GameTools.split(str3, ",", 1u);
					Variant variant3 = new Variant();
					variant3["id"] = str;
					variant3["level"] = str2;
					variant3["dropid"] = str3;
					variant3["carrlvl"] = str5;
					variant3["carr"] = str4;
					variant3["items"] = value;
					this._newcomer_gift._arr.Add(variant3);
				}
			}
			Variant variant4 = new Variant();
			uint num = 0u;
			while ((ulong)num < (ulong)((long)this._newcomer_gift.Length))
			{
				Variant variant5 = this._newcomer_gift[num];
				bool flag2 = (long)carr == (long)((ulong)variant5["carr"]._uint) && (long)carrlvl == (long)((ulong)variant5["carrlvl"]._uint);
				if (flag2)
				{
					variant4._arr.Add(variant5);
				}
				num += 1u;
			}
			return variant4;
		}

		public Variant getCurProSkillList(int carr)
		{
			bool flag = base.conf["skillList"].ContainsKey(carr.ToString());
			Variant result;
			if (flag)
			{
				string str = base.conf["skillList"][carr.ToString()]["skid"]._str;
				Variant variant = GameTools.split(str, ",", 1u);
				Variant variant2 = new Variant();
				for (int i = 0; i < variant.Length; i++)
				{
					Variant variant3 = new Variant();
					variant3["skid"] = variant[i];
					variant3["mark"] = "general";
					variant2[i] = variant3;
				}
				result = variant2;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetAutogameLinks()
		{
			bool flag = this._linkInfos == null;
			if (flag)
			{
				this._linkInfos = new Variant();
				bool flag2 = base.conf.ContainsKey("links") && base.conf["links"][0];
				if (flag2)
				{
					this._linkInfos = base.conf["links"][0]["link"];
				}
			}
			return this._linkInfos;
		}

		public Variant GetNpcProp(int npcid)
		{
			bool flag = this._npcPropObj == null;
			if (flag)
			{
				this._npcPropObj = new Variant();
				bool flag2 = base.conf.ContainsKey("npcprop") && base.conf["npcprop"][0];
				if (flag2)
				{
					foreach (Variant current in base.conf["npcprop"][0]["npc"]._arr)
					{
						this._npcPropObj[current["id"].ToString()] = current;
					}
				}
			}
			return this._npcPropObj[npcid.ToString()];
		}

		public Variant GetCgoals(int goalid)
		{
			bool flag = this._cgoaldata == null;
			if (flag)
			{
				this._cgoaldata = new Variant();
				bool flag2 = base.conf.ContainsKey("clientgoal") && base.conf["clientgoal"][0];
				if (flag2)
				{
					foreach (Variant current in base.conf["clientgoal"][0]["goal"]._arr)
					{
						Variant variant = current["mis"];
						foreach (Variant current2 in variant._arr)
						{
							string languageText = LanguagePack.getLanguageText("clientgoal", current2["link"]._str);
							current2["link"] = languageText;
						}
						this._cgoaldata[current["id"].ToString()] = variant;
					}
				}
			}
			return this._cgoaldata[goalid.ToString()];
		}

		public Variant GetBestoneInfo()
		{
			bool flag = this._bestoneArr == null;
			if (flag)
			{
				this._bestoneArr = new Variant();
				bool flag2 = base.conf.ContainsKey("bestone") && base.conf["bestone"][0];
				if (flag2)
				{
					foreach (Variant current in base.conf["bestone"][0]["carr"]._arr)
					{
						current["name"] = LanguagePack.getLanguageText("carrchief", current["name"]._str);
						this._bestoneArr._arr.Add(current);
					}
				}
			}
			return this._bestoneArr;
		}

		private void setPermanentItemsArr()
		{
			bool flag = base.conf.ContainsKey("permanentItems");
			if (flag)
			{
				this._permanentItemsArr = base.conf["permanentItems"][0]["tp"];
			}
		}

		public Variant get_permanentItems_by_type(string type)
		{
			bool flag = this._permanentItemsArr == null;
			if (flag)
			{
				this.setPermanentItemsArr();
			}
			bool flag2 = this._permanentItemsArr == null;
			Variant result;
			if (flag2)
			{
				result = null;
			}
			else
			{
				uint num = 0u;
				while ((ulong)num < (ulong)((long)this._permanentItemsArr.Length))
				{
					bool flag3 = this._permanentItemsArr[num]["type"]._str == type;
					if (flag3)
					{
						result = this._permanentItemsArr[num];
						return result;
					}
					num += 1u;
				}
				result = null;
			}
			return result;
		}

		public Variant Get3DShow(uint tpid)
		{
			bool flag = base.conf.ContainsKey("permanentAvatar");
			Variant result;
			if (flag)
			{
				Variant variant = base.conf["permanentAvatar"][0]["show3D"];
				foreach (Variant current in variant._arr)
				{
					bool flag2 = current["tpid"]._uint == tpid;
					if (flag2)
					{
						result = current;
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public Variant GetObjAchiveID(string type, int id)
		{
			bool flag = !this._objAchives;
			if (flag)
			{
				this._objAchives = new Variant();
				bool flag2 = base.conf.ContainsKey("achive");
				if (flag2)
				{
					Variant variant = base.conf["achive"][0];
					bool flag3 = variant;
					if (flag3)
					{
						foreach (string current in variant.Keys)
						{
							Variant variant2 = new Variant();
							foreach (Variant current2 in variant[current]._arr)
							{
								Variant value = GameTools.split(current2["achiveid"]._str, ",", 1u);
								variant2[current2["id"]._int.ToString()] = value;
							}
							this._objAchives[current] = variant2;
						}
					}
				}
			}
			bool flag4 = this._objAchives[type];
			Variant result;
			if (flag4)
			{
				result = this._objAchives[type][id.ToString()];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetPlayerGuideInfo(int tp)
		{
			return base.conf["playerguide"][tp.ToString()];
		}

		public Variant GetUpdateBoardInfo()
		{
			return base.conf["updateboard"];
		}

		public Variant GetMobilegifts()
		{
			bool flag = base.conf;
			Variant result;
			if (flag)
			{
				Variant variant = base.conf["mobilegift"];
				bool flag2 = variant && variant[0];
				if (flag2)
				{
					result = variant[0]["itm"];
					return result;
				}
			}
			result = null;
			return result;
		}

		public Variant GetLvlFinDo(uint ltpid)
		{
			bool flag = base.conf;
			Variant result;
			if (flag)
			{
				Variant variant = base.conf["lvlfindo"];
				bool flag2 = variant && variant[0];
				if (flag2)
				{
					foreach (Variant current in variant[0]["lvl"]._arr)
					{
						bool flag3 = ltpid == current["ltpid"]._uint;
						if (flag3)
						{
							result = current;
							return result;
						}
					}
				}
			}
			result = null;
			return result;
		}

		public int GetExattCombpt(int att, int grade = 1)
		{
			int num = 0;
			foreach (string current in this._combptExatt.Keys)
			{
				int num2 = StringUtil.FormatStringType(current);
				bool flag = (att & num2) == num2;
				if (flag)
				{
					bool flag2 = this._combptExatt[num2.ToString()][grade] != null;
					if (flag2)
					{
						num += this._combptExatt[num2.ToString()][grade]._int;
					}
				}
			}
			return num;
		}

		public Variant GetCombptConf()
		{
			bool flag = base.conf.ContainsKey("combpt");
			if (flag)
			{
				bool flag2 = this.combptConf == null;
				if (flag2)
				{
					this.combptConf = new Variant();
					foreach (Variant current in base.conf["combpt"]._arr)
					{
						this.combptConf[current["attname"]._str] = current["per"];
					}
				}
			}
			return this.combptConf;
		}

		public Variant GetVeaponData(int carr)
		{
			bool flag = base.conf == null;
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = base.conf["veapon"][carr.ToString()];
			}
			return result;
		}

		public Variant getClientPvipAwdByCount()
		{
			return base.conf["pvipitmname"][0]["item"];
		}

		public bool GetRechargeState()
		{
			return base.conf["rechargestate"][0]["flag"];
		}

		public Variant GetPvipCharge()
		{
			bool flag = this._pvipChargeData == null;
			if (flag)
			{
				this._pvipChargeData = new Variant();
				Variant variant = base.conf["pvipCharge"][0];
				bool flag2 = variant;
				if (flag2)
				{
					foreach (Variant current in variant["tpid"]._arr)
					{
						this._pvipChargeData[current["id"].ToString()] = current;
					}
				}
			}
			return this._pvipChargeData;
		}

		public Variant GetFeedsData()
		{
			bool flag = this._feeds == null;
			if (flag)
			{
				this._feeds = new Variant();
				Variant variant = base.conf["feed"];
				bool flag2 = variant != null;
				if (flag2)
				{
					foreach (Variant current in variant._arr)
					{
						this._feeds[current["type"]] = new Variant();
						foreach (string current2 in current.Keys)
						{
							this._feeds[current["type"]][current2] = current[current2];
						}
						this._feeds[current["type"].ToString()]["once"] = current["once"]._bool;
						bool flag3 = current.ContainsKey("con");
						if (flag3)
						{
							Variant variant2 = new Variant();
							foreach (Variant current3 in current["con"]._arr)
							{
								variant2[current3["arg"].ToString()] = current3;
								bool flag4 = current3.ContainsKey("once");
								if (flag4)
								{
									variant2[current3["arg"].ToString()]["once"] = current3["once"]._bool;
								}
								else
								{
									variant2[current3["arg"].ToString()]["once"] = this._feeds[current["type"].ToString()]["once"];
								}
								bool flag5 = !current3.ContainsKey("desc");
								if (flag5)
								{
									variant2[current3["arg"].ToString()]["desc"] = this._feeds[current["type"].ToString()]["desc"];
								}
							}
							this._feeds[current["type"].ToString()]["con"] = variant2;
						}
					}
				}
			}
			return this._feeds;
		}

		public int GetMountAvatar(int qual)
		{
			bool flag = base.conf["mount"];
			int result;
			if (flag)
			{
				bool flag2 = base.conf["mount"][qual.ToString()];
				if (flag2)
				{
					result = base.conf["mount"][qual.ToString()]["avatar"];
					return result;
				}
			}
			result = 0;
			return result;
		}

		public Variant GetSkillAttack(int carr)
		{
			bool flag = base.conf.ContainsKey("attackSkill");
			Variant result;
			if (flag)
			{
				result = base.conf["attackSkill"][carr.ToString()];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public int GetDailyQual(int mid, int rid, int qual)
		{
			Variant variant = base.conf["dailyMis"];
			bool flag = variant && variant[mid];
			if (flag)
			{
				foreach (Variant current in variant[mid.ToString()]["rqual"]._arr)
				{
					bool flag2 = current["id"]._int == rid;
					if (flag2)
					{
						bool flag3 = qual < current["qual"]._int;
						if (flag3)
						{
							qual = current["qual"]._int;
						}
						break;
					}
				}
			}
			return qual;
		}

		public Variant GetMultilevel()
		{
			return base.conf["multilevel"];
		}

		public string GetRepAni(int cid, string ani)
		{
			Variant variant = base.conf["replaceAni"];
			bool flag = variant && variant.ContainsKey(cid.ToString()) && variant[cid.ToString()]["rep"].ContainsKey(ani.ToString());
			string result;
			if (flag)
			{
				result = variant[cid.ToString()]["rep"][ani]["rani"];
			}
			else
			{
				result = ani;
			}
			return result;
		}

		public Variant GetMapLimit(int mapid)
		{
			bool flag = this._maplimit == null;
			if (flag)
			{
				this._maplimit = new Variant();
				Variant variant = base.conf["mapLimit"];
				bool flag2 = variant != null;
				if (flag2)
				{
					foreach (Variant current in variant._arr)
					{
						Variant variant2 = GameTools.split(current["mapid"]._str, ",", 1u);
						using (List<Variant>.Enumerator enumerator2 = variant2._arr.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								string key = enumerator2.Current;
								this._maplimit[key] = current;
							}
						}
					}
				}
			}
			return this._maplimit[mapid.ToString()];
		}

		public Variant GetLevelAvatar(int ltpid, int carr)
		{
			bool flag = this._levelAvatar == null;
			if (flag)
			{
				this._levelAvatar = new Variant();
				bool flag2 = base.conf.ContainsKey("lvlAvatar");
				if (flag2)
				{
					foreach (Variant current in base.conf["lvlAvatar"]._arr)
					{
						this._levelAvatar[current["ltpid"].ToString()] = new Variant();
						Variant variant = new Variant();
						foreach (Variant current2 in current["carr"]._arr)
						{
							variant[current2["id"].ToString()] = current2;
						}
						this._levelAvatar[current["ltpid"].ToString()]["prop"] = current.clone();
						this._levelAvatar[current["ltpid"].ToString()]["prop"].RemoveKey("carrobj");
						this._levelAvatar[current["ltpid"].ToString()]["carr"] = variant;
					}
				}
			}
			bool flag3 = this._levelAvatar.ContainsKey(ltpid.ToString());
			Variant result;
			if (flag3)
			{
				Variant variant2 = new Variant();
				bool flag4 = this._levelAvatar[ltpid.ToString()]["carr"].ContainsKey(carr.ToString());
				if (flag4)
				{
					variant2["prop"] = this._levelAvatar[ltpid.ToString()]["prop"];
					variant2["carr"] = this._levelAvatar[ltpid.ToString()]["carr"][carr.ToString()];
					result = variant2;
				}
				else
				{
					variant2["prop"] = this._levelAvatar[ltpid.ToString()]["prop"];
					variant2["carr"] = this._levelAvatar[ltpid.ToString()]["carr"][0];
					result = variant2;
				}
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetQuizConf()
		{
			return base.conf["quiz"];
		}

		public Variant GetpointShop()
		{
			return base.conf["pointShop"];
		}

		public Variant GetActFestival()
		{
			bool flag = this._actfestival == null;
			if (flag)
			{
				this._actfestival = new Variant();
				foreach (Variant current in base.conf["actFestival"]._arr)
				{
					string str = current["id"]._str;
					Variant value = GameTools.split(str, ",", 1u);
					this._actfestival[current["tp"].ToString()] = value;
				}
			}
			return this._actfestival;
		}

		public Variant GetMapObj(int type, int showtp)
		{
			Variant variant = base.conf["mapObject"];
			bool flag = variant && variant[type.ToString()];
			Variant result;
			if (flag)
			{
				result = variant[type]["show"][showtp.ToString()];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetResetObj(int lvl)
		{
			bool flag = base.conf.ContainsKey("resetlevel");
			Variant result;
			if (flag)
			{
				result = base.conf["resetlevel"][lvl.ToString()];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetCarrAtt(int carr, string att)
		{
			bool flag = this._carrAtt == null;
			Variant result;
			if (flag)
			{
				this._carrAtt = new Variant();
				bool flag2 = base.conf["carrAtt"] == null;
				if (flag2)
				{
					result = null;
					return result;
				}
				foreach (Variant current in base.conf["carrAtt"]._arr)
				{
					Variant variant = new Variant();
					foreach (Variant current2 in current["addAtt"]._arr)
					{
						string str = current2["addAtts"]._str;
						variant[current2["att"].ToString()] = GameTools.split(str, ",", 1u);
					}
					this._carrAtt[current["id"].ToString()] = variant;
				}
			}
			bool flag3 = this._carrAtt[carr.ToString()] == null;
			if (flag3)
			{
				result = null;
			}
			else
			{
				result = this._carrAtt[carr.ToString()][att];
			}
			return result;
		}

		public Variant GetlevelTitle(uint tpid)
		{
			bool flag = base.conf.ContainsKey("levelTitle");
			Variant result;
			if (flag)
			{
				foreach (Variant current in base.conf["levelTitle"]._arr)
				{
					bool flag2 = current["tpid"]._uint == tpid;
					if (flag2)
					{
						result = current;
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public Variant GetInvestYb()
		{
			bool flag = base.conf.ContainsKey("investYb");
			Variant result;
			if (flag)
			{
				string str = base.conf["investYb"][0]["yb"]._str;
				result = GameTools.split(str, ",", 1u);
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetMisTrackObj()
		{
			bool flag = base.conf["mistrack"];
			Variant result;
			if (flag)
			{
				result = base.conf["mistrack"][0];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetMultiPosKil(uint id)
		{
			return base.conf["multiPosKil"][id.ToString()];
		}

		public Variant GetAutoMis()
		{
			bool flag = base.conf.ContainsKey("autoMis");
			Variant result;
			if (flag)
			{
				bool flag2 = this._autoMis == null;
				if (flag2)
				{
					this._autoMis = new Variant();
					this._autoMis = base.conf["autoMis"][0];
					string str = this._autoMis["maxlvl"]._str;
					Variant variant = GameTools.split(str, ",", 1u);
					int num = variant[0]._int;
					bool flag3 = variant.Length > 1;
					if (flag3)
					{
						Random random = new Random();
						int num2 = random.Next(0, variant[1]._int - num);
						num += num2;
					}
					this._autoMis["maxlvl"] = num;
					bool flag4 = this._autoMis.ContainsKey("misids");
					if (flag4)
					{
						string str2 = this._autoMis["misids"]._str;
						Variant variant2 = GameTools.split(str2, ",", 1u);
						Random random2 = new Random();
						int num3 = random2.Next(0, variant2.Length);
						int num4 = num3;
						this._autoMis["misid"] = variant2[num4.ToString()];
					}
					bool flag5 = this._autoMis.ContainsKey("disconnectlvl");
					if (flag5)
					{
						string str3 = this._autoMis["disconnectlvl"]._str;
						Variant variant3 = GameTools.split(str3, ",", 1u);
						this._autoMis["disminlvl"] = variant3[0];
						this._autoMis["dismaxlvl"] = variant3[1];
					}
				}
				result = this._autoMis;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public uint GetColorByType(string s)
		{
			bool flag = base.conf.ContainsKey("showcolor") && base.conf["showcolor"].ContainsKey(s);
			uint result;
			if (flag)
			{
				result = StringUtil.FormatStringType(base.conf["showcolor"][s]["color"]._str)._uint;
			}
			else
			{
				result = 16777215u;
			}
			return result;
		}

		public Variant GetItemsByResetlvl(int lvl)
		{
			Variant variant = new Variant();
			bool flag = base.conf.ContainsKey("shopFilter");
			if (flag)
			{
				foreach (Variant current in base.conf["shopFilter"].Values)
				{
					bool flag2 = lvl < current["lvl"]._int;
					if (flag2)
					{
						variant._arr.AddRange(current["items"]._arr);
					}
				}
			}
			return variant;
		}

		public Variant GetGemComposeRateItems()
		{
			bool flag = base.conf.ContainsKey("rateItem");
			Variant result;
			if (flag)
			{
				result = base.conf["rateItem"];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetGemHasRateItems()
		{
			bool flag = base.conf.ContainsKey("hasRateItm");
			Variant result;
			if (flag)
			{
				result = base.conf["hasRateItm"];
			}
			else
			{
				result = null;
			}
			return result;
		}

		private Variant _getRideSkillPosArr()
		{
			bool flag = this._rideSkillPosArr == null;
			if (flag)
			{
				this._rideSkillPosArr = new Variant();
				Variant variant = base.conf["ridestarpos"];
				foreach (Variant current in variant._arr)
				{
					this._rideSkillPosArr[current["qual"].ToString()] = current;
				}
			}
			return this._rideSkillPosArr;
		}

		public Variant GetRideSkillPos(int qual)
		{
			Variant variant = this._getRideSkillPosArr();
			bool flag = variant && variant.ContainsKey(qual.ToString());
			Variant result;
			if (flag)
			{
				result = variant[qual]["pos"];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetMulitCompose(int id)
		{
			bool flag = base.conf.ContainsKey("mulitCompose");
			Variant result;
			if (flag)
			{
				bool flag2 = base.conf["mulitCompose"][id.ToString()];
				if (flag2)
				{
					result = base.conf["mulitCompose"][id.ToString()];
					return result;
				}
			}
			result = null;
			return result;
		}

		public Variant GetRideSkillImg()
		{
			bool flag = base.conf.ContainsKey("rideskillimg");
			Variant result;
			if (flag)
			{
				result = base.conf["rideskillimg"];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetVirSkillConfBySkid(uint skid)
		{
			bool flag = base.conf.ContainsKey("virtualskill");
			Variant result;
			if (flag)
			{
				Variant variant = base.conf["virtualskill"];
				foreach (Variant current in variant._arr)
				{
					bool flag2 = current["skid"]._uint == skid;
					if (flag2)
					{
						result = current;
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public Variant GetVirSkillConfByStateid(uint stateid)
		{
			bool flag = base.conf.ContainsKey("virtualskill");
			Variant result;
			if (flag)
			{
				Variant variant = base.conf["virtualskill"];
				foreach (Variant current in variant._arr)
				{
					bool flag2 = current["stateid"]._uint == stateid;
					if (flag2)
					{
						result = current;
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public Variant GetVirSkillContent(uint skid, uint sklvl)
		{
			bool flag = base.conf.ContainsKey("virtualskill");
			Variant result;
			if (flag)
			{
				Variant variant = base.conf["virtualskill"];
				foreach (Variant current in variant._arr)
				{
					bool flag2 = current["skid"]._uint == skid && current["lvl"];
					if (flag2)
					{
						foreach (Variant current2 in current["lvl"]._arr)
						{
							bool flag3 = current2["val"]._uint == sklvl;
							if (flag3)
							{
								result = current2;
								return result;
							}
						}
					}
				}
			}
			result = null;
			return result;
		}

		public Variant GetComGroup(int id)
		{
			bool flag = base.conf.ContainsKey("checkGroup");
			Variant result;
			if (flag)
			{
				result = base.conf["checkGroup"][id.ToString()];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetCrossWarInfo()
		{
			return base.conf["crosswar"];
		}

		public Variant GetFashionAchieves()
		{
			return base.conf["fashion"];
		}

		public bool IsFashionAchid(uint achid)
		{
			bool result;
			foreach (Variant current in base.conf["fashion"].Values)
			{
				bool flag = achid == current["id"]._uint;
				if (flag)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public Variant GetFashionParts(uint id)
		{
			Variant result;
			foreach (Variant current in base.conf["fashion"]._arr)
			{
				bool flag = id == current["id"]._uint;
				if (flag)
				{
					result = current["parts"];
					return result;
				}
			}
			result = null;
			return result;
		}

		public Variant GetCrossWarAchieve(int rnkv)
		{
			Variant result;
			foreach (Variant current in base.conf["crosswarAchieve"]["achieve"]._arr)
			{
				bool flag = rnkv >= current["min"]._int && rnkv <= current["max"]._int;
				if (flag)
				{
					result = current;
					return result;
				}
			}
			result = null;
			return result;
		}

		public Variant GetCrossAchieves()
		{
			return base.conf["crosswarAchieve"]["achieve"];
		}

		public Variant getBlockMapEffect(int lpid)
		{
			bool flag = base.conf["mapeffect"] && base.conf["mapeffect"].ContainsKey(lpid.ToString());
			Variant result;
			if (flag)
			{
				result = base.conf["mapeffect"][lpid.ToString()]["blockZone"];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetLuckdrawConfByOid(string oid)
		{
			bool flag = base.conf.ContainsKey("luckdraw");
			Variant result;
			if (flag)
			{
				foreach (Variant current in base.conf["luckdraw"]._arr)
				{
					bool flag2 = current["oid"]._str == oid;
					if (flag2)
					{
						result = current;
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public Variant GetLuckdrawConfById(int id)
		{
			bool flag = base.conf.ContainsKey("luckdraw");
			Variant result;
			if (flag)
			{
				foreach (Variant current in base.conf["luckdraw"]._arr)
				{
					bool flag2 = current["id"]._int == id;
					if (flag2)
					{
						result = current;
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public Variant GetLuckydrawConf()
		{
			return base.conf["luckdraw"];
		}

		public Variant GetBSBuffUses()
		{
			bool flag = this._bsBuffuse == null;
			if (flag)
			{
				Variant commonConf = this.GetCommonConf("bsbuffuse");
				string str = commonConf._str;
				this._bsBuffuse = GameTools.split(str, ",", 1u);
				bool flag2 = this._bsBuffuse;
				if (flag2)
				{
					for (int i = 0; i < this._bsBuffuse.Length; i++)
					{
						this._bsBuffuse[i] = this._bsBuffuse[i]._int;
					}
				}
			}
			return this._bsBuffuse;
		}

		public Variant GetLvlDirIds()
		{
			bool flag = this._lvlDirIds == null;
			if (flag)
			{
				Variant commonConf = this.GetCommonConf("lvlDirlvlId");
				string str = commonConf._str;
				this._lvlDirIds = GameTools.split(str, ",", 1u);
				for (int i = 0; i < this._lvlDirIds.Length; i++)
				{
					this._lvlDirIds[i] = this._lvlDirIds[i]._uint;
				}
			}
			return this._lvlDirIds;
		}

		public bool IsLevelNeedShow(int idx, Variant netData)
		{
			Variant variant = base.conf["lvlNeedHide"];
			bool result = true;
			bool flag = variant[idx.ToString()];
			if (flag)
			{
				Variant variant2 = GameTools.split(variant[idx.ToString()]["hide"]._str, ",", 1u);
				string str = variant2[0]._str;
				if (!(str == "level"))
				{
					if (str == "resetlvl")
					{
						result = (netData["resetlvl"]._int >= variant2[1]._int);
					}
				}
				else
				{
					result = (netData["level"]._int >= variant2[1]._int);
				}
			}
			return result;
		}

		public Variant GetAttShowVal()
		{
			string str = this.GetCommonConf("attshowval")._str;
			bool flag = str != null;
			Variant result;
			if (flag)
			{
				result = GameTools.split(str, ",", 1u);
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetNewBroadcastWay()
		{
			string text = this.GetCommonConf("newBroadcastWay").ToString();
			bool flag = text != null;
			Variant result;
			if (flag)
			{
				string[] array = text.Split(new char[]
				{
					','
				});
				Variant variant = new Variant();
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string val = array2[i];
					variant.pushBack(val);
				}
				result = variant;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetCommonConfArray(string id, bool toInt = false)
		{
			Variant commonConf = this.GetCommonConf(id);
			string str = commonConf._str;
			bool flag = str != null;
			Variant result;
			if (flag)
			{
				Variant variant = GameTools.split(str, ",", 1u);
				if (toInt)
				{
					for (int i = 0; i < variant.Length; i++)
					{
						variant[i] = variant[i]._int;
					}
				}
				result = variant;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetLevelHall()
		{
			return base.conf["levelhall"];
		}

		public Variant GetWorshipData()
		{
			return base.conf["worship"];
		}

		public int GetRecommendAutoItem(int level, string value)
		{
			bool flag = base.conf.ContainsKey("autoRecommend");
			int result;
			if (flag)
			{
				foreach (Variant current in base.conf["autoRecommend"]._arr)
				{
					bool flag2 = current["value"]._str == value;
					if (flag2)
					{
						foreach (Variant current2 in current["item"]._arr)
						{
							bool flag3 = (level >= current2["min"]._int || !current2.ContainsKey("min")) && (level <= current2["max"]._int || !current2.ContainsKey("max"));
							if (flag3)
							{
								result = current2["tpid"];
								return result;
							}
						}
					}
				}
			}
			result = 0;
			return result;
		}

		public int GetItemGrade(int tpid)
		{
			bool flag = this.m_conf["clientItem"][0]["item"] == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				foreach (Variant current in this.m_conf["clientItem"][0]["item"]._arr)
				{
					bool flag2 = current["tpid"] == tpid;
					if (flag2)
					{
						result = current["grade"]._int;
						return result;
					}
				}
				result = 0;
			}
			return result;
		}

		public Variant GetTranscriptinfo(int tpid)
		{
			bool flag = this.m_conf.ContainsKey("transcriptinfo");
			Variant result;
			if (flag)
			{
				foreach (Variant current in this.m_conf["transcriptinfo"].Values)
				{
					bool flag2 = current["tpid"] == tpid;
					if (flag2)
					{
						result = current;
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public Variant GetMisAcceptable(int mid)
		{
			bool flag = base.conf.ContainsKey("misAcceptable");
			Variant result;
			if (flag)
			{
				foreach (Variant current in base.conf["misAcceptable"]._arr)
				{
					bool flag2 = current["mid"]._int == mid;
					if (flag2)
					{
						result = current;
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public Variant GetMapAchieve(int idx)
		{
			bool flag = !this._mapAchieveInfo[idx];
			if (flag)
			{
				Variant variant = this.m_conf["mapachieve"][idx];
				Variant variant2 = GameTools.split(variant["achieves"], ",", 1u);
				for (int i = 0; i < variant2.Length; i++)
				{
					variant2[i] = variant2[i]._int;
				}
				this._mapAchieveInfo[idx] = GameTools.createGroup(new Variant[]
				{
					"idx",
					idx,
					"mapid",
					variant["mapid"],
					"achieves",
					variant2,
					"special",
					variant["special"]
				});
			}
			return this._mapAchieveInfo[idx];
		}

		public Variant GetAllMapAchieve()
		{
			foreach (Variant current in this.m_conf["mapachieve"]._arr)
			{
				int num = current["idx"];
				bool flag = !this._mapAchieveInfo[num];
				if (flag)
				{
					Variant variant = GameTools.split(current["achieves"]._str, ",", 1u);
					for (int i = 0; i < variant.Length; i++)
					{
						variant[i] = variant[i]._int;
					}
					this._mapAchieveInfo[num] = GameTools.createGroup(new Variant[]
					{
						"idx",
						num,
						"mapid",
						current["mapid"],
						"achieves",
						variant
					});
				}
			}
			return this._mapAchieveInfo;
		}

		public int GetMapAchieveNum()
		{
			return this.m_conf["mapachieve"].Length;
		}

		public Variant GetAchieveInfo(int id)
		{
			return this.m_conf["achieve"][id];
		}

		public Variant GetBfActivity(uint tp)
		{
			return this.m_conf["bfactivity"][tp];
		}

		public Variant GetHfActivity()
		{
			return this.m_conf["hfactivity"][0];
		}

		public Variant GetLoseAwd(int ltpid, int diff)
		{
			bool flag = this.m_conf["lose_awd"] != null;
			Variant result;
			if (flag)
			{
				Variant variant = this.m_conf["lose_awd"][ltpid];
				bool flag2 = variant;
				if (flag2)
				{
					result = variant["diff"][diff];
					return result;
				}
			}
			result = null;
			return result;
		}

		public Variant GetWinAwd(int ltpid, int diff)
		{
			bool flag = this.m_conf.ContainsKey("win_awd");
			Variant result;
			if (flag)
			{
				Variant variant = this.m_conf["win_awd"][ltpid.ToString()];
				bool flag2 = variant != null;
				if (flag2)
				{
					result = variant["diff"][diff];
					return result;
				}
			}
			result = null;
			return result;
		}

		public Variant GetCastSkillConf()
		{
			return this.m_conf.ContainsKey("castskillconf") ? this.m_conf["castskillconf"] : null;
		}

		public Variant GetSkillsetConf()
		{
			return this.m_conf.ContainsKey("skillsetconf") ? this.m_conf["skillsetconf"][0] : null;
		}

		public Variant GetViewUIS(uint tp)
		{
			bool flag = this.m_conf.ContainsKey("systemset") && this.m_conf["systemset"].ContainsKey(tp.ToString());
			Variant result;
			if (flag)
			{
				result = this.m_conf["systemset"][tp.ToString()]["u"];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant get_buffSkill()
		{
			return this.m_conf["auto_buff"];
		}

		public string getRangPlayerHeadImgPath(uint uCarr)
		{
			bool flag = this.m_conf.ContainsKey("rankheadImg");
			string result;
			if (flag)
			{
				Variant variant = this.m_conf["rankheadImg"];
				for (int i = 0; i < variant[0]["img"].Length; i++)
				{
					bool flag2 = variant[0]["img"][i]["carr"]._uint == uCarr;
					if (flag2)
					{
						result = variant[0]["img"][i]["content"];
						return result;
					}
				}
			}
			result = "";
			return result;
		}
	}
}
