using System;
using UnityEngine;
using Zenject;
using DG.Tweening;
using UniRx;

namespace Features.SceneSwitcher.Models
{
    public class SceneLoaderService : IInitializable
    {
        [Inject(Id = "FadeCanvasGroup")] private CanvasGroup _fadeScreenGroup;
        [SerializeField] private ReactiveProperty<float> _property;

        public void Initialize()
        {
            _property = new ReactiveProperty<float>();
            
            Debug.Log("[SceneLoaderService] Initialize");
           
            SceneStartup();
        }

        private void SceneStartup()
        {
            if (_fadeScreenGroup) _fadeScreenGroup.alpha = 1;
            _fadeScreenGroup.DOFade(0, 1);
        }
    }
}