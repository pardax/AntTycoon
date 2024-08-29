using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//(싱글톤) 메모리에 띄워놓고, 생성하면 해당 객체가 호출이 됨
public class Managers : MonoBehaviour
{
    public static Managers s_instance = null;

    //파라미터, 셋은 그대로 겟을 s_instance로 변경
    public static Managers instance { get { return s_instance; } }

    private static ResourceManager s_resourceManager = new ResourceManager();
    private static UIManager s_uiManager = new UIManager();
    private static DataManager s_dataManager = new DataManager();
    private static GameManagerEx s_gameManager = new GameManagerEx();
    private static SoundManager s_soundManager = new SoundManager();

    public static GameManagerEx Game { get { Init(); return s_gameManager; } }
    public static ResourceManager Resource { get { Init(); return s_resourceManager; } }
    public static DataManager Data { get { Init(); return s_dataManager; } }
    public static UIManager UI { get { Init(); return s_uiManager; } }
    public static SoundManager Sound { get { Init(); return s_soundManager; } }

    /*
    public static string GetText(int id)
    {
        if (Managers.Data.Texts.TryGetValue(id, out TextData value) == false)
            return "";

        return value.kor.Replace("{userName}", Managers.Game.Name);
    }
    */

    private void Start()
    {
        Debug.Log("Manager Running!!");
        Init();
    }

    private static void Init()
    {
        //만약 호출되었을떄 호출 전 인 상태인 경우 실행을 도모
        if (s_instance == null)
        {
            //★분석필요, 매니저를 구동시키는 곳
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
                go = new GameObject { name = "@Managers" };

            s_instance = Utils.GetOrAddComponent<Managers>(go);
            DontDestroyOnLoad(go);

            s_resourceManager.Init();
            s_soundManager.Init();

            /*
            s_dataManager.Init();
            s_adsManager.Init();
            s_iapManager.Init();
            s_sceneManager.Init();
            */

            Application.targetFrameRate = 60;
        }
    }


}
