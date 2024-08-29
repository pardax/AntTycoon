#region 리스트 중복
/* 
    for(int i = 1; i < 3; i++)
    {
        GameObject antEntity = Managers.Resource.Instantiate($"Characters/{SpawnCharacters[0]}", this.transform);
        antEntity.transform.position = FilterLocation(i);

        AntEntity entity = new AntEntity(i, antEntity);
        _EntityList.Add(entity);
    }
    bool temp;
    if (_EntityList[0].Equals(_EntityList[0]))
    {
        temp = true;
    }
    else
    {
        temp = false;
    }
    Debug.Log(temp);
    Debug.Log("로케이션" + _EntityList[0].locationID.Equals(1));
    Debug.Log(_EntityList[1].locationID);

    Debug.Log(_EntityList.Count);
*/
#endregion
#region 리스트 테스트
/*
        List<int> tempList = new List<int>();
        tempList.Add(777);
        tempList.Add(888);
        tempList.Add(999);
        Debug.Log(tempList.Count);
        Debug.Log("ori : " + tempList[0]);
        Debug.Log("ori : " + tempList[1]);
        Debug.Log("ori : " + tempList[2]);

        tempList.RemoveAt(1);

        Debug.Log(tempList.Count);
        Debug.Log("0 : " + tempList[0]);
        Debug.Log("1 : " + tempList[1]);
        Debug.Log("2 : " + tempList[2]);
*/
#endregion
#region 개미 프리팹 생성
/*
    Managers.Resource.Instantiate($"Characters/{SpawnCharacters[0]}", this.transform).transform.position = GetObject((int)GameObjects.Location1).transform.position;
    Managers.Resource.Instantiate($"Characters/{SpawnCharacters[1]}", this.transform).transform.position = GetObject((int)GameObjects.Location2).transform.position;
    Managers.Resource.Instantiate($"Characters/{SpawnCharacters[2]}", this.transform).transform.position = GetObject((int)GameObjects.Location3).transform.position;
    Managers.Resource.Instantiate($"Characters/{SpawnCharacters[3]}", this.transform).transform.position = GetObject((int)GameObjects.Location4).transform.position;
    Managers.Resource.Instantiate($"Characters/{SpawnCharacters[4]}", this.transform).transform.position = GetObject((int)GameObjects.Location5).transform.position;
    Managers.Resource.Instantiate($"Characters/{SpawnCharacters[5]}", this.transform).transform.position = GetObject((int)GameObjects.Location6).transform.position;
    Managers.Resource.Instantiate($"Characters/{SpawnCharacters[6]}", this.transform).transform.position = GetObject((int)GameObjects.Location7).transform.position;
*/
/*
    GameObject antEntity = Managers.Resource.Instantiate($"Characters/{SpawnCharacters[0]}", this.transform);
    antEntity.transform.position = FilterLocation(1);

    AntEntity entity = new AntEntity(1, antEntity);
    _EntityList.Add(entity);

    AntEntity temp = _EntityList[0];

    _EntityList.RemoveAt(0);

    temp.destoryMe();

    Debug.Log(_EntityList.Count);
*/
/*
        GameObject antEntity = Managers.Resource.Instantiate($"Characters/{SpawnCharacters[0]}", this.transform);
        antEntity.transform.position = FilterLocation(1);
        var a = antEntity.AddComponent<PlayerController>();

        GameObject antEntity1 = Managers.Resource.Instantiate($"Characters/{SpawnCharacters[0]}", this.transform);
        antEntity1.transform.position = FilterLocation(1);
        var b = antEntity1.AddComponent<PlayerController>();
 */
#endregion
#region 리스트 정렬
/*
    for (int i = 0; i < 3; i++)
    {
        GameObject antEntity = Managers.Resource.Instantiate($"Characters/{SpawnCharacters[i]}", this.transform);
        antEntity.transform.position = FilterLocation(i + 1);
        AntEntity entity = new AntEntity(i + 1, antEntity);
        _EntityList.Add(entity);
    }

    Debug.Log(_EntityList[0].locationID);
    Debug.Log(_EntityList[1].locationID);
    Debug.Log(_EntityList[2].locationID);

    List<int> tempList = new List<int>();
    int tempInt;

    for (int i = 0; i < _EntityList.Count; i++)
    {
        tempList.Add(_EntityList[i].locationID);
    }

    Debug.Log("temp0 : " + tempList[0]);
    Debug.Log("temp1 : " + tempList[1]);
    Debug.Log("temp2 : " + tempList[2]);

    tempList.Sort();

    Debug.Log("temp0 : " + tempList[0]);
    Debug.Log("temp1 : " + tempList[1]);
    Debug.Log("temp2 : " + tempList[2]);
*/
#endregion