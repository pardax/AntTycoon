using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Define;

public class UI_TestPopup : UI_Popup
{
    enum GameObjects
    {
        Location1, Location2, Location3, Location4,
        Location5, Location6, Location7, Location8
    }
    private string[] SpawnCharacters = { "Sinib", "Intern", "Daeri", "Gwajang", "Bujang", "Esa", "Sajang" };

    public override bool Init()
    {
        if (!base.Init())
            return false;

        BindObject(typeof(GameObjects));

        Debug.Log("Runned");
        Managers.Resource.Instantiate($"Characters/{SpawnCharacters[0]}", this.transform)
            .transform.position = GetObject((int)GameObjects.Location1).transform.position;
        Managers.Resource.Instantiate($"Characters/{SpawnCharacters[1]}", this.transform)
    .transform.position = GetObject((int)GameObjects.Location2).transform.position;
        Managers.Resource.Instantiate($"Characters/{SpawnCharacters[2]}", this.transform)
    .transform.position = GetObject((int)GameObjects.Location3).transform.position;
        Managers.Resource.Instantiate($"Characters/{SpawnCharacters[3]}", this.transform)
    .transform.position = GetObject((int)GameObjects.Location4).transform.position;
        Managers.Resource.Instantiate($"Characters/{SpawnCharacters[4]}", this.transform)
    .transform.position = GetObject((int)GameObjects.Location5).transform.position;
        Managers.Resource.Instantiate($"Characters/{SpawnCharacters[5]}", this.transform)
    .transform.position = GetObject((int)GameObjects.Location6).transform.position;



        return true;
    }


}
