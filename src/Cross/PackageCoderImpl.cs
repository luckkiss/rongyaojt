using System;
using System.Collections.Generic;

namespace Cross
{
	public class PackageCoderImpl : IPackageCoder
	{
		public Dictionary<uint, Variant> s2c_rpc_pkg_desc = new Dictionary<uint, Variant>();

		public Dictionary<uint, Variant> c2s_rpc_pkg_desc = new Dictionary<uint, Variant>();

		public bool InitRPCPackageDescribe(Variant par)
		{
			bool flag = !par.ContainsKey("data") || !par["data"].ContainsKey("s2c") || !par["data"].ContainsKey("c2s");
			bool result;
			if (flag)
			{
				DebugTrace.add(Define.DebugTrace.DTT_ERR, "Err: initialize_rpc_pkg_desc rpc pkg desc error");
				result = false;
			}
			else
			{
				Variant variant = par["data"]["s2c"];
				Variant variant2 = par["data"]["c2s"];
				for (int i = 0; i < variant.Length; i++)
				{
					uint @uint = variant[i]["id"]._uint32;
					this.s2c_rpc_pkg_desc[@uint] = variant[i];
				}
				for (int j = 0; j < variant2.Length; j++)
				{
					uint uint2 = variant2[j]["id"]._uint32;
					this.c2s_rpc_pkg_desc[uint2] = variant2[j];
				}
				result = true;
			}
			return result;
		}

