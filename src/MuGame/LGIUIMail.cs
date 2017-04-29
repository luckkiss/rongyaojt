using Cross;
using System;

namespace MuGame
{
	public interface LGIUIMail
	{
		void refreshMailList(Variant mailList);

		void openMail(Variant mail);

		void delMailItems(uint mailID);

		void lockMail(uint mailID);

		void unlockMail(uint mailID);
	}
}
