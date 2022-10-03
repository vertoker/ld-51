using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Data 
{
    public class GlobalState
    {
        public float mouseSense;

        public GlobalState()
        {
            mouseSense = PlayerPrefs.HasKey(GlobalConst.MouseSensitivityPref)
                    ? PlayerPrefs.GetFloat(GlobalConst.MouseSensitivityPref)
                    : GlobalConst.StandardSensitivity;
        }

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

