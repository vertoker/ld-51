using UnityEngine;
using Zenject;

namespace TextDataLib.Installers
{
    public class TextDataInstaller : MonoInstaller
    {
        [SerializeField]
        private TextDataConfig m_textDataConfig;

        public override void InstallBindings()
        {
            Container.Bind<TextDataConfig>().FromInstance(m_textDataConfig).
                AsSingle();
            Container.Bind<TextDataLoader>().AsSingle();
        }
    }
}
