using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class ResourceManager
{
    public Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();
    public Dictionary<string, SkeletonDataAsset> _skeletons = new Dictionary<string, SkeletonDataAsset>();

    public void Init()
    {

    }

	public T Load<T>(string path) where T : Object
    {
		if (typeof(T) == typeof(Sprite))
		{
			if (_sprites.TryGetValue(path, out Sprite sprite))
				return sprite as T;

			Sprite sp = Resources.Load<Sprite>(path);
			_sprites.Add(path, sp);
			return sp as T;
		}
		else if (typeof(T) == typeof(SkeletonDataAsset))
		{
			if (_skeletons.TryGetValue(path, out SkeletonDataAsset sprite))
				return sprite as T;

			SkeletonDataAsset sp = Resources.Load<SkeletonDataAsset>(path);
			_skeletons.Add(path, sp);
			return sp as T;
		}

		return Resources.Load<T>(path);
    }

	//오버로딩 Instantiate
	//경로
	public GameObject Instantiate(string path, Transform parent = null)
	{
		GameObject prefab = Load<GameObject>($"Prefabs/{path}");
		if (prefab == null)
		{
			Debug.Log($"Failed to load prefab : {path}");
			return null;
		}
		//프리팹을 아래의 오버로드 메서드에 전달
		return Instantiate(prefab, parent);
	}
	//프리팹
	public GameObject Instantiate(GameObject prefab, Transform parent = null)
	{
		GameObject go = Object.Instantiate(prefab, parent);
		go.name = prefab.name;
		return go;
	}

	public void Destroy(GameObject go)
	{
		if (go == null)
			return;

		Object.Destroy(go);
	}
}
