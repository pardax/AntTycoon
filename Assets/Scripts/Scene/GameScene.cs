using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        //���¸� ���Ӹ�� ���·� ����
        SceneType = Define.Scene.Game;

        //�ش� �޼��带 ���� ���� ������ �˸�
        Managers.UI.ShowPopupUI<UI_TitlePopup>();

        //Managers.UI.ShowPopupUI<UI_PlayPopup>();
        //Managers.UI.ShowPopupUI<UI_TestPopup>();
        //Managers.UI.ShowPopupUI<UI_EndingPopup>().SetInfo(1);

        return true;
    }
}
