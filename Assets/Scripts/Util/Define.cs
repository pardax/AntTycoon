using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum UIEvent
    {
        Click,
        Pressed,
        PointerDown,
        PointerUp,
    }

    public enum Scene 
    {
        Unknown,
        Dev,
        Game,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        Speech,
        Max
    }

    public const int TestButtonText = 19996;
    public const int StartButtonText = 19997;
    public const int ContinueButtonText = 19998;
    public const int CollectionButtonText = 19999;


}
