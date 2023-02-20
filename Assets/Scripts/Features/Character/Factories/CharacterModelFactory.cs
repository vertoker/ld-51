using Features.Character.Data;
using Features.Character.Models;
using Zenject;

namespace Features.Character.Factories
{
    public class CharacterModelFactory : IFactory<CharacterData, CharacterModel>
    {
        private readonly DiContainer _container;

        private CharacterModelFactory(DiContainer container)
        {
            _container = container;
        }
        
        public CharacterModel Create(CharacterData data)
        {
            return _container.Instantiate<CharacterModel>(new object[] {data});
        }
    }
}