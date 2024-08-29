using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//(�̱���) �޸𸮿� �������, �����ϸ� �ش� ��ü�� ȣ���� ��
public class Managers : MonoBehaviour
{
    public static Managers s_instance = null;

    //�Ķ����, ���� �״�� ���� s_instance�� ����
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
        //���� ȣ��Ǿ����� ȣ�� �� �� ������ ��� ������ ����
        if (s_instance == null)
        {
            //�ںм��ʿ�, �Ŵ����� ������Ű�� ��
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
