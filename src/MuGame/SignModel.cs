using System;

namespace MuGame
{
	internal class SignModel : ModelBase<SignModel>
	{
		public SignData ReadXML(int i)
		{
			SignData result = default(SignData);
			SXML sXML = XMLMgr.instance.GetSXML("signup.signup", "signup_times==" + i);
			bool flag = sXML != null;
			if (flag)
			{
				result.sign_day = sXML.getInt("SIGNUP_TIMES");
				result.vip_lv = sXML.getString("vip_double");
				result.item_id = sXML.getString("item_id");
				result.num = sXML.getInt("num");
			}
			return result;
		}
	}
}
