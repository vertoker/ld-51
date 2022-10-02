using Features.Character.Service;
using UnityEngine;
using Zenject;

namespace Features.Character.Placeholder
{
    public class PlaceholderSpawner : MonoBehaviour
    {
        private CharacterSpawnService _characterSpawnService;

        [Inject]
        public void Construct(CharacterSpawnService characterSpawnService)
        {
            _characterSpawnService = characterSpawnService;
        }
        
        private void Start()
        {
            _characterSpawnService.SpawnCharacter(transform.position);
        }
    }
}
