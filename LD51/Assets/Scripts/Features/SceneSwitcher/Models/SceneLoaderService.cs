using UnityEngine;
using Zenject;
using DG.Tweening;

namespace Features.SceneSwitcher.Models
{
    public class SceneLoaderService : IInitializable
    {
        [Inject(Id = "FadeCanvasGroup")] private CanvasGroup _fadeScreenGroup;

        public void Initialize()
        {
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