using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Configs;
using Data;
using UnityEngine;
using Zenject;

namespace Mechanics
{
    public class Door : Activatable
    {
        [SerializeField] private float durationOpenClose = 0.5f;
        [SerializeField] private AudioSource _doorSource;
        
        private Transform leftBlock, rightBlock;

        private AudioStateData _doorOpenData;
        private AudioStateData _doorCloseData;
        
        private readonly float leftStart = 0.25f, leftEnd = 0.75f;
        private readonly float rightStart = -0.25f, rightEnd = -0.75f;

        [Inject]
        public void Construct(SoundConfig soundConfig)
        {
            _doorOpenData = new AudioStateData(soundConfig.GetSoundsByType(SoundType.DoorOpen));
            _doorCloseData = new AudioStateData(soundConfig.GetSoundsByType(SoundType.DoorClose));
        }
        
        private void Awake()
        {
            leftBlock = transform.GetChild(0);
            rightBlock = transform.GetChild(1);
        }

        public override void Activate()
        {
            leftBlock.DOLocalMoveX(leftEnd, durationOpenClose);
            rightBlock.DOLocalMoveX(rightEnd, durationOpenClose);
            _doorSource.volume = PlayerPrefs.GetFloat(GlobalConst.AudioVolumePref);
            _doorSource.clip = _doorOpenData.GetRandom();
            _doorSource.Play();
        }
        public override void Deactivate()
        {
            leftBlock.DOLocalMoveX(leftStart, durationOpenClose);
            rightBlock.DOLocalMoveX(rightStart, durationOpenClose);
            _doorSource.volume = PlayerPrefs.GetFloat(GlobalConst.AudioVolumePref);
            _doorSource.clip = _doorCloseData.GetRandom();
            _doorSource.Play();
        }
    }
}