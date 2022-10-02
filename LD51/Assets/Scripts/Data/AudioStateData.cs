using System.Collections.Generic;
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
            var result = _clips[_currentClip];
            _currentClip = _currentClip + 1 < _clips.Count 
                ? _currentClip + 1
                : 0;
            
            return result;
        }
    }
}