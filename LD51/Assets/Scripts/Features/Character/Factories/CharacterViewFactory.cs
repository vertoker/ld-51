using Features.Character.Configs;
using Features.Character.Models;
using Features.Character.Views;
using Zenject;

namespace Features.Character.Factories
{
    public class CharacterViewFactory : IFactory<CharacterModel, CharacterView>
    {
        private readonly CharacterConfig _characterConfig;
        private readonly DiContainer _container;
        
        private CharacterViewFactory(DiContainer container,
            CharacterConfig characterConfig)
        {
            _characterConfig = characterConfig;
            _container = container;
        }
        
        public CharacterView Create(CharacterModel model)
        {
            return _container.InstantiatePrefabForComponent<CharacterView>(
                _characterConfig.CharacterPrefab,
                new object[] {model});
        }
    }
}