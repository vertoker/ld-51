using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data
{
    public class AudioStateData
    {
        private int _currentClip;
        private readonly List<AudioClip> _clips;

        public AudioStateData(List<AudioClip> clips)
        {
            _clips = clips;
        }

        public AudioClip GetNext()
        {
            if (_clips == null) return null;
            
            var result = _clips[_currentClip];
            _currentClip = _currentClip + 1 < _clips.Count 
                ? _currentClip + 1
                : 0;
            
            return result;
        }

        public AudioClip GetRandom()
        {
            return _clips?.ElementAt(Random.Range(0, _clips.Count));
        }
        
    }
}