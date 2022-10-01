using Features.Character.Service;
using UnityEngine;
using Zenject;

namespace Features.Character._Placeholder
{
    public class PlaceholderSpawner : MonoBehaviour
    {
        [Inject]
        private CharacterSpawnService _characterSpawnService;
        private void Start()
        {
            _characterSpawnService.SpawnCharacter(Vector3.zero);
        }
    }
}
