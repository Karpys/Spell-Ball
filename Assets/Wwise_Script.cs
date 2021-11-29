using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wwise_Script : MonoBehaviour
{
    // Start is called before the first frame update
    public WwiseMultiplePlay Test;
    public int idPlay;
    public void TestSound()
    {
        Test.PlaySound(idPlay++);
    }

    [System.Serializable]
    public class WwiseSounds
    {
        public AK.Wwise.Event Sound;
        public bool KillPrevious;
        public bool IsReplable;
        public bool HasPlayed;

        public void SetHasPlayed(bool hasPlayed)
        {
            HasPlayed = hasPlayed;
        }
    }

    [System.Serializable]

    public struct WwiseMultiplePlay
    {
        public GameObject Self;
        public List<WwiseSounds> Sounds;

        public void PlaySound(int id)
        {
            if (Sounds[id].IsReplable == false && Sounds[id].HasPlayed)
            {
                return;
            }

            if (Sounds[id].KillPrevious)
            {
                Sounds[id-1].Sound.Stop(Self);
            }

            
            
            if (Sounds[id].IsReplable == false)
            {
                Sounds[id].HasPlayed = true;
            }

            
            Sounds[id].Sound.Post(Self);
        }
    }

}
