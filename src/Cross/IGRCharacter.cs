using System;

namespace Cross
{
	public interface IGRCharacter : IGREntity
	{
		string curAnimName
		{
			get;
		}

		bool shadow
		{
			get;
			set;
		}

		float chaHeight
		{
			get;
		}

		Vec3 characterPos(Vec3 v);

		void attachEntity(string attachID, IGREntity ent);

		IGREntity dettachEntity(string attachID);

		IGRCharacter mountEntity(string mountPart, IGRCharacter ent);

		IGRCharacter unmountEntity();

		void applyAvatar(Variant conf, string mtrlId = null);

		void removeAvatar(string partID);

		IGRAvatarPart getAvatar(string partID);

		IGREntity getWeapon(string attachtoID);

		void playAnimation(string aniName, int loop);

		void pauseAnimation();

		void resumeAnimation();

		void stopAnimation();

		bool hasAnim(string aniName);

		void showAvatar(bool b);
	}
}
