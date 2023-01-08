using Zenject;
using CustomSongTimeEvents.Models;

namespace CustomSongTimeEvents.Installers
{
    public class SongTimeAppInstaller : Installer
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<SongTimeData>().AsSingle();
        }
    }
}
