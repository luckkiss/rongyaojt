using System;

namespace MuGame
{
	public interface LGIUIOutGame
	{
		void onStartConnectServer();

		void onLoginFailed();

		void onConnFailed();

		void onConnLost();

		void showCreate();

		void showSelect();

		void backSelectCharacter();

		void showMessage(string msg);

		void showLoginLine(int idx);

		void refreshChaList();

		void createChaError(int res);

		void deleteChaError(int res);

		void showError(int res);

		void clearAll();
	}
}
