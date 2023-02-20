using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Features.Core.Installer 
{
    public class CoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CoreData>().AsSingle();
            Container.Bind<CoreEvents>().AsSingle();
        }
    }
}