		protected bool _PackRPCPackage(Variant o, Variant desc, BitsArray d)
		{
			bool flag = true;
			int @int = desc["type"]._int;
			bool flag2 = @int == 1;
			if (flag2)
			{
				for (int i = 0; i < desc["child"].Length; i++)
				{
					flag = this._PackRPCPackage(o, desc["child"][i], d);
					bool flag3 = !flag;
					if (flag3)
					{
						DebugTrace.add(Define.DebugTrace.DTT_ERR, "rpc cmd node: [" + desc["name"]._str + "] pack error");
						break;
					}
				}
			}
			else
			{
				bool flag4 = @int == 2;
				if (flag4)
				{
					int int2 = desc["vtp"]._int;
					bool flag5 = int2 == 1;
					if (flag5)
					{
						bool flag6 = !o.ContainsKey(desc["name"]._str);
						if (flag6)
						{
							flag = false;
							DebugTrace.add(Define.DebugTrace.DTT_ERR, "rpc cmd without int node: [" + desc["name"]._str + "]");
						}
						else
						{
							d.writeInt(o[desc["name"]._str]._int, desc["bits"]._int);
						}
					}
					else
					{
						bool flag7 = int2 == 2;
						if (flag7)
						{
							bool flag8 = !o.ContainsKey(desc["name"]._str);
							if (flag8)
							{
								flag = false;
								DebugTrace.add(Define.DebugTrace.DTT_ERR, "rpc cmd without uint node: [" + desc["name"]._str + "]");
							}
							else
							{
								d.writeUint(o[desc["name"]._str]._uint, desc["bits"]._int);
							}
						}
						else
						{
							bool flag9 = int2 == 3;
							if (flag9)
							{
								bool flag10 = !o.ContainsKey(desc["name"]._str);
								if (flag10)
								{
									flag = false;
									DebugTrace.add(Define.DebugTrace.DTT_ERR, "rpc cmd without bool node: [" + desc["name"]._str + "]");
								}
								else
								{
									d.writeBool(o[desc["name"]._str]._bool);
								}
							}
							else
							{
								bool flag11 = int2 == 4;
								if (flag11)
								{
									bool flag12 = !o.ContainsKey(desc["name"]._str);
									if (flag12)
									{
										flag = false;
										DebugTrace.add(Define.DebugTrace.DTT_ERR, "rpc cmd without decimal node: [" + desc["name"]._str + "]");
									}
									else
									{
										d.writeDecimal(o[desc["name"]._str]._float, desc["bits"]._int);
									}
								}
								else
								{
									bool flag13 = int2 == 5;
									if (flag13)
									{
										bool flag14 = !o.ContainsKey(desc["name"]._str);
										if (flag14)
										{
											flag = false;
											DebugTrace.add(Define.DebugTrace.DTT_ERR, "rpc cmd without udecimal node: [" + desc["name"]._str + "]");
										}
										else
										{
											d.writeUdecimal(o[desc["name"]._str]._float, desc["bits"]._int);
										}
									}
									else
									{
										bool flag15 = int2 == 6;
										if (flag15)
										{
											bool flag16 = !o.ContainsKey(desc["name"]._str);
											if (flag16)
											{
												flag = false;
												DebugTrace.add(Define.DebugTrace.DTT_ERR, "rpc cmd without str node: [" + desc["name"]._str + "]");
											}
											else
											{
												d.writeStr(o[desc["name"]._str]._str);
											}
										}
									}
								}
							}
						}
					}
				}
				else
				{
					bool flag17 = @int == 8;
					if (flag17)
					{
						bool flag18 = !o.ContainsKey(desc["name"]._str) || !o[desc["name"]._str].isArr;
						if (flag18)
						{
							flag = false;
							DebugTrace.add(Define.DebugTrace.DTT_ERR, "rpc cmd without array node: [" + desc["name"]._str + "]");
						}
						else
						{
							List<Variant> arr = o[desc["name"]._str]._arr;
							int num = arr.Count;
							int num2 = (int)(Math.Pow(2.0, (double)desc["vbits"]._int) - 1.0);
							bool flag19 = num > num2;
							if (flag19)
							{
								num = num2;
							}
							d.writeUint((uint)num, desc["vbits"]._int);
							int int3 = desc["vtp"]._int;
							bool flag20 = int3 == 1;
							if (flag20)
							{
								for (int j = 0; j < num; j++)
								{
									d.writeInt(arr[j]._int, desc["bits"]._int);
								}
							}
							else
							{
								bool flag21 = int3 == 2;
								if (flag21)
								{
									for (int j = 0; j < num; j++)
									{
										d.writeUint(arr[j]._uint, desc["bits"]._int);
									}
								}
								else
								{
									bool flag22 = int3 == 3;
									if (flag22)
									{
										for (int j = 0; j < num; j++)
										{
											d.writeBool(arr[j]._bool);
										}
									}
									else
									{
										bool flag23 = int3 == 4;
										if (flag23)
										{
											for (int j = 0; j < num; j++)
											{
												d.writeDecimal(arr[j]._float, desc["bits"]._int);
											}
										}
										else
										{
											bool flag24 = int3 == 5;
											if (flag24)
											{
												for (int j = 0; j < num; j++)
												{
													d.writeUdecimal(arr[j]._float, desc["bits"]._int);
												}
											}
											else
											{
												bool flag25 = int3 == 6;
												if (flag25)
												{
													for (int j = 0; j < num; j++)
													{
														d.writeStr(arr[j]._str);
													}
												}
											}
										}
									}
								}
							}
						}
					}
					else
					{
						bool flag26 = @int == 3;
						if (flag26)
						{
							bool flag27 = !o.ContainsKey(desc["name"]._str) || !o[desc["name"]._str].isArr;
							if (flag27)
							{
								flag = false;
								DebugTrace.add(Define.DebugTrace.DTT_ERR, "rpc cmd without array node: [" + desc["name"]._str + "]");
							}
							else
							{
								int num = o[desc["name"]._str].Length;
								int num2 = (int)(Math.Pow(2.0, (double)desc["bits"]._int) - 1.0);
								bool flag28 = num > num2;
								if (flag28)
								{
									num = num2;
								}
								d.writeUint((uint)num, desc["bits"]._int);
								for (int j = 0; j < num; j++)
								{
									for (int i = 0; i < desc["child"].Length; i++)
									{
										flag = this._PackRPCPackage(o[desc["name"]._str][j], desc["child"][i], d);
										bool flag29 = !flag;
										if (flag29)
										{
											break;
										}
									}
								}
							}
						}
						else
						{
							bool flag30 = @int == 4;
							if (flag30)
							{
								bool flag31 = !o.ContainsKey(desc["name"]._str);
								if (flag31)
								{
									d.writeBool(false);
								}
								else
								{
									d.writeBool(true);
									for (int i = 0; i < desc["child"].Length; i++)
									{
										flag = this._PackRPCPackage(o, desc["child"][i], d);
										bool flag32 = !flag;
										if (flag32)
										{
											break;
										}
									}
								}
							}
							else
							{
								bool flag33 = @int == 9;
								if (flag33)
								{
									bool flag34 = !o.ContainsKey(desc["name"]._str);
									if (flag34)
									{
										flag = false;
										DebugTrace.add(Define.DebugTrace.DTT_ERR, "rpc cmd without object node: [" + desc["name"]._str + "]");
									}
									else
									{
										for (int i = 0; i < desc["child"].Length; i++)
										{
											flag = this._PackRPCPackage(o[desc["name"]._str], desc["child"][i], d);
											bool flag35 = !flag;
											if (flag35)
											{
												break;
											}
										}
									}
								}
								else
								{
									bool flag36 = @int == 5;
									if (flag36)
									{
										bool flag37 = !o.ContainsKey(desc["name"]._str);
										if (flag37)
										{
											flag = false;
											DebugTrace.add(Define.DebugTrace.DTT_ERR, "rpc cmd without switch node: [" + desc["name"]._str + "]");
										}
										else
										{
											bool flag38 = desc["vtp"]._int == 1;
											int num3;
											if (flag38)
											{
												num3 = o[desc["name"]._str]._int;
												d.writeInt(num3, desc["bits"]._int);
											}
											else
											{
												num3 = (int)o[desc["name"]._str]._uint;
												d.writeUint((uint)num3, desc["bits"]._int);
											}
											bool flag39 = false;
											for (int i = 0; i < desc["child"].Length; i++)
											{
												bool flag40 = desc["child"][i]["type"]._int != 6;
												if (!flag40)
												{
													bool flag41 = desc["child"][i]["val"]._int != num3;
													if (!flag41)
													{
														flag39 = true;
														for (int j = 0; j < desc["child"][i]["child"].Length; j++)
														{
															flag = this._PackRPCPackage(o, desc["child"][i]["child"][j], d);
															bool flag42 = !flag;
															if (flag42)
															{
																break;
															}
														}
														break;
													}
												}
											}
											bool flag43 = !flag39;
											if (flag43)
											{
												for (int i = 0; i < desc["child"].Length; i++)
												{
													bool flag44 = desc["child"][i]["type"]._int != 7;
													if (!flag44)
													{
														for (int j = 0; j < desc["child"][i]["child"].Length; j++)
														{
															flag = this._PackRPCPackage(o, desc["child"][i]["child"][j], d);
															bool flag45 = !flag;
															if (flag45)
															{
																break;
															}
														}
														break;
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return flag;
		}

		public ByteArray PackRPCPackage(uint cmdID, Variant par)
		{
			bool flag = !this.c2s_rpc_pkg_desc.ContainsKey(cmdID);
			ByteArray result;
			if (flag)
			{
				result = null;
			}
			else
			{
				BitsArray bitsArray = new BitsArray();
				bool flag2 = this._PackRPCPackage(par, this.c2s_rpc_pkg_desc[cmdID], bitsArray);
				bool flag3 = !flag2;
				if (flag3)
				{
					result = null;
				}
				else
				{
					ByteArray byteArray = new ByteArray();
					byteArray.writeUnsignedByte((byte)cmdID);
					byteArray.writeBytes(bitsArray.data, 0, bitsArray.length / 8);
					result = byteArray;
				}
			}
			return result;
		}

		private bool _UnpackRPCPackage(BitsArray b, Variant desc, Variant parent)
		{
			bool flag = true;
			int @int = desc["type"]._int;
			bool flag2 = @int == 1;
			if (flag2)
			{
				parent["cmd"] = desc["name"];
				parent["data"] = new Variant();
				for (int i = 0; i < desc["child"].Length; i++)
				{
					flag = this._UnpackRPCPackage(b, desc["child"][i], parent["data"]);
					bool flag3 = !flag;
					if (flag3)
					{
						break;
					}
				}
			}
			else
			{
				bool flag4 = @int == 2;
				if (flag4)
				{
					int int2 = desc["vtp"]._int;
					string text = desc["name"]._str.Replace("\0", "");
					bool flag5 = int2 == 1;
					if (flag5)
					{
						bool flag6 = b.bitsAvailable < desc["bits"]._int;
						if (flag6)
						{
							DebugTrace.add(Define.DebugTrace.DTT_ERR, string.Concat(new object[]
							{
								"int value node: [",
								text,
								"] with [",
								desc["bits"]._int,
								"] bits read error with [",
								b.bitsAvailable,
								"] bits_left"
							}));
							flag = false;
						}
						else
						{
							parent[text] = new Variant(b.readInt(desc["bits"]._int));
						}
					}
					else
					{
						bool flag7 = int2 == 2;
						if (flag7)
						{
							bool flag8 = b.bitsAvailable < desc["bits"]._int;
							if (flag8)
							{
								DebugTrace.add(Define.DebugTrace.DTT_ERR, string.Concat(new object[]
								{
									"uint value node: [",
									text,
									"] with [",
									desc["bits"]._int,
									"] bits read error with [",
									b.bitsAvailable,
									"] bits_left"
								}));
								flag = false;
							}
							else
							{
								parent[text] = new Variant(b.readUint(desc["bits"]._int));
							}
						}
						else
						{
							bool flag9 = int2 == 3;
							if (flag9)
							{
								bool flag10 = b.bitsAvailable < 1;
								if (flag10)
								{
									DebugTrace.add(Define.DebugTrace.DTT_ERR, string.Concat(new object[]
									{
										"bool value node: [",
										text,
										"] read error with [",
										b.bitsAvailable,
										"] bits_left"
									}));
									flag = false;
								}
								else
								{
									parent[text] = new Variant(b.readBool());
								}
							}
							else
							{
								bool flag11 = int2 == 4;
								if (flag11)
								{
									bool flag12 = b.bitsAvailable < desc["bits"]._int;
									if (flag12)
									{
										DebugTrace.add(Define.DebugTrace.DTT_ERR, string.Concat(new object[]
										{
											"decimal value node: [",
											text,
											"] with [",
											desc["bits"]._int,
											"] bits read error with [",
											b.bitsAvailable,
											"] bits_left"
										}));
										flag = false;
									}
									else
									{
										parent[text] = new Variant(b.readDecimal(desc["bits"]._int));
									}
								}
								else
								{
									bool flag13 = int2 == 5;
									if (flag13)
									{
										bool flag14 = b.bitsAvailable < desc["bits"]._int;
										if (flag14)
										{
											DebugTrace.add(Define.DebugTrace.DTT_ERR, string.Concat(new object[]
											{
												"udecimal value node: [",
												text,
												"] with [",
												desc["bits"]._int,
												"] bits read error with [",
												b.bitsAvailable,
												"] bits_left"
											}));
											flag = false;
										}
										else
										{
											parent[text] = new Variant(b.readUdecimal(desc["bits"]._int));
										}
									}
									else
									{
										bool flag15 = int2 == 6;
										if (flag15)
										{
											bool flag16 = b.bitsAvailable < 8;
											if (flag16)
											{
												DebugTrace.add(Define.DebugTrace.DTT_ERR, string.Concat(new object[]
												{
													"str value node: [",
													text,
													"] read error with [",
													b.bitsAvailable,
													"] bits_left"
												}));
												flag = false;
											}
											else
											{
												parent[text] = new Variant(b.readStr());
											}
										}
									}
								}
							}
						}
					}
				}
				else
				{
					bool flag17 = @int == 8;
					if (flag17)
					{
						bool flag18 = b.bitsAvailable < desc["vbits"]._int;
						if (flag18)
						{
							DebugTrace.add(Define.DebugTrace.DTT_ERR, string.Concat(new object[]
							{
								"value array node: [",
								desc["name"]._str,
								"] read error with [",
								b.bitsAvailable,
								"] bits_left"
							}));
							flag = false;
						}
						else
						{
							int num = (int)b.readUint(desc["vbits"]._int);
							Variant variant = new Variant();
							variant.setToArray();
							int int3 = desc["vtp"]._int;
							bool flag19 = int3 == 1;
							if (flag19)
							{
								bool flag20 = b.bitsAvailable < desc["bits"]._int * num;
								if (flag20)
								{
									DebugTrace.add(Define.DebugTrace.DTT_ERR, string.Concat(new object[]
									{
										"int array value node: [",
										desc["name"]._str,
										"] with [",
										desc["bits"]._int,
										"] bits read error with [",
										b.bitsAvailable,
										"] bits_left"
									}));
									flag = false;
								}
								else
								{
									for (int j = 0; j < num; j++)
									{
										variant.pushBack(new Variant(b.readInt(desc["bits"]._int)));
									}
								}
							}
							else
							{
								bool flag21 = int3 == 2;
								if (flag21)
								{
									bool flag22 = b.bitsAvailable < desc["bits"]._int * num;
									if (flag22)
									{
										DebugTrace.add(Define.DebugTrace.DTT_ERR, string.Concat(new object[]
										{
											"uint array value node: [",
											desc["name"]._str,
											"] with [",
											desc["bits"]._int,
											"] bits read error with [",
											b.bitsAvailable,
											"] bits_left"
										}));
										flag = false;
									}
									else
									{
										for (int j = 0; j < num; j++)
										{
											variant.pushBack(new Variant(b.readUint(desc["bits"]._int)));
										}
									}
								}
								else
								{
									bool flag23 = int3 == 3;
									if (flag23)
									{
										bool flag24 = b.bitsAvailable < num;
										if (flag24)
										{
											DebugTrace.add(Define.DebugTrace.DTT_ERR, string.Concat(new object[]
											{
												"bool array value node: [",
												desc["name"]._str,
												"] read error with [",
												b.bitsAvailable,
												"] bits_left"
											}));
											flag = false;
										}
										else
										{
											for (int j = 0; j < num; j++)
											{
												variant.pushBack(new Variant(b.readBool()));
											}
										}
									}
									else
									{
										bool flag25 = int3 == 4;
										if (flag25)
										{
											bool flag26 = b.bitsAvailable < desc["bits"]._int * num;
											if (flag26)
											{
												DebugTrace.add(Define.DebugTrace.DTT_ERR, string.Concat(new object[]
												{
													"decimal array value node: [",
													desc["name"]._str,
													"] with [",
													desc["bits"]._int,
													"] bits read error with [",
													b.bitsAvailable,
													"] bits_left"
												}));
												flag = false;
											}
											else
											{
												for (int j = 0; j < num; j++)
												{
													variant.pushBack(new Variant(b.readDecimal(desc["bits"]._int)));
												}
											}
										}
										else
										{
											bool flag27 = int3 == 5;
											if (flag27)
											{
												bool flag28 = b.bitsAvailable < desc["bits"]._int * num;
												if (flag28)
												{
													DebugTrace.add(Define.DebugTrace.DTT_ERR, string.Concat(new object[]
													{
														"udecimal array value node: [",
														desc["name"]._str,
														"] with [",
														desc["bits"]._int,
														"] bits read error with [",
														b.bitsAvailable,
														"] bits_left"
													}));
													flag = false;
												}
												else
												{
													for (int j = 0; j < num; j++)
													{
														variant.pushBack(new Variant(b.readUdecimal(desc["bits"]._int)));
													}
												}
											}
											else
											{
												bool flag29 = int3 == 6;
												if (flag29)
												{
													for (int j = 0; j < num; j++)
													{
														bool flag30 = b.bitsAvailable < 8;
														if (flag30)
														{
															DebugTrace.add(Define.DebugTrace.DTT_ERR, string.Concat(new object[]
															{
																"str array value node: [",
																desc["name"]._str,
																"] read error with [",
																b.bitsAvailable,
																"] bits_left"
															}));
															flag = false;
															break;
														}
														variant.pushBack(new Variant(b.readStr()));
													}
												}
											}
										}
									}
								}
							}
							parent[desc["name"]._str] = variant;
						}
					}
					else
					{
						bool flag31 = @int == 3;
						if (flag31)
						{
							bool flag32 = b.bitsAvailable < desc["bits"]._int;
							if (flag32)
							{
								DebugTrace.add(Define.DebugTrace.DTT_ERR, string.Concat(new object[]
								{
									"array node: [",
									desc["name"]._str,
									"] read error with [",
									b.bitsAvailable,
									"] bits_left"
								}));
								flag = false;
							}
							else
							{
								string key = desc["name"]._str.Replace("\0", "");
								int num = (int)b.readUint(desc["bits"]._int);
								parent[key] = new Variant();
								parent[key].setToArray();
								for (int j = 0; j < num; j++)
								{
									parent[key].pushBack(new Variant());
									for (int i = 0; i < desc["child"].Length; i++)
									{
										flag = this._UnpackRPCPackage(b, desc["child"][i], parent[key][j]);
										bool flag33 = !flag;
										if (flag33)
										{
											break;
										}
									}
								}
							}
						}
						else
						{
							bool flag34 = @int == 4;
							if (flag34)
							{
								bool flag35 = b.bitsAvailable < 1;
								if (flag35)
								{
									DebugTrace.add(Define.DebugTrace.DTT_ERR, string.Concat(new object[]
									{
										"if node: [",
										desc["name"]._str,
										"] read error with [",
										b.bitsAvailable,
										"] bits_left"
									}));
									flag = false;
								}
								else
								{
									bool flag36 = b.readBool();
									if (flag36)
									{
										for (int i = 0; i < desc["child"].Length; i++)
										{
											flag = this._UnpackRPCPackage(b, desc["child"][i], parent);
											bool flag37 = !flag;
											if (flag37)
											{
												break;
											}
										}
									}
								}
							}
							else
							{
								bool flag38 = @int == 9;
								if (flag38)
								{
									string key2 = desc["name"]._str.Replace("\0", "");
									parent[key2] = new Variant();
									for (int i = 0; i < desc["child"].Length; i++)
									{
										flag = this._UnpackRPCPackage(b, desc["child"][i], parent[key2]);
										bool flag39 = !flag;
										if (flag39)
										{
											break;
										}
									}
								}
								else
								{
									bool flag40 = @int == 5;
									if (flag40)
									{
										string text2 = desc["name"]._str.Replace("\0", "");
										bool flag41 = b.bitsAvailable < desc["bits"]._int;
										if (flag41)
										{
											DebugTrace.add(Define.DebugTrace.DTT_ERR, string.Concat(new object[]
											{
												"switch node: [",
												text2,
												"] with [",
												desc["bits"]._int,
												"] bits read error with [",
												b.bitsAvailable,
												"] bits_left"
											}));
											flag = false;
										}
										else
										{
											bool flag42 = desc["vtp"]._int == 1;
											int num2;
											if (flag42)
											{
												num2 = b.readInt(desc["bits"]._int);
												parent[text2] = new Variant(num2);
											}
											else
											{
												num2 = (int)b.readUint(desc["bits"]._int);
												parent[text2] = new Variant((uint)num2);
											}
											bool flag43 = false;
											for (int i = 0; i < desc["child"].Length; i++)
											{
												bool flag44 = desc["child"][i]["type"] != 6;
												if (!flag44)
												{
													bool flag45 = desc["child"][i]["val"] != num2;
													if (!flag45)
													{
														flag43 = true;
														for (int j = 0; j < desc["child"][i]["child"].Length; j++)
														{
															flag = this._UnpackRPCPackage(b, desc["child"][i]["child"][j], parent);
															bool flag46 = !flag;
															if (flag46)
															{
																break;
															}
														}
														break;
													}
												}
											}
											bool flag47 = !flag43;
											if (flag47)
											{
												for (int i = 0; i < desc["child"].Length; i++)
												{
													bool flag48 = desc["child"][i]["type"]._int != 7;
													if (!flag48)
													{
														for (int j = 0; j < desc["child"][i]["child"].Length; j++)
														{
															flag = this._UnpackRPCPackage(b, desc["child"][i]["child"][j], parent);
															bool flag49 = !flag;
															if (flag49)
															{
																break;
															}
														}
														break;
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return flag;
		}

		public Variant UnpackRPCPackage(byte[] bytes, int length)
		{
			Variant variant = new Variant();
			bool flag = length < 1;
			Variant result;
			if (flag)
			{
				result = variant;
			}
			else
			{
				ByteArray byteArray = new ByteArray(bytes, length);
				uint num = (uint)byteArray.readUnsignedByte();
				variant["cmd_id"] = num;
				bool flag2 = !this.s2c_rpc_pkg_desc.ContainsKey(num);
				if (flag2)
				{
					result = variant;
				}
				else
				{
					bool flag3 = this._UnpackRPCPackage(new BitsArray
					{
						data = byteArray,
						position = 8
					}, this.s2c_rpc_pkg_desc[num], variant);
					bool flag4 = !flag3;
					if (flag4)
					{
						result = new Variant();
					}
					else
					{
						result = variant;
					}
				}
			}
			return result;
		}

		protected ByteArray _PackTypePackage(Variant o)
		{
			ByteArray byteArray = new ByteArray();
			int num = 0;
			bool isBool = o.isBool;
			if (isBool)
			{
				bool @bool = o._bool;
				if (@bool)
				{
					num = 16;
				}
				else
				{
					num = 0;
				}
				byteArray.writeUnsignedByte((byte)num);
			}
			else
			{
				bool isInteger = o.isInteger;
				if (isInteger)
				{
					bool flag = o._int64 < 0L;
					if (flag)
					{
						bool flag2 = o._int64 >= -127L;
						if (flag2)
						{
							int num2 = 0;
							num = (num2 << 5 | 17);
							byteArray.writeUnsignedByte((byte)num);
							byteArray.writeByte((sbyte)o._int);
						}
						else
						{
							bool flag3 = o._int64 >= -32767L;
							if (flag3)
							{
								int num2 = 1;
								num = (num2 << 5 | 17);
								byteArray.writeUnsignedByte((byte)num);
								byteArray.writeShort((short)o._int);
							}
							else
							{
								bool flag4 = o._int64 >= -134217727L;
								if (flag4)
								{
									int num2 = 2;
									num = (num2 << 5 | 17);
									byteArray.writeUnsignedByte((byte)num);
									byteArray.writeInt(o._int);
								}
								else
								{
									int num2 = 3;
									num = (num2 << 5 | 17);
									byteArray.writeUnsignedByte((byte)num);
									byteArray.writeUnsignedInt((uint)(o._int64 % 4294967296L));
									byteArray.writeInt((int)(o._int64 >> 32));
								}
							}
						}
					}
					else
					{
						bool flag5 = o._uint64 <= 255uL;
						if (flag5)
						{
							int num2 = 0;
							num = (num2 << 5 | 1);
							byteArray.writeUnsignedByte((byte)num);
							byteArray.writeUnsignedByte((byte)o._uint);
						}
						else
						{
							bool flag6 = o._uint64 <= 65535uL;
							if (flag6)
							{
								int num2 = 1;
								num = (num2 << 5 | 1);
								byteArray.writeUnsignedByte((byte)num);
								byteArray.writeUnsignedShort((ushort)o._uint);
							}
							else
							{
								bool flag7 = o._uint64 <= (ulong)-1;
								if (flag7)
								{
									int num2 = 2;
									num = (num2 << 5 | 1);
									byteArray.writeUnsignedByte((byte)num);
									byteArray.writeUnsignedInt(o._uint);
								}
								else
								{
									int num2 = 3;
									num = (num2 << 5 | 1);
									byteArray.writeUnsignedByte((byte)num);
									byteArray.writeUnsignedInt((uint)(o._uint64 % 4294967296uL));
									byteArray.writeUnsignedInt((uint)(o._uint64 >> 32));
								}
							}
						}
					}
				}
				else
				{
					bool isNumber = o.isNumber;
					if (isNumber)
					{
						bool flag8 = (o._double <= 3.4E+38 && o._double >= 3.4E-38) || (o._double >= -3.4E-38 && o._double <= -3.4E+38);
						if (flag8)
						{
							int num2 = 2;
							num = (num2 << 5 | 18);
							byteArray.writeUnsignedByte((byte)num);
							byteArray.writeFloat(o._float);
						}
						else
						{
							int num2 = 3;
							num = (num2 << 5 | 18);
							byteArray.writeUnsignedByte((byte)num);
							byteArray.writeDouble(o._double);
						}
					}
					else
					{
						bool isStr = o.isStr;
						if (isStr)
						{
							int position = byteArray.position;
							byteArray.writeShort((short)num);
							byteArray.writeUTF8Bytes(o._str);
							byteArray.writeByte(0);
							int num3 = byteArray.position - position - 2;
							int position2 = byteArray.position;
							byteArray.position = position;
							bool flag9 = num3 > 4095;
							if (flag9)
							{
								num3 = 0;
							}
							num = (num3 << 4 | 3);
							byteArray.writeShort((short)num);
							byteArray.position = position2;
						}
						else
						{
							bool isArr = o.isArr;
							if (isArr)
							{
								int num3 = o.Length;
								num = (num3 << 4 | 5);
								byteArray.writeShort((short)num);
								for (int i = 0; i < o.Length; i++)
								{
									ByteArray byteArray2 = this._PackTypePackage(o[i]);
									byteArray.writeBytes(byteArray2, 0, byteArray2.length);
								}
							}
							else
							{
								bool isDct = o.isDct;
								if (isDct)
								{
									int num3 = o.Keys.Count;
									num = (num3 << 4 | 4);
									byteArray.writeShort((short)num);
									foreach (string current in o.Keys)
									{
										string text = current;
										bool flag10 = text.Length > 4;
										if (flag10)
										{
											text = current.Substring(0, 4);
										}
										byteArray.writeUTF8Bytes(text);
										for (int j = text.Length; j < 4; j++)
										{
											byteArray.writeByte(0);
										}
										ByteArray byteArray3 = this._PackTypePackage(o[current]);
										byteArray.writeBytes(byteArray3, 0, byteArray3.length);
									}
								}
							}
						}
					}
				}
			}
			return byteArray;
		}

		public ByteArray PackTypePackage(uint cmdID, Variant par)
		{
			ByteArray byteArray = new ByteArray();
			ByteArray byteArray2 = this._PackTypePackage(par);
			byteArray.writeShort((short)cmdID);
			byteArray.writeBytes(byteArray2, 0, byteArray2.length);
			return byteArray;
		}

		private Variant _UnpackTypePackage(ByteArray d)
		{
			Variant variant = new Variant();
			bool flag = d.bytesAvailable < 1;
			Variant result;
			if (flag)
			{
				result = variant;
			}
			else
			{
				uint num = (uint)d.readUnsignedByte();
				uint num2 = num & 15u;
				bool flag2 = num2 == 0u;
				if (flag2)
				{
					variant = new Variant((num & 16u) > 0u);
				}
				else
				{
					bool flag3 = num2 == 1u;
					if (flag3)
					{
						bool flag4 = (num & 16u) > 0u;
						int num3 = (int)(num >> 5);
						bool flag5 = flag4;
						if (flag5)
						{
							bool flag6 = num3 == 2;
							if (flag6)
							{
								variant = new Variant(d.readInt());
							}
							else
							{
								bool flag7 = num3 == 1;
								if (flag7)
								{
									variant = new Variant(d.readShort());
								}
								else
								{
									bool flag8 = num3 == 0;
									if (flag8)
									{
										variant = new Variant(d.readByte());
									}
									else
									{
										bool flag9 = num3 == 3;
										if (flag9)
										{
											variant = new Variant((long)((ulong)d.readUnsignedInt() + (ulong)((long)d.readInt() * 4294967296L)));
										}
									}
								}
							}
						}
						else
						{
							bool flag10 = num3 == 2;
							if (flag10)
							{
								variant = new Variant(d.readUnsignedInt());
							}
							else
							{
								bool flag11 = num3 == 1;
								if (flag11)
								{
									variant = new Variant(d.readUnsignedShort());
								}
								else
								{
									bool flag12 = num3 == 0;
									if (flag12)
									{
										variant = new Variant(d.readUnsignedByte());
									}
									else
									{
										bool flag13 = num3 == 3;
										if (flag13)
										{
											variant = new Variant((long)((ulong)d.readUnsignedInt() + (ulong)d.readUnsignedInt() * 4294967296uL));
										}
									}
								}
							}
						}
					}
					else
					{
						bool flag14 = num2 == 2u;
						if (flag14)
						{
							int num3 = (int)(num >> 5);
							int num4 = num3;
							if (num4 != 2)
							{
								if (num4 == 3)
								{
									variant = new Variant(d.readDouble());
								}
							}
							else
							{
								variant = new Variant(d.readFloat());
							}
						}
						else
						{
							bool flag15 = num2 == 3u;
							if (flag15)
							{
								uint num5 = (uint)d.readUnsignedByte();
								int num6 = (int)(num5 << 4 | num >> 4);
								bool flag16 = num6 == 0;
								if (flag16)
								{
									ByteArray byteArray = new ByteArray();
									byte b = d.readUnsignedByte();
									while (b != 0 && d.bytesAvailable > 0)
									{
										byteArray.writeByte((sbyte)b);
										b = d.readUnsignedByte();
									}
									byteArray.position = 0;
									variant = new Variant(byteArray.readUTF8Bytes(byteArray.length));
								}
								else
								{
									variant = new Variant(d.readUTF8Bytes(num6));
								}
							}
							else
							{
								bool flag17 = num2 == 4u;
								if (flag17)
								{
									uint num5 = (uint)d.readUnsignedByte();
									int num6 = (int)(num5 << 4 | num >> 4);
									variant = new Variant();
									variant.setToDct();
									for (int i = 0; i < num6; i++)
									{
										string text = d.readUTF8Bytes(4);
										int num7 = text.LastIndexOf('\0');
										bool flag18 = num7 != -1;
										if (flag18)
										{
											text = text.Substring(0, num7);
										}
										variant[text] = this._UnpackTypePackage(d);
									}
								}
								else
								{
									bool flag19 = num2 == 5u;
									if (flag19)
									{
										uint num5 = (uint)d.readUnsignedByte();
										int num6 = (int)(num5 << 4 | num >> 4);
										variant = new Variant();
										variant.setToArray();
										for (int j = 0; j < num6; j++)
										{
											variant.pushBack(this._UnpackTypePackage(d));
										}
									}
								}
							}
						}
					}
				}
				result = variant;
			}
			return result;
		}

		public Variant UnpackTypePackage(byte[] bytes, int length)
		{
			Variant variant = new Variant();
			bool flag = length < 3;
			Variant result;
			if (flag)
			{
				result = variant;
			}
			else
			{
				ByteArray byteArray = new ByteArray(bytes, length);
				variant["cmd"] = new Variant(byteArray.readUnsignedShort());
				variant["data"] = this._UnpackTypePackage(byteArray);
				result = variant;
			}
			return result;
		}

		public static ByteArray _PackFullTypePackage(Variant obj)
		{
			ByteArray byteArray = new ByteArray();
			int num = 0;
			bool isBool = obj.isBool;
			if (isBool)
			{
				bool @bool = obj._bool;
				if (@bool)
				{
					num = 16;
				}
				else
				{
					num = 0;
				}
				byteArray.writeByte((sbyte)num);
			}
			else
			{
				bool isInteger = obj.isInteger;
				if (isInteger)
				{
					bool flag = obj._int64 < 0L;
					if (flag)
					{
						bool flag2 = obj._int64 >= -127L;
						if (flag2)
						{
							int num2 = 0;
							num = (num2 << 5 | 17);
							byteArray.writeUnsignedByte((byte)num);
							byteArray.writeByte(obj._sbyte);
						}
						else
						{
							bool flag3 = obj._int64 >= -32767L;
							if (flag3)
							{
								int num2 = 1;
								num = (num2 << 5 | 17);
								byteArray.writeUnsignedByte((byte)num);
								byteArray.writeShort(obj._int16);
							}
							else
							{
								bool flag4 = obj._int64 >= -134217727L;
								if (flag4)
								{
									int num2 = 2;
									num = (num2 << 5 | 17);
									byteArray.writeUnsignedByte((byte)num);
									byteArray.writeInt(obj._int);
								}
								else
								{
									int num2 = 3;
									num = (num2 << 5 | 17);
									byteArray.writeUnsignedByte((byte)num);
									byteArray.writeUnsignedInt((uint)(obj._int64 % 4294967296L));
									byteArray.writeInt((int)(obj._int64 >> 32));
								}
							}
						}
					}
					else
					{
						bool flag5 = obj._uint64 <= 255uL;
						if (flag5)
						{
							int num2 = 0;
							num = (num2 << 5 | 1);
							byteArray.writeUnsignedByte((byte)num);
							byteArray.writeUnsignedByte(obj._byte);
						}
						else
						{
							bool flag6 = obj._uint64 <= 65535uL;
							if (flag6)
							{
								int num2 = 1;
								num = (num2 << 5 | 1);
								byteArray.writeUnsignedByte((byte)num);
								byteArray.writeUnsignedShort((ushort)obj._int16);
							}
							else
							{
								bool flag7 = obj._uint64 <= (ulong)-1;
								if (flag7)
								{
									int num2 = 2;
									num = (num2 << 5 | 1);
									byteArray.writeUnsignedByte((byte)num);
									byteArray.writeUnsignedInt(obj._uint);
								}
								else
								{
									int num2 = 3;
									num = (num2 << 5 | 1);
									byteArray.writeUnsignedByte((byte)num);
									byteArray.writeUnsignedInt((uint)(obj._uint64 % 4294967296uL));
									byteArray.writeUnsignedInt((uint)(obj._uint64 >> 32));
								}
							}
						}
					}
				}
				else
				{
					bool isNumber = obj.isNumber;
					if (isNumber)
					{
						bool flag8 = (obj._double <= 3.4E+38 && obj._double >= 3.4E-38) || (obj._double >= -3.4E-38 && obj._double <= -3.4E+38);
						if (flag8)
						{
							int num2 = 2;
							num = (num2 << 5 | 18);
							byteArray.writeUnsignedByte((byte)num);
							byteArray.writeFloat(obj._float);
						}
						else
						{
							int num2 = 3;
							num = (num2 << 5 | 18);
							byteArray.writeUnsignedByte((byte)num);
							byteArray.writeDouble(obj._double);
						}
					}
					else
					{
						bool isStr = obj.isStr;
						if (isStr)
						{
							int position = byteArray.position;
							byteArray.writeUnsignedInt((uint)num);
							byteArray.writeUTF8Bytes(obj._str);
							byteArray.writeByte(0);
							int num3 = byteArray.position - position - 4;
							int position2 = byteArray.position;
							byteArray.position = position;
							num = (num3 << 4 | 3);
							byteArray.writeUnsignedInt((uint)num);
							byteArray.position = position2;
						}
						else
						{
							bool isArr = obj.isArr;
							if (isArr)
							{
								int num3 = obj.Length;
								num = (num3 << 4 | 5);
								byteArray.writeUnsignedInt((uint)num);
								for (int i = 0; i < num3; i++)
								{
									ByteArray byteArray2 = PackageCoderImpl._PackFullTypePackage(obj[i]);
									byteArray.writeBytes(byteArray2, 0, byteArray2.length);
								}
							}
							else
							{
								bool isByteArray = obj.isByteArray;
								if (isByteArray)
								{
									int num3 = obj._byteAry.length;
									num = (num3 << 4 | 6);
									byteArray.writeUnsignedInt((uint)num);
									byteArray.writeBytes(obj._byteAry, 0, obj._byteAry.length);
								}
								else
								{
									bool isDct = obj.isDct;
									if (isDct)
									{
										int num3 = obj.Length;
										num = (num3 << 4 | 4);
										byteArray.writeUnsignedInt((uint)num);
										foreach (string current in obj.Keys)
										{
											byteArray.writeUnsignedByte((byte)current.Length);
											byteArray.writeUTF8Bytes(current);
											ByteArray byteArray3 = PackageCoderImpl._PackFullTypePackage(obj[current]);
											byteArray.writeBytes(byteArray3, 0, byteArray3.length);
										}
									}
									else
									{
										bool isIntkeyDct = obj.isIntkeyDct;
										if (isIntkeyDct)
										{
											int num3 = obj.Length;
											num = (num3 << 4 | 7);
											byteArray.writeUnsignedInt((uint)num);
											foreach (int current2 in obj.IntKeys)
											{
												byteArray.writeInt(current2);
												ByteArray byteArray4 = PackageCoderImpl._PackFullTypePackage(obj[current2]);
												byteArray.writeBytes(byteArray4, 0, byteArray4.length);
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return byteArray;
		}

		private static Variant _UnpackFullTypePackage(ByteArray d)
		{
			Variant variant = null;
			bool flag = d.bytesAvailable < 1;
			Variant result;
			if (flag)
			{
				result = variant;
			}
			else
			{
				uint num = (uint)d.readUnsignedByte();
				uint num2 = num & 15u;
				bool flag2 = num2 == 0u;
				if (flag2)
				{
					variant = new Variant((num & 16u) > 0u);
				}
				else
				{
					bool flag3 = num2 == 1u;
					if (flag3)
					{
						bool flag4 = (num & 16u) > 0u;
						uint num3 = num >> 5;
						bool flag5 = flag4;
						if (flag5)
						{
							bool flag6 = num3 == 2u;
							if (flag6)
							{
								variant = new Variant(d.readInt());
							}
							else
							{
								bool flag7 = num3 == 1u;
								if (flag7)
								{
									variant = new Variant(d.readShort());
								}
								else
								{
									bool flag8 = num3 == 0u;
									if (flag8)
									{
										variant = new Variant(d.readByte());
									}
									else
									{
										bool flag9 = num3 == 3u;
										if (flag9)
										{
											variant = new Variant((long)((ulong)d.readUnsignedInt() + (ulong)((long)d.readInt() * 4294967296L)));
										}
									}
								}
							}
						}
						else
						{
							bool flag10 = num3 == 2u;
							if (flag10)
							{
								variant = new Variant(d.readUnsignedInt());
							}
							else
							{
								bool flag11 = num3 == 1u;
								if (flag11)
								{
									variant = new Variant(d.readUnsignedShort());
								}
								else
								{
									bool flag12 = num3 == 0u;
									if (flag12)
									{
										variant = new Variant(d.readUnsignedByte());
									}
									else
									{
										bool flag13 = num3 == 3u;
										if (flag13)
										{
											variant = new Variant((long)((ulong)d.readUnsignedInt() + (ulong)((long)d.readInt() * 4294967296L)));
										}
									}
								}
							}
						}
					}
					else
					{
						bool flag14 = num2 == 2u;
						if (flag14)
						{
							uint num3 = num >> 5;
							uint num4 = num3;
							if (num4 != 2u)
							{
								if (num4 == 3u)
								{
									variant = new Variant(d.readDouble());
								}
							}
							else
							{
								variant = new Variant(d.readFloat());
							}
						}
						else
						{
							bool flag15 = num2 == 3u;
							if (flag15)
							{
								uint num5 = (uint)d.readUnsignedByte();
								uint num6 = (uint)d.readUnsignedShort();
								int num7 = (int)((num6 << 16 | num5 << 8 | num) >> 4);
								variant = new Variant(d.readUTF8Bytes(num7));
							}
							else
							{
								bool flag16 = num2 == 4u;
								if (flag16)
								{
									uint num5 = (uint)d.readUnsignedByte();
									uint num6 = (uint)d.readUnsignedShort();
									int num7 = (int)((num6 << 16 | num5 << 8 | num) >> 4);
									variant = new Variant();
									variant.setToDct();
									for (int i = 0; i < num7; i++)
									{
										int len = (int)d.readUnsignedByte();
										string text = d.readUTF8Bytes(len);
										int num8 = text.LastIndexOf("\0");
										bool flag17 = num8 != -1;
										if (flag17)
										{
											text = text.Substring(0, num8);
										}
										variant[text] = PackageCoderImpl._UnpackFullTypePackage(d);
										int num9 = variant[text]._str.LastIndexOf("\0");
										bool flag18 = num9 != -1;
										if (flag18)
										{
											string text2 = variant[text]._str.Substring(0, num9);
											variant[text] = variant[text]._str.Substring(0, num9);
										}
									}
								}
								else
								{
									bool flag19 = num2 == 5u;
									if (flag19)
									{
										uint num5 = (uint)d.readUnsignedByte();
										uint num6 = (uint)d.readUnsignedShort();
										int num7 = (int)((num6 << 16 | num5 << 8 | num) >> 4);
										variant = new Variant();
										variant.setToArray();
										for (int j = 0; j < num7; j++)
										{
											variant.pushBack(PackageCoderImpl._UnpackFullTypePackage(d));
										}
									}
									else
									{
										bool flag20 = num2 == 6u;
										if (flag20)
										{
											uint num5 = (uint)d.readUnsignedByte();
											uint num6 = (uint)d.readUnsignedShort();
											int num7 = (int)((num6 << 16 | num5 << 8 | num) >> 4);
											ByteArray byteArray = new ByteArray();
											d.readBytes(byteArray, 0, num7);
											variant = new Variant(byteArray);
										}
										else
										{
											bool flag21 = num2 == 7u;
											if (flag21)
											{
												uint num5 = (uint)d.readUnsignedByte();
												uint num6 = (uint)d.readUnsignedShort();
												int num7 = (int)((num6 << 16 | num5 << 8 | num) >> 4);
												variant = new Variant();
												variant.setToIntkeyDct();
												for (int k = 0; k < num7; k++)
												{
													int idx = d.readInt();
													variant[idx] = PackageCoderImpl._UnpackFullTypePackage(d);
												}
											}
										}
									}
								}
							}
						}
					}
				}
				result = variant;
			}
			return result;
		}

		public ByteArray PackFullTypePackage(uint cmdID, Variant par)
		{
			return null;
		}

		public Variant UnpackFullTypePackage(byte[] bytes, int length)
		{
			Variant variant = new Variant();
			bool flag = length < 3;
			Variant result;
			if (flag)
			{
				result = variant;
			}
			else
			{
				ByteArray byteArray = new ByteArray(bytes, length);
				variant["cmd"] = new Variant(byteArray.readUnsignedShort());
				variant["data"] = PackageCoderImpl._UnpackFullTypePackage(byteArray);
				result = variant;
			}
			return result;
		}

		public Variant UnpackFullTypePackage(ByteArray d)
		{
			return PackageCoderImpl._UnpackFullTypePackage(d);
		}

		public static ByteArray SerializeObject(Variant par)
		{
			ByteArray byteArray = PackageCoderImpl._PackFullTypePackage(par);
			byteArray.compress();
			return byteArray;
		}

		public static Variant UnserializeObject(byte[] bytes, int length)
		{
			ByteArray byteArray = new ByteArray(length);
			byteArray.pushBack(bytes, length);
			byteArray.uncompress();
			return PackageCoderImpl._UnpackFullTypePackage(byteArray);
		}
	}
}
