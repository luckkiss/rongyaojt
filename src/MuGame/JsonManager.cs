using Cross;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MuGame
{
	public class JsonManager
	{
		private static Dictionary<string, string> data_value = new Dictionary<string, string>();

		public static Variant StringToVariant(string jsonString, bool one = true)
		{
			if (one)
			{
				JsonManager.data_value.Clear();
			}
			Variant variant = new Variant();
			bool flag = jsonString.Length <= 0;
			Variant result;
			if (flag)
			{
				DebugTrace.print(" >>>>>>>>>>>> JsonManager StringToVariant err! <<<<<<<<<<<< ");
				result = variant;
			}
			else
			{
				jsonString = jsonString.Remove(0, 1);
				jsonString = jsonString.Remove(jsonString.Length - 1);
				jsonString = JsonManager.setMtool(jsonString, '[', ']', "data_arr");
				jsonString = JsonManager.setMtool(jsonString, '{', '}', "data_value");
				string[] array = jsonString.Split(new char[]
				{
					','
				});
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text = array2[i];
					string text2 = text.Replace("\"", "");
					text2 = text2.Replace("\\", "");
					string[] array3 = text2.Split(new char[]
					{
						':'
					});
					bool flag2 = array3.Length < 2;
					if (flag2)
					{
						bool flag3 = array3.Length != 0;
						if (!flag3)
						{
							DebugTrace.print("Erorr: jsons Text Erorr");
							result = null;
							return result;
						}
						bool flag4 = array3[0].Contains("data_value");
						if (flag4)
						{
							variant._arr.Add(JsonManager.StringToVariant(JsonManager.data_value[array3[0]], false));
						}
						else
						{
							bool flag5 = array3[0].Contains("data_arr");
							if (!flag5)
							{
								DebugTrace.print("Erorr: jsons Text Erorr");
								result = null;
								return result;
							}
							variant._arr.Add(JsonManager.StringToVariant(JsonManager.data_value[array3[0]], false));
						}
					}
					else
					{
						bool flag6 = array3.Length > 2;
						if (flag6)
						{
							for (int j = 2; j < array3.Length; j++)
							{
								string[] expr_1C6_cp_0 = array3;
								int expr_1C6_cp_1 = 1;
								expr_1C6_cp_0[expr_1C6_cp_1] = expr_1C6_cp_0[expr_1C6_cp_1] + ":" + array3[j];
							}
						}
						bool flag7 = array3[1].Contains("data_value");
						if (flag7)
						{
							variant[array3[0]] = JsonManager.StringToVariant(JsonManager.data_value[array3[1]], false);
						}
						else
						{
							bool flag8 = array3[1].Contains("data_arr");
							if (flag8)
							{
								variant[array3[0]] = JsonManager.StringToVariant(JsonManager.data_value[array3[1]], false);
							}
							else
							{
								variant[array3[0]] = array3[1];
							}
						}
					}
				}
				result = variant;
			}
			return result;
		}

		public static string VariantToString(Variant v)
		{
			string text = "{";
			foreach (string current in v.Keys)
			{
				text = string.Concat(new string[]
				{
					text,
					"\"",
					current,
					"\":\"",
					v[current]._str,
					"\","
				});
			}
			text = text.Remove(text.Length - 1);
			text += "}";
			return text;
		}

		private static string setMtool(string jsonString, char m, char mg, string data)
		{
			bool flag = !jsonString.Contains(m) && !jsonString.Contains(mg);
			string result;
			if (flag)
			{
				result = jsonString;
			}
			else
			{
				int num = jsonString.IndexOf(m);
				int num2 = 0;
				int num3 = 0;
				string text = "";
				for (int i = 0; i < jsonString.Length; i++)
				{
					bool flag2 = i >= num;
					if (flag2)
					{
						text += jsonString[i].ToString();
						bool flag3 = jsonString[i] == m;
						if (flag3)
						{
							num2++;
						}
						else
						{
							bool flag4 = jsonString[i] == mg;
							if (flag4)
							{
								num3++;
							}
						}
						bool flag5 = num2 == num3;
						if (flag5)
						{
							break;
						}
					}
				}
				bool flag6 = text == "";
				if (flag6)
				{
					result = jsonString;
				}
				else
				{
					jsonString = jsonString.Replace(text, data + JsonManager.data_value.Count);
					JsonManager.data_value[data + JsonManager.data_value.Count] = text;
					result = JsonManager.setMtool(jsonString, m, mg, data);
				}
			}
			return result;
		}
	}
}
