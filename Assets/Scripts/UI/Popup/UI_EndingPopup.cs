using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class UI_EndingPopup : UI_Popup
{
    enum GameObjects
    {
        FirstScene,
        SecondScene,
		ThirdScene,
		BadFirstScene,
		BadSecondScene
    }

	private int endIndex = 0;
	private int endingSet = 0;

	public override bool Init()
	{
		if (base.Init() == false)
			return false;

		
		if(endingSet == 1)
        {
			//엔딩
			Managers.Sound.Play(Sound.Effect, "Sound_Ending2", volume: 0.2f);

			this.endIndex = 0;

			BindObject(typeof(GameObjects));

			gameObject.BindEvent(OnClickImage);

			GetObject((int)GameObjects.SecondScene).SetActive(false);
			GetObject((int)GameObjects.ThirdScene).SetActive(false);

			GetObject((int)GameObjects.BadFirstScene).SetActive(false);
			GetObject((int)GameObjects.BadSecondScene).SetActive(false);
		}
		else if(endingSet == 2)
        {
			//배드엔딩
			Managers.Sound.Play(Sound.Effect, "Sound_Ending", volume: 0.2f);

			this.endIndex = 0;

			BindObject(typeof(GameObjects));

			gameObject.BindEvent(OnClickImage);

			GetObject((int)GameObjects.BadSecondScene).SetActive(false);

			GetObject((int)GameObjects.FirstScene).SetActive(false);
			GetObject((int)GameObjects.SecondScene).SetActive(false);
			GetObject((int)GameObjects.ThirdScene).SetActive(false);
		}

		return true;
	}

	public void SetInfo(int ending)
    {
		this.endingSet = ending;
    }

	void OnClickImage()
    {
		if(endingSet == 1)
        {
			switch (endIndex)
			{
				case 0:
					GetObject((int)GameObjects.FirstScene).SetActive(false);
					GetObject((int)GameObjects.SecondScene).SetActive(true);
					endIndex++;
					break;
				case 1:
					GetObject((int)GameObjects.ThirdScene).SetActive(true);
					endIndex++;
					break;
				case 2:
					Managers.UI.ClosePopupUI(this);
					Managers.Sound.Play(Sound.Bgm, "Sound_MainPlayBGM", volume: 0.2f);
					break;
			}
        }else if(endingSet == 2)
        {
			switch (endIndex)
			{
				case 0:
					GetObject((int)GameObjects.BadSecondScene).SetActive(true);
					endIndex++;
					break;
				case 1:
					Managers.UI.ClosePopupUI(this);
					Application.Quit();

					break;
			}
		}

	}
}
