using Zenject;
using CustomSongTimeEvents.Models;

namespace CustomSongTimeEvents.Installers
{
    public class SongTimePlayerInstaller : Installer
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<SongTimeController>().FromNewComponentOnNewGameObject().AsCached().NonLazy();
        }
    }
}
