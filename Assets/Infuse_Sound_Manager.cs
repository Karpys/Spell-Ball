using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infuse_Sound_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 ComboLevelSlice;
    private static Infuse_Sound_Manager inst;
    public static Infuse_Sound_Manager Infuse { get => inst; }

    public AK.Wwise.Event Hit_Sound;

    void Awake()
    {
        if (Infuse != null && Infuse != this)
            Destroy(gameObject);

        inst = this;
    }

    public void PlayInfuseSound(ColorEnum ColorBall,int ComboLevel)
    {
        string Power = "Power";
        string Color;
        if (ComboLevel >= 0 && ComboLevel < ComboLevelSlice.y)
        {
            Power += "1";
        }else if (ComboLevel >= ComboLevelSlice.y && ComboLevel < ComboLevelSlice.z)
        {
            Power += "2";
        }
        else
        {
            Power += "3";
        }

        switch (ColorBall)
        {
            case ColorEnum.BLEU:
                Color = "Blue";
                break;
            case ColorEnum.GREEN:
                Color = "Green";
                break;
            case ColorEnum.ORANGE:
                Color = "Yellow";
                break;
            case ColorEnum.RED:
                Color = "Red";
                break;
            default:
                return;
        }

        string EventToCall = "Play_" + Color + "_" + Power;
        Debug.Log(EventToCall);
        AkSoundEngine.PostEvent(EventToCall,gameObject);
    }
}
