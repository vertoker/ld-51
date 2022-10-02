using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "SoundConfig", menuName = "Config/Sound config")]
    public class SoundConfig : ScriptableObject
    {
        [SerializeField] private List<SoundData> sounds;

        public List<AudioClip> GetSoundsByType(SoundType type)
        {
            return sounds.FirstOrDefault(data => data.SoundType == type)?.MixedSounds;
        }
    }
}