using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    int _order = -20;

    public Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();

    public UI_Scene SceneUI { get; private set; }

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };

            return root;
        }
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        //ĵ���� ����� ĵ������ ������Ʈ ������
        Canvas canvas = Utils.GetOrAddComponent<Canvas>(go);
        //2D ����
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    //A : Ÿ��Ʋ �˾����� �� ���׸� Ÿ���� UI_TitlePopup
    public T ShowPopupUI<T>(string name = null, Transform parent = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        //A Ÿ��Ʋ�˾��̶�� �������� ������
        GameObject prefab = Managers.Resource.Load<GameObject>($"Prefabs/UI/Popup/{name}");

        //A Ÿ��Ʋ�˾��̶�� ��ũ��Ʈ�� ������
        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");

        //Ui_TitlePopup popup ������Ʈ ��ũ��Ʈ ����
        T popup = Utils.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);

        if (parent != null)
            go.transform.SetParent(parent);
        else if (SceneUI != null)
            go.transform.SetParent(SceneUI.transform);
        else
            go.transform.SetParent(Root.transform);

        go.transform.localScale = Vector3.one;
        go.transform.localPosition = prefab.transform.position;

        return popup;
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;

        if (_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed!");
            return;
        }

        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;

        UI_Popup popup = _popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;
        _order--;
    }
    //�˾��� ���̽��� �ξ����
    public T PeekPopupUI<T>() where T : UI_Popup
    {
        if (_popupStack.Count == 0)
            return null;
        //as , �� ��ȯ�� �����ϸ� ��ȯ, �ƴϸ� null
        return _popupStack.Peek() as T;
    }

    public void Init()
    {

    }
}
