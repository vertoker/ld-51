using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Features.UI.Menu.Views
{
    public class RunningLineBehaviour : MonoBehaviour
    {
        private const float SpeedMultiplier = 25f;
        
        [SerializeField] private List<string> _displayLines;
        [SerializeField] private TMP_Text _currentLineDisplay;
        [SerializeField] private float _speed;
        
        private int _currentLineIndex;

        private void Start()
        {
            ExecuteRunningLine(_currentLineIndex);
        }

        private void ExecuteRunningLine(int line)
        {
            _currentLineDisplay.text = _displayLines[line];
            var offset = TextWidthApproximation(_displayLines[line], _currentLineDisplay.font,
                _currentLineDisplay.fontSize, FontStyles.Normal);
            var startPosition = new Vector2(-offset,
                _currentLineDisplay.rectTransform.anchoredPosition.y);
            var endPoint = new Vector3(Screen.width, _currentLineDisplay.transform.position.y, 0f);
            _currentLineDisplay.rectTransform.anchoredPosition = startPosition;

            _currentLineDisplay.rectTransform
                .DOMove(endPoint, Screen.width / (_speed * SpeedMultiplier))
                .OnComplete(() =>
                {
                    _currentLineIndex = _currentLineIndex < _displayLines.Count
                        ? _currentLineIndex + 1
                        : 0;
                    ExecuteRunningLine(_currentLineIndex);
                })
                .SetEase(Ease.Linear);
        }
        
        public float TextWidthApproximation (string text, TMP_FontAsset fontAsset, float fontSize, FontStyles style)
        {
            var pointSizeScale = fontSize / (fontAsset.faceInfo.pointSize * fontAsset.faceInfo.scale);
            var emScale = 1 * 0.01f;
 
            var styleSpacingAdjustment = (style & FontStyles.Bold) == FontStyles.Bold ? fontAsset.boldSpacing : 0;
            var normalSpacingAdjustment = fontAsset.normalSpacingOffset;
 
            var width = 0f;
 
            foreach (var unicode in text)
            {
                if (fontAsset.characterLookupTable.TryGetValue(unicode, out var character))
                    width += character.glyph.metrics.horizontalAdvance * pointSizeScale + 
                             (styleSpacingAdjustment + normalSpacingAdjustment) * emScale;
            }
 
            return width;
        }
    }
}
