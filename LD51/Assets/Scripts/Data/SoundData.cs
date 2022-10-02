using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public enum SoundType
    {
        Footsteps,
        Action,
        Jump,
        Dash,
        Fall,
        TimeStop,
        TimeContinue,
    }
    
    [Serializable]
    public class SoundData
    {
        [SerializeField] private SoundType soundType;
        [SerializeField] private List<AudioClip> mixedSounds;

        public SoundType SoundType => soundType;
        public List<AudioClip> MixedSounds => mixedSounds;
    }
}