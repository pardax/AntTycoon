using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        //상태를 게임모드 상태로 변경
        SceneType = Define.Scene.Game;

        //해당 메서드를 통해 씬의 시작을 알림
        Managers.UI.ShowPopupUI<UI_TitlePopup>();

        //Managers.UI.ShowPopupUI<UI_PlayPopup>();
        //Managers.UI.ShowPopupUI<UI_TestPopup>();
        //Managers.UI.ShowPopupUI<UI_EndingPopup>().SetInfo(1);

        return true;
    }
}
