using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScene : MonoBehaviour
{
    //씬 값 초기화
    public Define.Scene SceneType = Define.Scene.Unknown;

    protected bool _init = false;

    // Start is called before the first frame update
    private void Start()
    {
        Init();
    }

    
    protected virtual bool Init()
    {
        //이미 실행중인 경우 실패 반환
        if (_init)
            return false;

        //스켈레톤 활용
        GameObject go = GameObject.Find("EventSystem");
        if (go == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";

        return true;
    }

    public virtual void Clear() { }
}
