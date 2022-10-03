
using UnityEngine;
using Data;
using Zenject;

namespace Bootstrap 
{
    public class GlobalInitialization : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GlobalState>().AsSingle();
        }
    }
}

