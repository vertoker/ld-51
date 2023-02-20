using System.Collections;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;

namespace Features.UI.Menu.Views
{
    public class GameNamePresenter : MonoBehaviour
    {
        private const float TimeToAddLetter = 0.15f;
        private const float TimeToAddSlowLetter = 0.35f;
        
        [SerializeField] private TMP_Text _nameDisplay;
        [SerializeField] private string _gameName;
        [SerializeField] private int _slowStartIndex;

        private int _currentIndex;
        private float _passedTime;
        
        private void Start()
        {
            Observable
                .EveryUpdate()
                .Subscribe(_ =>
                {
                    var requiredTime = _currentIndex < _slowStartIndex
                        ? TimeToAddLetter
                        : TimeToAddSlowLetter;
                    
                    _passedTime += Time.deltaTime;
                    if (_passedTime < requiredTime) return;
                    
                    _nameDisplay.text = ExtrudeText();
                    _passedTime = 0f;
                })
                .AddTo(this);
        }

        private string ExtrudeText()
        {
            var currentLine = _gameName.ToCharArray();
            var result = new char[_currentIndex + 1];
            
            for (var i = 0; i < _currentIndex; i++)
            {
                result[i] = currentLine[i];
            }
            
            if (_currentIndex < _gameName.Length)
                _currentIndex += 1;
            else
            {
                _currentIndex = 0;
                _nameDisplay.text = string.Empty;
            }
            
            return new string(result);
        }
    }
}
