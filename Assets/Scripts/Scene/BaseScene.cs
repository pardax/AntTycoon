using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScene : MonoBehaviour
{
    //�� �� �ʱ�ȭ
    public Define.Scene SceneType = Define.Scene.Unknown;

    protected bool _init = false;

    // Start is called before the first frame update
    private void Start()
    {
        Init();
    }

    
    protected virtual bool Init()
    {
        //�̹� �������� ��� ���� ��ȯ
        if (_init)
            return false;

        //���̷��� Ȱ��
        GameObject go = GameObject.Find("EventSystem");
        if (go == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";

        return true;
    }

    public virtual void Clear() { }
}
