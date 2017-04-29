using System;

public class err_string
{
	public static string get_Err_String(int err_code)
	{
		int num = err_code;
		string result;
		if (num <= -5000)
		{
			if (num <= -7501)
			{
				if (num <= -8100)
				{
					if (num <= -8506)
					{
						if (num == -9001)
						{
							result = "魔法值已满";
							return result;
						}
						if (num == -9000)
						{
							result = "生命值已满";
							return result;
						}
						switch (num)
						{
						case -8516:
							result = "达到今日购买的上限";
							return result;
						case -8515:
							result = "已经卖光";
							return result;
						case -8512:
							result = "宠物饱腹度已满";
							return result;
						case -8510:
							result = "宠物饱腹度为0    ";
							return result;
						case -8509:
							result = "宠物进阶失败";
							return result;
						case -8508:
							result = "宠物升阶道具数量错误";
							return result;
						case -8507:
							result = "宠物等级太低";
							return result;
						case -8506:
							result = "宠物阶数配置未找到";
							return result;
						}
					}
					else if (num <= -8300)
					{
						switch (num)
						{
						case -8407:
							result = "翅膀已达最高阶";
							return result;
						case -8406:
							result = "道具数量错误（太少、太多或者不符合步进）";
							return result;
						case -8405:
							result = "翅膀等级太低";
							return result;
						case -8404:
							result = "翅膀等级配置错误";
							return result;
						case -8403:
							result = "翅膀已到最高等级";
							return result;
						case -8402:
							result = "翅膀阶数错误";
							return result;
						case -8401:
							result = "翅膀未解锁";
							return result;
						default:
							switch (num)
							{
							case -8308:
								result = "无效的兑换码";
								return result;
							case -8307:
								result = "VIP已到达最大等级";
								return result;
							case -8306:
								result = "没有可用的道具卡";
								return result;
							case -8305:
								result = "未击败宝藏守护队伍";
								return result;
							case -8304:
								result = "卡类型错误";
								return result;
							case -8303:
								result = "季卡和年卡不能同时拥有";
								return result;
							case -8302:
								result = "已结是年卡用户";
								return result;
							case -8301:
								result = "已结是季卡用户";
								return result;
							case -8300:
								result = "已结是月卡用户";
								return result;
							}
							break;
						}
					}
					else
					{
						switch (num)
						{
						case -8213:
							result = "守护队伍已全部阵亡";
							return result;
						case -8212:
							result = "玩家不再守护宝藏";
							return result;
						case -8211:
							result = "不是你的宝藏";
							return result;
						case -8210:
							result = "英雄非空闲";
							return result;
						case -8209:
							result = "宝藏被使用中";
							return result;
						case -8208:
							result = "宝藏守护队伍已满";
							return result;
						case -8207:
							result = "协助守护有效时间已过";
							return result;
						case -8206:
							result = "已经在宝藏守护队伍中";
							return result;
						case -8205:
							result = "宝藏已改变";
							return result;
						case -8204:
							result = "不在同一个家族";
							return result;
						case -8203:
							result = "不是空闲英雄";
							return result;
						case -8202:
							result = "没有指派英雄";
							return result;
						case -8201:
							result = "可拥有宝藏上限";
							return result;
						case -8200:
							result = "家族等级不足";
							return result;
						default:
							switch (num)
							{
							case -8109:
								result = "英雄正在守护宝藏";
								return result;
							case -8108:
								result = "没有足够的经验";
								return result;
							case -8107:
								result = "时装部位已到最高等级";
								return result;
							case -8106:
								result = "时装已激活";
								return result;
							case -8105:
								result = "部位未激活时装";
								return result;
							case -8104:
								result = "商店剩余数量不足";
								return result;
							case -8103:
								result = "没有该时装";
								return result;
							case -8102:
								result = "消息不能为空";
								return result;
							case -8101:
								result = "名字未改变";
								return result;
							case -8100:
								result = "名字过长";
								return result;
							}
							break;
						}
					}
				}
				else if (num <= -7900)
				{
					if (num == -8001)
					{
						result = "VIP未激活";
						return result;
					}
					if (num == -8000)
					{
						result = "体力购买次数不足";
						return result;
					}
					switch (num)
					{
					case -7906:
						result = "挑战购买次数已用完";
						return result;
					case -7905:
						result = "荣誉商店中无此道具";
						return result;
					case -7904:
						result = "没有相应的比武场结算奖励配置";
						return result;
					case -7903:
						result = "没有可以领取的比武场结算奖励";
						return result;
					case -7902:
						result = "玩家不存在";
						return result;
					case -7901:
						result = "对方不在挑战列表中";
						return result;
					case -7900:
						result = "今天可挑战次数已满，无需购买";
						return result;
					}
				}
				else if (num <= -7700)
				{
					switch (num)
					{
					case -7803:
						result = "成就点数不足";
						return result;
					case -7802:
						result = "成就奖励已领取";
						return result;
					case -7801:
						result = "成就未完成";
						return result;
					case -7800:
						result = "没有该成就";
						return result;
					default:
						if (num == -7700)
						{
							result = "神兵等级不能超过玩家等级";
							return result;
						}
						break;
					}
				}
				else
				{
					if (num == -7600)
					{
						result = "荣誉点数不够";
						return result;
					}
					switch (num)
					{
					case -7504:
						result = "今日元宝刷新次数超限";
						return result;
					case -7503:
						result = "道具已经售罄";
						return result;
					case -7502:
						result = "道具未刷新到";
						return result;
					case -7501:
						result = "免费刷新时间未到";
						return result;
					}
				}
			}
			else if (num <= -6700)
			{
				if (num <= -7200)
				{
					switch (num)
					{
					case -7415:
						result = "携带英雄等级太低";
						return result;
					case -7414:
						result = "已到本难度的最高级";
						return result;
					case -7413:
						result = "无法在已选择难度的情况下扫荡别的副本";
						return result;
					case -7412:
						result = "该难度未通关过，无法扫荡";
						return result;
					case -7411:
						result = "没有找到相关的可领取奖励的配置";
						return result;
					case -7410:
						result = "没有可领取的奖励";
						return result;
					case -7409:
						result = "无需重置";
						return result;
					case -7408:
						result = "玩家等级太低，无法开启或进入蜃楼";
						return result;
					case -7407:
						result = "重置次数已达vip上限";
						return result;
					case -7406:
						result = "尚未拥有已选择的英雄";
						return result;
					case -7405:
						result = "已阵亡的英雄无法出战";
						return result;
					case -7404:
						result = "对手cid错误";
						return result;
					case -7403:
						result = "请先完成之前的难度";
						return result;
					case -7402:
						result = "请先领取上一关卡的奖励";
						return result;
					case -7401:
						result = "蜃楼关卡数错误";
						return result;
					case -7400:
						result = "蜃楼难度错误";
						return result;
					default:
						switch (num)
						{
						case -7305:
							result = "无法购买限时抢购物品";
							return result;
						case -7304:
							result = "开服活动发生错误";
							return result;
						case -7303:
							break;
						case -7302:
							result = "该开服活动时间已过";
							return result;
						case -7301:
							result = "条件未达成，无法领取开服活动奖励";
							return result;
						case -7300:
							result = "已经领取过该开服活动的奖励";
							return result;
						default:
							if (num == -7200)
							{
								result = "助战令数量不够";
								return result;
							}
							break;
						}
						break;
					}
				}
				else if (num <= -7000)
				{
					if (num == -7100)
					{
						result = "排行榜不存在";
						return result;
					}
					switch (num)
					{
					case -7003:
						result = "技能等级不能超过玩家等级";
						return result;
					case -7002:
						result = "技能已达最高等级";
						return result;
					case -7001:
						result = "技能配置不存在";
						return result;
					case -7000:
						result = "技能尚未学习";
						return result;
					}
				}
				else
				{
					if (num == -6701)
					{
						result = "所选路线军团等级不足";
						return result;
					}
					if (num == -6700)
					{
						result = "今日已经完成押镖";
						return result;
					}
				}
			}
			else if (num <= -6200)
			{
				switch (num)
				{
				case -6604:
					result = "队员有未完成的狩猎";
					return result;
				case -6603:
					result = "前有未完成的狩猎";
					return result;
				case -6602:
					result = "狩猎次数不足";
					return result;
				case -6601:
					result = "队员狩猎次数不足";
					return result;
				case -6600:
					result = "不是队长不能开启任务";
					return result;
				default:
					switch (num)
					{
					case -6406:
						result = "商品不再列表中";
						return result;
					case -6405:
						result = "不能购买自己上架的商品";
						return result;
					case -6404:
						result = "商品已经过期";
						return result;
					case -6403:
						result = "没有找到上架的商品";
						return result;
					case -6402:
						result = "没有上架的商品";
						return result;
					case -6401:
						result = "上架数量超过限制";
						return result;
					case -6400:
						result = "绑定道具不能上架";
						return result;
					default:
						switch (num)
						{
						case -6216:
							result = "召唤兽出战cd中";
							return result;
						case -6215:
							result = "召唤兽寿命不足,无法召唤";
							return result;
						case -6214:
							result = "召唤兽该技能栏已有技能";
							return result;
						case -6213:
							result = "召唤兽已经学习过该技能";
							return result;
						case -6212:
							result = "召唤兽列表已满";
							return result;
						case -6211:
							result = "召唤兽在出战状态无法融合";
							return result;
						case -6210:
							result = "召唤兽没有脱下装备无法融合";
							return result;
						case -6209:
							result = "召唤兽血脉不同无法融合";
							return result;
						case -6208:
							result = "召唤兽等级不足无法融合";
							return result;
						case -6207:
							result = "品质不够,无法学习该技能";
							return result;
						case -6206:
							result = "技能栏已满";
							return result;
						case -6205:
							result = "技能尚未学习";
							return result;
						case -6204:
							result = "寿命已达最大值";
							return result;
						case -6203:
							result = "该召唤兽已达最大等级";
							return result;
						case -6201:
							result = "复活达上限";
							return result;
						case -6200:
							result = "该召唤兽已经出战";
							return result;
						}
						break;
					}
					break;
				}
			}
			else if (num <= -6000)
			{
				switch (num)
				{
				case -6109:
					result = "符文升级剩余时间过长，无法免费加速";
					return result;
				case -6108:
					result = "符文不在研究中";
					return result;
				case -6107:
					result = "符文已达最高级";
					return result;
				case -6106:
					result = "符文研究条件未达成";
					return result;
				case -6105:
					result = "该符文正在研究中";
					return result;
				case -6104:
					result = "未开启该符文";
					return result;
				case -6103:
					result = "符文和角色职业不相符";
					return result;
				case -6102:
					result = "符文研究队列已满";
					return result;
				case -6101:
					result = "转生等级不够，没有开启符文系统";
					return result;
				case -6100:
					result = "等级太低，没有开启符文系统";
					return result;
				default:
					switch (num)
					{
					case -6011:
						result = "当前墨家禁地次数已用完";
						return result;
					case -6010:
						result = "墨家禁地购买次数已用完";
						return result;
					case -6009:
						result = "墨家禁地次数已满，无需购买";
						return result;
					case -6008:
						result = "只有墨家禁地能购买次数";
						return result;
					case -6007:
						result = "墨家禁地副本无法重置";
						return result;
					case -6006:
						result = "副本分数不够，无法扫荡";
						return result;
					case -6005:
						result = "今日该副本重置次数已耗尽";
						return result;
					case -6004:
						result = "今日该副本次数已耗尽";
						return result;
					case -6003:
						result = "未通关该副本";
						return result;
					case -6002:
						result = "不在副本内，或副本未找到";
						return result;
					case -6001:
						result = "无法领取副本奖励";
						return result;
					case -6000:
						result = "副本奖励已经领取过了";
						return result;
					}
					break;
				}
			}
			else
			{
				switch (num)
				{
				case -5102:
					result = "没有该道具出售";
					return result;
				case -5101:
					result = "自己购买超过上限";
					return result;
				case -5100:
					result = "全服该道具数量不足";
					return result;
				default:
					switch (num)
					{
					case -5014:
						result = "宠物阶数不能超过主人阶数";
						return result;
					case -5013:
						result = "宠物等级不能超过主人等级";
						return result;
					case -5012:
						result = "无法在比武场切换英雄";
						return result;
					case -5011:
						result = "英雄残卷不足";
						return result;
					case -5010:
						result = "英雄召唤配置错误";
						return result;
					case -5009:
						result = "角色等级不够，无法上阵英雄";
						return result;
					case -5008:
						result = "英雄不可重生";
						return result;
					case -5007:
						result = "宠物已达到本阶的最高等级";
						return result;
					case -5006:
						result = "英雄已达到最高强化等级";
						return result;
					case -5005:
						result = "英雄当前不在阵上";
						return result;
					case -5004:
						result = "英雄已死";
						return result;
					case -5003:
						result = "英雄阵号错误";
						return result;
					case -5002:
						result = "该英雄已经出阵";
						return result;
					case -5001:
						result = "你没有这个英雄";
						return result;
					case -5000:
						result = "已经拥有了该英雄了";
						return result;
					}
					break;
				}
			}
		}
		else if (num <= -1900)
		{
			if (num <= -2262)
			{
				if (num <= -4000)
				{
					if (num == -4200)
					{
						result = "比武场次数过多";
						return result;
					}
					switch (num)
					{
					case -4102:
						result = "道具售价错误";
						return result;
					case -4101:
						result = "超过道具最大堆叠";
						return result;
					case -4100:
						result = "道具合成出错";
						return result;
					default:
						switch (num)
						{
						case -4009:
							result = "你没有该道具";
							return result;
						case -4008:
							result = "存取道具时出错";
							return result;
						case -4007:
							result = "道具不足";
							return result;
						case -4006:
							result = "超过仓库可解锁的最大值";
							return result;
						case -4005:
							result = "超过背包可解锁的最大值";
							return result;
						case -4004:
							result = "体力不足";
							return result;
						case -4003:
							result = "钻石不足";
							return result;
						case -4002:
							result = "背包空余格子数不够";
							return result;
						case -4001:
							result = "背包已满";
							return result;
						case -4000:
							result = "金钱不足";
							return result;
						}
						break;
					}
				}
				else if (num <= -2400)
				{
					switch (num)
					{
					case -3105:
						result = "最大强化等级";
						return result;
					case -3104:
						result = "合成道具数量错误";
						return result;
					case -3103:
						result = "玩家的等级不满足进阶后的装备的等级要求";
						return result;
					case -3102:
						result = "玩家等级不足，装备无法进阶";
						return result;
					case -3101:
						result = "装备不能进阶，无进阶配置";
						return result;
					case -3100:
						result = "装备强化等级不够";
						return result;
					default:
						switch (num)
						{
						case -2406:
							result = "装备无法直接出售";
							return result;
						case -2405:
							result = "该道具不可出售";
							return result;
						case -2404:
							result = "已达到限制购买数量上限";
							return result;
						case -2403:
							result = "剩余可出售数量不足";
							return result;
						case -2402:
							result = "未到打折时间，或打折时间已过";
							return result;
						case -2401:
							result = "未找到相关的打折商品";
							return result;
						case -2400:
							result = "当前没有打折商品出售";
							return result;
						}
						break;
					}
				}
				else
				{
					switch (num)
					{
					case -2302:
						result = "角色已达最高等级";
						return result;
					case -2301:
						result = "已到最大转生等级";
						return result;
					case -2300:
						result = "玩家等级太低，无法转生";
						return result;
					default:
						switch (num)
						{
						case -2266:
							result = "没有升级投资回馈可领";
							return result;
						case -2263:
							result = "没有月投资累积回馈可领";
							return result;
						case -2262:
							result = "没有月投资回馈可领";
							return result;
						}
						break;
					}
				}
			}
			else if (num <= -2111)
			{
				switch (num)
				{
				case -2235:
					result = "未精炼装备不能追加";
					return result;
				case -2234:
					result = "宝石属性值已达到最大";
					return result;
				case -2233:
					result = "追加属性等级已达到最大";
					return result;
				case -2232:
					result = "错误传承条件";
					return result;
				case -2231:
					result = "附加属性已达到最大值";
					return result;
				case -2230:
					result = "不同阶级装备不能合成";
					return result;
				case -2229:
					result = "不同类型装备不能合成";
					return result;
				case -2228:
					result = "装备条件不足";
					return result;
				case -2227:
					result = "装备不可洗练稀有属性";
					return result;
				case -2226:
					result = "需要锁定的稀有属性不存在";
					return result;
				case -2225:
					result = "装备不可替换稀有属性";
					return result;
				case -2224:
					result = "不存在该稀有属性";
					return result;
				case -2223:
					result = "必须是卓越装备才可增加";
					return result;
				case -2222:
					result = "该装备不能增加稀有属性";
					return result;
				case -2221:
					result = "该装备已到达卓越稀有条数限制";
					return result;
				case -2220:
					result = "该道具不能用于增加稀有属性";
					return result;
				case -2219:
				case -2218:
				case -2217:
				case -2216:
				case -2215:
				case -2214:
				case -2213:
				case -2212:
				case -2199:
				case -2198:
				case -2197:
				case -2196:
				case -2195:
				case -2194:
				case -2193:
				case -2192:
				case -2188:
				case -2187:
				case -2186:
				case -2181:
				case -2180:
				case -2179:
				case -2178:
				case -2177:
				case -2176:
				case -2175:
				case -2174:
				case -2173:
				case -2172:
				case -2171:
				case -2169:
				case -2168:
				case -2166:
				case -2163:
				case -2162:
				case -2160:
				case -2158:
				case -2157:
				case -2156:
				case -2155:
					break;
				case -2211:
					result = "红包重新发放冷却中";
					return result;
				case -2210:
					result = "红包生成错误";
					return result;
				case -2209:
					result = "没有可回收的红包";
					return result;
				case -2208:
					result = "红包已领取";
					return result;
				case -2207:
					result = "红包已经领完了";
					return result;
				case -2206:
					result = "红包已过期";
					return result;
				case -2205:
					result = "红包不存在";
					return result;
				case -2204:
					result = "红包份数超过上限了";
					return result;
				case -2203:
					result = "红包份数太少了";
					return result;
				case -2202:
					result = "总金额大于定义的最大金额";
					return result;
				case -2201:
					result = "总金额小于定义的最小金额";
					return result;
				case -2200:
					result = "发放的元宝数量小于元宝份数";
					return result;
				case -2191:
					result = "未达到进入该地图条件";
					return result;
				case -2190:
					result = "进入副本条件不满足";
					return result;
				case -2189:
					result = "已设置该技能";
					return result;
				case -2185:
					result = "上一技能栏位未激活";
					return result;
				case -2184:
					result = "该技能栏位未激活";
					return result;
				case -2183:
					result = "该技能未激活";
					return result;
				case -2182:
					result = "未升级上一等级";
					return result;
				case -2170:
					result = "非同类型同等级的宝石，不可转换";
					return result;
				case -2167:
					result = "等级不足不可领取";
					return result;
				case -2165:
					result = "没有进行过升级投资";
					return result;
				case -2164:
					result = "已经领取月投资累积回馈";
					return result;
				case -2161:
					result = "没有进行过月投资";
					return result;
				case -2159:
					result = "工会未解散";
					return result;
				case -2154:
					result = "帮派贡献不足";
					return result;
				case -2153:
					result = "捐献次数不足";
					return result;
				default:
					switch (num)
					{
					case -2138:
						result = "战斗力不足";
						return result;
					case -2137:
						break;
					case -2136:
						result = "已进入了房间";
						return result;
					case -2135:
						result = "不是房主";
						return result;
					case -2134:
						result = "房间密码错误";
						return result;
					case -2133:
						result = "房间人数已满";
						return result;
					case -2132:
						result = "没有该房间";
						return result;
					case -2131:
						result = "不在房间内";
						return result;
					case -2130:
						result = "没有房间例表信息";
						return result;
					default:
						switch (num)
						{
						case -2116:
							result = "该侠客行副本需求前置任务未完成";
							return result;
						case -2115:
							result = "该侠客行副本只可打一次";
							return result;
						case -2114:
							result = "坐骑最顶级了";
							return result;
						case -2113:
							result = "坐骑未激活";
							return result;
						case -2112:
							result = "物品要求坐骑等阶不符";
							return result;
						case -2111:
							result = "物品不能使用于坐骑升级";
							return result;
						}
						break;
					}
					break;
				}
			}
			else if (num <= -2100)
			{
				if (num == -2101)
				{
					result = "首次vip奖信息未记录";
					return result;
				}
				if (num == -2100)
				{
					result = "已领过首次vip奖";
					return result;
				}
			}
			else
			{
				switch (num)
				{
				case -2005:
					result = "今天的禁言次数已经用完了";
					return result;
				case -2004:
					result = "新手指导员只能禁言30级以下用户";
					return result;
				case -2003:
					result = "GM权限不够";
					return result;
				case -2002:
					result = "GM命令错误";
					return result;
				case -2001:
					result = "非GM";
					return result;
				default:
					switch (num)
					{
					case -1904:
						result = "你没有通过身份认证";
						return result;
					case -1903:
						result = "在跨服战中，不能使用此物品";
						return result;
					case -1902:
						result = "在跨服战中，不能进行此操作";
						return result;
					case -1901:
						result = "跨服战联接断开";
						return result;
					case -1900:
						result = "联接跨服战失败";
						return result;
					}
					break;
				}
			}
		}
		else if (num <= -1700)
		{
			if (num <= -1850)
			{
				switch (num)
				{
				case -1869:
					result = "藏宝阁刷新时间未到";
					return result;
				case -1868:
					result = "不在活动时间内";
					return result;
				case -1867:
					result = "兑换次数不足";
					return result;
				case -1866:
					result = "黄钻能量累计总点数不足";
					return result;
				case -1865:
					result = "黄钻能量不足";
					return result;
				case -1864:
				case -1863:
				case -1862:
					break;
				case -1861:
					result = "洗练点数已使用了";
					return result;
				case -1860:
					result = "转生等级不够";
					return result;
				default:
					if (num == -1851)
					{
						result = "没有该称号";
						return result;
					}
					if (num == -1850)
					{
						result = "你已拥有此称号";
						return result;
					}
					break;
				}
			}
			else if (num <= -1830)
			{
				switch (num)
				{
				case -1842:
					result = "你所处的等级不能进行该膜拜";
					return result;
				case -1841:
					result = "膜拜时间未到";
					return result;
				case -1840:
					result = "你今天的膜拜次数已用完";
					return result;
				default:
					if (num == -1830)
					{
						result = "你已达到最大爵位等级";
						return result;
					}
					break;
				}
			}
			else
			{
				switch (num)
				{
				case -1809:
					result = "你已领取了此奖励";
					return result;
				case -1808:
					result = "你的账号处于安全锁定状态";
					return result;
				case -1807:
					result = "你已开通了帐号保护功能";
					return result;
				case -1806:
					result = "安全密码太长";
					return result;
				case -1805:
					result = "安全密码不能为空";
					return result;
				case -1804:
					result = "没有产生验证信息";
					return result;
				case -1803:
					result = "验证码错误";
					return result;
				case -1802:
					result = "与获取验证码的手机号码不匹配";
					return result;
				case -1801:
					result = "安全密码错误";
					return result;
				case -1800:
					result = "你还没有开通帐号保护功能";
					return result;
				default:
					switch (num)
					{
					case -1732:
						result = "你今天的结缘互动次数已用完";
						return result;
					case -1731:
						result = "已达到结缘最大等级";
						return result;
					case -1730:
						result = "你没有结缘";
						return result;
					case -1728:
						result = "此玩家已经结婚了";
						return result;
					case -1727:
						result = "你已经结婚了";
						return result;
					case -1726:
						result = "没有此申请信息";
						return result;
					case -1725:
						result = "你已经向此玩家申请过了";
						return result;
					case -1724:
						result = "此玩家接收的申请已满";
						return result;
					case -1723:
						result = "此结缘信息已过期";
						return result;
					case -1722:
						result = "不能申请自己发布的结缘";
						return result;
					case -1721:
						result = "你没有发布过结缘";
						return result;
					case -1720:
						result = "你已经发布过结缘了";
						return result;
					case -1719:
						result = "查询过于频繁";
						return result;
					case -1712:
						result = "武魂道具已达到最大等级";
						return result;
					case -1711:
						result = "武魂等级已激活";
						return result;
					case -1710:
						result = "武魂元宝激活次数用尽";
						return result;
					case -1709:
						result = "已装备同类武魂，不能再装备";
						return result;
					case -1708:
						result = "武魂装备空间不足位置受限";
						return result;
					case -1707:
						result = "武魂游戏币猎魂次数用尽";
						return result;
					case -1706:
						result = "武魂等级不足";
						return result;
					case -1705:
						result = "武魂品质不足";
						return result;
					case -1704:
						result = "武魂背包已没有扩充空间";
						return result;
					case -1703:
						result = "武魂不存在";
						return result;
					case -1702:
						result = "武魂等级未激活";
						return result;
					case -1701:
						result = "武魂掉落空间不足";
						return result;
					case -1700:
						result = "武魂背包空间不足";
						return result;
					}
					break;
				}
			}
		}
		else if (num <= -401)
		{
			switch (num)
			{
			case -1630:
				result = "已经领取过该连胜奖励了";
				return result;
			case -1629:
				result = "每周荣誉奖励配置不存在";
				return result;
			case -1628:
				result = "已经领取每周荣誉榜奖励";
				return result;
			case -1627:
				result = "未上每周荣誉榜";
				return result;
			case -1626:
				result = "该苍穹目标尚未达成";
				return result;
			case -1625:
				result = "已领取该苍穹目标分享奖励";
				return result;
			case -1624:
				result = "已领取今日必做分享奖励";
				return result;
			case -1623:
				result = "符合条件的已邀请好友数量不足";
				return result;
			case -1622:
				result = "该好友邀请奖励已获取过了";
				return result;
			case -1621:
				result = "今天已经领取过邀请奖励了";
				return result;
			case -1620:
				result = "今天已经领取过登陆奖励了";
				return result;
			case -1619:
				result = "已经领取过该签到奖励了";
				return result;
			case -1618:
				result = "今天已经签过到了";
				return result;
			case -1617:
				result = "没有补签次数";
				return result;
			case -1616:
			case -1615:
			case -1614:
			case -1613:
			case -1612:
			case -1611:
			case -1610:
			case -1609:
			case -1608:
			case -1607:
			case -1606:
			case -1605:
			case -1604:
			case -1603:
			case -1599:
			case -1598:
			case -1597:
			case -1596:
			case -1595:
			case -1594:
			case -1593:
			case -1592:
			case -1591:
			case -1590:
			case -1589:
			case -1588:
			case -1587:
			case -1586:
			case -1585:
			case -1584:
			case -1583:
			case -1582:
			case -1581:
			case -1580:
			case -1579:
			case -1578:
			case -1577:
			case -1576:
			case -1575:
			case -1574:
			case -1573:
			case -1572:
			case -1571:
			case -1570:
			case -1569:
			case -1549:
			case -1548:
			case -1547:
			case -1546:
			case -1545:
			case -1544:
			case -1543:
			case -1542:
			case -1541:
			case -1540:
			case -1539:
			case -1538:
			case -1537:
			case -1536:
			case -1535:
			case -1534:
			case -1533:
			case -1532:
			case -1531:
			case -1530:
			case -1529:
			case -1528:
			case -1527:
			case -1526:
			case -1525:
			case -1524:
			case -1523:
			case -1522:
			case -1521:
			case -1519:
			case -1518:
			case -1517:
			case -1516:
			case -1515:
			case -1514:
			case -1499:
			case -1498:
			case -1497:
			case -1496:
			case -1495:
			case -1494:
			case -1493:
			case -1492:
			case -1491:
			case -1490:
			case -1489:
			case -1488:
			case -1487:
			case -1486:
			case -1485:
			case -1484:
			case -1483:
			case -1482:
			case -1481:
			case -1480:
			case -1479:
			case -1478:
			case -1477:
			case -1476:
			case -1475:
			case -1474:
			case -1473:
			case -1472:
			case -1471:
			case -1399:
				break;
			case -1602:
				result = "领取等级奖励角色等级不足";
				return result;
			case -1601:
				result = "等级奖励不存在";
				return result;
			case -1600:
				result = "已经领取过该等级奖励了";
				return result;
			case -1568:
				result = "你不是年费黄钻";
				return result;
			case -1567:
				result = "当前还不能领取该黄钻礼包";
				return result;
			case -1566:
				result = "你已经领取过该黄钻礼包了";
				return result;
			case -1565:
				result = "你不是黄钻";
				return result;
			case -1564:
				result = "今天的黄钻奖励已经领取过了";
				return result;
			case -1563:
				result = "当前vip等级无法领取每日绑定元宝奖励";
				return result;
			case -1562:
				result = "今天已经领取过绑定元宝了";
				return result;
			case -1561:
				result = "今天已经进行过免费修行了";
				return result;
			case -1560:
				result = "当前vip等级没有免费修行";
				return result;
			case -1559:
				result = "当前vip等级无法免费领取离线经验奖励";
				return result;
			case -1558:
				result = "当前vip等级无法进行随身武器修复";
				return result;
			case -1557:
				result = "当前vip等级无法进行随身修理";
				return result;
			case -1556:
				result = "当前vip等级无法进行随身交易";
				return result;
			case -1555:
				result = "当前vip等级无每日固定奖励";
				return result;
			case -1554:
				result = "当前vip等级无每日随机奖励";
				return result;
			case -1553:
				result = "今天已经领取过奖励了(固定奖励)";
				return result;
			case -1552:
				result = "今天已经领取过奖励了(随机奖励)";
				return result;
			case -1551:
				result = "vip等级不足";
				return result;
			case -1550:
				result = "你不是vip";
				return result;
			case -1520:
				result = "你所在帮派的名次没有最终排名奖励";
				return result;
			case -1513:
				result = "你还不能领取该奖励，依赖目标未完成";
				return result;
			case -1512:
				result = "你已领取完该奖励";
				return result;
			case -1511:
				result = "用于领奖装备的此奖励已被玩家领过了";
				return result;
			case -1510:
				result = "你已经领取过该奖励了";
				return result;
			case -1509:
				result = "该活动奖励已被领取完了";
				return result;
			case -1508:
				result = "最终奖励已超时，不能领取";
				return result;
			case -1507:
				result = "该活动没有最终奖励";
				return result;
			case -1506:
				result = "活动进行中，不能获取最终奖励";
				return result;
			case -1505:
				result = "该活动没有每日奖励";
				return result;
			case -1504:
				result = "已经领取过每日奖励了";
				return result;
			case -1503:
				result = "已经领取过最终排名奖励了";
				return result;
			case -1502:
				result = "你的名次没有最终排名奖励";
				return result;
			case -1501:
				result = "未上排行榜，不能领取奖励";
				return result;
			case -1500:
				result = "排行活动未开启或已过期";
				return result;
			case -1470:
				result = "标志未更改";
				return result;
			case -1469:
				result = "帮派已经开始解散";
				return result;
			case -1468:
				result = "没有足够的消费点";
				return result;
			case -1467:
				result = "没有帮派申请攻城，不需要守城";
				return result;
			case -1466:
				result = "你所在的帮派没有申请攻城战,不能进入";
				return result;
			case -1465:
				result = "你所在的帮派已申请过了";
				return result;
			case -1464:
				result = "下次攻城战,你所在的帮派不需要申请就可进入";
				return result;
			case -1463:
				result = "申请攻城战时间已过";
				return result;
			case -1462:
				result = "该攻城战不需要申请进入";
				return result;
			case -1461:
				result = "该建筑已修理好了";
				return result;
			case -1460:
				result = "该攻城战没有需要修理的建筑";
				return result;
			case -1459:
				result = "该攻城战的消耗奖励已过期";
				return result;
			case -1458:
				result = "已领取过该攻城战的消耗奖励";
				return result;
			case -1457:
				result = "该攻城战没有消耗奖励";
				return result;
			case -1456:
				result = "帮派副本尚未创建，需帮主、长老创建后才能进入";
				return result;
			case -1455:
				result = "未加入阵营";
				return result;
			case -1454:
				result = "已领取过随机阵营奖励";
				return result;
			case -1453:
				result = "不存在的阵营";
				return result;
			case -1452:
				result = "已经在某个阵营中了";
				return result;
			case -1451:
				result = "非同一阵营帮派，不能加入";
				return result;
			case -1450:
				result = "不是弹劾帮主道具";
				return result;
			case -1449:
				result = "帮主3天内有上线，不能弹劾";
				return result;
			case -1448:
				result = "帮派等级差过大，不能宣战";
				return result;
			case -1447:
				result = "不是下属帮派，无需解除";
				return result;
			case -1446:
				result = "不能向自己的下属帮派宣战";
				return result;
			case -1445:
				result = "下属帮派已满，不能再宣战";
				return result;
			case -1444:
				result = "帮派宣战科技（设施）尚未开启";
				return result;
			case -1443:
				result = "已有约战的帮派不能宣战或被宣战";
				return result;
			case -1442:
				result = "不能向自己帮派宣战";
				return result;
			case -1441:
				result = "帮派宣战目标帮派等级不足";
				return result;
			case -1440:
				result = "帮派宣战帮派等级不足";
				return result;
			case -1439:
				result = "帮派宣战时间未到，不能宣战";
				return result;
			case -1438:
				result = "当前副本不能进行帮派召集";
				return result;
			case -1437:
				result = "今天的俸禄已经领取";
				return result;
			case -1436:
				result = "该帮派科技不能主动激活";
				return result;
			case -1435:
				result = "帮派商店等级不符合要求";
				return result;
			case -1434:
				result = "帮派官阶需求不符";
				return result;
			case -1433:
				result = "已激活该帮派科技状态";
				return result;
			case -1432:
				result = "尚未到获取粮草的时间";
				return result;
			case -1431:
				result = "帮派尚未拥有该科技";
				return result;
			case -1430:
				result = "帮派科技升级cd未到";
				return result;
			case -1429:
				result = "今天的帮派科技升级次数已用完";
				return result;
			case -1428:
				result = "不是帮派科技建设类道具";
				return result;
			case -1427:
				result = "已达到最大帮派科技等级";
				return result;
			case -1426:
				result = "帮派排名不足";
				return result;
			case -1425:
				result = "加入帮派申请已满";
				return result;
			case -1424:
				result = "帮派贡献不足";
				return result;
			case -1423:
				result = "每日捐献游戏币数量达到上限";
				return result;
			case -1422:
				result = "每日捐献元宝数量达到上限";
				return result;
			case -1421:
				result = "帮派等级不够";
				return result;
			case -1420:
				result = "已存在同名帮派";
				return result;
			case -1419:
				result = "帮派不存在";
				return result;
			case -1418:
				result = "帮派成员不满足解散最低成员数要求";
				return result;
			case -1417:
				result = "帮派人数不足";
				return result;
			case -1416:
				result = "副帮主职位人数已满";
				return result;
			case -1415:
				result = "帮派职务一致，无需调整";
				return result;
			case -1414:
				result = "目标不在你的帮派中";
				return result;
			case -1413:
				result = "帮派游戏币不足";
				return result;
			case -1412:
				result = "帮派元宝不足";
				return result;
			case -1411:
				result = "帮派已达到最大等级";
				return result;
			case -1410:
				result = "帮主不能离开帮派";
				return result;
			case -1409:
				result = "对方已经在某个帮派中了";
				return result;
			case -1408:
				result = "加入帮派申请不存在";
				return result;
			case -1407:
				result = "你的帮派权限不够";
				return result;
			case -1406:
				result = "你不是帮派成员";
				return result;
			case -1405:
				result = "等级不足,不能加入帮派";
				return result;
			case -1404:
				result = "已经申请过加入该帮派";
				return result;
			case -1403:
				result = "帮派成员已满";
				return result;
			case -1402:
				result = "不是帮派创建道具";
				return result;
			case -1401:
				result = "已经在某个帮派中了";
				return result;
			case -1400:
				result = "等级不足,不能创建帮派";
				return result;
			case -1398:
				result = "该成员阶级不够，无法直接任命首领！";
				return result;
			default:
				switch (num)
				{
				case -1363:
					result = "交易元宝数量过大";
					return result;
				case -1362:
					result = "交易金币数量过大";
					return result;
				case -1361:
					result = "交易后金币将超出上限";
					return result;
				case -1360:
					result = "交易道具数量不一致";
					return result;
				case -1359:
					result = "道具已绑定，不能交易";
					return result;
				case -1358:
					result = "不能与自己交易";
					return result;
				case -1357:
					result = "不能在双方未锁定情况下完成交易";
					return result;
				case -1356:
					result = "物品已经在交易中了";
					return result;
				case -1355:
					result = "交易已锁定，不能再添加物品";
					return result;
				case -1354:
					result = "不在交易中";
					return result;
				case -1353:
					result = "交易请求不存在";
					return result;
				case -1352:
					result = "对方已经在交易中了";
					return result;
				case -1351:
					result = "自己已经在交易中了";
					return result;
				case -1350:
				case -1349:
				case -1348:
				case -1347:
				case -1346:
				case -1345:
				case -1344:
				case -1343:
				case -1342:
				case -1341:
				case -1340:
				case -1339:
				case -1338:
				case -1337:
				case -1336:
				case -1335:
				case -1334:
				case -1333:
				case -1332:
				case -1331:
				case -1330:
				case -1329:
				case -1328:
				case -1327:
				case -1300:
				case -1299:
				case -1298:
				case -1297:
				case -1296:
				case -1295:
				case -1294:
				case -1293:
				case -1292:
				case -1291:
				case -1290:
				case -1289:
				case -1288:
				case -1287:
				case -1286:
				case -1285:
				case -1284:
				case -1283:
				case -1282:
				case -1281:
				case -1280:
				case -1279:
				case -1278:
				case -1277:
				case -1276:
				case -1275:
				case -1274:
				case -1273:
				case -1272:
				case -1250:
				case -1249:
				case -1239:
				case -1238:
				case -1237:
				case -1236:
				case -1235:
				case -1234:
				case -1233:
				case -1232:
				case -1231:
				case -1230:
				case -1229:
				case -1228:
				case -1227:
				case -1226:
				case -1225:
				case -1224:
				case -1223:
				case -1222:
				case -1221:
				case -1220:
				case -1219:
				case -1218:
				case -1200:
				case -1199:
				case -1198:
				case -1197:
				case -1196:
				case -1195:
				case -1194:
				case -1193:
				case -1192:
				case -1191:
				case -1190:
				case -1189:
				case -1188:
				case -1187:
				case -1186:
				case -1185:
				case -1184:
				case -1183:
				case -1182:
				case -1181:
				case -1180:
				case -1179:
				case -1178:
				case -1177:
				case -1176:
				case -1175:
				case -1174:
				case -1173:
				case -1172:
				case -1171:
				case -1170:
				case -1169:
				case -1168:
				case -1167:
				case -1166:
				case -1165:
				case -1164:
				case -1163:
				case -1162:
				case -1161:
				case -1160:
				case -1159:
				case -1158:
				case -1157:
				case -1156:
				case -1155:
				case -1154:
				case -1153:
				case -1152:
				case -1151:
				case -1150:
				case -1149:
				case -1148:
				case -1147:
				case -1146:
				case -1145:
				case -1144:
				case -1143:
				case -1142:
				case -1141:
				case -1140:
				case -1099:
				case -1098:
				case -1097:
				case -1096:
				case -1095:
				case -1094:
				case -1093:
				case -1092:
				case -1091:
				case -1090:
				case -1089:
				case -1088:
				case -1087:
				case -1086:
				case -1085:
				case -1084:
				case -1083:
				case -1082:
				case -1081:
				case -1080:
				case -1079:
				case -1078:
				case -1077:
				case -1076:
				case -1075:
				case -1074:
				case -1073:
				case -1072:
				case -1071:
				case -1070:
				case -1055:
				case -1049:
				case -1048:
				case -1047:
				case -1046:
				case -1045:
				case -1044:
				case -1043:
				case -1042:
				case -1041:
				case -1040:
				case -1039:
				case -1038:
				case -1037:
				case -1036:
				case -1035:
				case -1034:
				case -1033:
				case -1032:
				case -1031:
				case -1030:
				case -1029:
				case -1028:
				case -1027:
				case -1026:
				case -1025:
				case -1024:
				case -1023:
				case -1022:
				case -1020:
				case -996:
				case -995:
				case -994:
				case -993:
				case -992:
				case -991:
				case -990:
				case -989:
				case -988:
				case -987:
				case -986:
				case -985:
				case -984:
				case -983:
				case -982:
				case -981:
				case -980:
				case -979:
				case -978:
				case -977:
				case -976:
				case -975:
				case -974:
				case -973:
				case -972:
				case -900:
					break;
				case -1326:
					result = "组队副本中无法邀请";
					return result;
				case -1325:
					result = "组队副本中无法退出队伍";
					return result;
				case -1324:
					result = "目标队伍已不能申请加入";
					return result;
				case -1323:
					result = "已经roll过了";
					return result;
				case -1322:
					result = "不是你的队伍掉落物品，无权参与roll";
					return result;
				case -1321:
					result = "该队伍掉落物品不存在";
					return result;
				case -1320:
					result = "自己是队长，不用替换";
					return result;
				case -1319:
					result = "不能踢自己";
					return result;
				case -1318:
					result = "对方已经在队伍中了";
					return result;
				case -1317:
					result = "已经在队伍中了";
					return result;
				case -1316:
					result = "队伍人满了";
					return result;
				case -1315:
					result = "角色不存在或已离线";
					return result;
				case -1314:
					result = "队伍尚未发布";
					return result;
				case -1313:
					result = "队伍已经发布过了";
					return result;
				case -1312:
					result = "已达到最大发布数";
					return result;
				case -1311:
					result = "队伍不存在";
					return result;
				case -1310:
					result = "对方不在队伍中";
					return result;
				case -1309:
					result = "不在队伍中，请先创建队伍";
					return result;
				case -1308:
					result = "已经邀请过了";
					return result;
				case -1307:
					result = "未找到加入邀请";
					return result;
				case -1306:
					result = "达到加入最大邀请数";
					return result;
				case -1305:
					result = "不是队长";
					return result;
				case -1304:
					result = "已经申请过了";
					return result;
				case -1303:
					result = "未找到加入申请";
					return result;
				case -1302:
					result = "达到加入最大申请数";
					return result;
				case -1301:
					result = "无法创建更多队伍了";
					return result;
				case -1271:
					result = "对方不在你的黑名单";
					return result;
				case -1270:
					result = "好友推荐太频繁";
					return result;
				case -1269:
					result = "对方已申请你为好友";
					return result;
				case -1268:
					result = "不能和自己聊天";
					return result;
				case -1267:
					result = "已经申请该好友";
					return result;
				case -1266:
					result = "输入姓名错误";
					return result;
				case -1265:
					result = "对方已达到最大好友数";
					return result;
				case -1264:
					result = "已达到最大好友申请数";
					return result;
				case -1263:
					result = "已经获取过祝福瓶经验了";
					return result;
				case -1262:
					result = "祝福瓶中无经验了";
					return result;
				case -1261:
					result = "没有足够的好友";
					return result;
				case -1260:
					result = "没有可以获取经验的祝贺了";
					return result;
				case -1259:
					result = "没有对应的升级祝贺记录";
					return result;
				case -1258:
					result = "对方接受祝贺数已达上限";
					return result;
				case -1257:
					result = "已达到最大祝贺好友数";
					return result;
				case -1256:
					result = "已达到最大黑名单数";
					return result;
				case -1255:
					result = "已达到最大好友数";
					return result;
				case -1254:
					result = "不能添加自己的角色";
					return result;
				case -1253:
					result = "已经在黑名单中了";
					return result;
				case -1252:
					result = "已经是你仇人了";
					return result;
				case -1251:
					result = "已经是你好友了";
					return result;
				case -1248:
					result = "未穿戴指定装备";
					return result;
				case -1247:
					result = "未穿戴指定装备,装备追加属性条数不足";
					return result;
				case -1246:
					result = "未穿戴指定装备,或该装备强化等级不足";
					return result;
				case -1245:
					result = "尚未装备指定品质装备";
					return result;
				case -1244:
					result = "未拥有该类装备或品质不足";
					return result;
				case -1243:
					result = "武器击杀数要求不符";
					return result;
				case -1242:
					result = "武器击碎其他武器数要求不符";
					return result;
				case -1241:
					result = "装备宝石镶嵌数要求不符";
					return result;
				case -1240:
					result = "装备锻造等级要求不符";
					return result;
				case -1217:
					result = "没有完成指定任务";
					return result;
				case -1216:
					result = "转生次数不符合要求";
					return result;
				case -1215:
					result = "职业等级不符合要求";
					return result;
				case -1214:
					result = "侠客行称号要求不符";
					return result;
				case -1213:
					result = "尚有需要完成的苍穹目标未完成";
					return result;
				case -1212:
					result = "没有成功过符合要求的合成";
					return result;
				case -1211:
					result = "修行次数不符合要求";
					return result;
				case -1210:
					result = "成就要求不符";
					return result;
				case -1209:
					result = "智慧要求不符";
					return result;
				case -1208:
					result = "敏捷要求不符";
					return result;
				case -1207:
					result = "魔力要求不符";
					return result;
				case -1206:
					result = "体力要求不符";
					return result;
				case -1205:
					result = "力量要求不符";
					return result;
				case -1204:
					result = "等级要求不符";
					return result;
				case -1203:
					result = "某些属性不符//TODO:细化不符合属性错误信息";
					return result;
				case -1202:
					result = "性别要求不符";
					return result;
				case -1201:
					result = "职业要求不符";
					return result;
				case -1139:
					result = "抽奖次数不足";
					return result;
				case -1138:
					result = "购买失败，已达到每日购买上限";
					return result;
				case -1137:
					result = "挂售失败，已达到每日挂售上限";
					return result;
				case -1136:
					result = "增加武魂失败，已达到当前最大可升等级";
					return result;
				case -1135:
					result = "不是寻宝积分商城的道具";
					return result;
				case -1134:
					result = "不在抽奖有效时间段";
					return result;
				case -1133:
					result = "跟随装备未穿上或没有该装备";
					return result;
				case -1132:
					result = "已镀金";
					return result;
				case -1131:
					result = "不是镀金装备";
					return result;
				case -1130:
					result = "已达到最大气海值，不能再增加了";
					return result;
				case -1129:
					result = "已达到最大体力值，不能再增加体力值了";
					return result;
				case -1128:
					result = "已达到每日可获取的最大荣誉值，不能再增加荣誉值了";
					return result;
				case -1127:
					result = "需要延时道具";
					return result;
				case -1126:
					result = "不是时效道具";
					return result;
				case -1125:
					result = "商铺道具查询过于频繁";
					return result;
				case -1124:
					result = "不能合并时效道具";
					return result;
				case -1123:
					result = "不是积分商城的道具";
					return result;
				case -1122:
					result = "临时仓库空间不足";
					return result;
				case -1121:
					result = "不是荣誉商城的道具";
					return result;
				case -1120:
					result = "已达到最大商铺上限了";
					return result;
				case -1119:
					result = "不能再上架道具了";
					return result;
				case -1118:
					result = "商铺中没有资金可以获取";
					return result;
				case -1117:
					result = "不能出售绑定道具";
					return result;
				case -1116:
					result = "商铺道具已过期";
					return result;
				case -1115:
					result = "商铺中没有这个道具";
					return result;
				case -1114:
					result = "背包中没有这个道具";
					return result;
				case -1113:
					result = "随身仓库已经开启，无需再开启";
					return result;
				case -1112:
					result = "仓库空间不足";
					return result;
				case -1111:
					result = "没有随身背包,不能存取道具";
					return result;
				case -1110:
					result = "已经达到最大仓库空间了";
					return result;
				case -1109:
					result = "绑定与非绑定道具不能叠加";
					return result;
				case -1108:
					result = "已经达到最大背包空间了";
					return result;
				case -1107:
					result = "已达到最大可叠加上限";
					return result;
				case -1106:
					result = "已达到该道具可拥有最大上限";
					return result;
				case -1105:
					result = "不能叠加不同类型的道具";
					return result;
				case -1104:
					result = "道具数量不足";
					return result;
				case -1103:
					result = "道具不在已出售道具列表中";
					return result;
				case -1102:
					result = "你没有这个道具";
					return result;
				case -1101:
					result = "没有足够的背包空间了";
					return result;
				case -1100:
					result = "没有对应配置";
					return result;
				case -1069:
					result = "不能传送到该地图";
					return result;
				case -1068:
					result = "进入攻城战后，才能购买";
					return result;
				case -1067:
					result = "增加状态出错，可能你已有该状态";
					return result;
				case -1066:
					result = "该NPC处没有你想获取的状态";
					return result;
				case -1065:
					result = "该NPC没有仓库入口";
					return result;
				case -1064:
					result = "NPC没有目标传送点配置";
					return result;
				case -1063:
					result = "NPC没有地图传送配置";
					return result;
				case -1062:
					result = "NPC没有副本入口";
					return result;
				case -1061:
					result = "NPC不修复装备耐久";
					return result;
				case -1060:
					result = "NPC不修复击碎武器";
					return result;
				case -1059:
					result = "NPC不镶嵌";
					return result;
				case -1058:
					result = "NPC不锻造";
					return result;
				case -1057:
					result = "NPC不在附近";
					return result;
				case -1056:
					result = "该NPC没有该道具出售";
					return result;
				case -1054:
					result = "该NPC没有道具交易";
					return result;
				case -1053:
					result = "没有这个NPC";
					return result;
				case -1052:
					result = "NPC距离太远了";
					return result;
				case -1051:
					result = "该NPC不在当前地图中";
					return result;
				case -1050:
					result = "红名不能购买NPC道具";
					return result;
				case -1021:
					result = "未达到军衔升级条件";
					return result;
				case -1019:
					result = "已达到最大阶数";
					return result;
				case -1018:
					result = "不同部位装备不能相互传承";
					return result;
				case -1017:
					result = "不同职业装备不能相互传承";
					return result;
				case -1016:
					result = "竞技场积分不满足";
					return result;
				case -1015:
					result = "寻宝积分不足";
					return result;
				case -1014:
					result = "已达数值上限，不能再增加更多了";
					return result;
				case -1013:
					result = "你的金币已达上限，不能再获得更多了";
					return result;
				case -1012:
					result = "宠物天梯积分不足";
					return result;
				case -1011:
					result = "商城积分不足";
					return result;
				case -1010:
					result = "角色行动力不足";
					return result;
				case -1009:
					result = "帮派货币不足";
					return result;
				case -1008:
					result = "荣誉值不足";
					return result;
				case -1007:
					result = "背包中没有足够的所需道具";
					return result;
				case -1006:
					result = "绑定钻石不足";
					return result;
				case -1005:
					result = "你被目标用户拉入黑名单了";
					return result;
				case -1004:
					result = "目标用户不在线";
					return result;
				case -1003:
					result = "没有足够的所需道具";
					return result;
				case -1002:
					result = "没有足够的金币";
					return result;
				case -1001:
					result = "没有足够的钻石";
					return result;
				case -1000:
					result = "没有足够的古战场积分";
					return result;
				case -999:
					result = "没有足够的声望";
					return result;
				case -998:
					result = "没有足够的礼金";
					return result;
				case -997:
					result = "商城没有此类道具";
					return result;
				case -971:
					result = "不能分解其他职业装备";
					return result;
				case -970:
					result = "体力上限";
					return result;
				case -969:
					result = "道具使用次数不足";
					return result;
				case -968:
					result = "该道具不能用于增加卓越属性";
					return result;
				case -967:
					result = "该装备已到达卓越属性条数限制";
					return result;
				case -966:
					result = "该装备不能增加卓越属性";
					return result;
				case -965:
					result = "该装备已增加过卓越属性";
					return result;
				case -964:
					result = "未融合该类型装备，不可幻化";
					return result;
				case -963:
					result = "该装备不可幻化";
					return result;
				case -962:
					result = "该装备不可融合";
					return result;
				case -961:
					result = "已融合相同类型物品";
					return result;
				case -960:
					result = "时效装备不可融合";
					return result;
				case -959:
					result = "该装备已达到充能上限";
					return result;
				case -958:
					result = "该装备不能充能";
					return result;
				case -957:
					result = "该装备没有该属性";
					return result;
				case -956:
					result = "该装备不能洗练";
					return result;
				case -955:
					result = "该装备没有达到激活此属性需要的条件";
					return result;
				case -954:
					result = "该装备不需要鉴定";
					return result;
				case -953:
					result = "该装备不能修复";
					return result;
				case -952:
					result = "该装备不能激活380属性";
					return result;
				case -951:
					result = "该装备已经拥有了此属性";
					return result;
				case -950:
					result = "该装备没有指定的高级强化属性";
					return result;
				case -949:
					result = "已达到高级强化上限";
					return result;
				case -948:
					result = "该装备不能高级强化";
					return result;
				case -947:
					result = "已达到追加属性上限";
					return result;
				case -946:
					result = "该装备不能追加属性";
					return result;
				case -945:
					result = "当前场景不能使用道具";
					return result;
				case -944:
					result = "修行祝福点数不足";
					return result;
				case -943:
					result = "该装备不能锻造、镶嵌、分解";
					return result;
				case -942:
					result = "已有vip等级大于道具vip等级，不能使用";
					return result;
				case -941:
					result = "使用道具所需地图、坐标不对";
					return result;
				case -940:
					result = "待消耗装备镶嵌有宝石，不能消耗";
					return result;
				case -939:
					result = "装备已过期";
					return result;
				case -938:
					result = "道具已过期";
					return result;
				case -937:
					result = "装备不能分解";
					return result;
				case -936:
					result = "已经进行过免费修行了";
					return result;
				case -935:
					result = "时装不能直接修复";
					return result;
				case -934:
					result = "达到当前宝石合成的最大等级，不能再合成了";
					return result;
				case -933:
					result = "达到当前能锻造的最大等级，不能再锻造了";
					return result;
				case -932:
					result = "不能购买赠送道具给自己";
					return result;
				case -931:
					result = "道具不可丢弃";
					return result;
				case -930:
					result = "提升装备品质必须要有模具";
					return result;
				case -929:
					result = "材料装备镶嵌有宝石，不能合成或打造";
					return result;
				case -928:
					result = "材料宝石等级不符";
					return result;
				case -927:
					result = "没有对应部位的装备";
					return result;
				case -926:
					result = "没有对应品质的目标套装";
					return result;
				case -925:
					result = "目标模具品质不对";
					return result;
				case -924:
					result = "不能合成或打造不同套装装备";
					return result;
				case -923:
					result = "不能合成或打造非套装装备";
					return result;
				case -922:
					result = "不能合成或打造不同品质的装备";
					return result;
				case -921:
					result = "已达到最大宝石等级，不能再合成了";
					return result;
				case -920:
					result = "缺少合成所需最少宝石数";
					return result;
				case -919:
					result = "不能合成不同等级的宝石";
					return result;
				case -918:
					result = "不能合成不同类型的宝石";
					return result;
				case -917:
					result = "萃取源装备镶嵌了宝石";
					return result;
				case -916:
					result = "萃取源装备无锻造等级";
					return result;
				case -915:
					result = "萃取目标装备锻造等级比源装备高";
					return result;
				case -914:
					result = "消耗道具出错";
					return result;
				case -913:
					result = "不能再同一装备上镶嵌相同类型的宝石";
					return result;
				case -912:
					result = "消耗道具配置出错";
					return result;
				case -911:
					result = "装备无需修复耐久";
					return result;
				case -910:
					result = "武器未被击碎";
					return result;
				case -909:
					result = "不是可使用道具";
					return result;
				case -908:
					result = "目标不能再添加该状态了";
					return result;
				case -907:
					result = "镶嵌满了，不能再镶嵌了";
					return result;
				case -906:
					result = "装备没有孔，不能镶嵌";
					return result;
				case -905:
					result = "达到最大锻造等级，不能再锻造了";
					return result;
				case -904:
					result = "装备没有锻造属性";
					return result;
				case -903:
					result = "道具cd没有冷却";
					return result;
				case -902:
					result = "道具不能出售";
					return result;
				case -901:
					result = "该类型道具不存在";
					return result;
				case -899:
					result = "等级太低，无法使用道具";
					return result;
				case -898:
					result = "转生等级太低，无法使用道具";
					return result;
				default:
					switch (num)
					{
					case -854:
						result = "出售道具时间已到头";
						return result;
					case -853:
						result = "已达到每角色允许购买上限";
						return result;
					case -852:
						result = "商城中道具还没开卖";
						return result;
					case -851:
						result = "商城中道具没有上架";
						return result;
					case -850:
						result = "商城中道具卖完了";
						return result;
					case -824:
						result = "掉落物品不属于你";
						return result;
					case -823:
						result = "掉落物品不在附近";
						return result;
					case -822:
						result = "掉落物品已经消失了";
						return result;
					case -821:
						result = "掉落物品组不存在";
						return result;
					case -806:
						result = "地图切换点等级需求不符";
						return result;
					case -805:
						result = "区域采集过于频繁";
						return result;
					case -804:
						result = "不在采集区域中";
						return result;
					case -803:
						result = "目标地图不存在";
						return result;
					case -802:
						result = "地图切换点不在附近";
						return result;
					case -801:
						result = "地图切换点不存在";
						return result;
					case -796:
						result = "技能的等级不足";
						return result;
					case -795:
						result = "技能的强化值不足";
						return result;
					case -794:
						result = "学会的门派技能数量不足";
						return result;
					case -793:
						result = "学会的秘籍数量不足";
						return result;
					case -792:
						result = "尚未学会该技能";
						return result;
					case -791:
						result = "尚未学会任何门派技能";
						return result;
					case -790:
						result = "尚未学会任何秘技";
						return result;
					case -785:
						result = "技能与职业不符合";
						return result;
					case -784:
						result = "技能栏位未开启";
						return result;
					case -783:
						result = "聚气中，不能释放其它技术";
						return result;
					case -782:
						result = "技能已抄录";
						return result;
					case -781:
						result = "已达到最大抄录技能数";
						return result;
					case -780:
						result = "技能不能抄录";
						return result;
					case -779:
						result = "需抄录技能未抄录，不能释放";
						return result;
					case -778:
						result = "技能不能对尸体释放";
						return result;
					case -777:
						result = "没有在参悟中的技能，无需加速";
						return result;
					case -776:
						result = "已达最大技能强化等级";
						return result;
					case -775:
						result = "角色等级不足，不能升级技能";
						return result;
					case -774:
						result = "错误的技能释放目标";
						return result;
					case -773:
						result = "跳跃过程中不能施法";
						return result;
					case -772:
						result = "不在施法范围内";
						return result;
					case -771:
						result = "没有装备释放技能所需装备";
						return result;
					case -770:
						result = "吟唱中，不能再释放";
						return result;
					case -769:
						result = "目标技能免疫";
						return result;
					case -768:
						result = "目标不存在";
						return result;
					case -767:
						result = "没有足够的技能释放所需道具";
						return result;
					case -766:
						result = "技能CD中";
						return result;
					case -765:
						result = "技能等级数据错误";
						return result;
					case -764:
						result = "释放目标类型与配置不符";
						return result;
					case -763:
						result = "当前角色还不能释放技能";
						return result;
					case -762:
						result = "该道具还有可学技能，但是尚不符合学习要求";
						return result;
					case -761:
						result = "该道具已经没有什么技能可学的了";
						return result;
					case -760:
						result = "技能已经达到最大等级了";
						return result;
					case -759:
						result = "技能点不足";
						return result;
					case -758:
						result = "技能无法再加点升级了";
						return result;
					case -757:
						result = "道具中可升级的技能等级尚未满足道具指定的最低等级要求";
						return result;
					case -756:
						result = "道具中可升级的技能等级大于等于道具指定最大等级，没有可升级的了";
						return result;
					case -755:
						result = "道具中没有可升级的技能";
						return result;
					case -754:
						result = "未达到学习该技能的等级";
						return result;
					case -753:
						result = "技能已经在升级中了";
						return result;
					case -752:
						result = "尚未学会此技能";
						return result;
					case -751:
						result = "技能不存在";
						return result;
					case -743:
						result = "你身上已有更强大的此类状态";
						return result;
					case -742:
						result = "该状态已达到最大叠加数";
						return result;
					case -741:
						result = "超出允许最大叠加祝福状态参数上限";
						return result;
					case -740:
						result = "已存在同类型的祝福状态了，不能再开启";
						return result;
					case -727:
						result = "经脉值不足";
						return result;
					case -726:
						result = "本穴位需要开启道具才能开启";
						return result;
					case -725:
						result = "本经脉需要开启道具才能开启";
						return result;
					case -724:
						result = "角色等级不足，不能增加新的经脉修炼队列";
						return result;
					case -723:
						result = "已达到最大经脉修炼队列数量";
						return result;
					case -722:
						result = "没有需要快速完成的冷却队列";
						return result;
					case -721:
						result = "无可用的修炼队列了，请等待修炼队列冷却";
						return result;
					case -720:
						result = "尚未开通任何穴位";
						return result;
					case -719:
						result = "尚未开通任何经脉";
						return result;
					case -718:
						result = "经脉尚有穴位没开通";
						return result;
					case -717:
						result = "没有在修炼中的经脉，无需加速";
						return result;
					case -716:
						result = "穴位被封";
						return result;
					case -715:
						result = "经脉被封";
						return result;
					case -714:
						result = "穴位等级不足";
						return result;
					case -713:
						result = "穴位尚未开启";
						return result;
					case -712:
						result = "道具中没有可升级的穴位";
						return result;
					case -711:
						result = "不是穴位升级道具";
						return result;
					case -710:
						result = "穴位已经在升级中了";
						return result;
					case -709:
						result = "穴位达到最大等级了，不能再升级了";
						return result;
					case -708:
						result = "道具中的穴位全部已经开通了，没有可开通的穴位了";
						return result;
					case -707:
						result = "道具中可升级的穴位等级尚未满足道具指定的最低等级要求";
						return result;
					case -706:
						result = "道具中可升级的穴位等级大于等于道具指定最大等级，没有可升级的了";
						return result;
					case -705:
						result = "经脉尚未开启";
						return result;
					case -704:
						result = "穴位已经开通";
						return result;
					case -703:
						result = "穴位不存在";
						return result;
					case -702:
						result = "经脉已经开通";
						return result;
					case -701:
						result = "经脉不存在";
						return result;
					case -652:
						result = "侠客行任务未完成";
						return result;
					case -651:
						result = "前置侠客行副本任务未完成";
						return result;
					case -650:
						result = "侠客行副本任务没有奖励或已领取";
						return result;
					case -635:
						result = "专属怪在冷却中无法召唤";
						return result;
					case -634:
						result = "任务类型不匹配";
						return result;
					case -633:
						result = "任务目标不在附近";
						return result;
					case -632:
						result = "已到提交上限";
						return result;
					case -631:
						result = "收集任务道具不匹配";
						return result;
					case -630:
						result = "任务次数未达到";
						return result;
					case -629:
						result = "此任务不能购买双倍奖励";
						return result;
					case -628:
						result = "红名时不能传送";
						return result;
					case -627:
						result = "尚未加入阵营";
						return result;
					case -626:
						result = "所在帮派已经接受过今天的酒馆任务了";
						return result;
					case -625:
						result = "尚未完成过需要完成的任务";
						return result;
					case -624:
						result = "未进入过该副本";
						return result;
					case -623:
						result = "任务酒馆刷新冷却中";
						return result;
					case -622:
						result = "酒馆任务接受次数限制";
						return result;
					case -621:
						result = "任务酒馆已达最大品质";
						return result;
					case -620:
						result = "任务技能冷却中";
						return result;
					case -619:
						result = "任务技能不存在";
						return result;
					case -618:
						result = "任务已经有保险";
						return result;
					case -617:
						result = "任务不能买保险";
						return result;
					case -616:
						result = "有护送任务时不能传送";
						return result;
					case -615:
						result = "任务已超时";
						return result;
					case -614:
						result = "任务擂台连续杀人数没达到";
						return result;
					case -613:
						result = "任务擂台杀人数没达到";
						return result;
					case -612:
						result = "任务杀人数没达到";
						return result;
					case -611:
						result = "任务不能委托";
						return result;
					case -610:
						result = "任务不能放弃";
						return result;
					case -609:
						result = "任务时间已过";
						return result;
					case -608:
						result = "任务开启时间还未到";
						return result;
					case -607:
						result = "次数没有达到";
						return result;
					case -606:
						result = "任务还没接";
						return result;
					case -605:
						result = "没有足够的所需道具";
						return result;
					case -604:
						result = "前置任务未完成";
						return result;
					case -603:
						result = "任务已接完成了，不能再接了";
						return result;
					case -602:
						result = "任务已接，不能重复接";
						return result;
					case -601:
						result = "不存在该任务";
						return result;
					case -600:
						result = "探索未完成";
						return result;
					case -562:
						result = "你已领取过该成就的奖励";
						return result;
					case -561:
						result = "此成就没有奖励";
						return result;
					case -560:
						result = "你没有获得该成就";
						return result;
					case -538:
						result = "不能进化成该目标宠物";
						return result;
					case -537:
						result = "该宠物不能进化了";
						return result;
					case -536:
						result = "已获取过该宠物图鉴奖励";
						return result;
					case -535:
						result = "宠物图鉴尚未收集完整";
						return result;
					case -534:
						result = "宠物稀有度不足";
						return result;
					case -533:
						result = "宠物品阶不足";
						return result;
					case -532:
						result = "宠物技能数不足";
						return result;
					case -531:
						result = "宠物等级不够";
						return result;
					case -530:
						result = "宠物资质不足";
						return result;
					case -529:
						result = "宠物的技能等级不足";
						return result;
					case -528:
						result = "不能转生合体宠物";
						return result;
					case -527:
						result = "宠物PK挑战次数已用完";
						return result;
					case -526:
						result = "宠物PK记录不存在";
						return result;
					case -525:
						result = "宠物PK等级差限制";
						return result;
					case -524:
						result = "宠物不能PK自己";
						return result;
					case -523:
						result = "宠物天梯消息过于频繁";
						return result;
					case -522:
						result = "宠物不在宠物天梯";
						return result;
					case -521:
						result = "需要先进行宠物合体";
						return result;
					case -520:
						result = "不能放逐合体宠物";
						return result;
					case -519:
						result = "游戏币刷新小窝次数已用尽，无法再用游戏币刷新小窝了";
						return result;
					case -518:
						result = "修改宠物经验值出错";
						return result;
					case -517:
						result = "转生材料宠物没有经验值";
						return result;
					case -516:
						result = "当前出战宠物不能参与宠物转生";
						return result;
					case -515:
						result = "尚未拥有该宠物";
						return result;
					case -514:
						result = "尚未拥有任何宠物";
						return result;
					case -513:
						result = "宠物行动力不足";
						return result;
					case -512:
						result = "该道具无法提升该宠物的品阶";
						return result;
					case -511:
						result = "宠物已达到最大品阶，不能再提升了";
						return result;
					case -510:
						result = "宠物已达到最大疲劳值，无需再增加了";
						return result;
					case -509:
						result = "宠物技能未找到";
						return result;
					case -508:
						result = "该宠物技能书中尚有技能可学，但是当前宠物不满足要求";
						return result;
					case -507:
						result = "当前宠物在该宠物技能书中没有可学的技能了";
						return result;
					case -506:
						result = "当前宠物已达到最大能学技能数";
						return result;
					case -505:
						result = "当前宠物已达到最大拥有技能槽";
						return result;
					case -504:
						result = "没有出战宠物";
						return result;
					case -503:
						result = "已达到最大拥有宠物格子上限了";
						return result;
					case -502:
						result = "未找到指定宠物";
						return result;
					case -501:
						result = "已达到最大拥有宠物上限，不能再拥有了";
						return result;
					case -465:
						result = "有队友未准备！";
						return result;
					case -464:
						result = "召唤兽乐园剩余时间已用完！";
						return result;
					case -463:
						result = "等待队列中的扫荡副本不能立即完成扫荡";
						return result;
					case -462:
						result = "已达到开启扫荡队列数上限";
						return result;
					case -461:
						result = "副本扫荡完成时间未到";
						return result;
					case -460:
						result = "副本扫荡队列已满";
						return result;
					case -459:
						result = "该副本不能扫荡";
						return result;
					case -458:
						result = "竞技场排行奖励配置不存在";
						return result;
					case -457:
						result = "本次竞技场排行未上板";
						return result;
					case -456:
						result = "已经领取过本次竞技场奖励了";
						return result;
					case -455:
						result = "竞技场决赛奖励已超时";
						return result;
					case -454:
						result = "本轮竞技场决赛已结束";
						return result;
					case -453:
						result = "竞技场决赛副本参加角色已达上限";
						return result;
					case -452:
						result = "未达到参加竞技场决赛资格";
						return result;
					case -451:
						result = "已经加入竞技场了，不能再签入";
						return result;
					case -450:
						result = "已经在竞技场排队了，不能再签入";
						return result;
					case -449:
						result = "今天已经领取过该帮派领地每日资源";
						return result;
					case -448:
						result = "该帮派领地不能领取每日资源";
						return result;
					case -447:
						result = "不能进入其他帮派或防守方非下属帮派的奴役战争";
						return result;
					case -446:
						result = "目标帮派无宣战信息";
						return result;
					case -445:
						result = "尚未进入过该副本";
						return result;
					case -444:
						result = "帮派领地战奖励已过期";
						return result;
					case -443:
						result = "你没有帮派领地战奖励";
						return result;
					case -442:
						result = "本轮帮派领地争夺战已结束";
						return result;
					case -441:
						result = "帮派领地争夺战进行中，不能进入领地";
						return result;
					case -440:
						result = "不是你所在帮派的领地";
						return result;
					case -439:
						result = "尚未通关的副本或副本难度层级不能立即通关";
						return result;
					case -438:
						result = "立即通关副本战斗力不足";
						return result;
					case -437:
						result = "该副本无法立即通关";
						return result;
					case -436:
						result = "只有队长才能创建队伍副本";
						return result;
					case -435:
						result = "进入副本的冷却时间未到，不能进入";
						return result;
					case -434:
						result = "本轮首席大弟子已结束";
						return result;
					case -433:
						result = "领取首席大弟子奖励超时";
						return result;
					case -432:
						result = "没有首席大弟子奖励";
						return result;
					case -431:
						result = "首席大弟子比赛人数已满";
						return result;
					case -430:
						result = "当前已经在竞技场排队等待中，不能进入副本";
						return result;
					case -429:
						result = "副本只能通过帮派进入";
						return result;
					case -428:
						result = "副本只能通过匹配方式进入";
						return result;
					case -427:
						result = "副本不能直接创建";
						return result;
					case -426:
						result = "当前已接属性加成任务（如护送任务），不能进入副本";
						return result;
					case -425:
						result = "副本已结束，等待2分钟关闭中，不能进入";
						return result;
					case -424:
						result = "该副本最佳通关时间过长";
						return result;
					case -423:
						result = "该副本完成次数不足";
						return result;
					case -422:
						result = "该副本指定难度未完成";
						return result;
					case -421:
						result = "该副本未完成";
						return result;
					case -420:
						result = "尚未完成任何副本";
						return result;
					case -419:
						result = "已经在副本中了，需先退出副本";
						return result;
					case -418:
						result = "你的帮派未签入该副本";
						return result;
					case -417:
						result = "副本已达到最大玩家数";
						return result;
					case -416:
						result = "副本不能报名方式加入";
						return result;
					case -415:
						result = "进入副本需要已接受某个任务";
						return result;
					case -414:
						result = "你已经领取过该副本奖励或者该副本没有奖励";
						return result;
					case -413:
						result = "创建的副本难度过高";
						return result;
					case -412:
						result = "副本时间已过";
						return result;
					case -411:
						result = "副本开启时间还未到";
						return result;
					case -410:
						result = "已经达到进入副本的次数上限，不能再进入了";
						return result;
					case -409:
						result = "缺少进入副本需要的道具";
						return result;
					case -408:
						result = "进入副本需要完成前置任务";
						return result;
					case -407:
						result = "该副本已经创建过了";
						return result;
					case -406:
						result = "当前副本中没有这幅地图";
						return result;
					case -405:
						result = "创建的副本没有出身地图";
						return result;
					case -404:
						result = "创建副本失败";
						return result;
					case -403:
						result = "你不在队伍中，无法创建、加入队伍副本";
						return result;
					case -402:
						result = "你不在副本中";
						return result;
					case -401:
						result = "创建副本地图出错";
						return result;
					}
					break;
				}
				break;
			}
		}
		else if (num <= -350)
		{
			switch (num)
			{
			case -376:
				result = "已获取过该每日任务奖励";
				return result;
			case -375:
				result = "每日任务尚未完成";
				return result;
			case -374:
				result = "目标不能再添加该状态了";
				return result;
			case -373:
				result = "尚未完成任何每日必做任务";
				return result;
			case -372:
				result = "每日必做任务奖励已经获取过了";
				return result;
			case -371:
				result = "该每日必做任务无法立即完成";
				return result;
			case -370:
				result = "该每日必做任务已经完成了";
				return result;
			default:
				switch (num)
				{
				case -352:
					result = "目标任务奖励已获取";
					return result;
				case -351:
					result = "目标任务已过期";
					return result;
				case -350:
					result = "目标任务未开启";
					return result;
				}
				break;
			}
		}
		else
		{
			switch (num)
			{
			case -309:
				result = "邮件中包含附件";
				return result;
			case -308:
				result = "你拥有的牌太少，不能使用此功能";
				return result;
			case -307:
				result = "还未开启此牌";
				return result;
			case -306:
				result = "你今天的领奖次数已经用完了";
				return result;
			case -305:
				result = "没有翻牌奖励";
				return result;
			case -304:
				result = "划拳兑换币不足";
				return result;
			case -303:
				result = "尚未开始划拳";
				return result;
			case -302:
				result = "有尚未完成的划拳";
				return result;
			case -301:
				result = "不能给自己发邮件";
				return result;
			case -300:
				result = "邮件没有附件";
				return result;
			case -299:
			case -298:
			case -297:
			case -296:
			case -295:
			case -294:
			case -293:
			case -292:
			case -291:
			case -290:
			case -289:
			case -288:
			case -287:
			case -286:
			case -285:
			case -284:
			case -283:
			case -282:
			case -281:
			case -280:
			case -279:
			case -278:
			case -277:
			case -276:
			case -275:
			case -274:
			case -273:
			case -272:
			case -271:
			case -270:
			case -269:
			case -268:
			case -267:
			case -266:
			case -265:
			case -264:
			case -263:
			case -262:
			case -261:
			case -260:
			case -259:
			case -258:
			case -257:
			case -256:
			case -255:
			case -254:
			case -249:
			case -248:
			case -247:
			case -246:
			case -245:
			case -244:
			case -243:
			case -242:
			case -241:
			case -240:
			case -239:
			case -238:
			case -237:
			case -236:
			case -235:
			case -234:
			case -233:
			case -232:
			case -231:
			case -230:
			case -229:
			case -200:
			case -199:
			case -198:
			case -197:
			case -196:
			case -195:
			case -194:
			case -193:
			case -192:
			case -191:
			case -189:
			case -160:
			case -159:
				break;
			case -253:
				result = "采集目标不在范围内";
				return result;
			case -252:
				result = "不是采集目标";
				return result;
			case -251:
				result = "采集目标尚未恢复";
				return result;
			case -250:
				result = "采集目标不存在";
				return result;
			case -228:
				result = "只能分配一级属性";
				return result;
			case -227:
				result = "VIP等级小于三级";
				return result;
			case -226:
				result = "本地图中不能原地复活";
				return result;
			case -225:
				result = "已达到最高等级";
				return result;
			case -224:
				result = "不能攻击自己的宠物";
				return result;
			case -223:
				result = "今天的购买次数已经用完";
				return result;
			case -222:
				result = "转生次数已经用完";
				return result;
			case -221:
				result = "已达到最大转生等级";
				return result;
			case -220:
				result = "没有可重置的相关属性点";
				return result;
			case -219:
				result = "可用属性点不足";
				return result;
			case -218:
				result = "已达到最大职业等级";
				return result;
			case -217:
				result = "今天已经领取过该补偿经验";
				return result;
			case -216:
				result = "没有补偿经验可领取";
				return result;
			case -215:
				result = "本副本中不能原地复活";
				return result;
			case -214:
				result = "复活等待时间未到，还不能复活";
				return result;
			case -213:
				result = "开启战斗模式后XX时间内才能切换回和平模式";
				return result;
			case -212:
				result = "运镖任务中不能改为和平模式";
				return result;
			case -211:
				result = "罪恶值已经为0";
				return result;
			case -210:
				result = "PK状态一样，无需修改";
				return result;
			case -209:
				result = "不是复活道具";
				return result;
			case -208:
				result = "不能复活未死亡角色";
				return result;
			case -207:
				result = "和平区域中不能pk";
				return result;
			case -206:
				result = "和平地图中不能pk";
				return result;
			case -205:
				result = "和平状态不能攻击其他玩家";
				return result;
			case -204:
				result = "行动力不足";
				return result;
			case -203:
				result = "怒气不足";
				return result;
			case -202:
				result = "生命不足";
				return result;
			case -201:
				result = "法力不足";
				return result;
			case -190:
				result = "目标不在此地图";
				return result;
			case -188:
				result = "此地图不能换线";
				return result;
			case -187:
				result = "等级不足，不能发言";
				return result;
			case -186:
				result = "你换线过于频繁";
				return result;
			case -185:
				result = "灵魂状态不能完成该操作";
				return result;
			case -184:
				result = "已经领取过在线时间奖励";
				return result;
			case -183:
				result = "领取奖励在线时间不足";
				return result;
			case -182:
				result = "不是本角色的道具卡";
				return result;
			case -181:
				result = "不是本用户的道具卡";
				return result;
			case -180:
				result = "服务器即将关闭";
				return result;
			case -179:
				result = "你已经死亡，不能完成该操作";
				return result;
			case -178:
				result = "被禁言了";
				return result;
			case -177:
				result = "道具卡已过期";
				return result;
			case -176:
				result = "不存在的道具卡类型";
				return result;
			case -175:
				result = "该道具卡已领取过";
				return result;
			case -174:
				result = "道具卡id错误";
				return result;
			case -173:
				result = "整理背包过于频繁";
				return result;
			case -172:
				result = "还没到可以领取奖励的时间";
				return result;
			case -171:
				result = "没有在线奖励可以领取了";
				return result;
			case -170:
				result = "发言过于频繁";
				return result;
			case -169:
				result = "不存在这个角色";
				return result;
			case -168:
				result = "角色与自己不在同一地图";
				return result;
			case -167:
				result = "角色不在附近";
				return result;
			case -166:
				result = "角色不存在或者已离线";
				return result;
			case -165:
				result = "登录验证失效，需重新登录";
				return result;
			case -164:
				result = "用户不存在";
				return result;
			case -163:
				result = "同一会话已登录其他用户，需要先退出再重新登录";
				return result;
			case -162:
				result = "已经登录过了";
				return result;
			case -161:
				result = "同一用户已经从其他地方登录";
				return result;
			case -158:
				result = "角色不存在";
				return result;
			case -157:
				result = "角色创建后3天之内不能删除";
				return result;
			case -156:
				result = "功能临时关闭，请耐心等待并注意官方公告";
				return result;
			case -155:
				result = "删除失败，请先解除军团首领的职务";
				return result;
			case -154:
				result = "角色已达上限，不能再创建角色了";
				return result;
			case -153:
				result = "已存在同名角色";
				return result;
			case -152:
				result = "职业不存在";
				return result;
			case -151:
				result = "角色性别错误";
				return result;
			case -150:
				result = "没有达到创建该职业所需条件";
				return result;
			case -149:
				result = "安全密码错误";
				return result;
			default:
				switch (num)
				{
				case -108:
					result = "被封号了";
					return result;
				case -107:
					result = "非法字符";
					return result;
				case -106:
					result = "已达到最大反沉迷累积在线时间";
					return result;
				case -105:
					result = "数据错误";
					return result;
				case -104:
					result = "未登录";
					return result;
				case -103:
					result = "参数错误";
					return result;
				case -102:
					result = "服务器内部错误";
					return result;
				case -101:
					result = "服务器配置错误";
					return result;
				}
				break;
			}
		}
		result = err_code.ToString();
		return result;
	}
}
