using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
    public class Door : Activatable
    {
        [SerializeField] private float durationOpenClose = 0.5f;
        private Transform leftBlock, rightBlock;

        private readonly float leftStart = 0.25f, leftEnd = 0.75f;
        private readonly float rightStart = -0.25f, rightEnd = -0.75f;

        private void Awake()
        {
            leftBlock = transform.GetChild(0);
            rightBlock = transform.GetChild(1);
        }

        public override void Activate()
        {
            leftBlock.DOLocalMoveX(leftEnd, durationOpenClose);
            rightBlock.DOLocalMoveX(rightEnd, durationOpenClose);
        }
        public override void Deactivate()
        {
            leftBlock.DOLocalMoveX(leftStart, durationOpenClose);
            rightBlock.DOLocalMoveX(rightStart, durationOpenClose);
        }
    }
}