
using UnityEngine;
using Data;

namespace Bootstrap 
{
    public class GlobalInitialization
    {
        [RuntimeInitializeOnLoadMethod]
        static void Init() 
        {
            AudioListener.volume =
                (PlayerPrefs.HasKey(GlobalConst.AudioVolumePref)) ?
                PlayerPrefs.GetFloat(GlobalConst.AudioVolumePref) :
                GlobalConst.StandardVolume;

            Debug.Log("INIT COMPLETE");

        }
    }
}

