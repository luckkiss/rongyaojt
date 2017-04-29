using Cross;
using DG.Tweening;
using GameFramework;
using Lui;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_chatroom : FloatUi
	{
		private enum IgnoreVoice
		{
			World = 2,
			Legion,
			Team,
			Whisper
		}

		private enum IgnoreChat
		{
			Nearby = 1,
			Legion = 3,
			Team,
			Whisper
		}

		private struct ItemChatMsgObj
		{
			public Transform parent;

			public Transform itemChatMsg;

			public Transform txt_msg;
		}

		private struct minBagItem
		{
			private BaseButton btn_Item;

			private string name;

			private uint id;

			private Sprite sprite;
		}

		public struct msgItemInfo
		{
			public msgType msgtype;

			public string msg;

			public uint tpid;

			public uint id;

			public int quality;

			public uint intensify_lv;

			public uint add_level;

			public uint add_exp;

			public uint stage;

			public uint blessing_lv;

			public uint combpt;

			public List<uint> subjoin_att;

			public List<uint> gem_att;

			public uint cid;

			public string uname;
		}

		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_chatroom.<>c <>9 = new a3_chatroom.<>c();

			public static Action<GameObject> <>9__97_1;

			internal void <init>b__97_1(GameObject go)
			{
				PanelQuickTalk.root.gameObject.SetActive(false);
			}
		}

		public static a3_chatroom _instance;

		private ChatToType chatToType = ChatToType.All;

		public float dummyTop = float.NaN;

		public float dummyEnd = float.NaN;

		public float expandHeightMsgMe;

		public float expandWidthMsgMe;

		public float expandHeightMsgOthers;

		public float expandWidthMsgOthers;

		public float beginMsgOthersOffsetX;

		public float beginMsgMeOffsetX;

		public float msgLineSpace;

		public float firstMsgYInit;

		private Text txtCurrentChat;

		private RectTransform ipt_msg;

		private InputField iptf_msg;

		private List<Transform> AllItemMsg = new List<Transform>();

		private Toggle btn_all;

		private Toggle btn_world;

		private Toggle btn_nearby;

		private Toggle btn_legion;

		private Toggle btn_team;

		private Toggle btn_secretlanguage;

		private Transform panelAll;

		private Transform panelWorld;

		private Transform panelNearby;

		private Transform panelLegion;

		private Transform panelTeam;

		private Transform panelScretlanguage;

		private Panel4Player panel4Player;

		private BaseButton panel4PlayerTouchBg;

		private BaseButton btn_close;

		private Transform plusItems;

		private Transform quickMsg;

		private Transform minBag;

		private BaseButton btn_inBody;

		private BaseButton btn_inBag;

		private Transform bodyPanel;

		private LScrollPage bodyPanelLScrollPage;

		private Transform bodyPanelContent;

		private Transform bodyToggle;

		private Transform bagPanel;

		private Transform bagPanelContent;

		private Transform toggleGroup;

		private Transform bagToggle;

		private Transform prefabPageItem;

		private LScrollPage bagPanelLScrollPage;

		private GameObject itemEPrefab;

		private Transform PanelFriends;

		private GameObject itemChatCharName;

		private Text itemChatMsgConfig;

		private Transform prefabWhisperPanel;

		private Dictionary<ChatToType, Transform> chatToButtons;

		private Dictionary<ChatToType, Transform> chatToPanels;

		private Dictionary<ChatToType, a3_chatroom.ItemChatMsgObj> itemChatMsgObjs;

		private Dictionary<ChatToType, Stack<RectTransform>> chatToTypeMsgPostions;

		private Dictionary<ChatToType, float> yLastMessage = new Dictionary<ChatToType, float>();

		private Dictionary<string, Stack<RectTransform>> dicPrivateChatToTypeMsgPostions;

		private Dictionary<uint, GameObject> itemicon = new Dictionary<uint, GameObject>();

		private Dictionary<uint, GameObject> houseicon = new Dictionary<uint, GameObject>();

		private Queue<a3_chatroom.msgItemInfo> msgDic = new Queue<a3_chatroom.msgItemInfo>();

		private whisper[] stackWhisper = new whisper[5];

		private Dictionary<string, Transform> dicWhisper = new Dictionary<string, Transform>();

		private Dictionary<uint, Variant> dicEquip = new Dictionary<uint, Variant>();

		private Transform endMsgPosition;

		private RectTransform endMsgPosition2D;

		private string mWhisperName;

		public Text textItemCurrentPage;

		private Transform itemChatMsg;

		private Transform itemChatMsgMe;

		private Transform itemSysMsg;

		private Transform panelSettings;

		private Transform panelBag;

		private Transform btn_tagSettings;

		private Transform btn_tagBag;

		private Transform btn_tagSettingsActive;

		private Transform btn_tagSettingsInactive;

		private Transform btn_tagBagActive;

		private Transform btn_tagBagInactive;

		private Text textItemName;

		private Text textItemInfo;

		private Dictionary<ChatToType, bool> ignoreVoiceStat;

		private Dictionary<ChatToType, bool> ignoreChatStat;

		private Text text_ignoreLegion;

		private Text text_ignoreTeam;

		private Text text_ignoreWhisper;

		private Text text_ignoreNearby;

		private Text text_ignoreVoiceLegion;

		private Text text_ignoreVoiceTeam;

		private Text text_ignoreVoiceWhisper;

		private Text text_ignoreVoiceWorld;

		private Transform iconChatMsg;

		private Transform iconChatMsgMe;

		private int lengthLimit;

		private int currentDisplayEquipCount;

		private float OffsetY;

		private long voicetimer;

		private bool isLoaded;

		private string iptf_msgStr;

		private float OutTimer;

		private bool isMeSay;

		private bool createOnceInExpbar;

		private List<msg4roll> rollWordStrList;

		private int LRichTextWidth;

		public Dictionary<ChatToType, bool> IsMsgOutOfRange;

		private uint itemIndex;

		private bool ipt_msgChanged;

		private bool isWhisper;

		private List<LPageItem> listPageItem;

		private List<GameObject> itemsGameObject;

		private int itemsCount;

		private int onePageItemCount;

		private int pageCount;

		private bool isFirstTimeOpen;

		private float delayExcuteShowTime;

		[SerializeField]
		private float BorderMaxValue;

		public override void init()
		{
			a3_chatroom._instance = this;
			this.AllItemMsg.Clear();
			this.chatToTypeMsgPostions = new Dictionary<ChatToType, Stack<RectTransform>>();
			this.dicPrivateChatToTypeMsgPostions = new Dictionary<string, Stack<RectTransform>>();
			this.chatToButtons = new Dictionary<ChatToType, Transform>();
			this.chatToPanels = new Dictionary<ChatToType, Transform>();
			this.btn_close = new BaseButton(base.transform.FindChild("btn_close"), 1, 1);
			this.btn_close.onClick = new Action<GameObject>(this.onCloseClick);
			this.ipt_msg = base.transform.FindChild("bottom/InputField").GetComponent<RectTransform>();
			this.iptf_msg = this.ipt_msg.GetComponent<InputField>();
			this.minBag = base.transform.FindChild("bottom/minBag");
			this.quickMsg = base.transform.FindChild("bottom/quickMsg");
			BaseButton baseButton = new BaseButton(base.transform.FindChild("bottom/btn_Funcs/btn_bag"), 1, 1);
			this.txtCurrentChat = base.getComponentByPath<Text>("bottom/txtCurrentChat/Text");
			BaseButton baseButton2 = new BaseButton(base.transform.FindChild("bottom/btn_Funcs/btn_pos"), 1, 1);
			BaseButton baseButton3 = new BaseButton(base.transform.FindChild("bottom/btn_Funcs/btn_quickTalk"), 1, 1);
			BaseButton baseButton4 = new BaseButton(base.transform.FindChild("bottom/btn_sendMsg"), 1, 1);
			BaseButton baseButton5 = new BaseButton(base.transform.FindChild("panels/PanelQucikTalk/btn_quickTalk_sl"), 1, 1);
			EventTriggerListener eventTriggerListener = EventTriggerListener.Get(base.transform.FindChild("bottom/btn_Funcs/btn_voice").gameObject);
			this.btn_inBody = new BaseButton(base.transform.FindChild("bottom/minBag/BagPanel/btn_inBody"), 1, 1);
			this.btn_inBag = new BaseButton(base.transform.FindChild("bottom/minBag/BagPanel/btn_inBag"), 1, 1);
			Toggle tgPlus = base.getComponentByPath<Toggle>("bottom/tgPlus");
			tgPlus.onValueChanged.AddListener(new UnityAction<bool>(this.onTgPlusClick));
			this.plusItems = base.transform.FindChild("bottom/btn_Funcs");
			this.lengthLimit = this.iptf_msg.transform.FindChild("limit").GetComponent<InputField>().characterLimit;
			this.iptf_msg.onValueChanged.AddListener(delegate(string str)
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				string text = str.Replace(']', '[');
				string[] array = text.Split(new char[]
				{
					'['
				});
				for (int j = 0; j < array.Length; j++)
				{
					int num4 = 0;
					for (int k = 0; k < array[j].Length; k++)
					{
						bool flag = array[j][k] == '#';
						if (flag)
						{
							num4++;
						}
					}
					bool flag2 = num4 == 1;
					if (flag2)
					{
						num2++;
						num = 2 + num + array[j].Length;
					}
					else
					{
						bool flag3 = num3 + array[j].Length <= this.lengthLimit;
						if (flag3)
						{
							num3 += array[j].Length;
						}
					}
					bool flag4 = num2 == 3;
					if (flag4)
					{
						break;
					}
				}
				this.currentDisplayEquipCount = num2;
				try
				{
					bool flag5 = str.Length > Mathf.Max(num3 + num, this.lengthLimit);
					if (flag5)
					{
						this.iptf_msg.text = str.Substring(0, num + this.lengthLimit - 1);
					}
				}
				catch (Exception)
				{
					this.iptf_msg.text = str.Substring(0, this.lengthLimit);
				}
			});
			eventTriggerListener.onDown = new EventTriggerListener.VoidDelegate(this.onVoiceDrag);
			eventTriggerListener.onExit = new EventTriggerListener.VoidDelegate(this.onVoiceout);
			eventTriggerListener.onUp = new EventTriggerListener.VoidDelegate(this.onVoiceUp);
			baseButton.onClick = new Action<GameObject>(this.onBagClick);
			baseButton2.onClick = new Action<GameObject>(this.onPosClick);
			baseButton3.onClick = new Action<GameObject>(this.onQuickTalkClick);
			baseButton4.onClick = new Action<GameObject>(this.onSendMsgClick);
			BaseButton arg_2E7_0 = baseButton5;
			Action<GameObject> arg_2E7_1;
			if ((arg_2E7_1 = a3_chatroom.<>c.<>9__97_1) == null)
			{
				arg_2E7_1 = (a3_chatroom.<>c.<>9__97_1 = new Action<GameObject>(a3_chatroom.<>c.<>9.<init>b__97_1));
			}
			arg_2E7_0.onClick = arg_2E7_1;
			this.btn_inBody.onClick = new Action<GameObject>(this.onBtnInBodyClick);
			this.btn_inBag.onClick = new Action<GameObject>(this.onBtnInBagClick);
			this.btn_all = base.transform.FindChild("msgShow/left/btn_all").GetComponent<Toggle>();
			this.btn_world = base.transform.FindChild("msgShow/left/btn_world").GetComponent<Toggle>();
			this.btn_nearby = base.transform.FindChild("msgShow/left/btn_nearby").GetComponent<Toggle>();
			this.btn_legion = base.transform.FindChild("msgShow/left/btn_legion").GetComponent<Toggle>();
			this.btn_team = base.transform.FindChild("msgShow/left/btn_team").GetComponent<Toggle>();
			this.btn_secretlanguage = base.transform.FindChild("msgShow/left/btn_secretlanguage").GetComponent<Toggle>();
			this.btn_all.onValueChanged.AddListener(new UnityAction<bool>(this.onAllClick));
			this.btn_world.onValueChanged.AddListener(new UnityAction<bool>(this.onWorldClick));
			this.btn_nearby.onValueChanged.AddListener(new UnityAction<bool>(this.onNearbyClick));
			this.btn_legion.onValueChanged.AddListener(new UnityAction<bool>(this.onLegionClick));
			this.btn_team.onValueChanged.AddListener(new UnityAction<bool>(this.onTeamClick));
			this.chatToButtons.Add(ChatToType.All, this.btn_all.transform);
			this.chatToButtons.Add(ChatToType.World, this.btn_world.transform);
			this.chatToButtons.Add(ChatToType.Nearby, this.btn_nearby.transform);
			this.chatToButtons.Add(ChatToType.Legion, this.btn_legion.transform);
			this.chatToButtons.Add(ChatToType.Team, this.btn_team.transform);
			this.chatToButtons.Add(ChatToType.Whisper, this.btn_secretlanguage.transform);
			this.panelAll = base.transform.FindChild("msgShow/right/PanelAll");
			this.panelWorld = base.transform.FindChild("msgShow/right/PanelWorld");
			this.panelNearby = base.transform.FindChild("msgShow/right/PanelNearby");
			this.panelLegion = base.transform.FindChild("msgShow/right/PanelLegion");
			this.panelTeam = base.transform.FindChild("msgShow/right/PanelTeam");
			this.panelScretlanguage = base.transform.FindChild("msgShow/right/PanelSecretlanguage");
			this.panel4Player.transform = base.transform.FindChild("panels/Panel4Player");
			this.dummyTop = this.panelAll.FindChild("viewMask/scroll/dummyTop").transform.position.y;
			this.dummyEnd = this.panelAll.FindChild("viewMask/scroll/dummyEnd").transform.position.y;
			this.panel4PlayerTouchBg = new BaseButton(base.transform.FindChild("panels/Panel4Player/bg"), 1, 1);
			this.endMsgPosition = base.transform.FindChild("msgShow/right/endPos");
			this.endMsgPosition2D = this.endMsgPosition.GetComponent<RectTransform>();
			this.panel4Player.txt_name = base.transform.FindChild("panels/Panel4Player/txt_name").GetComponent<Text>();
			this.panel4Player.btn_see = new BaseButton(base.transform.FindChild("panels/Panel4Player/buttons/btn_see"), 1, 1);
			this.panel4Player.btn_see.onClick = new Action<GameObject>(this.onBtnSeePlayerInfo);
			this.panel4Player.btn_addFriend = new BaseButton(base.transform.FindChild("panels/Panel4Player/buttons/btn_addFriend"), 1, 1);
			this.panel4Player.btn_addFriend.onClick = new Action<GameObject>(this.onBtnAddFriendClick);
			this.panel4Player.btn_pinvite = new BaseButton(base.transform.FindChild("panels/Panel4Player/buttons/btn_pinvite"), 1, 1);
			this.panel4Player.btn_privateChat = new BaseButton(base.transform.FindChild("panels/Panel4Player/buttons/btn_privateChat"), 1, 1);
			this.panel4Player.btn_privateChat.onClick = new Action<GameObject>(this.onPrivateChatClick);
			this.chatToPanels.Add(ChatToType.All, this.panelAll);
			this.chatToPanels.Add(ChatToType.World, this.panelWorld);
			this.chatToPanels.Add(ChatToType.Nearby, this.panelNearby);
			this.chatToPanels.Add(ChatToType.Legion, this.panelLegion);
			this.chatToPanels.Add(ChatToType.Team, this.panelTeam);
			this.chatToPanels.Add(ChatToType.Whisper, this.panelScretlanguage);
			this.panel4PlayerTouchBg.onClick = new Action<GameObject>(this.onPanel4PlayerTouchBgClick);
			this.initItemChatMsgObjs();
			this.bodyPanel = base.transform.FindChild("bottom/minBag/BagPanel/bodyPanel");
			this.bodyPanelLScrollPage = this.bodyPanel.FindChild("ScrollView").gameObject.AddComponent<LScrollPage>();
			this.bodyPanelLScrollPage.smooting = 4f;
			this.bodyPanelContent = this.bodyPanel.FindChild("ScrollView/Content");
			this.bodyToggle = this.bodyPanel.FindChild("bodyToggle");
			this.bagPanel = base.transform.FindChild("bottom/minBag/BagPanel/bagPanel");
			this.bagPanelLScrollPage = this.bagPanel.FindChild("ScrollView").gameObject.AddComponent<LScrollPage>();
			this.bagPanelLScrollPage.smooting = 4f;
			this.bagPanelContent = this.bagPanel.FindChild("ScrollView/Content");
			this.bagToggle = this.bagPanel.FindChild("bagToggle");
			new BaseButton(base.transform.FindChild("bottom/minBag/btn_close"), 1, 1).onClick = delegate(GameObject go)
			{
				go.transform.parent.gameObject.SetActive(false);
				this.onTgPlusClick(tgPlus.isOn = false);
			};
			this.PanelFriends = base.transform.FindChild("panels/PanelFriends");
			whisper.mainBody = this.PanelFriends.FindChild("main");
			whisper.panelsSecretlanguage = base.transform.FindChild("msgShow/right/PanelsSecretlanguage");
			whisper.btn_Whisper = new BaseButton(this.PanelFriends.FindChild("btnWhisper"), 1, 1);
			whisper.btn_Whisper.onClick = new Action<GameObject>(this.onWhisperbtnClick);
			Transform transform = this.PanelFriends.FindChild("main/friends");
			for (int i = 0; i < transform.childCount; i++)
			{
				whisper whisper = default(whisper);
				whisper.btn_Player = new BaseButton(transform.GetChild(i), 1, 1);
				this.stackWhisper[i] = whisper;
				transform.GetChild(i).gameObject.SetActive(false);
			}
			PanelQuickTalk.root = base.transform.FindChild("panels/PanelQucikTalk");
			PanelQuickTalk.btn01 = new BaseButton(PanelQuickTalk.root.FindChild("buttons/Button01"), 1, 1);
			PanelQuickTalk.btn02 = new BaseButton(PanelQuickTalk.root.FindChild("buttons/Button02"), 1, 1);
			PanelQuickTalk.btn03 = new BaseButton(PanelQuickTalk.root.FindChild("buttons/Button03"), 1, 1);
			PanelQuickTalk.btn01.onClick = new Action<GameObject>(this.onPanelQuickTalkClick);
			PanelQuickTalk.btn02.onClick = new Action<GameObject>(this.onPanelQuickTalkClick);
			PanelQuickTalk.btn03.onClick = new Action<GameObject>(this.onPanelQuickTalkClick);
			this.prefabPageItem = base.transform.FindChild("template/PageItem");
			this.itemChatMsg = base.transform.FindChild("template/itemChatMsg");
			this.expandWidthMsgOthers = this.itemChatMsg.transform.FindChild("rect/msgBody").GetComponent<RectTransform>().sizeDelta.x;
			this.expandHeightMsgOthers = this.itemChatMsg.transform.FindChild("rect/msgBody").GetComponent<RectTransform>().sizeDelta.y;
			this.beginMsgOthersOffsetX = this.itemChatMsg.GetComponent<RectTransform>().anchoredPosition.y;
			this.itemChatMsgMe = base.transform.FindChild("template/itemChatMsgMe");
			this.expandWidthMsgMe = this.itemChatMsgMe.transform.FindChild("rect/msgBody").GetComponent<RectTransform>().sizeDelta.x;
			this.expandHeightMsgMe = this.itemChatMsgMe.transform.FindChild("rect/msgBody").GetComponent<RectTransform>().sizeDelta.y;
			this.beginMsgMeOffsetX = this.itemChatMsgMe.GetComponent<RectTransform>().anchoredPosition.y;
			this.itemSysMsg = base.transform.FindChild("template/itemSysMsg");
			this.itemChatCharName = base.transform.FindChild("template/itemChatMsg/itemChatCharName").gameObject;
			this.itemChatMsgConfig = base.transform.FindChild("template/itemChatMsgConfig").GetComponent<Text>();
			this.toggleGroup = base.transform.FindChild("template/toggleGroup");
			this.prefabWhisperPanel = base.transform.FindChild("template/PanelSecretlanguage");
			Transform transform2 = base.transform.FindChild("template/config");
			this.msgLineSpace = transform2.GetComponent<Text>().lineSpacing * -1f;
			this.firstMsgYInit = transform2.GetComponent<RectTransform>().anchoredPosition.y;
			this.btn_all.onValueChanged.AddListener(new UnityAction<bool>(this.onAllClick));
			this.btn_world.onValueChanged.AddListener(new UnityAction<bool>(this.onWorldClick));
			this.btn_nearby.onValueChanged.AddListener(new UnityAction<bool>(this.onNearbyClick));
			this.btn_legion.onValueChanged.AddListener(new UnityAction<bool>(this.onLegionClick));
			this.btn_team.onValueChanged.AddListener(new UnityAction<bool>(this.onTeamClick));
			this.btn_secretlanguage.onValueChanged.AddListener(new UnityAction<bool>(this.onSecretLanguageClick));
			this.panel4Player.btn_addFriend.addEvent();
			base.gameObject.SetActive(false);
			this.itemEPrefab = base.transform.FindChild("template/ItemE").gameObject;
			this.textItemCurrentPage = base.transform.FindChild("bottom/minBag/BagPanel/PageInfo/Text").GetComponent<Text>();
			new BaseButton(base.transform.FindChild("bottom/minBag/BagPanel/Left"), 1, 1).onClick = delegate(GameObject go)
			{
				bool activeSelf = this.bodyPanel.gameObject.activeSelf;
				if (activeSelf)
				{
					this.bodyPanelLScrollPage.OnHardDrag(true);
				}
				else
				{
					bool activeSelf2 = this.bagPanel.gameObject.activeSelf;
					if (activeSelf2)
					{
						this.bagPanelLScrollPage.OnHardDrag(true);
					}
				}
			};
			new BaseButton(base.transform.FindChild("bottom/minBag/BagPanel/Right"), 1, 1).onClick = delegate(GameObject go)
			{
				bool activeSelf = this.bodyPanel.gameObject.activeSelf;
				if (activeSelf)
				{
					this.bodyPanelLScrollPage.OnHardDrag(false);
				}
				else
				{
					bool activeSelf2 = this.bagPanel.gameObject.activeSelf;
					if (activeSelf2)
					{
						this.bagPanelLScrollPage.OnHardDrag(false);
					}
				}
			};
			this.textItemName = base.transform.Find("template/ItemE/ItemName").GetComponent<Text>();
			this.textItemInfo = base.transform.Find("template/ItemE/ItemInfo").GetComponent<Text>();
			new BaseButton(PanelQuickTalk.root.FindChild("buttons/Button01/Text/Button01_Edit"), 1, 1).onClick = (new BaseButton(PanelQuickTalk.root.FindChild("buttons/Button02/Text/Button02_Edit"), 1, 1).onClick = (new BaseButton(PanelQuickTalk.root.FindChild("buttons/Button03/Text/Button03_Edit"), 1, 1).onClick = new Action<GameObject>(this.onQuickTalkMsgEditClick)));
			this.panelBag = base.transform.FindChild("bottom/minBag/BagPanel");
			this.panelSettings = base.transform.FindChild("bottom/minBag/SettingPanel");
			this.btn_tagSettings = base.transform.FindChild("bottom/minBag/btn_tagSettings");
			this.btn_tagSettingsInactive = this.btn_tagSettings.FindChild("btn_release");
			this.btn_tagSettingsActive = this.btn_tagSettings.FindChild("btn_down");
			this.btn_tagBag = base.transform.FindChild("bottom/minBag/btn_tagBag");
			this.btn_tagBagInactive = this.btn_tagBag.FindChild("btn_release");
			this.btn_tagBagActive = this.btn_tagBag.FindChild("btn_down");
			new BaseButton(this.btn_tagSettings, 1, 1).onClick = delegate(GameObject go)
			{
				this.panelBag.gameObject.SetActive(false);
				this.btn_tagSettingsActive.gameObject.SetActive(true);
				this.btn_tagSettingsInactive.gameObject.SetActive(false);
				this.btn_tagBagActive.gameObject.SetActive(false);
				this.btn_tagBagInactive.gameObject.SetActive(true);
				this.panelSettings.gameObject.SetActive(true);
			};
			new BaseButton(this.btn_tagBag, 1, 1).onClick = delegate(GameObject go)
			{
				this.panelBag.gameObject.SetActive(true);
				this.btn_tagSettingsActive.gameObject.SetActive(false);
				this.btn_tagSettingsInactive.gameObject.SetActive(true);
				this.btn_tagBagActive.gameObject.SetActive(true);
				this.btn_tagBagInactive.gameObject.SetActive(false);
				this.panelSettings.gameObject.SetActive(false);
			};
			this.AddListenerForToggles();
			this.OffsetY = (float)this.GetIntValueFromMsgConfig("offset_y", this.itemChatMsgConfig);
		}

		private int GetIntValueFromMsgConfig(string filter, Text textPrefab)
		{
			string text = textPrefab.text;
			string value = string.Format("</{0}>", filter);
			filter = string.Format("<{0}>", filter);
			int num = text.IndexOf(filter) + filter.Length;
			string s = text.Substring(num, text.IndexOf(value) - num);
			int result = 0;
			int.TryParse(s, out result);
			return result;
		}

		private void AddListenerForToggles()
		{
			this.text_ignoreLegion = base.transform.FindChild("bottom/minBag/SettingPanel/Panel/ToggleGrp_IgnoreChat/ToggleLegion/Label").GetComponent<Text>();
			this.text_ignoreTeam = base.transform.FindChild("bottom/minBag/SettingPanel/Panel/ToggleGrp_IgnoreChat/ToggleTeam/Label").GetComponent<Text>();
			this.text_ignoreWhisper = base.transform.FindChild("bottom/minBag/SettingPanel/Panel/ToggleGrp_IgnoreChat/ToggleWhisper/Label").GetComponent<Text>();
			this.text_ignoreNearby = base.transform.FindChild("bottom/minBag/SettingPanel/Panel/ToggleGrp_IgnoreChat/ToggleNearby/Label").GetComponent<Text>();
			base.transform.FindChild("bottom/minBag/SettingPanel/Panel/ToggleGrp_IgnoreChat/ToggleLegion").GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool isSelected)
			{
				Graphic arg_25_0 = this.text_ignoreLegion;
				this.ignoreChatStat[ChatToType.Legion] = isSelected;
				arg_25_0.color = (isSelected ? Color.green : Color.white);
			});
			base.transform.FindChild("bottom/minBag/SettingPanel/Panel/ToggleGrp_IgnoreChat/ToggleTeam").GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool isSelected)
			{
				Graphic arg_25_0 = this.text_ignoreTeam;
				this.ignoreChatStat[ChatToType.Team] = isSelected;
				arg_25_0.color = (isSelected ? Color.green : Color.white);
			});
			base.transform.FindChild("bottom/minBag/SettingPanel/Panel/ToggleGrp_IgnoreChat/ToggleWhisper").GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool isSelected)
			{
				Graphic arg_25_0 = this.text_ignoreWhisper;
				this.ignoreChatStat[ChatToType.Whisper] = isSelected;
				arg_25_0.color = (isSelected ? Color.green : Color.white);
			});
			base.transform.FindChild("bottom/minBag/SettingPanel/Panel/ToggleGrp_IgnoreChat/ToggleNearby").GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool isSelected)
			{
				Graphic arg_25_0 = this.text_ignoreNearby;
				this.ignoreChatStat[ChatToType.Nearby] = isSelected;
				arg_25_0.color = (isSelected ? Color.green : Color.white);
			});
			this.text_ignoreVoiceWorld = base.transform.FindChild("bottom/minBag/SettingPanel/Panel/ToggleGrp_IgnoreVoice/ToggleWorld/Label").GetComponent<Text>();
			this.text_ignoreVoiceLegion = base.transform.FindChild("bottom/minBag/SettingPanel/Panel/ToggleGrp_IgnoreVoice/ToggleLegion/Label").GetComponent<Text>();
			this.text_ignoreVoiceTeam = base.transform.FindChild("bottom/minBag/SettingPanel/Panel/ToggleGrp_IgnoreVoice/ToggleTeam/Label").GetComponent<Text>();
			this.text_ignoreVoiceWhisper = base.transform.FindChild("bottom/minBag/SettingPanel/Panel/ToggleGrp_IgnoreVoice/ToggleWhisper/Label").GetComponent<Text>();
			base.transform.FindChild("bottom/minBag/SettingPanel/Panel/ToggleGrp_IgnoreVoice/ToggleWorld").GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool isSelected)
			{
				Graphic arg_25_0 = this.text_ignoreVoiceWorld;
				this.ignoreVoiceStat[ChatToType.World] = isSelected;
				arg_25_0.color = (isSelected ? Color.green : Color.white);
			});
			base.transform.FindChild("bottom/minBag/SettingPanel/Panel/ToggleGrp_IgnoreVoice/ToggleLegion").GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool isSelected)
			{
				Graphic arg_25_0 = this.text_ignoreVoiceLegion;
				this.ignoreVoiceStat[ChatToType.Legion] = isSelected;
				arg_25_0.color = (isSelected ? Color.green : Color.white);
			});
			base.transform.FindChild("bottom/minBag/SettingPanel/Panel/ToggleGrp_IgnoreVoice/ToggleTeam").GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool isSelected)
			{
				Graphic arg_25_0 = this.text_ignoreVoiceTeam;
				this.ignoreVoiceStat[ChatToType.Team] = isSelected;
				arg_25_0.color = (isSelected ? Color.green : Color.white);
			});
			base.transform.FindChild("bottom/minBag/SettingPanel/Panel/ToggleGrp_IgnoreVoice/ToggleWhisper").GetComponent<Toggle>().onValueChanged.AddListener(delegate(bool isSelected)
			{
				Graphic arg_25_0 = this.text_ignoreVoiceWhisper;
				this.ignoreVoiceStat[ChatToType.Whisper] = isSelected;
				arg_25_0.color = (isSelected ? Color.green : Color.white);
			});
		}

		private void onQuickTalkMsgEditClick(GameObject go)
		{
			GameObject gameObject = go.transform.FindChild("Edit").gameObject;
			go.transform.FindChild("Save").gameObject.SetActive(gameObject.activeSelf);
			gameObject.SetActive(!gameObject.activeSelf);
			GameObject gameObject2 = go.transform.parent.parent.GetChild(1).gameObject;
			bool activeSelf = gameObject2.activeSelf;
			if (activeSelf)
			{
				string text = gameObject2.transform.GetChild(2).GetComponent<Text>().text;
				Text componentInParent = go.transform.GetComponentInParent<Text>();
				bool flag = text.Length > 0;
				if (flag)
				{
					componentInParent.text = new StringBuilder().AppendFormat("   {0}", text).ToString();
				}
			}
			gameObject2.SetActive(!gameObject2.activeSelf);
		}

		public override void onShowed()
		{
		}

		public override void onClosed()
		{
			foreach (GameObject current in this.itemsGameObject)
			{
				UnityEngine.Object.Destroy(current.gameObject);
			}
			for (int i = this.bodyPanelContent.childCount - 1; i >= 0; i--)
			{
				UnityEngine.Object.Destroy(this.bodyPanelContent.GetChild(i).gameObject);
			}
			for (int j = this.bagPanelContent.childCount - 1; j >= 0; j--)
			{
				UnityEngine.Object.Destroy(this.bagPanelContent.GetChild(j).gameObject);
			}
			this.isLoaded = false;
		}

		public void privateChat(string name)
		{
			bool flag = string.IsNullOrEmpty(name);
			if (!flag)
			{
				bool flag2 = !base.gameObject.activeSelf;
				if (flag2)
				{
					base.gameObject.SetActive(true);
				}
				base.gameObject.transform.localPosition = Vector3.zero;
				this.chatToType = ChatToType.Whisper;
				this.setChatToButtonGroup();
				this.hideMinChatTo();
				this.chatToType = ChatToType.Whisper;
				this.setChatToPanel();
				bool flag3 = !this.PanelFriends.gameObject.activeSelf;
				if (flag3)
				{
					this.PanelFriends.gameObject.SetActive(true);
				}
				this.createWhisperPanel(name);
				this.mWhisperName = name;
				this.iptf_msg.text = this.mWhisperName + "/";
			}
		}

		private void DestroyItem()
		{
			bool flag = this.bagPanelContent != null;
			if (flag)
			{
				for (int i = 0; i < this.bagPanelContent.childCount; i++)
				{
					UnityEngine.Object.Destroy(this.bagPanelContent.GetChild(i).gameObject);
				}
			}
			bool flag2 = this.bodyPanelContent != null;
			if (flag2)
			{
				for (int j = 0; j < this.bodyPanelContent.childCount; j++)
				{
					UnityEngine.Object.Destroy(this.bodyPanelContent.GetChild(j).gameObject);
				}
			}
			bool flag3 = this.bodyToggle != null;
			if (flag3)
			{
				for (int k = 0; k < this.bodyToggle.childCount; k++)
				{
					UnityEngine.Object.Destroy(this.bodyToggle.GetChild(k).gameObject);
				}
			}
		}

		private void initItemChatMsgObjs()
		{
			this.itemChatMsgObjs = new Dictionary<ChatToType, a3_chatroom.ItemChatMsgObj>();
			foreach (KeyValuePair<ChatToType, Transform> current in this.chatToPanels)
			{
				a3_chatroom.ItemChatMsgObj value = default(a3_chatroom.ItemChatMsgObj);
				RectTransform parent = (RectTransform)current.Value.FindChild("viewMask/scroll/msgContainer");
				value.parent = parent;
				this.itemChatMsgObjs.Add(current.Key, value);
			}
		}

		private void onCloseClick(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_CHATROOM);
		}

		private void onVoiceDrag(GameObject go)
		{
			bool flag = !this.checkCanSendMsg();
			if (!flag)
			{
				this.voicetimer = NetClient.instance.CurServerTimeStampMS;
				base.Invoke("voiceNeedEnd", 30f);
				GameSdkMgr.beginVoiceRecord();
			}
		}

		private void onVoiceout(GameObject go)
		{
			base.CancelInvoke("voiceNeedEnd");
			GameSdkMgr.cancelVoiceRecord();
		}

		private void onVoiceUp(GameObject go)
		{
			base.CancelInvoke("voiceNeedEnd");
			bool flag = NetClient.instance.CurServerTimeStampMS - this.voicetimer < 600L;
			if (flag)
			{
				GameSdkMgr.cancelVoiceRecord();
			}
			else
			{
				GameSdkMgr.endVoiceRecord();
			}
		}

		private void voiceNeedEnd()
		{
			GameSdkMgr.endVoiceRecord();
		}

		private void onVoiceRecordedHandle(string state, string path, int sec)
		{
			bool flag = state == "end";
			if (flag)
			{
				bool flag2 = !this.checkCanSendMsg();
				if (!flag2)
				{
					this.sendMsg(path + "," + sec, true);
				}
			}
			else
			{
				bool flag3 = state == "error";
				if (flag3)
				{
					debug.Log("::voiceError::" + path);
				}
				else
				{
					bool flag4 = state == "begin";
					if (flag4)
					{
					}
				}
			}
		}

		private void onVoicePlayedHandle(string state)
		{
			debug.Log(".....endVoiceRecord loadHanlde:" + state);
			bool flag = state == "played";
			if (!flag)
			{
				bool flag2 = state == "error";
				if (flag2)
				{
				}
			}
		}

		private void onTgPlusClick(bool b)
		{
			bool flag = !this.isLoaded;
			if (flag)
			{
				this.loadItem();
				this.isLoaded = true;
			}
			this.minBag.gameObject.SetActive(!this.minBag.gameObject.activeSelf);
		}

		private void onBagClick(GameObject g)
		{
			this.minBag.gameObject.SetActive(!this.minBag.gameObject.activeSelf);
		}

		private void onPosClick(GameObject g)
		{
			string name = ModelBase<PlayerModel>.getInstance().name;
			string text = GRMap.curSvrConf.ContainsKey("map_name") ? GRMap.curSvrConf["map_name"]._str : "--";
			InputField inputField = this.iptf_msg;
			inputField.text = string.Concat(new object[]
			{
				inputField.text,
				"[",
				text,
				"(",
				(int)SelfRole._inst.m_curModel.position.x,
				",",
				(int)SelfRole._inst.m_curModel.position.z,
				")]"
			});
		}

		private void onQuickTalkClick(GameObject g)
		{
			bool activeSelf = PanelQuickTalk.root.gameObject.activeSelf;
			if (activeSelf)
			{
				PanelQuickTalk.root.gameObject.SetActive(false);
			}
			else
			{
				PanelQuickTalk.root.gameObject.SetActive(true);
			}
		}

		private void onSendMsgClick(GameObject g)
		{
			bool flag = !this.checkCanSendMsg();
			if (!flag)
			{
				string text = this.iptf_msg.text;
				text = KeyWord.filter(text);
				bool flag2 = string.IsNullOrEmpty(text.Trim());
				if (flag2)
				{
					flytxt.instance.fly("不能发送空消息", 0, default(Color), null);
				}
				else
				{
					this.sendMsg(text, false);
				}
			}
		}

		private bool checkCanSendMsg()
		{
			bool flag = this.OutTimer > 0f;
			bool result;
			if (flag)
			{
				flytxt.instance.fly("发言过于频繁", 0, default(Color), null);
				result = false;
			}
			else
			{
				uint up_lvl = ModelBase<PlayerModel>.getInstance().up_lvl;
				switch (this.chatToType)
				{
				case ChatToType.All:
				{
					bool flag2 = up_lvl < 1u;
					if (flag2)
					{
						flytxt.instance.fly("人物等级到达 1转1级 后可以使用世界频道", 0, default(Color), null);
						result = false;
						return result;
					}
					break;
				}
				case ChatToType.World:
				{
					bool flag3 = up_lvl < 1u;
					if (flag3)
					{
						flytxt.instance.fly("人物等级到达 1转1级 后可以使用世界频道", 0, default(Color), null);
						result = false;
						return result;
					}
					break;
				}
				case ChatToType.Whisper:
				{
					bool flag4 = up_lvl < 1u;
					if (flag4)
					{
						flytxt.instance.fly("人物等级到达 1转1级 后可以使用密语频道", 0, default(Color), null);
						result = false;
						return result;
					}
					break;
				}
				case ChatToType.PrivateSecretlanguage:
				{
					bool flag5 = up_lvl < 1u;
					if (flag5)
					{
						flytxt.instance.fly("人物等级到达 1转1级 后可以使用密语频道", 0, default(Color), null);
						result = false;
						return result;
					}
					break;
				}
				}
				result = true;
			}
			return result;
		}

		public void SendMsg(string strMsg, ChatToType chatType = ChatToType.Nearby)
		{
			this.chatToButtons[this.chatToType].GetComponent<Toggle>().isOn = false;
			switch (chatType)
			{
			case ChatToType.All:
				this.onAllClick(true);
				break;
			case ChatToType.Nearby:
				this.onNearbyClick(true);
				break;
			case ChatToType.World:
				this.onWorldClick(true);
				break;
			case ChatToType.Legion:
				this.onLegionClick(true);
				break;
			case ChatToType.Team:
				this.onTeamClick(true);
				break;
			}
			this.sendMsg(strMsg, false);
		}

		private void sendMsg(string strMsg, bool isvoice)
		{
			bool flag = strMsg == "";
			if (!flag)
			{
				uint type = (uint)this.chatToType;
				bool flag2 = this.chatToType == ChatToType.All;
				if (flag2)
				{
					type = 2u;
				}
				string name = ModelBase<PlayerModel>.getInstance().name;
				this.iptf_msgStr = strMsg;
				bool flag3 = this.chatToType == ChatToType.Whisper || this.chatToType == ChatToType.PrivateSecretlanguage;
				if (flag3)
				{
					string text = strMsg;
					bool flag4 = !text.Contains("/");
					if (flag4)
					{
						flytxt.instance.fly("请选择密语对象", 0, default(Color), null);
					}
					else
					{
						string[] array = text.Split(new char[]
						{
							'/'
						});
						this.mWhisperName = array[0];
						strMsg = array[1];
						name = this.mWhisperName;
						strMsg = strMsg.Substring(strMsg.IndexOf('/') + 1);
						BaseProxy<ChatProxy>.getInstance().sendMsg(strMsg, name, 5u, isvoice);
					}
				}
				else
				{
					bool flag5 = this.chatToType == ChatToType.Team && !ModelBase<PlayerModel>.getInstance().IsInATeam;
					if (flag5)
					{
						flytxt.instance.fly("不在队伍中,不能发送消息", 0, default(Color), null);
					}
					else
					{
						bool flag6 = this.chatToType == ChatToType.Legion && ModelBase<A3_LegionModel>.getInstance().myLegion.id <= 0;
						if (flag6)
						{
							flytxt.instance.fly("未加入军团,不能发送消息", 0, default(Color), null);
						}
						else
						{
							BaseProxy<ChatProxy>.getInstance().sendMsg(strMsg, name, type, isvoice);
							this.meSays(isvoice);
						}
					}
				}
			}
		}

		private void onBtnInBodyClick(GameObject go)
		{
			bool flag = this.bodyPanel == null || this.bagPanel == null;
			if (!flag)
			{
				this.bodyPanel.gameObject.SetActive(true);
				this.bagPanel.gameObject.SetActive(false);
			}
		}

		private void onBtnInBagClick(GameObject go)
		{
			bool flag = this.bodyPanel == null || this.bagPanel == null;
			if (!flag)
			{
				this.bodyPanel.gameObject.SetActive(false);
				this.bagPanel.gameObject.SetActive(true);
			}
		}

		public void meSays(bool isvoice)
		{
			this.isMeSay = true;
			chatData chatData = default(chatData);
			bool flag = this.chatToType == ChatToType.PrivateSecretlanguage || this.chatToType == ChatToType.Whisper;
			if (flag)
			{
				chatData.cid = this.panel4Player.cid;
				chatData.name = this.mWhisperName;
				if (isvoice)
				{
					chatData.msg = (chatData.url = this.iptf_msgStr);
				}
				else
				{
					chatData.msg = this.iptf_msgStr.Substring(this.iptf_msgStr.IndexOf('/') + 1);
				}
				this.setChatToPanel();
			}
			else
			{
				chatData.cid = ModelBase<PlayerModel>.getInstance().cid;
				chatData.name = ModelBase<PlayerModel>.getInstance().name;
				if (isvoice)
				{
					chatData.msg = (chatData.url = this.iptf_msgStr);
				}
				else
				{
					chatData.msg = this.iptf_msgStr;
				}
			}
			chatData.tp = 0u;
			chatData.vip = ModelBase<A3_VipModel>.getInstance().Level;
			chatData.mapId = ModelBase<PlayerModel>.getInstance().mapid;
			this.iptf_msg.text = string.Empty;
			switch (this.chatToType)
			{
			case ChatToType.All:
				chatData.tp = 1u;
				this.OutTimer = 5f;
				this.showMsgInPanel(chatData, null);
				break;
			case ChatToType.Nearby:
				PlayerChatUIMgr.getInstance().show(SelfRole._inst, BaseProxy<ChatProxy>.getInstance().analysisStrName(chatData.msg));
				chatData.tp = 2u;
				this.OutTimer = 3f;
				this.showMsgInPanel(chatData, null);
				break;
			case ChatToType.World:
				chatData.tp = 1u;
				this.OutTimer = 5f;
				this.showMsgInPanel(chatData, null);
				break;
			case ChatToType.Legion:
				chatData.tp = 3u;
				this.OutTimer = 3f;
				this.showMsgInPanel(chatData, null);
				break;
			case ChatToType.Team:
				chatData.tp = 4u;
				this.OutTimer = 3f;
				this.showMsgInPanel(chatData, null);
				break;
			case ChatToType.Whisper:
				chatData.tp = 5u;
				this.OutTimer = 3f;
				this.showMsgInPanel(chatData, null);
				break;
			case ChatToType.PrivateSecretlanguage:
				chatData.tp = 6u;
				this.OutTimer = 3f;
				this.showMsgInPanel(chatData, null);
				break;
			}
		}

		public void otherSays(Variant v)
		{
			this.isMeSay = false;
			chatData chatData = default(chatData);
			bool flag = v.ContainsKey("tp");
			if (flag)
			{
				chatData.tp = v["tp"];
			}
			bool flag2 = chatData.tp != 6u;
			if (flag2)
			{
				chatData.cid = v["cid"];
				chatData.name = v["name"];
			}
			chatData.msg = v["msg"];
			bool flag3 = v.ContainsKey("vip");
			if (flag3)
			{
				chatData.vip = v["vip"];
			}
			bool flag4 = v.ContainsKey("items");
			if (flag4)
			{
				chatData.items = v["items"]._arr;
			}
			bool flag5 = v.ContainsKey("mpid");
			if (flag5)
			{
				chatData.mapId = v["mpid"]._uint;
				chatData.x = v["x"]._int;
				chatData.y = v["y"]._int;
			}
			bool flag6 = v.ContainsKey("url");
			if (flag6)
			{
				chatData.msg = "--";
				chatData.url = v["url"];
			}
			bool flag7 = v.ContainsKey("carr");
			if (flag7)
			{
				chatData.carr = v["carr"];
			}
			this.dicEquip.Clear();
			bool flag8 = chatData.items != null;
			if (flag8)
			{
				for (int i = 0; i < chatData.items.Count; i++)
				{
					this.dicEquip[(uint)i] = chatData.items[i];
				}
			}
			switch (chatData.tp)
			{
			case 0u:
				this.showMsgInPanel(chatData, null);
				break;
			case 1u:
				this.showMsgInPanel(chatData, null);
				break;
			case 2u:
				this.showMsgInPanel(chatData, null);
				break;
			case 3u:
				this.showMsgInPanel(chatData, null);
				break;
			case 4u:
				this.showMsgInPanel(chatData, null);
				break;
			case 5u:
				this.showMsgInPanel(chatData, null);
				break;
			case 6u:
				this.showMsgInPanel(chatData, null);
				break;
			case 7u:
				this.showMsgInPanel(chatData, null);
				break;
			}
		}

		private void showMsgInPanel(chatData data, ChatToType? chatType = null)
		{
			ChatToType valueOrDefault = chatType.GetValueOrDefault(this.chatToType);
			this.createOnceInExpbar = true;
			bool flag = string.IsNullOrEmpty(data.msg);
			if (!flag)
			{
				bool flag2 = this.isMeSay;
				if (flag2)
				{
					bool flag3 = valueOrDefault == ChatToType.All;
					if (flag3)
					{
						this.createMsgObjInPanel(data, ChatToType.World);
					}
					else
					{
						bool flag4 = valueOrDefault != ChatToType.PrivateSecretlanguage;
						if (flag4)
						{
							this.createMsgObjInPanel(data, valueOrDefault);
						}
					}
					bool flag5 = valueOrDefault == ChatToType.PrivateSecretlanguage;
					if (flag5)
					{
						this.createMsgObjInPanel(data, valueOrDefault);
						this.createMsgObjInPanel(data, ChatToType.Whisper);
					}
					bool flag6 = valueOrDefault == ChatToType.Whisper;
					if (flag6)
					{
						this.createMsgObjInPanel(data, ChatToType.PrivateSecretlanguage);
					}
				}
				else
				{
					this.chatToType = (ChatToType)data.tp;
					bool flag7 = data.tp == 5u;
					if (flag7)
					{
						this.chatToType = ChatToType.PrivateSecretlanguage;
						this.createWhisperPanel(data.name);
						bool flag8 = this.chatToType != ChatToType.PrivateSecretlanguage;
						if (flag8)
						{
							this.dicWhisper[data.name].gameObject.SetActive(false);
						}
						this.createMsgObjInPanel(data, ChatToType.Whisper);
						this.createMsgObjInPanel(data, ChatToType.PrivateSecretlanguage);
					}
					else
					{
						bool flag9 = data.tp != 6u;
						if (flag9)
						{
							this.createMsgObjInPanel(data, this.chatToType);
						}
					}
				}
				bool flag10 = !this.ignoreChatStat.ContainsKey((ChatToType)data.tp) || !this.ignoreChatStat[(ChatToType)data.tp];
				if (flag10)
				{
					this.createMsgObjInPanel(data, ChatToType.All);
				}
			}
		}

		private void createMsgObjInPanel(chatData data, ChatToType ctp)
		{
			bool flag = ctp == ChatToType.PrivateSecretlanguage;
			if (!flag)
			{
				this.rollWordStrList.Clear();
				bool flag2 = true;
				bool flag3 = data.tp == 6u;
				bool flag4 = flag3;
				if (!flag4)
				{
					bool flag5 = this.isMeSay;
					GameObject gameObject;
					Text component;
					if (flag5)
					{
						gameObject = UnityEngine.Object.Instantiate<GameObject>(this.itemChatMsgMe.gameObject);
						component = gameObject.transform.FindChild("itemChatCharName").GetComponent<Text>();
						component.text = ModelBase<PlayerModel>.getInstance().name;
						Transform parent = gameObject.transform.FindChild("HeadIconMan");
						UnityEngine.Object.Instantiate<GameObject>(base.transform.FindChild(string.Format("template/HeadIcon/{0}", ModelBase<PlayerModel>.getInstance().profession)).gameObject).transform.SetParent(parent, false);
					}
					else
					{
						gameObject = UnityEngine.Object.Instantiate<GameObject>(this.itemChatMsg.gameObject);
						component = gameObject.transform.FindChild("itemChatCharName").GetComponent<Text>();
						component.text = data.name;
						Transform parent = gameObject.transform.FindChild("HeadIconMan");
						UnityEngine.Object.Instantiate<GameObject>(base.transform.FindChild(string.Format("template/HeadIcon/{0}", data.carr.ToString())).gameObject).transform.SetParent(parent, false);
					}
					Text component2 = gameObject.transform.FindChild("Channel").GetComponent<Text>();
					Text component3 = gameObject.transform.FindChild("VipLevel/Text").GetComponent<Text>();
					component.color = Color.white;
					component.fontSize = this.itemChatMsgConfig.fontSize;
					bool flag6 = ctp != ChatToType.PrivateSecretlanguage;
					if (flag6)
					{
						bool flag7 = ctp == ChatToType.SystemMsg;
						if (flag7)
						{
							gameObject.transform.SetParent(this.itemChatMsgObjs[ChatToType.All].parent, false);
						}
						else
						{
							gameObject.transform.SetParent(this.itemChatMsgObjs[ctp].parent, false);
						}
						this.LRichTextWidth = 450;
					}
					else
					{
						bool flag8 = !this.dicWhisper.ContainsKey(data.name);
						if (flag8)
						{
							this.createWhisperPanel(data.name);
						}
						this.dicWhisper[data.name].transform.gameObject.SetActive(true);
						Transform parent2 = this.dicWhisper[data.name].FindChild("viewMask/scroll/msgContainer");
						gameObject.transform.SetParent(parent2, false);
						this.LRichTextWidth = 450;
					}
					bool flag9 = ctp == ChatToType.SystemMsg;
					LRichText lRichText;
					if (flag9)
					{
						lRichText = gameObject.AddComponent<LRichText>();
					}
					else
					{
						Transform transform = gameObject.transform.FindChild("rect/msgBody");
						bool flag10 = transform == null;
						if (flag10)
						{
							return;
						}
						lRichText = transform.gameObject.AddComponent<LRichText>();
					}
					this.AllItemMsg.Add(gameObject.transform);
					lRichText.font = this.itemChatMsgConfig.font;
					lRichText.maxLineWidth = this.LRichTextWidth;
					bool flag11 = data.tp != 5u && data.tp != 7u;
					if (flag11)
					{
						bool flag12 = data.tp == 6u;
						if (flag12)
						{
							string chatToStr = this.getChatToStr(ChatToType.SystemMsg);
							msg4roll msg4roll = new msg4roll();
							msg4roll.msgT = msgType.chatTo;
							msg4roll.msgStr = chatToStr;
							msg4roll.color = this.getWordColor();
							this.rollWordStrList.Add(msg4roll);
							lRichText.insertElement(chatToStr, this.getWordColor(), this.itemChatMsgConfig.fontSize, false, false, this.getWordColor(), "");
							if (data.cid != ModelBase<PlayerModel>.getInstance().cid)
							{
								string.Concat(new object[]
								{
									"name:",
									data.name,
									":",
									data.cid
								});
							}
							lRichText.insertElement(data.name + ":", Color.white, this.itemChatMsgConfig.fontSize, false, false, Color.blue, "");
							msg4roll msg4roll2 = new msg4roll();
							msg4roll2.msgT = msgType.non;
							msg4roll2.msgStr = data.name + ":";
							msg4roll2.color = Color.white;
							this.rollWordStrList.Add(msg4roll2);
						}
						else
						{
							string chatToStr2 = this.getChatToStr(ctp);
							msg4roll msg4roll3 = new msg4roll();
							msg4roll3.msgT = msgType.chatTo;
							msg4roll3.msgStr = chatToStr2;
							msg4roll3.color = this.getWordColor();
							this.rollWordStrList.Add(msg4roll3);
							component2.text = chatToStr2;
							component2.color = this.getWordColor();
							component2.fontSize = this.itemChatMsgConfig.fontSize;
							bool flag13 = data.vip > 0;
							if (flag13)
							{
								component3.text = data.vip.ToString();
							}
							else
							{
								component3.text = "0";
							}
							string data2 = (data.cid == ModelBase<PlayerModel>.getInstance().cid) ? "" : string.Concat(new object[]
							{
								"name:",
								data.name,
								":",
								data.cid
							});
							msg4roll msg4roll4 = new msg4roll();
							msg4roll4.msgT = msgType.uname;
							msg4roll4.msgStr = data.name + ":";
							msg4roll4.color = Color.white;
							msg4roll4.data = data2;
							this.rollWordStrList.Add(msg4roll4);
						}
					}
					else
					{
						bool flag14 = this.isMeSay;
						if (flag14)
						{
							bool flag15 = this.panel4Player.name == null;
							if (flag15)
							{
								this.panel4Player.name = this.mWhisperName;
							}
							lRichText.insertElement("我", Color.white, this.itemChatMsgConfig.fontSize, false, false, Color.white, "");
							msg4roll msg4roll5 = new msg4roll();
							msg4roll5.msgT = msgType.non;
							msg4roll5.msgStr = "我";
							msg4roll5.color = Color.white;
							this.rollWordStrList.Add(msg4roll5);
							lRichText.insertElement("【密】", Color.magenta, this.itemChatMsgConfig.fontSize, false, false, Color.magenta, "");
							msg4roll msg4roll6 = new msg4roll();
							msg4roll6.msgT = msgType.non;
							msg4roll6.msgStr = "【密】";
							msg4roll6.color = Color.white;
							this.rollWordStrList.Add(msg4roll6);
							lRichText.insertElement(data.name + ":", this.getWordColor(), this.itemChatMsgConfig.fontSize, false, false, this.getWordColor(), string.Concat(new object[]
							{
								"name:",
								data.name,
								":",
								data.cid
							}));
							msg4roll msg4roll7 = new msg4roll();
							msg4roll7.msgT = msgType.uname;
							msg4roll7.msgStr = data.name + ":";
							msg4roll7.color = this.getWordColor();
							msg4roll7.data = string.Concat(new object[]
							{
								"name:",
								data.name,
								":",
								data.cid
							});
							this.rollWordStrList.Add(msg4roll7);
							for (int i = 0; i < this.stackWhisper.Length; i++)
							{
								bool flag16 = !string.IsNullOrEmpty(this.stackWhisper[i].name);
								if (flag16)
								{
									bool flag17 = this.stackWhisper[i].name.Equals(data.name);
									if (flag17)
									{
										flag2 = true;
										break;
									}
								}
								flag2 = false;
							}
							bool flag18 = !flag2 && string.IsNullOrEmpty(this.stackWhisper[4].name);
							if (flag18)
							{
								for (int j = 0; j < this.stackWhisper.Length; j++)
								{
									bool flag19 = this.stackWhisper[j].name == null;
									if (flag19)
									{
										whisper wp = default(whisper);
										wp.btn_Player = this.stackWhisper[j].btn_Player;
										wp.name = data.name;
										wp.cid = data.cid;
										wp.btn_Player.gameObject.SetActive(true);
										wp.btn_Player.gameObject.transform.FindChild("Text").GetComponent<Text>().text = data.name;
										wp.btn_Player.onClick = delegate(GameObject go)
										{
											this.onBtnPlayerWhisperClick(wp.cid, wp.name);
										};
										this.stackWhisper[j] = wp;
										break;
									}
								}
							}
							else
							{
								bool flag20 = !flag2 && !string.IsNullOrEmpty(this.stackWhisper[4].name);
								if (flag20)
								{
									whisper whisper = default(whisper);
									whisper = this.stackWhisper[0];
									for (int k = 0; k < this.stackWhisper.Length; k++)
									{
										bool flag21 = k < this.stackWhisper.Length - 1;
										if (flag21)
										{
											this.stackWhisper[k] = this.stackWhisper[k + 1];
											whisper = this.stackWhisper[k + 1];
										}
										else
										{
											whisper wp = default(whisper);
											wp.btn_Player = this.stackWhisper[k].btn_Player;
											wp.cid = data.cid;
											wp.name = data.name;
											wp.btn_Player.gameObject.SetActive(true);
											wp.btn_Player.gameObject.transform.FindChild("Text").GetComponent<Text>().text = data.name;
											wp.btn_Player.onClick = delegate(GameObject go)
											{
												this.onBtnPlayerWhisperClick(wp.cid, wp.name);
											};
											this.stackWhisper[k] = wp;
										}
									}
								}
							}
						}
						else
						{
							lRichText.insertElement(data.name, this.getWordColor(), this.itemChatMsgConfig.fontSize, false, false, Color.white, string.Concat(new object[]
							{
								"name:",
								data.name,
								":",
								data.cid
							}));
							msg4roll msg4roll8 = new msg4roll();
							msg4roll8.msgT = msgType.uname;
							msg4roll8.msgStr = data.name;
							msg4roll8.color = this.getWordColor();
							msg4roll8.data = string.Concat(new object[]
							{
								"name:",
								data.name,
								":",
								data.cid
							});
							this.rollWordStrList.Add(msg4roll8);
							lRichText.insertElement("【密】", Color.magenta, this.itemChatMsgConfig.fontSize, false, false, Color.magenta, "");
							msg4roll msg4roll9 = new msg4roll();
							msg4roll9.msgT = msgType.non;
							msg4roll9.msgStr = "【密】";
							msg4roll9.color = this.getWordColor();
							this.rollWordStrList.Add(msg4roll9);
							lRichText.insertElement("我", Color.white, this.itemChatMsgConfig.fontSize, false, false, Color.white, "");
							msg4roll msg4roll10 = new msg4roll();
							msg4roll10.msgT = msgType.non;
							msg4roll10.msgStr = "我";
							msg4roll10.color = Color.white;
							this.rollWordStrList.Add(msg4roll10);
							lRichText.insertElement(":", this.getWordColor(), this.itemChatMsgConfig.fontSize, false, false, this.getWordColor(), "");
							msg4roll msg4roll11 = new msg4roll();
							msg4roll11.msgT = msgType.non;
							msg4roll11.msgStr = ":";
							msg4roll11.color = this.getWordColor();
							this.rollWordStrList.Add(msg4roll11);
							for (int l = 0; l < this.stackWhisper.Length; l++)
							{
								bool flag22 = !string.IsNullOrEmpty(this.stackWhisper[l].name);
								if (flag22)
								{
									bool flag23 = this.stackWhisper[l].name.Equals(data.name);
									if (flag23)
									{
										flag2 = true;
										break;
									}
								}
								flag2 = false;
							}
							bool flag24 = !flag2 && string.IsNullOrEmpty(this.stackWhisper[4].name);
							if (flag24)
							{
								for (int m = 0; m < this.stackWhisper.Length; m++)
								{
									bool flag25 = this.stackWhisper[m].name == null;
									if (flag25)
									{
										whisper wp = default(whisper);
										wp.btn_Player = this.stackWhisper[m].btn_Player;
										wp.cid = data.cid;
										wp.name = data.name;
										wp.btn_Player.gameObject.SetActive(true);
										wp.btn_Player.gameObject.transform.FindChild("Text").GetComponent<Text>().text = data.name;
										wp.btn_Player.onClick = delegate(GameObject go)
										{
											this.onBtnPlayerWhisperClick(wp.cid, wp.name);
										};
										this.stackWhisper[m] = wp;
										break;
									}
								}
							}
							else
							{
								bool flag26 = !flag2 && !string.IsNullOrEmpty(this.stackWhisper[4].name);
								if (flag26)
								{
									whisper whisper2 = default(whisper);
									whisper2 = this.stackWhisper[0];
									for (int n = 0; n < this.stackWhisper.Length; n++)
									{
										bool flag27 = n < this.stackWhisper.Length - 1;
										if (flag27)
										{
											this.stackWhisper[n] = this.stackWhisper[n + 1];
											whisper2 = this.stackWhisper[n + 1];
										}
										else
										{
											whisper wp = default(whisper);
											wp.btn_Player = this.stackWhisper[n].btn_Player;
											wp.name = data.name;
											wp.btn_Player.gameObject.SetActive(true);
											wp.btn_Player.gameObject.transform.FindChild("Text").GetComponent<Text>().text = data.name;
											wp.btn_Player.onClick = delegate(GameObject go)
											{
												this.onBtnPlayerWhisperClick(wp.cid, wp.name);
											};
											this.stackWhisper[n] = wp;
										}
									}
								}
							}
						}
					}
					bool flag28 = data.url != null;
					if (flag28)
					{
						string[] array = data.url.Split(new char[]
						{
							','
						});
						string cont = ContMgr.getCont("chat_voice", new string[]
						{
							array[1]
						});
						lRichText.insertElement(cont, Color.gray, this.itemChatMsgConfig.fontSize, false, false, Color.gray, "voice:" + array[0]);
						msg4roll msg4roll12 = new msg4roll();
						msg4roll12.msgT = msgType.voice;
						msg4roll12.msgStr = cont;
						msg4roll12.color = Color.gray;
						msg4roll12.data = "voice:" + array[0];
						this.rollWordStrList.Add(msg4roll12);
					}
					else
					{
						this.analysisStr(data);
						foreach (a3_chatroom.msgItemInfo current in this.msgDic)
						{
							Color colorByQuality = Globle.getColorByQuality(current.quality);
							switch (current.msgtype)
							{
							case msgType.non:
							{
								lRichText.insertElement(current.msg, Color.white, this.itemChatMsgConfig.fontSize, false, false, Color.white, "");
								msg4roll msg4roll13 = new msg4roll();
								msg4roll13.msgT = msgType.non;
								msg4roll13.msgStr = current.msg;
								msg4roll13.color = Color.white;
								this.rollWordStrList.Add(msg4roll13);
								break;
							}
							case msgType.item:
							{
								lRichText.insertElement("【" + current.msg + "】", colorByQuality, this.itemChatMsgConfig.fontSize, false, false, colorByQuality, "item:" + current.tpid);
								msg4roll msg4roll14 = new msg4roll();
								msg4roll14.msgT = msgType.item;
								msg4roll14.msgStr = "【" + current.msg + "】";
								msg4roll14.color = colorByQuality;
								msg4roll14.data = "item:" + current.tpid;
								this.rollWordStrList.Add(msg4roll14);
								break;
							}
							case msgType.equip:
							{
								lRichText.insertElement("【" + current.msg + "】", colorByQuality, this.itemChatMsgConfig.fontSize, false, false, colorByQuality, "equip:" + current.id);
								msg4roll msg4roll15 = new msg4roll();
								msg4roll15.msgT = msgType.equip;
								msg4roll15.msgStr = "【" + current.msg + "】";
								msg4roll15.color = colorByQuality;
								msg4roll15.data = "equip:" + current.id;
								this.rollWordStrList.Add(msg4roll15);
								break;
							}
							case msgType.pos:
							{
								lRichText.insertElement("【", this.getWordColor(), 20, false, false, this.getWordColor(), "");
								lRichText.insertElement(current.msg, this.getWordColor(), this.itemChatMsgConfig.fontSize, false, false, Color.blue, "position:" + current.msg + data.mapId);
								msg4roll msg4roll16 = new msg4roll();
								msg4roll16.msgT = msgType.pos;
								msg4roll16.msgStr = "【" + current.msg + "】";
								msg4roll16.color = this.getWordColor();
								msg4roll16.data = "position:" + current.msg + data.mapId;
								this.rollWordStrList.Add(msg4roll16);
								lRichText.insertElement("】", this.getWordColor(), 20, false, false, this.getWordColor(), "");
								break;
							}
							case msgType.uname:
							{
								lRichText.insertElement(" " + current.uname, new Color(254f, 255f, 255f, 1f), this.itemChatMsgConfig.fontSize, false, false, Color.grey, string.Concat(new object[]
								{
									"name:",
									current.uname,
									":",
									current.cid
								}));
								msg4roll msg4roll17 = new msg4roll();
								msg4roll17.msgT = msgType.uname;
								msg4roll17.msgStr = current.uname;
								msg4roll17.color = new Color(254f, 255f, 255f, 1f);
								msg4roll17.data = string.Concat(new object[]
								{
									"name:",
									current.uname,
									":",
									current.cid
								});
								this.rollWordStrList.Add(msg4roll17);
								break;
							}
							}
						}
					}
					this.setLRichTextAction(lRichText, false);
					lRichText.reloadData();
					float num = this.firstMsgYInit;
					Stack<RectTransform> stack;
					bool flag29 = this.chatToTypeMsgPostions.TryGetValue(ctp, out stack);
					if (flag29)
					{
						Vector2 sizeDelta = stack.Peek().sizeDelta;
						Vector2 vector = stack.Peek().localScale;
						bool flag30 = flag3;
						if (flag30)
						{
							gameObject.transform.localPosition = new Vector2(0f, num = -sizeDelta.y * vector.y + stack.Peek().localPosition.y - 6f);
						}
						else
						{
							gameObject.transform.localPosition = new Vector2(this.beginMsgOthersOffsetX, num = -sizeDelta.y * vector.y + stack.Peek().localPosition.y - 25f);
						}
						this.chatToTypeMsgPostions[ctp].Push(gameObject.GetComponent<RectTransform>());
						Vector2 sizeDelta2 = gameObject.GetComponent<RectTransform>().sizeDelta;
						Vector2 vector2 = gameObject.GetComponent<RectTransform>().localScale;
						Vector2 anchoredPosition = gameObject.GetComponent<RectTransform>().anchoredPosition;
						float num2 = gameObject.transform.position.y - sizeDelta2.y * vector2.y;
						bool flag31 = this.IsMsgOutOfRange[ctp] || (this.IsMsgOutOfRange[ctp] = (num2 < this.endMsgPosition.position.y));
						if (flag31)
						{
							bool flag32 = this.itemChatMsgObjs.ContainsKey(ctp);
							if (flag32)
							{
								Vector2 sizeDelta3 = this.itemChatMsgObjs[ctp].parent.GetComponent<RectTransform>().sizeDelta;
								this.itemChatMsgObjs[ctp].parent.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeDelta3.x, sizeDelta3.y + Mathf.Abs(this.yLastMessage[ctp] - (Mathf.Abs(anchoredPosition.y) + sizeDelta2.y)));
							}
						}
						bool flag33 = num2 < this.endMsgPosition.position.y;
						if (flag33)
						{
							RectTransform rectObj = this.itemChatMsgObjs[ctp].parent.GetComponent<RectTransform>();
							rectObj.DOKill(false);
							float offsetY = this.OffsetY;
							float posY = this.itemChatMsgObjs[ctp].parent.GetComponent<RectTransform>().anchoredPosition.y + Mathf.Abs(this.yLastMessage[ctp] - (Mathf.Abs(anchoredPosition.y) + sizeDelta2.y)) + this.msgLineSpace + offsetY;
							rectObj.DOLocalMoveY(posY, 1f, false).OnKill(delegate
							{
								rectObj.anchoredPosition = new Vector2(rectObj.anchoredPosition.x, posY);
							});
						}
					}
					else
					{
						bool flag34 = flag3;
						if (flag34)
						{
							gameObject.transform.localPosition = new Vector2(0f, 0f);
						}
						else
						{
							gameObject.transform.localPosition = new Vector2(this.beginMsgOthersOffsetX, -25f);
						}
						Stack<RectTransform> stack2 = new Stack<RectTransform>();
						stack2.Push(gameObject.GetComponent<RectTransform>());
						this.chatToTypeMsgPostions.Add(ctp, stack2);
					}
					bool flag35 = this.yLastMessage.ContainsKey(ctp);
					if (flag35)
					{
						this.yLastMessage[ctp] = Mathf.Abs(gameObject.GetComponent<RectTransform>().anchoredPosition.y) + gameObject.GetComponent<RectTransform>().sizeDelta.y;
					}
					else
					{
						this.yLastMessage.Add(ctp, Mathf.Abs(gameObject.GetComponent<RectTransform>().anchoredPosition.y) + gameObject.GetComponent<RectTransform>().sizeDelta.y);
					}
					bool flag36 = this.createOnceInExpbar;
					if (flag36)
					{
						a3_expbar.instance.setRollWord(this.rollWordStrList);
						this.createOnceInExpbar = false;
					}
					bool flag37 = this.chatToType == ChatToType.SystemMsg;
					if (flag37)
					{
						this.chatToType = ChatToType.All;
					}
					gameObject.transform.localScale = Vector3.one;
					RectTransform component4 = gameObject.GetComponent<RectTransform>();
					bool flag38 = ctp != ChatToType.SystemMsg;
					if (flag38)
					{
						bool flag39 = this.isMeSay;
						if (flag39)
						{
							component4.pivot = Vector2.one;
							component4.anchoredPosition = new Vector2(-50f, num + this.msgLineSpace);
							component4.sizeDelta = component4.transform.FindChild("rect/msgBody").GetComponent<RectTransform>().sizeDelta + new Vector2(this.expandWidthMsgMe, this.expandHeightMsgMe);
						}
						else
						{
							component4.anchoredPosition = new Vector2(component4.anchoredPosition.x, num + this.msgLineSpace);
							component4.sizeDelta = component4.transform.FindChild("rect/msgBody").GetComponent<RectTransform>().sizeDelta + new Vector2(this.expandWidthMsgOthers, this.expandHeightMsgOthers);
						}
					}
					bool flag40 = gameObject != null;
					if (flag40)
					{
						Transform transform2 = gameObject.transform.FindChild("rect/msgBody");
						bool flag41 = transform2 != null;
						if (flag41)
						{
							for (int num3 = transform2.childCount; num3 > 0; num3--)
							{
								transform2.GetChild(num3 - 1).localScale = Vector3.one;
							}
						}
					}
				}
			}
		}

		private void analysisStr(chatData data)
		{
			this.itemIndex = 0u;
			this.msgDic.Clear();
			string msg = data.msg;
			bool flag = msg == null;
			if (!flag)
			{
				bool flag2 = msg.Contains('[') && msg.Contains(']');
				if (flag2)
				{
					string text = msg.Replace("]", "[");
					string[] array = text.Split(new char[]
					{
						'['
					});
					for (int i = 0; i < array.Length; i++)
					{
						bool flag3 = string.IsNullOrEmpty(array[i]);
						if (!flag3)
						{
							string text2 = array[i];
							Match match = Regex.Match(text2, "\\(.*?,.*?\\)");
							bool success = match.Success;
							if (success)
							{
								a3_chatroom.msgItemInfo item = default(a3_chatroom.msgItemInfo);
								item.msgtype = msgType.pos;
								item.msg = text2;
								this.msgDic.Enqueue(item);
							}
							else
							{
								bool flag4 = text2.Contains("#");
								if (flag4)
								{
									this.setItemStr(text2, data, this.itemIndex);
									this.itemIndex += 1u;
								}
								else
								{
									bool flag5 = text2.Contains("/");
									if (flag5)
									{
										a3_chatroom.msgItemInfo item2 = default(a3_chatroom.msgItemInfo);
										item2.msgtype = msgType.uname;
										string[] array2 = text2.Split(new char[]
										{
											'/'
										});
										uint cid;
										uint.TryParse(array2[0], out cid);
										string uname = array2[1];
										item2.cid = cid;
										item2.uname = uname;
										this.msgDic.Enqueue(item2);
										a3_chatroom.msgItemInfo item3 = default(a3_chatroom.msgItemInfo);
										item3.msgtype = msgType.non;
										item3.msg = "  ";
										this.msgDic.Enqueue(item3);
									}
									else
									{
										a3_chatroom.msgItemInfo item4 = default(a3_chatroom.msgItemInfo);
										item4.msgtype = msgType.non;
										item4.msg = text2;
										this.msgDic.Enqueue(item4);
									}
								}
							}
						}
					}
				}
				else
				{
					a3_chatroom.msgItemInfo item5 = default(a3_chatroom.msgItemInfo);
					item5.msgtype = msgType.non;
					item5.msg = msg;
					this.msgDic.Enqueue(item5);
				}
			}
		}

		private void setItemStr(string posOrItemStr, chatData data, uint index)
		{
			bool flag = this.isMeSay;
			if (flag)
			{
				bool flag2 = posOrItemStr.Contains("#");
				if (flag2)
				{
					uint num;
					bool flag3 = uint.TryParse(posOrItemStr.Split(new char[]
					{
						'#'
					})[1], out num);
					bool flag4 = !flag3;
					if (flag4)
					{
						a3_chatroom.msgItemInfo item = default(a3_chatroom.msgItemInfo);
						item.msgtype = msgType.non;
						item.msg = posOrItemStr;
						this.msgDic.Enqueue(item);
					}
					else
					{
						string text = posOrItemStr.Split(new char[]
						{
							'#'
						})[0];
						text = (text.Contains("神·") ? text.Remove(0, 2) : text);
						a3_ItemData itemDataByName = ModelBase<a3_BagModel>.getInstance().getItemDataByName(text);
						bool flag5 = itemDataByName.item_type == 1;
						if (flag5)
						{
							a3_chatroom.msgItemInfo item2 = default(a3_chatroom.msgItemInfo);
							item2.msgtype = msgType.item;
							item2.msg = posOrItemStr.Split(new char[]
							{
								'#'
							})[0];
							item2.tpid = itemDataByName.tpid;
							item2.quality = itemDataByName.quality;
							this.msgDic.Enqueue(item2);
						}
						else
						{
							bool flag6 = itemDataByName.item_type == 2;
							if (flag6)
							{
								bool flag7 = ModelBase<a3_EquipModel>.getInstance().getEquips().ContainsKey(num);
								if (flag7)
								{
									a3_BagItemData a3_BagItemData = ModelBase<a3_EquipModel>.getInstance().getEquips()[num];
									a3_chatroom.msgItemInfo item3 = default(a3_chatroom.msgItemInfo);
									item3.msgtype = msgType.equip;
									item3.msg = posOrItemStr.Split(new char[]
									{
										'#'
									})[0];
									item3.id = num;
									item3.quality = a3_BagItemData.confdata.quality;
									this.msgDic.Enqueue(item3);
								}
								else
								{
									bool flag8 = ModelBase<a3_BagModel>.getInstance().getItems(false).ContainsKey(num);
									if (flag8)
									{
										a3_BagItemData a3_BagItemData2 = ModelBase<a3_BagModel>.getInstance().getItems(false)[num];
										a3_chatroom.msgItemInfo item4 = default(a3_chatroom.msgItemInfo);
										item4.msgtype = msgType.equip;
										item4.msg = posOrItemStr.Split(new char[]
										{
											'#'
										})[0];
										item4.id = num;
										item4.quality = a3_BagItemData2.confdata.quality;
										this.msgDic.Enqueue(item4);
									}
								}
							}
						}
					}
				}
			}
			else
			{
				bool flag9 = posOrItemStr.Contains("#");
				if (flag9)
				{
					bool flag10 = this.dicEquip[index].ContainsKey("intensify_lv");
					if (flag10)
					{
						uint @uint = this.dicEquip[index]["tpid"]._uint;
						a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(@uint);
						a3_chatroom.msgItemInfo item5 = default(a3_chatroom.msgItemInfo);
						item5.msgtype = msgType.equip;
						item5.msg = posOrItemStr.Split(new char[]
						{
							'#'
						})[0];
						item5.id = index;
						item5.quality = itemDataById.quality;
						this.msgDic.Enqueue(item5);
					}
					else
					{
						a3_chatroom.msgItemInfo msgItemInfo = default(a3_chatroom.msgItemInfo);
						msgItemInfo.msgtype = msgType.item;
						msgItemInfo.msg = posOrItemStr.Split(new char[]
						{
							'#'
						})[0];
						msgItemInfo.tpid = index;
						a3_ItemData itemDataByName2 = ModelBase<a3_BagModel>.getInstance().getItemDataByName(msgItemInfo.msg);
						msgItemInfo.quality = itemDataByName2.quality;
						this.msgDic.Enqueue(msgItemInfo);
					}
				}
			}
		}

		public void setLRichTextAction(LRichText lrt_msg, bool singlePanel = false)
		{
			lrt_msg.onClickHandler = delegate(string dataTmp)
			{
				bool flag = string.IsNullOrEmpty(dataTmp);
				if (!flag)
				{
					string[] array = dataTmp.Split(new char[]
					{
						':'
					});
					string text = array[0];
					string a = text;
					if (!(a == "name"))
					{
						if (!(a == "item"))
						{
							if (!(a == "equip"))
							{
								if (!(a == "position"))
								{
									if (a == "voice")
									{
										GameSdkMgr.playVoice(array[1] + ":" + array[2]);
									}
								}
								else
								{
									string text2 = array[1].Split(new char[]
									{
										'('
									})[1];
									string s = text2.Split(new char[]
									{
										','
									})[0];
									string s2 = text2.Split(new char[]
									{
										','
									})[1].Split(new char[]
									{
										')'
									})[0];
									int id = int.Parse(text2.Split(new char[]
									{
										','
									})[1].Split(new char[]
									{
										')'
									})[1]);
									float x;
									float.TryParse(s, out x);
									float z;
									float.TryParse(s2, out z);
									Vector3 pos = new Vector3(x, 0f, z);
									SelfRole.moveto(id, pos, null, 0.3f);
								}
							}
							else
							{
								uint num;
								uint.TryParse(array[1], out num);
								bool flag2 = ModelBase<a3_EquipModel>.getInstance().getEquips().ContainsKey(num);
								if (flag2)
								{
									a3_BagItemData a3_BagItemData = ModelBase<a3_EquipModel>.getInstance().getEquips()[num];
									ArrayList arrayList = new ArrayList();
									arrayList.Add(a3_BagItemData);
									arrayList.Add(equip_tip_type.Comon_tip);
									InterfaceMgr.getInstance().open(InterfaceMgr.A3_EQUIPTIP, arrayList, false);
								}
								else
								{
									bool flag3 = ModelBase<a3_BagModel>.getInstance().getItems(false).ContainsKey(num);
									if (flag3)
									{
										a3_BagItemData a3_BagItemData2 = ModelBase<a3_BagModel>.getInstance().getItems(false)[num];
										ArrayList arrayList2 = new ArrayList();
										arrayList2.Add(a3_BagItemData2);
										arrayList2.Add(equip_tip_type.Comon_tip);
										InterfaceMgr.getInstance().open(InterfaceMgr.A3_EQUIPTIP, arrayList2, false);
									}
									else
									{
										uint @uint = this.dicEquip[num]["tpid"]._uint;
										a3_BagItemData a3_BagItemData3 = default(a3_BagItemData);
										a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(@uint);
										a3_BagItemData3.tpid = @uint;
										a3_BagItemData3.confdata = itemDataById;
										a3_BagItemData3.equipdata = default(a3_EquipData);
										bool flag4 = this.dicEquip[num].ContainsKey("intensify_lv");
										if (flag4)
										{
											a3_BagItemData3.equipdata.intensify_lv = this.dicEquip[num]["intensify_lv"]._int;
										}
										bool flag5 = this.dicEquip[num].ContainsKey("colour");
										if (flag5)
										{
											a3_BagItemData3.equipdata.color = this.dicEquip[num]["colour"]._uint;
										}
										bool flag6 = this.dicEquip[num].ContainsKey("add_level");
										if (flag6)
										{
											a3_BagItemData3.equipdata.add_level = this.dicEquip[num]["add_level"]._int;
										}
										bool flag7 = this.dicEquip[num].ContainsKey("add_exp");
										if (flag7)
										{
											a3_BagItemData3.equipdata.add_exp = this.dicEquip[num]["add_exp"]._int;
										}
										bool flag8 = this.dicEquip[num].ContainsKey("stage");
										if (flag8)
										{
											a3_BagItemData3.equipdata.stage = this.dicEquip[num]["stage"]._int;
										}
										bool flag9 = this.dicEquip[num].ContainsKey("blessing_lv");
										if (flag9)
										{
											a3_BagItemData3.equipdata.blessing_lv = this.dicEquip[num]["blessing_lv"]._int;
										}
										bool flag10 = this.dicEquip[num].ContainsKey("combpt");
										if (flag10)
										{
											a3_BagItemData3.equipdata.combpt = this.dicEquip[num]["combpt"]._int;
										}
										a3_BagItemData3.equipdata.subjoin_att = new Dictionary<int, int>();
										a3_BagItemData3.equipdata.subjoin_att.Clear();
										bool flag11 = this.dicEquip[num].ContainsKey("subjoin_att");
										if (flag11)
										{
											foreach (Variant current in this.dicEquip[num]["subjoin_att"]._arr)
											{
												int key = current["att_type"];
												int value = current["att_value"];
												a3_BagItemData3.equipdata.subjoin_att[key] = value;
											}
										}
										a3_BagItemData3.equipdata.gem_att = new Dictionary<int, int>();
										a3_BagItemData3.equipdata.gem_att.Clear();
										bool flag12 = this.dicEquip[num].ContainsKey("gem_att");
										if (flag12)
										{
											Variant variant = this.dicEquip[num]["gem_att"];
											foreach (Variant current2 in variant._arr)
											{
												int key2 = current2["att_type"];
												int value2 = current2["att_value"];
												a3_BagItemData3.equipdata.gem_att[key2] = value2;
											}
										}
										ArrayList arrayList3 = new ArrayList();
										arrayList3.Add(a3_BagItemData3);
										arrayList3.Add(equip_tip_type.Comon_tip);
										InterfaceMgr.getInstance().open(InterfaceMgr.A3_EQUIPTIP, arrayList3, false);
									}
								}
							}
						}
						else
						{
							uint num;
							uint.TryParse(array[1], out num);
							a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(num);
							bool flag13 = itemDataById.item_type == 1;
							if (flag13)
							{
								a3_BagItemData a3_BagItemData4 = default(a3_BagItemData);
								a3_BagItemData4.num = 0;
								a3_BagItemData4.confdata = itemDataById;
								ArrayList arrayList4 = new ArrayList();
								arrayList4.Add(a3_BagItemData4);
								arrayList4.Add(equip_tip_type.Chat_tip);
								InterfaceMgr.getInstance().open(InterfaceMgr.A3_ITEMTIP, arrayList4, false);
							}
							else
							{
								uint @uint = this.dicEquip[num]["tpid"]._uint;
								a3_ItemData itemDataById2 = ModelBase<a3_BagModel>.getInstance().getItemDataById(@uint);
								bool flag14 = itemDataById2.item_type == 1;
								if (flag14)
								{
									a3_BagItemData a3_BagItemData5 = default(a3_BagItemData);
									a3_BagItemData5.num = 0;
									a3_BagItemData5.confdata = itemDataById2;
									ArrayList arrayList5 = new ArrayList();
									arrayList5.Add(a3_BagItemData5);
									arrayList5.Add(equip_tip_type.Chat_tip);
									InterfaceMgr.getInstance().open(InterfaceMgr.A3_ITEMTIP, arrayList5, false);
								}
							}
						}
					}
					else
					{
						bool singlePanel2 = singlePanel;
						if (singlePanel2)
						{
							ArrayList arrayList6 = new ArrayList();
							arrayList6.Add(array[1].ToString());
							arrayList6.Add(uint.Parse(array[2]));
							InterfaceMgr.getInstance().open(InterfaceMgr.A3_INTERACTOTHERUI, arrayList6, false);
						}
						else
						{
							this.panel4Player.transform.gameObject.SetActive(true);
							this.panel4Player.txt_name.text = array[1].ToString();
							this.panel4Player.name = array[1].ToString();
							this.panel4Player.cid = uint.Parse(array[2]);
						}
					}
				}
			};
		}

		private Color getWordColor()
		{
			Color result = Color.white;
			switch (this.chatToType)
			{
			case ChatToType.All:
				result = Color.yellow;
				break;
			case ChatToType.Nearby:
				result = new Color(254f, 254f, 254f);
				break;
			case ChatToType.World:
				result = Color.yellow;
				break;
			case ChatToType.Legion:
				result = Color.green;
				break;
			case ChatToType.Team:
				result = Color.blue;
				break;
			case ChatToType.Whisper:
				result = Color.magenta;
				break;
			case ChatToType.SystemMsg:
				result = Color.red;
				break;
			case ChatToType.PrivateSecretlanguage:
				result = Color.magenta;
				break;
			}
			return result;
		}

		private void onBtnPlayerWhisperClick(uint cid, string name)
		{
			this.panel4Player.name = name;
			this.panel4Player.cid = cid;
			this.mWhisperName = name;
			this.iptf_msg.text = name + "/";
			whisper.mainBody.gameObject.SetActive(false);
			this.chatToType = ChatToType.PrivateSecretlanguage;
			this.setChatToPanel();
			this.chatToType = ChatToType.Whisper;
			this.createWhisperPanel(name);
		}

		private void createWhisperPanel(string name)
		{
			bool flag = !this.dicWhisper.ContainsKey(name);
			if (flag)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefabWhisperPanel.gameObject);
				gameObject.transform.SetParent(whisper.panelsSecretlanguage, false);
				gameObject.SetActive(true);
				gameObject.transform.localPosition = Vector3.zero;
				this.dicWhisper.Add(name, gameObject.transform);
			}
			else
			{
				this.dicWhisper[name].gameObject.SetActive(true);
			}
		}

		private void onBtnNewInfo(GameObject go)
		{
		}

		private void onAllClick(bool b)
		{
			if (b)
			{
				this.chatToType = ChatToType.All;
				this.setCurrentChatTo();
			}
		}

		private void onWorldClick(bool b)
		{
			if (b)
			{
				this.chatToType = ChatToType.World;
				this.setCurrentChatTo();
			}
		}

		private void hideMinChatTo()
		{
			this.txtCurrentChat.text = this.getNameByChatTo(this.chatToType);
			bool flag = this.chatToType != ChatToType.Whisper && this.chatToType != ChatToType.PrivateSecretlanguage;
			if (flag)
			{
				bool flag2 = !this.ipt_msgChanged;
				if (flag2)
				{
					Vector2 sizeDelta = this.ipt_msg.GetComponent<RectTransform>().sizeDelta;
					this.ipt_msgChanged = true;
				}
			}
			else
			{
				bool flag3 = this.ipt_msgChanged;
				if (flag3)
				{
					Vector2 sizeDelta2 = this.ipt_msg.GetComponent<RectTransform>().sizeDelta;
					this.ipt_msgChanged = false;
				}
			}
			bool flag4 = this.chatToType != ChatToType.Whisper;
			if (flag4)
			{
				bool activeSelf = this.PanelFriends.gameObject.activeSelf;
				if (activeSelf)
				{
					this.PanelFriends.gameObject.SetActive(false);
				}
				bool flag5 = this.isWhisper;
				if (flag5)
				{
					this.isWhisper = false;
				}
			}
			else
			{
				bool flag6 = !this.isWhisper;
				if (flag6)
				{
					bool flag7 = this.chatToType == ChatToType.Whisper;
					if (flag7)
					{
						bool flag8 = !this.PanelFriends.gameObject.activeSelf;
						if (flag8)
						{
							this.PanelFriends.gameObject.SetActive(true);
						}
						this.isWhisper = true;
					}
				}
			}
		}

		private void onNearbyClick(bool b)
		{
			if (b)
			{
				this.chatToType = ChatToType.Nearby;
				this.setCurrentChatTo();
			}
		}

		private void onLegionClick(bool b)
		{
			if (b)
			{
				this.chatToType = ChatToType.Legion;
				this.setCurrentChatTo();
			}
		}

		private void onTeamClick(bool b)
		{
			if (b)
			{
				this.chatToType = ChatToType.Team;
				this.setCurrentChatTo();
			}
		}

		private void onSecretLanguageClick(bool b)
		{
			if (b)
			{
				this.chatToType = ChatToType.Whisper;
				this.setCurrentChatTo();
			}
		}

		private void onMinWorldClick(bool b)
		{
			this.chatToType = ChatToType.World;
			this.hideMinChatTo();
		}

		private void onMinSecretLanguageClick(bool b)
		{
			this.chatToType = ChatToType.Whisper;
			this.hideMinChatTo();
		}

		private void onMinTeamClick(bool b)
		{
			this.chatToType = ChatToType.Team;
			this.hideMinChatTo();
		}

		private void onMinLegionClick(bool b)
		{
			this.chatToType = ChatToType.Legion;
			this.hideMinChatTo();
		}

		private void onMinNearbyClick(bool b)
		{
			this.chatToType = ChatToType.Nearby;
			this.hideMinChatTo();
		}

		private void onPanel4PlayerTouchBgClick(GameObject go)
		{
			bool activeSelf = this.panel4Player.transform.gameObject.activeSelf;
			if (activeSelf)
			{
				this.panel4Player.transform.gameObject.SetActive(false);
			}
			else
			{
				this.panel4Player.transform.gameObject.SetActive(true);
			}
		}

		private void onBtnSeePlayerInfo(GameObject go)
		{
			bool activeSelf = this.panel4Player.transform.gameObject.activeSelf;
			if (activeSelf)
			{
				this.panel4Player.transform.gameObject.SetActive(false);
			}
			ArrayList arrayList = new ArrayList();
			arrayList.Add(this.panel4Player.cid);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_TARGETINFO, arrayList, false);
		}

		private void onBtnAddFriendClick(GameObject go)
		{
			bool activeSelf = this.panel4Player.transform.gameObject.activeSelf;
			if (activeSelf)
			{
				this.panel4Player.transform.gameObject.SetActive(false);
			}
			BaseProxy<FriendProxy>.getInstance().sendAddFriend(this.panel4Player.cid, this.panel4Player.name, true);
		}

		private void onPrivateChatClick(GameObject go)
		{
			bool activeSelf = this.panel4Player.transform.gameObject.activeSelf;
			if (activeSelf)
			{
				this.panel4Player.transform.gameObject.SetActive(false);
			}
			this.chatToType = ChatToType.Whisper;
			this.setChatToButtonGroup();
			this.hideMinChatTo();
			this.setChatToPanel();
			bool flag = !this.PanelFriends.gameObject.activeSelf;
			if (flag)
			{
				this.PanelFriends.gameObject.SetActive(true);
			}
			this.chatToType = ChatToType.Whisper;
			this.createWhisperPanel(this.panel4Player.name);
			this.mWhisperName = this.panel4Player.name;
			this.iptf_msg.text = this.mWhisperName + "/";
		}

		private void onWhisperbtnClick(GameObject go)
		{
			bool flag = !whisper.mainBody.gameObject.activeSelf;
			if (flag)
			{
				whisper.mainBody.gameObject.SetActive(true);
			}
			else
			{
				whisper.mainBody.gameObject.SetActive(false);
			}
		}

		private void onPanelQuickTalkClick(GameObject go)
		{
			PanelQuickTalk.root.gameObject.SetActive(false);
			InputField expr_18 = this.iptf_msg;
			expr_18.text += go.transform.FindChild("Text").GetComponent<Text>().text;
		}

		private void setCurrentChatTo()
		{
			this.setChatToButtonGroup();
			this.setChatToPanel();
			this.hideMinChatTo();
		}

		private void setChatToButtonGroup()
		{
			foreach (KeyValuePair<ChatToType, Transform> current in this.chatToButtons)
			{
				bool flag = current.Key == this.chatToType;
				if (flag)
				{
					current.Value.GetComponent<Toggle>().isOn = true;
				}
			}
			this.txtCurrentChat.text = this.getNameByChatTo(this.chatToType);
		}

		private string getNameByChatTo(ChatToType chatTo)
		{
			string result = string.Empty;
			switch (chatTo)
			{
			case ChatToType.All:
			case ChatToType.World:
				result = "世界";
				break;
			case ChatToType.Nearby:
				result = "附近";
				break;
			case ChatToType.Legion:
				result = "军团";
				break;
			case ChatToType.Team:
				result = "队伍";
				break;
			case ChatToType.Whisper:
				result = "密语";
				break;
			case ChatToType.PrivateSecretlanguage:
				result = "密语";
				break;
			}
			return result;
		}

		private void setChatToPanel()
		{
			foreach (KeyValuePair<ChatToType, Transform> current in this.chatToPanels)
			{
				bool flag = current.Key == this.chatToType;
				if (flag)
				{
					bool flag2 = !current.Value.gameObject.activeSelf;
					if (flag2)
					{
						current.Value.gameObject.SetActive(true);
					}
				}
				else
				{
					bool activeSelf = current.Value.gameObject.activeSelf;
					if (activeSelf)
					{
						current.Value.gameObject.SetActive(false);
					}
				}
			}
			foreach (KeyValuePair<string, Transform> current2 in this.dicWhisper)
			{
				current2.Value.gameObject.SetActive(false);
			}
		}

		private string getChatToStr(ChatToType ctp)
		{
			string result = string.Empty;
			bool flag = this.chatToType != ctp;
			if (flag)
			{
				ctp = this.chatToType;
			}
			switch (ctp)
			{
			case ChatToType.All:
				result = "[世界]";
				break;
			case ChatToType.Nearby:
				result = "[附近]";
				break;
			case ChatToType.World:
				result = "[世界]";
				break;
			case ChatToType.Legion:
				result = "[军团]";
				break;
			case ChatToType.Team:
				result = "[队伍]";
				break;
			case ChatToType.Whisper:
				result = "[密语]";
				break;
			case ChatToType.SystemMsg:
				result = "[系统]";
				break;
			}
			return result;
		}

		public void LoadBagItems()
		{
			this.itemsGameObject.Clear();
			Dictionary<uint, a3_BagItemData> items = ModelBase<a3_BagModel>.getInstance().getItems(false);
			this.itemsCount = items.Count;
			this.listPageItem.Clear();
			List<a3_BagItemData> list = new List<a3_BagItemData>();
			list.Clear();
			foreach (a3_BagItemData current in items.Values)
			{
				list.Add(current);
			}
			this.pageCount = Mathf.CeilToInt((float)this.itemsCount / (float)this.onePageItemCount);
			for (int i = 0; i < this.pageCount; i++)
			{
				this.listPageItem.Add(this.CreatePageItem(this.bagPanelContent));
			}
			string[,] array = new string[2, this.itemsCount];
			for (int j = 0; j < this.pageCount; j++)
			{
				List<GameObject> list2 = new List<GameObject>();
				list2.Clear();
				int num = j * this.onePageItemCount;
				while (num < j * this.onePageItemCount + this.onePageItemCount && num < this.itemsCount)
				{
					list2.Add(this.CreateItemIcon(list[num], false));
					array[0, num] = list[num].confdata.item_name;
					bool isEquip = list[num].isEquip;
					if (isEquip)
					{
						array[1, num] = string.Format("战斗力:{0}", list[num].equipdata.combpt.ToString());
					}
					else
					{
						bool isSummon = list[num].isSummon;
						if (!isSummon)
						{
							array[1, num] = string.Format("使用等级:{0}", list[num].confdata.use_lv);
						}
					}
					num++;
				}
				this.listPageItem[j].Init(list2, this.itemEPrefab, "ItemBorder/Item", array);
			}
			LPageItem.ResetPage();
			LScrollPageMark lScrollPageMark = (this.toggleGroup.transform.GetComponent<LScrollPageMark>() == null) ? this.toggleGroup.gameObject.AddComponent<LScrollPageMark>() : this.toggleGroup.transform.GetComponent<LScrollPageMark>();
			lScrollPageMark.scrollPage = this.bagPanelLScrollPage;
			lScrollPageMark.toggleGroup = lScrollPageMark.gameObject.GetComponent<ToggleGroup>();
			lScrollPageMark.togglePrefab = lScrollPageMark.gameObject.transform.FindChild("Toggle").GetComponent<Toggle>();
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(lScrollPageMark.gameObject);
			gameObject.gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.SetParent(this.bagToggle, false);
			this.itemsGameObject.Add(gameObject.gameObject);
		}

		public void LoadBodyItems()
		{
			this.itemsGameObject.Clear();
			Dictionary<int, a3_BagItemData> equipsByType = ModelBase<a3_EquipModel>.getInstance().getEquipsByType();
			this.itemsCount = equipsByType.Count;
			this.listPageItem.Clear();
			List<a3_BagItemData> list = new List<a3_BagItemData>();
			list.Clear();
			foreach (a3_BagItemData current in equipsByType.Values)
			{
				list.Add(current);
			}
			this.pageCount = Mathf.CeilToInt((float)this.itemsCount / (float)this.onePageItemCount);
			for (int i = 0; i < this.pageCount; i++)
			{
				this.listPageItem.Add(this.CreatePageItem(this.bodyPanelContent));
			}
			for (int j = 0; j < this.pageCount; j++)
			{
				List<GameObject> list2 = new List<GameObject>();
				string[,] array = new string[2, this.itemsCount];
				list2.Clear();
				int num = j * this.onePageItemCount;
				while (num < j * this.onePageItemCount + this.onePageItemCount && num < this.itemsCount)
				{
					list2.Add(this.CreateItemIcon(list[num], true));
					array[0, num] = list[num].confdata.item_name;
					bool isEquip = list[num].isEquip;
					if (isEquip)
					{
						array[1, num] = string.Format("战斗力:{0}", list[num].equipdata.combpt.ToString());
					}
					else
					{
						bool isSummon = list[num].isSummon;
						if (!isSummon)
						{
							array[1, num] = string.Format("使用等级:{0}", list[num].confdata.use_lv);
						}
					}
					num++;
				}
				this.listPageItem[j].Init(list2, this.itemEPrefab, "ItemBorder/Item", array);
			}
			LPageItem.ResetPage();
			LScrollPageMark lScrollPageMark = (this.toggleGroup.gameObject.GetComponent<LScrollPageMark>() == null) ? this.toggleGroup.gameObject.AddComponent<LScrollPageMark>() : this.toggleGroup.gameObject.GetComponent<LScrollPageMark>();
			lScrollPageMark.scrollPage = this.bodyPanelLScrollPage;
			lScrollPageMark.toggleGroup = lScrollPageMark.gameObject.GetComponent<ToggleGroup>();
			lScrollPageMark.togglePrefab = lScrollPageMark.gameObject.transform.FindChild("Toggle").GetComponent<Toggle>();
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(lScrollPageMark.gameObject);
			gameObject.gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.SetParent(this.bodyToggle, false);
			this.itemsGameObject.Add(gameObject.gameObject);
		}

		public void LoadPetItems()
		{
		}

		public void loadItem()
		{
			this.LoadBodyItems();
			this.LoadBagItems();
			this.LoadPetItems();
			bool flag = this.isFirstTimeOpen;
			if (flag)
			{
				this.onBtnInBodyClick(null);
				this.isFirstTimeOpen = false;
			}
		}

		private LPageItem CreatePageItem(Transform parent)
		{
			LPageItem original = this.prefabPageItem.gameObject.GetComponent<LPageItem>() ?? this.prefabPageItem.gameObject.AddComponent<LPageItem>();
			LPageItem lPageItem = UnityEngine.Object.Instantiate<LPageItem>(original);
			lPageItem.gameObject.SetActive(true);
			lPageItem.transform.SetParent(parent, false);
			lPageItem.transform.localScale = Vector3.one;
			lPageItem.transform.localPosition = Vector3.zero;
			lPageItem.prefabItemParent = lPageItem.transform.FindChild("Panel");
			this.itemsGameObject.Add(lPageItem.gameObject);
			return lPageItem;
		}

		private GameObject CreateItemIcon(a3_BagItemData data, bool ishouse = false)
		{
			GameObject gameObject = IconImageMgr.getInstance().createA3ItemIcon(data, false, data.num, 1f, false);
			gameObject.GetComponent<Image>().raycastTarget = false;
			gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * 0.8f, gameObject.transform.localScale.y * 0.8f, gameObject.transform.localScale.z * 0.8f);
			bool flag = data.num <= 1;
			if (flag)
			{
				gameObject.transform.FindChild("num").gameObject.SetActive(false);
			}
			GameObject iconClick = UnityEngine.Object.Instantiate<GameObject>(base.transform.FindChild("template/ClickArea").gameObject);
			iconClick.name = "ClickArea";
			iconClick.transform.SetParent(gameObject.transform, false);
			BaseButton baseButton = new BaseButton(iconClick.transform, 1, 1);
			baseButton.onClick = delegate(GameObject go)
			{
				this.onItemClick(iconClick, data.id, ishouse);
			};
			this.itemsGameObject.Add(gameObject);
			return gameObject;
		}

		private void onItemClick(GameObject go, uint id, bool ishouse)
		{
			bool flag = this.currentDisplayEquipCount == 3;
			if (flag)
			{
				flytxt.instance.fly("最多只能展示三件物品", 0, default(Color), null);
			}
			else if (ishouse)
			{
				a3_BagItemData data = ModelBase<a3_EquipModel>.getInstance().getEquips()[id];
				InputField inputField = this.iptf_msg;
				inputField.text = string.Concat(new object[]
				{
					inputField.text,
					"[",
					ModelBase<a3_BagModel>.getInstance().getEquipName(data),
					"#",
					id,
					"]"
				});
			}
			else
			{
				uint tpid = ModelBase<a3_BagModel>.getInstance().getItems(false)[id].tpid;
				uint id2 = ModelBase<a3_BagModel>.getInstance().getItems(false)[id].id;
				InputField inputField = this.iptf_msg;
				inputField.text = string.Concat(new object[]
				{
					inputField.text,
					"[",
					ModelBase<a3_BagModel>.getInstance().getItemDataById(tpid).item_name,
					"#",
					id,
					"]"
				});
			}
		}

		private void Update()
		{
			bool flag = this.OutTimer > 0f;
			if (flag)
			{
				this.OutTimer -= Time.deltaTime;
			}
			bool flag2 = this.delayExcuteShowTime > 0f;
			if (flag2)
			{
				this.delayExcuteShowTime -= Time.deltaTime;
			}
			else
			{
				for (int i = 0; i < this.AllItemMsg.Count; i++)
				{
					bool flag3 = this.AllItemMsg[i].position.y < this.dummyTop + this.BorderMaxValue && this.AllItemMsg[i].position.y > this.dummyEnd - this.BorderMaxValue;
					if (flag3)
					{
						bool flag4 = !this.AllItemMsg[i].gameObject.activeSelf;
						if (flag4)
						{
							this.AllItemMsg[i].gameObject.SetActive(true);
						}
					}
					else
					{
						bool activeSelf = this.AllItemMsg[i].gameObject.activeSelf;
						if (activeSelf)
						{
							this.AllItemMsg[i].gameObject.SetActive(false);
						}
					}
				}
				this.delayExcuteShowTime = 0.3f;
			}
		}

		public a3_chatroom()
		{
			Dictionary<ChatToType, bool> expr_7C = new Dictionary<ChatToType, bool>();
			expr_7C[ChatToType.World] = false;
			expr_7C[ChatToType.Legion] = false;
			expr_7C[ChatToType.Team] = false;
			expr_7C[ChatToType.Whisper] = false;
			this.ignoreVoiceStat = expr_7C;
			Dictionary<ChatToType, bool> expr_AB = new Dictionary<ChatToType, bool>();
			expr_AB[ChatToType.Nearby] = false;
			expr_AB[ChatToType.Legion] = false;
			expr_AB[ChatToType.Team] = false;
			expr_AB[ChatToType.Whisper] = false;
			this.ignoreChatStat = expr_AB;
			this.currentDisplayEquipCount = 0;
			this.voicetimer = 0L;
			this.isLoaded = false;
			this.iptf_msgStr = string.Empty;
			this.OutTimer = 0f;
			this.isMeSay = false;
			this.createOnceInExpbar = true;
			this.rollWordStrList = new List<msg4roll>();
			this.LRichTextWidth = 0;
			Dictionary<ChatToType, bool> expr_126 = new Dictionary<ChatToType, bool>();
			expr_126[ChatToType.All] = false;
			expr_126[ChatToType.Legion] = false;
			expr_126[ChatToType.Nearby] = false;
			expr_126[ChatToType.Team] = false;
			expr_126[ChatToType.World] = false;
			expr_126[ChatToType.Whisper] = false;
			expr_126[ChatToType.PrivateSecretlanguage] = false;
			this.IsMsgOutOfRange = expr_126;
			this.itemIndex = 0u;
			this.ipt_msgChanged = false;
			this.isWhisper = false;
			this.listPageItem = new List<LPageItem>();
			this.itemsGameObject = new List<GameObject>();
			this.itemsCount = 0;
			this.onePageItemCount = 6;
			this.pageCount = 0;
			this.isFirstTimeOpen = true;
			this.delayExcuteShowTime = 0.3f;
			this.BorderMaxValue = 600f;
			base..ctor();
		}
	}
}
